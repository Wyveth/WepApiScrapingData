using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class GameRepository : Repository<Game>
    {
        #region Constructor
        public GameRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public async Task<Game> GetByName(string name)
        {
            return await this._context.Games.SingleAsync(x => x.Name_FR.Equals(name));
        }
        #endregion
        #endregion
    }
}
