using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Data;

namespace WepApiScrapingData.Services.Game
{
    /// <summary>Nombre d'enregistrements de jeu (aperçu avant purge, ou résultat de la purge).</summary>
    public class GameDataCountsDto
    {
        /// <summary>Quiz solo (ou invités) supprimables.</summary>
        public int Quizzes { get; set; }
        public int Questions { get; set; }
        public int Answers { get; set; }
        /// <summary>Parties de pendu (solo et coop).</summary>
        public int HangmanGames { get; set; }
        /// <summary>Quiz conservés parce qu'ils servent à un duel (le classement en dépend).</summary>
        public int DuelQuizzesKept { get; set; }
    }

    /// <summary>
    /// Administration des données de jeu : vide les quiz et les parties de pendu enregistrés.
    /// Les quiz utilisés par des duels (Quizz1/Quizz2) sont conservés, avec leurs questions et réponses.
    /// </summary>
    public class GameDataAdminService
    {
        private readonly ScrapingContext _context;
        private readonly ILogger<GameDataAdminService> _logger;

        public GameDataAdminService(ScrapingContext context, ILogger<GameDataAdminService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>Quiz à supprimer : tous ceux qui ne sont rattachés à aucun duel.</summary>
        private IQueryable<long> PurgeableQuizzIds() =>
            _context.Quizzs
                .Where(q => !_context.Duels.Any(d => d.Quizz1Id == q.Id || d.Quizz2Id == q.Id))
                .Select(q => q.Id);

        /// <summary>Questions des quiz à supprimer (une question n'appartient qu'à un quiz, mais on vérifie quand même).</summary>
        private IQueryable<long> PurgeableQuestionIds()
        {
            var quizzIds = PurgeableQuizzIds();
            return _context.Quizz_Question
                .Where(qq => quizzIds.Contains(qq.QuizzId))
                .Select(qq => qq.QuestionId)
                .Where(questionId => !_context.Quizz_Question.Any(other => other.QuestionId == questionId && !quizzIds.Contains(other.QuizzId)));
        }

        public async Task<GameDataCountsDto> PreviewAsync()
        {
            var questionIds = PurgeableQuestionIds();
            return new GameDataCountsDto
            {
                Quizzes = await PurgeableQuizzIds().CountAsync(),
                Questions = await questionIds.Distinct().CountAsync(),
                Answers = await _context.Question_Answer.Where(qa => questionIds.Contains(qa.QuestionId)).Select(qa => qa.AnswerId).Distinct().CountAsync(),
                HangmanGames = await _context.HangmanGames.CountAsync(),
                DuelQuizzesKept = await _context.Quizzs.CountAsync(q => _context.Duels.Any(d => d.Quizz1Id == q.Id || d.Quizz2Id == q.Id))
            };
        }

        /// <summary>
        /// Supprime, dans une seule transaction, les quiz hors duels (avec leurs questions et réponses) et toutes les parties de pendu.
        /// Les suppressions se font en masse (ExecuteDelete), dans l'ordre imposé par les clés étrangères (DeleteBehavior.Restrict).
        /// </summary>
        public async Task<GameDataCountsDto> PurgeAsync(string adminUserId)
        {
            var result = new GameDataCountsDto();

            await using var transaction = await _context.Database.BeginTransactionAsync();

            // Identifiants figés en table temporaire côté SQL : les sous-requêtes ne doivent pas changer pendant les suppressions
            var questionIds = await PurgeableQuestionIds().Distinct().ToListAsync();
            var answerIds = await _context.Question_Answer
                .Where(qa => questionIds.Contains(qa.QuestionId))
                .Select(qa => qa.AnswerId)
                .Distinct()
                .ToListAsync();
            var quizzIds = await PurgeableQuizzIds().ToListAsync();

            foreach (var chunk in questionIds.Chunk(2000))
                await _context.Question_Answer.Where(qa => chunk.Contains(qa.QuestionId)).ExecuteDeleteAsync();
            foreach (var chunk in quizzIds.Chunk(2000))
                await _context.Quizz_Question.Where(qq => chunk.Contains(qq.QuizzId)).ExecuteDeleteAsync();
            foreach (var chunk in questionIds.Chunk(2000))
                result.Questions += await _context.Questions.Where(q => chunk.Contains(q.Id)).ExecuteDeleteAsync();
            foreach (var chunk in answerIds.Chunk(2000))
                result.Answers += await _context.Answers
                    .Where(a => chunk.Contains(a.Id) && !_context.Question_Answer.Any(qa => qa.AnswerId == a.Id))
                    .ExecuteDeleteAsync();
            foreach (var chunk in quizzIds.Chunk(2000))
                result.Quizzes += await _context.Quizzs.Where(q => chunk.Contains(q.Id)).ExecuteDeleteAsync();

            result.HangmanGames = await _context.HangmanGames.ExecuteDeleteAsync();

            await transaction.CommitAsync();

            result.DuelQuizzesKept = await _context.Quizzs.CountAsync();
            _logger.LogWarning(
                "Purge des données de jeu par {AdminUserId} : {Quizzes} quiz, {Questions} questions, {Answers} réponses, {Hangman} parties de pendu supprimés ; {Kept} quiz de duel conservés.",
                adminUserId, result.Quizzes, result.Questions, result.Answers, result.HangmanGames, result.DuelQuizzesKept);

            return result;
        }
    }
}
