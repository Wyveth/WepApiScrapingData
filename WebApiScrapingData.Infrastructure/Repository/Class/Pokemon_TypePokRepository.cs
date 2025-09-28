using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_TypePokRepository : Repository<Pokemon_TypePok>
    {
        #region Constructor
        public Pokemon_TypePokRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        public Task<List<Pokemon_TypePok>> GetPokemonsByTypePok(long typePokId)
        {
            return _context.Pokemon_TypePok.Where(m => m.TypePokId.Equals(typePokId)).ToListAsync();
        }
        #endregion
    }
}
