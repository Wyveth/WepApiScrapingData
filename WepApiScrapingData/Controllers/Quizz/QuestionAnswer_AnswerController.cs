using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository.Quizz;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete.Quizz;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers.Quizz
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class QuestionAnswer_AnswerController : GenericController<QuestionAnswer_Answer, QuestionAnswer_AnswerDto, QuestionAnswer_AnswerRepository>
    {
        #region Constructor
        public QuestionAnswer_AnswerController(ILogger<QuestionAnswer_Answer> logger, GenericMapper<QuestionAnswer_Answer, QuestionAnswer_AnswerDto> mapper, QuestionAnswer_AnswerRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
        #endregion
    }
}
