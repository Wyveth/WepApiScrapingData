using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class MigrationController : ControllerBase
    {
        #region Private fields
        private readonly ILogger<MigrationController> _logger;
        private readonly ScrapingContext _context;
        #endregion

        public MigrationController(ILogger<MigrationController> logger, ScrapingContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Migration-Add-Guid")]
        public virtual IActionResult Update()
        {
            IActionResult result = this.BadRequest();

            try
            {
                foreach (Pokemon item in _context.Pokemons)
                    item.Guid = Guid.NewGuid();

                foreach (DataInfo item in _context.DataInfos)
                    item.Guid = Guid.NewGuid();
                
                foreach (TypePok item in _context.TypesPok)
                    item.Guid = Guid.NewGuid();

                foreach (Talent item in _context.Talents)
                    item.Guid = Guid.NewGuid();

                foreach (Attaque item in _context.Attaques)
                    item.Guid = Guid.NewGuid();

                foreach (TypeAttaque item in _context.TypeAttaques)
                    item.Guid = Guid.NewGuid();
                
                foreach (Pokemon_TypePok item in _context.Pokemon_TypePok)
                    item.Guid = Guid.NewGuid();

                foreach (Pokemon_Weakness item in _context.Pokemon_Weakness)
                    item.Guid = Guid.NewGuid();

                foreach (Pokemon_Talent item in _context.Pokemon_Talent)
                    item.Guid = Guid.NewGuid();

                foreach (Pokemon_Attaque item in _context.Pokemon_Attaque)
                    item.Guid = Guid.NewGuid();

                foreach (Game item in _context.Games)
                    item.Guid = Guid.NewGuid();

                var success = _context.SaveChanges();

                if (success > 0)
                {
                    result = this.Ok();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
