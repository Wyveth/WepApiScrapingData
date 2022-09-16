using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Diagnostics;
using System.Net;
using WebApiScrapingData.Domain;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ScrapingDataController : ControllerBase
    {
        #region Public Methods
        [HttpGet]
        public void ScrapingAll()
        {
            List<DataJson> dataJsons = new List<DataJson>();

            getTranslateWithUrl(Constantes.Garchompite_URL, new());

            #region Europe
            string url_FR = Constantes.urlTestEvolution;
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

            RecursiveGetDataJsonWithUrl(url_FR, url_EN, url_ES, url_IT, url_DE, url_RU, url_JP, url_CO, url_CN, dataJsons);

            WriteToJson(dataJsons);
        }


        #endregion

        #region Private Methods
        #region Call Url Static/Dynamic
        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = await client.GetAsync(fullUrl);
            return await response.Content.ReadAsStringAsync();
        }

        private static HtmlNode CallUrlDynamic(string fullUrl)
        {
            ScrapingBrowser _scrapBrowser = new ScrapingBrowser() { IgnoreCookies = true, Timeout = TimeSpan.FromMinutes(5), };
            _scrapBrowser.Headers["Accept"] = "text/html, application/xhtml+xml, */*";
            _scrapBrowser.Headers["User-Agent"] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW 64; Trident/5.0)";
            return _scrapBrowser.NavigateToPage(new Uri(fullUrl)).Html;
        }
        #endregion

        #region Json
        private DataJson ParseHtmlToJson(HtmlDocument htmlDoc_FR, HtmlDocument htmlDoc_EN, HtmlDocument htmlDoc_ES, HtmlDocument htmlDoc_IT, HtmlDocument htmlDoc_DE, HtmlDocument htmlDoc_RU, HtmlDocument htmlDoc_JP, HtmlDocument htmlDoc_CO, HtmlDocument htmlDoc_CN, bool many = false, int option = 0)
        {
            List<HtmlNode> values;
            HtmlNode value;
            int i = 0;
            DataJson dataJson = new DataJson();

            #region Get Name & Number
            value = htmlDoc_FR.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title")).First();

            dataJson.number = value.InnerText.Trim().Split("\n")[1].Trim().Split(".")[1];

            int numbPok = int.Parse(dataJson.number.ToString());

            values = htmlDoc_FR.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
            #endregion
            #region FR
            #region Get Name & Number
            value = htmlDoc_FR.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title")).First();

            dataJson.number = value.InnerText.Trim().Split("\n")[1].Trim().Split(".")[1];

            if (many)
            {
                values = htmlDoc_FR.DocumentNode.Descendants("option").ToList();

                if (values[option].InnerText.Contains(Constantes.Alola))
                {
                    dataJson.FR.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_FR;
                    dataJson.FR.displayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_FR;
                }
                else if (values[option].InnerText.Contains(Constantes.Galar))
                {
                    dataJson.FR.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_FR;
                    dataJson.FR.displayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_FR;
                }
                else if (values[option].InnerText.Contains(Constantes.Hisui))
                {
                    dataJson.FR.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_FR;
                    dataJson.FR.displayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_FR;
                }
                else
                {
                    if (values[option].InnerText.Contains("Forme de Motisma"))
                    {
                        dataJson.FR.name = values[option].InnerText.Split(" ")[2];
                        dataJson.FR.displayName = values[option].InnerText.Split(" ")[2];
                    }
                    else if (values[option].InnerText.Contains(value.InnerText.Trim().Split("\n")[0]))
                    {
                        dataJson.FR.name = values[option].InnerText;
                        dataJson.FR.displayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else if (value.InnerText.Trim().Split("\n")[0] != values[option].InnerText)
                    {
                        dataJson.FR.name = value.InnerText.Trim().Split("\n")[0] + " " + values[option].InnerText;
                        dataJson.FR.displayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else
                    {
                        dataJson.FR.name = values[option].InnerText;
                        dataJson.FR.displayName = values[option].InnerText;
                    }
                }
            }
            else
            {
                dataJson.FR.name = value.InnerText.Trim().Split("\n")[0];
                dataJson.FR.displayName = value.InnerText.Trim().Split("\n")[0];
            }

            dataJson.FR.name = dataJson.FR.name.Replace("&#39;", "'").Replace(':', ' ');
            dataJson.FR.displayName = dataJson.FR.displayName.Replace("&#39;", "'").Replace(':', ' ');
            Debug.WriteLine(dataJson.number + ": " + dataJson.FR.name);
            #endregion

            #region Get Description
            values = htmlDoc_FR.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
            value = values[option].Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-x")).First();

            dataJson.FR.descriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

            value = values[option].Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-y")).First();

            dataJson.FR.descriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
            #endregion

            #region Get Image Pokémon
            values = htmlDoc_FR.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("profile-images")).First().Descendants("img").ToList();

            dataJson.urlImg = values[option].Attributes["src"].Value;
            #endregion

            #region Get Sprite Pokemon
            GetUrlSprite(Constantes.urlAllSprites, dataJson);
            #endregion

            #region Get Size, Category, Weight, Talent
            values = htmlDoc_FR.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
            .ToList();
            value = values[option];

            values = value.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

            dataJson.FR.size = values[0].InnerText;
            dataJson.FR.weight = values[1].InnerText;
            dataJson.FR.category = values[3].InnerText.Replace("&#39;", "'");
            if (values.Count().Equals(5))
                dataJson.FR.talent = values[4].InnerText;
            else if (values.Count() > 5)
                dataJson.FR.talent = values[4].InnerText + "," + values[5].InnerText;

            #endregion

            #region Get Description Talent
            values = value.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

            if (values.Count().Equals(1))
            {
                value = values[0].Descendants("p").First();
                dataJson.FR.descriptionTalent = value.InnerText.Replace("&#39;", "'");
            }
            else if (values.Count() > 1)
            {
                for (i = 0; i < 2; i++)
                {
                    value = values[i].Descendants("p").First();
                    if (i.Equals(0))
                        dataJson.FR.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                    else
                        dataJson.FR.descriptionTalent += ";" + value.InnerText.Replace("&#39;", "'");
                }

            }
            #endregion

            #region Type
            values = htmlDoc_FR.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("dtm-type")).First();

            if (value.InnerText.Trim().Split("\n").Length > 4)
                dataJson.FR.types = value.InnerText.Trim().Split("\n")[4].Trim();
            if (value.InnerText.Trim().Split("\n").Length > 10)
                dataJson.FR.types += "," + value.InnerText.Trim().Split("\n")[10].Trim();
            #endregion

            #region Faiblesse
            values = htmlDoc_FR.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("dtm-weaknesses")).First();
            values = value.Descendants("a").Where(node => node.GetAttributeValue("href", "").Contains("weakness")).ToList();

            i = 0;
            foreach (var item in values)
            {
                string type = item.OuterHtml.Split("\n\t")[1].Trim().Split(">")[1];
                if (i.Equals(0))
                {
                    dataJson.FR.weakness = type;
                    i++;
                }
                else
                {
                    dataJson.FR.weakness += "," + type;
                }

            }
            #endregion

            #region Evolution
            values = htmlDoc_FR.DocumentNode.Descendants("h3")
                .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

            i = 0;

            foreach (var item in values)
            {
                string pokName = item.InnerHtml.Trim().Split("\n")[0];
                if (i == 0)
                {
                    dataJson.FR.evolutions = pokName.Replace("&#39;", "'");
                    i++;
                }
                else
                {
                    dataJson.FR.evolutions += ',' + pokName.Replace("&#39;", "'");
                }
            }
            #endregion

            #region Stats + WhenEvolution
            GetStats(Constantes.urlStatsPB, dataJson, option);
            #endregion

            #region Type Evolution
            if (dataJson.FR.name.Contains(Constantes.MegaEvolution) && !dataJson.FR.name.Contains(Constantes.Meganium))
                dataJson.typeEvolution = Constantes.MegaLevel;
            else if (dataJson.FR.name.Contains(Constantes.GigaEvolution))
                dataJson.typeEvolution = Constantes.GigaLevel;
            else if (dataJson.FR.name.Contains(Constantes.Alola))
                dataJson.typeEvolution = Constantes.Alola;
            else if (dataJson.FR.name.Contains(Constantes.Galar))
                dataJson.typeEvolution = Constantes.Galar;
            else if (dataJson.FR.name.Contains(Constantes.Femelle))
                dataJson.typeEvolution = Constantes.VarianteSexe;
            else if (dataJson.FR.name.Contains(Constantes.Hisui))
                dataJson.typeEvolution = Constantes.Hisui;
            else if (option != 0)
                dataJson.typeEvolution = Constantes.Variant;
            else
                dataJson.typeEvolution = Constantes.NormalEvolution;
            #endregion

            #region Generation

            if ((numbPok >= 899 && numbPok <= 905) || dataJson.FR.name.Contains(Constantes.Hisui))
                dataJson.generation = 0;
            else if (numbPok <= 151)
                dataJson.generation = 1;
            else if (numbPok <= 251)
                dataJson.generation = 2;
            else if (numbPok <= 386)
                dataJson.generation = 3;
            else if (numbPok <= 494)
                dataJson.generation = 4;
            else if (numbPok <= 649)
                dataJson.generation = 5;
            else if (numbPok <= 721)
                dataJson.generation = 6;
            else if (numbPok <= 809)
                dataJson.generation = 7;
            else if (numbPok <= 898)
                dataJson.generation = 8;
            #endregion

            #region Next Url
            if (numbPok != Constantes.lastPokemonNumber)
            {
                value = htmlDoc_FR.DocumentNode.Descendants("a")
                    .Where(node => node.GetAttributeValue("class", "").Contains("next")).First();

                dataJson.FR.nextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
            }
            #endregion
            #endregion

            #region EN
            GetDataByEurope(htmlDoc_EN, dataJson.EN, dataJson.number, numbPok, values, value, i, many, option, Constantes.EN);
            #endregion

            #region ES
            GetDataByEurope(htmlDoc_ES, dataJson.ES, dataJson.number, numbPok, values, value, i, many, option, Constantes.ES);
            #endregion

            #region IT
            GetDataByEurope(htmlDoc_IT, dataJson.IT, dataJson.number, numbPok, values, value, i, many, option, Constantes.IT);
            #endregion

            #region DE
            GetDataByEurope(htmlDoc_DE, dataJson.DE, dataJson.number, numbPok, values, value, i, many, option, Constantes.DE);
            #endregion

            #region RU
            //GetDataByEurope(htmlDoc_RU, dataJson.RU, dataJson.number, numbPok, values, value, i, many, option, Constantes.RU);
            #endregion

            #region JP
            //GetDataByAsia(htmlDoc_JP, dataJson.JP, dataJson.number, numbPok, values, value, i, many, option, Constantes.JP);
            #endregion

            #region CO
            //GetDataByAsia(htmlDoc_CO, dataJson.CO, dataJson.number, numbPok, values, value, i, many, option, Constantes.CO);
            #endregion

            #region CN
            //GetDataByAsia(htmlDoc_CN, dataJson.CN, dataJson.number, numbPok, values, value, i, many, option, Constantes.CN);
            #endregion

            return dataJson;
        }

        private List<DataJson> RecursiveGetDataJsonWithUrl(string url_FR, string url_EN, string url_ES, string url_IT, string url_DE, string url_RU, string url_JP, string url_CO, string url_CN, List<DataJson> dataJsons)
        {
            #region Europe
            string response_FR = CallUrl(url_FR).Result;
            HtmlDocument htmlDoc_FR = new HtmlDocument();
            htmlDoc_FR.LoadHtml(response_FR);

            string response_EN = CallUrl(url_EN).Result;
            HtmlDocument htmlDoc_EN = new HtmlDocument();
            htmlDoc_EN.LoadHtml(response_EN);

            string response_ES = CallUrl(url_ES).Result;
            HtmlDocument htmlDoc_ES = new HtmlDocument();
            htmlDoc_ES.LoadHtml(response_ES);

            string response_IT = CallUrl(url_IT).Result;
            HtmlDocument htmlDoc_IT = new HtmlDocument();
            htmlDoc_IT.LoadHtml(response_IT);

            string response_DE = CallUrl(url_DE).Result;
            HtmlDocument htmlDoc_DE = new HtmlDocument();
            htmlDoc_DE.LoadHtml(response_DE);

            string response_RU = CallUrl(url_RU).Result;
            HtmlDocument htmlDoc_RU = new HtmlDocument();
            htmlDoc_RU.LoadHtml(response_RU);
            #endregion

            #region Asia
            //string response_JP = CallUrlDynamic(url_JP).Result;
            HtmlDocument htmlDoc_JP = new HtmlDocument();
            //htmlDoc_JP.LoadHtml(response_JP);

            //string response_CO = CallUrl(url_CO).Result;
            HtmlDocument htmlDoc_CO = new HtmlDocument();
            //htmlDoc_CO.LoadHtml(response_CO);

            //string response_CN = CallUrl(url_CN).Result;
            HtmlDocument htmlDoc_CN = new HtmlDocument();
            //htmlDoc_CN.LoadHtml(response_CN);
            #endregion

            HtmlNode value = htmlDoc_FR.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "").Contains("profile-images")).First();
            int countImg = value.Descendants("img").Count();

            DataJson dataJson = new DataJson();
            if (countImg.Equals(1))
            {
                dataJson = ParseHtmlToJson(htmlDoc_FR, htmlDoc_EN, htmlDoc_ES, htmlDoc_IT, htmlDoc_DE, htmlDoc_RU, htmlDoc_JP, htmlDoc_CO, htmlDoc_CN);
                dataJsons.Add(dataJson);
            }
            else
            {
                for (int i = 0; i < countImg; i++)
                {
                    dataJson = ParseHtmlToJson(htmlDoc_FR, htmlDoc_EN, htmlDoc_ES, htmlDoc_IT, htmlDoc_DE, htmlDoc_RU, htmlDoc_JP, htmlDoc_CO, htmlDoc_CN, true, i);

                    if (i > 0)
                    {
                        //A faire
                    }
                    dataJsons.Add(dataJson);
                }
            }

            if (!string.IsNullOrEmpty(dataJson.FR.nextUrl))
                RecursiveGetDataJsonWithUrl(dataJson.FR.nextUrl, dataJson.EN.nextUrl, dataJson.ES.nextUrl, dataJson.IT.nextUrl, dataJson.DE.nextUrl, dataJson.RU.nextUrl, dataJson.JP.nextUrl, dataJson.CO.nextUrl, dataJson.CN.nextUrl, dataJsons);

            return dataJsons;
        }

        private void WriteToJson(List<DataJson> dataJsons)
        {
            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            System.IO.File.WriteAllText("PokeScrap.json", json);
        }
        #endregion

        #region Get Url Img/Sprite
        private void GetUrlSprite(string spitesPkmDB, DataJson dataJson)
        {
            string response = CallUrl(spitesPkmDB).Result;
            dataJson.urlSprite = Constantes.urlPokepedia + GetUrlMini(response, dataJson.number, dataJson.FR.name);
            if (dataJson.urlSprite == Constantes.urlPokepedia)
                Debug.WriteLine(dataJson.FR.name);
        }

        private string GetUrlMini(string html, string number, string name)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var imgPok = htmlDoc.DocumentNode.Descendants("a")
                .Where(node => node.GetAttributeValue("title", "").Contains(number)).ToList();

            HtmlNode? x;
            string url = "";

            if (name.Contains("Mâle") || name.Contains("Femelle"))
                Debug.WriteLine(name);

            if (name.Contains(' ') && name.Contains('-'))
            {
                foreach (string item in name.Split(' ')[1].Split('-'))
                {
                    if (item.Equals("Normale") || item.Equals("Normal") || name.Equals("Plumeline Style Pom-Pom"))
                    {
                        url = imgPok[0].InnerHtml.Split("\"")[3];
                        break;
                    }
                    else
                    {
                        x = imgPok.Find(m => m.InnerHtml.Contains(item.Replace("é", "%C3%A9").Replace("É", "%C3%89").Replace("œ", "%C5%93").Replace("î", "%C3%AE").Replace("ō", "%C5%8D").Replace("ü", "%C3%BC")));
                        if (x != null)
                        {
                            url = x.InnerHtml.Split("\"")[3];
                            break;
                        }
                    }
                }
            }
            else if (name.Contains('-'))
            {
                foreach (string item in name.Split('-'))
                {
                    if (name.Equals("Ho-Oh") || name.Equals("Porygon-Z") || name.Equals("Ama-Ama"))
                    {
                        x = imgPok[0];
                        if (x != null)
                        {
                            url = x.InnerHtml.Split("\"")[3];
                            break;
                        }
                    }
                    if (item.Equals("Normale") || item.Equals("Normal"))
                    {
                        url = imgPok[0].InnerHtml.Split("\"")[3];
                        break;
                    }
                    else
                    {
                        x = imgPok.Find(m => m.InnerHtml.Contains(item.Replace("é", "%C3%A9").Replace("É", "%C3%89").Replace("œ", "%C5%93").Replace("î", "%C3%AE").Replace("ō", "%C5%8D").Replace("ü", "%C3%BC")));
                        if (x != null)
                        {
                            url = x.InnerHtml.Split("\"")[3];
                            break;
                        }
                    }
                }
            }
            else if (name.Contains("Alola"))
            {
                x = imgPok.Find(m => m.InnerHtml.Contains(Constantes.Alola));
                if (x != null)
                {
                    url = x.InnerHtml.Split("\"")[3];
                }
            }
            else if (name.Contains("Galar"))
            {
                x = imgPok.Find(m => m.InnerHtml.Contains(Constantes.Galar));
                if (x != null)
                {
                    url = x.InnerHtml.Split("\"")[3];
                }
            }
            else if (name.Contains("Mâle") || name.Contains("Femelle"))
            {
                if (name.Contains("Déflaisan"))
                {
                    if (name.Contains("Mâle"))
                        url = "/images/1/12/Miniature_521_%E2%99%82_EB.png";
                    else
                        url = "/images/e/e3/Miniature_521_%E2%99%80_EB.png";
                }
                else if (name.Contains("Viskuse"))
                {
                    if (name.Contains("Mâle"))
                        url = "/images/5/5e/Miniature_592_%E2%99%82_EB.png";
                    else
                        url = "/images/9/9a/Miniature_592_%E2%99%80_EB.png";
                }
                else if (name.Contains("Moyade"))
                {
                    if (name.Contains("Mâle"))
                        url = "/images/1/1a/Miniature_593_%E2%99%82_EB.png";
                    else
                        url = "/images/9/95/Miniature_593_%E2%99%80_EB.png";
                }
                else if (name.Contains("Némélios"))
                {
                    if (name.Contains("Mâle"))
                        url = "/images/2/2f/Miniature_668_%E2%99%82_XY.png";
                    else
                        url = "/images/a/ac/Miniature_668_%E2%99%80_XY.png";
                }
                else if (name.Contains("Mistigrix"))
                {
                    if (name.Contains("Mâle"))
                        url = "/images/1/1d/Miniature_678_%E2%99%82_EB.png";
                    else
                        url = "/images/b/bd/Miniature_678_%E2%99%80_EB.png";
                }
                else if (name.Contains("Wimessir"))
                {
                    if (name.Contains("Mâle"))
                        url = imgPok[0].InnerHtml.Split("\"")[3];
                    else
                        url = imgPok[1].InnerHtml.Split("\"")[3];
                }

                x = imgPok.Find(m => m.InnerHtml.Contains(Constantes.Galar));
                if (x != null)
                {
                    url = x.InnerHtml.Split("\"")[3];
                }
            }
            else if (name.Contains(' '))
            {
                foreach (string item in name.Split(' '))
                {
                    if (item.Equals("Normale") || item.Equals("Normal"))
                    {
                        url = imgPok[0].InnerHtml.Split("\"")[3];
                        break;
                    }
                    else
                    {
                        if (name.Contains("M. Mime") || name.Contains("Mime Jr.") || name.Contains("M. Glaquette"))
                        {
                            if (name.Equals("M. Mime") || name.Contains("Mime Jr.") || name.Contains("M. Glaquette"))
                                x = imgPok[0];
                            else
                                x = imgPok[1];

                            url = x.InnerHtml.Split("\"")[3];
                        }
                        else if (name.Contains("Shifours") || name.Contains("Sylveroy"))
                        {
                            if (name.Contains("Shifours"))
                            {
                                if (name.Contains("Style Poing Final"))
                                {
                                    if (!name.Contains("Gigamax"))
                                        x = imgPok[0];
                                    else
                                        x = imgPok[1];

                                    url = x.InnerHtml.Split("\"")[3];
                                }
                                else
                                {
                                    if (!name.Contains("Gigamax"))
                                        x = imgPok[2];
                                    else
                                        x = imgPok[3];

                                    url = x.InnerHtml.Split("\"")[3];
                                }
                            }
                            else if (name.Contains("Sylveroy"))
                            {
                                if (name.Contains("Cavalier du Froid"))
                                    x = imgPok[1];
                                else
                                    x = imgPok[2];
                                url = x.InnerHtml.Split("\"")[3];
                            }
                        }
                        else if (name.Contains("Type"))
                        {
                            x = imgPok[0];
                            url = x.InnerHtml.Split("\"")[3];
                        }
                        else
                        {
                            x = imgPok.Find(m => m.InnerHtml.Contains(item.Replace("é", "%C3%A9").Replace("É", "%C3%89").Replace("œ", "%C5%93").Replace("î", "%C3%AE").Replace("ō", "%C5%8D").Replace("ü", "%C3%BC")));
                            if (x != null)
                            {
                                url = x.InnerHtml.Split("\"")[3];
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                url = imgPok[0].InnerHtml.Split("\"")[3];
            }

            return url;
        }
        #endregion

        #region Get Data Stat & When Evolution
        private void GetStats(string urlStats, DataJson dataJson, int option)
        {
            string name = GetNamePokebip(dataJson, option);
            string response = CallUrl(urlStats + name).Result;
            GetStatsPokemon(response, dataJson);
        }

        private string GetNamePokebip(DataJson dataJson, int option)
        {
            string nameSite = "";

            if (dataJson.FR.name.Contains("♀") || dataJson.FR.name.Contains("♂"))
            {
                if (dataJson.FR.name.Contains("♀"))
                {
                    nameSite = dataJson.FR.name.Split('♀')[0] + "-f";
                }
                else
                {
                    nameSite = dataJson.FR.name.Split('♂')[0] + "-m";
                }
            }
            else if (dataJson.FR.name.Contains(Constantes.Alola))
            {
                nameSite = dataJson.FR.displayName.Split(' ')[0] + "-" + Constantes.Alola;
            }
            else if (dataJson.FR.name.Contains(Constantes.Galar))
            {
                if (dataJson.FR.name.Contains("M. Mime"))
                    nameSite = dataJson.FR.displayName.Replace(". ", "-").Split(' ')[0];
                else
                    nameSite = dataJson.FR.displayName.Split(' ')[0] + "-" + Constantes.Galar;
            }
            else if (dataJson.FR.name.Contains(Constantes.Hisui))
            {
                nameSite = dataJson.FR.displayName.Split(' ')[0];
            }
            else if (dataJson.FR.name.Contains("Mâle") || dataJson.FR.name.Contains("Femelle"))
            {
                if (dataJson.FR.name.Contains("Déflaisan") || dataJson.FR.name.Contains("Viskuse") || dataJson.FR.name.Contains("Moyade") || dataJson.FR.name.Contains("Mistigrix") || dataJson.FR.name.Contains("Wimessir"))
                {
                    if (dataJson.FR.name.Contains("Mâle"))
                        nameSite = dataJson.FR.displayName;
                    else
                        nameSite = dataJson.FR.displayName + "-femelle";
                }
                else if (dataJson.FR.name.Contains("Némélios"))
                {
                    nameSite = dataJson.FR.displayName;
                }
            }
            else if (dataJson.FR.name.Contains(' '))
            {
                if (dataJson.FR.name.Contains(". "))
                {
                    nameSite = dataJson.FR.displayName.Replace(". ", "-");
                }
                else if (dataJson.FR.name.Contains("Primo"))
                {
                    nameSite = "Primo-" + dataJson.FR.displayName;
                }
                else if (dataJson.FR.name.Contains("Forme")
                    || dataJson.FR.name.Contains("Cape")
                    || dataJson.FR.name.Contains("Temps")
                    || dataJson.FR.name.Contains("Mer")
                    || dataJson.FR.name.Contains("Aspect")
                    || dataJson.FR.name.Contains("Motif")
                    || dataJson.FR.name.Contains("Style")
                    || dataJson.FR.name.Contains("Mode")
                    || dataJson.FR.name.Contains("Héros")
                    || dataJson.FR.name.Contains("Forme Normal")
                    || dataJson.FR.name.Contains("Enchaîné")
                    || dataJson.FR.name.Contains("Cavalier")
                    || dataJson.FR.name.Contains("Necrozma"))
                {
                    if (option != 0)
                    {
                        if (dataJson.FR.name.Contains("Dialga") || dataJson.FR.name.Contains("Palkia"))
                        {
                            nameSite = dataJson.FR.displayName;
                        }
                        else if (dataJson.FR.name.Contains("Prismillon"))
                        {
                            nameSite = dataJson.FR.name.Split(' ')[0] + "-" + dataJson.FR.name.Split(' ')[1] + "-" + dataJson.FR.name.Split(' ')[2];
                        }
                        else if (dataJson.FR.name.Contains("Parfaite"))
                        {
                            nameSite = dataJson.FR.displayName + "-Parfait";
                        }
                        else if (dataJson.FR.name.Contains("Necrozma"))
                        {
                            if (dataJson.FR.name.Contains("Crinière"))
                                nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Split(' ')[1] + "-" + dataJson.FR.name.Split(' ')[3];
                            else if (dataJson.FR.name.Contains("Ailes"))
                                nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Split(' ')[1] + "-" + dataJson.FR.name.Replace('\'', ' ').Split(' ')[4];
                            else
                                nameSite = dataJson.FR.displayName;
                        }
                        else if (dataJson.FR.name.Contains("Shifours"))
                        {
                            if (dataJson.FR.name.Contains("Gigamax"))
                            {
                                nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Replace(" (", " ").Replace(")", "").Split(' ')[3] + "-" + dataJson.FR.name.Replace(" (", " ").Replace(")", "").Split(' ')[4] + "-" + dataJson.FR.name.Replace(" (", " ").Replace(")", "").Split(' ')[1];
                            }
                            else
                            {
                                nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Split(' ')[2] + "-" + dataJson.FR.name.Split(' ')[3];
                            }
                        }
                        else if (dataJson.FR.name.Contains("Sylveroy"))
                        {
                            nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Replace('’', ' ').Split(' ')[1] + "-" + dataJson.FR.name.Replace("’", " ").Split(' ')[3];
                        }
                        else
                        {
                            nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Split(' ')[2];
                        }
                    }
                    else
                    {
                        if (dataJson.FR.name.Contains("Salarsen"))
                            nameSite = dataJson.FR.displayName + "-" + dataJson.FR.name.Split(' ')[2];
                        else
                            nameSite = dataJson.FR.displayName;
                    }
                }
                else
                {
                    if (!dataJson.FR.name.Contains("Mime Jr."))
                        nameSite = dataJson.FR.name.Replace(' ', '-');
                    else
                        nameSite = dataJson.FR.name.Split(" ")[0] + "-" + dataJson.FR.name.Split(" ")[1].Split('.')[0];
                }
            }
            else
            {
                if (dataJson.FR.name.Contains("Arceus"))
                {
                    nameSite = "arceus-normal";
                }
                else if (dataJson.FR.name.Contains("Spectreval"))
                {
                    nameSite = "sprectreval";
                }
                else
                {
                    nameSite = dataJson.FR.name;
                }
            }

            return nameSite;
        }

        private void GetStatsPokemon(string html, DataJson dataJson)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var stats = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("progress-bar")).ToList();

            if (stats.Count >= 6)
            {
                dataJson.statPv = int.Parse(stats[0].InnerText);
                dataJson.statAttaque = int.Parse(stats[1].InnerText);
                dataJson.statDefense = int.Parse(stats[2].InnerText);
                dataJson.statAttaqueSpe = int.Parse(stats[3].InnerText);
                dataJson.statDefenseSpe = int.Parse(stats[4].InnerText);
                dataJson.statVitesse = int.Parse(stats[5].InnerText);
                dataJson.statTotal = dataJson.statPv + dataJson.statAttaque + dataJson.statDefense + dataJson.statAttaqueSpe + dataJson.statDefenseSpe + dataJson.statVitesse;
            }
            else
                Debug.WriteLine("Stats Erreur: " + dataJson.number + ": " + dataJson.FR.name);

            var detailsEvol = htmlDoc.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("list-group-item")).ToList();

            var filter = "";
            if (dataJson.FR.name.Contains(Constantes.Alola) || dataJson.FR.name.Contains(Constantes.Galar) || dataJson.FR.name.Contains(Constantes.Hisui))
                filter = dataJson.FR.displayName;
            else
                filter = dataJson.FR.name;

            HtmlNode? evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter));

            if (evol != null)
            {
                var i = evol.ChildNodes[5].InnerText.Trim().Split("\n").Length;

                if (i.Equals(1))
                {
                    dataJson.FR.whenEvolution = dataJson.EN.whenEvolution = dataJson.ES.whenEvolution = dataJson.IT.whenEvolution = dataJson.DE.whenEvolution = Constantes.Base_EU;
                    dataJson.RU.whenEvolution = Constantes.Base_RU;
                }
                else
                {
                    if (evol.ChildNodes[5].InnerText.Trim().Split("\n")[2].Contains("Reproduction"))
                        evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter) && !m.ChildNodes[5].InnerText.Trim().Contains("Reproduction"));

                    if (evol == null)
                        evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter));

                    dataJson.FR.whenEvolution = evol.ChildNodes[5].InnerText.Trim().Split("\n")[2].Trim().Replace("&#039;", "'");

                    if (dataJson.FR.whenEvolution.Contains("."))
                        dataJson.FR.whenEvolution = dataJson.FR.whenEvolution.Split('.')[dataJson.FR.whenEvolution.Split('.').Length - 1];

                    GetTranslationWhenEvolution(dataJson);
                }

                Debug.WriteLine("Evolution Essai: " + dataJson.number + ": " + dataJson.FR.name + " - " + dataJson.FR.whenEvolution);
            }
            else
                Debug.WriteLine("Evolution Erreur: " + dataJson.number + ": " + dataJson.FR.name);
        }

        private void GetTranslationWhenEvolution(DataJson dataJson)
        {
            if (dataJson.FR.whenEvolution.Contains(Constantes.Level_FR))
                Translate(dataJson, Constantes.Level_FR);
            else if (dataJson.FR.whenEvolution.Contains(Constantes.Stone_FR))
                Translate(dataJson, Constantes.Stone_FR);
            else if (dataJson.FR.whenEvolution.Contains(Constantes.MegaEvolutionWith_FR))
                Translate(dataJson, Constantes.MegaEvolutionWith_FR);
            else if (dataJson.FR.whenEvolution.Contains(Constantes.GigantamaxForm_FR))
                Translate(dataJson, Constantes.GigantamaxForm_FR);
            else if (dataJson.FR.whenEvolution.Contains(Constantes.Exchange_FR))
                Translate(dataJson, Constantes.Exchange_FR);
            else if (dataJson.FR.whenEvolution.Contains(Constantes.Reproduction_FR))
                Translate(dataJson, Constantes.Reproduction_FR);
            else
            {
                //Translate(dataJson, Constantes.Level_FR);
            }
        }

        private void Translate(DataJson dataJson, string value)
        {
            switch (value)
            {
                case Constantes.Level_FR:
                    #region Level
                    if (dataJson.FR.whenEvolution.Contains(Constantes.PID_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.PID_FR, Constantes.PID_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.PID_FR, Constantes.PID_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.PID_FR, Constantes.PID_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.PID_FR, Constantes.PID_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.PID_FR, Constantes.PID_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.PID_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.PID_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.PID_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.ASD_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.ASD_FR, Constantes.ASD_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.ASD_FR, Constantes.ASD_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.ASD_FR, Constantes.ASD_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.ASD_FR, Constantes.ASD_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.ASD_FR, Constantes.ASD_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.ASD_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.ASD_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.ASD_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.AID_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.AID_FR, Constantes.AID_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.AID_FR, Constantes.AID_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.AID_FR, Constantes.AID_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.AID_FR, Constantes.AID_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.AID_FR, Constantes.AID_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.AID_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.AID_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.AID_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.AED_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.AED_FR, Constantes.AED_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.AED_FR, Constantes.AED_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.AED_FR, Constantes.AED_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.AED_FR, Constantes.AED_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.AED_FR, Constantes.AED_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.AED_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.AED_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.AED_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.PIB_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.PIB_FR, Constantes.PIB_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.PIB_FR, Constantes.PIB_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.PIB_FR, Constantes.PIB_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.PIB_FR, Constantes.PIB_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.PIB_FR, Constantes.PIB_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.PIB_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.PIB_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.PIB_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.FM_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.FM_FR, Constantes.FM_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.FM_FR, Constantes.FM_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.FM_FR, Constantes.FM_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.FM_FR, Constantes.FM_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.FM_FR, Constantes.FM_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.FM_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.FM_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.FM_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.M_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.M_FR, Constantes.M_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.M_FR, Constantes.M_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.M_FR, Constantes.M_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.M_FR, Constantes.M_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.M_FR, Constantes.M_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.M_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.M_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.M_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.D_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.D_FR, Constantes.D_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.D_FR, Constantes.D_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.D_FR, Constantes.D_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.D_FR, Constantes.D_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.D_FR, Constantes.D_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.D_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.D_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.D_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.N_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.N_FR, Constantes.N_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.N_FR, Constantes.N_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.N_FR, Constantes.N_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.N_FR, Constantes.N_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.N_FR, Constantes.N_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.N_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.N_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.N_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.DLUL_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.DLUL_FR, Constantes.DLUL_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.DLUL_FR, Constantes.DLUL_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.DLUL_FR, Constantes.DLUL_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.DLUL_FR, Constantes.DLUL_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.DLUL_FR, Constantes.DLUL_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.DLUL_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.DLUL_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.DLUL_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.NSUS_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.NSUS_FR, Constantes.NSUS_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.NSUS_FR, Constantes.NSUS_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.NSUS_FR, Constantes.NSUS_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.NSUS_FR, Constantes.NSUS_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.NSUS_FR, Constantes.NSUS_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.NSUS_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.NSUS_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.NSUS_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.CR_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.CR_FR, Constantes.CR_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.CR_FR, Constantes.CR_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.CR_FR, Constantes.CR_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.CR_FR, Constantes.CR_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.CR_FR, Constantes.CR_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.CR_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.CR_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.CR_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.OSA_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.OSA_FR, Constantes.OSA_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.OSA_FR, Constantes.OSA_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.OSA_FR, Constantes.OSA_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.OSA_FR, Constantes.OSA_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.OSA_FR, Constantes.OSA_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.OSA_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.OSA_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.OSA_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.CB_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.CB_FR, Constantes.CB_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.CB_FR, Constantes.CB_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.CB_FR, Constantes.CB_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.CB_FR, Constantes.CB_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.CB_FR, Constantes.CB_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.CB_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.CB_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.CB_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.SPN_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.SPN_FR, Constantes.SPN_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.SPN_FR, Constantes.SPN_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.SPN_FR, Constantes.SPN_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.SPN_FR, Constantes.SPN_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.SPN_FR, Constantes.SPN_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.SPN_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.SPN_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.SPN_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.WN_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.WN_FR, Constantes.WN_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.WN_FR, Constantes.WN_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.WN_FR, Constantes.WN_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.WN_FR, Constantes.WN_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.WN_FR, Constantes.WN_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.WN_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.WN_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.WN_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.DPIT_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.DPIT_FR, Constantes.DPIT_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.DPIT_FR, Constantes.DPIT_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.DPIT_FR, Constantes.DPIT_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.DPIT_FR, Constantes.DPIT_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.DPIT_FR, Constantes.DPIT_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.DPIT_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.DPIT_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.DPIT_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.OSG_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.OSG_FR, Constantes.OSG_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.OSG_FR, Constantes.OSG_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.OSG_FR, Constantes.OSG_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.OSG_FR, Constantes.OSG_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.OSG_FR, Constantes.OSG_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.OSG_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.OSG_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.OSG_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.AG_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.AG_FR, Constantes.AG_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.AG_FR, Constantes.AG_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.AG_FR, Constantes.AG_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.AG_FR, Constantes.AG_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.AG_FR, Constantes.AG_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.AG_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.AG_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.AG_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.NR_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.NR_FR, Constantes.NR_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.NR_FR, Constantes.NR_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.NR_FR, Constantes.NR_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.NR_FR, Constantes.NR_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.NR_FR, Constantes.NR_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.NR_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.NR_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.NR_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.SUSE_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.SUSE_FR, Constantes.SUSE_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.SUSE_FR, Constantes.SUSE_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.SUSE_FR, Constantes.SUSE_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.SUSE_FR, Constantes.SUSE_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.SUSE_FR, Constantes.SUSE_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.SUSE_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.SUSE_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.SUSE_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.LULB_FR))
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.LULB_FR, Constantes.LULB_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.LULB_FR, Constantes.LULB_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.LULB_FR, Constantes.LULB_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.LULB_FR, Constantes.LULB_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.LULB_FR, Constantes.LULB_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.LULB_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.LULB_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.LULB_JP);
                    }
                    else
                    {
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(value, Constantes.Level_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(value, Constantes.Level_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(value, Constantes.Level_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(value, Constantes.Level_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(value, Constantes.Level_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(value, Constantes.Level_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(value, Constantes.Level_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(value, Constantes.Level_JP);
                    }
                    #endregion
                    break;
                case Constantes.Stone_FR:
                    #region Stone
                    Dictionary<string, string> dicTranslationStone = new();
                    if (dataJson.FR.whenEvolution.Contains(Constantes.MoonStone_FR))
                    {
                        getTranslateWithUrl(Constantes.MoonStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.MoonStone_FR, Constantes.MoonStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.MoonStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.MoonStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.MoonStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.FireStone_FR))
                    {
                        getTranslateWithUrl(Constantes.FireStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.FireStone_FR, Constantes.FireStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.FireStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.FireStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.FireStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.LeafStone_FR))
                    {
                        getTranslateWithUrl(Constantes.LeafStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.EN], Constantes.OSA_FR, Constantes.OSA_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.ES], Constantes.OSA_FR, Constantes.OSA_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.IT], Constantes.OSA_FR, Constantes.OSA_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.DE], Constantes.OSA_FR, Constantes.OSA_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.LeafStone_FR, Constantes.LeafStone_RU, Constantes.OSA_FR, Constantes.OSA_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.LeafStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.LeafStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.LeafStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.WaterStone_FR))
                    {
                        getTranslateWithUrl(Constantes.WaterStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.WaterStone_FR, Constantes.WaterStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.WaterStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.WaterStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.WaterStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.ThunderStone_FR))
                    {
                        getTranslateWithUrl(Constantes.ThunderStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.EN], Constantes.OSA_FR, Constantes.OSA_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.ES], Constantes.OSA_FR, Constantes.OSA_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.IT], Constantes.OSA_FR, Constantes.OSA_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.DE], Constantes.OSA_FR, Constantes.OSA_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ThunderStone_FR, Constantes.ThunderStone_RU, Constantes.OSA_FR, Constantes.OSA_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.CO], Constantes.OSA_FR, Constantes.OSA_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.CN], Constantes.OSA_FR, Constantes.OSA_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.JP], Constantes.OSA_FR, Constantes.OSA_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.SunStone_FR))
                    {
                        getTranslateWithUrl(Constantes.SunStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.SunStone_FR, Constantes.SunStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.SunStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.SunStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.SunStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.ShinyStone_FR))
                    {
                        getTranslateWithUrl(Constantes.ShinyStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ShinyStone_FR, Constantes.ShinyStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.DuckStone_FR))
                    {
                        getTranslateWithUrl(Constantes.DuckStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DuckStone_FR, Constantes.DuckStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DuckStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DuckStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DuckStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.IceStone_FR))
                    {
                        getTranslateWithUrl(Constantes.IceStone_URL, dicTranslationStone);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.IceStone_FR, Constantes.IceStone_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.IceStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.IceStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.IceStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.DawnStone_FR))
                    {
                        getTranslateWithUrl(Constantes.DawnStone_URL, dicTranslationStone);

                        if (dataJson.FR.whenEvolution.Contains(Constantes.M_FR))
                        {
                            dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.EN], Constantes.M_FR, Constantes.M_EN);
                            dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.ES], Constantes.M_FR, Constantes.M_ES);
                            dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.IT], Constantes.M_FR, Constantes.M_IT);
                            dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.DE], Constantes.M_FR, Constantes.M_DE);
                            dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, Constantes.DawnStone_RU, Constantes.M_FR, Constantes.M_RU);
                            dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CO], Constantes.M_FR, Constantes.M_CO);
                            dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CN], Constantes.M_FR, Constantes.M_CN);
                            dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.JP], Constantes.M_FR, Constantes.M_JP);
                        }
                        else if (dataJson.FR.whenEvolution.Contains(Constantes.FM_FR))
                        {
                            dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.EN], Constantes.FM_FR, Constantes.OSA_EN);
                            dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.ES], Constantes.FM_FR, Constantes.OSA_ES);
                            dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.IT], Constantes.FM_FR, Constantes.OSA_IT);
                            dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.DE], Constantes.FM_FR, Constantes.OSA_DE);
                            dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, Constantes.DawnStone_RU, Constantes.FM_FR, Constantes.OSA_RU);
                            dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CO], Constantes.FM_FR, Constantes.OSA_CO);
                            dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CN], Constantes.FM_FR, Constantes.OSA_CN);
                            dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.JP], Constantes.FM_FR, Constantes.OSA_JP);
                        }
                        else
                        {
                            dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.EN]);
                            dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.ES]);
                            dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.IT]);
                            dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.DE]);
                            dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, Constantes.DawnStone_RU);
                            dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CO]);
                            dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CN]);
                            dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.JP]);
                        }
                    }
                    #endregion
                    break;
                case Constantes.MegaEvolutionWith_FR:
                    #region Méga Evolution
                    Dictionary<string, string> dicTranslationMega = new();
                    if (dataJson.FR.whenEvolution.Contains(Constantes.Venusaurite_FR))
                    {
                        getTranslateWithUrl(Constantes.Venusaurite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Venusaurite_FR, Constantes.Venusaurite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Charizardite_X_FR))
                    {
                        getTranslateWithUrl(Constantes.Charizardite_X_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_X_FR, Constantes.Charizardite_X_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Charizardite_Y_FR))
                    {
                        getTranslateWithUrl(Constantes.Charizardite_Y_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Charizardite_Y_FR, Constantes.Charizardite_Y_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Blastoisinite_FR))
                    {
                        getTranslateWithUrl(Constantes.Blastoisinite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blastoisinite_FR, Constantes.Blastoisinite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Beedrillite_FR))
                    {
                        getTranslateWithUrl(Constantes.Beedrillite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Beedrillite_FR, Constantes.Beedrillite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Pidgeotite_FR))
                    {
                        getTranslateWithUrl(Constantes.Pidgeotite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pidgeotite_FR, Constantes.Pidgeotite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Alakazite_FR))
                    {
                        getTranslateWithUrl(Constantes.Alakazite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Alakazite_FR, Constantes.Alakazite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Alakazite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Alakazite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Alakazite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Slowbronite_FR))
                    {
                        getTranslateWithUrl(Constantes.Slowbronite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Slowbronite_FR, Constantes.Slowbronite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Gengarite_FR))
                    {
                        getTranslateWithUrl(Constantes.Gengarite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gengarite_FR, Constantes.Gengarite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gengarite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gengarite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gengarite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Kangaskhanite_FR))
                    {
                        getTranslateWithUrl(Constantes.Kangaskhanite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Kangaskhanite_FR, Constantes.Kangaskhanite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Pinsirite_FR))
                    {
                        getTranslateWithUrl(Constantes.Pinsirite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Pinsirite_FR, Constantes.Pinsirite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Gyaradosite_FR))
                    {
                        getTranslateWithUrl(Constantes.Gyaradosite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gyaradosite_FR, Constantes.Gyaradosite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Aerodactylite_FR))
                    {
                        getTranslateWithUrl(Constantes.Aerodactylite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aerodactylite_FR, Constantes.Aerodactylite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Mewtwonite_X_FR))
                    {
                        getTranslateWithUrl(Constantes.Mewtwonite_X_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_X_FR, Constantes.Mewtwonite_X_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Mewtwonite_Y_FR))
                    {
                        getTranslateWithUrl(Constantes.Mewtwonite_Y_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, Constantes.Mewtwonite_Y_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Ampharosite_FR))
                    {
                        getTranslateWithUrl(Constantes.Ampharosite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Ampharosite_FR, Constantes.Ampharosite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Steelixite_FR))
                    {
                        getTranslateWithUrl(Constantes.Steelixite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Steelixite_FR, Constantes.Steelixite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Steelixite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Steelixite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Steelixite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Scizorite_FR))
                    {
                        getTranslateWithUrl(Constantes.Scizorite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Scizorite_FR, Constantes.Scizorite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Scizorite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Scizorite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Scizorite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Heracronite_FR))
                    {
                        getTranslateWithUrl(Constantes.Heracronite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Heracronite_FR, Constantes.Heracronite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Heracronite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Heracronite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Heracronite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Houndoominite_FR))
                    {
                        getTranslateWithUrl(Constantes.Houndoominite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Houndoominite_FR, Constantes.Houndoominite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Tyranitarite_FR))
                    {
                        getTranslateWithUrl(Constantes.Tyranitarite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Tyranitarite_FR, Constantes.Tyranitarite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Sceptilite_FR))
                    {
                        getTranslateWithUrl(Constantes.Sceptilite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sceptilite_FR, Constantes.Sceptilite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Blazikenite_FR))
                    {
                        getTranslateWithUrl(Constantes.Blazikenite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Blazikenite_FR, Constantes.Blazikenite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Swampertite_FR))
                    {
                        getTranslateWithUrl(Constantes.Swampertite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Swampertite_FR, Constantes.Swampertite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Swampertite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Swampertite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Swampertite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Gardevoirite_FR))
                    {
                        getTranslateWithUrl(Constantes.Gardevoirite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Gardevoirite_FR, Constantes.Gardevoirite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Sablenite_FR))
                    {
                        getTranslateWithUrl(Constantes.Sablenite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sablenite_FR, Constantes.Sablenite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sablenite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sablenite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sablenite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Mawilite_FR))
                    {
                        getTranslateWithUrl(Constantes.Mawilite_URL, dicTranslationMega);

                        dataJson.FR.whenEvolution = Constantes.MegaEvolutionWith_FR + " " + Constantes.Mawilite_FR;
                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Mawilite_FR, Constantes.Mawilite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mawilite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mawilite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Mawilite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Aggronite_FR))
                    {
                        getTranslateWithUrl(Constantes.Aggronite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Aggronite_FR, Constantes.Aggronite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Aggronite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Aggronite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Aggronite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Medichamite_FR))
                    {
                        getTranslateWithUrl(Constantes.Medichamite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Medichamite_FR, Constantes.Medichamite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Medichamite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Medichamite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Medichamite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Manectite_FR))
                    {
                        getTranslateWithUrl(Constantes.Manectite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Manectite_FR, Constantes.Manectite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Manectite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Manectite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Manectite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Sharpedonite_FR))
                    {
                        getTranslateWithUrl(Constantes.Sharpedonite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Sharpedonite_FR, Constantes.Sharpedonite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Cameruptite_FR))
                    {
                        getTranslateWithUrl(Constantes.Cameruptite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Cameruptite_FR, Constantes.Cameruptite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Altarianite_FR))
                    {
                        getTranslateWithUrl(Constantes.Altarianite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Altarianite_FR, Constantes.Altarianite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Altarianite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Altarianite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Altarianite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Banettite_FR))
                    {
                        getTranslateWithUrl(Constantes.Banettite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Banettite_FR, Constantes.Banettite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Banettite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Banettite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Banettite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Absolite_FR))
                    {
                        getTranslateWithUrl(Constantes.Absolite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Absolite_FR, Constantes.Absolite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Absolite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Absolite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Absolite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Glalitite_FR))
                    {
                        getTranslateWithUrl(Constantes.Glalitite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Glalitite_FR, Constantes.Glalitite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Glalitite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Glalitite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Glalitite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Salamencite_FR))
                    {
                        getTranslateWithUrl(Constantes.Salamencite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Salamencite_FR, Constantes.Salamencite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Salamencite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Salamencite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Salamencite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Metagrossite_FR))
                    {
                        getTranslateWithUrl(Constantes.Metagrossite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Metagrossite_FR, Constantes.Metagrossite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Latiasite_FR))
                    {
                        getTranslateWithUrl(Constantes.Latiasite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiasite_FR, Constantes.Latiasite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Latiasite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Latiasite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Latiasite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Latiosite_FR))
                    {
                        getTranslateWithUrl(Constantes.Latiosite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Latiosite_FR, Constantes.Latiosite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Latiosite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Latiosite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Latiosite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Lopunnite_FR))
                    {
                        getTranslateWithUrl(Constantes.Lopunnite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lopunnite_FR, Constantes.Lopunnite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Garchompite_FR))
                    {
                        getTranslateWithUrl(Constantes.Garchompite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Garchompite_FR, Constantes.Garchompite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Garchompite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Garchompite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Garchompite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Lucarionite_FR))
                    {
                        getTranslateWithUrl(Constantes.Lucarionite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Lucarionite_FR, Constantes.Lucarionite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Abomasite_FR))
                    {
                        getTranslateWithUrl(Constantes.Abomasite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Abomasite_FR, Constantes.Abomasite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Abomasite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Abomasite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Abomasite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Galladite_FR))
                    {
                        getTranslateWithUrl(Constantes.Galladite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Galladite_FR, Constantes.Galladite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Galladite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Galladite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Galladite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Audinite_FR))
                    {
                        getTranslateWithUrl(Constantes.Audinite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Audinite_FR, Constantes.Audinite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Audinite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Audinite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Audinite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.whenEvolution.Contains(Constantes.Diancite_FR))
                    {
                        getTranslateWithUrl(Constantes.Diancite_URL, dicTranslationMega);

                        dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.Diancite_FR, Constantes.Diancite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Diancite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Diancite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.Diancite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    #endregion
                    break;
                case Constantes.GigantamaxForm_FR:
                    #region Gigamax Form
                    dataJson.EN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_EN, dataJson.FR.name, dataJson.EN.name);
                    dataJson.ES.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_ES, dataJson.FR.name, dataJson.ES.name);
                    dataJson.IT.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_IT, dataJson.FR.name, dataJson.IT.name);
                    dataJson.DE.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_DE, dataJson.FR.name, dataJson.DE.name);
                    dataJson.RU.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_RU, dataJson.FR.name, dataJson.RU.name);
                    dataJson.CO.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_CO, dataJson.FR.name, dataJson.CO.name);
                    dataJson.CN.whenEvolution = dataJson.FR.whenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_CN, dataJson.FR.name, dataJson.CN.name);
                    dataJson.JP.whenEvolution = dataJson.FR.whenEvolution.translationCleanOther(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_JP, dataJson.FR.name, dataJson.JP.name);
                    #endregion
                    break;
                case Constantes.Exchange_FR:
                    break;
                case Constantes.Reproduction_FR:
                    break;
                default:
                    break;
            }
        }

        private Dictionary<string, string> getTranslateWithUrl(string url, Dictionary<string, string> dicTranslationStone)
        {
            HtmlNode node = CallUrlDynamic(url);

            //EN
            HtmlNode value = node.Descendants("h1").Where(node => node.GetAttributeValue("lang", "").Contains("en")).First();
            dicTranslationStone.Add(Constantes.EN, value.InnerText);
            //ES
            value = node.Descendants("table").Where(node => node.InnerHtml.Contains("Language")).Last().Descendants("tr").Where(node => node.InnerHtml.Contains(Constantes.ES_Lib)).Last();
            dicTranslationStone.Add(Constantes.ES, value.InnerText.Split("\n")[3]);
            //IT
            value = node.Descendants("table").Where(node => node.InnerHtml.Contains("Language")).First().Descendants("tr").Where(node => node.InnerHtml.Contains(Constantes.IT_Lib)).Last();
            dicTranslationStone.Add(Constantes.IT, value.InnerText.Split("\n")[3]);
            //DE
            value = node.Descendants("table").Where(node => node.InnerHtml.Contains("Language")).First().Descendants("tr").Where(node => node.InnerHtml.Contains(Constantes.DE_Lib)).Last();
            dicTranslationStone.Add(Constantes.DE, value.InnerText.Split("\n")[3]);
            //CO
            value = node.Descendants("table").Where(node => node.InnerHtml.Contains("Language")).First().Descendants("tr").Where(node => node.InnerHtml.Contains(Constantes.CO_Lib)).Last();
            dicTranslationStone.Add(Constantes.CO, value.InnerText.Split("\n")[3].Split(" ")[0]);
            //CN
            value = node.Descendants("table").Where(node => node.InnerHtml.Contains("Language")).First().Descendants("tr").Where(node => node.InnerHtml.Contains(Constantes.CN_Lib)).Last();
            dicTranslationStone.Add(Constantes.CN, value.InnerText.Split("\n")[3].Split(" ")[0]);

            //JP
            value = node.Descendants("span").Where(node => node.GetAttributeValue("lang", "").Contains("ja")).First();
            dicTranslationStone.Add(Constantes.JP, value.InnerText);

            return dicTranslationStone;
        }

        private void GetDataByEurope(HtmlDocument htmlDoc, DataInfo dataInfo, string number, int numbPok, List<HtmlNode> values, HtmlNode value, int i = 0, bool many = false, int option = 0, string region = "")
        {
            #region Get Name & Number
            value = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title")).First();

            if (many)
            {
                values = htmlDoc.DocumentNode.Descendants("option").ToList();

                if (values[option].InnerText.Contains(Constantes.Alola))
                {
                    if (region.Equals(Constantes.EN))
                        dataInfo.name = Constantes.regionAlola_EN + " " + value.InnerText.Trim().Split("\n")[0];
                    else if (region.Equals(Constantes.ES))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_ES;
                    else if (region.Equals(Constantes.IT))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_IT;
                    else if (region.Equals(Constantes.DE))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_DE;
                    else if (region.Equals(Constantes.RU))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_RU;
                    dataInfo.displayName = dataInfo.name;
                }
                else if (values[option].InnerText.Contains(Constantes.Galar))
                {
                    if (region.Equals(Constantes.EN))
                        dataInfo.name = Constantes.regionGalar_EN + " " + value.InnerText.Trim().Split("\n")[0];
                    else if (region.Equals(Constantes.ES))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_ES;
                    else if (region.Equals(Constantes.IT))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_IT;
                    else if (region.Equals(Constantes.DE))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_DE;
                    else if (region.Equals(Constantes.RU))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_RU;
                    dataInfo.displayName = dataInfo.name;
                }
                else if (values[option].InnerText.Contains(Constantes.Hisui))
                {
                    if (region.Equals(Constantes.EN))
                        dataInfo.name = Constantes.regionHisui_EN + " " + value.InnerText.Trim().Split("\n")[0];
                    else if (region.Equals(Constantes.ES))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_ES;
                    else if (region.Equals(Constantes.IT))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_IT;
                    else if (region.Equals(Constantes.DE))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_DE;
                    else if (region.Equals(Constantes.RU))
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_RU;
                    dataInfo.displayName = dataInfo.name;
                }
                else
                {
                    if (values[option].InnerText.Contains("Forme de Motisma"))
                    {
                        dataInfo.name = values[option].InnerText.Split(" ")[2];
                        dataInfo.displayName = dataInfo.name;
                    }
                    else if (values[option].InnerText.Contains(value.InnerText.Trim().Split("\n")[0]))
                    {
                        dataInfo.name = values[option].InnerText;
                        dataInfo.displayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else if (value.InnerText.Trim().Split("\n")[0] != values[option].InnerText)
                    {
                        dataInfo.name = value.InnerText.Trim().Split("\n")[0] + " " + values[option].InnerText;
                        dataInfo.displayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else
                    {
                        dataInfo.name = values[option].InnerText;
                        dataInfo.displayName = dataInfo.name;
                    }
                }
            }
            else
            {
                dataInfo.name = value.InnerText.Trim().Split("\n")[0];
                dataInfo.displayName = dataInfo.name;
            }

            dataInfo.name = dataInfo.name.Replace("&#39;", "'").Replace(':', ' ');
            dataInfo.displayName = dataInfo.name;
            Debug.WriteLine(number + ": " + dataInfo.name);
            #endregion

            #region Get Description
            values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
            value = values[option].Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-x")).First();

            dataInfo.descriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

            value = values[option].Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-y")).First();

            dataInfo.descriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
            #endregion

            #region Get Size, Category, Weight, Talent
            values = htmlDoc.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
            .ToList();
            value = values[option];

            values = value.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

            dataInfo.size = values[0].InnerText;
            dataInfo.weight = values[1].InnerText;
            dataInfo.category = values[3].InnerText.Replace("&#39;", "'");
            if (values.Count().Equals(5))
                dataInfo.talent = values[4].InnerText;
            else if (values.Count() > 5)
                dataInfo.talent = values[4].InnerText + "," + values[5].InnerText;

            #endregion

            #region Get Description Talent
            values = value.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

            if (values.Count().Equals(1))
            {
                value = values[0].Descendants("p").First();
                dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
            }
            else if (values.Count() > 1)
            {
                for (i = 0; i < 2; i++)
                {
                    value = values[i].Descendants("p").First();
                    if (i.Equals(0))
                        dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                    else
                        dataInfo.descriptionTalent += ";" + value.InnerText.Replace("&#39;", "'");
                }

            }
            #endregion

            #region Type
            values = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("dtm-type")).First();

            if (value.InnerText.Trim().Split("\n").Length > 4)
                dataInfo.types = value.InnerText.Trim().Split("\n")[4].Trim();
            if (value.InnerText.Trim().Split("\n").Length > 10)
                dataInfo.types += "," + value.InnerText.Trim().Split("\n")[10].Trim();
            #endregion

            #region Faiblesse
            values = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("dtm-weaknesses")).First();
            values = value.Descendants("a").Where(node => node.GetAttributeValue("href", "").Contains("weakness")).ToList();

            i = 0;
            foreach (var item in values)
            {
                string type = item.OuterHtml.Split("\n\t")[1].Trim().Split(">")[1];
                if (i.Equals(0))
                {
                    dataInfo.weakness = type;
                    i++;
                }
                else
                {
                    dataInfo.weakness += "," + type;
                }

            }
            #endregion

            #region Evolution
            values = htmlDoc.DocumentNode.Descendants("h3")
                .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

            i = 0;

            foreach (var item in values)
            {
                string pokName = item.InnerHtml.Trim().Split("\n")[0];
                if (i == 0)
                {
                    dataInfo.evolutions = pokName.Replace("&#39;", "'");
                    i++;
                }
                else
                {
                    dataInfo.evolutions += ',' + pokName.Replace("&#39;", "'");
                }
            }
            #endregion

            #region Next Url
            if (numbPok != Constantes.lastPokemonNumber)
            {
                value = htmlDoc.DocumentNode.Descendants("a")
                    .Where(node => node.GetAttributeValue("class", "").Contains("next")).First();

                dataInfo.nextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
            }
            #endregion
        }

        private void GetDataByAsia(HtmlDocument htmlDoc, DataInfo dataInfo, string number, int numbPok, List<HtmlNode> values, HtmlNode value, int i, bool many, int option, string region)
        {
            if (region.Equals(Constantes.JP))
            {
                #region Get Name & Number
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("ttl__detail")).ToList();

                dataInfo.name = value.InnerText.Trim().Split("\n")[0];
                dataInfo.displayName = value.InnerText.Trim().Split("\n")[0];

                dataInfo.name = dataInfo.name.Replace("&#39;", "'").Replace(':', ' ');
                dataInfo.displayName = dataInfo.displayName.Replace("&#39;", "'").Replace(':', ' ');
                Debug.WriteLine(number + ": " + dataInfo.name);
                #endregion

                #region Get Description
                values = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
                value = values[option].Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-x")).First();

                dataInfo.descriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

                value = values[option].Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-y")).First();

                dataInfo.descriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
                #endregion

                #region Get Talent
                values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
                .ToList();
                value = values[option];

                values = value.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

                dataInfo.size = values[0].InnerText;
                dataInfo.weight = values[1].InnerText;
                dataInfo.category = values[3].InnerText.Replace("&#39;", "'");
                if (values.Count().Equals(5))
                    dataInfo.talent = values[4].InnerText;
                else if (values.Count() > 5)
                    dataInfo.talent = values[4].InnerText + "," + values[5].InnerText;

                #endregion

                #region Get Description Talent
                values = value.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

                if (values.Count().Equals(1))
                {
                    value = values[0].Descendants("p").First();
                    dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                }
                else if (values.Count() > 1)
                {
                    for (i = 0; i < 2; i++)
                    {
                        value = values[i].Descendants("p").First();
                        if (i.Equals(0))
                            dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                        else
                            dataInfo.descriptionTalent += ";" + value.InnerText.Replace("&#39;", "'");
                    }

                }
                #endregion

                #region Evolution
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

                i = 0;

                foreach (var item in values)
                {
                    string pokName = item.InnerHtml.Trim().Split("\n")[0];
                    if (i == 0)
                    {
                        dataInfo.evolutions = pokName.Replace("&#39;", "'");
                        i++;
                    }
                    else
                    {
                        dataInfo.evolutions += ',' + pokName.Replace("&#39;", "'");
                    }
                }
                #endregion

                #region Next Url
                if (numbPok != Constantes.lastPokemonNumber)
                {
                    value = htmlDoc.DocumentNode.Descendants("a")
                        .Where(node => node.GetAttributeValue("class", "").Contains("next")).First();

                    dataInfo.nextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
                }
                #endregion
            }
            else if (region.Equals(Constantes.CO))
            {
                #region Get Name & Number
                value = htmlDoc.DocumentNode.Descendants("h3").First();

                dataInfo.name = value.InnerText.Trim().Split(" ")[2];
                dataInfo.displayName = value.InnerText.Trim().Split(" ")[2];
                Debug.WriteLine(number + ": " + dataInfo.name);
                #endregion

                #region Get Description
                values = htmlDoc.DocumentNode.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "").Contains("descript")).ToList();

                dataInfo.descriptionVx = values[0].InnerText;

                dataInfo.descriptionVy = values[1].InnerText;
                #endregion

                #region Get Talent
                values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("bx-detail"))
                .ToList();
                value = values[0];

                values = value.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("col-4")).ToList();

                dataInfo.size = values[1].InnerText;
                dataInfo.weight = values[4].InnerText;
                dataInfo.category = values[2].InnerText.Replace("&#39;", "'");
                if (values.Count().Equals(5))
                    dataInfo.talent = values[4].InnerText;
                else if (values.Count() > 5)
                    dataInfo.talent = values[4].InnerText + "," + values[5].InnerText;

                #endregion

                #region Get Description Talent
                values = value.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

                if (values.Count().Equals(1))
                {
                    value = values[0].Descendants("p").First();
                    dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                }
                else if (values.Count() > 1)
                {
                    for (i = 0; i < 2; i++)
                    {
                        value = values[i].Descendants("p").First();
                        if (i.Equals(0))
                            dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                        else
                            dataInfo.descriptionTalent += ";" + value.InnerText.Replace("&#39;", "'");
                    }

                }
                #endregion

                #region Evolution
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

                i = 0;

                foreach (var item in values)
                {
                    string pokName = item.InnerHtml.Trim().Split("\n")[0];
                    if (i == 0)
                    {
                        dataInfo.evolutions = pokName.Replace("&#39;", "'");
                        i++;
                    }
                    else
                    {
                        dataInfo.evolutions += ',' + pokName.Replace("&#39;", "'");
                    }
                }
                #endregion

                #region Next Url
                if (numbPok != Constantes.lastPokemonNumber)
                {
                    value = htmlDoc.DocumentNode.Descendants("a")
                        .Where(node => node.GetAttributeValue("class", "").Contains("next")).First();

                    dataInfo.nextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
                }
                #endregion
            }
            else if (region.Equals(Constantes.CN))
            {
                #region Get Name & Number
                values = htmlDoc.DocumentNode.Descendants("h2")
                    .Where(node => node.GetAttributeValue("class", "").Contains("ttl__detail")).ToList();

                dataInfo.name = value.InnerText.Trim().Split("\n")[0];
                dataInfo.displayName = value.InnerText.Trim().Split("\n")[0];

                dataInfo.name = dataInfo.name.Replace("&#39;", "'").Replace(':', ' ');
                dataInfo.displayName = dataInfo.displayName.Replace("&#39;", "'").Replace(':', ' ');
                Debug.WriteLine(number + ": " + dataInfo.name);
                #endregion

                #region Get Description
                values = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
                value = values[option].Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-x")).First();

                dataInfo.descriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

                value = values[option].Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-y")).First();

                dataInfo.descriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
                #endregion

                #region Get Talent
                values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
                .ToList();
                value = values[option];

                values = value.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

                dataInfo.size = values[0].InnerText;
                dataInfo.weight = values[1].InnerText;
                dataInfo.category = values[3].InnerText.Replace("&#39;", "'");
                if (values.Count().Equals(5))
                    dataInfo.talent = values[4].InnerText;
                else if (values.Count() > 5)
                    dataInfo.talent = values[4].InnerText + "," + values[5].InnerText;

                #endregion

                #region Get Description Talent
                values = value.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

                if (values.Count().Equals(1))
                {
                    value = values[0].Descendants("p").First();
                    dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                }
                else if (values.Count() > 1)
                {
                    for (i = 0; i < 2; i++)
                    {
                        value = values[i].Descendants("p").First();
                        if (i.Equals(0))
                            dataInfo.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                        else
                            dataInfo.descriptionTalent += ";" + value.InnerText.Replace("&#39;", "'");
                    }

                }
                #endregion

                #region Evolution
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

                i = 0;

                foreach (var item in values)
                {
                    string pokName = item.InnerHtml.Trim().Split("\n")[0];
                    if (i == 0)
                    {
                        dataInfo.evolutions = pokName.Replace("&#39;", "'");
                        i++;
                    }
                    else
                    {
                        dataInfo.evolutions += ',' + pokName.Replace("&#39;", "'");
                    }
                }
                #endregion

                #region Next Url
                if (numbPok != Constantes.lastPokemonNumber)
                {
                    value = htmlDoc.DocumentNode.Descendants("a")
                        .Where(node => node.GetAttributeValue("class", "").Contains("next")).First();

                    dataInfo.nextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
                }
                #endregion
            }
        }
        #endregion

        #endregion
    }
}
