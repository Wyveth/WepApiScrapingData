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
    public class AttaqueController : ControllerBase
    {
        #region Fields
        private readonly Repository<Attaque> _repository;
        private readonly Repository<Pokemon> _repositoryP;
        #endregion

        #region Constructors
        public AttaqueController(Repository<Attaque> repository, Repository<Pokemon> repositoryP)
        {
            _repository = repository;
            _repositoryP = repositoryP;
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<Attaque>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Attaque> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<Attaque>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
        }

        [HttpGet]
        [Route("GenerateJsonXamarinV1")]
        public async Task GenerateJsonXamarinV1()
        {
            IEnumerable<Attaque> attaques = await _repository.GetAll();

            List<AttaqueMobileJsonV1> attaquesJson = new List<AttaqueMobileJsonV1>();

            foreach (Attaque item in attaques.ToList())
            {
                AttaqueMobileJsonV1 attaqueJson = new AttaqueMobileJsonV1();
                attaqueJson.Name = item.Name_FR;
                attaqueJson.Description = item.Description_FR;
                attaqueJson.Power = item.Power;
                attaqueJson.Precision = item.Precision;
                attaqueJson.PP = item.PP;
                attaqueJson.TypeAttaque = item.typeAttaque?.Name_FR;
                attaqueJson.TypePok = item.typePok?.Name_FR;

                attaquesJson.Add(attaqueJson);

                Debug.WriteLine("Attaque : " + item.Name_FR);
            }

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJsonMobile(attaquesJson);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        [HttpPut]
        [Route("UpdateName")]
        public async Task UpdateName()
        {
            IEnumerable<Attaque> attaques = await _repository.GetAll();
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

            _repository.UpdateRange(attaques);
        }
    }
}
