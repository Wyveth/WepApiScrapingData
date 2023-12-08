using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Quizz
{
    public class Question_AnswerRepository : Repository<Question_Answer>
    {
        #region Constructor
        public Question_AnswerRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
