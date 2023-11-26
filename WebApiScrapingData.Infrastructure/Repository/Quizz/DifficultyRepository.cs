using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class DifficultyRepository : Repository<Difficulty>
    {
        #region Constructor
        public DifficultyRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
