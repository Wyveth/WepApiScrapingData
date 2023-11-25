using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_TalentRepository : Repository<Pokemon_Talent>
    {
        #region Constructor
        public Pokemon_TalentRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
