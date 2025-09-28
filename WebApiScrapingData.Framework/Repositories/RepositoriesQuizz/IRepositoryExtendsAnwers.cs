using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsAnswer<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<Pokemon> pokemonsAnswer);
        Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<TypePok> typesAnswer);
        Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer, bool Reverse);
        Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer);
        Task<List<Answer>> GenerateCorrectAnswersDesc(QuestionType questionType, List<Pokemon> pokemonsAnswer);
        Task<List<Answer>> GenerateCorrectAnswersStat(QuestionType questionType, List<Pokemon> pokemonsAnswer, string typeStat);
        Task<List<Answer>> GenerateAnswers(Quizz quizz, QuestionType questionType, List<Answer> answers);
        Task<string> ConvertDescription(Pokemon pokemon);
    }
}
