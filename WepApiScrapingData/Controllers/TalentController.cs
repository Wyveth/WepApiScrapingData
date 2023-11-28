using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TalentController : ControllerBase
    {
        #region Fields
        private readonly Repository<Talent> _repository;
        private readonly Repository<Pokemon> _repositoryPokemon;
        #endregion

        #region Constructors
        public TalentController(Repository<Talent> repository, Repository<Pokemon> repositoryPokemon)
        {
            _repository = repository;
            _repositoryPokemon = repositoryPokemon;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IEnumerable<Talent>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Talent> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("Find")]
        public async Task<IEnumerable<Talent>> GetFind(Expression<Func<Talent, bool>> predicate)
        {
            return await _repository.Find(predicate);
        }
        #endregion
    }
}
