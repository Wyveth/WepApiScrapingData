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
        private readonly IRepositoryExtendsPokemon<Pokemon> _repositoryP;
        #endregion

        #region Constructors
        public AttaqueController(IRepository<Attaque> repository, IRepositoryExtendsPokemon<Pokemon> repositoryP)
        {
            _repository = repository;
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

        [HttpGet]
        [Route("GenerateJsonXamarinV1")]
        public async Task GenerateJsonXamarinV1()
        {
            IEnumerable<Attaque> attaques = await _repository.GetAll();

            List<AttaqueMobileJsonV1> attaquesJson = new List<AttaqueMobileJsonV1>();

            foreach (Attaque item in attaques.ToList())
            {
                AttaqueMobileJsonV1 attaqueJson = new AttaqueMobileJsonV1();
                attaqueJson.Name = item.Name_FR;
                attaqueJson.Description = item.Description_FR;
                attaqueJson.Power = item.Power;
                attaqueJson.Precision = item.Precision;
                attaqueJson.PP = item.PP;
                attaqueJson.TypeAttaque = item.typeAttaque?.Name_FR;
                attaqueJson.TypePok = item.typePok?.Name_FR;

                attaquesJson.Add(attaqueJson);

                Debug.WriteLine("Attaque : " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(attaquesJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}
