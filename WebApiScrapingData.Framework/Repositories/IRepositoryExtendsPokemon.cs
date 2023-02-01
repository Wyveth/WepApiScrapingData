using WebApiScrapingData.Domain.Class;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsPokemon<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllLight();

        Task<List<Pokemon>> GetFamilyWithoutVariantAsync(string family);
            
        Task<IEnumerable<TEntity>> GetAllVariantAsync(string number);

        Task SaveInfoPokemonAttackInDB(string json);
    }
}
