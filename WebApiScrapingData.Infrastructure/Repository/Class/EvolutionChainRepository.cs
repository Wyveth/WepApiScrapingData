using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class EvolutionChainRepository : Repository<EvolutionChain>
    {
        #region Constructor
        public EvolutionChainRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
