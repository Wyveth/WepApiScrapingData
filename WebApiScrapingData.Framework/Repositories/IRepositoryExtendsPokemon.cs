using System.Linq.Expressions;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Interface;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Core.Repositories
{
    public interface IRepositoryExtendsPokemon<T> : IRepository<T> where T : class, ITIdentity
    {
        Task<T?> FirstOrDefaultByName(string name, string lang = "FR");

        Task<IEnumerable<T>> GetAllByLang(string lang = "FR");

        Task<IEnumerable<Pokemon>> GetAllLight(int? gen = null, bool desc = false, int max = 0, string lang = "FR");

        Task<IEnumerable<Pokemon>> GetFamilyWithoutVariantAsync(string family, string lang = "FR");

        Task<IEnumerable<Pokemon>> GetAllVariantAsync(string number, string lang = "FR");

        Task<Pokemon> GetPokemonRandom(bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus);
        
        Task<Pokemon> GetPokemonRandom(ClassQuizz.Quizz quizz);

        Task<Pokemon> GetPokemonRandom(ClassQuizz.Quizz quizz, TypePok typePok, List<Pokemon> alreadySelected);
        
        Task<Pokemon> GetPokemonRandom(ClassQuizz.Quizz quizz, List<Pokemon> alreadySelected);

        Task SaveInfoPokemonAttackInDB(string json);

        Task<bool> ImportJsonToDb(string json);
    }
}
