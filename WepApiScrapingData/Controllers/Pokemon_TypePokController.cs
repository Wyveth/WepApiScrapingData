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
    public class Pokemon_TypePokController : GenericController<Pokemon_TypePok, Pokemon_TypePokDto, Pokemon_TypePokRepository>
    {
        public Pokemon_TypePokController(ILogger<Pokemon_TypePok> logger, GenericMapper<Pokemon_TypePok, Pokemon_TypePokDto> mapper, Pokemon_TypePokRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
    }
}
