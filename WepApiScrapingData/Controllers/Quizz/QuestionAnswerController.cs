using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository;
using WebApiScrapingData.Infrastructure.Repository.Quizz;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete.Quizz;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers.Quizz
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class QuestionAnswerController : GenericController<QuestionAnswer, QuestionAnswerDto, QuestionAnswerRepository>
    {
        #region Constructor
        public QuestionAnswerController(ILogger<QuestionAnswer> logger, GenericMapper<QuestionAnswer, QuestionAnswerDto> mapper, QuestionAnswerRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
        #endregion
    }
}
