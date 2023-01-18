using HtmlAgilityPack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
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
    public class PokemonController : ControllerBase
    {
        #region Fields
        private readonly IRepositoryExtendsPokemon<Pokemon> _repository;
        private readonly IRepository<TypePok> _repositoryTP;
        private readonly IRepository<Talent> _repositoryTL;
        private readonly IRepository<Pokemon_TypePok> _repositoryPTP;
        private readonly IRepository<Pokemon_Weakness> _repositoryPWN;
        private readonly IRepository<Pokemon_Talent> _repositoryPTL;
        #endregion

        #region Constructors
        public PokemonController(IRepositoryExtendsPokemon<Pokemon> repository, IRepository<TypePok> repositoryTP, IRepository<Talent> repositoryTL, IRepository<Pokemon_TypePok> repositoryPTP, IRepository<Pokemon_Weakness> repositoryPWN, IRepository<Pokemon_Talent> repositoryPTL)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryTL = repositoryTL;
            _repositoryPTP = repositoryPTP;
            _repositoryPWN = repositoryPWN;
            _repositoryPTL = repositoryPTL;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("ScrapingAll")]
        public void ScrapingAll()
        {
            List<PokemonJson> dataJsons = new List<PokemonJson>();

            Debug.WriteLine("Start Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.RecursiveGetDataJsonWithUrl(Constantes.urlStartFR,
                Constantes.urlStartEN,
                Constantes.urlStartES,
                Constantes.urlStartIT,
                Constantes.urlStartDE,
                Constantes.urlStartRU,
                Constantes.urlStartCO,
                Constantes.urlStartCN,
                Constantes.urlStartJP,
                dataJsons);
            Debug.WriteLine("End Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(dataJsons);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("ScrapingAllByGen/{gen}")]
        public async Task ScrapingAllByGen(int gen = 0)
        {
            List<PokemonJson> dataJsons = new List<PokemonJson>();

            #region Url Européen
            string urlStartFR = string.Empty;
            string urlStartEN = string.Empty;
            string urlStartES = string.Empty;
            string urlStartIT = string.Empty;
            string urlStartDE = string.Empty;
            #endregion

            #region Url Asie
            string urlStartRU = string.Empty;
            string urlStartCO = string.Empty;
            string urlStartCN = string.Empty;
            string urlStartJP = string.Empty;
            #endregion

            switch (gen)
            {
                case 0:
                    urlStartFR = Constantes.urlStart0Gen_FR;
                    urlStartEN = Constantes.urlStart0Gen_EN;
                    urlStartES = Constantes.urlStart0Gen_ES;
                    urlStartIT = Constantes.urlStart0Gen_IT;
                    urlStartDE = Constantes.urlStart0Gen_DE;
                    urlStartRU = Constantes.urlStart0Gen_RU;
                    urlStartCO = Constantes.urlStart0Gen_CO;
                    urlStartCN = Constantes.urlStart0Gen_CN;
                    urlStartJP = Constantes.urlStart0Gen_JP;
                    break;
                case 1:
                    urlStartFR = Constantes.urlStart1Gen_FR;
                    urlStartEN = Constantes.urlStart1Gen_EN;
                    urlStartES = Constantes.urlStart1Gen_ES;
                    urlStartIT = Constantes.urlStart1Gen_IT;
                    urlStartDE = Constantes.urlStart1Gen_DE;
                    urlStartRU = Constantes.urlStart1Gen_RU;
                    urlStartCO = Constantes.urlStart1Gen_CO;
                    urlStartCN = Constantes.urlStart1Gen_CN;
                    urlStartJP = Constantes.urlStart1Gen_JP;
                    break;
                case 2:
                    urlStartFR = Constantes.urlStart2Gen_FR;
                    urlStartEN = Constantes.urlStart2Gen_EN;
                    urlStartES = Constantes.urlStart2Gen_ES;
                    urlStartIT = Constantes.urlStart2Gen_IT;
                    urlStartDE = Constantes.urlStart2Gen_DE;
                    urlStartRU = Constantes.urlStart2Gen_RU;
                    urlStartCO = Constantes.urlStart2Gen_CO;
                    urlStartCN = Constantes.urlStart2Gen_CN;
                    urlStartJP = Constantes.urlStart2Gen_JP;
                    break;
                case 3:
                    urlStartFR = Constantes.urlStart3Gen_FR;
                    urlStartEN = Constantes.urlStart3Gen_EN;
                    urlStartES = Constantes.urlStart3Gen_ES;
                    urlStartIT = Constantes.urlStart3Gen_IT;
                    urlStartDE = Constantes.urlStart3Gen_DE;
                    urlStartRU = Constantes.urlStart3Gen_RU;
                    urlStartCO = Constantes.urlStart3Gen_CO;
                    urlStartCN = Constantes.urlStart3Gen_CN;
                    urlStartJP = Constantes.urlStart3Gen_JP;
                    break;
                case 4:
                    urlStartFR = Constantes.urlStart4Gen_FR;
                    urlStartEN = Constantes.urlStart4Gen_EN;
                    urlStartES = Constantes.urlStart4Gen_ES;
                    urlStartIT = Constantes.urlStart4Gen_IT;
                    urlStartDE = Constantes.urlStart4Gen_DE;
                    urlStartRU = Constantes.urlStart4Gen_RU;
                    urlStartCO = Constantes.urlStart4Gen_CO;
                    urlStartCN = Constantes.urlStart4Gen_CN;
                    urlStartJP = Constantes.urlStart4Gen_JP;
                    break;
                case 5:
                    urlStartFR = Constantes.urlStart5Gen_FR;
                    urlStartEN = Constantes.urlStart5Gen_EN;
                    urlStartES = Constantes.urlStart5Gen_ES;
                    urlStartIT = Constantes.urlStart5Gen_IT;
                    urlStartDE = Constantes.urlStart5Gen_DE;
                    urlStartRU = Constantes.urlStart5Gen_RU;
                    urlStartCO = Constantes.urlStart5Gen_CO;
                    urlStartCN = Constantes.urlStart5Gen_CN;
                    urlStartJP = Constantes.urlStart5Gen_JP;
                    break;
                case 6:
                    urlStartFR = Constantes.urlStart6Gen_FR;
                    urlStartEN = Constantes.urlStart6Gen_EN;
                    urlStartES = Constantes.urlStart6Gen_ES;
                    urlStartIT = Constantes.urlStart6Gen_IT;
                    urlStartDE = Constantes.urlStart6Gen_DE;
                    urlStartRU = Constantes.urlStart6Gen_RU;
                    urlStartCO = Constantes.urlStart6Gen_CO;
                    urlStartCN = Constantes.urlStart6Gen_CN;
                    urlStartJP = Constantes.urlStart6Gen_JP;
                    break;
                case 7:
                    urlStartFR = Constantes.urlStart7Gen_FR;
                    urlStartEN = Constantes.urlStart7Gen_EN;
                    urlStartES = Constantes.urlStart7Gen_ES;
                    urlStartIT = Constantes.urlStart7Gen_IT;
                    urlStartDE = Constantes.urlStart7Gen_DE;
                    urlStartRU = Constantes.urlStart7Gen_RU;
                    urlStartCO = Constantes.urlStart7Gen_CO;
                    urlStartCN = Constantes.urlStart7Gen_CN;
                    urlStartJP = Constantes.urlStart7Gen_JP;
                    break;
                case 8:
                    urlStartFR = Constantes.urlStart8Gen_FR;
                    urlStartEN = Constantes.urlStart8Gen_EN;
                    urlStartES = Constantes.urlStart8Gen_ES;
                    urlStartIT = Constantes.urlStart8Gen_IT;
                    urlStartDE = Constantes.urlStart8Gen_DE;
                    urlStartRU = Constantes.urlStart8Gen_RU;
                    urlStartCO = Constantes.urlStart8Gen_CO;
                    urlStartCN = Constantes.urlStart8Gen_CN;
                    urlStartJP = Constantes.urlStart8Gen_JP;
                    break;
                case 9:
                    urlStartFR = Constantes.urlStart9Gen_FR;
                    urlStartEN = Constantes.urlStart9Gen_EN;
                    urlStartES = Constantes.urlStart9Gen_ES;
                    urlStartIT = Constantes.urlStart9Gen_IT;
                    urlStartDE = Constantes.urlStart9Gen_DE;
                    urlStartRU = Constantes.urlStart9Gen_RU;
                    urlStartCO = Constantes.urlStart9Gen_CO;
                    urlStartCN = Constantes.urlStart9Gen_CN;
                    urlStartJP = Constantes.urlStart9Gen_JP;
                    break;
            }

            Debug.WriteLine("Start Scraping Gen - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.RecursiveGetDataJsonWithUrl(urlStartFR, urlStartEN, urlStartES, urlStartIT, urlStartDE, urlStartRU, urlStartCO, urlStartCN, urlStartJP, dataJsons, gen: gen);
            Debug.WriteLine("End Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(dataJsons, gen: gen);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("Scraping/{FR}/{EN}/{ES}/{IT}/{DE}/{RU}/{CO}/{CN}/{JP}/{gen}")]
        public void Scraping(string FR, string EN, string ES, string IT, string DE, string RU, string CO, string CN, string JP, int gen)
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
            ScrapingDataUtils.RecursiveGetDataJsonWithUrl(url_FR, url_EN, url_ES, url_IT, url_DE, url_RU, url_CO, url_CN, url_JP, dataJsons, true, gen);
            Debug.WriteLine("End Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(dataJsons, true);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [EnableCors(SecurityMethods.DEFAULT_POLICY)]
        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{limit}/{max}")]
        public async Task<IEnumerable<Pokemon>> GetAllinDB(int max = 20, bool limit = true)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            if (limit)
                return pokemons.Take(max);
            else
                return pokemons;
        }

        [HttpGet]
        [Route("Light/{limit}/{max}")]
        public async Task<IEnumerable<Pokemon>> GetAllLight(int max = 20, bool limit = true)
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAllLight();
            if (limit)
                return pokemons.Take(max);
            else
                return pokemons;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Pokemon> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public IEnumerable<Pokemon> GetFindByName(string name)
        {
            return _repository.Find(m => m.FR.Name.Equals(name));
        }

        [HttpGet]
        [Route("GetEvol/{family}")]
        public async Task<IEnumerable<Pokemon>> GetEvol(string family)
        {
            return await _repository.GetFamilyWithoutVariantAsync(family);
        }

        [HttpGet]
        [Route("GetVariant/{number}")]
        public async Task<IEnumerable<Pokemon>> GetVariant(string number)
        {
            return await _repository.GetAllVariantAsync(number);
        }

        [HttpPost]
        [Route("SaveInDB")]
        public void SaveInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap/PokeScrap.json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("SaveGenInDB/{gen}")]
        public void SaveGenInDB(int gen)
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap/PokeScrapGen" + gen + ".json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateTypePokInDB")]
        public async Task UpdateTypePokInDB()
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.ToList())
            {
                List<Pokemon_TypePok> pokemon_TypePoks = new();

                foreach (string type in pokemon.FR.Types.Split(','))
                {
                    TypePok typePok = await _repositoryTP.SingleOrDefault(x => x.Name_FR.Equals(type));
                    Pokemon_TypePok pokemon_TypePok = new()
                    {
                        PokemonId = pokemon.Id,
                        TypePokId = typePok.Id
                    };

                    pokemon_TypePoks.Add(pokemon_TypePok);
                }

                _repositoryPTP.AddRange(pokemon_TypePoks);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateWeaknessInDB")]
        public async Task UpdateWeaknessInDB()
        {
            try
            {
                List<Pokemon> pokemons = _repository.GetAll().Result.ToList();
                foreach (Pokemon pokemon in pokemons)
                {
                    List<Pokemon_Weakness> pokemon_Weaknesses = new();

                    foreach (string weakness in pokemon.FR.Weakness.Split(','))
                    {
                        TypePok typePok = await _repositoryTP.SingleOrDefault(m => m.Name_FR.Equals(weakness));
                        Pokemon_Weakness pokemon_Weakness = new()
                        {
                            PokemonId = pokemon.Id,
                            TypePokId = typePok.Id
                        };

                        pokemon_Weaknesses.Add(pokemon_Weakness);
                    }

                    await _repositoryPWN.AddRange(pokemon_Weaknesses);
                }

                _repository.UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateTalentInDB")]
        public async Task UpdateTalentInDB()
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.ToList())
            {
                List<Pokemon_Talent> pokemon_Talents = new();

                foreach (string type in pokemon.FR.Talent.Split(','))
                {
                    Talent talentPok = await _repositoryTL.SingleOrDefault(x => x.Name_FR.Equals(type));
                    Pokemon_Talent pokemon_Talent = new()
                    {
                        PokemonId = pokemon.Id,
                        TalentId = talentPok.Id
                    };

                    pokemon_Talents.Add(pokemon_Talent);
                }

                _repositoryPTL.AddRange(pokemon_Talents);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("DlUpdatePathUrl")]
        public async Task DlUpdatePathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.Where(m => m.PathImg == null))
            {
                pokemon.PathImg = await HttpClientUtils.DownloadFileTaskAsync(httpClient, pokemon.UrlImg, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation);
                pokemon.PathSprite = await HttpClientUtils.DownloadFileTaskAsync(httpClient, pokemon.UrlSprite, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation, true);
            }

            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpPut]
        [Route("UpdateSprite")]
        public async Task UpdateSprite()
        {
            string response = HttpClientUtils.CallUrl(Constantes.urlAllSprites).Result;
            ScrapingDataUtils.GetUrlsMini(response, _repository);
        }

        [HttpPut]
        [Route("DlUpdatePathUrlSound")]
        public async Task DlUpdatePathUrlSound()
        {
            var httpClient = new HttpClient();
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons)
            {
                pokemon.PathSound = await HttpClientUtils.DownloadSoundFileTaskAsync(httpClient, pokemon.UrlSound, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation);
            }

            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpPut]
        [Route("UpdateSound")]
        public async Task UpdateSound()
        {
            string response = HttpClientUtils.CallUrl(Constantes.urlAllSprites).Result;
            ScrapingDataUtils.GetUrlsSound(response, _repository);
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
