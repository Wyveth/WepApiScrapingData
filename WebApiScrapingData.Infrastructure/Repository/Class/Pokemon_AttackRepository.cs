using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_AttackRepository : Repository<Pokemon_Attack>
    {
        #region Constructor
        public Pokemon_AttackRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
