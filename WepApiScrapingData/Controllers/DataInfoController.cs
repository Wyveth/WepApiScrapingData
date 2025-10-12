using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataInfoController : GenericController<DataInfo, DataInfoDto, DataInfoRepository>
    {
        public DataInfoController(ILogger<DataInfo> logger, GenericMapper<DataInfo, DataInfoDto> mapper, DataInfoRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
    }
}
