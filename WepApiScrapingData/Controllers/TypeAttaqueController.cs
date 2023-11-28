using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{

    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypeAttaqueController : ControllerBase
    {
        #region Fields
        private readonly Repository<TypeAttaque> _repository;
        private readonly Repository<TypePok> _repositoryTP;
        private readonly Repository<Attaque> _repositoryA;
        #endregion

        #region Constructors
        public TypeAttaqueController(Repository<TypeAttaque> repository, Repository<TypePok> repositoryTP, Repository<Attaque> repositoryA)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryA = repositoryA;
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<TypeAttaque>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<TypeAttaque> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<TypeAttaque>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
        }
    }
}
