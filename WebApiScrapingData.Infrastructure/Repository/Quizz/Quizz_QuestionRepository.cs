using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Infrastructure.Repository.Quizz
{
    public class Quizz_QuestionRepository : Repository<Quizz_Question>
    {
        #region fields
        private readonly QuestionRepository _repositoryQ;
        #endregion

        #region Constructor
        public Quizz_QuestionRepository(ScrapingContext context, QuestionRepository repositoryQ) : base(context) {
            _repositoryQ = repositoryQ;
        }
        #endregion

        #region Public Methods
        public async Task<List<Quizz_Question>> GenerateQuizzQuestions(ClassQuizz.Quizz quizz)
        {
            List<Question> questions = await _repositoryQ.GenerateQuestions(quizz);

            List<Quizz_Question> quizzQuestions = new List<Quizz_Question>();
            foreach (var question in questions)
            {
                Quizz_Question quizzQuestion = new Quizz_Question()
                {
                    Question = question,
                    Quizz = quizz,
                };
                quizzQuestions.Add(quizzQuestion);
            }

            await AddRangeAsync(quizzQuestions);
            return quizzQuestions;
        }
        #endregion
    }
}
