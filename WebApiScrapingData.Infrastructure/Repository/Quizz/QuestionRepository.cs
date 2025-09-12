using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Repository.Quizz;
using WebApiScrapingData.Infrastructure.Utils;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionRepository : Repository<Question>, IRepositoryExtendsQuestion<Question>
    {
        #region fields
        private readonly QuestionTypeRepository _repositoryQT;
        private readonly Question_AnswerRepository _repositoryQA;
        #endregion

        #region Constructor
        public QuestionRepository(ScrapingContext context, QuestionTypeRepository repositoryQT, Question_AnswerRepository repositoryQA) : base(context) {
            _repositoryQT = repositoryQT;
            _repositoryQA = repositoryQA;
        }
        #endregion

        #region Public Methods
        public async override Task<Question?> Get(long id)
        {
            return await Task.FromResult(_context.Questions.Include(q => q.QuestionType).Include(q => q.Question_Answers).ThenInclude(qa => qa.Answer).FirstOrDefault(q => q.Id == id));
        }
        
        public async Task<List<Question>> GenerateQuestions(ClassQuizz.Quizz quizz)
        {
            int nbQuestionMax = await GetNbQuestionByDifficulty(quizz);

            List<Question> questions = new List<Question>();

            for (int nbQuestion = 0; nbQuestion < nbQuestionMax; nbQuestion++)
            {
                await Task.Run(async () =>
                {
                    QuestionType questionType = await _repositoryQT.GetQuestionTypeRandom(quizz.Easy, quizz.Normal, quizz.Hard);
                    
                    Question question = new Question()
                    {
                        Order = nbQuestion + 1,
                        QuestionType = questionType,
                        Done = false
                    };

                    await AddAsync(question);

                    question.Question_Answers = await _repositoryQA.GenerateQuestionAnswers(quizz, question, questionType);

                    questions.Add(question);
                    Debug.Write("Creation Question:" + questions.Count + "/" + nbQuestionMax);
                });
            }
            return questions;
        }

        public async Task<int> GetNbQuestionByDifficulty(ClassQuizz.Quizz quizz)
        {
            int nbQuestionMax = 0;
            if (quizz.Easy)
                nbQuestionMax = 10;
            else if (quizz.Normal)
                nbQuestionMax = 15;
            else if (quizz.Hard)
                nbQuestionMax = 20;

            return await Task.FromResult(nbQuestionMax);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        
    }
}
