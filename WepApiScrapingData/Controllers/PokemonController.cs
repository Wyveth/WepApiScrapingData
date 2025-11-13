using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Mapper;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class PokemonController : GenericController<Pokemon, PokemonDto, PokemonRepository>
    {
        private PokemonMapper _mapper;
        #region Constructors
        public PokemonController(ILogger<Pokemon> logger, PokemonMapper mapper, PokemonRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("Light")]
        public async Task<ActionResult<IEnumerable<PokemonLightDto>>> GetAllLight(
            [FromQuery] int? gen = null,
            [FromQuery] bool desc = false,
            [FromQuery] int max = 0,
            [FromQuery] string lang = Constantes.FR)
        {
            var entities = await _repository.GetAllLight(gen, desc, max, lang);

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.MapLight(p, lang)).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<PokemonDto?>> GetById(int id, [FromQuery] string lang = Constantes.FR)
        {
            var entitiy = await _repository.GetById(id, lang);

            if(entitiy == null)
                return NotFound();

            return Ok(_mapper.Map(entitiy, lang));
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetFindByName(string name, [FromQuery] string lang = Constantes.FR)
        {
            var entities = await _repository.FindByNameAsync(name, lang);

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.Map(p, lang)).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("FindByNumber/{number}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetFindByNumber(string number, [FromQuery] string lang = Constantes.FR)
        {
            var entities = await _repository.Find(m => m.Number.Equals(number));

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.Map(p, lang)).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetEvol/{family}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetEvol(string family, [FromQuery] string lang = Constantes.FR)
        {
            var entities = await _repository.GetFamilyWithoutVariantAsync(family, lang);

            var result = (entities ?? Enumerable.Empty<Pokemon>())
                .Select(p => _mapper.Map(p, lang))
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetFamilyOrVariants")]
        public async Task<ActionResult<IEnumerable<FamilyDto>>> GetFamilyOrVariants(int evolutionChainId, string displayName, [FromQuery] string lang = Constantes.FR)
        {
            var entities = await _repository.GetFamilyAsync(evolutionChainId, displayName, lang);

            var result = (entities ?? Enumerable.Empty<Pokemon>())
                .Select(p => _mapper.MapFamily(p, lang))
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetVariant/{number}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetVariant(string number, [FromQuery] string lang = Constantes.FR)
        {
            var entities = await _repository.GetAllVariantAsync(number, lang);

            var result = (entities ?? Enumerable.Empty<Pokemon>())
                .Select(p => _mapper.MapFamily(p, lang))
                .ToList();

            return Ok(result);
        }
        #endregion
    }
}
