using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypePokController : GenericController<TypePok, TypePokDto, TypePokRepository>
    {
        #region Constructors
        public TypePokController(ILogger<TypePok> logger, GenericMapper<TypePok, TypePokDto> mapper, TypePokRepository repository) : base(logger, mapper, repository)
        {
        }
        #endregion
    }
}
