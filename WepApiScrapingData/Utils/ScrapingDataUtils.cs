﻿using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Domain.ClassJson;

namespace WepApiScrapingData.Utils
{
    public static class ScrapingDataUtils
    {
        #region Json
        public static PokemonJson ParseHtmlToJson(HtmlDocument htmlDoc_FR, HtmlDocument htmlDoc_EN, HtmlDocument htmlDoc_ES, HtmlDocument htmlDoc_IT, HtmlDocument htmlDoc_DE, HtmlDocument htmlDoc_RU, HtmlDocument htmlDoc_JP, HtmlDocument htmlDoc_CO, HtmlDocument htmlDoc_CN, bool many = false, int option = 0)
        {
            List<HtmlNode> values;
            HtmlNode value;
            int i = 0;
            PokemonJson dataJson = new PokemonJson();
            StringBuilder builder;

            #region Get Number
            value = htmlDoc_FR.DocumentNode.Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title"));

            dataJson.Number = value.InnerText.Trim().Split("\n")[1].Trim().Split(".")[1];

            int numbPok = int.Parse(dataJson.Number.ToString());
            #endregion
            #region FR
            #region Get Name & Number
            if (many)
            {
                values = htmlDoc_FR.DocumentNode.Descendants("option").ToList();

                if (values[option].InnerText.Contains(Constantes.Alola))
                {
                    dataJson.FR.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_FR;
                    dataJson.FR.DisplayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_FR;
                }
                else if (values[option].InnerText.Contains(Constantes.Galar))
                {
                    dataJson.FR.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_FR;
                    dataJson.FR.DisplayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_FR;
                }
                else if (values[option].InnerText.Contains(Constantes.Hisui))
                {
                    dataJson.FR.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_FR;
                    dataJson.FR.DisplayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_FR;
                }
                else
                {
                    if (values[option].InnerText.Contains("Forme de Motisma"))
                    {
                        dataJson.FR.Name = values[option].InnerText.Split(" ")[2];
                        dataJson.FR.DisplayName = values[option].InnerText.Split(" ")[2];
                    }
                    else if (values[option].InnerText.Contains(value.InnerText.Trim().Split("\n")[0]))
                    {
                        dataJson.FR.Name = values[option].InnerText;
                        dataJson.FR.DisplayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else if (value.InnerText.Trim().Split("\n")[0] != values[option].InnerText)
                    {
                        dataJson.FR.Name = value.InnerText.Trim().Split("\n")[0] + " " + values[option].InnerText;
                        dataJson.FR.DisplayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else
                    {
                        dataJson.FR.Name = values[option].InnerText;
                        dataJson.FR.DisplayName = values[option].InnerText;
                    }
                }
            }
            else
            {
                dataJson.FR.Name = value.InnerText.Trim().Split("\n")[0];
                dataJson.FR.DisplayName = value.InnerText.Trim().Split("\n")[0];
            }

            dataJson.FR.Name = dataJson.FR.Name.Replace("&#39;", "'").Replace(':', ' ');
            dataJson.FR.DisplayName = dataJson.FR.DisplayName.Replace("&#39;", "'").Replace(':', ' ');
            Debug.WriteLine(dataJson.Number + ": " + dataJson.FR.Name);
            #endregion

            #region Get Description
            values = htmlDoc_FR.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
            value = values[option].Descendants("p")
                .First(node => node.GetAttributeValue("class", "").Contains("version-x"));

            dataJson.FR.DescriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

            value = values[option].Descendants("p")
                .First(node => node.GetAttributeValue("class", "").Contains("version-y"));

            dataJson.FR.DescriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
            #endregion

            #region Get Image Pokémon
            values = htmlDoc_FR.DocumentNode.Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("profile-images")).Descendants("img").ToList();

            dataJson.UrlImg = values[option].Attributes["src"].Value;
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

            dataJson.FR.Size = values[0].InnerText;
            dataJson.FR.Weight = values[1].InnerText;
            dataJson.FR.Category = values[3].InnerText.Replace("&#39;", "'");
            if (values.Count.Equals(5))
                dataJson.FR.Talent = values[4].InnerText;
            else if (values.Count > 5)
                dataJson.FR.Talent = values[4].InnerText + "," + values[5].InnerText;

            #endregion

            #region Get Description Talent
            values = value.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

            if (values.Count.Equals(1))
            {
                value = values[0].Descendants("p").First();
                dataJson.FR.DescriptionTalent = value.InnerText.Replace("&#39;", "'");
            }
            else if (values.Count > 1)
            {
                builder = new();
                for (i = 0; i < 2; i++)
                {
                    value = values[i].Descendants("p").First();
                    if (i.Equals(0))
                        builder.Append(value.InnerText.Replace("&#39;", "'"));
                    else
                        builder.Append(";" + value.InnerText.Replace("&#39;", "'"));
                }
                dataJson.FR.DescriptionTalent = builder.ToString();
            }
            #endregion

            #region Type
            values = htmlDoc_FR.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("dtm-type"));

            if (value.InnerText.Trim().Split("\n").Length > 4)
                dataJson.FR.Types = value.InnerText.Trim().Split("\n")[4].Trim();
            if (value.InnerText.Trim().Split("\n").Length > 10)
                dataJson.FR.Types += "," + value.InnerText.Trim().Split("\n")[10].Trim();
            #endregion

            #region Faiblesse
            values = htmlDoc_FR.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("dtm-weaknesses"));
            values = value.Descendants("a").Where(node => node.GetAttributeValue("href", "").Contains("weakness")).ToList();

            i = 0;
            builder = new();
            foreach (var item in values)
            {
                string type = item.OuterHtml.Split("\n\t")[1].Trim().Split(">")[1];

                if (i.Equals(0))
                    builder.Append(type);
                else
                    builder.Append("," + type);

                i++;
            }
            dataJson.FR.Weakness = builder.ToString();
            #endregion

            #region Evolution
            values = htmlDoc_FR.DocumentNode.Descendants("h3")
                .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

            i = 0;
            builder = new();
            foreach (var item in values)
            {
                string pokName = item.InnerHtml.Trim().Split("\n")[0];
                if (i == 0)
                    builder.Append(pokName.Replace("&#39;", "'"));
                else
                    builder.Append("," + pokName.Replace("&#39;", "'"));

                i++;
            }
            dataJson.FR.Evolutions = builder.ToString();
            #endregion

            #region Stats + WhenEvolution
            GetStats(Constantes.urlStatsPB, dataJson, option);
            #endregion

            #region Type Evolution
            if (dataJson.FR.Name.Contains(Constantes.MegaEvolution) && !dataJson.FR.Name.Contains(Constantes.Meganium))
                dataJson.TypeEvolution = Constantes.MegaLevel;
            else if (dataJson.FR.Name.Contains(Constantes.GigaEvolution))
                dataJson.TypeEvolution = Constantes.GigaLevel;
            else if (dataJson.FR.Name.Contains(Constantes.Alola))
                dataJson.TypeEvolution = Constantes.Alola;
            else if (dataJson.FR.Name.Contains(Constantes.Galar))
                dataJson.TypeEvolution = Constantes.Galar;
            else if (dataJson.FR.Name.Contains(Constantes.Femelle))
                dataJson.TypeEvolution = Constantes.VarianteSexe;
            else if (dataJson.FR.Name.Contains(Constantes.Hisui))
                dataJson.TypeEvolution = Constantes.Hisui;
            else if (option != 0)
                dataJson.TypeEvolution = Constantes.Variant;
            else
                dataJson.TypeEvolution = Constantes.NormalEvolution;
            #endregion

            #region Generation
            if ((numbPok >= 899 && numbPok <= 905) || dataJson.FR.Name.Contains(Constantes.Hisui))
                dataJson.Generation = 0;
            else if (numbPok <= 151)
                dataJson.Generation = 1;
            else if (numbPok <= 251)
                dataJson.Generation = 2;
            else if (numbPok <= 386)
                dataJson.Generation = 3;
            else if (numbPok <= 494)
                dataJson.Generation = 4;
            else if (numbPok <= 649)
                dataJson.Generation = 5;
            else if (numbPok <= 721)
                dataJson.Generation = 6;
            else if (numbPok <= 809)
                dataJson.Generation = 7;
            else if (numbPok <= 898)
                dataJson.Generation = 8;
            #endregion

            #region Next Url
            if (numbPok != Constantes.lastPokemonNumber)
            {
                value = htmlDoc_FR.DocumentNode.Descendants("a")
                    .First(node => node.GetAttributeValue("class", "").Contains("next"));

                dataJson.FR.NextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
            }
            #endregion
            #endregion

            #region EN
            GetDataByEurope(htmlDoc_EN, dataJson.EN, dataJson.Number, numbPok, many, option, Constantes.EN);
            #endregion

            #region ES
            GetDataByEurope(htmlDoc_ES, dataJson.ES, dataJson.Number, numbPok, many, option, Constantes.ES);
            #endregion

            #region IT
            GetDataByEurope(htmlDoc_IT, dataJson.IT, dataJson.Number, numbPok, many, option, Constantes.IT);
            #endregion

            #region DE
            GetDataByEurope(htmlDoc_DE, dataJson.DE, dataJson.Number, numbPok, many, option, Constantes.DE);
            #endregion

            #region RU
            //GetDataByEurope(htmlDoc_RU, dataJson.RU, dataJson.Number, numbPok, many, option, Constantes.RU);
            #endregion

            #region JP
            //GetDataByAsia(htmlDoc_JP, dataJson.JP, dataJson.Number, numbPok, values, value, i, many, option, Constantes.JP);
            #endregion

            #region CO
            //GetDataByAsia(htmlDoc_CO, dataJson.CO, dataJson.Number, numbPok, values, value, i, many, option, Constantes.CO);
            #endregion

            #region CN
            //GetDataByAsia(htmlDoc_CN, dataJson.CN, dataJson.Number, numbPok, values, value, i, many, option, Constantes.CN);
            #endregion

            return dataJson;
        }

        public static void RecursiveGetDataJsonWithUrl(string url_FR, string url_EN, string url_ES, string url_IT, string url_DE, string url_RU, string url_CO, string url_CN, string url_JP, List<PokemonJson> dataJsons)
        {
            #region Europe
            string response_FR = HttpClientUtils.CallUrl(url_FR).Result;
            HtmlDocument htmlDoc_FR = new HtmlDocument();
            htmlDoc_FR.LoadHtml(response_FR);

            string response_EN = HttpClientUtils.CallUrl(url_EN).Result;
            HtmlDocument htmlDoc_EN = new HtmlDocument();
            htmlDoc_EN.LoadHtml(response_EN);

            string response_ES = HttpClientUtils.CallUrl(url_ES).Result;
            HtmlDocument htmlDoc_ES = new HtmlDocument();
            htmlDoc_ES.LoadHtml(response_ES);

            string response_IT = HttpClientUtils.CallUrl(url_IT).Result;
            HtmlDocument htmlDoc_IT = new HtmlDocument();
            htmlDoc_IT.LoadHtml(response_IT);

            string response_DE = HttpClientUtils.CallUrl(url_DE).Result;
            HtmlDocument htmlDoc_DE = new HtmlDocument();
            htmlDoc_DE.LoadHtml(response_DE);

            //string response_RU = HttpClientUtils.CallUrl(url_RU).Result;
            HtmlDocument htmlDoc_RU = new HtmlDocument();
            //htmlDoc_RU.LoadHtml(response_RU);
            #endregion

            #region Asia
            //string response_CO = HttpClientUtils.CallUrl(url_CO).Result;
            HtmlDocument htmlDoc_CO = new HtmlDocument();
            //htmlDoc_CO.LoadHtml(response_CO);

            //string response_CN = HttpClientUtils.CallUrl(url_CN).Result;
            HtmlDocument htmlDoc_CN = new HtmlDocument();
            //htmlDoc_CN.LoadHtml(response_CN);

            //string response_JP = HttpClientUtils.CallUrlDynamic(url_JP).Result;
            HtmlDocument htmlDoc_JP = new HtmlDocument();
            //htmlDoc_JP.LoadHtml(response_JP);
            #endregion

            HtmlNode value = htmlDoc_FR.DocumentNode.Descendants("div")
               .First(node => node.GetAttributeValue("class", "").Contains("profile-images"));
            int countImg = value.Descendants("img").Count();

            PokemonJson dataJson = new PokemonJson();
            if (countImg.Equals(1))
            {
                dataJson = ParseHtmlToJson(htmlDoc_FR, htmlDoc_EN, htmlDoc_ES, htmlDoc_IT, htmlDoc_DE, htmlDoc_RU, htmlDoc_JP, htmlDoc_CO, htmlDoc_CN);
                dataJsons.Add(dataJson);
                GetTranslationWhenEvolution(dataJson);
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
                    GetTranslationWhenEvolution(dataJson);
                }
            }

            if (!string.IsNullOrEmpty(dataJson.FR.NextUrl))
                RecursiveGetDataJsonWithUrl(dataJson.FR.NextUrl, dataJson.EN.NextUrl, dataJson.ES.NextUrl, dataJson.IT.NextUrl, dataJson.DE.NextUrl, dataJson.RU.NextUrl, dataJson.CO.NextUrl, dataJson.CN.NextUrl, dataJson.JP.NextUrl, dataJsons);
        }

        public static void WriteToJson(List<PokemonJson> dataJsons)
        {
            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            System.IO.File.WriteAllText("PokeScrap.json", json);
        }
        #endregion

        #region Get Url Img/Sprite
        public static void GetUrlSprite(string spitesPkmDB, PokemonJson dataJson)
        {
            string response = HttpClientUtils.CallUrl(spitesPkmDB).Result;
            dataJson.UrlSprite = Constantes.urlPokepedia + GetUrlMini(response, dataJson.Number, dataJson.FR.Name);
            if (dataJson.UrlSprite == Constantes.urlPokepedia)
                Debug.WriteLine(dataJson.FR.Name);
        }

        public static string GetUrlMini(string html, string number, string name)
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
        public static void GetStats(string urlStats, PokemonJson dataJson, int option)
        {
            string name = GetNamePokebip(dataJson, option);
            string response = HttpClientUtils.CallUrl(urlStats + name).Result;
            GetStatsPokemon(response, dataJson);
        }

        public static string GetNamePokebip(PokemonJson dataJson, int option)
        {
            string nameSite = "";

            if (dataJson.FR.Name.Contains("♀") || dataJson.FR.Name.Contains("♂"))
            {
                if (dataJson.FR.Name.Contains("♀"))
                {
                    nameSite = dataJson.FR.Name.Split('♀')[0] + "-f";
                }
                else
                {
                    nameSite = dataJson.FR.Name.Split('♂')[0] + "-m";
                }
            }
            else if (dataJson.FR.Name.Contains(Constantes.Alola))
            {
                nameSite = dataJson.FR.DisplayName.Split(' ')[0] + "-" + Constantes.Alola;
            }
            else if (dataJson.FR.Name.Contains(Constantes.Galar))
            {
                if (dataJson.FR.Name.Contains("M. Mime"))
                    nameSite = dataJson.FR.DisplayName.Replace(". ", "-").Split(' ')[0];
                else
                    nameSite = dataJson.FR.DisplayName.Split(' ')[0] + "-" + Constantes.Galar;
            }
            else if (dataJson.FR.Name.Contains(Constantes.Hisui))
            {
                nameSite = dataJson.FR.DisplayName.Split(' ')[0];
            }
            else if (dataJson.FR.Name.Contains("Mâle") || dataJson.FR.Name.Contains("Femelle"))
            {
                if (dataJson.FR.Name.Contains("Déflaisan") || dataJson.FR.Name.Contains("Viskuse") || dataJson.FR.Name.Contains("Moyade") || dataJson.FR.Name.Contains("Mistigrix") || dataJson.FR.Name.Contains("Wimessir"))
                {
                    if (dataJson.FR.Name.Contains("Mâle"))
                        nameSite = dataJson.FR.DisplayName;
                    else
                        nameSite = dataJson.FR.DisplayName + "-femelle";
                }
                else if (dataJson.FR.Name.Contains("Némélios"))
                {
                    nameSite = dataJson.FR.DisplayName;
                }
            }
            else if (dataJson.FR.Name.Contains(' '))
            {
                if (dataJson.FR.Name.Contains(". "))
                {
                    nameSite = dataJson.FR.DisplayName.Replace(". ", "-");
                }
                else if (dataJson.FR.Name.Contains("Primo"))
                {
                    nameSite = "Primo-" + dataJson.FR.DisplayName;
                }
                else if (dataJson.FR.Name.Contains("Forme")
                    || dataJson.FR.Name.Contains("Cape")
                    || dataJson.FR.Name.Contains("Temps")
                    || dataJson.FR.Name.Contains("Mer")
                    || dataJson.FR.Name.Contains("Aspect")
                    || dataJson.FR.Name.Contains("Motif")
                    || dataJson.FR.Name.Contains("Style")
                    || dataJson.FR.Name.Contains("Mode")
                    || dataJson.FR.Name.Contains("Héros")
                    || dataJson.FR.Name.Contains("Forme Normal")
                    || dataJson.FR.Name.Contains("Enchaîné")
                    || dataJson.FR.Name.Contains("Cavalier")
                    || dataJson.FR.Name.Contains("Necrozma"))
                {
                    if (option != 0)
                    {
                        if (dataJson.FR.Name.Contains("Dialga") || dataJson.FR.Name.Contains("Palkia"))
                        {
                            nameSite = dataJson.FR.DisplayName;
                        }
                        else if (dataJson.FR.Name.Contains("Prismillon"))
                        {
                            nameSite = dataJson.FR.Name.Split(' ')[0] + "-" + dataJson.FR.Name.Split(' ')[1] + "-" + dataJson.FR.Name.Split(' ')[2];
                        }
                        else if (dataJson.FR.Name.Contains("Parfaite"))
                        {
                            nameSite = dataJson.FR.DisplayName + "-Parfait";
                        }
                        else if (dataJson.FR.Name.Contains("Necrozma"))
                        {
                            if (dataJson.FR.Name.Contains("Crinière"))
                                nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Split(' ')[1] + "-" + dataJson.FR.Name.Split(' ')[3];
                            else if (dataJson.FR.Name.Contains("Ailes"))
                                nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Split(' ')[1] + "-" + dataJson.FR.Name.Split(' ')[3].Substring(2);
                            else
                                nameSite = dataJson.FR.DisplayName;
                        }
                        else if (dataJson.FR.Name.Contains("Shifours"))
                        {
                            if (dataJson.FR.Name.Contains("Gigamax"))
                            {
                                nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Replace(" (", " ").Replace(")", "").Split(' ')[3] + "-" + dataJson.FR.Name.Replace(" (", " ").Replace(")", "").Split(' ')[4] + "-" + dataJson.FR.Name.Replace(" (", " ").Replace(")", "").Split(' ')[1];
                            }
                            else
                            {
                                nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Split(' ')[2] + "-" + dataJson.FR.Name.Split(' ')[3];
                            }
                        }
                        else if (dataJson.FR.Name.Contains("Sylveroy"))
                        {
                            nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Replace('’', ' ').Split(' ')[1] + "-" + dataJson.FR.Name.Replace("’", " ").Split(' ')[3];
                        }
                        else
                        {
                            nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Split(' ')[2];
                        }
                    }
                    else
                    {
                        if (dataJson.FR.Name.Contains("Salarsen"))
                            nameSite = dataJson.FR.DisplayName + "-" + dataJson.FR.Name.Split(' ')[2];
                        else
                            nameSite = dataJson.FR.DisplayName;
                    }
                }
                else
                {
                    if (!dataJson.FR.Name.Contains("Mime Jr."))
                        nameSite = dataJson.FR.Name.Replace(' ', '-');
                    else
                        nameSite = dataJson.FR.Name.Split(" ")[0] + "-" + dataJson.FR.Name.Split(" ")[1].Split('.')[0];
                }
            }
            else
            {
                if (dataJson.FR.Name.Contains("Arceus"))
                {
                    nameSite = "arceus-normal";
                }
                else if (dataJson.FR.Name.Contains("Spectreval"))
                {
                    nameSite = "sprectreval";
                }
                else
                {
                    nameSite = dataJson.FR.Name;
                }
            }

            return nameSite;
        }

        public static void GetStatsPokemon(string html, PokemonJson dataJson)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var stats = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("progress-bar")).ToList();

            if (stats.Count >= 6)
            {
                dataJson.StatPv = int.Parse(stats[0].InnerText);
                dataJson.StatAttaque = int.Parse(stats[1].InnerText);
                dataJson.StatDefense = int.Parse(stats[2].InnerText);
                dataJson.StatAttaqueSpe = int.Parse(stats[3].InnerText);
                dataJson.StatDefenseSpe = int.Parse(stats[4].InnerText);
                dataJson.StatVitesse = int.Parse(stats[5].InnerText);
                dataJson.StatTotal = dataJson.StatPv + dataJson.StatAttaque + dataJson.StatDefense + dataJson.StatAttaqueSpe + dataJson.StatDefenseSpe + dataJson.StatVitesse;
            }
            else
                Debug.WriteLine("Stats Erreur: " + dataJson.Number + ": " + dataJson.FR.Name);

            var detailsEvol = htmlDoc.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("list-group-item")).ToList();

            var filter = "";
            if (dataJson.FR.Name.Contains(Constantes.Alola) || dataJson.FR.Name.Contains(Constantes.Galar) || dataJson.FR.Name.Contains(Constantes.Hisui))
                filter = dataJson.FR.DisplayName;
            else
                filter = dataJson.FR.Name;

            HtmlNode? evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter));

            if (evol != null)
            {
                var i = evol.ChildNodes[5].InnerText.Trim().Split("\n").Length;

                if (i.Equals(1))
                {
                    dataJson.FR.WhenEvolution = dataJson.EN.WhenEvolution = dataJson.ES.WhenEvolution = dataJson.IT.WhenEvolution = dataJson.DE.WhenEvolution = Constantes.Base_EU;
                    dataJson.RU.WhenEvolution = Constantes.Base_RU;
                }
                else
                {
                    if (evol.ChildNodes[5].InnerText.Trim().Split("\n")[2].Contains("Reproduction"))
                        evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter) && !m.ChildNodes[5].InnerText.Trim().Contains("Reproduction"));

                    if (evol == null)
                        evol = detailsEvol.Find(m => m.ChildNodes[5].InnerText.Trim().Contains(filter));

                    if (evol != null)
                        dataJson.FR.WhenEvolution = evol.ChildNodes[5].InnerText.Trim().Split("\n")[2].Trim().Replace("&#039;", "'");

                    if (dataJson.FR.WhenEvolution.Contains("."))
                        dataJson.FR.WhenEvolution = dataJson.FR.WhenEvolution.Split('.')[dataJson.FR.WhenEvolution.Split('.').Length - 1];
                }

                Debug.WriteLine("Evolution Essai: " + dataJson.Number + ": " + dataJson.FR.Name + " - " + dataJson.FR.WhenEvolution);
            }
            else
                Debug.WriteLine("Evolution Erreur: " + dataJson.Number + ": " + dataJson.FR.Name);
        }

        public static void GetTranslationWhenEvolution(PokemonJson dataJson)
        {
            if (dataJson.FR.WhenEvolution.Contains(Constantes.Level_FR))
                TranslationUtils.Translate(dataJson, Constantes.Level_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.Stone_FR))
                TranslationUtils.Translate(dataJson, Constantes.Stone_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.MegaEvolutionWith_FR))
                TranslationUtils.Translate(dataJson, Constantes.MegaEvolutionWith_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.GigantamaxForm_FR))
                TranslationUtils.Translate(dataJson, Constantes.GigantamaxForm_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.Exchange_FR))
                TranslationUtils.Translate(dataJson, Constantes.Exchange_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.Reproduction_FR))
                TranslationUtils.Translate(dataJson, Constantes.Reproduction_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.LvlUpWith_FR))
                TranslationUtils.Translate(dataJson, Constantes.LvlUpWith_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.LvlUpLearn_FR))
                TranslationUtils.Translate(dataJson, Constantes.LvlUpLearn_FR);
            else if (dataJson.FR.WhenEvolution.Contains(Constantes.LvlUpWH_FR))
                TranslationUtils.Translate(dataJson, Constantes.LvlUpWH_FR);
            else
                TranslationUtils.Translate(dataJson, "");
        }

        public static void GetDataByEurope(HtmlDocument htmlDoc, DataInfoJson dataInfo, string number, int numbPok, bool many = false, int option = 0, string region = "")
        {
            List<HtmlNode> values;
            HtmlNode value;
            int i = 0;
            StringBuilder builder;

            #region Get Name & Number
            value = htmlDoc.DocumentNode.Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title"));

            if (many)
            {
                values = htmlDoc.DocumentNode.Descendants("option").ToList();

                if (values[option].InnerText.Contains(Constantes.Alola))
                {
                    if (region.Equals(Constantes.EN))
                        dataInfo.Name = Constantes.regionAlola_EN + " " + value.InnerText.Trim().Split("\n")[0];
                    else if (region.Equals(Constantes.ES))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_ES;
                    else if (region.Equals(Constantes.IT))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_IT;
                    else if (region.Equals(Constantes.DE))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_DE;
                    else if (region.Equals(Constantes.RU))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_RU;
                    dataInfo.DisplayName = dataInfo.Name;
                }
                else if (values[option].InnerText.Contains(Constantes.Galar))
                {
                    if (region.Equals(Constantes.EN))
                        dataInfo.Name = Constantes.regionGalar_EN + " " + value.InnerText.Trim().Split("\n")[0];
                    else if (region.Equals(Constantes.ES))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_ES;
                    else if (region.Equals(Constantes.IT))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_IT;
                    else if (region.Equals(Constantes.DE))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_DE;
                    else if (region.Equals(Constantes.RU))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_RU;
                    dataInfo.DisplayName = dataInfo.Name;
                }
                else if (values[option].InnerText.Contains(Constantes.Hisui))
                {
                    if (region.Equals(Constantes.EN))
                        dataInfo.Name = Constantes.regionHisui_EN + " " + value.InnerText.Trim().Split("\n")[0];
                    else if (region.Equals(Constantes.ES))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_ES;
                    else if (region.Equals(Constantes.IT))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_IT;
                    else if (region.Equals(Constantes.DE))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_DE;
                    else if (region.Equals(Constantes.RU))
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_RU;
                    dataInfo.DisplayName = dataInfo.Name;
                }
                else
                {
                    if (values[option].InnerText.Contains("Forme de Motisma"))
                    {
                        dataInfo.Name = values[option].InnerText.Split(" ")[2];
                        dataInfo.DisplayName = dataInfo.Name;
                    }
                    else if (values[option].InnerText.Contains(value.InnerText.Trim().Split("\n")[0]))
                    {
                        dataInfo.Name = values[option].InnerText;
                        dataInfo.DisplayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else if (value.InnerText.Trim().Split("\n")[0] != values[option].InnerText)
                    {
                        dataInfo.Name = value.InnerText.Trim().Split("\n")[0] + " " + values[option].InnerText;
                        dataInfo.DisplayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else
                    {
                        dataInfo.Name = values[option].InnerText;
                        dataInfo.DisplayName = dataInfo.Name;
                    }
                }
            }
            else
            {
                dataInfo.Name = value.InnerText.Trim().Split("\n")[0];
                dataInfo.DisplayName = value.InnerText.Trim().Split("\n")[0];
            }

            dataInfo.Name = dataInfo.Name.Replace("&#39;", "'").Replace(':', ' ');
            dataInfo.DisplayName = dataInfo.DisplayName.Replace("&#39;", "'").Replace(':', ' ');
            Debug.WriteLine(number + ": " + dataInfo.Name);
            #endregion

            #region Get Description
            values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
            value = values[option].Descendants("p")
                .First(node => node.GetAttributeValue("class", "").Contains("version-x"));

            dataInfo.DescriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

            value = values[option].Descendants("p")
                .First(node => node.GetAttributeValue("class", "").Contains("version-y"));

            dataInfo.DescriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
            #endregion

            #region Get Size, Category, Weight, Talent
            values = htmlDoc.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
            .ToList();
            value = values[option];

            values = value.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

            dataInfo.Size = values[0].InnerText;
            dataInfo.Weight = values[1].InnerText;
            dataInfo.Category = values[3].InnerText.Replace("&#39;", "'");
            if (values.Count.Equals(5))
                dataInfo.Talent = values[4].InnerText;
            else if (values.Count > 5)
                dataInfo.Talent = values[4].InnerText + "," + values[5].InnerText;

            #endregion

            #region Get Description Talent
            values = value.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

            if (values.Count.Equals(1))
            {
                value = values[0].Descendants("p").First();
                dataInfo.DescriptionTalent = value.InnerText.Replace("&#39;", "'");
            }
            else if (values.Count > 1)
            {
                builder = new();
                for (i = 0; i < 2; i++)
                {
                    value = values[i].Descendants("p").First();

                    if (i.Equals(0))
                        builder.Append(value.InnerText.Replace("&#39;", "'"));
                    else
                        builder.Append(";" + value.InnerText.Replace("&#39;", "'"));
                }
                dataInfo.DescriptionTalent = builder.ToString();
            }
            #endregion

            #region Type
            values = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("dtm-type"));

            if (value.InnerText.Trim().Split("\n").Length > 4)
                dataInfo.Types = value.InnerText.Trim().Split("\n")[4].Trim();
            if (value.InnerText.Trim().Split("\n").Length > 10)
                dataInfo.Types += "," + value.InnerText.Trim().Split("\n")[10].Trim();
            #endregion

            #region Faiblesse
            values = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-attributes")).ToList();
            value = values[option].Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("dtm-weaknesses"));
            values = value.Descendants("a").Where(node => node.GetAttributeValue("href", "").Contains("weakness")).ToList();

            i = 0;
            builder = new();
            foreach (var item in values)
            {
                string type = item.OuterHtml.Split("\n\t")[1].Trim().Split(">")[1];

                if (i.Equals(0))
                    builder.Append(type);
                else
                    builder.Append("," + type);

                i++;
            }
            dataInfo.Weakness = builder.ToString();
            #endregion

            #region Evolution
            values = htmlDoc.DocumentNode.Descendants("h3")
                .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

            i = 0;
            builder = new();
            foreach (var item in values)
            {
                string pokName = item.InnerHtml.Trim().Split("\n")[0];

                if (i == 0)
                    builder.Append(pokName.Replace("&#39;", "'"));
                else
                    builder.Append("," + pokName.Replace("&#39;", "'"));

                i++;
            }
            dataInfo.Evolutions = builder.ToString();
            #endregion

            #region Next Url
            if (numbPok != Constantes.lastPokemonNumber)
            {
                value = htmlDoc.DocumentNode.Descendants("a")
                    .First(node => node.GetAttributeValue("class", "").Contains("next"));

                dataInfo.NextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
            }
            #endregion
        }

        public static void GetDataByAsia(HtmlDocument htmlDoc, DataInfoJson dataInfo, string number, int numbPok, bool many, int option, string region)
        {
            List<HtmlNode> values;
            HtmlNode value;
            int i = 0;
            StringBuilder builder;

            if (region.Equals(Constantes.JP))
            {
                #region Get Name & Number
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("ttl__detail")).ToList();

                //dataInfo.Name = value.InnerText.Trim().Split("\n")[0];
                //dataInfo.DisplayName = value.InnerText.Trim().Split("\n")[0];

                dataInfo.Name = dataInfo.Name.Replace("&#39;", "'").Replace(':', ' ');
                dataInfo.DisplayName = dataInfo.DisplayName.Replace("&#39;", "'").Replace(':', ' ');
                Debug.WriteLine(number + ": " + dataInfo.Name);
                #endregion

                #region Get Description
                values = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
                value = values[option].Descendants("p")
                    .First(node => node.GetAttributeValue("class", "").Contains("version-x"));

                dataInfo.DescriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

                value = values[option].Descendants("p")
                    .First(node => node.GetAttributeValue("class", "").Contains("version-y"));

                dataInfo.DescriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
                #endregion

                #region Get Talent
                values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
                .ToList();
                value = values[option];

                values = value.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

                dataInfo.Size = values[0].InnerText;
                dataInfo.Weight = values[1].InnerText;
                dataInfo.Category = values[3].InnerText.Replace("&#39;", "'");
                if (values.Count.Equals(5))
                    dataInfo.Talent = values[4].InnerText;
                else if (values.Count > 5)
                    dataInfo.Talent = values[4].InnerText + "," + values[5].InnerText;

                #endregion

                #region Get Description Talent
                values = value.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

                if (values.Count.Equals(1))
                {
                    value = values[0].Descendants("p").First();
                    dataInfo.DescriptionTalent = value.InnerText.Replace("&#39;", "'");
                }
                else if (values.Count > 1)
                {
                    builder = new();
                    for (i = 0; i < 2; i++)
                    {
                        value = values[i].Descendants("p").First();

                        if (i.Equals(0))
                            builder.Append(value.InnerText.Replace("&#39;", "'"));
                        else
                            builder.Append(";" + value.InnerText.Replace("&#39;", "'"));
                    }
                    dataInfo.DescriptionTalent = builder.ToString();
                }
                #endregion

                #region Evolution
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

                i = 0;
                builder = new();
                foreach (var item in values)
                {
                    string pokName = item.InnerHtml.Trim().Split("\n")[0];

                    if (i == 0)
                        builder.Append(pokName.Replace("&#39;", "'"));
                    else
                        builder.Append("," + pokName.Replace("&#39;", "'"));

                    i++;
                }
                dataInfo.Evolutions = builder.ToString();
                #endregion

                #region Next Url
                if (numbPok != Constantes.lastPokemonNumber)
                {
                    value = htmlDoc.DocumentNode.Descendants("a")
                        .First(node => node.GetAttributeValue("class", "").Contains("next"));

                    dataInfo.NextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
                }
                #endregion
            }
            else if (region.Equals(Constantes.CO))
            {
                #region Get Name & Number
                value = htmlDoc.DocumentNode.Descendants("h3").First();

                dataInfo.Name = value.InnerText.Trim().Split(" ")[2];
                dataInfo.DisplayName = value.InnerText.Trim().Split(" ")[2];
                Debug.WriteLine(number + ": " + dataInfo.Name);
                #endregion

                #region Get Description
                values = htmlDoc.DocumentNode.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "").Contains("descript")).ToList();

                dataInfo.DescriptionVx = values[0].InnerText;

                dataInfo.DescriptionVy = values[1].InnerText;
                #endregion

                #region Get Talent
                values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("bx-detail"))
                .ToList();
                value = values[0];

                values = value.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("col-4")).ToList();

                dataInfo.Size = values[1].InnerText;
                dataInfo.Weight = values[4].InnerText;
                dataInfo.Category = values[2].InnerText.Replace("&#39;", "'");
                if (values.Count.Equals(5))
                    dataInfo.Talent = values[4].InnerText;
                else if (values.Count > 5)
                    dataInfo.Talent = values[4].InnerText + "," + values[5].InnerText;

                #endregion

                #region Get Description Talent
                values = value.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

                if (values.Count.Equals(1))
                {
                    value = values[0].Descendants("p").First();
                    dataInfo.DescriptionTalent = value.InnerText.Replace("&#39;", "'");
                }
                else if (values.Count > 1)
                {
                    builder = new();
                    for (i = 0; i < 2; i++)
                    {
                        value = values[i].Descendants("p").First();
                        if (i.Equals(0))
                            builder.Append(value.InnerText.Replace("&#39;", "'"));
                        else
                            builder.Append(";" + value.InnerText.Replace("&#39;", "'"));
                    }
                    dataInfo.DescriptionTalent = builder.ToString();
                }
                #endregion

                #region Evolution
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

                i = 0;
                builder = new();
                foreach (var item in values)
                {
                    string pokName = item.InnerHtml.Trim().Split("\n")[0];

                    if (i == 0)
                        builder.Append(pokName.Replace("&#39;", "'"));
                    else
                        builder.Append("," + pokName.Replace("&#39;", "'"));

                    i++;
                }
                dataInfo.Evolutions = builder.ToString();
                #endregion

                #region Next Url
                if (numbPok != Constantes.lastPokemonNumber)
                {
                    value = htmlDoc.DocumentNode.Descendants("a")
                        .First(node => node.GetAttributeValue("class", "").Contains("next"));

                    dataInfo.NextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
                }
                #endregion
            }
            else if (region.Equals(Constantes.CN))
            {
                #region Get Name & Number
                values = htmlDoc.DocumentNode.Descendants("h2")
                    .Where(node => node.GetAttributeValue("class", "").Contains("ttl__detail")).ToList();

                //dataInfo.Name = value.InnerText.Trim().Split("\n")[0];
                //dataInfo.DisplayName = value.InnerText.Trim().Split("\n")[0];

                dataInfo.Name = dataInfo.Name.Replace("&#39;", "'").Replace(':', ' ');
                dataInfo.DisplayName = dataInfo.DisplayName.Replace("&#39;", "'").Replace(':', ' ');
                Debug.WriteLine(number + ": " + dataInfo.Name);
                #endregion

                #region Get Description
                values = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("version-descriptions")).ToList();
                value = values[option].Descendants("p")
                    .First(node => node.GetAttributeValue("class", "").Contains("version-x"));

                dataInfo.DescriptionVx = value.InnerText.Trim().Replace("&#39;", "'");

                value = values[option].Descendants("p")
                    .First(node => node.GetAttributeValue("class", "").Contains("version-y"));

                dataInfo.DescriptionVy = value.InnerText.Trim().Replace("&#39;", "'");
                #endregion

                #region Get Talent
                values = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info color-bg color-lightblue match"))
                .ToList();
                value = values[option];

                values = value.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "").Contains("attribute-value")).ToList();

                dataInfo.Size = values[0].InnerText;
                dataInfo.Weight = values[1].InnerText;
                dataInfo.Category = values[3].InnerText.Replace("&#39;", "'");
                if (values.Count.Equals(5))
                    dataInfo.Talent = values[4].InnerText;
                else if (values.Count > 5)
                    dataInfo.Talent = values[4].InnerText + "," + values[5].InnerText;

                #endregion

                #region Get Description Talent
                values = value.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("pokemon-ability-info-detail")).ToList();

                if (values.Count.Equals(1))
                {
                    value = values[0].Descendants("p").First();
                    dataInfo.DescriptionTalent = value.InnerText.Replace("&#39;", "'");
                }
                else if (values.Count > 1)
                {
                    builder = new();
                    for (i = 0; i < 2; i++)
                    {
                        value = values[i].Descendants("p").First();
                        if (i.Equals(0))
                            builder.Append(value.InnerText.Replace("&#39;", "'"));
                        else
                            builder.Append(";" + value.InnerText.Replace("&#39;", "'"));
                    }
                    dataInfo.DescriptionTalent = builder.ToString();
                }
                #endregion

                #region Evolution
                values = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("match")).ToList();

                i = 0;
                builder = new();
                foreach (var item in values)
                {
                    string pokName = item.InnerHtml.Trim().Split("\n")[0];

                    if (i == 0)
                        builder.Append(pokName.Replace("&#39;", "'"));
                    else
                        builder.Append("," + pokName.Replace("&#39;", "'"));

                    i++;
                }
                dataInfo.Evolutions = builder.ToString();
                #endregion

                #region Next Url
                if (numbPok != Constantes.lastPokemonNumber)
                {
                    value = htmlDoc.DocumentNode.Descendants("a")
                        .First(node => node.GetAttributeValue("class", "").Contains("next"));

                    dataInfo.NextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
                }
                #endregion
            }
        }
        #endregion
    }
}
