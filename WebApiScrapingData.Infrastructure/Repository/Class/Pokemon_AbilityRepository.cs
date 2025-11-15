using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_AbilityRepository : Repository<Pokemon_Ability>
    {
        #region Constructor
        public Pokemon_AbilityRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        public async Task<List<Pokemon_Ability>> GetTalentsByPokemon(long pokemonId)
        {
            var result = await _context.Pokemon_Ability.ToListAsync();
            return result.FindAll(m => m.PokemonId.Equals(pokemonId));
        }
        #endregion
    }
}
