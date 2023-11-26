using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_WeaknessRepository : Repository<Pokemon_Weakness>
    {
        #region Constructor
        public Pokemon_WeaknessRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
