using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class TypeAttackRepository : Repository<TypeAttack>
    {
        #region Constructor
        public TypeAttackRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public async Task<TypeAttack> GetByName(string name)
        {
            return await this._context.TypeAttacks.FirstOrDefaultAsync(x => x.Name_FR.Equals(name));
        }
        #endregion
        #endregion
    }
}
