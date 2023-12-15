using Microsoft.AspNetCore.Identity;
using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsQuizz<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<T> GenerateQuizz(IdentityUser identityUser, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, bool easy, bool normal, bool hard);
    }
}
