using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class AttaqueController : ControllerBase
    {
        #region Fields
        private readonly IRepository<Attaque> _repository;
        private readonly IRepository<TypeAttaque> _repositoryTA;
        private readonly IRepositoryExtendsPokemon<Pokemon> _repositoryP;
        #endregion

        #region Constructors
        public AttaqueController(IRepository<Attaque> repository, IRepository<TypeAttaque> repositoryTA, IRepositoryExtendsPokemon<Pokemon> repositoryP)
        {
            _repository = repository;
            _repositoryTA = repositoryTA;
            _repositoryP = repositoryP;
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<Attaque>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Attaque> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public IEnumerable<Attaque> GetFindByName(string name)
        {
            return _repository.Find(m => m.Name_FR.Equals(name));
        }
    }
}
