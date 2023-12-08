using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class AnswerRepository : Repository<Answer>, IRepositoryExtendsAnswer<Answer>
    {
        #region Constructor
        public AnswerRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        public Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Pokemon> pokemonsAnswer)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateCorrectAnswers(QuestionType questionType, List<TypePok> typesAnswer)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer, bool Reverse)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateCorrectAnswersDesc(QuestionType questionType, List<Pokemon> pokemonsAnswer)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateCorrectAnswersStat(QuestionType questionType, List<Pokemon> pokemonsAnswer, string typeStat)
        {
            throw new NotImplementedException();
        }

        public Task<List<Answer>> GenerateAnswers(ClassQuizz.Quizz quizz, QuestionType questionType, List<Answer> answers)
        {
            throw new NotImplementedException();
        }

        public Task<string> ConvertDescription(Pokemon pokemon)
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
