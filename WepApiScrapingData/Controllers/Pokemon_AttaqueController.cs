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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Pokemon_AttaqueController : GenericController<Pokemon_Attaque, Pokemon_AttaqueDto, Pokemon_AttaqueRepository>
    {
        public Pokemon_AttaqueController(ILogger<Pokemon_Attaque> logger, GenericMapper<Pokemon_Attaque, Pokemon_AttaqueDto> mapper, Pokemon_AttaqueRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
    }
}
