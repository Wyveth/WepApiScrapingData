using System.Collections.Concurrent;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Duels;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Play;

namespace WepApiScrapingData.Services.Game
{
    /// <summary>
    /// Duels de quiz entre comptes : matchmaking par mode et difficulté, défis privés par code ami,
    /// déroulement en temps réel (chrono commun, piloté par le serveur) ou asynchrone, score avec bonus de rapidité.
    /// </summary>
    public class DuelService
    {
        #region Constantes
        /// <summary>Points d'une bonne réponse, et bonus maximal de rapidité.</summary>
        public const int PointsPerAnswer = 100;
        public const int MaxSpeedBonus = 50;


        private const string CodeAlphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private const int CodeLength = 8;
        private const int MaxListedDuels = 30;
        #endregion

        #region Fields
        /// <summary>Verrous en mémoire : une seule instance d'API (deux réponses simultanées ne doivent pas avancer deux fois).</summary>
        private static readonly ConcurrentDictionary<long, SemaphoreSlim> DuelLocks = new();
        private static readonly SemaphoreSlim MatchmakingLock = new(1, 1);

        private readonly ScrapingContext _context;
        private readonly QuizzGameService _quizz;
        private readonly DuelNotifier _notifier;
        private readonly ILogger<DuelService> _logger;
        private readonly DuelOptions _options;
        private readonly WepApiScrapingData.Hubs.PresenceTracker _presence;
        #endregion

        #region Réglages (appsettings.json, section "Duel")
        /// <summary>Temps réel : compte à rebours avant la première question, pause entre deux questions, tolérance réseau.</summary>
        private int CountdownSeconds => Math.Max(0, _options.CountdownSeconds);
        private int PauseSeconds => Math.Max(0, _options.PauseSeconds);
        private int GraceSeconds => Math.Max(0, _options.GraceSeconds);

        private TimeSpan LiveSearchTimeout => TimeSpan.FromMinutes(Math.Max(1, _options.LiveSearchTimeoutMinutes));
        private TimeSpan LiveInvitationTimeout => TimeSpan.FromMinutes(Math.Max(1, _options.LiveInvitationTimeoutMinutes));
        private TimeSpan AsyncTimeout => TimeSpan.FromDays(Math.Max(1, _options.AsyncTimeoutDays));

        /// <summary>Durées par difficulté et délais, pour l'affichage côté client.</summary>
        public DuelSettingsDto GetSettings() => new()
        {
            QuestionSeconds = QuizzCatalog.DifficultyCodes.ToDictionary(code => code, code => _options.SecondsFor(code)),
            CountdownSeconds = CountdownSeconds,
            PauseSeconds = PauseSeconds,
            WinPoints = QuizzCatalog.DifficultyCodes.ToDictionary(code => code, code => _options.WinPointsFor(code))
        };
        #endregion

        #region Constructor
        public DuelService(ScrapingContext context, QuizzGameService quizz, DuelNotifier notifier, ILogger<DuelService> logger, IOptions<DuelOptions> options, WepApiScrapingData.Hubs.PresenceTracker presence)
        {
            _options = options.Value;
            _presence = presence;
            _context = context;
            _quizz = quizz;
            _notifier = notifier;
            _logger = logger;
        }
        #endregion

        #region Code ami
        public async Task<FriendCodeDto> GetFriendCodeAsync(string userId)
        {
            var profile = await GetOrCreateProfileAsync(userId);
            if (string.IsNullOrEmpty(profile.FriendCode))
            {
                profile.FriendCode = await NewFriendCodeAsync();
                await _context.SaveChangesAsync();
            }
            return new FriendCodeDto { Code = FormatCode(profile.FriendCode!) };
        }

        public async Task<FriendCodeDto> RegenerateFriendCodeAsync(string userId)
        {
            var profile = await GetOrCreateProfileAsync(userId);
            profile.FriendCode = await NewFriendCodeAsync();
            profile.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return new FriendCodeDto { Code = FormatCode(profile.FriendCode) };
        }
        #endregion

        #region Création et cycle de vie
        /// <summary>Rejoint le plus ancien duel en attente du même mode et de la même difficulté, ou en crée un.</summary>
        public async Task<DuelDto> SearchAsync(string userId, DuelSearchDto dto)
        {
            var mode = ParseMode(dto.Mode);
            var difficulty = ParseDifficulty(dto.Difficulty);

            Duel duel;
            await MatchmakingLock.WaitAsync();
            try
            {
                var now = DateTime.UtcNow;
                var liveLimit = now - LiveSearchTimeout;

                var existing = await _context.Duels.FirstOrDefaultAsync(d =>
                    d.Player1Id == userId && d.Status == DuelStatus.Searching && !d.IsPrivate
                    && d.Mode == mode && d.DifficultyCode == difficulty);
                if (existing != null && (mode == DuelMode.Async || existing.CreatedAt > liveLimit))
                    return await ToDtoAsync(existing.Id, userId);

                var candidate = await _context.Duels
                    .Where(d => d.Status == DuelStatus.Searching && !d.IsPrivate && d.Player2Id == null
                        && d.Mode == mode && d.DifficultyCode == difficulty && d.Player1Id != userId
                        && (mode == DuelMode.Async || d.CreatedAt > liveLimit))
                    .OrderBy(d => d.CreatedAt)
                    .FirstOrDefaultAsync();

                if (candidate != null)
                {
                    await JoinAndStartAsync(candidate, userId);
                    duel = candidate;
                }
                else
                {
                    var quizz = await _quizz.GenerateAsync(new QuizzCreateDto { Difficulties = new List<string> { difficulty } }, userId);
                    duel = NewDuel(userId, mode, difficulty, null, quizz.Id);
                    duel.Status = DuelStatus.Searching;
                    _context.Duels.Add(duel);
                    await _context.SaveChangesAsync();
                }
            }
            finally
            {
                MatchmakingLock.Release();
            }

            await _notifier.DuelUpdatedAsync(duel);
            return await ToDtoAsync(duel.Id, userId);
        }

        /// <summary>Envoie un défi privé au joueur qui possède ce code ami.</summary>
        public async Task<DuelDto> ChallengeAsync(string userId, DuelChallengeDto dto)
        {
            var mode = ParseMode(dto.Mode);
            var difficulty = ParseDifficulty(dto.Difficulty);
            var code = NormalizeCode(dto.FriendCode);

            var target = await _context.UserProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.FriendCode == code)
                ?? throw new QuizzGameException(StatusCodes.Status404NotFound, "Aucun joueur ne possède ce code.");
            if (target.UserId == userId)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Vous ne pouvez pas vous défier vous-même.");

            var generations = (dto.Generations ?? new List<int>()).Where(g => g >= 0 && g <= 9).Distinct().OrderBy(g => g).ToList();
            var quizz = await _quizz.GenerateAsync(new QuizzCreateDto { Generations = generations, Difficulties = new List<string> { difficulty } }, userId);

            var duel = NewDuel(userId, mode, difficulty, generations, quizz.Id);
            duel.Status = DuelStatus.Invited;
            duel.IsPrivate = true;
            duel.Player2Id = target.UserId;
            _context.Duels.Add(duel);
            await _context.SaveChangesAsync();

            await _notifier.InvitationsChangedAsync(target.UserId);
            await _notifier.DuelUpdatedAsync(duel);
            return await ToDtoAsync(duel.Id, userId);
        }

        public async Task<DuelDto> AcceptAsync(string userId, Guid id)
        {
            var duel = await LoadDuelAsync(id, userId);
            await WithDuelLockAsync(duel.Id, async () =>
            {
                await _context.Entry(duel).ReloadAsync();
                if (duel.Status != DuelStatus.Invited || duel.Player2Id != userId)
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce défi n'est plus disponible.");
                if (IsExpired(duel, DateTime.UtcNow))
                {
                    duel.Status = DuelStatus.Expired;
                    await _context.SaveChangesAsync();
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce défi a expiré.");
                }
                await JoinAndStartAsync(duel, userId);
            });

            await _notifier.InvitationsChangedAsync(userId);
            await _notifier.DuelUpdatedAsync(duel);
            return await ToDtoAsync(duel.Id, userId);
        }

        public async Task<DuelDto> DeclineAsync(string userId, Guid id)
        {
            var duel = await LoadDuelAsync(id, userId);
            if (duel.Status != DuelStatus.Invited || duel.Player2Id != userId)
                throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce défi n'est plus disponible.");

            duel.Status = DuelStatus.Declined;
            duel.FinishedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            await _notifier.InvitationsChangedAsync(userId);
            await _notifier.DuelUpdatedAsync(duel);
            return await ToDtoAsync(duel.Id, userId);
        }

        /// <summary>Annule une recherche ou un défi envoyé, tant que l'adversaire n'a pas rejoint.</summary>
        public async Task<DuelDto> CancelAsync(string userId, Guid id)
        {
            var duel = await LoadDuelAsync(id, userId);
            if (duel.Player1Id != userId || (duel.Status != DuelStatus.Searching && duel.Status != DuelStatus.Invited))
                throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce duel ne peut plus être annulé.");

            duel.Status = DuelStatus.Cancelled;
            duel.FinishedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            if (duel.Player2Id != null)
                await _notifier.InvitationsChangedAsync(duel.Player2Id);
            await _notifier.DuelUpdatedAsync(duel);
            return await ToDtoAsync(duel.Id, userId);
        }

        /// <summary>Abandon : l'adversaire gagne.</summary>
        public async Task<DuelDto> ForfeitAsync(string userId, Guid id)
        {
            var duel = await LoadDuelAsync(id, userId);
            await WithDuelLockAsync(duel.Id, async () =>
            {
                await _context.Entry(duel).ReloadAsync();
                if (duel.Status != DuelStatus.InProgress)
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce duel n'est pas en cours.");

                await RefreshScoresAsync(duel);
                duel.ForfeitedById = userId;
                duel.WinnerId = duel.Player1Id == userId ? duel.Player2Id : duel.Player1Id;
                duel.Status = DuelStatus.Finished;
                duel.FinishedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            });

            await _notifier.DuelUpdatedAsync(duel);
            return await ToDtoAsync(duel.Id, userId);
        }
        #endregion

        #region Lecture
        public async Task<DuelDto> GetAsync(string userId, Guid id)
        {
            var duel = await LoadDuelAsync(id, userId);
            return await ToDtoAsync(duel.Id, userId);
        }

        public async Task<List<DuelDto>> ListMineAsync(string userId)
        {
            var ids = await _context.Duels.AsNoTracking()
                .Where(d => d.Player1Id == userId || d.Player2Id == userId)
                .OrderByDescending(d => d.CreatedAt)
                .Take(MaxListedDuels)
                .Select(d => d.Id)
                .ToListAsync();
            return await ToDtosAsync(ids, userId);
        }

        /// <summary>Duel en cours du joueur (le front bloque alors le Pokédex), ou null.</summary>
        public async Task<DuelDto?> ActiveAsync(string userId)
        {
            var id = await _context.Duels.AsNoTracking()
                .Where(d => d.Status == DuelStatus.InProgress && (d.Player1Id == userId || d.Player2Id == userId))
                .OrderByDescending(d => d.StartedAt)
                .Select(d => (long?)d.Id)
                .FirstOrDefaultAsync();
            return id == null ? null : await ToDtoAsync(id.Value, userId);
        }

        /// <summary>Défis reçus en attente de réponse.</summary>
        public async Task<List<DuelDto>> InvitationsAsync(string userId)
        {
            var now = DateTime.UtcNow;
            var invited = await _context.Duels.AsNoTracking()
                .Where(d => d.Status == DuelStatus.Invited && d.Player2Id == userId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
            return await ToDtosAsync(invited.Where(d => !IsExpired(d, now)).Select(d => d.Id).ToList(), userId);
        }

        public async Task<DuelStatsDto> StatsAsync(string userId)
        {
            var finished = await _context.Duels.AsNoTracking()
                .Where(d => d.Status == DuelStatus.Finished && (d.Player1Id == userId || d.Player2Id == userId))
                .Select(d => d.WinnerId)
                .ToListAsync();
            return new DuelStatsDto
            {
                Played = finished.Count,
                Wins = finished.Count(w => w == userId),
                Draws = finished.Count(w => w == null),
                Losses = finished.Count(w => w != null && w != userId)
            };
        }
        #endregion

        #region Classement
        /// <summary>
        /// Classement des duels terminés : victoires pondérées par la difficulté (réglage WinPoints) et points marqués cumulés,
        /// sur tous les temps ou sur le mois en cours (UTC). Les abandons comptent comme des victoires pour l'adversaire.
        /// </summary>
        public async Task<LeaderboardDto> LeaderboardAsync(string? userId, string? period, string? sort, int top)
        {
            var monthly = string.Equals(period, "month", StringComparison.OrdinalIgnoreCase);
            var byPoints = string.Equals(sort, "points", StringComparison.OrdinalIgnoreCase);
            top = Math.Clamp(top, 1, 100);

            var now = DateTime.UtcNow;
            DateTime? since = monthly ? new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc) : null;

            var query = _context.Duels.AsNoTracking().Where(d => d.Status == DuelStatus.Finished && d.Player2Id != null);
            if (since != null)
                query = query.Where(d => d.FinishedAt >= since);

            var duels = await query
                .Select(d => new { d.Player1Id, d.Player2Id, d.Score1, d.Score2, d.WinnerId, d.DifficultyCode })
                .ToListAsync();

            var rows = new Dictionary<string, LeaderboardEntryDto>();
            LeaderboardEntryDto Row(string id) => rows.TryGetValue(id, out var row) ? row : rows[id] = new LeaderboardEntryDto();

            foreach (var duel in duels)
            {
                foreach (var (playerId, points) in new[] { (duel.Player1Id, duel.Score1), (duel.Player2Id!, duel.Score2) })
                {
                    var row = Row(playerId);
                    row.Played++;
                    row.DuelPoints += points;
                    if (duel.WinnerId == null) row.Draws++;
                    else if (duel.WinnerId == playerId)
                    {
                        row.Wins++;
                        row.RankingPoints += _options.WinPointsFor(duel.DifficultyCode);
                    }
                    else row.Losses++;
                }
            }

            var ordered = (byPoints
                    ? rows.OrderByDescending(r => r.Value.DuelPoints).ThenByDescending(r => r.Value.RankingPoints)
                    : rows.OrderByDescending(r => r.Value.RankingPoints).ThenByDescending(r => r.Value.DuelPoints))
                .ThenByDescending(r => r.Value.Wins)
                .ToList();

            // Rang partagé en cas d'égalité parfaite sur les deux critères
            for (int i = 0; i < ordered.Count; i++)
            {
                var current = ordered[i].Value;
                var previous = i > 0 ? ordered[i - 1].Value : null;
                current.Rank = previous != null && previous.RankingPoints == current.RankingPoints && previous.DuelPoints == current.DuelPoints
                    ? previous.Rank
                    : i + 1;
                current.IsMe = ordered[i].Key == userId;
            }

            var shown = ordered.Take(top).ToList();
            var mine = userId != null && rows.ContainsKey(userId) && !shown.Any(r => r.Key == userId)
                ? ordered.First(r => r.Key == userId)
                : (KeyValuePair<string, LeaderboardEntryDto>?)null;

            var ids = shown.Select(r => r.Key).Concat(mine != null ? new[] { mine.Value.Key } : Array.Empty<string>()).ToList();
            var names = await _context.Users.AsNoTracking()
                .Where(u => ids.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToDictionaryAsync(u => u.Id, u => u.UserName);

            foreach (var (id, row) in shown.Concat(mine != null ? new[] { mine.Value } : Array.Empty<KeyValuePair<string, LeaderboardEntryDto>>()))
            {
                row.UserName = names.TryGetValue(id, out var name) ? name : null;
                row.IsOnline = _presence.IsOnline(id);
            }

            return new LeaderboardDto
            {
                Period = monthly ? "month" : "all",
                Sort = byPoints ? "points" : "wins",
                Since = since,
                Entries = shown.Select(r => r.Value).ToList(),
                Me = mine?.Value
            };
        }
        #endregion

        #region Jeu
        public async Task<QuestionDto> GetQuestionAsync(string userId, Guid id, int order, string? lang)
        {
            var duel = await LoadDuelAsync(id, userId);
            var quizzGuid = await EnsureCanSeeQuestionAsync(duel, userId, order);
            return await _quizz.GetQuestionAsync(quizzGuid, order, lang, userId, viaDuel: true);
        }

        public async Task<List<string>> GetMediaPathsAsync(string userId, Guid id, int order, bool sound)
        {
            var duel = await LoadDuelAsync(id, userId);
            var quizzGuid = await EnsureCanSeeQuestionAsync(duel, userId, order);
            return await _quizz.GetSubjectMediaPathsAsync(quizzGuid, order, sound, userId, viaDuel: true);
        }

        public async Task<AnswerResultDto> AnswerAsync(string userId, Guid id, int order, AnswerSubmitDto submit)
        {
            var duel = await LoadDuelAsync(id, userId);
            AnswerResultDto? result = null;

            await WithDuelLockAsync(duel.Id, async () =>
            {
                await _context.Entry(duel).ReloadAsync();
                var quizzGuid = await EnsureCanSeeQuestionAsync(duel, userId, order);
                var now = DateTime.UtcNow;

                if (duel.Mode == DuelMode.Live)
                {
                    if (duel.Status != DuelStatus.InProgress || order != duel.CurrentOrder)
                        throw new QuizzGameException(StatusCodes.Status409Conflict, "Cette question n'est plus en cours.");
                    if (duel.QuestionStartedAt == null || now > duel.QuestionStartedAt.Value.AddSeconds(duel.QuestionSeconds + GraceSeconds))
                        throw new QuizzGameException(StatusCodes.Status409Conflict, "Temps écoulé.");
                }

                result = await _quizz.SubmitAsync(quizzGuid, order, submit, userId, viaDuel: true);

                if (duel.Mode == DuelMode.Live)
                {
                    // Les deux ont répondu : on passe à la suite sans attendre la fin du chrono
                    var opponentQuizzId = duel.Player1Id == userId ? duel.Quizz2Id : duel.Quizz1Id;
                    var opponentDone = opponentQuizzId != null && (await LoadQuestionsAsync(opponentQuizzId.Value))
                        .Any(q => q.Order == order && q.Done);
                    if (opponentDone)
                        await EndLiveQuestionAsync(duel);
                }
                else
                {
                    await RefreshScoresAsync(duel);
                    if (duel.Status == DuelStatus.InProgress && await BothQuizzDoneAsync(duel))
                        await FinishAsync(duel);
                    else
                        await _context.SaveChangesAsync();
                }
            });

            await _notifier.DuelUpdatedAsync(duel);
            return result!;
        }

        /// <summary>
        /// Appelé chaque seconde par DuelTimerService : clôt les questions dont le chrono est écoulé (temps réel)
        /// et fait expirer les recherches et défis sans réponse.
        /// </summary>
        public async Task TickAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var liveIds = await _context.Duels.AsNoTracking()
                .Where(d => d.Mode == DuelMode.Live && d.Status == DuelStatus.InProgress && d.QuestionStartedAt != null)
                .Select(d => new { d.Id, d.QuestionStartedAt, d.QuestionSeconds })
                .ToListAsync(cancellationToken);

            foreach (var live in liveIds.Where(l => l.QuestionStartedAt!.Value.AddSeconds(l.QuestionSeconds + GraceSeconds) < now))
            {
                var duel = await _context.Duels.FirstAsync(d => d.Id == live.Id, cancellationToken);
                var advanced = false;
                await WithDuelLockAsync(duel.Id, async () =>
                {
                    await _context.Entry(duel).ReloadAsync(cancellationToken);
                    if (duel.Status == DuelStatus.InProgress && duel.QuestionStartedAt != null
                        && duel.QuestionStartedAt.Value.AddSeconds(duel.QuestionSeconds + GraceSeconds) < DateTime.UtcNow)
                    {
                        await EndLiveQuestionAsync(duel);
                        advanced = true;
                    }
                });
                if (advanced)
                    await _notifier.DuelUpdatedAsync(duel);
            }

            var pending = await _context.Duels
                .Where(d => d.Status == DuelStatus.Searching || d.Status == DuelStatus.Invited)
                .ToListAsync(cancellationToken);
            var expired = pending.Where(d => IsExpired(d, now)).ToList();
            if (expired.Count == 0)
                return;

            foreach (var duel in expired)
            {
                duel.Status = DuelStatus.Expired;
                duel.FinishedAt = now;
            }
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var duel in expired)
            {
                if (duel.Player2Id != null)
                    await _notifier.InvitationsChangedAsync(duel.Player2Id);
                await _notifier.DuelUpdatedAsync(duel);
            }
        }
        #endregion

        #region Private Methods
        private Duel NewDuel(string userId, DuelMode mode, string difficulty, List<int>? generations, long quizzId) => new()
        {
            QuestionSeconds = _options.SecondsFor(difficulty),
            Guid = Guid.NewGuid(),
            Mode = mode,
            DifficultyCode = difficulty,
            Generations = generations == null || generations.Count == 0 ? null : string.Join(',', generations),
            Player1Id = userId,
            Quizz1Id = quizzId,
            CreatedAt = DateTime.UtcNow
        };

        private async Task<Duel> LoadDuelAsync(Guid id, string userId)
        {
            var duel = await _context.Duels.FirstOrDefaultAsync(d => d.Guid == id);
            // 404 pour un duel dont on n'est pas participant : on ne révèle pas son existence
            if (duel == null || (duel.Player1Id != userId && duel.Player2Id != userId))
                throw new QuizzGameException(StatusCodes.Status404NotFound, "Duel introuvable.");
            return duel;
        }

        private async Task JoinAndStartAsync(Duel duel, string userId)
        {
            var copy = await _quizz.CloneAsync(duel.Quizz1Id!.Value, userId);
            duel.Player2Id = userId;
            duel.Quizz2Id = copy.Id;
            duel.Status = DuelStatus.InProgress;
            duel.StartedAt = DateTime.UtcNow;

            if (duel.Mode == DuelMode.Live)
            {
                duel.CurrentOrder = 1;
                duel.QuestionStartedAt = DateTime.UtcNow.AddSeconds(CountdownSeconds);
                await SetShownAtAsync(duel, 1, duel.QuestionStartedAt.Value);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>Temps réel : marque la question comme non répondue pour les retardataires, met à jour les scores, puis enchaîne.</summary>
        private async Task EndLiveQuestionAsync(Duel duel)
        {
            var order = duel.CurrentOrder;
            foreach (var quizzId in new[] { duel.Quizz1Id, duel.Quizz2Id }.Where(q => q != null).Select(q => q!.Value))
            {
                var question = (await LoadQuestionsAsync(quizzId)).FirstOrDefault(q => q.Order == order);
                if (question != null && !question.Done)
                {
                    question.Done = true;
                    question.DateModification = DateTime.UtcNow;
                }
            }

            await RefreshScoresAsync(duel);

            var total = duel.Quizz1Id == null ? 0 : (await LoadQuestionsAsync(duel.Quizz1Id.Value)).Count;
            if (order >= total)
            {
                await FinishAsync(duel);
                return;
            }

            duel.CurrentOrder = order + 1;
            duel.QuestionStartedAt = DateTime.UtcNow.AddSeconds(PauseSeconds);
            await SetShownAtAsync(duel, duel.CurrentOrder, duel.QuestionStartedAt.Value);
            await _context.SaveChangesAsync();
        }

        private async Task FinishAsync(Duel duel)
        {
            var (score1, time1) = await ComputeScoreAsync(duel, duel.Quizz1Id);
            var (score2, time2) = await ComputeScoreAsync(duel, duel.Quizz2Id);
            duel.Score1 = score1;
            duel.Score2 = score2;

            // Égalité de points : le plus rapide au total l'emporte ; sinon match nul
            if (score1 != score2)
                duel.WinnerId = score1 > score2 ? duel.Player1Id : duel.Player2Id;
            else if (time1 != time2)
                duel.WinnerId = time1 < time2 ? duel.Player1Id : duel.Player2Id;
            else
                duel.WinnerId = null;

            foreach (var quizzId in new[] { duel.Quizz1Id, duel.Quizz2Id }.Where(q => q != null).Select(q => q!.Value))
            {
                var quizz = await _context.Quizzs.FirstAsync(q => q.Id == quizzId);
                quizz.Done = true;
            }

            duel.Status = DuelStatus.Finished;
            duel.FinishedAt = DateTime.UtcNow;
            duel.QuestionStartedAt = null;
            await _context.SaveChangesAsync();
        }

        private async Task RefreshScoresAsync(Duel duel)
        {
            duel.Score1 = (await ComputeScoreAsync(duel, duel.Quizz1Id)).Score;
            duel.Score2 = (await ComputeScoreAsync(duel, duel.Quizz2Id)).Score;
        }

        /// <summary>
        /// Score : 100 points par bonne réponse + jusqu'à 50 points selon la rapidité (linéaire sur la durée de la question).
        /// Temps total : somme des temps de réponse, une question sans réponse compte pour la durée entière.
        /// </summary>
        private async Task<(int Score, double TotalSeconds)> ComputeScoreAsync(Duel duel, long? quizzId)
        {
            if (quizzId == null) return (0, 0);

            int score = 0;
            double totalSeconds = 0;
            foreach (var question in await LoadQuestionsAsync(quizzId.Value))
            {
                if (!question.Done) continue;
                double? elapsed = question.ShownAt != null && question.AnsweredAt != null
                    ? Math.Max(0, (question.AnsweredAt.Value - question.ShownAt.Value).TotalSeconds)
                    : null;
                totalSeconds += Math.Min(elapsed ?? duel.QuestionSeconds, duel.QuestionSeconds);

                if (QuizzGameService.IsQuestionCorrect(question))
                {
                    var ratio = elapsed == null ? 0 : Math.Clamp(1 - elapsed.Value / duel.QuestionSeconds, 0, 1);
                    score += PointsPerAnswer + (int)Math.Round(MaxSpeedBonus * ratio);
                }
            }
            return (score, totalSeconds);
        }

        private async Task<bool> BothQuizzDoneAsync(Duel duel)
        {
            if (duel.Quizz1Id == null || duel.Quizz2Id == null) return false;
            return (await LoadQuestionsAsync(duel.Quizz1Id.Value)).All(q => q.Done)
                && (await LoadQuestionsAsync(duel.Quizz2Id.Value)).All(q => q.Done);
        }

        private async Task SetShownAtAsync(Duel duel, int order, DateTime shownAt)
        {
            foreach (var quizzId in new[] { duel.Quizz1Id, duel.Quizz2Id }.Where(q => q != null).Select(q => q!.Value))
            {
                var question = (await LoadQuestionsAsync(quizzId)).FirstOrDefault(q => q.Order == order);
                if (question != null)
                    question.ShownAt = shownAt;
            }
        }

        /// <summary>Questions d'un quiz (suivies par le contexte, partagé avec QuizzGameService), triées par ordre.</summary>
        private async Task<List<Question>> LoadQuestionsAsync(long quizzId)
        {
            var links = await _context.Quizz_Question
                .Where(qq => qq.QuizzId == quizzId)
                .Include(qq => qq.Question).ThenInclude(q => q!.Question_Answers!).ThenInclude(qa => qa.Answer)
                .AsSplitQuery()
                .ToListAsync();
            return links.Where(l => l.Question != null).Select(l => l.Question!).OrderBy(q => q.Order).ToList();
        }

        /// <summary>Vérifie que le joueur peut voir cette question et renvoie le Guid de son quiz.</summary>
        private async Task<Guid> EnsureCanSeeQuestionAsync(Duel duel, string userId, int order)
        {
            var quizzId = duel.Player1Id == userId ? duel.Quizz1Id : duel.Quizz2Id;
            if (quizzId == null)
                throw new QuizzGameException(StatusCodes.Status409Conflict, "Le duel n'a pas encore commencé.");

            var questions = await LoadQuestionsAsync(quizzId.Value);
            var question = questions.FirstOrDefault(q => q.Order == order)
                ?? throw new QuizzGameException(StatusCodes.Status404NotFound, "Question introuvable.");

            if (!question.Done)
            {
                if (duel.Mode == DuelMode.Live)
                {
                    if (duel.Status != DuelStatus.InProgress || order != duel.CurrentOrder)
                        throw new QuizzGameException(StatusCodes.Status409Conflict, "Cette question n'est pas en cours.");
                    if (duel.QuestionStartedAt == null || DateTime.UtcNow < duel.QuestionStartedAt.Value)
                        throw new QuizzGameException(StatusCodes.Status409Conflict, "La question n'a pas encore commencé.");
                }
                else
                {
                    var playable = duel.Status == DuelStatus.InProgress
                        || (duel.Player1Id == userId && (duel.Status == DuelStatus.Searching || duel.Status == DuelStatus.Invited));
                    if (!playable)
                        throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce duel n'est pas jouable.");
                    // Pas de saut en avant : la première question sans réponse d'abord
                    var next = questions.FirstOrDefault(q => !q.Done)?.Order;
                    if (next != null && order > next)
                        throw new QuizzGameException(StatusCodes.Status409Conflict, "Répondez d'abord à la question précédente.");
                }
            }

            return await _context.Quizzs.Where(q => q.Id == quizzId).Select(q => q.Guid).FirstAsync();
        }

        private bool IsExpired(Duel duel, DateTime now)
        {
            if (duel.Status != DuelStatus.Searching && duel.Status != DuelStatus.Invited)
                return false;
            var timeout = duel.Mode == DuelMode.Async ? AsyncTimeout
                : duel.Status == DuelStatus.Invited ? LiveInvitationTimeout
                : LiveSearchTimeout;
            return duel.CreatedAt + timeout < now;
        }

        private async Task WithDuelLockAsync(long duelId, Func<Task> action)
        {
            var semaphore = DuelLocks.GetOrAdd(duelId, _ => new SemaphoreSlim(1, 1));
            await semaphore.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async Task<DuelDto> ToDtoAsync(long duelId, string userId) =>
            (await ToDtosAsync(new List<long> { duelId }, userId)).First();

        private async Task<List<DuelDto>> ToDtosAsync(List<long> duelIds, string userId)
        {
            if (duelIds.Count == 0) return new List<DuelDto>();

            var duels = await _context.Duels.AsNoTracking()
                .Where(d => duelIds.Contains(d.Id))
                .Include(d => d.Player1)
                .Include(d => d.Player2)
                .ToListAsync();

            var quizzIds = duels.SelectMany(d => new[] { d.Quizz1Id, d.Quizz2Id }).Where(q => q != null).Select(q => q!.Value).Distinct().ToList();
            var progress = await _context.Quizz_Question.AsNoTracking()
                .Where(qq => quizzIds.Contains(qq.QuizzId))
                .Select(qq => new { qq.QuizzId, qq.Question!.Order, qq.Question.Done })
                .ToListAsync();
            var byQuizz = progress.GroupBy(p => p.QuizzId).ToDictionary(g => g.Key, g => g.OrderBy(x => x.Order).ToList());

            var now = DateTime.UtcNow;
            return duelIds
                .Select(id => duels.First(d => d.Id == id))
                .Select(d =>
                {
                    bool isPlayer1 = d.Player1Id == userId;
                    var myQuizz = isPlayer1 ? d.Quizz1Id : d.Quizz2Id;
                    var opponentQuizz = isPlayer1 ? d.Quizz2Id : d.Quizz1Id;
                    var mine = myQuizz != null && byQuizz.TryGetValue(myQuizz.Value, out var m) ? m : null;
                    var theirs = opponentQuizz != null && byQuizz.TryGetValue(opponentQuizz.Value, out var t) ? t : null;
                    var opponentUser = isPlayer1 ? d.Player2 : d.Player1;
                    int total = mine?.Count ?? (d.Quizz1Id != null && byQuizz.TryGetValue(d.Quizz1Id.Value, out var q1) ? q1.Count : 0);
                    int? nextOrder = mine?.FirstOrDefault(x => !x.Done)?.Order;

                    bool canPlay = nextOrder != null && (d.Mode == DuelMode.Live
                        ? d.Status == DuelStatus.InProgress
                        : d.Status == DuelStatus.InProgress || (isPlayer1 && (d.Status == DuelStatus.Searching || d.Status == DuelStatus.Invited)));

                    string? result = null;
                    if (d.Status == DuelStatus.Finished)
                        result = d.WinnerId == null ? "Draw" : d.WinnerId == userId ? "Win" : "Loss";

                    return new DuelDto
                    {
                        Id = d.Guid,
                        Mode = d.Mode.ToString(),
                        Status = d.Status.ToString(),
                        Difficulty = d.DifficultyCode,
                        Generations = string.IsNullOrEmpty(d.Generations) ? new List<int>() : d.Generations.Split(',').Select(int.Parse).ToList(),
                        IsPrivate = d.IsPrivate,
                        IsChallenger = isPlayer1,
                        Me = new DuelPlayerDto
                        {
                            UserName = (isPlayer1 ? d.Player1 : d.Player2)?.UserName,
                            Score = isPlayer1 ? d.Score1 : d.Score2,
                            Answered = mine?.Count(x => x.Done) ?? 0,
                            Done = mine != null && mine.All(x => x.Done)
                        },
                        Opponent = opponentUser == null ? null : new DuelPlayerDto
                        {
                            UserName = opponentUser.UserName,
                            Score = isPlayer1 ? d.Score2 : d.Score1,
                            Answered = theirs?.Count(x => x.Done) ?? 0,
                            Done = theirs != null && theirs.All(x => x.Done)
                        },
                        Total = total,
                        NextOrder = nextOrder,
                        CanPlay = canPlay,
                        CurrentOrder = d.CurrentOrder,
                        QuestionStartsAt = d.Mode == DuelMode.Live ? d.QuestionStartedAt : null,
                        QuestionEndsAt = d.Mode == DuelMode.Live ? d.QuestionStartedAt?.AddSeconds(d.QuestionSeconds) : null,
                        QuestionSeconds = d.QuestionSeconds,
                        ServerTime = now,
                        Result = result,
                        Forfeit = d.ForfeitedById != null,
                        CreatedAt = d.CreatedAt,
                        StartedAt = d.StartedAt,
                        FinishedAt = d.FinishedAt
                    };
                })
                .ToList();
        }

        private async Task<UserProfile> GetOrCreateProfileAsync(string userId)
        {
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
            {
                profile = new UserProfile { UserId = userId };
                _context.UserProfiles.Add(profile);
            }
            return profile;
        }

        private async Task<string> NewFriendCodeAsync()
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                var chars = new char[CodeLength];
                for (int i = 0; i < CodeLength; i++)
                    chars[i] = CodeAlphabet[RandomNumberGenerator.GetInt32(CodeAlphabet.Length)];
                var code = new string(chars);
                if (!await _context.UserProfiles.AnyAsync(p => p.FriendCode == code))
                    return code;
            }
            throw new QuizzGameException(StatusCodes.Status500InternalServerError, "Impossible de générer un code.");
        }

        private static string NormalizeCode(string? code) =>
            new string((code ?? string.Empty).ToUpperInvariant().Where(char.IsLetterOrDigit).ToArray());

        private static string FormatCode(string code) =>
            code.Length == CodeLength ? $"{code[..4]}-{code[4..]}" : code;

        /// <summary>Mode « à son rythme » désactivé pour l'instant (le code reste en place pour plus tard).</summary>
        private const bool AsyncModeEnabled = false;

        private static DuelMode ParseMode(string? mode)
        {
            if (!Enum.TryParse<DuelMode>(mode, ignoreCase: true, out var parsed) || !Enum.IsDefined(parsed))
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Mode de duel inconnu (Live ou Async).");
            if (parsed == DuelMode.Async && !AsyncModeEnabled)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Les duels à son rythme ne sont pas encore disponibles.");
            return parsed;
        }

        private static string ParseDifficulty(string? difficulty) =>
            QuizzCatalog.DifficultyCodes.FirstOrDefault(c => string.Equals(c, difficulty, StringComparison.OrdinalIgnoreCase))
            ?? throw new QuizzGameException(StatusCodes.Status400BadRequest, "Difficulté inconnue (Easy, Normal ou Hard).");
        #endregion
    }
}
