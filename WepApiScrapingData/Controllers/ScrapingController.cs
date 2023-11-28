using HtmlAgilityPack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class ScrapingController : ControllerBase
    {
        #region Fields
        private readonly Repository<Attaque> _repositoryA;
        private readonly Repository<Talent> _repositoryTL;
        #endregion

        #region Constructors
        public ScrapingController(Repository<Attaque> repositoryA, Repository<Talent> repositoryTL)
        {
            _repositoryA = repositoryA;
            _repositoryTL = repositoryTL;
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
        [Route("{FR}/{EN}/{ES}/{IT}/{DE}/{RU}/{CO}/{CN}/{JP}/{gen}")]
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

        [HttpPut]
        [Route("UpdateNameAttaque")]
        public async Task UpdateNameAttaque()
        {
            IEnumerable<Attaque> attaques = await _repositoryA.GetAll();
            foreach (Attaque item in attaques.ToList())
            {
                StringBuilder url = new StringBuilder();
                url.Append(Constantes.urlBulbapedia);

                switch (item.Name_EN.Replace(" ", "_"))
                {
                    case Constantes.King_s_Shield_EN:
                        url.Append(Constantes.King_s_Shield_EN_New);
                        item.Name_EN = Constantes.King_s_Shield_EN_New;
                        break;
                    case Constantes.Will_EN:
                        url.Append(Constantes.Will_EN_New);
                        item.Name_EN = Constantes.Will_EN_New;
                        break;
                    case Constantes.V_EN:
                        url.Append(Constantes.V_EN_New);
                        item.Name_EN = Constantes.V_EN_New;
                        break;
                    case Constantes.X_EN:
                        url.Append(Constantes.X_EN_New);
                        item.Name_EN = Constantes.X_EN_New;
                        break;
                    case Constantes.U_EN:
                        url.Append(Constantes.U_EN_New);
                        item.Name_EN = Constantes.U_EN_New;
                        break;
                    case Constantes.Forest_s_Curse_EN:
                        url.Append(Constantes.Forest_s_Curse_EN_New);
                        item.Name_EN = Constantes.Forest_s_Curse_EN_New;
                        break;
                    case Constantes.Mud_EN:
                        url.Append(Constantes.Mud_EN_New);
                        item.Name_EN = Constantes.Mud_EN_New;
                        break;
                    case Constantes.Land_s_Wrath_EN:
                        url.Append(Constantes.Land_s_Wrath_EN_New);
                        item.Name_EN = Constantes.Land_s_Wrath_EN_New;
                        break;
                    case Constantes.Topsy_EN:
                        url.Append(Constantes.Topsy_EN_New);
                        item.Name_EN = Constantes.Topsy_EN_New;
                        break;
                    case Constantes.Power_EN:
                        url.Append(Constantes.Power_EN_New);
                        item.Name_EN = Constantes.Power_EN_New;
                        break;
                    case Constantes.Baby_EN:
                        url.Append(Constantes.Baby_EN_New);
                        item.Name_EN = Constantes.Baby_EN_New;
                        break;
                    case Constantes.Nature_s_Madness_EN:
                        url.Append(Constantes.Nature_s_Madness_EN_New);
                        item.Name_EN = Constantes.Nature_s_Madness_EN_New;
                        break;
                    case Constantes.Freeze_EN:
                        url.Append(Constantes.Freeze_EN_New);
                        item.Name_EN = Constantes.Freeze_EN_New;
                        break;
                    case Constantes.Soft_EN:
                        url.Append(Constantes.Soft_EN_New);
                        item.Name_EN = Constantes.Soft_EN_New;
                        break;
                    case Constantes.Lock_EN:
                        url.Append(Constantes.Lock_EN_New);
                        item.Name_EN = Constantes.Lock_EN_New;
                        break;
                    case Constantes.Self_EN:
                        url.Append(Constantes.Self_EN_New);
                        item.Name_EN = Constantes.Self_EN_New;
                        break;
                    case Constantes.Multi_EN:
                        url.Append(Constantes.Multi_EN_New);
                        item.Name_EN = Constantes.Multi_EN_New;
                        break;
                    case Constantes.Double_EN:
                        url.Append(Constantes.Double_EN_New);
                        item.Name_EN = Constantes.Double_EN_New;
                        break;
                    default:
                        url.Append(item.Name_EN.Replace(' ', '_').Replace("’", "%27"));//’
                        break;
                }

                url.Append(Constantes.url_move);

                try
                {
                    string response = HttpClientUtils.CallUrl(url.ToString()).Result;
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(response);

                    HtmlNode jpNode = htmlDoc.DocumentNode.Descendants("span")
                        .First(node => node.GetAttributeValue("class", "").Contains("explain"));

                    item.Name_JP = jpNode.InnerText;

                    HtmlNode table = htmlDoc.DocumentNode.Descendants("table").First(node => node.InnerText.Contains("Language"));

                    List<HtmlNode> trs = table.Descendants("tr").ToList();

                    for (int i = 0; i < trs.Count - 1; i++)
                    {
                        List<HtmlNode> hNode = trs[0].Descendants("tr").ToList()[i].Descendants("td").ToList();

                        if (hNode.Count > 0)
                        {
                            switch (hNode[0].InnerText.Split('\n')[0].Trim())
                            {
                                case "Spain":
                                    item.Name_ES = hNode[1].InnerHtml.Split('<')[0].Split("VI")[0].Split('\n')[0].Trim();
                                    if (item.Name_ES.Equals(""))
                                    {
                                        item.Name_ES = hNode[1].InnerText.Split("VI")[0].Split('\n')[0].Split('\n')[0].Trim();
                                    }
                                    break;
                                case "Spanish":
                                    item.Name_ES = hNode[1].InnerHtml.Split('<')[0].Split("VI")[0].Split('\n')[0].Trim();
                                    if (item.Name_ES.Equals(""))
                                    {
                                        item.Name_ES = hNode[1].InnerText.Split("VI")[0].Split('\n')[0].Trim();
                                    }
                                    break;
                                case "European Spanish":
                                    item.Name_ES = hNode[1].InnerHtml.Split('<')[0].Split("VI")[0].Split('\n')[0].Trim();
                                    if (item.Name_ES.Equals(""))
                                    {
                                        item.Name_ES = hNode[1].InnerText.Split("VI")[0].Split('\n')[0].Trim();
                                    }
                                    break;
                                case "Italian":
                                    item.Name_IT = hNode[1].InnerHtml.Split('<')[0].Split('*')[0].Split('\n')[0].Trim();
                                    if (item.Name_IT.Equals(""))
                                    {
                                        item.Name_IT = hNode[1].InnerText.Split('*')[0].Split('\n')[0].Trim();
                                    }
                                    break;
                                case "German":
                                    item.Name_DE = hNode[1].InnerHtml.Split('<')[0].Split('\n')[0].Trim();
                                    if (item.Name_DE.Equals(""))
                                    {
                                        item.Name_DE = hNode[1].InnerText.Split('\n')[0].Trim();
                                    }
                                    break;
                                case "Russian":
                                    item.Name_RU = hNode[1].InnerHtml.Split('<')[0].Split('\n')[0].Trim();
                                    break;
                                case "Korean":
                                    item.Name_CO = hNode[1].InnerHtml.Split('<')[0].Split('\n')[0].Trim();
                                    break;
                                case "Mandarin":
                                    item.Name_CN = hNode[1].InnerHtml.Split('/')[0].Split('\n')[0].Split("<i>")[0].Trim();
                                    break;
                            }
                        }
                    }

                    HtmlNode tableDescription = htmlDoc.DocumentNode.Descendants("table").First(node => node.InnerText.Contains("Description"));
                    List<HtmlNode> trs_desc = tableDescription.Descendants("tr").ToList()[0].Descendants("tr").ToList();
                    item.Description_EN = trs_desc[trs_desc.Count - 1].Descendants("td").ToList()[1].InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "").Trim();

                    if (item.Description_EN.Contains("This move can’t be used") || item.Description_EN.Contains("This move can't be used."))
                    {
                        item.Description_EN = trs_desc[trs_desc.Count - 2].Descendants("td").ToList()[1].InnerText.Replace("\n", "").Replace("\r", "").Replace("\t", "").Trim();
                    }

                    Debug.WriteLine("Url Trouvé:" + url.ToString());
                }
                catch
                {
                    Debug.WriteLine("Url Non Trouvé:" + url.ToString());
                }

            }

            _repositoryA.UpdateRange(attaques);
        }

        [HttpPut]
        [Route("UpdateTalentNameRUCOCNJP")]
        public async Task UpdateNameTalentRUCOCNJP()
        {
            IEnumerable<Talent> talents = await _repositoryTL.GetAll();

            foreach (Talent item in talents.ToList())
            {
                StringBuilder url = new StringBuilder();
                url.Append(Constantes.urlBulbapedia);
                url.Append(item.Name_EN.Replace(" ", "_"));
                url.Append("_(Ability)");

                try
                {
                    string response = HttpClientUtils.CallUrl(url.ToString()).Result;
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(response);

                    HtmlNode jpNode = htmlDoc.DocumentNode.Descendants("span")
                            .First(node => node.GetAttributeValue("class", "").Contains("explain"));

                    item.Name_JP = jpNode.InnerText;

                    HtmlNode table = htmlDoc.DocumentNode.Descendants("table").First(node => node.InnerText.Contains("Language"));

                    List<HtmlNode> trs = table.Descendants("tr").ToList();

                    for (int i = 0; i < trs.Count - 1; i++)
                    {
                        List<HtmlNode> hNode = trs[0].Descendants("tr").ToList()[i].Descendants("td").ToList();

                        if (hNode.Count > 0)
                        {
                            switch (hNode[0].InnerText.Split('\n')[0].Trim())
                            {
                                case "Russian":
                                    item.Name_RU = hNode[1].InnerHtml.Split('<')[0].Split('\n')[0].Trim();
                                    break;
                                case "Korean":
                                    item.Name_CO = hNode[1].InnerHtml.Split('<')[0].Split('\n')[0].Trim();
                                    break;
                                case "Mandarin":
                                    item.Name_CN = hNode[1].InnerHtml.Split('/')[0].Split('\n')[0].Split("<i>")[0].Trim();
                                    break;
                            }
                        }
                    }

                    Debug.WriteLine("Url Trouvé:" + url.ToString());
                }
                catch
                {
                    Debug.WriteLine("Url Non Trouvé:" + url.ToString());
                }
            }

            _repositoryTL.UpdateRange(talents);
            _repositoryTL.UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
