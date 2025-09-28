using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Body;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class PokemonController : GenericController<Pokemon, PokemonDto, PokemonRepository>
    {
        #region Constructors
        public PokemonController(ILogger<Pokemon> logger, GenericMapper<Pokemon, PokemonDto> mapper, PokemonRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("{limit}/{max}")]
        public async Task<IEnumerable<Pokemon>> GetAllinDB(int max = 20, bool limit = true)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            if (limit)
                return pokemons.Take(max);
            else
                return pokemons;
        }

        [HttpGet]
        [Route("Light/{limit}/{max}")]
        public async Task<IEnumerable<Pokemon>> GetAllLight(int max = 20, bool limit = true, int? gen = null, bool desc = false)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAllLight(gen, desc);
            if (limit)
                return pokemons.Take(max);
            else
                return pokemons;
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public Task<IEnumerable<Pokemon>> GetFindByName(string name)
        {
            return _repository.Find(m => m.FR.Name.Equals(name));
        }

        [HttpGet]
        [Route("FindByNumber/{number}")]
        public Task<IEnumerable<Pokemon>> GetFindByNumber(string number)
        {
            return _repository.Find(m => m.Number.Equals(number));
        }

        [HttpGet]
        [Route("Research")]
        public async Task<IEnumerable<Pokemon>> Research([FromBody] Research research, string loc, int? gen = null, bool desc = false)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAllLight(gen, desc);
            return null;
            //return pokemons.Where(m => m.);
        }

        [HttpGet]
        [Route("GetEvol/{family}")]
        public async Task<IEnumerable<Pokemon>> GetEvol(string family)
        {
            return await _repository.GetFamilyWithoutVariantAsync(family);
        }

        [HttpGet]
        [Route("GetVariant/{number}")]
        public async Task<IEnumerable<Pokemon>> GetVariant(string number)
        {
            return await _repository.GetAllVariantAsync(number);
        }
        #endregion
    }
}
