using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsQuestionType<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<QuestionType> GetQuestionTypeRandom(bool easy, bool normal, bool hard);
    }
}
