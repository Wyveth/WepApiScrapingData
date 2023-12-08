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
    public class Pokemon_WeaknessController : GenericController<Pokemon_Weakness, Pokemon_WeaknessDto, Pokemon_WeaknessRepository>
    {
        public Pokemon_WeaknessController(ILogger<Pokemon_Weakness> logger, GenericMapper<Pokemon_Weakness, Pokemon_WeaknessDto> mapper, Pokemon_WeaknessRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
    }
}
