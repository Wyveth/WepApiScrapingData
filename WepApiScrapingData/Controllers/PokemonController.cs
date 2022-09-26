using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        #region Fields
        private readonly IRepository<Pokemon> _repository;
        #endregion

        #region Constructors
        public PokemonController(IRepository<Pokemon> repository)
        {
            _repository = repository;
        }
        #endregion
        
        #region Public Methods
        [HttpGet]
        [Route("ScrapingAll")]
        public void ScrapingAll()
        {
            List<PokemonJson> dataJsons = new List<PokemonJson>();

            #region Europe
            string url_FR = Constantes.urlStartFR;
            string url_EN = Constantes.urlStartEN;
            string url_ES = Constantes.urlStartES;
            string url_IT = Constantes.urlStartIT;
            string url_DE = Constantes.urlStartDE;
            string url_RU = Constantes.urlStartRU;
            #endregion

            #region Asia
            string url_JP = Constantes.urlStartJP;
            string url_CO = Constantes.urlStartCO;
            string url_CN = Constantes.urlStartCN;
            #endregion

            Debug.WriteLine("Start Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.RecursiveGetDataJsonWithUrl(url_FR, url_EN, url_ES, url_IT, url_DE, url_RU, url_JP, url_CO, url_CN, dataJsons);
            Debug.WriteLine("End Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(dataJsons);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Pokemon> GetAllinDB()
        {
            return _repository.GetAll().Where(x => x.Id < 20);
        }

        [HttpGet]
        [Route("GetSingle")]
        public Pokemon GetSingleInDB(int id)
        {
            return _repository.Get(id);
        }

        [HttpGet]
        [Route("Find")]
        public IEnumerable<Pokemon> GetFindInDB(Expression<Func<Pokemon, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        [HttpPost]
        [Route("SaveInDB")]
        public void SaveInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap.json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
