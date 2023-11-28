using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{

    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class GameController : GenericController<Game, GameDto, GameRepository>
    {
        #region Constructors
        public GameController(ILogger<Game> logger,
            GenericMapper<Game, GameDto> mapper,
            GameRepository service) : base(logger, mapper, service)
        {
        }
        #endregion

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<Game>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
        }
    }
}
