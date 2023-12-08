using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pokemon_TalentController : GenericController<Pokemon_Talent, Pokemon_TalentDto, Pokemon_TalentRepository>
    {
        public Pokemon_TalentController(ILogger<Pokemon_Talent> logger, GenericMapper<Pokemon_Talent, Pokemon_TalentDto> mapper, Pokemon_TalentRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
    }
}
