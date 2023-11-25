using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class Pokemon_AttaqueRepository : Repository<Pokemon_Attaque>
    {
        #region Constructor
        public Pokemon_AttaqueRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
