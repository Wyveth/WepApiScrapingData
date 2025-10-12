using HtmlAgilityPack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class XamarinController : ControllerBase
    {
        #region Fields
        private readonly PokemonRepository _repository;
        private readonly AttaqueRepository _repositoryA;
        private readonly TalentRepository _repositoryTL;
        private readonly TypeAttaqueRepository _repositoryTA;
        private readonly TypePokRepository _repositoryTP;
        #endregion

        #region Constructors
        public XamarinController(PokemonRepository repository, AttaqueRepository repositoryA, TalentRepository repositoryTL, TypeAttaqueRepository repositoryTA, TypePokRepository repositoryTP)
        {
            _repository = repository;
            _repositoryA = repositoryA;
            _repositoryTL = repositoryTL;
            _repositoryTA = repositoryTA;
            _repositoryTP = repositoryTP;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("Generate/JsonPokemonV1")]
        public async Task PokemonV1()
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

                foreach (Pokemon_Talent pokemon_Talent in item.Pokemon_Talents)
                {
                    SkillJson skillJson = new();
                    skillJson.Name = pokemon_Talent.Talent.Name_FR;
                    skillJson.isHidden = pokemon_Talent.IsHidden;
                    pokemonJson.Talents.Add(skillJson);
                }

                foreach (Pokemon_TypePok pokemon_Type in item.Pokemon_TypePoks)
                {
                    TypePokJson typePokJson = new();
                    typePokJson.Name = pokemon_Type.TypePok.Name_FR;
                    pokemonJson.Types.Add(typePokJson);
                }

                foreach (Pokemon_Weakness pokemon_Weakness in item.Pokemon_Weaknesses)
                {
                    TypePokJson typePokJson = new();
                    typePokJson.Name = pokemon_Weakness.TypePok.Name_FR;
                    pokemonJson.Weakness.Add(typePokJson);
                }

                foreach (Pokemon_Attaque pokemon_Attaque in item.Pokemon_Attaques)
                {
                    AttackJson attackJson = new();
                    attackJson.Name = pokemon_Attaque.Attaque.Name_FR;
                    attackJson.TypeLearn = pokemon_Attaque.TypeLearn;
                    attackJson.Level = pokemon_Attaque.Level;
                    attackJson.CTCS = pokemon_Attaque.CTCS;
                    pokemonJson.Attaques.Add(attackJson);
                }

                pokemonJson.Evolutions = item.FR.Evolutions;
                pokemonJson.TypeEvolution = item.TypeEvolution;
                pokemonJson.WhenEvolution = item.FR.WhenEvolution;
                pokemonJson.StatPv = item.StatPv;
                pokemonJson.StatAttaque = item.StatAttaque;
                pokemonJson.StatDefense = item.StatDefense;
                pokemonJson.StatAttaqueSpe = item.StatAttaqueSpe;
                pokemonJson.StatDefenseSpe = item.StatDefenseSpe;
                pokemonJson.StatVitesse = item.StatVitesse;
                pokemonJson.StatTotal = item.StatTotal;
                pokemonJson.Generation = item.Generation;
                pokemonJson.EggMoves = item.EggMoves;
                pokemonJson.CaptureRate = item.CaptureRate;
                pokemonJson.BasicHappiness = item.BasicHappiness;
                pokemonJson.Game = item.Game.Name_FR;
                pokemonJson.NextUrl = item.FR.NextUrl;

                pokemonsJson.Add(pokemonJson);

                Debug.WriteLine("Pokemon : " + item.Number + " - " + item.FR.Name);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(pokemonsJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("Generate/JsonPokemonV2")]
        public async Task PokemonV2()
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
        [Route("Generate/JsonAttaqueV1")]
        public async Task AttaqueV1()
        {
            IEnumerable<Attaque> attaques = await _repositoryA.GetAll();

            List<AttaqueMobileJsonV1> attaquesJson = new List<AttaqueMobileJsonV1>();

            foreach (Attaque item in attaques.ToList())
            {
                AttaqueMobileJsonV1 attaqueJson = new AttaqueMobileJsonV1();
                attaqueJson.Name = item.Name_FR;
                attaqueJson.Description = item.Description_FR;
                attaqueJson.Power = item.Power;
                attaqueJson.Precision = item.Precision;
                attaqueJson.PP = item.PP;
                attaqueJson.TypeAttaque = item.TypeAttaque?.Name_FR;
                attaqueJson.TypePok = item.TypePok?.Name_FR;

                attaquesJson.Add(attaqueJson);

                Debug.WriteLine("Attaque : " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(attaquesJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("Generate/JsonTalentV1")]
        public async Task TalentV1()
        {
            IEnumerable<Talent> talents = await _repositoryTL.GetAll();

            List<TalentMobileJsonV1> talentsJson = new List<TalentMobileJsonV1>();

            foreach (Talent item in talents.ToList())
            {
                TalentMobileJsonV1 talentJson = new TalentMobileJsonV1();
                talentJson.Name = item.Name_FR;
                talentJson.Description = item.Description_FR;

                talentsJson.Add(talentJson);

                Debug.WriteLine("Talent : " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(talentsJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("Generate/JsonTypeAttaqueV1")]
        public async Task GenerateJsonTypeAttaqueV1()
        {
            IEnumerable<TypeAttaque> typesAttaque = await _repositoryTA.GetAll();

            List<TypeAttaqueMobileJsonV1> typesAttaqueJson = new List<TypeAttaqueMobileJsonV1>();

            foreach (TypeAttaque item in typesAttaque.ToList())
            {
                TypeAttaqueMobileJsonV1 typeAttaqueJson = new TypeAttaqueMobileJsonV1();
                typeAttaqueJson.Name = item.Name_FR;
                typeAttaqueJson.Description = item.Description_FR;
                typeAttaqueJson.UrlImg = item.UrlImg;
                typeAttaqueJson.PathImg = item.PathImg;

                typesAttaqueJson.Add(typeAttaqueJson);

                Debug.WriteLine("Type Attaque : " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(typesAttaqueJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("Generate/JsonTypePokV1")]
        public async Task GenerateJsonTypePokV1()
        {
            IEnumerable<TypePok> typesPok = await _repositoryTP.GetAll();

            List<TypePokMobileJsonV1> typesPokJson = new List<TypePokMobileJsonV1>();

            foreach (TypePok item in typesPok.ToList())
            {
                TypePokMobileJsonV1 typePokJson = new TypePokMobileJsonV1();
                typePokJson.Name = item.Name_FR;
                typePokJson.NameEN = item.Name_EN;
                typePokJson.UrlMiniGo = item.UrlMiniGo;
                typePokJson.UrlFondGo = item.UrlFondGo;
                typePokJson.UrlMiniHome = item.UrlMiniHome;
                typePokJson.UrlIconHome = item.UrlIconHome;
                typePokJson.UrlAutoHome = item.UrlAutoHome;
                typePokJson.ImgColor = item.ImgColor;
                typePokJson.InfoColor = item.InfoColor;
                typePokJson.TypeColor = item.TypeColor;

                typesPokJson.Add(typePokJson);

                Debug.WriteLine("TypePok: " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(typesPokJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("Generate/JsonTypeAttaqueV2")]
        public async Task GenerateJsonTypeAttaqueV2()
        {
            IEnumerable<TypePok> typesPok = await _repositoryTP.GetAll();

            List<TypePokMobileJsonV2> typesPokJson = new List<TypePokMobileJsonV2>();

            foreach (TypePok item in typesPok.ToList())
            {
                TypePokMobileJsonV2 typePokJson = new TypePokMobileJsonV2();
                typePokJson.Name_FR = item.Name_FR;
                typePokJson.Name_EN = item.Name_EN;
                typePokJson.Name_ES = item.Name_ES;
                typePokJson.Name_IT = item.Name_IT;
                typePokJson.Name_DE = item.Name_DE;
                typePokJson.Name_RU = item.Name_RU;
                typePokJson.Name_CO = item.Name_CO;
                typePokJson.Name_CN = item.Name_CN;
                typePokJson.Name_JP = item.Name_JP;
                typePokJson.UrlMiniGo = item.UrlMiniGo;
                typePokJson.UrlFondGo = item.UrlFondGo;
                typePokJson.UrlMiniHome = item.UrlMiniHome;
                typePokJson.UrlIconHome = item.UrlIconHome;
                typePokJson.UrlAutoHome = item.UrlAutoHome;
                typePokJson.ImgColor = item.ImgColor;
                typePokJson.InfoColor = item.InfoColor;
                typePokJson.TypeColor = item.TypeColor;

                typesPokJson.Add(typePokJson);

                Debug.WriteLine("TypePok: " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(typesPokJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpGet]
        [Route("ScrapingAll")]
        public void ScrapingAll()
        {
            List<TypePokMobileJsonV2> typeJsons = new List<TypePokMobileJsonV2>();
            PopulateTypes(typeJsons);
            WriteToJsonType(typeJsons);
        }
        #endregion

        #region Json
        private void PopulateTypes(List<TypePokMobileJsonV2> typeJsons)
        {
            List<string> types = new List<string>();
            types.Add(Constantes.Steel_FR);
            types.Add(Constantes.Dragon_FR);
            types.Add(Constantes.Electric_FR);
            types.Add(Constantes.Fire_FR);
            types.Add(Constantes.Bug_FR);
            types.Add(Constantes.Grass_FR);
            types.Add(Constantes.Psychic_FR);
            types.Add(Constantes.Ground_FR);
            types.Add(Constantes.Dark_FR);
            types.Add(Constantes.Fighting_FR);
            types.Add(Constantes.Water_FR);
            types.Add(Constantes.Fairy_FR);
            types.Add(Constantes.Ice_FR);
            types.Add(Constantes.Normal_FR);
            types.Add(Constantes.Poison_FR);
            types.Add(Constantes.Rock_FR);
            types.Add(Constantes.Ghost_FR);
            types.Add(Constantes.Flying_FR);

            foreach (string type in types)
            {
                TypePokMobileJsonV2 typeJson = new TypePokMobileJsonV2();
                typeJson.Name_FR = type;
                typeJson.Name_EN = GetNameTypeByLanguage(type, Constantes.EN);
                typeJson.Name_ES = GetNameTypeByLanguage(type, Constantes.ES);
                typeJson.Name_IT = GetNameTypeByLanguage(type, Constantes.IT);
                typeJson.Name_DE = GetNameTypeByLanguage(type, Constantes.DE);
                typeJson.Name_RU = GetNameTypeByLanguage(type, Constantes.RU);
                typeJson.Name_JP = GetNameTypeByLanguage(type, Constantes.JP);
                typeJson.Name_CO = GetNameTypeByLanguage(type, Constantes.CO);
                typeJson.Name_CN = GetNameTypeByLanguage(type, Constantes.CN);
                typeJson.UrlMiniGo = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Miniature_Type_" + type + "_GO.png");
                typeJson.UrlFondGo = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Fond_Type_" + type + "_GO.png");
                typeJson.UrlMiniHome = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Miniature_Type_" + type + "_HOME.png");
                typeJson.UrlIconHome = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Icône_Type_" + type + "_HOME.png");
                typeJson.UrlAutoHome = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Autocollant_Type_" + type + "_HOME.png");
                GetColor(typeJson);
                typeJsons.Add(typeJson);
            }
        }

        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }
        
        private void GetColor(TypePokMobileJsonV2 typeJson)
        {
            switch (typeJson.Name_FR)
            {
                case Constantes.Steel_FR:
                    typeJson.ImgColor = Constantes.ImgColorSteel;
                    typeJson.InfoColor = Constantes.InfoColorSteel;
                    typeJson.TypeColor = Constantes.TypeColorSteel;
                    break;
                case Constantes.Fighting_FR:
                    typeJson.ImgColor = Constantes.ImgColorFighting;
                    typeJson.InfoColor = Constantes.InfoColorFighting;
                    typeJson.TypeColor = Constantes.TypeColorFighting;
                    break;
                case Constantes.Dragon_FR:
                    typeJson.ImgColor = Constantes.ImgColorDragon;
                    typeJson.InfoColor = Constantes.InfoColorDragon;
                    typeJson.TypeColor = Constantes.TypeColorDragon;
                    break;
                case Constantes.Water_FR:
                    typeJson.ImgColor = Constantes.ImgColorWater;
                    typeJson.InfoColor = Constantes.InfoColorWater;
                    typeJson.TypeColor = Constantes.TypeColorWater;
                    break;
                case Constantes.Electric_FR:
                    typeJson.ImgColor = Constantes.ImgColorElectric;
                    typeJson.InfoColor = Constantes.InfoColorElectric;
                    typeJson.TypeColor = Constantes.TypeColorElectric;
                    break;
                case Constantes.Fairy_FR:
                    typeJson.ImgColor = Constantes.ImgColorFairy;
                    typeJson.InfoColor = Constantes.InfoColorFairy;
                    typeJson.TypeColor = Constantes.TypeColorFairy;
                    break;
                case Constantes.Fire_FR:
                    typeJson.ImgColor = Constantes.ImgColorFire;
                    typeJson.InfoColor = Constantes.InfoColorFire;
                    typeJson.TypeColor = Constantes.TypeColorFire;
                    break;
                case Constantes.Ice_FR:
                    typeJson.ImgColor = Constantes.ImgColorIce;
                    typeJson.InfoColor = Constantes.InfoColorIce;
                    typeJson.TypeColor = Constantes.TypeColorIce;
                    break;
                case Constantes.Bug_FR:
                    typeJson.ImgColor = Constantes.ImgColorBug;
                    typeJson.InfoColor = Constantes.InfoColorBug;
                    typeJson.TypeColor = Constantes.TypeColorBug;
                    break;
                case Constantes.Normal_FR:
                    typeJson.ImgColor = Constantes.ImgColorNormal;
                    typeJson.InfoColor = Constantes.InfoColorNormal;
                    typeJson.TypeColor = Constantes.TypeColorNormal;
                    break;
                case Constantes.Grass_FR:
                    typeJson.ImgColor = Constantes.ImgColorGrass;
                    typeJson.InfoColor = Constantes.InfoColorGrass;
                    typeJson.TypeColor = Constantes.TypeColorGrass;
                    break;
                case Constantes.Poison_FR:
                    typeJson.ImgColor = Constantes.ImgColorPoison;
                    typeJson.InfoColor = Constantes.InfoColorPoison;
                    typeJson.TypeColor = Constantes.TypeColorPoison;
                    break;
                case Constantes.Psychic_FR:
                    typeJson.ImgColor = Constantes.ImgColorPsychic;
                    typeJson.InfoColor = Constantes.InfoColorPsychic;
                    typeJson.TypeColor = Constantes.TypeColorPsychic;
                    break;
                case Constantes.Rock_FR:
                    typeJson.ImgColor = Constantes.ImgColorRock;
                    typeJson.InfoColor = Constantes.InfoColorRock;
                    typeJson.TypeColor = Constantes.TypeColorRock;
                    break;
                case Constantes.Ground_FR:
                    typeJson.ImgColor = Constantes.ImgColorGround;
                    typeJson.InfoColor = Constantes.InfoColorGround;
                    typeJson.TypeColor = Constantes.TypeColorGround;
                    break;
                case Constantes.Ghost_FR:
                    typeJson.ImgColor = Constantes.ImgColorGhost;
                    typeJson.InfoColor = Constantes.InfoColorGhost;
                    typeJson.TypeColor = Constantes.TypeColorGhost;
                    break;
                case Constantes.Dark_FR:
                    typeJson.ImgColor = Constantes.ImgColorDark;
                    typeJson.InfoColor = Constantes.InfoColorDark;
                    typeJson.TypeColor = Constantes.TypeColorDark;
                    break;
                case Constantes.Flying_FR:
                    typeJson.ImgColor = Constantes.ImgColorFlying;
                    typeJson.InfoColor = Constantes.InfoColorFlying;
                    typeJson.TypeColor = Constantes.TypeColorFlying;
                    break;
            }
        }

        private string GetUrlImg(string url)
        {
            string html = CallUrl(url).Result;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var value = htmlDoc.DocumentNode.Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("fullMedia"));

            return value.OuterHtml.Split('"')[3];
        }

        private void WriteToJsonType(List<TypePokMobileJsonV1> typeJsons)
        {
            string json = JsonConvert.SerializeObject(typeJsons, Formatting.Indented);
            System.IO.File.WriteAllText("TypeScrapV1.json", json);
        }

        private void WriteToJsonType(List<TypePokMobileJsonV2> typeJsons)
        {
            string json = JsonConvert.SerializeObject(typeJsons, Formatting.Indented);
            System.IO.File.WriteAllText("TypeScrapV2.json", json);
        }

        private string GetNameTypeByLanguage(string type, string Language)
        {
            if (Language.Equals(Constantes.EN))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_EN;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_EN;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_EN;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_EN;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_EN;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_EN;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_EN;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_EN;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_EN;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_EN;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_EN;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_EN;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_EN;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_EN;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_EN;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_EN;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_EN;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_EN;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.ES))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_ES;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_ES;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_ES;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_ES;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_ES;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_ES;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_ES;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_ES;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_ES;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_ES;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_ES;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_ES;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_ES;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_ES;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_ES;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_ES;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_ES;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_ES;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.IT))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_IT;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_IT;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_IT;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_IT;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_IT;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_IT;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_IT;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_IT;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_IT;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_IT;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_IT;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_IT;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_IT;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_IT;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_IT;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_IT;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_IT;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_IT;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.DE))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_DE;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_DE;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_DE;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_DE;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_DE;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_DE;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_DE;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_DE;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_DE;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_DE;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_DE;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_DE;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_DE;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_DE;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_DE;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_DE;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_DE;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_DE;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.RU))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_RU;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_RU;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_RU;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_RU;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_RU;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_RU;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_RU;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_RU;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_RU;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_RU;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_RU;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_RU;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_RU;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_RU;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_RU;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_RU;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_RU;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_RU;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.JP))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_JP;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_JP;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_JP;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_JP;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_JP;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_JP;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_JP;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_JP;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_JP;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_JP;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_JP;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_JP;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_JP;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_JP;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_JP;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_JP;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_JP;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_JP;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.CO))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_CO;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_CO;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_CO;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_CO;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_CO;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_CO;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_CO;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_CO;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_CO;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_CO;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_CO;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_CO;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_CO;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_CO;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_CO;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_CO;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_CO;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_CO;
                        break;
                    default:
                        break;
                }
            else if (Language.Equals(Constantes.CN))
                switch (type)
                {
                    case Constantes.Steel_FR:
                        type = Constantes.Steel_CN;
                        break;
                    case Constantes.Fighting_FR:
                        type = Constantes.Fighting_CN;
                        break;
                    case Constantes.Dragon_FR:
                        type = Constantes.Dragon_CN;
                        break;
                    case Constantes.Water_FR:
                        type = Constantes.Water_CN;
                        break;
                    case Constantes.Electric_FR:
                        type = Constantes.Electric_CN;
                        break;
                    case Constantes.Fairy_FR:
                        type = Constantes.Fairy_CN;
                        break;
                    case Constantes.Fire_FR:
                        type = Constantes.Fire_CN;
                        break;
                    case Constantes.Ice_FR:
                        type = Constantes.Ice_CN;
                        break;
                    case Constantes.Bug_FR:
                        type = Constantes.Bug_CN;
                        break;
                    case Constantes.Normal_FR:
                        type = Constantes.Normal_CN;
                        break;
                    case Constantes.Grass_FR:
                        type = Constantes.Grass_CN;
                        break;
                    case Constantes.Poison_FR:
                        type = Constantes.Poison_CN;
                        break;
                    case Constantes.Psychic_FR:
                        type = Constantes.Psychic_CN;
                        break;
                    case Constantes.Rock_FR:
                        type = Constantes.Rock_CN;
                        break;
                    case Constantes.Ground_FR:
                        type = Constantes.Ground_CN;
                        break;
                    case Constantes.Ghost_FR:
                        type = Constantes.Ghost_CN;
                        break;
                    case Constantes.Dark_FR:
                        type = Constantes.Dark_CN;
                        break;
                    case Constantes.Flying_FR:
                        type = Constantes.Flying_CN;
                        break;
                    default:
                        break;
                }

            return type;
        }
        #endregion
    }
}
