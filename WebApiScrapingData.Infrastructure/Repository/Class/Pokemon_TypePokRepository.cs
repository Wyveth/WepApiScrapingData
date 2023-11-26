using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_TypePokRepository : Repository<Pokemon_TypePok>
    {
        #region Constructor
        public Pokemon_TypePokRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
