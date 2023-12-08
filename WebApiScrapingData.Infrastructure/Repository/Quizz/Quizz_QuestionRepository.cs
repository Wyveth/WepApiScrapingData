using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Quizz
{
    public class Quizz_QuestionRepository : Repository<Quizz_Question>
    {
        #region Constructor
        public Quizz_QuestionRepository(ScrapingContext context) : base(context) { }
        #endregion
    }
}
