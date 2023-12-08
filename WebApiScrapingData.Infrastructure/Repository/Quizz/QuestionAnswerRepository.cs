using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Quizz
{
    public class QuestionAnswerRepository : Repository<QuestionAnswer>
    {
        #region Constructor
        public QuestionAnswerRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
