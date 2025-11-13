using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_EvolutionChainRepository : Repository<Pokemon_EvolutionChain>
    {
        #region Constructor
        public Pokemon_EvolutionChainRepository(ScrapingContext context) : base(context) { }
        #endregion

        public async Task<Pokemon_EvolutionChain?> GetAsync(long pokemonId, long evolutionChainId)
        {
            return await _context.Pokemon_EvolutionChain.FirstOrDefaultAsync(x => x.PokemonId == pokemonId && x.EvolutionChainId == evolutionChainId);
        }

        public async Task<bool> ExistsAsync(long pokemonId, long evolutionChainId)
        {
            return await _context.Pokemon_EvolutionChain.AnyAsync(x => x.PokemonId == pokemonId && x.EvolutionChainId == evolutionChainId);
        }
    }
}
