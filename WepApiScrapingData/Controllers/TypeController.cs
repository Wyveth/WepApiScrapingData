using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WebApiScrapingData.Domain;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        #region Public Methods
        [HttpGet]
        public void ScrapingAll()
        {
            List<TypeJson> typeJsons = new List<TypeJson>();
            PopulateTypes(typeJsons);
            WriteToJsonType(typeJsons);
        }
        #endregion

        #region Json
        private void PopulateTypes(List<TypeJson> typeJsons)
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
                TypeJson typeJson = new TypeJson();
                typeJson.name_FR = type;
                typeJson.name_EN = GetNameTypeByLanguage(type, Constantes.EN);
                typeJson.name_ES = GetNameTypeByLanguage(type, Constantes.ES);
                typeJson.name_IT = GetNameTypeByLanguage(type, Constantes.IT);
                typeJson.name_DE = GetNameTypeByLanguage(type, Constantes.DE);
                typeJson.urlMiniGo = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Miniature_Type_" + type + "_GO.png");
                typeJson.urlFondGo = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Fond_Type_" + type + "_GO.png");
                typeJson.urlMiniHome = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Miniature_Type_" + type + "_HOME.png");
                typeJson.urlIconHome = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Icône_Type_" + type + "_HOME.png");
                typeJson.urlAutoHome = Constantes.urlPokepedia + GetUrlImg(Constantes.urlPokepedia + "/Fichier:Autocollant_Type_" + type + "_HOME.png");
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
        private void GetColor(TypeJson typeJson)
        {
            switch (typeJson.name_FR)
            {
                case Constantes.Steel_FR:
                    typeJson.imgColor = Constantes.ImgColorSteel;
                    typeJson.infoColor = Constantes.InfoColorSteel;
                    typeJson.typeColor = Constantes.TypeColorSteel;
                    break;
                case Constantes.Fighting_FR:
                    typeJson.imgColor = Constantes.ImgColorFighting;
                    typeJson.infoColor = Constantes.InfoColorFighting;
                    typeJson.typeColor = Constantes.TypeColorFighting;
                    break;
                case Constantes.Dragon_FR:
                    typeJson.imgColor = Constantes.ImgColorDragon;
                    typeJson.infoColor = Constantes.InfoColorDragon;
                    typeJson.typeColor = Constantes.TypeColorDragon;
                    break;
                case Constantes.Water_FR:
                    typeJson.imgColor = Constantes.ImgColorWater;
                    typeJson.infoColor = Constantes.InfoColorWater;
                    typeJson.typeColor = Constantes.TypeColorWater;
                    break;
                case Constantes.Electric_FR:
                    typeJson.imgColor = Constantes.ImgColorElectric;
                    typeJson.infoColor = Constantes.InfoColorElectric;
                    typeJson.typeColor = Constantes.TypeColorElectric;
                    break;
                case Constantes.Fairy_FR:
                    typeJson.imgColor = Constantes.ImgColorFairy;
                    typeJson.infoColor = Constantes.InfoColorFairy;
                    typeJson.typeColor = Constantes.TypeColorFairy;
                    break;
                case Constantes.Fire_FR:
                    typeJson.imgColor = Constantes.ImgColorFire;
                    typeJson.infoColor = Constantes.InfoColorFire;
                    typeJson.typeColor = Constantes.TypeColorFire;
                    break;
                case Constantes.Ice_FR:
                    typeJson.imgColor = Constantes.ImgColorIce;
                    typeJson.infoColor = Constantes.InfoColorIce;
                    typeJson.typeColor = Constantes.TypeColorIce;
                    break;
                case Constantes.Bug_FR:
                    typeJson.imgColor = Constantes.ImgColorBug;
                    typeJson.infoColor = Constantes.InfoColorBug;
                    typeJson.typeColor = Constantes.TypeColorBug;
                    break;
                case Constantes.Normal_FR:
                    typeJson.imgColor = Constantes.ImgColorNormal;
                    typeJson.infoColor = Constantes.InfoColorNormal;
                    typeJson.typeColor = Constantes.TypeColorNormal;
                    break;
                case Constantes.Grass_FR:
                    typeJson.imgColor = Constantes.ImgColorGrass;
                    typeJson.infoColor = Constantes.InfoColorGrass;
                    typeJson.typeColor = Constantes.TypeColorGrass;
                    break;
                case Constantes.Poison_FR:
                    typeJson.imgColor = Constantes.ImgColorPoison;
                    typeJson.infoColor = Constantes.InfoColorPoison;
                    typeJson.typeColor = Constantes.TypeColorPoison;
                    break;
                case Constantes.Psychic_FR:
                    typeJson.imgColor = Constantes.ImgColorPsychic;
                    typeJson.infoColor = Constantes.InfoColorPsychic;
                    typeJson.typeColor = Constantes.TypeColorPsychic;
                    break;
                case Constantes.Rock_FR:
                    typeJson.imgColor = Constantes.ImgColorRock;
                    typeJson.infoColor = Constantes.InfoColorRock;
                    typeJson.typeColor = Constantes.TypeColorRock;
                    break;
                case Constantes.Ground_FR:
                    typeJson.imgColor = Constantes.ImgColorGround;
                    typeJson.infoColor = Constantes.InfoColorGround;
                    typeJson.typeColor = Constantes.TypeColorGround;
                    break;
                case Constantes.Ghost_FR:
                    typeJson.imgColor = Constantes.ImgColorGhost;
                    typeJson.infoColor = Constantes.InfoColorGhost;
                    typeJson.typeColor = Constantes.TypeColorGhost;
                    break;
                case Constantes.Dark_FR:
                    typeJson.imgColor = Constantes.ImgColorDark;
                    typeJson.infoColor = Constantes.InfoColorDark;
                    typeJson.typeColor = Constantes.TypeColorDark;
                    break;
                case Constantes.Flying_FR:
                    typeJson.imgColor = Constantes.ImgColorFlying;
                    typeJson.infoColor = Constantes.InfoColorFlying;
                    typeJson.typeColor = Constantes.TypeColorFlying;
                    break;
            }
        }

        private string GetUrlImg(string url)
        {
            string html = CallUrl(url).Result;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var value = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("fullMedia")).First();

            return value.OuterHtml.Split('"')[3];
        }

        private void WriteToJsonType(List<TypeJson> typeJsons)
        {
            string json = JsonConvert.SerializeObject(typeJsons, Formatting.Indented);
            System.IO.File.WriteAllText("TypeScrap.json", json);
        }

        private string GetNameTypeByLanguage(string type, string Language)
        {
            string typeToReturn = "";
            if(Language.Equals(Constantes.EN))
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

            return type;
        }
        #endregion
    }
}
