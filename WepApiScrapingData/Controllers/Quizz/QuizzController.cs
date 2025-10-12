using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Class = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Repository;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete.Quizz;
using WepApiScrapingData.ExtensionMethods;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Data;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Repository.Quizz;
using WebApiScrapingData.Domain.Class.Quizz;

namespace WepApiScrapingData.Controllers.Quizz
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class QuizzController : GenericController<Class.Quizz, QuizzDto, QuizzRepository>
    {
        #region fields
        private readonly Quizz_QuestionRepository _repositoryQQ;
        private readonly QuestionRepository _repositoryQ;
        private readonly Question_AnswerRepository _repositoryQA;
        private readonly AnswerRepository _repositoryA;
        #endregion

        #region Constructors
        public QuizzController(ILogger<Class.Quizz> logger, GenericMapper<Class.Quizz, QuizzDto> mapper, QuizzRepository repository, ScrapingContext context, Quizz_QuestionRepository repositoryQQ, QuestionRepository repositoryQ, Question_AnswerRepository repositoryQA, AnswerRepository repositoryA) : base(logger, mapper, repository, context)
        {
            _repositoryQQ = repositoryQQ;
            _repositoryQ = repositoryQ;
            _repositoryQA = repositoryQA;
            _repositoryA = repositoryA;
        }
        #endregion

        [HttpGet]
        [Route("GenerateQuizz")]
        public async Task<ClassQuizz.Quizz> GenerateQuizz()
        {
            QuizzGenerationOptions options = new QuizzGenerationOptions
            {
                Gen1 = true,
                Gen2 = false,
                Gen3 = false,
                Gen4 = false,
                Gen5 = false,
                Gen6 = false,
                Gen7 = false,
                Gen8 = false,
                Gen9 = false,
                Easy = true,
                Normal = false,
                Hard = false
            };

            return await _repository.GenerateQuizz(null, options);
        }

        [HttpDelete]
        [Route("PurgeQuizzDone/{id}")]
        public async Task PurgeQuizzDone(int id)
        {
            ClassQuizz.Quizz quizz = _repository.Get(id).Result;
            IEnumerable<Quizz_Question> quizz_questions = null;
            IEnumerable<Question_Answer> question_answers = null;

            if (quizz != null)
            {
                quizz_questions = _repositoryQQ.Find(m => m.QuizzId.Equals(quizz.Id)).Result;
                await _repositoryQQ.RemoveRangeAsync(quizz_questions);

                foreach (Quizz_Question quizz_question in quizz_questions)
                {
                    question_answers = _repositoryQA.Find(m => m.QuestionId.Equals(quizz_question.QuestionId)).Result;
                    await _repositoryQA.RemoveRangeAsync(question_answers);

                    foreach (Question_Answer question_answer in question_answers)
                    {
                        await _repositoryA.RemoveAsync(_repositoryA.Get(question_answer.AnswerId).Result);
                    }

                    await _repositoryQ.RemoveAsync(_repositoryQ.Get(quizz_question.QuestionId).Result);
                }

                await _repository.RemoveAsync(quizz);
            }
        }
    }
}
