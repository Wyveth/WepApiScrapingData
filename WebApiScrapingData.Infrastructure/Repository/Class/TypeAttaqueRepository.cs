using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class TypeAttaqueRepository : Repository<TypeAttaque>
    {
        #region Constructor
        public TypeAttaqueRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public async Task<TypeAttaque> GetByName(string name)
        {
            return await this._context.TypeAttaques.SingleAsync(x => x.Name_FR.Equals(name));
        }
        #endregion
        #endregion
    }
}
