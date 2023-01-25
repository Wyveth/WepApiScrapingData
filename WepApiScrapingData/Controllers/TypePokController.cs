using HtmlAgilityPack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypePokController : ControllerBase
    {
        #region Fields
        private readonly IRepository<TypePok> _repository;
        #endregion

        #region Constructors
        public TypePokController(IRepository<TypePok> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("ScrapingAll")]
        public void ScrapingAll()
        {
            List<TypePokJson> typeJsons = new List<TypePokJson>();
            PopulateTypes(typeJsons);
            WriteToJsonType(typeJsons);
        }

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
        public IEnumerable<TypePok> GetFind(Expression<Func<TypePok, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        [HttpGet]
        [Route("GenerateJsonXamarin")]
        public async Task GenerateJsonXamarin()
        {
            IEnumerable<TypePok> typesPok = await _repository.GetAll();

            List<TypePokMobileJson> typesPokJson = new List<TypePokMobileJson>();

            foreach (TypePok item in typesPok.ToList())
            {
                TypePokMobileJson typePokJson = new TypePokMobileJson();
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

        [HttpPost]
        [Route("SaveInDB")]
        public void SaveInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("TypeScrap.json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("DlUpdatePathUrl")]
        public async Task DlUpdatePathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<TypePok> typesPok = await _repository.GetAll();
            foreach (TypePok typePok in typesPok)
            {
                typePok.PathMiniGo = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlMiniGo, typePok.Name_EN + "_" + Constantes.MiniGo, Constantes.MiniGo);
                typePok.PathFondGo = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlFondGo, typePok.Name_EN + "_" + Constantes.FondGo, Constantes.FondGo);
                typePok.PathMiniHome_FR = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlMiniHome, typePok.Name_EN + "_" + Constantes.MiniHome, Constantes.MiniHome);
                typePok.PathMiniHome_EN = "Content/Types/MiniHome/EN/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_ES = "Content/Types/MiniHome/ES/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_IT = "Content/Types/MiniHome/IT/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_DE = "Content/Types/MiniHome/DE/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_RU = "Content/Types/MiniHome/RU/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_CO = "Content/Types/MiniHome/CO/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_CN = "Content/Types/MiniHome/CN/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_JP = "Content/Types/MiniHome/JP/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathIconHome = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlIconHome, typePok.Name_EN + "_" + Constantes.IconHome, Constantes.IconHome);
                typePok.PathAutoHome = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlAutoHome, typePok.Name_EN + "_" + Constantes.AutoHome, Constantes.AutoHome);
            }

            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }
        #endregion

        #region Json
        private void PopulateTypes(List<TypePokJson> typeJsons)
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
                TypePokJson typeJson = new TypePokJson();
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
        private void GetColor(TypePokJson typeJson)
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

        private void WriteToJsonType(List<TypePokJson> typeJsons)
        {
            string json = JsonConvert.SerializeObject(typeJsons, Formatting.Indented);
            System.IO.File.WriteAllText("TypeScrap.json", json);
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
