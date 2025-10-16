using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Body;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Query;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Mapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            [FromQuery] string lang = "FR")
        {
            var entities = await _repository.GetAllLight(gen, desc, max, lang);

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.MapLight(p, lang)).ToList();

            return Ok(result ?? []);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetFindByName(string name, [FromQuery] string lang = "fr")
        {
            var entities = await _repository.FindByNameAsync(name, lang);

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.Map(p, lang)).ToList();

            return Ok(result ?? []);
        }

        [HttpGet]
        [Route("FindByNumber/{number}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetFindByNumber(string number, [FromQuery] string lang = "fr")
        {
            var entities = await _repository.Find(m => m.Number.Equals(number));

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.Map(p, lang)).ToList();

            return Ok(result ?? []);
        }

        [HttpGet]
        [Route("GetEvol/{family}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetEvol(string family, [FromQuery] string lang = "fr")
        {
            var entities = await _repository.GetFamilyWithoutVariantAsync(family, lang);

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.Map(p, lang)).ToList();

            return Ok(result ?? []);
        }

        [HttpGet]
        [Route("GetVariant/{number}")]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetVariant(string number, [FromQuery] string lang = "fr")
        {
            var entities = await _repository.GetAllVariantAsync(number, lang);

            if (entities == null || !entities.Any())
                return NotFound();

            var result = entities.Select(p => _mapper.Map(p, lang)).ToList();

            return Ok(result ?? []);
        }
        #endregion
    }
}
