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
