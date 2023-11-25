using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionTypeRepository : Repository<QuestionType>
    {
        #region Constructor
        public QuestionTypeRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
