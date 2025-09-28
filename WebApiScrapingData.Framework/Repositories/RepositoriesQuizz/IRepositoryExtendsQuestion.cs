using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsQuestion<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<List<Question>> GenerateQuestions(Quizz quizz);
        Task<int> GetNbQuestionByDifficulty(Quizz quizz);
    }
}
