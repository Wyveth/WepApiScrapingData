using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Diagnostics;
using WebApiScrapingData.Core.Repositories;
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
        private readonly IRepository<TypePok> _repositoryTP;
        private readonly IRepository<Pokemon_TypePok> _repositoryPTP;
        private readonly IRepository<Pokemon_Weakness> _repositoryWN;
        #endregion

        #region Constructors
        public PokemonController(IRepository<Pokemon> repository, IRepository<TypePok> repositoryTP, IRepository<Pokemon_TypePok> repositoryPTP, IRepository<Pokemon_Weakness> repositoryWN)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryPTP = repositoryPTP;
            _repositoryWN = repositoryWN;
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
        [Route("Scraping/{FR}/{EN}/{ES}/{IT}/{DE}/{RU}/{JP}/{CO}/{CN}")]
        public void Scraping(string FR, string EN, string ES, string IT, string DE, string RU, string CO, string CN, string JP)
        {
            List<PokemonJson> dataJsons = new List<PokemonJson>();

            #region Europe
            string url_FR = Constantes.urlPathFR + FR;
            string url_EN = Constantes.urlPathEN + EN;
            string url_ES = Constantes.urlPathES + ES;
            string url_IT = Constantes.urlPathIT + IT;
            string url_DE = Constantes.urlPathDE + DE;
            string url_RU = Constantes.urlPathRU + RU;
            #endregion

            #region Asia
            string url_CO = Constantes.urlStartCO + CO;
            string url_CN = Constantes.urlStartCN + CN;
            string url_JP = Constantes.urlStartJP + JP;
            #endregion

            Debug.WriteLine("Start Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.RecursiveGetDataJsonWithUrl(url_FR, url_EN, url_ES, url_IT, url_DE, url_RU, url_JP, url_CO, url_CN, dataJsons, true);
            Debug.WriteLine("End Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(dataJsons, true);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("GetAll/{limit}/{max}")]
        public IEnumerable<Pokemon> GetAllinDB(int max = 20, bool limit = true)
        {
            if (limit)
            {
                return _repository.GetAll().Take(max);
            }
            else
            {
                return _repository.GetAll();
            }
        }

        [HttpGet]
        [Route("GetSingle/{id}")]
        public Pokemon GetSingleInDB(int id)
        {
            return _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public IEnumerable<Pokemon> GetFindInDB(string name)
        {
            return _repository.Find(m => m.FR.Name.Equals(name));
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

        [HttpPut]
        [Route("UpdateTypePokInDB")]
        public void UpdateTypePokInDB()
        {
            foreach (Pokemon pokemon in _repository.GetAll().ToList())
            {
                List<Pokemon_TypePok> Pokemon_TypePoks = new();

                foreach (string type in pokemon.FR.Types.Split(','))
                {
                    Pokemon_TypePok pokemon_TypePok = new()
                    {
                        PokemonId = pokemon.Id,
                        TypePokId = _repositoryTP.GetAll().Where(x => x.Name_FR.Equals(type)).FirstOrDefault().Id
                    };

                    Pokemon_TypePoks.Add(pokemon_TypePok);
                }

                _repositoryPTP.AddRange(Pokemon_TypePoks);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateWeaknessInDB")]
        public void UpdateWeaknessInDB()
        {
            try
            {
                foreach (Pokemon pokemon in _repository.GetAll().ToList())
                {
                    List<Pokemon_Weakness> Pokemon_Weaknesses = new();

                    foreach (string weakness in pokemon.FR.Weakness.Split(','))
                    {
                        Pokemon_Weakness pokemon_Weakness = new()
                        {
                            PokemonId = pokemon.Id,
                            TypePokId = _repositoryTP.GetAll().Where(x => x.Name_FR.Equals(weakness)).FirstOrDefault().Id
                        };

                        Pokemon_Weaknesses.Add(pokemon_Weakness);
                    }

                    _repositoryWN.AddRange(Pokemon_Weaknesses);
                }

                _repository.UnitOfWork.SaveChanges();
            }
            catch(Exception e)
            {
               Console.WriteLine(e.InnerException.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateDataByteWithUrl")]
        public async Task UpdateDataByteWithUrl()
        {
            var httpClient = new HttpClient();

            foreach (Pokemon pokemon in _repository.GetAll().ToList())
            {
                pokemon.DataImg = await httpClient.DownloadImageAsync(pokemon.UrlImg);
                pokemon.DataSprite = await httpClient.DownloadImageAsync(pokemon.UrlSprite);

                _repository.Edit(pokemon);
            }

            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpGet]
        [Route("TestUrlStaticOrDynamic/{language}")]
        public async void TestUrlStaticOrDynamic(string language)
        {
            string response = "";
            HtmlDocument htmlDoc = new HtmlDocument();
            string value = "";
            
            switch (language)
            {
                case Constantes.RU:
                    response = HttpClientUtils.CallUrl(Constantes.urlStartRU).Result;
                    htmlDoc.LoadHtml(response);
                    value = htmlDoc.DocumentNode.Descendants("b").First().InnerText;
                    //if (value.Equals("Бульбазавр"))
                    //    return true;
                    break;
                case Constantes.CO:
                    response = HttpClientUtils.CallUrl(Constantes.urlStartCO).Result;
                    htmlDoc.LoadHtml(response);
                    value = htmlDoc.DocumentNode.Descendants("h3").First().InnerText;
                    //if (value.Contains("이상해씨"))
                    //    return true;
                    break;
                case Constantes.CN:
                    response = HttpClientUtils.CallUrl(Constantes.urlStartCN).Result;
                    htmlDoc.LoadHtml(response);
                    value = htmlDoc.DocumentNode.Descendants("p").First(node => node.GetAttributeValue("class", "").Contains("pokemon-slider__main-name")).InnerText;
                    //if (value.Equals("妙蛙种子"))
                    //    return true;
                    break;
                case Constantes.JP:
                    //try
                    //{
                    //    await new BrowserFetcher(Product.Chrome).DownloadAsync();
                    //    var lol = new LaunchOptions
                    //    {
                    //        Headless = true,
                    //        Product = Product.Chrome,
                    //        ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
                    //    };
                    //    var browser = await Puppeteer.LaunchAsync(lol);
                    //    var page = await browser.NewPageAsync();
                    //    await page.GoToAsync("http://www.google.com");
                    //    await page.ScreenshotAsync("./google.png");

                    //    var webpage = await HttpClientUtils.CallUrlDynamicTest(Constantes.urlStartJP);
                    //    //List<HtmlNode> values = htmlNode.Descendants("img").ToList();
                    //    //if (value.Contains("フシギダネ"))
                    //    //    return true;
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e.ToString());
                    //}
                    break;
                default:
                    break;
            }

            //return true;
        }
        #endregion
    }
}
