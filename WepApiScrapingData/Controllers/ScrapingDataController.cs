using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WebApiScrapingData.Domain;
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

            string url = Constantes.urlTestCanarticho;
            RecursiveGetDataJsonWithUrl(url, dataJsons);

            WriteToJson(dataJsons);
        }

        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = await client.GetAsync(fullUrl);
            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region Json
        private DataJson ParseHtmlToJson(HtmlDocument htmlDoc, bool many = false, int option = 0)
        {
            List<HtmlNode> values;
            HtmlNode value;
            int i = 0;
            DataJson dataJson = new DataJson();

            #region Get Name & Number
            value = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title")).First();

            dataJson.number = value.InnerText.Trim().Split("\n")[1].Trim().Split(".")[1];
            int numbPok = int.Parse(dataJson.number.ToString());

            if (many)
            {
                values = htmlDoc.DocumentNode.Descendants("option").ToList();

                if (values[option].InnerText.Contains(Constantes.Alola))
                {
                    dataJson.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola;
                    dataJson.displayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola;
                }
                else if (values[option].InnerText.Contains(Constantes.Galar))
                {
                    dataJson.name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar;
                    dataJson.displayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar;
                }
                else
                {
                    if (values[option].InnerText.Contains("Forme de Motisma"))
                    {
                        dataJson.name = values[option].InnerText.Split(" ")[2];
                        dataJson.displayName = values[option].InnerText.Split(" ")[2];
                    }
                    else if (values[option].InnerText.Contains(value.InnerText.Trim().Split("\n")[0]))
                    {
                        dataJson.name = values[option].InnerText;
                        dataJson.displayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else if (value.InnerText.Trim().Split("\n")[0] != values[option].InnerText)
                    {
                        dataJson.name = value.InnerText.Trim().Split("\n")[0] + " " + values[option].InnerText;
                        dataJson.displayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else
                    {
                        dataJson.name = values[option].InnerText;
                        dataJson.displayName = values[option].InnerText;
                    }
                }
            }
            else
            {
                dataJson.name = value.InnerText.Trim().Split("\n")[0];
                dataJson.displayName = value.InnerText.Trim().Split("\n")[0];
            }

            dataJson.name = dataJson.name.Replace("&#39;", "'").Replace(':', ' ');
            dataJson.displayName = dataJson.displayName.Replace("&#39;", "'").Replace(':', ' ');
            Debug.WriteLine(dataJson.number + ": " + dataJson.name);
            #endregion

            #region Get Description
            values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
            value = values[option].Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-x")).First();

            dataJson.descriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

            value = values[option].Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-y")).First();

            dataJson.descriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
            #endregion

            #region Get Image Pokémon
            values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("profile-images")).First().Descendants("img").ToList();

            dataJson.urlImg = values[option].Attributes["src"].Value;
            #endregion

            #region Get Sprite Pokemon
            GetUrlSprite(Constantes.urlAllSprites, dataJson);
            #endregion

            #region Get Size, Category, Weight, Talent
            values = htmlDoc.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
            .ToList();
            value = values[option];

            values = value.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

            dataJson.size = values[0].InnerText;
            dataJson.weight = values[1].InnerText;
            dataJson.category = values[3].InnerText.Replace("&#39;", "'");
            if (values.Count().Equals(5))
                dataJson.talent = values[4].InnerText;
            else if (values.Count() > 5)
                dataJson.talent = values[4].InnerText + "," + values[5].InnerText;

            #endregion

            #region Get Description Talent
            values = value.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

            if (values.Count().Equals(1))
            {
                value = values[0].Descendants("p").First();
                dataJson.descriptionTalent = value.InnerText.Replace("&#39;", "'");
            }
            else if (values.Count() > 1)
            {
                for (i = 0; i < 2; i++)
                {
                    value = values[i].Descendants("p").First();
                    if (i.Equals(0))
                        dataJson.descriptionTalent = value.InnerText.Replace("&#39;", "'");
                    else
                        dataJson.descriptionTalent += ";" + value.InnerText.Replace("&#39;", "'");
                }

            }
            #endregion

            #region Stats + WhenEvolution
            GetStats(Constantes.urlStatsPB, dataJson, option);
            #endregion

            #region Type
            values = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("dtm-type")).First();

            if (value.InnerText.Trim().Split("\n").Length > 4)
                dataJson.types = value.InnerText.Trim().Split("\n")[4].Trim();
            if (value.InnerText.Trim().Split("\n").Length > 10)
                dataJson.types += "," + value.InnerText.Trim().Split("\n")[10].Trim();
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
                    dataJson.weakness = type;
                    i++;
                }
                else
                {
                    dataJson.weakness += "," + type;
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
                    dataJson.evolutions = pokName.Replace("&#39;", "'");
                    i++;
                }
                else
                {
                    dataJson.evolutions += ',' + pokName.Replace("&#39;", "'");
                }
            }
            #endregion

            #region Type Evolution
            if (dataJson.name.Contains(Constantes.MegaEvolution))
                dataJson.typeEvolution = Constantes.MegaLevel;
            else if (dataJson.name.Contains(Constantes.GigaEvolution))
                dataJson.typeEvolution = Constantes.GigaLevel;
            else if (dataJson.name.Contains(Constantes.Alola))
                dataJson.typeEvolution = Constantes.Alola;
            else if (dataJson.name.Contains(Constantes.Galar))
                dataJson.typeEvolution = Constantes.Galar;
            else if (dataJson.name.Contains(Constantes.Femelle))
                dataJson.typeEvolution = Constantes.VarianteSexe;
            else if (dataJson.name.Contains(Constantes.Hisui))
                dataJson.typeEvolution = Constantes.Hisui;
            else if (option != 0)
                dataJson.typeEvolution = Constantes.Variant;
            else
                dataJson.typeEvolution = Constantes.NormalEvolution;
            #endregion

            #region Generation

            if ((numbPok >= 899 && numbPok <= 905) || dataJson.name.Contains(Constantes.Hisui))
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
                value = htmlDoc.DocumentNode.Descendants("a")
                    .Where(node => node.GetAttributeValue("class", "").Contains("next")).First();

                dataJson.nextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
            }
            #endregion

            return dataJson;
        }

        private List<DataJson> RecursiveGetDataJsonWithUrl(string url, List<DataJson> dataJsons)
        {
            string response = CallUrl(url).Result;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            HtmlNode value = htmlDoc.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "").Contains("profile-images")).First();
            int countImg = value.Descendants("img").Count();

            DataJson dataJson = new DataJson();
            if (countImg.Equals(1))
            {
                dataJson = ParseHtmlToJson(htmlDoc);
                dataJsons.Add(dataJson);
            }
            else
            {
                for (int i = 0; i < countImg; i++)
                {
                    dataJson = ParseHtmlToJson(htmlDoc, true, i);
                    dataJsons.Add(dataJson);
                }
            }

            if (!string.IsNullOrEmpty(dataJson.nextUrl))
                RecursiveGetDataJsonWithUrl(dataJson.nextUrl, dataJsons);

            return dataJsons;
        }

        private void WriteToJson(List<DataJson> dataJsons)
        {
            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            System.IO.File.WriteAllText("PokeScrap.json", json);
        }

        #region Get Url Img/Sprite
        private void GetUrlSprite(string spitesPkmDB, DataJson dataJson)
        {
            string response = CallUrl(spitesPkmDB).Result;
            dataJson.urlSprite = Constantes.urlPokepedia + GetUrlMini(response, dataJson.number, dataJson.name);
            if (dataJson.urlSprite == Constantes.urlPokepedia)
                Debug.WriteLine(dataJson.name);
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

        #region Private Methods
        #region Get Data Stat & When Evolution
        private string GetNamePokebip(DataJson dataJson, int option)
        {
            string nameSite = "";

            if (dataJson.name.Contains("♀") || dataJson.name.Contains("♂"))
            {
                if (dataJson.name.Contains("♀"))
                {
                    nameSite = dataJson.name.Split('♀')[0] + "-f";
                }
                else
                {
                    nameSite = dataJson.name.Split('♂')[0] + "-m";
                }
            }
            else if (dataJson.name.Contains(Constantes.Alola))
            {
                nameSite = dataJson.displayName.Split(' ')[0] + "-" + Constantes.Alola;
            }
            else if (dataJson.name.Contains(Constantes.Galar))
            {
                if (dataJson.name.Contains("M. Mime"))
                    nameSite = dataJson.displayName.Replace(". ", "-").Split(' ')[0];
                else
                    nameSite = dataJson.displayName.Split(' ')[0] + "-" + Constantes.Galar;
            }
            else if (dataJson.name.Contains(Constantes.Hisui))
            {
                nameSite = dataJson.displayName.Split(' ')[0];
            }
            else if (dataJson.name.Contains("Mâle") || dataJson.name.Contains("Femelle"))
            {
                if (dataJson.name.Contains("Déflaisan") || dataJson.name.Contains("Viskuse") || dataJson.name.Contains("Moyade") || dataJson.name.Contains("Mistigrix") || dataJson.name.Contains("Wimessir"))
                {
                    if (dataJson.name.Contains("Mâle"))
                        nameSite = dataJson.displayName;
                    else
                        nameSite = dataJson.displayName + "-femelle";
                }
                else if (dataJson.name.Contains("Némélios"))
                {
                    nameSite = dataJson.displayName;
                }
            }
            else if (dataJson.name.Contains(' '))
            {
                if (dataJson.name.Contains(". "))
                {
                    nameSite = dataJson.displayName.Replace(". ", "-");
                }
                else if (dataJson.name.Contains("Primo"))
                {
                    nameSite = "Primo-" + dataJson.displayName;
                }
                else if (dataJson.name.Contains("Forme")
                    || dataJson.name.Contains("Cape")
                    || dataJson.name.Contains("Temps")
                    || dataJson.name.Contains("Mer")
                    || dataJson.name.Contains("Aspect")
                    || dataJson.name.Contains("Motif")
                    || dataJson.name.Contains("Style")
                    || dataJson.name.Contains("Mode")
                    || dataJson.name.Contains("Héros")
                    || dataJson.name.Contains("Forme Normal")
                    || dataJson.name.Contains("Enchaîné")
                    || dataJson.name.Contains("Cavalier")
                    || dataJson.name.Contains("Necrozma"))
                {
                    if (option != 0)
                    {
                        if (dataJson.name.Contains("Dialga") || dataJson.name.Contains("Palkia"))
                        {
                            nameSite = dataJson.displayName;
                        }
                        else if (dataJson.name.Contains("Prismillon"))
                        {
                            nameSite = dataJson.name.Split(' ')[0] + "-" + dataJson.name.Split(' ')[1] + "-" + dataJson.name.Split(' ')[2];
                        }
                        else if (dataJson.name.Contains("Parfaite"))
                        {
                            nameSite = dataJson.displayName + "-Parfait";
                        }
                        else if (dataJson.name.Contains("Necrozma"))
                        {
                            if (dataJson.name.Contains("Crinière"))
                                nameSite = dataJson.displayName + "-" + dataJson.name.Split(' ')[1] + "-" + dataJson.name.Split(' ')[3];
                            else if (dataJson.name.Contains("Ailes"))
                                nameSite = dataJson.displayName + "-" + dataJson.name.Split(' ')[1] + "-" + dataJson.name.Replace('\'', ' ').Split(' ')[4];
                            else
                                nameSite = dataJson.displayName;
                        }
                        else if (dataJson.name.Contains("Shifours"))
                        {
                            if (dataJson.name.Contains("Gigamax"))
                            {
                                nameSite = dataJson.displayName + "-" + dataJson.name.Replace(" (", " ").Replace(")", "").Split(' ')[3] + "-" + dataJson.name.Replace(" (", " ").Replace(")", "").Split(' ')[4] + "-" + dataJson.name.Replace(" (", " ").Replace(")", "").Split(' ')[1];
                            }
                            else
                            {
                                nameSite = dataJson.displayName + "-" + dataJson.name.Split(' ')[2] + "-" + dataJson.name.Split(' ')[3];
                            }
                        }
                        else if (dataJson.name.Contains("Sylveroy"))
                        {
                            nameSite = dataJson.displayName + "-" + dataJson.name.Replace('’', ' ').Split(' ')[1] + "-" + dataJson.name.Replace("’", " ").Split(' ')[3];
                        }
                        else
                        {
                            nameSite = dataJson.displayName + "-" + dataJson.name.Split(' ')[2];
                        }
                    }
                    else
                    {
                        if (dataJson.name.Contains("Salarsen"))
                            nameSite = dataJson.displayName + "-" + dataJson.name.Split(' ')[2];
                        else
                            nameSite = dataJson.displayName;
                    }
                }
                else
                {
                    nameSite = dataJson.name.Replace(' ', '-');
                }
            }
            else
            {
                if (dataJson.name.Contains("Arceus"))
                {
                    nameSite = "arceus-normal";
                }
                else if (dataJson.name.Contains("Spectreval"))
                {
                    nameSite = "sprectreval";
                }
                else
                {
                    nameSite = dataJson.name;
                }
            }

            return nameSite;
        }
        private void GetStats(string urlStats, DataJson dataJson, int option)
        {
            string name = GetNamePokebip(dataJson, option);
            string response = CallUrl(urlStats + name).Result;
            GetStatsPokemon(response, dataJson);
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
                Debug.WriteLine("Stats Erreur: " + dataJson.number + ": " + dataJson.name);

            var detailsEvol = htmlDoc.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("list-group-item")).ToList();

            var filter = "";
            if (dataJson.name.Contains(Constantes.Alola) || dataJson.name.Contains(Constantes.Galar) || dataJson.name.Contains(Constantes.Hisui))
                filter = dataJson.displayName;
            else
                filter = dataJson.name;

            HtmlNode? evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter));

            if (evol != null)
            {
                var i = evol.ChildNodes[5].InnerText.Trim().Split("\n").Length;

                if (i.Equals(1))
                    dataJson.whenEvolution = "Base";
                else
                    dataJson.whenEvolution = evol.ChildNodes[5].InnerText.Trim().Split("\n")[2].Trim();

                Debug.WriteLine("Evolution Essai: " + dataJson.number + ": " + dataJson.name + " - " + dataJson.whenEvolution);
            }
            else
                Debug.WriteLine("Evolution Erreur: " + dataJson.number + ": " + dataJson.name);
        }
        #endregion
        #endregion
        #endregion
    }
}
