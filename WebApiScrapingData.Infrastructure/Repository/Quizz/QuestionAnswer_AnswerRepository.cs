using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Quizz
{
    public class QuestionAnswer_AnswerRepository : Repository<QuestionAnswer_Answer>
    {
        #region Constructor
        public QuestionAnswer_AnswerRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
