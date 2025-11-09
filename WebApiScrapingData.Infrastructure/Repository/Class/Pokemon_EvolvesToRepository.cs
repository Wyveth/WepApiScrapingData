using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_EvolvesToRepository : Repository<Pokemon_EvolvesTo>
    {
        #region Constructor
        public Pokemon_EvolvesToRepository(ScrapingContext context) : base(context) { }
        #endregion

        public async Task<Pokemon_EvolvesTo?> GetAsync(long pokemonId, long evolvesToId)
        {
            return await _context.Pokemon_EvolveTo.FirstOrDefaultAsync(x => x.PokemonId == pokemonId && x.EvolveToId == evolvesToId);
        }

        public async Task<bool> ExistsAsync(long pokemonId, long evolvesToId)
        {
            return await _context.Pokemon_EvolveTo.AnyAsync(x => x.PokemonId == pokemonId && x.EvolveToId == evolvesToId);
        }
    }
}
