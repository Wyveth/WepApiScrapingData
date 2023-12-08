using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuizzDifficultyRepository : Repository<QuizzDifficulty>
    {
        #region Constructor
        public QuizzDifficultyRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
