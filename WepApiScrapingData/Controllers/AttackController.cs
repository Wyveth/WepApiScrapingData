using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    public class AttackController : GenericController<Attack, AttackDto, AttackRepository>
    {
        #region Constructors
        public AttackController(ILogger<Attack> logger, GenericMapper<Attack, AttackDto> mapper, AttackRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<Attack> GetFindByName(string name)
        {
            return await _repository.GetByName(name);
        }
        #endregion
    }
}
