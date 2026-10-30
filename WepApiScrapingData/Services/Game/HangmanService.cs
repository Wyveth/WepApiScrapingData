using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Hangman;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Play;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Services.Game
{
    /// <summary>
    /// Pendu Pokémon : solo (compte facultatif) ou coop tour par tour (deux comptes, vies partagées).
    /// On enchaîne les Pokémon tant qu'il reste des vies ; chaque Pokémon trouvé rend des vies.
    /// Les indices (types, génération, description, silhouette) se débloquent au fil des erreurs.
    /// </summary>
    public class HangmanService
    {
        #region Constantes
        /// <summary>Langues où un pendu en alphabet latin a du sens.</summary>
        private static readonly string[] Languages = ["FR", "EN", "ES", "IT", "DE"];
        private const string Mask = "[...]";
        private const int MaxListed = 20;
        private static readonly TimeSpan InvitationTimeout = TimeSpan.FromDays(7);
        #endregion

        #region Fields
        private static readonly ConcurrentDictionary<long, SemaphoreSlim> Locks = new();

        private readonly ScrapingContext _context;
        private readonly DuelNotifier _notifier;
        private readonly HangmanOptions _options;
        #endregion

        #region Constructor
        public HangmanService(ScrapingContext context, DuelNotifier notifier, IOptions<HangmanOptions> options)
        {
            _context = context;
            _notifier = notifier;
            _options = options.Value;
        }
        #endregion

        #region Public Methods
        public async Task<HangmanDto> CreateSoloAsync(string? userId, HangmanCreateDto dto)
        {
            var game = NewGame(HangmanMode.Solo, dto, userId);
            game.Status = HangmanStatus.InProgress;
            game.StartedAt = DateTime.UtcNow;
            await NextPokemonAsync(game);

            _context.HangmanGames.Add(game);
            await _context.SaveChangesAsync();
            return await ToDtoAsync(game, userId);
        }

        public async Task<HangmanDto> InviteAsync(string userId, HangmanInviteDto dto)
        {
            var code = new string((dto.FriendCode ?? string.Empty).ToUpperInvariant().Where(char.IsLetterOrDigit).ToArray());
            var partner = await _context.UserProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.FriendCode == code)
                ?? throw new QuizzGameException(StatusCodes.Status404NotFound, "Aucun joueur ne possède ce code.");
            if (partner.UserId == userId)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Invitez un autre joueur que vous-même.");

            var game = NewGame(HangmanMode.Coop, dto, userId);
            game.Player2Id = partner.UserId;
            game.Status = HangmanStatus.Invited;
            _context.HangmanGames.Add(game);
            await _context.SaveChangesAsync();

            await _notifier.InvitationsChangedAsync(partner.UserId);
            await _notifier.HangmanUpdatedAsync(game.Guid, game.Player1Id, game.Player2Id);
            return await ToDtoAsync(game, userId);
        }

        public async Task<HangmanDto> AcceptAsync(string userId, Guid id)
        {
            var game = await LoadAsync(id, userId);
            await WithLockAsync(game.Id, async () =>
            {
                await _context.Entry(game).ReloadAsync();
                if (game.Status != HangmanStatus.Invited || game.Player2Id != userId)
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Cette invitation n'est plus disponible.");
                if (game.CreatedAt + InvitationTimeout < DateTime.UtcNow)
                {
                    game.Status = HangmanStatus.Cancelled;
                    await _context.SaveChangesAsync();
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Cette invitation a expiré.");
                }

                game.Status = HangmanStatus.InProgress;
                game.StartedAt = DateTime.UtcNow;
                // Celui qui a invité commence
                game.CurrentTurnUserId = game.Player1Id;
                await NextPokemonAsync(game);
                await _context.SaveChangesAsync();
            });

            await _notifier.InvitationsChangedAsync(userId);
            await _notifier.HangmanUpdatedAsync(game.Guid, game.Player1Id, game.Player2Id);
            return await ToDtoAsync(game, userId);
        }

        public async Task<HangmanDto> DeclineAsync(string userId, Guid id)
        {
            var game = await LoadAsync(id, userId);
            if (game.Status != HangmanStatus.Invited || game.Player2Id != userId)
                throw new QuizzGameException(StatusCodes.Status409Conflict, "Cette invitation n'est plus disponible.");

            game.Status = HangmanStatus.Declined;
            game.FinishedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            await _notifier.InvitationsChangedAsync(userId);
            await _notifier.HangmanUpdatedAsync(game.Guid, game.Player1Id, game.Player2Id);
            return await ToDtoAsync(game, userId);
        }

        /// <summary>Annule une invitation envoyée, ou abandonne la partie en cours (le score est conservé).</summary>
        public async Task<HangmanDto> QuitAsync(string? userId, Guid id)
        {
            var game = await LoadAsync(id, userId);
            await WithLockAsync(game.Id, async () =>
            {
                await _context.Entry(game).ReloadAsync();
                if (game.Status == HangmanStatus.Invited)
                    game.Status = HangmanStatus.Cancelled;
                else if (game.Status == HangmanStatus.InProgress)
                {
                    game.Status = HangmanStatus.Finished;
                    game.LastPokemonId = game.CurrentPokemonId;
                    game.LastFound = false;
                }
                else
                    return;
                game.FinishedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            });

            if (game.Player2Id != null)
                await _notifier.InvitationsChangedAsync(game.Player2Id);
            await _notifier.HangmanUpdatedAsync(game.Guid, game.Player1Id, game.Player2Id);
            return await ToDtoAsync(game, userId);
        }

        public async Task<HangmanDto> GetAsync(string? userId, Guid id)
        {
            var game = await LoadAsync(id, userId);
            return await ToDtoAsync(game, userId);
        }

        public async Task<HangmanDto> GuessAsync(string? userId, Guid id, HangmanGuessDto dto)
        {
            var game = await LoadAsync(id, userId);
            var letter = (dto?.Letter ?? string.Empty).Select(ToLetter).FirstOrDefault(l => l != null);
            if (letter == null || (dto?.Letter ?? string.Empty).Trim().Length != 1)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Proposez une seule lettre.");

            await WithLockAsync(game.Id, async () =>
            {
                await _context.Entry(game).ReloadAsync();
                if (game.Status != HangmanStatus.InProgress || game.CurrentPokemonId == null)
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "La partie n'est pas en cours.");
                if (game.Mode == HangmanMode.Coop && game.CurrentTurnUserId != userId)
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Ce n'est pas votre tour.");
                if (game.GuessedLetters.Contains(letter.Value))
                    throw new QuizzGameException(StatusCodes.Status409Conflict, "Lettre déjà proposée.");

                var name = await NameAsync(game.CurrentPokemonId.Value, game.Lang);
                var letters = Letters(name);
                game.GuessedLetters += letter.Value;

                if (!letters.Contains(letter.Value))
                {
                    game.WrongCount++;
                    game.Lives--;
                }

                if (letters.All(l => game.GuessedLetters.Contains(l)))
                {
                    // Trouvé : vies rendues, Pokémon suivant
                    game.Found++;
                    game.Lives = Math.Min(game.MaxLives, game.Lives + Math.Max(0, _options.LivesPerFound));
                    game.LastPokemonId = game.CurrentPokemonId;
                    game.LastFound = true;
                    await NextPokemonAsync(game);
                }
                else if (game.Lives <= 0)
                {
                    game.Lives = 0;
                    game.Status = HangmanStatus.Finished;
                    game.FinishedAt = DateTime.UtcNow;
                    game.LastPokemonId = game.CurrentPokemonId;
                    game.LastFound = false;
                }

                // Coop : la main passe après chaque proposition
                if (game.Mode == HangmanMode.Coop)
                    game.CurrentTurnUserId = game.CurrentTurnUserId == game.Player1Id ? game.Player2Id : game.Player1Id;

                await _context.SaveChangesAsync();
            });

            if (game.Mode == HangmanMode.Coop)
                await _notifier.HangmanUpdatedAsync(game.Guid, game.Player1Id, game.Player2Id);
            return await ToDtoAsync(game, userId);
        }

        public async Task<List<HangmanDto>> InvitationsAsync(string userId)
        {
            var limit = DateTime.UtcNow - InvitationTimeout;
            var games = await _context.HangmanGames
                .Where(g => g.Status == HangmanStatus.Invited && g.Player2Id == userId && g.CreatedAt > limit)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
            var result = new List<HangmanDto>();
            foreach (var game in games)
                result.Add(await ToDtoAsync(game, userId));
            return result;
        }

        /// <summary>Parties du joueur connecté (en cours et récentes).</summary>
        public async Task<List<HangmanDto>> MineAsync(string userId)
        {
            var games = await _context.HangmanGames
                .Where(g => g.Player1Id == userId || g.Player2Id == userId)
                .OrderByDescending(g => g.CreatedAt)
                .Take(MaxListed)
                .ToListAsync();
            var result = new List<HangmanDto>();
            foreach (var game in games)
                result.Add(await ToDtoAsync(game, userId, withBest: false));
            return result;
        }

        /// <summary>Image du Pokémon en cours, seulement une fois l'indice « silhouette » débloqué (le client la noircit).</summary>
        public async Task<List<string>> SilhouettePathsAsync(string? userId, Guid id)
        {
            var game = await LoadAsync(id, userId);
            if (game.Status != HangmanStatus.InProgress || game.CurrentPokemonId == null || !_options.HintsEnabled || game.WrongCount < _options.SilhouetteHintAt)
                return new List<string>();

            var paths = await _context.Pokemons.AsNoTracking()
                .Where(p => p.Id == game.CurrentPokemonId)
                .Select(p => new { p.PathImgNormal, p.PathImgLegacy })
                .FirstOrDefaultAsync();
            return new[] { paths?.PathImgNormal, paths?.PathImgLegacy }.Where(p => !string.IsNullOrEmpty(p)).Select(p => p!).ToList();
        }
        #endregion

        #region Private Methods
        private HangmanGame NewGame(HangmanMode mode, HangmanCreateDto dto, string? userId)
        {
            var lang = Languages.FirstOrDefault(l => string.Equals(l, dto.Lang, StringComparison.OrdinalIgnoreCase)) ?? "EN";
            var generations = (dto.Generations ?? new List<int>()).Where(g => g >= 0 && g <= 9).Distinct().OrderBy(g => g).ToList();
            var start = Math.Max(1, _options.StartLives);
            return new HangmanGame
            {
                Guid = Guid.NewGuid(),
                Mode = mode,
                Lang = lang,
                Generations = generations.Count == 0 ? null : string.Join(',', generations),
                Player1Id = userId,
                Lives = start,
                MaxLives = Math.Max(start, _options.MaxLives),
                CreatedAt = DateTime.UtcNow
            };
        }

        private async Task<HangmanGame> LoadAsync(Guid id, string? userId)
        {
            var game = await _context.HangmanGames.FirstOrDefaultAsync(g => g.Guid == id);
            // Solo invité : accessible avec son identifiant ; sinon seulement par ses joueurs (404 sinon)
            bool allowed = game != null && (game.Player1Id == null || game.Player1Id == userId || game.Player2Id == userId);
            if (!allowed)
                throw new QuizzGameException(StatusCodes.Status404NotFound, "Partie introuvable.");
            return game!;
        }

        /// <summary>Tire un nouveau Pokémon (forme de base, générations choisies, sans répétition tant que possible).</summary>
        private async Task NextPokemonAsync(HangmanGame game)
        {
            var generations = string.IsNullOrEmpty(game.Generations) ? new List<int>() : game.Generations.Split(',').Select(int.Parse).ToList();
            var query = _context.Pokemons.AsNoTracking().Where(p => p.TypeEvolution == Constantes.NormalEvolution);
            if (generations.Count > 0)
                query = query.Where(p => generations.Contains(p.Generation));

            var candidates = await query.Select(p => p.Id).ToListAsync();
            var used = (game.UsedPokemonIds ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToHashSet();
            var available = candidates.Where(id => !used.Contains(id)).ToList();
            if (available.Count == 0)
            {
                // Tous déjà tirés : on recommence la liste
                used.Clear();
                available = candidates;
            }
            if (available.Count == 0)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Aucun Pokémon disponible pour ces générations.");

            // Un nom sans lettre (cas improbable) ne peut pas être deviné : on en tire un autre
            for (int attempt = 0; attempt < 20; attempt++)
            {
                var id = available[Random.Shared.Next(available.Count)];
                if (Letters(await NameAsync(id, game.Lang)).Count > 0 || attempt == 19)
                {
                    used.Add(id);
                    game.CurrentPokemonId = id;
                    break;
                }
            }

            game.UsedPokemonIds = string.Join(',', used);
            game.GuessedLetters = string.Empty;
            game.WrongCount = 0;
        }

        private async Task<string> NameAsync(long pokemonId, string lang)
        {
            var pokemon = await _context.Pokemons.AsNoTracking().Where(p => p.Id == pokemonId).IncludeLang(lang).FirstAsync();
            var info = LangHelper.Info(pokemon, lang);
            return (string.IsNullOrWhiteSpace(info.Name) ? info.DisplayName : info.Name)?.Trim() ?? string.Empty;
        }

        /// <summary>Lettre A à Z correspondante (accents retirés), ou null pour un caractère non devinable (espace, tiret, symbole…).</summary>
        private static char? ToLetter(char c)
        {
            foreach (var part in c.ToString().Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(part) == UnicodeCategory.NonSpacingMark) continue;
                var upper = char.ToUpperInvariant(part);
                return upper >= 'A' && upper <= 'Z' ? upper : null;
            }
            return null;
        }

        private static HashSet<char> Letters(string name) =>
            name.Select(ToLetter).Where(l => l != null).Select(l => l!.Value).ToHashSet();

        private static string MaskName(string name, string guessed) =>
            new(name.Select(c => ToLetter(c) is char l && !guessed.Contains(l) ? '_' : c).ToArray());

        private async Task<HangmanDto> ToDtoAsync(HangmanGame game, string? userId, bool withBest = true)
        {
            var dto = new HangmanDto
            {
                Id = game.Guid,
                Mode = game.Mode.ToString(),
                Status = game.Status.ToString(),
                Lang = game.Lang,
                Generations = string.IsNullOrEmpty(game.Generations) ? new List<int>() : game.Generations.Split(',').Select(int.Parse).ToList(),
                Lives = game.Lives,
                MaxLives = game.MaxLives,
                Found = game.Found,
                WrongCount = game.WrongCount,
                IsInviter = game.Mode == HangmanMode.Coop && game.Player1Id == userId,
                IsMyTurn = game.Status == HangmanStatus.InProgress && (game.Mode == HangmanMode.Solo || game.CurrentTurnUserId == userId),
                CreatedAt = game.CreatedAt,
                FinishedAt = game.FinishedAt
            };

            if (game.Mode == HangmanMode.Coop)
            {
                var ids = new[] { game.Player1Id, game.Player2Id }.Where(i => i != null).Select(i => i!).ToList();
                var names = await _context.Users.AsNoTracking().Where(u => ids.Contains(u.Id)).ToDictionaryAsync(u => u.Id, u => u.UserName);
                var partnerId = game.Player1Id == userId ? game.Player2Id : game.Player1Id;
                dto.Partner = partnerId != null && names.TryGetValue(partnerId, out var partner) ? partner : null;
                dto.TurnUserName = game.CurrentTurnUserId != null && names.TryGetValue(game.CurrentTurnUserId, out var turn) ? turn : null;
            }

            if (game.Status == HangmanStatus.InProgress && game.CurrentPokemonId != null)
            {
                var pokemon = await _context.Pokemons.AsNoTracking()
                    .Where(p => p.Id == game.CurrentPokemonId)
                    .Include(p => p.Pokemon_TypePoks).ThenInclude(t => t.TypePok)
                    .IncludeLang(game.Lang)
                    .AsSplitQuery()
                    .FirstAsync();
                var info = LangHelper.Info(pokemon, game.Lang);
                var name = (string.IsNullOrWhiteSpace(info.Name) ? info.DisplayName : info.Name)?.Trim() ?? string.Empty;
                var letters = Letters(name);

                dto.Mask = MaskName(name, game.GuessedLetters);
                dto.Letters = game.GuessedLetters.Select(l => new HangmanLetterDto { Letter = l.ToString(), Correct = letters.Contains(l) }).ToList();

                if (_options.HintsEnabled)
                {
                    if (game.WrongCount >= _options.TypeHintAt)
                        dto.Hints.Types = pokemon.Pokemon_TypePoks.OrderBy(t => t.Id).Where(t => t.TypePok != null)
                            .Select(t => new HangmanTypeHintDto { Name = LangHelper.TypeName(t.TypePok!, game.Lang), Image = LangHelper.TypeImage(t.TypePok!, game.Lang) })
                            .ToList();
                    if (game.WrongCount >= _options.GenerationHintAt)
                        dto.Hints.Generation = pokemon.Generation;
                    if (game.WrongCount >= _options.DescriptionHintAt)
                        dto.Hints.Description = MaskedDescription(info, name);
                    dto.Hints.Silhouette = game.WrongCount >= _options.SilhouetteHintAt
                        && !string.IsNullOrEmpty(pokemon.PathImgNormal ?? pokemon.PathImgLegacy);

                    var thresholds = new[] { _options.TypeHintAt, _options.GenerationHintAt, _options.DescriptionHintAt, _options.SilhouetteHintAt };
                    dto.NextHintAt = thresholds.Where(t => t > game.WrongCount).OrderBy(t => t).Select(t => (int?)t).FirstOrDefault();
                }
            }

            if (game.LastPokemonId != null)
            {
                var last = await _context.Pokemons.AsNoTracking().Where(p => p.Id == game.LastPokemonId).IncludeLang(game.Lang).FirstOrDefaultAsync();
                if (last != null)
                {
                    var info = LangHelper.Info(last, game.Lang);
                    dto.Last = new HangmanRevealDto
                    {
                        Name = string.IsNullOrWhiteSpace(info.Name) ? info.DisplayName : info.Name,
                        Image = last.PathImgNormal ?? last.PathImgLegacy,
                        Found = game.LastFound
                    };
                }
            }

            if (withBest && userId != null)
            {
                dto.BestScore = await _context.HangmanGames.AsNoTracking()
                    .Where(g => g.Status == HangmanStatus.Finished && g.Mode == game.Mode && (g.Player1Id == userId || g.Player2Id == userId))
                    .MaxAsync(g => (int?)g.Found);
            }

            return dto;
        }

        private static string? MaskedDescription(DataInfo info, string name)
        {
            var description = LangHelper.CleanText(string.IsNullOrWhiteSpace(info.DescriptionVx) ? info.DescriptionVy : info.DescriptionVx);
            if (string.IsNullOrWhiteSpace(description)) return null;
            foreach (var n in new[] { name, info.Name, info.DisplayName }.Where(n => !string.IsNullOrWhiteSpace(n)).Distinct().OrderByDescending(n => n!.Length))
                description = Regex.Replace(description, Regex.Escape(n!), Mask, RegexOptions.IgnoreCase);
            return description;
        }

        private async Task WithLockAsync(long id, Func<Task> action)
        {
            var semaphore = Locks.GetOrAdd(id, _ => new SemaphoreSlim(1, 1));
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
        #endregion
    }
}
