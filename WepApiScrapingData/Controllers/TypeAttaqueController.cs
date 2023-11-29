using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{

    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypeAttaqueController : GenericController<TypeAttaque, TypeAttaqueDto, TypeAttaqueRepository>
    {
        #region Constructors
        public TypeAttaqueController(ILogger<TypeAttaque> logger, GenericMapper<TypeAttaque, TypeAttaqueDto> mapper, TypeAttaqueRepository repository) : base(logger, mapper, repository)
        {
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<TypeAttaque>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
        }
        #endregion
    }
}
