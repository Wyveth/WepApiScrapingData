using Microsoft.AspNetCore.Identity;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuizzRepository : Repository<ClassQuizz.Quizz>, IRepositoryExtendsQuizz<ClassQuizz.Quizz>
    {
        #region fields
        private readonly QuestionRepository _repositoryQ;
        #endregion

        #region Constructor
        public QuizzRepository(ScrapingContext context,QuestionRepository repositoryQ) : base(context)
        {
            _repositoryQ = repositoryQ;
        }
        #endregion

        #region Public Methods
        public async Task<ClassQuizz.Quizz> GenerateQuizz(IdentityUser identityUser, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, bool easy, bool normal, bool hard)
        {
            ClassQuizz.Quizz quizz = new ClassQuizz.Quizz()
            {
                Quizz_Questions = await _repositoryQ.GenerateQuestions(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, easy, normal, hard),
                Gen1 = gen1,
                Gen2 = gen2,
                Gen3 = gen3,
                Gen4 = gen4,
                Gen5 = gen5,
                Gen6 = gen6,
                Gen7 = gen7,
                Gen8 = gen8,
                Gen9 = gen9,
                GenArceus = genArceus,
                Easy = easy,
                Normal = normal,
                Hard = hard,
                Done = false,
                IdentityUser = identityUser
            };

            //await CreateAsync(quizz);

            return await Task.FromResult(quizz);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
