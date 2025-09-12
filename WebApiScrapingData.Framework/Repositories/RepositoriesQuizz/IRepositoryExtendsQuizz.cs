using Microsoft.AspNetCore.Identity;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsQuizz<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<T> GenerateQuizz(IdentityUser identityUser, QuizzGenerationOptions options);
    }
}
