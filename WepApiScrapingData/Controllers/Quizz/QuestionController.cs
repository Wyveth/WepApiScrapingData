using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete.Quizz;
using WepApiScrapingData.ExtensionMethods;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WepApiScrapingData.Controllers.Quizz
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class QuestionController : GenericController<Question, QuestionDto, QuestionRepository>
    {
        #region fields
        private readonly QuizzRepository _quizzRepository;
        private readonly AnswerRepository _repositoryA;
        #endregion

        #region Constructor
        public QuestionController(ILogger<Question> logger, GenericMapper<Question, QuestionDto> mapper, QuizzRepository quizzRepository,  QuestionRepository repository, AnswerRepository repositoryA, ScrapingContext context) : base(logger, mapper, repository, context)
        {
            _quizzRepository = quizzRepository;
            _repositoryA = repositoryA;
        }
        #endregion

        [HttpGet]
        [Route("GenerateAnswers/{quizzId}/{id}")]
        public async Task<Question> GenerateQuizz(int quizzId, int id)
        {
            ClassQuizz.Quizz quizz = await _quizzRepository.Get(quizzId);
            Question question = await _repository.Get(id);

            List<Answer> answers = new List<Answer>();
            foreach (Question_Answer question_answer in question.Question_Answers)
                answers.Add(question_answer.Answer);
            

            answers = await _repositoryA.GenerateAnswers(quizz, question.QuestionType, answers);

            foreach (Answer answer in answers)
            {
                Question_Answer question_answer = new Question_Answer()
                {
                    Answer = answer,
                    Question = question
                };
                if (answer.Id == 0)
                {
                    question.Question_Answers.Add(question_answer);
                }
            }

            return question;
        }
    }
}
