using HtmlAgilityPack;
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
    public class PokemonController : ControllerBase
    {
        #region Fields
        private readonly IRepositoryExtendsPokemon<Pokemon> _repository;
        private readonly IRepository<TypePok> _repositoryTP;
        private readonly IRepository<Talent> _repositoryTL;
        private readonly IRepository<Attaque> _repositoryAT;
        private readonly IRepository<TypeAttaque> _repositoryTA;
        private readonly IRepository<Game> _repositoryG;
        private readonly IRepository<Pokemon_TypePok> _repositoryPTP;
        private readonly IRepository<Pokemon_Weakness> _repositoryPWN;
        private readonly IRepository<Pokemon_Talent> _repositoryPTL;
        private readonly IRepository<Pokemon_Attaque> _repositoryPAT;
        #endregion

        #region Constructors
        public PokemonController(IRepositoryExtendsPokemon<Pokemon> repository, IRepository<TypePok> repositoryTP, IRepository<Talent> repositoryTL, IRepository<Attaque> repositoryAT, IRepository<TypeAttaque> repositoryTA, IRepository<Game> repositoryG, IRepository<Pokemon_TypePok> repositoryPTP, IRepository<Pokemon_Weakness> repositoryPWN, IRepository<Pokemon_Talent> repositoryPTL, IRepository<Pokemon_Attaque> repositoryPAT)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryTL = repositoryTL;
            _repositoryAT = repositoryAT;
            _repositoryTA = repositoryTA;
            _repositoryG = repositoryG;
            _repositoryPTP = repositoryPTP;
            _repositoryPWN = repositoryPWN;
            _repositoryPTL = repositoryPTL;
            _repositoryPAT = repositoryPAT;
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
        [Route("ScrapingDataPokeBip")]
        public void ScrapingDataPokeBip()
        {
            List<PokemonPokeBipJson> dataJsons = new List<PokemonPokeBipJson>();

            Debug.WriteLine("Start Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.RecursiveGetDataPBJsonWithUrl(Constantes.urlStartFR, dataJsons);
            Debug.WriteLine("End Scraping - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(dataJsons);
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

        [HttpGet]
        [Route("GenerateJsonXamarinV1")]
        public async Task GenerateJsonXamarinV1()
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();

            List<PokemonMobileJsonV1> pokemonsJson = new List<PokemonMobileJsonV1>();

            foreach (Pokemon item in pokemons.ToList())
            {
                PokemonMobileJsonV1 pokemonJson = new PokemonMobileJsonV1();
                pokemonJson.Number = item.Number;
                if (item.Number.Length == 4 && item.Number.StartsWith("0"))
                    pokemonJson.Number = item.Number.Substring(1, 3);
                pokemonJson.Name = item.FR.Name;
                pokemonJson.DisplayName = item.FR.DisplayName;
                pokemonJson.NameEN = item.EN.Name.Replace(" ", "_");
                pokemonJson.DescriptionVx = item.FR.DescriptionVx;
                pokemonJson.DescriptionVy = item.FR.DescriptionVy;
                pokemonJson.UrlImg = item.UrlImg;
                pokemonJson.UrlSprite = item.UrlSprite;
                pokemonJson.UrlSound = item.UrlSound;
                pokemonJson.Size = item.FR.Size;
                pokemonJson.Category = item.FR.Category;
                pokemonJson.Weight = item.FR.Weight;
                pokemonJson.Talent = item.FR.Talent;
                pokemonJson.DescriptionTalent = item.FR.DescriptionTalent;
                pokemonJson.Types = item.FR.Types;
                pokemonJson.Weakness = item.FR.Weakness;
                pokemonJson.Evolutions = item.FR.Evolutions;
                pokemonJson.TypeEvolution = item.TypeEvolution;
                pokemonJson.WhenEvolution = item.FR.WhenEvolution;
                pokemonJson.statPv = item.StatPv;
                pokemonJson.statAttaque = item.StatAttaque;
                pokemonJson.statDefense = item.StatDefense;
                pokemonJson.statAttaqueSpe = item.StatAttaqueSpe;
                pokemonJson.statDefenseSpe = item.StatDefenseSpe;
                pokemonJson.statVitesse = item.StatVitesse;
                pokemonJson.statTotal = item.StatTotal;
                pokemonJson.Generation = item.Generation;
                pokemonJson.NextUrl = item.FR.NextUrl;

                pokemonsJson.Add(pokemonJson);

                Debug.WriteLine("Pokemon : " + item.Number + " - " + item.FR.Name);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(pokemonsJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("GenerateJsonXamarinV2")]
        public async Task GenerateJsonXamarinV2()
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();

            List<PokemonMobileJsonV2> pokemonsJson = new List<PokemonMobileJsonV2>();

            foreach (Pokemon item in pokemons.ToList())
            {
                PokemonMobileJsonV2 pokemonJson = new PokemonMobileJsonV2();

                pokemonJson.Number = item.Number;
                if (item.Number.Length == 4 && item.Number.StartsWith("0"))
                    pokemonJson.Number = item.Number.Substring(1, 3);

                pokemonJson.FR.Name = item.FR.Name;
                pokemonJson.FR.DisplayName = item.FR.DisplayName;
                pokemonJson.FR.DescriptionVx = item.FR.DescriptionVx;
                pokemonJson.FR.DescriptionVy = item.FR.DescriptionVy;
                pokemonJson.FR.Size = item.FR.Size;
                pokemonJson.FR.Category = item.FR.Category;
                pokemonJson.FR.Weight = item.FR.Weight;
                pokemonJson.FR.Talent = item.FR.Talent;
                pokemonJson.FR.DescriptionTalent = item.FR.DescriptionTalent;
                pokemonJson.FR.Types = item.FR.Types;
                pokemonJson.FR.Weakness = item.FR.Weakness;
                pokemonJson.FR.Evolutions = item.FR.Evolutions;
                pokemonJson.FR.WhenEvolution = item.FR.WhenEvolution;
                pokemonJson.FR.NextUrl = item.FR.NextUrl;

                pokemonJson.EN.Name = item.EN.Name;
                pokemonJson.EN.DisplayName = item.EN.DisplayName;
                pokemonJson.EN.DescriptionVx = item.EN.DescriptionVx;
                pokemonJson.EN.DescriptionVy = item.EN.DescriptionVy;
                pokemonJson.EN.Size = item.EN.Size;
                pokemonJson.EN.Category = item.EN.Category;
                pokemonJson.EN.Weight = item.EN.Weight;
                pokemonJson.EN.Talent = item.EN.Talent;
                pokemonJson.EN.DescriptionTalent = item.EN.DescriptionTalent;
                pokemonJson.EN.Types = item.EN.Types;
                pokemonJson.EN.Weakness = item.EN.Weakness;
                pokemonJson.EN.Evolutions = item.EN.Evolutions;
                pokemonJson.EN.WhenEvolution = item.EN.WhenEvolution;
                pokemonJson.EN.NextUrl = item.EN.NextUrl;

                pokemonJson.ES.Name = item.ES.Name;
                pokemonJson.ES.DisplayName = item.ES.DisplayName;
                pokemonJson.ES.DescriptionVx = item.ES.DescriptionVx;
                pokemonJson.ES.DescriptionVy = item.ES.DescriptionVy;
                pokemonJson.ES.Size = item.ES.Size;
                pokemonJson.ES.Category = item.ES.Category;
                pokemonJson.ES.Weight = item.ES.Weight;
                pokemonJson.ES.Talent = item.ES.Talent;
                pokemonJson.ES.DescriptionTalent = item.ES.DescriptionTalent;
                pokemonJson.ES.Types = item.ES.Types;
                pokemonJson.ES.Weakness = item.ES.Weakness;
                pokemonJson.ES.Evolutions = item.ES.Evolutions;
                pokemonJson.ES.WhenEvolution = item.ES.WhenEvolution;
                pokemonJson.ES.NextUrl = item.ES.NextUrl;

                pokemonJson.IT.Name = item.IT.Name;
                pokemonJson.IT.DisplayName = item.IT.DisplayName;
                pokemonJson.IT.DescriptionVx = item.IT.DescriptionVx;
                pokemonJson.IT.DescriptionVy = item.IT.DescriptionVy;
                pokemonJson.IT.Size = item.IT.Size;
                pokemonJson.IT.Category = item.IT.Category;
                pokemonJson.IT.Weight = item.IT.Weight;
                pokemonJson.IT.Talent = item.IT.Talent;
                pokemonJson.IT.DescriptionTalent = item.IT.DescriptionTalent;
                pokemonJson.IT.Types = item.IT.Types;
                pokemonJson.IT.Weakness = item.IT.Weakness;
                pokemonJson.IT.Evolutions = item.IT.Evolutions;
                pokemonJson.IT.WhenEvolution = item.IT.WhenEvolution;
                pokemonJson.IT.NextUrl = item.IT.NextUrl;

                pokemonJson.DE.Name = item.DE.Name;
                pokemonJson.DE.DisplayName = item.DE.DisplayName;
                pokemonJson.DE.DescriptionVx = item.DE.DescriptionVx;
                pokemonJson.DE.DescriptionVy = item.DE.DescriptionVy;
                pokemonJson.DE.Size = item.DE.Size;
                pokemonJson.DE.Category = item.DE.Category;
                pokemonJson.DE.Weight = item.DE.Weight;
                pokemonJson.DE.Talent = item.DE.Talent;
                pokemonJson.DE.DescriptionTalent = item.DE.DescriptionTalent;
                pokemonJson.DE.Types = item.DE.Types;
                pokemonJson.DE.Weakness = item.DE.Weakness;
                pokemonJson.DE.Evolutions = item.DE.Evolutions;
                pokemonJson.DE.WhenEvolution = item.DE.WhenEvolution;
                pokemonJson.DE.NextUrl = item.DE.NextUrl;

                pokemonJson.TypeEvolution = item.TypeEvolution;
                pokemonJson.StatPv = item.StatPv;
                pokemonJson.StatAttaque = item.StatAttaque;
                pokemonJson.StatDefense = item.StatDefense;
                pokemonJson.StatAttaqueSpe = item.StatAttaqueSpe;
                pokemonJson.StatDefenseSpe = item.StatDefenseSpe;
                pokemonJson.StatVitesse = item.StatVitesse;
                pokemonJson.StatTotal = item.StatTotal;
                pokemonJson.Generation = item.Generation;
                pokemonJson.UrlImg = item.UrlImg;
                pokemonJson.UrlSprite = item.UrlSprite;
                pokemonJson.UrlSound = item.UrlSound;

                pokemonsJson.Add(pokemonJson);

                Debug.WriteLine("Pokemon : " + item.Number + " - " + item.FR.Name);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobileV2(pokemonsJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }


        [HttpGet]
        [Route("ExportDb")]
        public Task ExportDb()
        {
            List<Pokemon> pokemons = _repository.GetAll().Result.ToList();

            List<TypeAttaque> typeAttaques = _repositoryTA.GetAll().Result.ToList();
            List<Attaque> attaques = _repositoryAT.GetAll().Result.ToList();
            List<TypePok> typePoks = _repositoryTP.GetAll().Result.ToList();
            List<Talent> talents = _repositoryTL.GetAll().Result.ToList();
            List<Game> games = _repositoryG.GetAll().Result.ToList();

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(pokemons, typePoks, talents, attaques, typeAttaques, games);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            return Task.CompletedTask;
        }

        [HttpGet]
        [Route("ImportDb")]
        public Task ImportDb()
        {
            string json;
            using (StreamReader sr = new StreamReader("DbToJson.json"))
            {
                json = sr.ReadToEnd();
                _repository.ImportJsonToDb(json);
            }

            return Task.CompletedTask;
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
        [Route("SaveInfoPokemonAttackInDB")]
        public void SaveInfoPokemonAttackInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap/PokeBipScrapGen.json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveInfoPokemonAttackInDB(json);
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
            ScrapingDataUtils.GetUrlSound(response, _repository);
        }

        [HttpPut]
        [Route("UpdateSoundGen9")]
        public async Task UpdateSoundGen9()
        {
            string response = HttpClientUtils.CallUrl(Constantes.urlAllSpritesOld).Result;
            ScrapingDataUtils.GetUrlSoundGen9(response, _repository);
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

        [HttpGet]
        [Route("UpdateGlobale")]
        public void UpdateGlobale()
        {
            List<Talent> talents = this._repositoryTL.GetAll().Result.ToList();
            foreach (var item in talents)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryTL.EditRange(talents);
            _repositoryTL.UnitOfWork.SaveChanges();

            List<Attaque> attaques = this._repositoryAT.GetAll().Result.ToList();
            foreach (var item in attaques)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryAT.EditRange(attaques);
            _repositoryAT.UnitOfWork.SaveChanges();

            List<Pokemon_Attaque> pokemon_Attaques = this._repositoryPAT.GetAll().Result.ToList();
            foreach (var item in pokemon_Attaques)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryPAT.EditRange(pokemon_Attaques);
            _repositoryPAT.UnitOfWork.SaveChanges();

            List<Pokemon_Talent> pokemon_Talents = this._repositoryPTL.GetAll().Result.ToList();
            foreach (var item in pokemon_Talents)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryPTL.EditRange(pokemon_Talents);
            _repositoryPTL.UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
