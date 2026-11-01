using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Play;
using WepApiScrapingData.Utils;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WepApiScrapingData.Services.Game
{
    /// <summary>Erreur fonctionnelle du jeu, traduite en code HTTP par le contrôleur.</summary>
    public class QuizzGameException : Exception
    {
        public int StatusCode { get; }

        public QuizzGameException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Moteur du quiz pour le client web : génération complète en une transaction
    /// (bonnes réponses et leurres), lecture localisée, correction côté serveur et résultats.
    /// Remplace, pour le jeu, la génération partielle de QuizzRepository/AnswerRepository.
    /// </summary>
    public class QuizzGameService
    {
        #region Constantes
        public const string KindPokemon = "Pokemon";
        public const string KindPokemonDescription = "PokemonDescription";
        public const string KindType = "Type";
        public const string KindAbility = "Ability";
        public const string KindAbilityDescription = "AbilityDescription";
        public const string KindStat = "Stat";

        private const string Mask = "[...]";
        private const int MaxBuildAttempts = 12;
        private const int MaxListedQuizz = 50;
        #endregion

        #region Fields
        private static readonly SemaphoreSlim CatalogLock = new(1, 1);
        private static volatile bool _catalogReady;

        private readonly ScrapingContext _context;
        private readonly ILogger<QuizzGameService> _logger;
        #endregion

        #region Constructor
        public QuizzGameService(ScrapingContext context, ILogger<QuizzGameService> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<QuizzSummaryDto> CreateAsync(QuizzCreateDto options, string? userId)
        {
            var generations = (options?.Generations ?? new List<int>()).Where(g => g >= 0 && g <= 9).Distinct().ToList();
            var difficulties = (options?.Difficulties ?? new List<string>())
                .Select(d => QuizzCatalog.DifficultyCodes.FirstOrDefault(c => string.Equals(c, d, StringComparison.OrdinalIgnoreCase)))
                .Where(d => d != null)
                .Select(d => d!)
                .Distinct()
                .ToList();
            if (difficulties.Count == 0)
                difficulties.Add(Constantes.Easy_Code);

            var catalog = await EnsureCatalogAsync();
            var data = await LoadGenerationDataAsync(generations);
            if (data.Pool.Count < 20)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Pas assez de Pokémon pour les générations choisies.");

            bool easy = difficulties.Contains(Constantes.Easy_Code);
            bool normal = difficulties.Contains(Constantes.Normal_Code);
            bool hard = difficulties.Contains(Constantes.Hard_Code);
            int nbQuestions = QuizzCatalog.NbQuestions(easy, normal, hard);

            var built = new List<BuiltQuestion>();
            for (int i = 0; i < nbQuestions; i++)
            {
                BuiltQuestion? question = null;
                for (int attempt = 0; attempt < MaxBuildAttempts && question == null; attempt++)
                    question = Build(DrawDefinition(difficulties), data);

                // Repli : question "Quel est ce pokémon ?" de la première difficulté choisie
                question ??= Build(QuizzCatalog.QuestionTypes.First(d => d.Code == Constantes.QTypPok_Code && d.DifficultyCode == difficulties[0] && !d.IsBlurred && !d.IsHide), data)
                    ?? throw new QuizzGameException(StatusCodes.Status500InternalServerError, "Impossible de générer les questions.");
                built.Add(question);
            }

            var quizz = Stamp(new ClassQuizz.Quizz
            {
                Gen1 = generations.Contains(1),
                Gen2 = generations.Contains(2),
                Gen3 = generations.Contains(3),
                Gen4 = generations.Contains(4),
                Gen5 = generations.Contains(5),
                Gen6 = generations.Contains(6),
                Gen7 = generations.Contains(7),
                Gen8 = generations.Contains(8),
                Gen9 = generations.Contains(9),
                GenArceus = generations.Contains(0),
                Easy = easy,
                Normal = normal,
                Hard = hard,
                Done = false
            });
            _context.Quizzs.Add(quizz);
            if (userId != null)
                _context.Entry(quizz).Property("IdentityUserId").CurrentValue = userId;

            int order = 1;
            foreach (var item in built)
            {
                var question = Stamp(new Question
                {
                    Order = order++,
                    DataObjectID = item.SubjectId,
                    QuestionType = catalog[item.Definition],
                    Done = false
                });
                _context.Questions.Add(question);
                _context.Quizz_Question.Add(Stamp(new Quizz_Question { Quizz = quizz, Question = question }));

                int answerOrder = 1;
                foreach (var built_answer in item.Answers.OrderBy(_ => Random.Shared.Next()))
                {
                    var answer = Stamp(new Answer
                    {
                        Order = answerOrder++,
                        IsCorrect = built_answer.IsCorrect,
                        IsCorrectID = built_answer.EntityId,
                        Type = built_answer.Kind,
                        Libelle = built_answer.Value,
                        IsSelected = false
                    });
                    _context.Answers.Add(answer);
                    _context.Question_Answer.Add(Stamp(new Question_Answer { Question = question, Answer = answer }));
                }
            }

            await _context.SaveChangesAsync();

            var loaded = await LoadQuizzAsync(quizz.Guid, userId);
            return Summarize(loaded.Quizz, loaded.Questions, loaded.OwnerId);
        }

        public async Task<QuizzSummaryDto> GetSummaryAsync(Guid id, string? userId)
        {
            var loaded = await LoadQuizzAsync(id, userId);
            return Summarize(loaded.Quizz, loaded.Questions, loaded.OwnerId);
        }

        public async Task<List<QuizzSummaryDto>> ListMineAsync(string userId)
        {
            var quizzs = await _context.Quizzs.AsNoTracking()
                .Where(q => EF.Property<string>(q, "IdentityUserId") == userId)
                .OrderByDescending(q => q.DateCreation)
                .Take(MaxListedQuizz)
                .Include(q => q.Quizz_Questions).ThenInclude(qq => qq.Question).ThenInclude(q => q!.Question_Answers!).ThenInclude(qa => qa.Answer)
                .AsSplitQuery()
                .ToListAsync();

            return quizzs
                .Select(q => Summarize(q, q.Quizz_Questions.Select(qq => qq.Question!).OrderBy(x => x.Order).ToList(), userId))
                .ToList();
        }

        public async Task<QuestionDto> GetQuestionAsync(Guid id, int order, string? lang, string? userId)
        {
            var language = LangHelper.Normalize(lang);
            var loaded = await LoadQuizzAsync(id, userId);
            var question = FindQuestion(loaded.Questions, order);
            var lookup = await BuildLookupAsync(new[] { question }, language);

            var questionType = question.QuestionType!;
            var answers = Answers(question);

            var dto = new QuestionDto
            {
                QuizzId = loaded.Quizz.Guid,
                Order = question.Order,
                Total = loaded.Questions.Count,
                Code = questionType.Code ?? string.Empty,
                Label = QuestionLabel(question, lookup),
                DifficultyCode = questionType.Difficulty?.Code,
                IsMultipleAnswers = questionType.IsMultipleAnswers,
                IsBlurred = questionType.IsBlurred,
                IsGrayscale = questionType.IsGrayscale,
                IsHide = questionType.IsHide,
                Subject = BuildSubject(question, lookup, reveal: question.Done),
                Answers = answers.Select(a => new AnswerChoiceDto
                {
                    Id = a.Id,
                    Order = a.Order,
                    Label = AnswerLabel(a, lookup)
                }).ToList(),
                Done = question.Done
            };

            if (question.Done)
                dto.Result = BuildResult(loaded.Quizz, loaded.Questions, question, alreadyAnswered: true);

            return dto;
        }

        public async Task<AnswerResultDto> SubmitAsync(Guid id, int order, AnswerSubmitDto submit, string? userId)
        {
            var loaded = await LoadQuizzAsync(id, userId);
            var question = FindQuestion(loaded.Questions, order);

            if (question.Done)
                return BuildResult(loaded.Quizz, loaded.Questions, question, alreadyAnswered: true);

            var answers = Answers(question);
            var selected = (submit?.AnswerIds ?? new List<long>()).Distinct().ToList();

            if (selected.Count == 0)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Aucune réponse sélectionnée.");
            if (selected.Any(s => answers.All(a => a.Id != s)))
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Réponse inconnue pour cette question.");
            if (!question.QuestionType!.IsMultipleAnswers && selected.Count > 1)
                throw new QuizzGameException(StatusCodes.Status400BadRequest, "Une seule réponse est attendue.");

            var now = DateTime.UtcNow;
            foreach (var answer in answers)
            {
                answer.IsSelected = selected.Contains(answer.Id);
                answer.DateModification = now;
            }

            question.Done = true;
            question.DateModification = now;

            loaded.Quizz.Done = loaded.Questions.All(q => q.Done);
            loaded.Quizz.DateModification = now;

            await _context.SaveChangesAsync();

            return BuildResult(loaded.Quizz, loaded.Questions, question, alreadyAnswered: false);
        }

        public async Task<QuizzResultDto> GetResultAsync(Guid id, string? lang, string? userId)
        {
            var language = LangHelper.Normalize(lang);
            var loaded = await LoadQuizzAsync(id, userId);
            var lookup = await BuildLookupAsync(loaded.Questions, language);

            return new QuizzResultDto
            {
                Summary = Summarize(loaded.Quizz, loaded.Questions, loaded.OwnerId),
                Questions = loaded.Questions.Select(q =>
                {
                    var answers = Answers(q);
                    return new QuestionResultDto
                    {
                        Order = q.Order,
                        Code = q.QuestionType?.Code ?? string.Empty,
                        Label = QuestionLabel(q, lookup),
                        SubjectName = q.Done ? SubjectName(q, lookup) : null,
                        Done = q.Done,
                        IsCorrect = IsCorrect(q),
                        // La correction n'est donnée que pour les questions déjà répondues
                        CorrectAnswers = q.Done ? answers.Where(a => a.IsCorrect).Select(a => AnswerLabel(a, lookup)).ToList() : new List<string?>(),
                        SelectedAnswers = answers.Where(a => a.IsSelected).Select(a => AnswerLabel(a, lookup)).ToList()
                    };
                }).ToList()
            };
        }

        /// <summary>Rattache un quiz invité au compte connecté.</summary>
        public async Task<QuizzSummaryDto> ClaimAsync(Guid id, string userId)
        {
            var loaded = await LoadQuizzAsync(id, userId);
            if (loaded.OwnerId == null)
            {
                _context.Entry(loaded.Quizz).Property("IdentityUserId").CurrentValue = userId;
                await _context.SaveChangesAsync();
            }
            return Summarize(loaded.Quizz, loaded.Questions, userId);
        }

        public async Task DeleteAsync(Guid id, string? userId)
        {
            var loaded = await LoadQuizzAsync(id, userId);
            var links = loaded.Quizz.Quizz_Questions.ToList();
            var questionAnswers = loaded.Questions.SelectMany(q => q.Question_Answers ?? new List<Question_Answer>()).ToList();
            var answers = questionAnswers.Where(qa => qa.Answer != null).Select(qa => qa.Answer!).ToList();

            // Toutes les relations sont en DeleteBehavior.Restrict : suppression des enfants d'abord
            _context.Question_Answer.RemoveRange(questionAnswers);
            _context.Answers.RemoveRange(answers);
            _context.Quizz_Question.RemoveRange(links);
            _context.Questions.RemoveRange(loaded.Questions);
            _context.Quizzs.Remove(loaded.Quizz);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Chemins relatifs (sous Content) candidats pour l'image ou le cri du sujet, par ordre de préférence.
        /// Le contrôleur sert le premier fichier qui existe. Servi par l'API pour ne pas révéler le nom du Pokémon dans l'URL.
        /// </summary>
        public async Task<List<string>> GetSubjectMediaPathsAsync(Guid id, int order, bool sound, string? userId)
        {
            var loaded = await LoadQuizzAsync(id, userId);
            var question = FindQuestion(loaded.Questions, order);
            var kind = SubjectKind(question.QuestionType?.Code);

            if (kind == KindPokemon)
            {
                var pokemon = await _context.Pokemons.AsNoTracking()
                    .Where(p => p.Id == question.DataObjectID)
                    .Select(p => new { p.PathImgNormal, p.PathImgLegacy, p.PathSoundCurrent, p.PathSoundLegacy, p.PathSound })
                    .FirstOrDefaultAsync();
                if (pokemon == null) return new List<string>();
                // PathSound est le fichier réellement téléchargé (Content/Sound/G{n}/Nom.ogg), comme dans le Pokédex ;
                // PathSoundCurrent/Legacy pointent vers des sous-dossiers qui peuvent ne pas exister.
                return NonEmpty(sound
                    ? new[] { pokemon.PathSound, pokemon.PathSoundCurrent, pokemon.PathSoundLegacy }
                    : new[] { pokemon.PathImgNormal, pokemon.PathImgLegacy });
            }

            if (kind == KindType && !sound)
            {
                var type = await _context.TypesPok.AsNoTracking()
                    .Where(t => t.Id == question.DataObjectID)
                    .Select(t => new { t.PathIconHome, t.PathAutoHome, t.PathMiniGo })
                    .FirstOrDefaultAsync();
                // Icône sans texte : le libellé du type est la réponse attendue
                return type == null ? new List<string>() : NonEmpty(new[] { type.PathIconHome, type.PathAutoHome, type.PathMiniGo });
            }

            return new List<string>();
        }

        private static List<string> NonEmpty(IEnumerable<string?> paths) =>
            paths.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p!).Distinct().ToList();
        #endregion

        #region Catalogue
        /// <summary>
        /// Garantit la présence des difficultés et types de questions du catalogue en base
        /// (les tables ne sont alimentées par aucune migration). Idempotent.
        /// </summary>
        private async Task<Dictionary<QuestionTypeDefinition, QuestionType>> EnsureCatalogAsync()
        {
            if (!_catalogReady)
            {
                await CatalogLock.WaitAsync();
                try
                {
                    if (!_catalogReady)
                    {
                        await SeedCatalogAsync();
                        _catalogReady = true;
                    }
                }
                finally
                {
                    CatalogLock.Release();
                }
            }

            var rows = await _context.QuestionTypes.Include(q => q.Difficulty).ToListAsync();
            var result = new Dictionary<QuestionTypeDefinition, QuestionType>();
            foreach (var definition in QuizzCatalog.QuestionTypes)
            {
                var row = rows.FirstOrDefault(r => Matches(r, definition));
                if (row != null)
                    result[definition] = row;
            }

            if (result.Count != QuizzCatalog.QuestionTypes.Count)
            {
                _catalogReady = false;
                throw new QuizzGameException(StatusCodes.Status500InternalServerError, "Catalogue des questions incomplet.");
            }
            return result;
        }

        private async Task SeedCatalogAsync()
        {
            var difficulties = await _context.Difficulties.ToListAsync();
            foreach (var code in QuizzCatalog.DifficultyCodes)
            {
                if (difficulties.Any(d => d.Code == code))
                    continue;

                var difficulty = Stamp(new Difficulty
                {
                    Code = code,
                    Libelle_FR = QuizzCatalog.Label(code, "FR"),
                    Libelle_EN = QuizzCatalog.Label(code, "EN"),
                    Libelle_ES = QuizzCatalog.Label(code, "ES"),
                    Libelle_IT = QuizzCatalog.Label(code, "IT"),
                    Libelle_DE = QuizzCatalog.Label(code, "DE"),
                    Libelle_RU = QuizzCatalog.Label(code, "RU"),
                    Libelle_CO = QuizzCatalog.Label(code, "CO"),
                    Libelle_CN = QuizzCatalog.Label(code, "CN"),
                    Libelle_JP = QuizzCatalog.Label(code, "JP")
                });
                _context.Difficulties.Add(difficulty);
                difficulties.Add(difficulty);
            }

            var rows = await _context.QuestionTypes.Include(q => q.Difficulty).ToListAsync();
            int added = 0;
            foreach (var definition in QuizzCatalog.QuestionTypes)
            {
                if (rows.Any(r => Matches(r, definition)))
                    continue;

                var prefix = QuizzCatalog.QuestionTypePrefix(definition.Code);
                _context.QuestionTypes.Add(Stamp(new QuestionType
                {
                    Code = definition.Code,
                    Libelle_FR = QuizzCatalog.Label(prefix, "FR"),
                    Libelle_EN = QuizzCatalog.Label(prefix, "EN"),
                    Libelle_ES = QuizzCatalog.Label(prefix, "ES"),
                    Libelle_IT = QuizzCatalog.Label(prefix, "IT"),
                    Libelle_DE = QuizzCatalog.Label(prefix, "DE"),
                    Libelle_RU = QuizzCatalog.Label(prefix, "RU"),
                    Libelle_CO = QuizzCatalog.Label(prefix, "CO"),
                    Libelle_CN = QuizzCatalog.Label(prefix, "CN"),
                    Libelle_JP = QuizzCatalog.Label(prefix, "JP"),
                    NbAnswers = definition.NbAnswers,
                    NbAnswersPossible = definition.NbAnswersPossible,
                    IsMultipleAnswers = definition.IsMultipleAnswers,
                    IsBlurred = definition.IsBlurred,
                    IsGrayscale = definition.IsGrayscale,
                    IsHide = definition.IsHide,
                    IsSameType = false,
                    Difficulty = difficulties.First(d => d.Code == definition.DifficultyCode)
                }));
                added++;
            }

            await _context.SaveChangesAsync();
            if (added > 0)
                _logger.LogInformation("Catalogue du quiz : {Count} types de questions ajoutés.", added);
        }

        private static bool Matches(QuestionType row, QuestionTypeDefinition definition) =>
            row.Code == definition.Code
            && row.Difficulty?.Code == definition.DifficultyCode
            && row.NbAnswers == definition.NbAnswers
            && row.IsMultipleAnswers == definition.IsMultipleAnswers
            && row.IsBlurred == definition.IsBlurred
            && row.IsGrayscale == definition.IsGrayscale
            && row.IsHide == definition.IsHide;
        #endregion

        #region Generation
        private sealed class PokemonPick
        {
            public long Id { get; set; }
            public long? EvolutionChainId { get; set; }
            public int StatPv { get; set; }
            public int StatAttack { get; set; }
            public int StatDefense { get; set; }
            public int StatAttackSpe { get; set; }
            public int StatDefenseSpe { get; set; }
            public int StatSpeed { get; set; }
            public List<long> TypeIds { get; set; } = new();
            public List<long> WeaknessIds { get; set; } = new();
            public List<long> AbilityIds { get; set; } = new();
            public bool HasDescription { get; set; }
        }

        private sealed class GenerationData
        {
            public List<PokemonPick> Pool { get; init; } = new();
            public List<long> TypeIds { get; init; } = new();
            public List<long> AbilityIds { get; init; } = new();
            public ILookup<long, long> Families { get; init; } = Enumerable.Empty<long>().ToLookup(x => x, x => x);
            public HashSet<long> UsedPokemons { get; } = new();
            public HashSet<long> UsedTypes { get; } = new();
            public HashSet<long> UsedAbilities { get; } = new();
        }

        private sealed record BuiltAnswer(long EntityId, bool IsCorrect, string Kind, string? Value = null);

        private sealed class BuiltQuestion
        {
            public QuestionTypeDefinition Definition { get; init; } = null!;
            public long SubjectId { get; init; }
            public List<BuiltAnswer> Answers { get; init; } = new();
        }

        private async Task<GenerationData> LoadGenerationDataAsync(List<int> generations)
        {
            bool any = generations.Count > 0;
            bool g0 = generations.Contains(0), g1 = generations.Contains(1), g2 = generations.Contains(2),
                 g3 = generations.Contains(3), g4 = generations.Contains(4), g5 = generations.Contains(5),
                 g6 = generations.Contains(6), g7 = generations.Contains(7), g8 = generations.Contains(8),
                 g9 = generations.Contains(9);

            // Même règle de filtre que PokemonRepository.GetPokemonsWithFilterGen
            var query = _context.Pokemons.AsNoTracking().Where(p =>
                !any
                || (g1 && p.Generation == 1 && p.TypeEvolution == Constantes.NormalEvolution)
                || (g2 && p.Generation == 2 && p.TypeEvolution == Constantes.NormalEvolution)
                || (g3 && p.Generation == 3 && p.TypeEvolution == Constantes.NormalEvolution)
                || (g4 && p.Generation == 4 && p.TypeEvolution == Constantes.NormalEvolution)
                || (g5 && p.Generation == 5 && p.TypeEvolution == Constantes.NormalEvolution)
                || (g6 && (p.Generation == 6 || p.TypeEvolution == Constantes.MegaEvolution))
                || (g7 && (p.Generation == 7 || p.TypeEvolution == Constantes.Alola))
                || (g8 && (p.Generation == 8 || p.TypeEvolution == Constantes.Galar || p.TypeEvolution == Constantes.GigaEvolution))
                || (g9 && (p.Generation == 9 || p.TypeEvolution == Constantes.Paldea))
                || (g0 && (p.Generation == 0 || p.TypeEvolution == Constantes.Hisui)));

            var pool = await query
                .Select(p => new PokemonPick
                {
                    Id = p.Id,
                    EvolutionChainId = p.EvolutionChainId,
                    StatPv = p.StatPv,
                    StatAttack = p.StatAttack,
                    StatDefense = p.StatDefense,
                    StatAttackSpe = p.StatAttackSpe,
                    StatDefenseSpe = p.StatDefenseSpe,
                    StatSpeed = p.StatSpeed,
                    TypeIds = p.Pokemon_TypePoks.OrderBy(t => t.Id).Select(t => t.TypePokId).ToList(),
                    WeaknessIds = p.Pokemon_Weaknesses.Select(w => w.TypePokId).ToList(),
                    AbilityIds = p.Pokemon_Abilities.Select(a => a.AbilityId).ToList(),
                    HasDescription = p.FR.DescriptionVx != null && p.FR.DescriptionVx != ""
                })
                .AsSplitQuery()
                .ToListAsync();

            var typeIds = await _context.TypesPok.AsNoTracking().Select(t => t.Id).ToListAsync();
            var abilityIds = await _context.Abilities.AsNoTracking()
                .Where(a => a.Name_FR != null && a.Description_FR != null && a.Description_FR != "")
                .Select(a => a.Id)
                .ToListAsync();
            var families = await _context.Pokemons.AsNoTracking()
                .Where(p => p.EvolutionChainId != null)
                .Select(p => new { p.Id, ChainId = p.EvolutionChainId!.Value })
                .ToListAsync();

            return new GenerationData
            {
                Pool = pool,
                TypeIds = typeIds,
                AbilityIds = abilityIds,
                Families = families.ToLookup(f => f.ChainId, f => f.Id)
            };
        }

        private static QuestionTypeDefinition DrawDefinition(List<string> difficulties)
        {
            var difficulty = difficulties[Random.Shared.Next(difficulties.Count)];
            var roll = Random.Shared.Next(100);
            var group = QuizzCatalog.Weights(difficulty).First(w => roll <= w.UpperBound);

            var candidates = QuizzCatalog.QuestionTypes
                .Where(d => d.DifficultyCode == difficulty && group.Codes.Contains(d.Code))
                .ToList();
            if (candidates.Count == 0)
                candidates = QuizzCatalog.QuestionTypes.Where(d => d.DifficultyCode == difficulty).ToList();

            return candidates[Random.Shared.Next(candidates.Count)];
        }

        private static BuiltQuestion? Build(QuestionTypeDefinition definition, GenerationData data)
        {
            int n = definition.NbAnswers;
            var code = definition.Code;

            if (code == Constantes.QTypPok_Code || code == Constantes.QTypPokDescReverse_Code || code == Constantes.QTypPokDesc_Code)
            {
                bool needDescription = code != Constantes.QTypPok_Code;
                var subject = PickPokemon(data, p => !needDescription || p.HasDescription);
                if (subject == null) return null;

                var kind = code == Constantes.QTypPokDesc_Code ? KindPokemonDescription : KindPokemon;
                var distractors = Sample(data.Pool.Where(p => p.Id != subject.Id && (!needDescription || p.HasDescription)).Select(p => p.Id), n - 1);
                if (distractors.Count < n - 1) return null;

                return MakeQuestion(definition, subject.Id,
                    distractors.Select(id => new BuiltAnswer(id, false, kind)).Prepend(new BuiltAnswer(subject.Id, true, kind)));
            }

            if (code == Constantes.QTypTypPok_Code || code == Constantes.QTypTypPokVarious_Code || code == Constantes.QTypWeakPokVarious_Code)
            {
                n = Math.Min(n, data.TypeIds.Count);
                bool weakness = code == Constantes.QTypWeakPokVarious_Code;
                var subject = PickPokemon(data, p => weakness
                    ? p.WeaknessIds.Count > 0 && p.WeaknessIds.Distinct().Count() < n
                    : p.TypeIds.Count > 0);
                if (subject == null) return null;

                var correct = code == Constantes.QTypTypPok_Code
                    ? new List<long> { subject.TypeIds[0] }
                    : (weakness ? subject.WeaknessIds : subject.TypeIds).Distinct().ToList();
                // Les autres types du Pokémon ne servent pas de leurre pour la question "type principal"
                var excluded = new HashSet<long>(subject.TypeIds.Concat(correct));
                var distractors = Sample(data.TypeIds.Where(t => !excluded.Contains(t)), n - correct.Count);

                return MakeQuestion(definition, subject.Id,
                    correct.Select(t => new BuiltAnswer(t, true, KindType))
                        .Concat(distractors.Select(t => new BuiltAnswer(t, false, KindType))));
            }

            if (code == Constantes.QTypTyp_Code)
            {
                var subject = PickId(data.TypeIds, data.UsedTypes, _ => true);
                if (subject == null) return null;

                n = Math.Min(n, data.TypeIds.Count);
                var distractors = Sample(data.TypeIds.Where(t => t != subject.Value), n - 1);
                return MakeQuestion(definition, subject.Value,
                    distractors.Select(t => new BuiltAnswer(t, false, KindType)).Prepend(new BuiltAnswer(subject.Value, true, KindType)));
            }

            if (code == Constantes.QTypPokTypVarious_Code)
            {
                var subject = PickId(data.TypeIds, data.UsedTypes, t =>
                    data.Pool.Any(p => p.TypeIds.Contains(t)) && data.Pool.Count(p => !p.TypeIds.Contains(t)) >= n - 1);
                if (subject == null) return null;

                var withType = data.Pool.Where(p => p.TypeIds.Contains(subject.Value)).Select(p => p.Id).ToList();
                int nbCorrect = Random.Shared.Next(1, Math.Min(3, Math.Min(n - 1, withType.Count)) + 1);
                var correct = Sample(withType, nbCorrect);
                var distractors = Sample(data.Pool.Where(p => !p.TypeIds.Contains(subject.Value)).Select(p => p.Id), n - correct.Count);

                return MakeQuestion(definition, subject.Value,
                    correct.Select(id => new BuiltAnswer(id, true, KindPokemon))
                        .Concat(distractors.Select(id => new BuiltAnswer(id, false, KindPokemon))));
            }

            if (code == Constantes.QTypPokFamilyVarious_Code)
            {
                var subject = PickPokemon(data, p => p.EvolutionChainId != null && data.Families[p.EvolutionChainId.Value].Count() > 1);
                if (subject == null) return null;

                var family = new HashSet<long>(data.Families[subject.EvolutionChainId!.Value]);
                var correct = Sample(family.Where(id => id != subject.Id), Math.Min(n - 1, family.Count - 1));
                var distractors = Sample(data.Pool.Where(p => !family.Contains(p.Id)).Select(p => p.Id), n - correct.Count);
                if (correct.Count == 0 || correct.Count + distractors.Count < n) return null;

                return MakeQuestion(definition, subject.Id,
                    correct.Select(id => new BuiltAnswer(id, true, KindPokemon))
                        .Concat(distractors.Select(id => new BuiltAnswer(id, false, KindPokemon))));
            }

            if (code == Constantes.QTypPokTalentVarious_Code)
            {
                var abilitySet = new HashSet<long>(data.AbilityIds);
                var subject = PickPokemon(data, p =>
                {
                    var known = p.AbilityIds.Where(abilitySet.Contains).Distinct().Count();
                    return known > 0 && known < n;
                });
                if (subject == null) return null;

                var correct = subject.AbilityIds.Where(abilitySet.Contains).Distinct().ToList();
                var distractors = Sample(data.AbilityIds.Where(a => !correct.Contains(a)), n - correct.Count);

                return MakeQuestion(definition, subject.Id,
                    correct.Select(a => new BuiltAnswer(a, true, KindAbility))
                        .Concat(distractors.Select(a => new BuiltAnswer(a, false, KindAbility))));
            }

            if (code == Constantes.QTypTalent_Code || code == Constantes.QTypTalentReverse_Code)
            {
                var subject = PickId(data.AbilityIds, data.UsedAbilities, _ => true);
                if (subject == null) return null;

                // Talent : on montre le nom, on choisit la description. TalentReverse : l'inverse.
                var kind = code == Constantes.QTypTalent_Code ? KindAbilityDescription : KindAbility;
                var distractors = Sample(data.AbilityIds.Where(a => a != subject.Value), n - 1);
                if (distractors.Count < n - 1) return null;

                return MakeQuestion(definition, subject.Value,
                    distractors.Select(a => new BuiltAnswer(a, false, kind)).Prepend(new BuiltAnswer(subject.Value, true, kind)));
            }

            if (code == Constantes.QTypPokStat_Code)
            {
                var subject = PickPokemon(data, p => p.StatPv > 0);
                if (subject == null) return null;

                var statKey = QuizzCatalog.StatKeys[Random.Shared.Next(QuizzCatalog.StatKeys.Length)];
                int value = StatValue(subject, statKey);
                int min = Math.Max(1, value - 50), max = Math.Min(255, value + 50);

                var values = new HashSet<int> { value };
                while (values.Count < Math.Min(n, max - min + 1))
                    values.Add(Random.Shared.Next(min, max + 1));

                var kind = $"{KindStat}:{statKey}";
                return MakeQuestion(definition, subject.Id,
                    values.Select(v => new BuiltAnswer(subject.Id, v == value, kind, v.ToString())));
            }

            return null;
        }

        private static BuiltQuestion MakeQuestion(QuestionTypeDefinition definition, long subjectId, IEnumerable<BuiltAnswer> answers) =>
            new() { Definition = definition, SubjectId = subjectId, Answers = answers.ToList() };

        private static PokemonPick? PickPokemon(GenerationData data, Func<PokemonPick, bool> predicate)
        {
            var candidates = data.Pool.Where(p => predicate(p) && !data.UsedPokemons.Contains(p.Id)).ToList();
            if (candidates.Count == 0)
                candidates = data.Pool.Where(predicate).ToList();
            if (candidates.Count == 0)
                return null;

            var picked = candidates[Random.Shared.Next(candidates.Count)];
            data.UsedPokemons.Add(picked.Id);
            return picked;
        }

        private static long? PickId(List<long> source, HashSet<long> used, Func<long, bool> predicate)
        {
            var candidates = source.Where(id => predicate(id) && !used.Contains(id)).ToList();
            if (candidates.Count == 0)
                candidates = source.Where(predicate).ToList();
            if (candidates.Count == 0)
                return null;

            var picked = candidates[Random.Shared.Next(candidates.Count)];
            used.Add(picked);
            return picked;
        }

        private static List<long> Sample(IEnumerable<long> source, int count) =>
            count <= 0 ? new List<long>() : source.Distinct().OrderBy(_ => Random.Shared.Next()).Take(count).ToList();

        private static int StatValue(PokemonPick pokemon, string statKey) => statKey switch
        {
            Constantes.Pv => pokemon.StatPv,
            Constantes.Attack => pokemon.StatAttack,
            Constantes.Defense => pokemon.StatDefense,
            Constantes.AttackSpe => pokemon.StatAttackSpe,
            Constantes.DefenseSpe => pokemon.StatDefenseSpe,
            _ => pokemon.StatSpeed
        };
        #endregion

        #region Reading
        private sealed record LoadedQuizz(ClassQuizz.Quizz Quizz, List<Question> Questions, string? OwnerId);

        private sealed class Lookup
        {
            public string Lang { get; init; } = "FR";
            public Dictionary<long, Pokemon> Pokemons { get; init; } = new();
            public Dictionary<long, TypePok> Types { get; init; } = new();
            public Dictionary<long, Ability> Abilities { get; init; } = new();
            public ILookup<long, Pokemon> Families { get; init; } = Enumerable.Empty<Pokemon>().ToLookup(p => 0L);
        }

        private async Task<LoadedQuizz> LoadQuizzAsync(Guid id, string? userId)
        {
            var quizz = await _context.Quizzs
                .Where(q => q.Guid == id)
                .Include(q => q.Quizz_Questions).ThenInclude(qq => qq.Question).ThenInclude(q => q!.QuestionType).ThenInclude(t => t!.Difficulty)
                .Include(q => q.Quizz_Questions).ThenInclude(qq => qq.Question).ThenInclude(q => q!.Question_Answers!).ThenInclude(qa => qa.Answer)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (quizz == null)
                throw new QuizzGameException(StatusCodes.Status404NotFound, "Quiz introuvable.");

            var ownerId = _context.Entry(quizz).Property<string?>("IdentityUserId").CurrentValue;
            // Un quiz rattaché à un compte n'est visible que par son propriétaire ; 404 pour ne pas révéler son existence
            if (ownerId != null && ownerId != userId)
                throw new QuizzGameException(StatusCodes.Status404NotFound, "Quiz introuvable.");

            var questions = quizz.Quizz_Questions
                .Where(qq => qq.Question != null)
                .Select(qq => qq.Question!)
                .OrderBy(q => q.Order)
                .ToList();

            return new LoadedQuizz(quizz, questions, ownerId);
        }

        private static Question FindQuestion(List<Question> questions, int order) =>
            questions.FirstOrDefault(q => q.Order == order)
            ?? throw new QuizzGameException(StatusCodes.Status404NotFound, "Question introuvable.");

        private static List<Answer> Answers(Question question) =>
            (question.Question_Answers ?? new List<Question_Answer>())
                .Where(qa => qa.Answer != null)
                .Select(qa => qa.Answer!)
                .OrderBy(a => a.Order)
                .ToList();

        private static bool IsCorrect(Question question)
        {
            if (!question.Done) return false;
            var answers = Answers(question);
            return answers.Any(a => a.IsSelected) && answers.All(a => a.IsCorrect == a.IsSelected);
        }

        private static string SubjectKind(string? code) => code switch
        {
            Constantes.QTypPokDescReverse_Code => KindPokemonDescription,
            Constantes.QTypTyp_Code or Constantes.QTypPokTypVarious_Code => KindType,
            Constantes.QTypTalent_Code => KindAbility,
            Constantes.QTypTalentReverse_Code => KindAbilityDescription,
            _ => KindPokemon
        };

        private static string AnswerKind(Answer answer) =>
            answer.Type != null && answer.Type.StartsWith(KindStat + ":") ? KindStat : answer.Type ?? KindPokemon;

        private async Task<Lookup> BuildLookupAsync(IEnumerable<Question> questions, string lang)
        {
            var pokemonIds = new HashSet<long>();
            var typeIds = new HashSet<long>();
            var abilityIds = new HashSet<long>();
            bool needFamilies = false;

            foreach (var question in questions)
            {
                switch (SubjectKind(question.QuestionType?.Code))
                {
                    case KindPokemon: pokemonIds.Add(question.DataObjectID); break;
                    case KindPokemonDescription: pokemonIds.Add(question.DataObjectID); needFamilies = true; break;
                    case KindType: typeIds.Add(question.DataObjectID); break;
                    default: abilityIds.Add(question.DataObjectID); break;
                }

                foreach (var answer in Answers(question))
                {
                    switch (AnswerKind(answer))
                    {
                        case KindPokemon:
                        case KindStat: pokemonIds.Add(answer.IsCorrectID); break;
                        case KindPokemonDescription: pokemonIds.Add(answer.IsCorrectID); needFamilies = true; break;
                        case KindType: typeIds.Add(answer.IsCorrectID); break;
                        default: abilityIds.Add(answer.IsCorrectID); break;
                    }
                }
            }

            var pokemonIdList = pokemonIds.ToList();
            var typeIdList = typeIds.ToList();
            var abilityIdList = abilityIds.ToList();

            var pokemons = await _context.Pokemons.AsNoTracking()
                .Where(p => pokemonIdList.Contains(p.Id))
                .IncludeLang(lang)
                .ToListAsync();

            var families = Enumerable.Empty<Pokemon>().ToLookup(p => 0L);
            if (needFamilies)
            {
                var chainIds = pokemons.Where(p => p.EvolutionChainId != null).Select(p => p.EvolutionChainId!.Value).Distinct().ToList();
                var members = await _context.Pokemons.AsNoTracking()
                    .Where(p => p.EvolutionChainId != null && chainIds.Contains(p.EvolutionChainId.Value))
                    .IncludeLang(lang)
                    .ToListAsync();
                families = members.ToLookup(p => p.EvolutionChainId!.Value);
            }

            return new Lookup
            {
                Lang = lang,
                Pokemons = pokemons.ToDictionary(p => p.Id),
                Types = await _context.TypesPok.AsNoTracking().Where(t => typeIdList.Contains(t.Id)).ToDictionaryAsync(t => t.Id),
                Abilities = await _context.Abilities.AsNoTracking().Where(a => abilityIdList.Contains(a.Id)).ToDictionaryAsync(a => a.Id),
                Families = families
            };
        }

        private static string? QuestionLabel(Question question, Lookup lookup)
        {
            var label = question.QuestionType == null ? null : LangHelper.QuestionTypeLabel(question.QuestionType, lookup.Lang);
            if (label == null || question.QuestionType?.Code != Constantes.QTypPokStat_Code)
                return label;

            var statKind = Answers(question).Select(a => a.Type).FirstOrDefault(t => t != null && t.StartsWith(KindStat + ":"));
            var statKey = statKind?[(KindStat.Length + 1)..] ?? Constantes.Pv;
            var name = lookup.Pokemons.TryGetValue(question.DataObjectID, out var pokemon) ? LangHelper.PokemonName(pokemon, lookup.Lang) : "?";
            try
            {
                return string.Format(label, QuizzCatalog.StatLabel(statKey, lookup.Lang), name);
            }
            catch (FormatException)
            {
                return label;
            }
        }

        private static QuestionSubjectDto BuildSubject(Question question, Lookup lookup, bool reveal)
        {
            var questionType = question.QuestionType!;
            var kind = SubjectKind(questionType.Code);
            var dto = new QuestionSubjectDto { Kind = kind };

            switch (kind)
            {
                case KindPokemon:
                    lookup.Pokemons.TryGetValue(question.DataObjectID, out var pokemon);
                    bool nameIsAnswer = questionType.Code == Constantes.QTypPok_Code;
                    bool imageMasked = questionType.IsBlurred || questionType.IsHide;
                    if (pokemon != null && (reveal || (!nameIsAnswer && !imageMasked)))
                        dto.Name = LangHelper.PokemonName(pokemon, lookup.Lang);
                    dto.HasImage = pokemon != null && !string.IsNullOrEmpty(pokemon.PathImgNormal ?? pokemon.PathImgLegacy);
                    dto.HasSound = pokemon != null && !string.IsNullOrEmpty(pokemon.PathSound ?? pokemon.PathSoundCurrent ?? pokemon.PathSoundLegacy);
                    break;

                case KindPokemonDescription:
                    if (lookup.Pokemons.TryGetValue(question.DataObjectID, out var described))
                    {
                        dto.Description = MaskedDescription(described, lookup);
                        if (reveal)
                            dto.Name = LangHelper.PokemonName(described, lookup.Lang);
                    }
                    break;

                case KindType:
                    if (lookup.Types.TryGetValue(question.DataObjectID, out var type))
                    {
                        // "Quel est ce type ?" : le nom est la réponse
                        if (reveal || questionType.Code != Constantes.QTypTyp_Code)
                            dto.Name = LangHelper.TypeName(type, lookup.Lang);
                        dto.HasImage = !string.IsNullOrEmpty(type.PathIconHome ?? type.PathAutoHome ?? type.PathMiniGo);
                    }
                    break;

                case KindAbility:
                    if (lookup.Abilities.TryGetValue(question.DataObjectID, out var ability))
                    {
                        dto.Name = LangHelper.AbilityName(ability, lookup.Lang);
                        if (reveal)
                            dto.Description = LangHelper.AbilityDescription(ability, lookup.Lang);
                    }
                    break;

                case KindAbilityDescription:
                    if (lookup.Abilities.TryGetValue(question.DataObjectID, out var abilityDescribed))
                    {
                        dto.Description = LangHelper.AbilityDescription(abilityDescribed, lookup.Lang);
                        if (reveal)
                            dto.Name = LangHelper.AbilityName(abilityDescribed, lookup.Lang);
                    }
                    break;
            }

            return dto;
        }

        private static string? SubjectName(Question question, Lookup lookup) => BuildSubject(question, lookup, reveal: true).Name;

        private static string? AnswerLabel(Answer answer, Lookup lookup)
        {
            switch (AnswerKind(answer))
            {
                case KindStat:
                    return answer.Libelle;
                case KindPokemon:
                    return lookup.Pokemons.TryGetValue(answer.IsCorrectID, out var pokemon) ? LangHelper.PokemonName(pokemon, lookup.Lang) : answer.Libelle;
                case KindPokemonDescription:
                    return lookup.Pokemons.TryGetValue(answer.IsCorrectID, out var described) ? MaskedDescription(described, lookup) : answer.Libelle;
                case KindType:
                    return lookup.Types.TryGetValue(answer.IsCorrectID, out var type) ? LangHelper.TypeName(type, lookup.Lang) : answer.Libelle;
                case KindAbilityDescription:
                    return lookup.Abilities.TryGetValue(answer.IsCorrectID, out var abilityDescribed) ? LangHelper.AbilityDescription(abilityDescribed, lookup.Lang) : answer.Libelle;
                default:
                    return lookup.Abilities.TryGetValue(answer.IsCorrectID, out var ability) ? LangHelper.AbilityName(ability, lookup.Lang) : answer.Libelle;
            }
        }

        /// <summary>Description du Pokémon dont le nom (et celui de sa famille) est masqué, comme ConvertDescription dans Xamarin.</summary>
        private static string? MaskedDescription(Pokemon pokemon, Lookup lookup)
        {
            var info = LangHelper.Info(pokemon, lookup.Lang);
            var description = string.IsNullOrWhiteSpace(info?.DescriptionVx) ? info?.DescriptionVy : info.DescriptionVx;
            if (string.IsNullOrWhiteSpace(description))
                return description;

            var names = new List<string?> { info!.Name, info.DisplayName };
            if (pokemon.EvolutionChainId != null)
            {
                foreach (var member in lookup.Families[pokemon.EvolutionChainId.Value])
                {
                    var memberInfo = LangHelper.Info(member, lookup.Lang);
                    names.Add(memberInfo?.Name);
                    names.Add(memberInfo?.DisplayName);
                }
            }

            foreach (var name in names.Where(n => !string.IsNullOrWhiteSpace(n)).Distinct().OrderByDescending(n => n!.Length))
                description = Regex.Replace(description, Regex.Escape(name!), Mask, RegexOptions.IgnoreCase);

            return description;
        }

        private static AnswerResultDto BuildResult(ClassQuizz.Quizz quizz, List<Question> questions, Question question, bool alreadyAnswered)
        {
            var answers = Answers(question);
            return new AnswerResultDto
            {
                IsCorrect = IsCorrect(question),
                AlreadyAnswered = alreadyAnswered,
                CorrectAnswerIds = answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList(),
                SelectedAnswerIds = answers.Where(a => a.IsSelected).Select(a => a.Id).ToList(),
                QuizzDone = quizz.Done,
                NextOrder = questions.FirstOrDefault(q => !q.Done)?.Order
            };
        }

        private static QuizzSummaryDto Summarize(ClassQuizz.Quizz quizz, List<Question> questions, string? ownerId)
        {
            var generations = new List<int>();
            if (quizz.GenArceus) generations.Add(0);
            if (quizz.Gen1) generations.Add(1);
            if (quizz.Gen2) generations.Add(2);
            if (quizz.Gen3) generations.Add(3);
            if (quizz.Gen4) generations.Add(4);
            if (quizz.Gen5) generations.Add(5);
            if (quizz.Gen6) generations.Add(6);
            if (quizz.Gen7) generations.Add(7);
            if (quizz.Gen8) generations.Add(8);
            if (quizz.Gen9) generations.Add(9);

            var difficulties = new List<string>();
            if (quizz.Easy) difficulties.Add(Constantes.Easy_Code);
            if (quizz.Normal) difficulties.Add(Constantes.Normal_Code);
            if (quizz.Hard) difficulties.Add(Constantes.Hard_Code);

            return new QuizzSummaryDto
            {
                Id = quizz.Guid,
                CreatedAt = quizz.DateCreation,
                Generations = generations,
                Difficulties = difficulties,
                NbQuestions = questions.Count,
                NbAnswered = questions.Count(q => q.Done),
                NbCorrect = questions.Count(IsCorrect),
                Done = quizz.Done,
                NextOrder = questions.FirstOrDefault(q => !q.Done)?.Order,
                IsOwned = ownerId != null
            };
        }

        private static T Stamp<T>(T entity) where T : Identity
        {
            var now = DateTime.UtcNow;
            entity.Guid = Guid.NewGuid();
            entity.DateCreation = now;
            entity.DateModification = now;
            entity.UserCreation = "quizz-web";
            entity.UserModification = "quizz-web";
            return entity;
        }
        #endregion
    }
}
