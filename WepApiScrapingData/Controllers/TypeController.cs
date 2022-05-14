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
            types.Add("Acier");
            types.Add("Dragon");
            types.Add("Électrik");
            types.Add("Feu");
            types.Add("Insecte");
            types.Add("Plante");
            types.Add("Psy");
            types.Add("Sol");
            types.Add("Ténèbres");
            types.Add("Combat");
            types.Add("Eau");
            types.Add("Fée");
            types.Add("Glace");
            types.Add("Normal");
            types.Add("Poison");
            types.Add("Roche");
            types.Add("Spectre");
            types.Add("Vol");

            foreach (string type in types)
            {
                TypeJson typeJson = new TypeJson();
                typeJson.name = type;
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
            switch (typeJson.name)
            {
                case Constantes.Steel:
                    typeJson.imgColor = Constantes.ImgColorSteel;
                    typeJson.infoColor = Constantes.InfoColorSteel;
                    break;
                case Constantes.Fighting:
                    typeJson.imgColor = Constantes.ImgColorFighting;
                    typeJson.infoColor = Constantes.InfoColorFighting;
                    break;
                case Constantes.Dragon:
                    typeJson.imgColor = Constantes.ImgColorDragon;
                    typeJson.infoColor = Constantes.InfoColorDragon;
                    break;
                case Constantes.Water:
                    typeJson.imgColor = Constantes.ImgColorWater;
                    typeJson.infoColor = Constantes.InfoColorWater;
                    break;
                case Constantes.Electric:
                    typeJson.imgColor = Constantes.ImgColorElectric;
                    typeJson.infoColor = Constantes.InfoColorElectric;
                    break;
                case Constantes.Fairy:
                    typeJson.imgColor = Constantes.ImgColorFairy;
                    typeJson.infoColor = Constantes.InfoColorFairy;
                    break;
                case Constantes.Fire:
                    typeJson.imgColor = Constantes.ImgColorFire;
                    typeJson.infoColor = Constantes.InfoColorFire;
                    break;
                case Constantes.Ice:
                    typeJson.imgColor = Constantes.ImgColorIce;
                    typeJson.infoColor = Constantes.InfoColorIce;
                    break;
                case Constantes.Bug:
                    typeJson.imgColor = Constantes.ImgColorBug;
                    typeJson.infoColor = Constantes.InfoColorBug;
                    break;
                case Constantes.Normal:
                    typeJson.imgColor = Constantes.ImgColorNormal;
                    typeJson.infoColor = Constantes.InfoColorNormal;
                    break;
                case Constantes.Grass:
                    typeJson.imgColor = Constantes.ImgColorGrass;
                    typeJson.infoColor = Constantes.InfoColorGrass;
                    break;
                case Constantes.Poison:
                    typeJson.imgColor = Constantes.ImgColorPoison;
                    typeJson.infoColor = Constantes.InfoColorPoison;
                    break;
                case Constantes.Psychic:
                    typeJson.imgColor = Constantes.ImgColorPsychic;
                    typeJson.infoColor = Constantes.InfoColorPsychic;
                    break;
                case Constantes.Rock:
                    typeJson.imgColor = Constantes.ImgColorRock;
                    typeJson.infoColor = Constantes.InfoColorRock;
                    break;
                case Constantes.Ground:
                    typeJson.imgColor = Constantes.ImgColorGround;
                    typeJson.infoColor = Constantes.InfoColorGround;
                    break;
                case Constantes.Ghost:
                    typeJson.imgColor = Constantes.ImgColorGhost;
                    typeJson.infoColor = Constantes.InfoColorGhost;
                    break;
                case Constantes.Dark:
                    typeJson.imgColor = Constantes.ImgColorDark;
                    typeJson.infoColor = Constantes.InfoColorDark;
                    break;
                case Constantes.Flying:
                    typeJson.imgColor = Constantes.ImgColorSteel;
                    typeJson.infoColor = Constantes.InfoColorSteel;
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
        #endregion
    }
}
