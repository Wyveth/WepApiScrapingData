using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsQuestion<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<string> GenerateQuestions(bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool genArceus, bool easy, bool normal, bool hard);
        Task<int> GetNbQuestionByDifficulty(bool easy, bool normal, bool hard);
    }
}
