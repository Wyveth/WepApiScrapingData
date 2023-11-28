using HtmlAgilityPack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Body;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class PokemonController : ControllerBase
    {
        #region Fields
        private readonly PokemonRepository _repository;
        #endregion

        #region Constructors
        public PokemonController(PokemonRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            return await _repository.GetAll();
        }

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
        public async Task<IEnumerable<Pokemon>> GetAllLight(int max = 20, bool limit = true)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAllLight();
            if (limit)
                return pokemons.Take(max);
            else
                return pokemons;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Pokemon> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public IEnumerable<Pokemon> GetFindByName(string name)
        {
            return _repository.Find(m => m.FR.Name.Equals(name));
        }

        [HttpGet]
        [Route("FindByNumber/{number}")]
        public IEnumerable<Pokemon> GetFindByNumber(string number)
        {
            return _repository.Find(m => m.Number.Equals(number));
        }

        [HttpGet]
        [Route("Research")]
        public async Task<IEnumerable<Pokemon>> Research([FromBody] Research research, string loc)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAllLight();
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
