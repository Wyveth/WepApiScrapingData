using Microsoft.Data.SqlClient.Server;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Infrastructure.Repository.Quizz
{
    public class Question_AnswerRepository : Repository<Question_Answer>
    {
        #region fields
        private readonly AnswerRepository _repositoryA;
        #endregion

        #region Constructor
        public Question_AnswerRepository(ScrapingContext context, AnswerRepository repositoryA) : base(context) {
            _repositoryA = repositoryA;
        }
        #endregion

        #region Public Methods
        public async Task<List<Question_Answer>> GenerateQuestionAnswers(ClassQuizz.Quizz quizz, Question question, QuestionType questionType)
        {
            
            List<Answer> answers = await _repositoryA.GenerateAnswers(quizz, question, questionType);
            
            List<Question_Answer> question_Answers = new List<Question_Answer>();

            foreach (Answer answer in answers)
            {
                Question_Answer questionAnswer = new Question_Answer()
                {
                    Question = question,
                    Answer = answer
                };
                
                question_Answers.Add(questionAnswer);
            }

            await AddRangeAsync(question_Answers);
            return question_Answers;
        }
        #endregion
    }
}
