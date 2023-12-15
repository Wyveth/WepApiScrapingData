using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_TalentRepository : Repository<Pokemon_Talent>
    {
        #region Constructor
        public Pokemon_TalentRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        public async Task<List<Pokemon_Talent>> GetTalentsByPokemon(long pokemonId)
        {
            var result = await _context.Pokemon_Talent.ToListAsync();
            return result.FindAll(m => m.PokemonId.Equals(pokemonId));
        }
        #endregion
    }
}
