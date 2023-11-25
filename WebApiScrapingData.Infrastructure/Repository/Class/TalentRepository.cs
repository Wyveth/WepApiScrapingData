using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class TalentRepository : Repository<Talent>
    {
        #region Constructor
        public TalentRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public async Task<Talent> GetByName(string name)
        {
            return await this._context.Talents.SingleAsync(x => x.Name_FR.Equals(name));
        }
        #endregion
        #endregion
    }
}
