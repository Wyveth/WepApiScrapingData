using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypePokController : ControllerBase
    {
        #region Fields
        private readonly TypePokRepository _repository;
        #endregion

        #region Constructors
        public TypePokController(TypePokRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IEnumerable<TypePok>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<TypePok> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("Find")]
        public async Task<IEnumerable<TypePok>> GetFind(Expression<Func<TypePok, bool>> predicate)
        {
            return await _repository.Find(predicate);
        }
        #endregion
    }
}
