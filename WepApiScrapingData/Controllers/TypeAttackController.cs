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
    public class TypeAttackController : GenericController<TypeAttack, TypeAttackDto, TypeAttackRepository>
    {
        #region Constructors
        public TypeAttackController(ILogger<TypeAttack> logger, GenericMapper<TypeAttack, TypeAttackDto> mapper, TypeAttackRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<TypeAttack>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
        }
        #endregion
    }
}
