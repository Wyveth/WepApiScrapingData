using Microsoft.AspNetCore.Identity;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsQuizz<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GenerateQuizz(IdentityUser profile, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool genArceus, bool easy, bool normal, bool hard);
    }
}
