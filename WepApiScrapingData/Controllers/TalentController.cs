using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TalentController : GenericController<Talent, TalentDto, TalentRepository>
    {
        #region Constructors
        public TalentController(ILogger<Talent> logger, GenericMapper<Talent, TalentDto> mapper, TalentRepository repository) : base(logger, mapper, repository)
        {
        }
        #endregion
    }
}
