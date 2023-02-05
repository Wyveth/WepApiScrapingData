using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsAnswer<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Pokemon> pokemonsAnswer);
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<TypePok> typesAnswer);
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer, bool Reverse);
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer);
        Task<string> GenerateCorrectAnswersDesc(QuestionType questionType, List<Pokemon> pokemonsAnswer);
        Task<string> GenerateCorrectAnswersStat(QuestionType questionType, List<Pokemon> pokemonsAnswer, string typeStat);
        Task<List<Answer>> GenerateAnswers(Quizz quizz, QuestionType questionType, List<Answer> answers);
        Task<string> ConvertDescription(Pokemon pokemon);
    }
}
