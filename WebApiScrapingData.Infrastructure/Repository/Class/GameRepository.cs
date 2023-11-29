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
    }
}
