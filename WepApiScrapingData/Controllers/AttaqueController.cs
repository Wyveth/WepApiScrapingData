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
    public class AttaqueController : GenericController<Attaque, AttaqueDto, AttaqueRepository>
    {
        #region Constructors
        public AttaqueController(ILogger<Attaque> logger, GenericMapper<Attaque, AttaqueDto> mapper, AttaqueRepository repository) : base(logger, mapper, repository)
        {
        }
        #endregion

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<Attaque>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
        }
    }
}
