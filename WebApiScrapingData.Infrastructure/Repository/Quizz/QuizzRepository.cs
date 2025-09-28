using Microsoft.AspNetCore.Identity;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Repository.Quizz;
using WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuizzRepository : Repository<ClassQuizz.Quizz>, IRepositoryExtendsQuizz<ClassQuizz.Quizz>
    {
        #region fields
        private readonly Quizz_QuestionRepository _repositoryQQ;
        #endregion

        #region Constructor
        public QuizzRepository(ScrapingContext context, Quizz_QuestionRepository repositoryQQ) : base(context)
        {
            _repositoryQQ = repositoryQQ;
        }
        #endregion
             
        #region Public Methods
        public async Task<ClassQuizz.Quizz> GenerateQuizz(IdentityUser? identityUser, QuizzGenerationOptions options)
        {
            ClassQuizz.Quizz quizz = new ClassQuizz.Quizz()
            {
                Gen1 = options.Gen1,
                Gen2 = options.Gen2,
                Gen3 = options.Gen3,
                Gen4 = options.Gen4,
                Gen5 = options.Gen5,
                Gen6 = options.Gen6,
                Gen7 = options.Gen7,
                Gen8 = options.Gen8,
                Gen9 = options.Gen9,
                GenArceus = options.GenArceus,
                Easy = options.Easy,
                Normal = options.Normal,
                Hard = options.Hard,
                Done = false,
                IdentityUser = identityUser
            };

            await AddAsync(quizz);

            quizz.Quizz_Questions = await _repositoryQQ.GenerateQuizzQuestions(quizz);

            return quizz;
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
