using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionRepository : Repository<Question>, IRepositoryExtendsQuestion<Question>
    {
        #region Constructor
        public QuestionRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        public Task<string> GenerateQuestions(bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool genArceus, bool easy, bool normal, bool hard)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbQuestionByDifficulty(bool easy, bool normal, bool hard)
        {
            throw new NotImplementedException();
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
