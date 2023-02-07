using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;

namespace WepApiScrapingData.Utils
{
    public static class ScrapingDataUtils
    {
        #region Json
        public static PokemonJson ParseHtmlToJson(HtmlDocument htmlDoc_FR, HtmlDocument htmlDoc_EN, HtmlDocument htmlDoc_ES, HtmlDocument htmlDoc_IT, HtmlDocument htmlDoc_DE, HtmlDocument htmlDoc_RU, HtmlDocument htmlDoc_CO, HtmlDocument htmlDoc_CN, HtmlDocument htmlDoc_JP, bool many = false, int option = 0)
        {
            List<HtmlNode> values;
            HtmlNode value;
            int i = 0;
            PokemonJson dataJson = new PokemonJson();
            StringBuilder builder;

            #region Get Number
            value = htmlDoc_FR.DocumentNode.Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title"));

            dataJson.Number = value.InnerText.Trim().Split("\n")[1].Trim().Split(";")[1];

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

            #region Stats + TalentHidden + Attack  + WhenEvolution
            GetStats(Constantes.urlStatsPB, dataJson, option);
            #endregion

            #region Type Evolution
            if (dataJson.FR.Name.Contains(Constantes.MegaEvolution) && (!dataJson.FR.Name.Contains(Constantes.Meganium) && !dataJson.FR.Name.Contains(Constantes.Megapagos)))
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
            else if (dataJson.FR.Name.Contains(Constantes.Paldea))
                dataJson.TypeEvolution = Constantes.Paldea;
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
            else if (numbPok <= 905)
                dataJson.Generation = 0;
            else if (numbPok <= 1008)
                dataJson.Generation = 9;
            #endregion

            #region Get Sprite/Sound Pokemon
            if (dataJson.Generation != 9)
                GetUrlSprite(Constantes.urlAllSprites, dataJson);
            else
                GetUrlSpriteGen9(Constantes.urlAllSpritesOld, dataJson);
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

        public static PokemonPokeBipJson ParseHtmlPBToJson(HtmlDocument htmlDoc_FR, bool many = false, int option = 0)
        {
            List<HtmlNode> values;
            HtmlNode value;
            PokemonPokeBipJson dataJson = new PokemonPokeBipJson();

            #region Get Number
            value = htmlDoc_FR.DocumentNode.Descendants("div")
                .First(node => node.GetAttributeValue("class", "").Contains("pokedex-pokemon-pagination-title"));

            dataJson.Number = value.InnerText.Trim().Split("\n")[1].Trim().Split(";")[1];

            int numbPok = int.Parse(dataJson.Number.ToString());
            #endregion
            
            #region Get Name & Number
            if (many)
            {
                values = htmlDoc_FR.DocumentNode.Descendants("option").ToList();

                if (values[option].InnerText.Contains(Constantes.Alola))
                {
                    dataJson.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_FR;
                    dataJson.DisplayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionAlola_FR;
                }
                else if (values[option].InnerText.Contains(Constantes.Galar))
                {
                    dataJson.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_FR;
                    dataJson.DisplayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionGalar_FR;
                }
                else if (values[option].InnerText.Contains(Constantes.Hisui))
                {
                    dataJson.Name = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_FR;
                    dataJson.DisplayName = value.InnerText.Trim().Split("\n")[0] + " " + Constantes.regionHisui_FR;
                }
                else
                {
                    if (values[option].InnerText.Contains("Forme de Motisma"))
                    {
                        dataJson.Name = values[option].InnerText.Split(" ")[2];
                        dataJson.DisplayName = values[option].InnerText.Split(" ")[2];
                    }
                    else if (values[option].InnerText.Contains(value.InnerText.Trim().Split("\n")[0]))
                    {
                        dataJson.Name = values[option].InnerText;
                        dataJson.DisplayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else if (value.InnerText.Trim().Split("\n")[0] != values[option].InnerText)
                    {
                        dataJson.Name = value.InnerText.Trim().Split("\n")[0] + " " + values[option].InnerText;
                        dataJson.DisplayName = value.InnerText.Trim().Split("\n")[0];
                    }
                    else
                    {
                        dataJson.Name = values[option].InnerText;
                        dataJson.DisplayName = values[option].InnerText;
                    }
                }
            }
            else
            {
                dataJson.Name = value.InnerText.Trim().Split("\n")[0];
                dataJson.DisplayName = value.InnerText.Trim().Split("\n")[0];
            }

            dataJson.Name = dataJson.Name.Replace("&#39;", "'").Replace(':', ' ');
            dataJson.DisplayName = dataJson.DisplayName.Replace("&#39;", "'").Replace(':', ' ');
            Debug.WriteLine(dataJson.Number + ": " + dataJson.Name);
            #endregion

            #region TalentHidden + Attack
            GetDataPB(Constantes.urlStatsPB, dataJson, option);
            #endregion

            #region Next Url
            if (numbPok != Constantes.lastPokemonNumber)
            {
                value = htmlDoc_FR.DocumentNode.Descendants("a")
                    .First(node => node.GetAttributeValue("class", "").Contains("next"));

                dataJson.NextUrl = Constantes.urlPath + value.OuterHtml.Split('"')[1];
            }
            Console.WriteLine("Pokemon:" + dataJson.Number + ": " + dataJson.Name);
            #endregion

            return dataJson;
        }

        public static void RecursiveGetDataJsonWithUrl(string url_FR, string url_EN, string url_ES, string url_IT, string url_DE, string url_RU, string url_CO, string url_CN, string url_JP, List<PokemonJson> dataJsons, bool limit = false, int gen = -1)
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
                dataJson = ParseHtmlToJson(htmlDoc_FR, htmlDoc_EN, htmlDoc_ES, htmlDoc_IT, htmlDoc_DE, htmlDoc_RU, htmlDoc_CO, htmlDoc_CN, htmlDoc_JP);
                dataJsons.Add(dataJson);
                GetTranslationWhenEvolution(dataJson);
            }
            else
            {
                for (int i = 0; i < countImg; i++)
                {
                    dataJson = ParseHtmlToJson(htmlDoc_FR, htmlDoc_EN, htmlDoc_ES, htmlDoc_IT, htmlDoc_DE, htmlDoc_RU, htmlDoc_CO, htmlDoc_CN, htmlDoc_JP, true, i);

                    if (i > 0)
                    {
                        //A faire pour les sites asiatiques
                    }

                    dataJsons.Add(dataJson);
                    GetTranslationWhenEvolution(dataJson);

                    if (dataJson.FR.Name.Contains(Constantes.Prismillon) && i.Equals(countImg - 1))
                    {

                        #region Archipel
                        PokemonJson pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Archipelago_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Archipelago_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Archipelago_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Archipelago_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Archipelago_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Archipelago_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Archipelago_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Archipelago_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Archipelago_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Archipelago_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Archipelago_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Banquise
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Polar_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Polar_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Polar_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Polar_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Polar_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Polar_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Polar_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Polar_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Polar_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Polar_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Polar_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Blizzard
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_IceSnow_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_IceSnow_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_IceSnow_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_IceSnow_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_IceSnow_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_IceSnow_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_IceSnow_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_IceSnow_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_IceSnow_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_IceSnow_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_IceSnow_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Cyclone
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Monsoon_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Monsoon_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Monsoon_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Monsoon_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Monsoon_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Monsoon_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Monsoon_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Monsoon_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Monsoon_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Monsoon_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Monsoon_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Glace
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Tundra_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Tundra_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Tundra_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Tundra_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Tundra_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Tundra_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Tundra_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Tundra_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Tundra_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Tundra_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Tundra_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Jungle
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Jungle_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Jungle_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Jungle_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Jungle_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Jungle_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Jungle_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Jungle_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Jungle_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Jungle_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Jungle_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Jungle_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Mangrove
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Savanna_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Savanna_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Savanna_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Savanna_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Savanna_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Savanna_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Savanna_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Savanna_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Savanna_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Savanna_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Savanna_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Métropole
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Modern_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Modern_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Modern_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Modern_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Modern_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Modern_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Modern_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Modern_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Modern_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Modern_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Modern_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Sable
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Sandstorm_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Sandstorm_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Sandstorm_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Sandstorm_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Sandstorm_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Sandstorm_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Sandstorm_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Sandstorm_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Sandstorm_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Sandstorm_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Sandstorm_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Soleil Levant
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Ocean_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Ocean_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Ocean_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Ocean_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Ocean_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Ocean_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Ocean_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Ocean_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Ocean_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Ocean_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Ocean_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Zénith
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Sun_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Sun_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Sun_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Sun_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Sun_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Sun_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Sun_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Sun_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Sun_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Sun_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Sun_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Fantaisie
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_Fancy_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_Fancy_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_Fancy_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_Fancy_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_Fancy_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_Fancy_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_Fancy_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_Fancy_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_Fancy_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_Fancy_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_Fancy_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Pokéball
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Vivillon_PokeBall_Pattern_FR;
                        pokemonJson.EN.Name = Constantes.Vivillon_PokeBall_Pattern_EN;
                        pokemonJson.ES.Name = Constantes.Vivillon_PokeBall_Pattern_ES;
                        pokemonJson.IT.Name = Constantes.Vivillon_PokeBall_Pattern_IT;
                        pokemonJson.DE.Name = Constantes.Vivillon_PokeBall_Pattern_DE;
                        pokemonJson.RU.Name = Constantes.Vivillon_PokeBall_Pattern_RU;
                        pokemonJson.CO.Name = Constantes.Vivillon_PokeBall_Pattern_CO;
                        pokemonJson.CN.Name = Constantes.Vivillon_PokeBall_Pattern_CN;
                        pokemonJson.JP.Name = Constantes.Vivillon_PokeBall_Pattern_JP;
                        pokemonJson.UrlImg = Constantes.Vivillon_PokeBall_Pattern_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Vivillon_PokeBall_Pattern_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion
                    }
                    else if (dataJson.FR.Name.Contains(Constantes.Couafarel) && i.Equals(countImg - 1))
                    {
                        #region Demoiselle
                        PokemonJson pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Furfrou_Debutante_Trim_FR;
                        pokemonJson.EN.Name = Constantes.Furfrou_Debutante_Trim_EN;
                        pokemonJson.ES.Name = Constantes.Furfrou_Debutante_Trim_ES;
                        pokemonJson.IT.Name = Constantes.Furfrou_Debutante_Trim_IT;
                        pokemonJson.DE.Name = Constantes.Furfrou_Debutante_Trim_DE;
                        pokemonJson.RU.Name = Constantes.Furfrou_Debutante_Trim_RU;
                        pokemonJson.CO.Name = Constantes.Furfrou_Debutante_Trim_CO;
                        pokemonJson.CN.Name = Constantes.Furfrou_Debutante_Trim_CN;
                        pokemonJson.JP.Name = Constantes.Furfrou_Debutante_Trim_JP;
                        pokemonJson.UrlImg = Constantes.Furfrou_Debutante_Trim_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Furfrou_Debutante_Trim_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Madame
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Furfrou_Matron_Trim_FR;
                        pokemonJson.EN.Name = Constantes.Furfrou_Matron_Trim_EN;
                        pokemonJson.ES.Name = Constantes.Furfrou_Matron_Trim_ES;
                        pokemonJson.IT.Name = Constantes.Furfrou_Matron_Trim_IT;
                        pokemonJson.DE.Name = Constantes.Furfrou_Matron_Trim_DE;
                        pokemonJson.RU.Name = Constantes.Furfrou_Matron_Trim_RU;
                        pokemonJson.CO.Name = Constantes.Furfrou_Matron_Trim_CO;
                        pokemonJson.CN.Name = Constantes.Furfrou_Matron_Trim_CN;
                        pokemonJson.JP.Name = Constantes.Furfrou_Matron_Trim_JP;
                        pokemonJson.UrlImg = Constantes.Furfrou_Matron_Trim_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Furfrou_Matron_Trim_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Monsieur
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Furfrou_Dandy_Trim_FR;
                        pokemonJson.EN.Name = Constantes.Furfrou_Dandy_Trim_EN;
                        pokemonJson.ES.Name = Constantes.Furfrou_Dandy_Trim_ES;
                        pokemonJson.IT.Name = Constantes.Furfrou_Dandy_Trim_IT;
                        pokemonJson.DE.Name = Constantes.Furfrou_Dandy_Trim_DE;
                        pokemonJson.RU.Name = Constantes.Furfrou_Dandy_Trim_RU;
                        pokemonJson.CO.Name = Constantes.Furfrou_Dandy_Trim_CO;
                        pokemonJson.CN.Name = Constantes.Furfrou_Dandy_Trim_CN;
                        pokemonJson.JP.Name = Constantes.Furfrou_Dandy_Trim_JP;
                        pokemonJson.UrlImg = Constantes.Furfrou_Dandy_Trim_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Furfrou_Dandy_Trim_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Reine
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Furfrou_Queen_Trim_FR;
                        pokemonJson.EN.Name = Constantes.Furfrou_Queen_Trim_EN;
                        pokemonJson.ES.Name = Constantes.Furfrou_Queen_Trim_ES;
                        pokemonJson.IT.Name = Constantes.Furfrou_Queen_Trim_IT;
                        pokemonJson.DE.Name = Constantes.Furfrou_Queen_Trim_DE;
                        pokemonJson.RU.Name = Constantes.Furfrou_Queen_Trim_RU;
                        pokemonJson.CO.Name = Constantes.Furfrou_Queen_Trim_CO;
                        pokemonJson.CN.Name = Constantes.Furfrou_Queen_Trim_CN;
                        pokemonJson.JP.Name = Constantes.Furfrou_Queen_Trim_JP;
                        pokemonJson.UrlImg = Constantes.Furfrou_Queen_Trim_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Furfrou_Queen_Trim_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Kabuki
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Furfrou_Kabuki_Trim_FR;
                        pokemonJson.EN.Name = Constantes.Furfrou_Kabuki_Trim_EN;
                        pokemonJson.ES.Name = Constantes.Furfrou_Kabuki_Trim_ES;
                        pokemonJson.IT.Name = Constantes.Furfrou_Kabuki_Trim_IT;
                        pokemonJson.DE.Name = Constantes.Furfrou_Kabuki_Trim_DE;
                        pokemonJson.RU.Name = Constantes.Furfrou_Kabuki_Trim_RU;
                        pokemonJson.CO.Name = Constantes.Furfrou_Kabuki_Trim_CO;
                        pokemonJson.CN.Name = Constantes.Furfrou_Kabuki_Trim_CN;
                        pokemonJson.JP.Name = Constantes.Furfrou_Kabuki_Trim_JP;
                        pokemonJson.UrlImg = Constantes.Furfrou_Kabuki_Trim_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Furfrou_Kabuki_Trim_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Pharaon
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.FR.Name = Constantes.Furfrou_Pharaoh_Trim_FR;
                        pokemonJson.EN.Name = Constantes.Furfrou_Pharaoh_Trim_EN;
                        pokemonJson.ES.Name = Constantes.Furfrou_Pharaoh_Trim_ES;
                        pokemonJson.IT.Name = Constantes.Furfrou_Pharaoh_Trim_IT;
                        pokemonJson.DE.Name = Constantes.Furfrou_Pharaoh_Trim_DE;
                        pokemonJson.RU.Name = Constantes.Furfrou_Pharaoh_Trim_RU;
                        pokemonJson.CO.Name = Constantes.Furfrou_Pharaoh_Trim_CO;
                        pokemonJson.CN.Name = Constantes.Furfrou_Pharaoh_Trim_CN;
                        pokemonJson.JP.Name = Constantes.Furfrou_Pharaoh_Trim_JP;
                        pokemonJson.UrlImg = Constantes.Furfrou_Pharaoh_Trim_UrlImg;
                        pokemonJson.UrlSprite = Constantes.Furfrou_Pharaoh_Trim_UrlSprite;
                        dataJsons.Add(pokemonJson);
                        #endregion
                    }
                }
            }

            int numPok = int.Parse(dataJson.Number);
            bool keepGoing = true;

            if ((gen == 0 && !(numPok >= 899 && numPok < 905))
                || (gen == 1 && !(numPok >= 1 && numPok < 151))
                || (gen == 2 && !(numPok >= 152 && numPok < 251))
                || (gen == 3 && !(numPok >= 252 && numPok < 386))
                || (gen == 4 && !(numPok >= 387 && numPok < 494))
                || (gen == 5 && !(numPok >= 495 && numPok < 649))
                || (gen == 6 && !(numPok >= 650 && numPok < 721))
                || (gen == 7 && !(numPok >= 722 && numPok < 809))
                || (gen == 8 && !(numPok >= 810 && numPok < 898))
                || (gen == 9 && !(numPok >= 906 && numPok < 1008)))
            {
                keepGoing = false;
            }

            if (!string.IsNullOrEmpty(dataJson.FR.NextUrl) && !limit && keepGoing)
                RecursiveGetDataJsonWithUrl(dataJson.FR.NextUrl, dataJson.EN.NextUrl, dataJson.ES.NextUrl, dataJson.IT.NextUrl, dataJson.DE.NextUrl, dataJson.RU.NextUrl, dataJson.CO.NextUrl, dataJson.CN.NextUrl, dataJson.JP.NextUrl, dataJsons);
        }

        public static void RecursiveGetDataPBJsonWithUrl(string url_FR, List<PokemonPokeBipJson> dataJsons)
        {
            #region Europe
            string response_FR = HttpClientUtils.CallUrl(url_FR).Result;
            HtmlDocument htmlDoc_FR = new HtmlDocument();
            htmlDoc_FR.LoadHtml(response_FR);
            #endregion

            HtmlNode value = htmlDoc_FR.DocumentNode.Descendants("div")
               .First(node => node.GetAttributeValue("class", "").Contains("profile-images"));
            int countImg = value.Descendants("img").Count();

            PokemonPokeBipJson dataJson = new PokemonPokeBipJson();
            if (countImg.Equals(1))
            {
                dataJson = ParseHtmlPBToJson(htmlDoc_FR);
                dataJsons.Add(dataJson);
            }
            else
            {
                for (int i = 0; i < countImg; i++)
                {
                    dataJson = ParseHtmlPBToJson(htmlDoc_FR, true, i);

                    dataJsons.Add(dataJson);

                    if (dataJson.Name.Contains(Constantes.Prismillon) && i.Equals(countImg - 1))
                    {
                        #region Archipel
                        PokemonPokeBipJson pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Archipelago_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Banquise
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Polar_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Blizzard
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_IceSnow_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Cyclone
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Monsoon_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Glace
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Tundra_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Jungle
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Jungle_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Mangrove
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Savanna_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Métropole
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Modern_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Sable
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Sandstorm_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Soleil Levant
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Ocean_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Zénith
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Sun_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Fantaisie
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_Fancy_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Pokéball
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Vivillon_PokeBall_Pattern_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion
                    }
                    else if (dataJson.Name.Contains(Constantes.Couafarel) && i.Equals(countImg - 1))
                    {
                        #region Demoiselle
                        PokemonPokeBipJson pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Furfrou_Debutante_Trim_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Madame
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Furfrou_Matron_Trim_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Monsieur
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Furfrou_Dandy_Trim_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Reine
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Furfrou_Queen_Trim_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Kabuki
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Furfrou_Kabuki_Trim_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion

                        #region Pharaon
                        pokemonJson = MapToCopy(dataJson);
                        pokemonJson.Name = Constantes.Furfrou_Pharaoh_Trim_FR;
                        dataJsons.Add(pokemonJson);
                        #endregion
                    }
                }
            }

            if (!string.IsNullOrEmpty(dataJson.NextUrl))
                RecursiveGetDataPBJsonWithUrl(dataJson.NextUrl, dataJsons);
        }

        public static void WriteToJson(List<PokemonJson> dataJsons, bool limit = false, int gen = -1, bool mobile = false)
        {
            StringBuilder nameFile = new StringBuilder();
            if (gen != -1)
                nameFile.Append("PokeScrapGen" + gen.ToString() + ".json");
            else
                if (limit)
                    nameFile.Append("PokeScrapUnique.json");
                else
                    nameFile.Append("PokeScrap.json");

            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }

        public static void WriteToJson(List<Pokemon> pokemons, List<TypePok> typePoks, List<Talent> talents, List<Attaque> attaques, List<TypeAttaque> typeAttaques, List<Game> games)
        {
            StringBuilder nameFile = new StringBuilder();
            nameFile.Append("TypePokDbToJson.json");

            string json = JsonConvert.SerializeObject(typePoks, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);

            nameFile = new StringBuilder();
            nameFile.Append("TalentDbToJson.json");

            json = JsonConvert.SerializeObject(talents, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);

            nameFile = new StringBuilder();
            nameFile.Append("AttaqueDbToJson.json");

            json = JsonConvert.SerializeObject(attaques, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);

            nameFile = new StringBuilder();
            nameFile.Append("TypeAttaqueDbToJson.json");

            json = JsonConvert.SerializeObject(typeAttaques, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);

            nameFile = new StringBuilder();
            nameFile.Append("GameDbToJson.json");

            json = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);

            nameFile = new StringBuilder();
            nameFile.Append("DbToJson.json");

            json = JsonConvert.SerializeObject(pokemons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }

        public static void WriteToJson(List<PokemonPokeBipJson> dataJsons)
        {
            StringBuilder nameFile = new StringBuilder();
            nameFile.Append("PokeBipScrapGen.json");

            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }

        public static void WriteToJsonMobile(List<PokemonMobileJsonV1> dataJsons)
        {
            StringBuilder nameFile = new StringBuilder();
            nameFile.Append("PokeScrapV1.json");

            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }

        public static void WriteToJsonMobileV2(List<PokemonMobileJsonV2> dataJsons)
        {
            StringBuilder nameFile = new StringBuilder();
            nameFile.Append("PokeScrapV2.json");

            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }

        public static void WriteToJsonMobile(List<TypePokMobileJsonV1> dataJsons)
        {
            StringBuilder nameFile = new StringBuilder();
            nameFile.Append("TypeScrapV1.json");

            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }

        public static void WriteToJsonMobile(List<TypePokMobileJsonV2> dataJsons)
        {
            StringBuilder nameFile = new StringBuilder();
            nameFile.Append("TypeScrapV2.json");

            string json = JsonConvert.SerializeObject(dataJsons, Formatting.Indented);
            File.WriteAllText(nameFile.ToString(), json);
        }
        #endregion

        #region Get Url Img/Sprite/Sound
        public static void GetUrlSprite(string spitesPkmDB, PokemonJson dataJson)
        {
            string response = HttpClientUtils.CallUrl(spitesPkmDB).Result;
            dataJson.UrlSprite = Constantes.urlPokepedia + GetUrlMini(response, dataJson.Number, dataJson.FR.Name);
            if (dataJson.UrlSprite == Constantes.urlPokepedia)
                Debug.WriteLine(dataJson.FR.Name);
        }

        public static string GetUrlMini_OLD(string html, string number, string name)
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

        public static string GetUrlMini(string html, string number, string name)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNode? TR = htmlDoc.DocumentNode.Descendants("tr").FirstOrDefault(m => m.InnerText.Contains(name));

            List<HtmlNode>? listTD = TR?.Descendants("td").ToList();

            string url = "";

            if (listTD != null)
            {
                if (listTD.Count == 8)
                {
                    if (listTD[4].InnerHtml.Contains("images"))
                    {
                        url = listTD[4].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                    else
                    {
                        url = listTD[5].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                }
                else if (listTD.Count == 9)
                {
                    if (listTD[5].InnerHtml.Contains("images"))
                    {
                        url = listTD[5].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                    else
                    {
                        url = listTD[6].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                }
            }

            if (!url.Contains("images"))
                Console.WriteLine("Error Sprite: " + number + " - " + name);

            return Constantes.urlPokepedia + url;
        }

        public static void GetUrlSpriteGen9(string spitesPkmDB, PokemonJson dataJson)
        {
            string response = HttpClientUtils.CallUrl(spitesPkmDB).Result;
            dataJson.UrlSprite = Constantes.urlPokepedia + GetUrlMiniGen9(response, dataJson.Number, dataJson.FR.Name);
            if (dataJson.UrlSprite == Constantes.urlPokepedia)
                Debug.WriteLine(dataJson.FR.Name);
        }

        public static string GetUrlMiniGen9(string html, string number, string name)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNode? table = htmlDoc.DocumentNode.Descendants("table").First(node => node.GetAttributeValue("class", "").Contains("tableaustandard"));

            var nameSearch = "";
            switch (name)
            {
                case "Fragroin Mâle":
                    name = "FragroinMâle";
                    break;
                case "Fragroin Femelle":
                    name = "FragroinFemelle";
                    break;
                case "Famignol Famille de Trois":
                    name = "FamignolFamille de Trois";
                    break;
                case "Famignol Famille de Quatre":
                    name = "FamignolFamille de Quatre";
                    break;
                case "Tapatoès Plumage Vert":
                    name = "Plumage Vert";
                    break;
                case "Tapatoès Plumage Bleu":
                    name = "TapatoèsPlumage Bleu";
                    break;
                case "Tapatoès Plumage Jaune":
                    name = "TapatoèsPlumage Jaune";
                    break;
                case "Tapatoès Plumage Blanc":
                    name = "TapatoèsPlumage Blanc";
                    break;
                case "Superdofin Forme Ordinaire":
                    name = "SuperdofinForme Ordinaire";
                    break;
                case "Superdofin Forme Super":
                    name = "SuperdofinForme Super";
                    break;
                case "Nigirigon Forme Courbée":
                    name = "NigirigonForme Courbée";
                    break;
                case "Nigirigon Forme Affalée":
                    name = "NigirigonForme Affalée";
                    break;
                case "Nigirigon Forme Raide":
                    name = "NigirigonForme Raide";
                    break;
                case "Deusolourdo Forme Double":
                    name = "DeusolourdoForme Double";
                    break;
                case "Deusolourdo Forme Triple":
                    name = "DeusolourdoForme Triple";
                    break;
                case "Mordudor Forme Coffre":
                    name = "MordudorForme Coffre";
                    break;
                case "Mordudor Forme Marche":
                    name = "MordudorForme Marche";
                    break;
                default:
                    nameSearch = name;
                    break;
            }


            HtmlNode? TR = table.Descendants("tr").FirstOrDefault(m => m.InnerText.Contains(name));

            List<HtmlNode>? listTD = TR?.Descendants("td").ToList();

            string url = "";

            if (listTD != null)
            {
                if (listTD.Count <= 7)
                {
                    if (listTD[0].InnerHtml.Contains("images"))
                    {
                        url = listTD[0].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                    else
                    {
                        url = listTD[1].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                }
                else if (listTD.Count >= 8)
                {
                    if (listTD[1].InnerHtml.Contains("images"))
                    {
                        url = listTD[1].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                    else
                    {
                        url = listTD[2].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    }
                }
            }

            if (!url.Contains("images"))
                Console.WriteLine("Error Sprite Gen 9: " + number + " - " + name);

            return url;
        }

        public static void GetUrlsMini(string html, IRepositoryExtendsPokemon<Pokemon> repositoryPkm)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<string> updates = new List<string>();
            List<string> erreurs = new List<string>();

            List<HtmlNode> TRs = htmlDoc.DocumentNode.Descendants("tr").ToList();

            string previousPokemon = "";

            foreach (HtmlNode tr in TRs)
            {
                List<HtmlNode>? listTD = tr?.Descendants("td").ToList();

                string url = "";
                string name = "";

                if (listTD != null)
                {
                    if (listTD.Count == 8)
                    {
                        if (listTD[4].InnerHtml.Contains("images"))
                        {
                            name = listTD[0].InnerText.Replace("Mâle", string.Empty);
                            url = listTD[4].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                        }
                        else
                        {
                            name = listTD[0].InnerText.Replace("Mâle", string.Empty);
                            url = listTD[5].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                        }
                    }
                    else if (listTD.Count == 9)
                    {
                        if (listTD[5].InnerHtml.Contains("images"))
                        {
                            name = listTD[1].InnerText.Replace("Mâle", string.Empty);
                            url = listTD[5].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                        }
                        else
                        {
                            int number;
                            bool success = int.TryParse(listTD[0].InnerText, out number);
                            if (success)
                            {
                                name = listTD[1].InnerText.Replace("Mâle", string.Empty);
                            }
                            else
                            {
                                name = listTD[0].InnerText.Replace("Mâle", string.Empty);
                            }

                            url = listTD[6].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                        }
                    }
                }

                if (url != null)
                {
                    string valueError = ": " + Constantes.urlPokepedia;
                    string value = name + ": " + Constantes.urlPokepedia + url;

                    if (value != valueError)
                    {
                        if (listTD[0].InnerHtml.Contains("small"))
                            name = listTD[0].InnerHtml.Replace("/", string.Empty).Replace("\\", string.Empty).Split("\"")[4].Replace("<a>", String.Empty).Replace("<br>", " ").Replace("<small>", String.Empty).Replace(">", String.Empty);

                        if (listTD[1].InnerHtml.Contains("small"))
                            name = listTD[1].InnerHtml.Replace("/", string.Empty).Replace("\\", string.Empty).Split("\"")[4].Replace("<a>", String.Empty).Replace("<br>", " ").Replace("<small>", String.Empty).Replace(">", String.Empty);

                        if (!value.Contains("Déflaisan"))
                        {

                            name = name.Replace("Mâle", string.Empty);
                        }
                    }
                }
                else
                {
                    if (listTD.Count == 8)
                        url = listTD[7].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    else
                        url = listTD[8].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (string.IsNullOrEmpty(previousPokemon) || !name.Contains(previousPokemon))
                    {
                        previousPokemon = name;
                    }

                    else
                    {
                        if (name.Contains(previousPokemon) && !name.Contains(" ") && !name.Contains(Constantes.MegaEvolution) && !name.Contains(Constantes.Ultra))
                        {
                            string test = name.Replace(previousPokemon, string.Empty);
                            name = previousPokemon + " " + test;
                        }
                    }
                }

                if (name.Equals("Paragruel"))
                {
                    url = listTD[8].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                }
                else if (name.Equals("Paragruel Femelle"))
                {
                    url = listTD[7].Descendants("img").Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                }

                string checkUrl = Constantes.urlPokepedia + url.Replace("%E2%99%82", "♂").Replace("%E2%99%80", "♀");
                if (checkUrl != Constantes.urlPokepedia)
                {
                    name = name.Trim()
                        .Replace("Rattata c", "Rattatac")
                        .Replace("Paras ect", "Parasect")
                        .Replace("HisuiMonarque", "Hisui Monarque")
                        .Replace("Smogo go", "Smogogo")
                        .Replace("Kabuto ps", "Kabutops")
                        .Replace("Draco losse", "Dracolosse")
                        .Replace("Coxy claque", "Coxyclaque")
                        .Replace("Zarbi Forme A", "Zarbi")
                        .Replace("HisuiFemelle", "Hisui Femelle")
                        .Replace("Morphéo Forme Normale", "Morphéo")
                        .Replace("Kyogre Primo-", "Kyogre Primo-Résurgence")
                        .Replace("Groudon Primo-", "Groudon Primo-Résurgence")
                        .Replace("DialgaForme Originelle", "Dialga Forme Originelle")
                        .Replace("PalkiaForme Originelle", "Palkia Forme Originelle")
                        .Replace("BargantuaMotif Blanc", "Bargantua Motif Blanc")
                        .Replace("Darumacho de Galar Mode Normal", "Darumacho de Galar")
                        .Replace("Prismillon Motif Poké Ball", "Prismillon Motif PokéBall")
                        .Replace("Xerneas Mode Déchaîné", "Xerneas")
                        .Replace("&#160;", " ")
                        .Replace("Hoopa Hoopa", "Hoopa")
                        .Replace("Type:0", "Type 0")
                        .Replace("Necrozma Ailes de l'Aurore", "Necrozma Ailes de l’Aurore")
                        .Replace("Bekaglaçon Tête de Gel", "Bekaglaçon")
                        .Replace("Shifours Gigamax Style Poing Final", "Shifours Gigamax (Style Poing Final)")
                        .Replace("Shifours Gigamax Style Mille Poings", "Shifours Gigamax (Style Mille Poings)")
                        .Replace("Sylveroy Cavalier d'Effroi", "Sylveroy Cavalier d’Effroi");

                    if (checkUrl.Contains("♀"))
                    {
                        if (!name.Contains(Constantes.Female_FR))
                        {
                            if (!name.Contains("Déflaisan")
                                && !name.Contains("Farfuret de Hisui Femelle"))
                                name = name + " " + Constantes.Female_FR;
                        }
                    }
                    else if (checkUrl.Contains("♂"))
                    {
                        if (!name.Contains(Constantes.M_FR))
                        {
                            if (name.Contains("Wimessir")
                                || name.Contains("Paragruel")
                                || name.Contains("Viskuse")
                                || name.Contains("Moyade")
                                || name.Contains("Mistigrix"))
                                name = name + " " + Constantes.M_FR;
                        }
                    }
                    else if (name.Equals("Kyurem"))
                    {
                        name = name.Replace("Kyurem", "Forme de Kyurem");
                    }
                    else if (name.Contains("Shifours"))
                    {
                        if (name.Equals("Shifours"))
                            name = name.Replace("Shifours", "Shifours Style Poing Final");
                        else if (name.Contains("Shifours Gigamax"))
                        {
                            if (name.Contains("Poing Final"))
                                name = "Shifours Gigamax(Style Poing Final)";
                            else if (name.Contains("Mille Poings"))
                                name = "Shifours Gigamax(Style Mille Poings)";
                        }
                    }
                    else if (name.Contains("Salarsen"))
                    {
                        if (checkUrl.Contains("Aig%C3%BCe"))
                            name = "Salarsen Forme Aigüe";
                        else if (checkUrl.Contains("Grave"))
                            name = "Salarsen Forme Grave";
                    }
                    else if (name.Contains("Amovénus"))
                    {
                        name = name.Replace("Forme", " Forme");
                    }
                    else if (name.Equals("Flabébé Fleur Rouge"))
                    {
                        name = "Flabébé";
                    }
                    else if (name.Equals("Floette Fleur Rouge"))
                    {
                        name = "Floette";
                    }
                    else if (name.Equals("Florges Fleur Rouge"))
                    {
                        name = "Florges";
                    }

                    try
                    {
                        Pokemon pokemon = repositoryPkm.Find(x => x.FR.Name == name).FirstOrDefault();

                        if (pokemon != null)
                        {
                            if (!pokemon.UrlSprite.Equals(checkUrl))
                            {
                                updates.Add(name);
                                updates.Add("Old Url: " + pokemon.UrlSprite);
                                updates.Add("New Url: " + checkUrl);

                                pokemon.UrlSprite = checkUrl;
                            }
                        }
                        else
                        {
                            erreurs.Add(name + ": " + checkUrl);
                        }
                    }
                    catch
                    {
                        erreurs.Add(name + ": " + checkUrl);
                    }
                }
            }

            Console.WriteLine("Début Erreurs");
            Console.WriteLine("Erreurs : " + erreurs.Count);
            foreach (string error in erreurs)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("Fin Erreurs");

            Console.WriteLine("Début Updates");
            Console.WriteLine("Updates : " + updates.Count);
            foreach (string update in updates)
            {
                Console.WriteLine(update);
            }
            Console.WriteLine("Fin Updates");

            repositoryPkm.UnitOfWork.SaveChanges();
        }

        public static void GetUrlSound(string html, IRepositoryExtendsPokemon<Pokemon> repositoryPkm)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<string> updates = new List<string>();
            List<string> erreurs = new List<string>();

            List<HtmlNode> TRs = htmlDoc.DocumentNode.Descendants("tr").ToList();

            string previousPokemon = "";

            foreach (HtmlNode tr in TRs)
            {
                List<HtmlNode>? listTD = tr?.Descendants("td").ToList();

                string url = "";
                string name = "";

                if (listTD != null)
                {
                    if (listTD.Count == 8)
                    {
                        if (listTD[4].InnerHtml.Contains("images"))
                        {
                            name = listTD[0].InnerText.Replace("Mâle", string.Empty);
                        }
                        else
                        {
                            name = listTD[0].InnerText.Replace("Mâle", string.Empty);
                        }
                    }
                    else if (listTD.Count == 9)
                    {
                        if (listTD[5].InnerHtml.Contains("images"))
                        {
                            name = listTD[1].InnerText.Replace("Mâle", string.Empty);
                        }
                        else
                        {
                            int number;
                            bool success = int.TryParse(listTD[0].InnerText, out number);
                            if (success)
                            {
                                name = listTD[1].InnerText.Replace("Mâle", string.Empty);
                            }
                            else
                            {
                                name = listTD[0].InnerText.Replace("Mâle", string.Empty);
                            }
                        }
                    }
                }

                if (listTD.Count == 8)
                {
                    url = listTD[0].InnerHtml.Split("\"")[1];
                }
                else if (listTD.Count == 9)
                {
                    url = listTD[1].InnerHtml.Split("\"")[1];
                }


                if (url != null)
                {
                    string valueError = ": " + Constantes.urlPokepedia;
                    string value = name + ": " + Constantes.urlPokepedia + url;

                    if (value != valueError)
                    {
                        if (listTD[0].InnerHtml.Contains("small"))
                            name = listTD[0].InnerHtml.Replace("/", string.Empty).Replace("\\", string.Empty).Split("\"")[4].Replace("<a>", String.Empty).Replace("<br>", " ").Replace("<small>", String.Empty).Replace(">", String.Empty);

                        if (listTD[1].InnerHtml.Contains("small"))
                            name = listTD[1].InnerHtml.Replace("/", string.Empty).Replace("\\", string.Empty).Split("\"")[4].Replace("<a>", String.Empty).Replace("<br>", " ").Replace("<small>", String.Empty).Replace(">", String.Empty);

                        if (!value.Contains("Déflaisan"))
                        {

                            name = name.Replace("Mâle", string.Empty);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (string.IsNullOrEmpty(previousPokemon) || !name.Contains(previousPokemon))
                    {
                        previousPokemon = name;
                    }

                    else
                    {
                        if (name.Contains(previousPokemon) && !name.Contains(" ") && !name.Contains(Constantes.MegaEvolution) && !name.Contains(Constantes.Ultra))
                        {
                            string test = name.Replace(previousPokemon, string.Empty);
                            name = previousPokemon + " " + test;
                        }
                    }
                }

                string urlAudio = "";
                string checkUrl = Constantes.urlPokepedia + url.Replace("%E2%99%82", "♂").Replace("%E2%99%80", "♀");
                if (checkUrl != Constantes.urlPokepedia)
                {
                    name = name.Trim()
                        .Replace("Rattata c", "Rattatac")
                        .Replace("Paras ect", "Parasect")
                        .Replace("HisuiMonarque", "Hisui Monarque")
                        .Replace("Smogo go", "Smogogo")
                        .Replace("Kabuto ps", "Kabutops")
                        .Replace("Draco losse", "Dracolosse")
                        .Replace("Coxy claque", "Coxyclaque")
                        .Replace("Zarbi Forme A", "Zarbi")
                        .Replace("HisuiFemelle", "Hisui Femelle")
                        .Replace("Morphéo Forme Normale", "Morphéo")
                        .Replace("Kyogre Primo-", "Kyogre Primo-Résurgence")
                        .Replace("Groudon Primo-", "Groudon Primo-Résurgence")
                        .Replace("DialgaForme Originelle", "Dialga Forme Originelle")
                        .Replace("PalkiaForme Originelle", "Palkia Forme Originelle")
                        .Replace("BargantuaMotif Blanc", "Bargantua Motif Blanc")
                        .Replace("Darumacho de Galar Mode Normal", "Darumacho de Galar")
                        .Replace("Prismillon Motif Poké Ball", "Prismillon Motif PokéBall")
                        .Replace("Xerneas Mode Déchaîné", "Xerneas")
                        .Replace("&#160;", " ")
                        .Replace("Hoopa Hoopa", "Hoopa")
                        .Replace("Type:0", "Type 0")
                        .Replace("Necrozma Ailes de l'Aurore", "Necrozma Ailes de l’Aurore")
                        .Replace("Bekaglaçon Tête de Gel", "Bekaglaçon")
                        .Replace("Shifours Gigamax Style Poing Final", "Shifours Gigamax (Style Poing Final)")
                        .Replace("Shifours Gigamax Style Mille Poings", "Shifours Gigamax (Style Mille Poings)")
                        .Replace("Sylveroy Cavalier d'Effroi", "Sylveroy Cavalier d’Effroi");

                    if (checkUrl.Contains("♀"))
                    {
                        if (!name.Contains(Constantes.Female_FR))
                        {
                            if (!name.Contains("Déflaisan")
                                && !name.Contains("Farfuret de Hisui Femelle"))
                                name = name + " " + Constantes.Female_FR;
                        }
                    }
                    else if (checkUrl.Contains("♂"))
                    {
                        if (!name.Contains(Constantes.M_FR))
                        {
                            if (name.Contains("Wimessir")
                                || name.Contains("Paragruel")
                                || name.Contains("Viskuse")
                                || name.Contains("Moyade")
                                || name.Contains("Mistigrix"))
                                name = name + " " + Constantes.M_FR;
                        }
                    }
                    else if (name.Equals("Kyurem"))
                    {
                        name = name.Replace("Kyurem", "Forme de Kyurem");
                    }
                    else if (name.Contains("Shifours"))
                    {
                        if (name.Equals("Shifours"))
                            name = name.Replace("Shifours", "Shifours Style Poing Final");
                        else if (name.Contains("Shifours Gigamax"))
                        {
                            if (name.Contains("Poing Final"))
                                name = "Shifours Gigamax(Style Poing Final)";
                            else if (name.Contains("Mille Poings"))
                                name = "Shifours Gigamax(Style Mille Poings)";
                        }
                    }
                    else if (name.Contains("Salarsen"))
                    {
                        if (checkUrl.Contains("Aig%C3%BCe"))
                            name = "Salarsen Forme Aigüe";
                        else if (checkUrl.Contains("Grave"))
                            name = "Salarsen Forme Grave";
                    }
                    else if (name.Contains("Amovénus"))
                    {
                        name = name.Replace("Forme", " Forme");
                    }
                    else if (name.Equals("Flabébé Fleur Rouge"))
                    {
                        name = "Flabébé";
                    }
                    else if (name.Equals("Floette Fleur Rouge"))
                    {
                        name = "Floette";
                    }
                    else if (name.Equals("Florges Fleur Rouge"))
                    {
                        name = "Florges";
                    }

                    try
                    {
                        string response = HttpClientUtils.CallUrl(checkUrl).Result;
                        htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(response);

                        HtmlNode audio = htmlDoc.DocumentNode.Descendants("audio").FirstOrDefault();
                        if (audio != null)
                            urlAudio = "https:" + audio.OuterHtml.Split('\"')[1];

                        Pokemon pokemon = repositoryPkm.Find(x => x.FR.Name == name).FirstOrDefault();

                        if (pokemon != null && !string.IsNullOrEmpty(urlAudio))
                        {
                            if (pokemon.UrlSound != null)
                            {
                                if (!pokemon.UrlSound.Equals(urlAudio))
                                {
                                    updates.Add(name);
                                    updates.Add("Old Sound Url: " + pokemon.UrlSound);
                                    updates.Add("New Sound Url: " + urlAudio);

                                    pokemon.UrlSound = urlAudio;
                                }
                            }
                            else
                            {
                                updates.Add(name);
                                updates.Add("New Sound Url: " + urlAudio);

                                pokemon.UrlSound = urlAudio;
                            }
                            Console.WriteLine(name + ": " + urlAudio);
                        }
                        else
                        {
                            erreurs.Add(name + ": " + urlAudio);
                        }
                    }
                    catch
                    {
                        erreurs.Add(name + ": " + urlAudio);
                        repositoryPkm.UnitOfWork.SaveChanges();
                    }
                }
            }

            Console.WriteLine("Début Erreurs");
            Console.WriteLine("Erreurs : " + erreurs.Count);
            foreach (string error in erreurs)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("Fin Erreurs");

            Console.WriteLine("Début Updates");
            Console.WriteLine("Updates : " + updates.Count);
            foreach (string update in updates)
            {
                Console.WriteLine(update);
            }
            Console.WriteLine("Fin Updates");

            repositoryPkm.UnitOfWork.SaveChanges();

            #region Sound Not OK 27/01/2023
            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/6/6a/Cri_4_d_029.ogg'
            //WHERE id = 45

            // UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/c/c6/Cri_4_d_052.ogg'
            //WHERE id = 75

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/9/9f/Cri_4_d_128.ogg'
            //WHERE id in (179, 180, 181)

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/8/8d/Cri_4_d_194.ogg'
            //WHERE id = 260

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/7/7b/Cri_5_n_592.ogg'
            //WHERE id = 733

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/9/9c/Cri_5_n_593.ogg'
            //WHERE id = 735

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/9/94/Cri_6_x_668.ogg'
            //WHERE id = 840

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/5/55/Cri_6_x_678.ogg'
            //WHERE id = 860

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/2/21/Cri_849_Aig%C3%BCe_EB.ogg'
            //WHERE id = 1063

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/e/e2/Cri_876_%E2%99%82_EB.ogg'
            //WHERE id = 1095

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/2/21/Cri_876_%E2%99%80_EB.ogg'
            //WHERE id = 1096

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/f/fe/Cri_916_%E2%99%82_EV.ogg'
            //WHERE id = 1148

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/4/4f/Cri_922_EV.ogg'
            //WHERE id = 1155

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/1/13/Cri_923_EV.ogg'
            //WHERE id = 1156

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/c/ce/Cri_925_Trois_EV.ogg'
            //WHERE id = 1159

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/6/6c/Cri_931_EV.ogg'
            //WHERE id = 1165

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/c/c0/Cri_964_Ordinaire_EV.ogg'
            //WHERE id = 1201

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/9/94/Cri_978_Courb%C3%A9e_EV.ogg'
            //WHERE id = 1216

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/6/63/Cri_982_EV.ogg'
            //WHERE id = 1222

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/2/2e/Cri_999_Coffre_EV.ogg'
            //WHERE id = 1240

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/a/a1/Cri_1007_Finale_EV.ogg'
            //WHERE id = 1248

            //UPDATE Pokemons
            //SET UrlSound = 'https://www.pokepedia.fr/images/7/7d/Cri_1008_Ultime_EV.ogg'
            //WHERE id = 1249
            #endregion
        }

        public static void GetUrlSoundGen9(string html, IRepositoryExtendsPokemon<Pokemon> repositoryPkm)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<string> updates = new List<string>();
            List<string> erreurs = new List<string>();

            HtmlNode? table = htmlDoc.DocumentNode.Descendants("table").First(node => node.GetAttributeValue("class", "").Contains("tableaustandard"));

            List<HtmlNode>? TRs = table.Descendants("tr").ToList();

            string previousPokemon = "";

            foreach (HtmlNode tr in TRs)
            {
                List<HtmlNode>? listTD = tr?.Descendants("td").ToList();

                string url = "";
                string name = "";

                if (listTD != null)
                {
                    if (listTD.Count == 8)
                    {
                        name = listTD[2].InnerText.Replace("Mâle", string.Empty);
                        url = listTD[2].InnerHtml.Split("\"")[1];
                    }
                    else if (listTD.Count == 7)
                    {
                        name = listTD[1].InnerText.Replace("Mâle", string.Empty);
                        url = listTD[1].InnerHtml.Split("\"")[1];
                    }
                }

                if (url != null)
                {
                    string valueError = ": " + Constantes.urlPokepedia;
                    string value = name + ": " + Constantes.urlPokepedia + url;

                    if (value != valueError)
                    {
                        if (listTD[0].InnerHtml.Contains("small"))
                            name = listTD[0].InnerHtml.Replace("/", string.Empty).Replace("\\", string.Empty).Split("\"")[4].Replace("<a>", String.Empty).Replace("<br>", " ").Replace("<small>", String.Empty).Replace(">", String.Empty);

                        if (listTD[1].InnerHtml.Contains("small"))
                            name = listTD[1].InnerHtml.Replace("/", string.Empty).Replace("\\", string.Empty).Split("\"")[4].Replace("<a>", String.Empty).Replace("<br>", " ").Replace("<small>", String.Empty).Replace(">", String.Empty);

                        if (!value.Contains("Déflaisan"))
                        {

                            name = name.Replace("Mâle", string.Empty);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (string.IsNullOrEmpty(previousPokemon) || !name.Contains(previousPokemon))
                    {
                        previousPokemon = name;
                    }

                    else
                    {
                        if (name.Contains(previousPokemon) && !name.Contains(" ") && !name.Contains(Constantes.MegaEvolution) && !name.Contains(Constantes.Ultra))
                        {
                            string test = name.Replace(previousPokemon, string.Empty);
                            name = previousPokemon + " " + test;
                        }
                    }
                }

                string urlAudio = "";
                string checkUrl = Constantes.urlPokepedia + url.Replace("%E2%99%82", "♂").Replace("%E2%99%80", "♀");
                if (checkUrl != Constantes.urlPokepedia)
                {
                    name = name.Trim()
                        .Replace("Rattata c", "Rattatac")
                        .Replace("Paras ect", "Parasect")
                        .Replace("HisuiMonarque", "Hisui Monarque")
                        .Replace("Smogo go", "Smogogo")
                        .Replace("Kabuto ps", "Kabutops")
                        .Replace("Draco losse", "Dracolosse")
                        .Replace("Coxy claque", "Coxyclaque")
                        .Replace("Zarbi Forme A", "Zarbi")
                        .Replace("HisuiFemelle", "Hisui Femelle")
                        .Replace("Morphéo Forme Normale", "Morphéo")
                        .Replace("Kyogre Primo-", "Kyogre Primo-Résurgence")
                        .Replace("Groudon Primo-", "Groudon Primo-Résurgence")
                        .Replace("DialgaForme Originelle", "Dialga Forme Originelle")
                        .Replace("PalkiaForme Originelle", "Palkia Forme Originelle")
                        .Replace("BargantuaMotif Blanc", "Bargantua Motif Blanc")
                        .Replace("Darumacho de Galar Mode Normal", "Darumacho de Galar")
                        .Replace("Prismillon Motif Poké Ball", "Prismillon Motif PokéBall")
                        .Replace("Xerneas Mode Déchaîné", "Xerneas")
                        .Replace("&#160;", " ")
                        .Replace("Hoopa Hoopa", "Hoopa")
                        .Replace("Type:0", "Type 0")
                        .Replace("Necrozma Ailes de l'Aurore", "Necrozma Ailes de l’Aurore")
                        .Replace("Bekaglaçon Tête de Gel", "Bekaglaçon")
                        .Replace("Shifours Gigamax Style Poing Final", "Shifours Gigamax (Style Poing Final)")
                        .Replace("Shifours Gigamax Style Mille Poings", "Shifours Gigamax (Style Mille Poings)")
                        .Replace("Sylveroy Cavalier d'Effroi", "Sylveroy Cavalier d’Effroi");

                    if (checkUrl.Contains("♀"))
                    {
                        if (!name.Contains(Constantes.Female_FR))
                        {
                            if (!name.Contains("Déflaisan")
                                && !name.Contains("Farfuret de Hisui Femelle"))
                                name = name + " " + Constantes.Female_FR;
                        }
                    }
                    else if (checkUrl.Contains("♂"))
                    {
                        if (!name.Contains(Constantes.M_FR))
                        {
                            if (name.Contains("Wimessir")
                                || name.Contains("Paragruel")
                                || name.Contains("Viskuse")
                                || name.Contains("Moyade")
                                || name.Contains("Mistigrix"))
                                name = name + " " + Constantes.M_FR;
                        }
                    }
                    else if (name.Equals("Kyurem"))
                    {
                        name = name.Replace("Kyurem", "Forme de Kyurem");
                    }
                    else if (name.Contains("Shifours"))
                    {
                        if (name.Equals("Shifours"))
                            name = name.Replace("Shifours", "Shifours Style Poing Final");
                        else if (name.Contains("Shifours Gigamax"))
                        {
                            if (name.Contains("Poing Final"))
                                name = "Shifours Gigamax(Style Poing Final)";
                            else if (name.Contains("Mille Poings"))
                                name = "Shifours Gigamax(Style Mille Poings)";
                        }
                    }
                    else if (name.Contains("Salarsen"))
                    {
                        if (checkUrl.Contains("Aig%C3%BCe"))
                            name = "Salarsen Forme Aigüe";
                        else if (checkUrl.Contains("Grave"))
                            name = "Salarsen Forme Grave";
                    }
                    else if (name.Contains("Amovénus"))
                    {
                        name = name.Replace("Forme", " Forme");
                    }
                    else if (name.Equals("Flabébé Fleur Rouge"))
                    {
                        name = "Flabébé";
                    }
                    else if (name.Equals("Floette Fleur Rouge"))
                    {
                        name = "Floette";
                    }
                    else if (name.Equals("Florges Fleur Rouge"))
                    {
                        name = "Florges";
                    }

                    try
                    {
                        string response = HttpClientUtils.CallUrl(checkUrl).Result;
                        htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(response);

                        HtmlNode audio = htmlDoc.DocumentNode.Descendants("audio").FirstOrDefault();
                        if (audio != null)
                            urlAudio = "https:" + audio.OuterHtml.Split('\"')[1];

                        Pokemon pokemon = repositoryPkm.Find(x => x.FR.Name == name).FirstOrDefault();

                        if (pokemon != null && !string.IsNullOrEmpty(urlAudio))
                        {
                            if (pokemon.UrlSound != null)
                            {
                                if (!pokemon.UrlSound.Equals(urlAudio))
                                {
                                    updates.Add(name);
                                    updates.Add("Old Sound Url: " + pokemon.UrlSound);
                                    updates.Add("New Sound Url: " + urlAudio);

                                    pokemon.UrlSound = urlAudio;
                                }
                            }
                            else
                            {
                                updates.Add(name);
                                updates.Add("New Sound Url: " + urlAudio);

                                pokemon.UrlSound = urlAudio;
                            }
                            Console.WriteLine(name + ": " + urlAudio);
                        }
                        else
                        {
                            erreurs.Add(name + ": " + urlAudio);
                        }
                    }
                    catch
                    {
                        erreurs.Add(name + ": " + urlAudio);
                        repositoryPkm.UnitOfWork.SaveChanges();
                    }
                }
            }

            Console.WriteLine("Début Erreurs");
            Console.WriteLine("Erreurs : " + erreurs.Count);
            foreach (string error in erreurs)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("Fin Erreurs");

            Console.WriteLine("Début Updates");
            Console.WriteLine("Updates : " + updates.Count);
            foreach (string update in updates)
            {
                Console.WriteLine(update);
            }
            Console.WriteLine("Fin Updates");

            repositoryPkm.UnitOfWork.SaveChanges();
        }
        #endregion

        #region Get Data Stat & When Evolution
        public static void GetStats(string urlStats, PokemonJson dataJson, int option)
        {
            string name = GetNamePokebip(dataJson.FR.Name, dataJson.FR.DisplayName, option);
            string response = HttpClientUtils.CallUrlNoRedirect(urlStats + name).Result; //**New** Redirection because update on the website

            string newUrl = response.Split("'")[1];
            response = HttpClientUtils.CallUrl(newUrl).Result;

            Debug.WriteLine(newUrl);

            GetStatsPokemon(response, dataJson);
        }

        public static void GetDataPB(string urlStats, PokemonPokeBipJson dataJson, int option)
        {
            string name = GetNamePokebip(dataJson.Name, dataJson.DisplayName, option);

            if (name.Contains(Constantes.Couafarel))
                name = Constantes.Couafarel;

            string response = HttpClientUtils.CallUrlNoRedirect(urlStats + name).Result; //**New** Redirection because update on the website

            string newUrl = response.Split("'")[1];

            string version = String.Empty;
            
            if (newUrl.Split("/").Length > 5)
                version = newUrl.Split("/")[4];

            string game = null;

            switch (version)
            {
                case Constantes.RedBlueUrl:
                    game = Constantes.RedBlue_Name_FR;
                    break;
                case Constantes.YellowUrl:
                    game = Constantes.Yellow_Name_FR;
                    break;
                case Constantes.GoldSilverUrl:
                    game = Constantes.GoldSilver_Name_FR;
                    break;
                case Constantes.CrystalUrl:
                    game = Constantes.Crystal_Name_FR;
                    break;
                case Constantes.RubySapphireUrl:
                    game = Constantes.RubySapphire_Name_FR;
                    break;
                case Constantes.EmeraldUrl:
                    game = Constantes.Emerald_Name_FR;
                    break;
                case Constantes.FireRedLeafGreenUrl:
                    game = Constantes.FireRedLeafGreen_Name_FR;
                    break;
                case Constantes.DiamondPearlUrl:
                    game = Constantes.DiamondPearl_Name_FR;
                    break;
                case Constantes.PlatinumUrl:
                    game = Constantes.Platinum_Name_FR;
                    break;
                case Constantes.HeartGoldSoulSilverUrl:
                    game = Constantes.HeartGoldSoulSilver_Name_FR;
                    break;
                case Constantes.BlackWhiteUrl:
                    game = Constantes.BlackWhite_Name_FR;
                    break;
                case Constantes.Black2White2Url:
                    game = Constantes.Black2White2_Name_FR;
                    break;
                case Constantes.X_YUrl:
                    game = Constantes.X_Y_Name_FR;
                    break;
                case Constantes.OmegaRubyAlphaSapphireUrl:
                    game = Constantes.OmegaRubyAlphaSapphire_Name_FR;
                    break;
                case Constantes.SunMoonUrl:
                    game = Constantes.SunMoon_Name_FR;
                    break;
                case Constantes.UltraSunUltraMoonUrl:
                    game = Constantes.UltraSunUltraMoon_Name_FR;
                    break;
                case Constantes.LetsGoPikachuEvoliUrl:
                    game = Constantes.LetsGoPikachuEvoli_Name_FR;
                    break;
                case Constantes.SwordShieldUrl:
                    game = Constantes.SwordShield_Name_FR;
                    break;
                case Constantes.ShiningDiamondShiningPearlUrl:
                    game = Constantes.ShiningDiamondShiningPearl_Name_FR;
                    break;
                case Constantes.ArceusUrl:
                    game = Constantes.Arceus_Name_FR;
                    break;
                case Constantes.ScarletVioletUrl:
                    game = Constantes.ScarletViolet_Name_FR;
                    break;
                default:
                    break;
            }
            
            response = HttpClientUtils.CallUrl(newUrl).Result;

            Debug.WriteLine(newUrl);

            GetDataTalentAttackHidden(response, dataJson, version);
        }

        public static string GetNamePokebip(string name, string displayName, int option)
        {
            string nameSite = "";

            if (name.Contains("♀") || name.Contains("♂"))
            {
                if (name.Contains("♀"))
                {
                    nameSite = name.Split('♀')[0] + "-f";
                }
                else
                {
                    nameSite = name.Split('♂')[0] + "-m";
                }
            }
            else if (name.Contains(Constantes.Alola))
            {
                nameSite = displayName.Split(' ')[0] + "-" + Constantes.Alola;
            }
            else if (name.Contains(Constantes.Galar))
            {
                if (name.Contains("M. Mime"))
                    nameSite = displayName.Replace(". ", "-").Split(' ')[0];
                else
                    nameSite = displayName.Split(' ')[0] + "-" + Constantes.Galar;
            }
            else if (name.Contains(Constantes.Hisui))
            {
                nameSite = displayName.Split(' ')[0];
            }
            else if (name.Contains("Mâle") || name.Contains("Femelle"))
            {
                if (name.Contains("Déflaisan") || name.Contains("Viskuse") || name.Contains("Moyade") || name.Contains("Mistigrix") || name.Contains("Paragruel") || name.Contains("Wimessir") || name.Contains("Fragroin"))
                {
                    if (name.Contains("Mâle"))
                        nameSite = displayName;
                    else
                        nameSite = displayName + "-femelle";
                }
                else if (name.Contains("Némélios"))
                {
                    nameSite = displayName;
                }
            }
            else if (name.Contains(' '))
            {
                if (name.Contains(". "))
                {
                    nameSite = displayName.Replace(". ", "-");
                }
                else if (name.Contains("Primo"))
                {
                    nameSite = "Primo-" + displayName;
                }
                else if (name.Contains("Forme")
                    || name.Contains("Cape")
                    || name.Contains("Temps")
                    || name.Contains("Mer")
                    || name.Contains("Aspect")
                    || name.Contains("Motif")
                    || name.Contains("Style")
                    || name.Contains("Mode")
                    || name.Contains("Héros")
                    || name.Contains("Forme Normal")
                    || name.Contains("Enchaîné")
                    || name.Contains("Cavalier")
                    || name.Contains("Necrozma"))
                {
                    if (option != 0)
                    {
                        if (name.Contains("Dialga") || name.Contains("Palkia"))
                        {
                            nameSite = displayName;
                        }
                        else if (name.Contains("Prismillon"))
                        {
                            nameSite = name.Split(' ')[0] + "-" + name.Split(' ')[1] + "-" + name.Split(' ')[2];
                        }
                        else if (name.Contains("Parfaite"))
                        {
                            nameSite = displayName + "-Parfait";
                        }
                        else if (name.Contains("Necrozma"))
                        {
                            if (name.Contains("Crinière"))
                                nameSite = displayName + "-" + name.Split(' ')[1] + "-" + name.Split(' ')[3];
                            else if (name.Contains("Ailes"))
                                nameSite = displayName + "-" + name.Split(' ')[1] + "-" + name.Split(' ')[3].Substring(2);
                            else
                                nameSite = displayName;
                        }
                        else if (name.Contains("Shifours"))
                        {
                            if (name.Contains("Gigamax"))
                            {
                                nameSite = displayName + "-" + name.Replace(" (", " ").Replace(")", "").Split(' ')[3] + "-" + name.Replace(" (", " ").Replace(")", "").Split(' ')[4] + "-" + name.Replace(" (", " ").Replace(")", "").Split(' ')[1];
                            }
                            else
                            {
                                nameSite = displayName + "-" + name.Split(' ')[2] + "-" + name.Split(' ')[3];
                            }
                        }
                        else if (name.Contains("Sylveroy"))
                        {
                            nameSite = displayName + "-" + name.Replace('’', ' ').Split(' ')[1] + "-" + name.Replace("’", " ").Split(' ')[3];
                        }
                        else if (name.Contains("Superdofin"))
                        {
                            if (name.Contains("Forme Ordinaire"))
                                nameSite = "superdofin";
                            else if (name.Contains("Forme Super"))
                                nameSite = "superdofin-super";
                        }
                        else
                        {
                            nameSite = displayName + "-" + name.Split(' ')[2];
                        }
                    }
                    else
                    {
                        if (name.Contains("Salarsen"))
                            nameSite = displayName + "-" + name.Split(' ')[2];
                        else
                            nameSite = displayName;
                    }
                }
                else
                {
                    if (name.Contains("Famignol"))
                    {
                        if (name.Contains("Famille de Trois"))
                            nameSite = "famignol";
                        else if (name.Contains("Famille de Quatre"))
                            nameSite = "famignol-famille-quatre";
                    }
                    else if (name.Contains("Tapatoès"))
                    {
                        if (name.Contains("Plumage Vert"))
                            nameSite = "tapatoes";
                        else if (name.Contains("Plumage Bleu"))
                            nameSite = "tapatoes-bleu";
                        else if (name.Contains("Plumage Jaune"))
                            nameSite = "tapatoes-jaune";
                        else if (name.Contains("Plumage Blanc"))
                            nameSite = "tapatoes-blanc";
                    }
                    else if (name.Contains("Tauros"))
                    {
                        if (name.Contains("Combative"))
                            nameSite = "tauros-race-combative";
                        else if (name.Contains("Flamboyante"))
                            nameSite = "tauros-race-flamboyante";
                        else if (name.Contains("Aquatique"))
                            nameSite = "tauros-race-aquatique";
                    }
                    else if (name.Contains("Axoloto"))
                    {
                        nameSite = "axoloto-paldea";
                    }
                    else if (!name.Contains("Mime Jr."))
                        nameSite = name.Replace(' ', '-');
                    else
                        nameSite = name.Split(" ")[0] + "-" + name.Split(" ")[1].Split('.')[0];
                }
            }
            else
            {
                if (name.Contains("Arceus"))
                {
                    nameSite = "arceus-normal";
                }
                else if (name.Contains("Spectreval"))
                {
                    nameSite = "sprectreval";
                }
                else
                {
                    nameSite = name;
                }
            }

            return nameSite;
        }

        public static void GetStatsPokemon(string html, PokemonJson dataJson)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<HtmlNode> tabOptions = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("tab-pane")).ToList();

            var stats = tabOptions[0].Descendants("div")
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
            if (dataJson.FR.Name.Contains(Constantes.Alola)
                || dataJson.FR.Name.Contains(Constantes.Galar)
                || dataJson.FR.Name.Contains(Constantes.Hisui)
                || dataJson.FR.Name.Contains("Fragroin Mâle")
                || dataJson.FR.Name.Contains("Famignol")
                || dataJson.FR.Name.Contains("Tapatoès Plumage Vert")
                || dataJson.FR.Name.Contains("Superdofin Forme Ordinaire")
                || dataJson.FR.Name.Contains("Nigirigon Forme Courbée")
                || dataJson.FR.Name.Contains("Deusolourdo Forme Double"))
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

        public static void GetDataTalentAttackHidden(string html, PokemonPokeBipJson dataJson, string version)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            #region Informations Générales
            List<HtmlNode> htmlNodes = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("panel-info")).ToList();

            dataJson.Specie = htmlNodes[1].ChildNodes[3].ChildNodes[1].InnerText.Split('\n')[2].Trim();
            dataJson.EggMoves = htmlNodes[1].ChildNodes[3].ChildNodes[5].InnerText.Split('\n')[2].Trim();
            dataJson.CaptureRate = htmlNodes[1].ChildNodes[3].ChildNodes[7].InnerText.Split('\n')[2].Trim();
            if (htmlNodes[1].ChildNodes[3].ChildNodes.Count > 9)
                dataJson.BasicHappiness = htmlNodes[1].ChildNodes[3].ChildNodes[9].InnerText.Split('\n')[2].Trim();
            #endregion

            #region Talent Caché
            htmlNodes = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("panel-heading")).ToList();

            foreach (var item in htmlNodes)
            {
                var strings = item.InnerText.Split("\n");
                if (strings.Length >= 2)
                {
                    if (strings[1].Trim() == "Talent caché")
                    {
                        dataJson.HiddenSkill = strings[3].Trim();
                    }
                }
            }
            #endregion

            #region Attaques
            List<AttackJson> attackJsons = new List<AttackJson>();
            HtmlNode htmlNode = htmlDoc.DocumentNode.Descendants("div").First(node => node.GetAttributeValue("id", "").Contains("section-moves"));

            List<HtmlNode> nodesH3 = htmlNode.Descendants("h3").ToList();
            List<HtmlNode> nodesTable = htmlNode.Descendants("tbody").ToList();

            int i = 0;
            foreach (var nodeH3 in nodesH3.Select(m => m.InnerText.Trim()))
            {
                if (nodeH3.Equals("Attaques apprises par évolution"))
                {
                    foreach (HtmlNode tr in nodesTable[i].Descendants("tr").ToList())
                    {
                        List<HtmlNode> tds = tr.Descendants("td").ToList();
                        AttackJson attackJson = new AttackJson();

                        attackJson.Name = tds[0].InnerText.Replace("\n", "").Trim();
                        attackJson.NameEN = tds[0].InnerHtml.Split("\n")[4].Split("<")[2].Split("-")[1].Trim();
                        attackJson.Power = tds[1].InnerText;
                        attackJson.Precision = tds[2].InnerText;
                        attackJson.PP = tds[3].InnerText;
                        attackJson.Type = tds[4].InnerHtml.Contains("'") ? tds[4].InnerHtml.Split("'")[5] : tds[4].InnerHtml;
                        attackJson.Category = tds[5].InnerHtml.Contains("'") ? tds[5].InnerHtml.Split("'")[5] : tds[5].InnerHtml;
                        attackJson.Description = tds[6].InnerText.Replace("&quot;", "\"");
                        attackJson.TypeLearn = Constantes.LearnEvolution;
                        attackJson.Game = version;

                        attackJsons.Add(attackJson);
                    }
                }
                else if (nodeH3.Equals("Attaques apprises par niveau"))
                {
                    foreach (HtmlNode tr in nodesTable[i].Descendants("tr").ToList())
                    {
                        List<HtmlNode> tds = tr.Descendants("td").ToList();
                        AttackJson attackJson = new AttackJson();

                        attackJson.Name = tds[0].InnerText.Replace("\n", "").Trim();
                        attackJson.NameEN = tds[0].InnerHtml.Split("\n")[4].Split("<")[2].Split("-")[1].Trim();
                        attackJson.Level = tds[1].InnerText.Split('\n')[1].Trim();
                        attackJson.Power = tds[2].InnerText;
                        attackJson.Precision = tds[3].InnerText;
                        attackJson.PP = tds[4].InnerText;
                        attackJson.Type = tds[5].InnerHtml.Contains("'") ? tds[5].InnerHtml.Split("'")[5] : tds[5].InnerHtml;
                        attackJson.Category = tds[6].InnerHtml.Contains("'") ? tds[6].InnerHtml.Split("'")[5] : tds[6].InnerHtml;
                        attackJson.Description = tds[7].InnerText.Replace("&quot;", "\"");
                        attackJson.TypeLearn = Constantes.LearnLevel;
                        attackJson.Game = version;

                        attackJsons.Add(attackJson);
                    }
                }
                else if (nodeH3.Equals("Attaques apprises par CT/CS"))
                {
                    foreach (HtmlNode tr in nodesTable[i].Descendants("tr").ToList())
                    {
                        List<HtmlNode> tds = tr.Descendants("td").ToList();
                        AttackJson attackJson = new AttackJson();

                        attackJson.Name = tds[0].InnerText.Replace("\n", "").Trim();
                        attackJson.NameEN = tds[0].InnerHtml.Split("\n")[4].Split("<")[2].Split("-")[1].Trim();
                        attackJson.CTCS = tds[1].InnerText;
                        attackJson.Power = tds[2].InnerText;
                        attackJson.Precision = tds[3].InnerText;
                        attackJson.PP = tds[4].InnerText;
                        attackJson.Type = tds[5].InnerHtml.Contains("'") ? tds[5].InnerHtml.Split("'")[5] : tds[5].InnerHtml;
                        attackJson.Category = tds[6].InnerHtml.Contains("'") ? tds[6].InnerHtml.Split("'")[5] : tds[6].InnerHtml;
                        attackJson.Description = tds[7].InnerText.Replace("&quot;", "\"");
                        attackJson.TypeLearn = Constantes.LearnCTCS;
                        attackJson.Game = version;

                        attackJsons.Add(attackJson);
                    }
                }
                else if (nodeH3.Equals("Attaques apprises par Maitre des Capacités (Move Tutor)"))
                {
                    foreach (HtmlNode tr in nodesTable[i].Descendants("tr").ToList())
                    {
                        List<HtmlNode> tds = tr.Descendants("td").ToList();
                        AttackJson attackJson = new AttackJson();

                        attackJson.Name = tds[0].InnerText.Replace("\n", "").Trim();
                        attackJson.NameEN = tds[0].InnerHtml.Split("\n")[4].Split("<")[2].Split("-")[1].Trim();
                        attackJson.Power = tds[1].InnerText;
                        attackJson.Precision = tds[2].InnerText;
                        attackJson.PP = tds[3].InnerText;
                        attackJson.Type = tds[4].InnerHtml.Contains("'") ? tds[4].InnerHtml.Split("'")[5] : tds[4].InnerHtml;
                        attackJson.Category = tds[5].InnerHtml.Contains("'") ? tds[5].InnerHtml.Split("'")[5] : tds[5].InnerHtml;
                        attackJson.Description = tds[6].InnerText.Replace("&quot;", "\"");
                        attackJson.TypeLearn = Constantes.LearnMoveTutor;
                        attackJson.Game = version;

                        attackJsons.Add(attackJson);
                    }
                }
                else if (nodeH3.Equals("Attaques apprises par reproduction (Egg Moves)"))
                {
                    foreach (HtmlNode tr in nodesTable[i].Descendants("tr").ToList())
                    {
                        List<HtmlNode> tds = tr.Descendants("td").ToList();
                        AttackJson attackJson = new AttackJson();

                        attackJson.Name = tds[0].InnerText.Replace("\n", "").Trim();
                        attackJson.NameEN = tds[0].InnerHtml.Split("\n")[4].Split("<")[2].Split("-")[1].Trim();
                        attackJson.Power = tds[2].InnerText;
                        attackJson.Precision = tds[3].InnerText;
                        attackJson.PP = tds[4].InnerText;
                        attackJson.Type = tds[5].InnerHtml.Contains("'") ? tds[5].InnerHtml.Split("'")[5] : tds[5].InnerHtml;
                        attackJson.Category = tds[6].InnerHtml.Contains("'") ? tds[6].InnerHtml.Split("'")[5] : tds[6].InnerHtml;
                        attackJson.Description = tds[7].InnerText.Replace("&quot;", "\"");
                        attackJson.TypeLearn = Constantes.LearnEgg;
                        attackJson.Game = version;

                        attackJsons.Add(attackJson);
                    }
                }
                i++;
            }

            dataJson.AttackJsons = attackJsons;
            #endregion
        }

        public static void GetTranslationWhenEvolution(PokemonJson dataJson)
        {
            if (dataJson.FR.WhenEvolution != null)
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
            else
            {
                Debug.WriteLine("Error WhenEvolution: " + dataJson.FR.Name);
            }
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

        #region Private Methods
        private static PokemonJson MapToCopy(PokemonJson dataJson)
        {
            PokemonJson pokemonJson = new PokemonJson();

            pokemonJson.Number = dataJson.Number;

            pokemonJson.FR = new()
            {
                Name = dataJson.FR.Name,
                DisplayName = dataJson.FR.DisplayName,
                DescriptionVx = dataJson.FR.DescriptionVx,
                DescriptionVy = dataJson.FR.DescriptionVy,
                Size = dataJson.FR.Size,
                Category = dataJson.FR.Category,
                Weight = dataJson.FR.Weight,
                Talent = dataJson.FR.Talent,
                DescriptionTalent = dataJson.FR.DescriptionTalent,
                Types = dataJson.FR.Types,
                Weakness = dataJson.FR.Weakness,
                Evolutions = dataJson.FR.Evolutions,
                WhenEvolution = dataJson.FR.WhenEvolution,
                NextUrl = dataJson.FR.NextUrl
            };

            pokemonJson.EN = new()
            {
                Name = dataJson.EN.Name,
                DisplayName = dataJson.EN.DisplayName,
                DescriptionVx = dataJson.EN.DescriptionVx,
                DescriptionVy = dataJson.EN.DescriptionVy,
                Size = dataJson.EN.Size,
                Category = dataJson.EN.Category,
                Weight = dataJson.EN.Weight,
                Talent = dataJson.EN.Talent,
                DescriptionTalent = dataJson.EN.DescriptionTalent,
                Types = dataJson.EN.Types,
                Weakness = dataJson.EN.Weakness,
                Evolutions = dataJson.EN.Evolutions,
                WhenEvolution = dataJson.EN.WhenEvolution,
                NextUrl = dataJson.EN.NextUrl
            };

            pokemonJson.ES = new()
            {
                Name = dataJson.ES.Name,
                DisplayName = dataJson.ES.DisplayName,
                DescriptionVx = dataJson.ES.DescriptionVx,
                DescriptionVy = dataJson.ES.DescriptionVy,
                Size = dataJson.ES.Size,
                Category = dataJson.ES.Category,
                Weight = dataJson.ES.Weight,
                Talent = dataJson.ES.Talent,
                DescriptionTalent = dataJson.ES.DescriptionTalent,
                Types = dataJson.ES.Types,
                Weakness = dataJson.ES.Weakness,
                Evolutions = dataJson.ES.Evolutions,
                WhenEvolution = dataJson.ES.WhenEvolution,
                NextUrl = dataJson.ES.NextUrl
            };

            pokemonJson.IT = new()
            {
                Name = dataJson.IT.Name,
                DisplayName = dataJson.IT.DisplayName,
                DescriptionVx = dataJson.IT.DescriptionVx,
                DescriptionVy = dataJson.IT.DescriptionVy,
                Size = dataJson.IT.Size,
                Category = dataJson.IT.Category,
                Weight = dataJson.IT.Weight,
                Talent = dataJson.IT.Talent,
                DescriptionTalent = dataJson.IT.DescriptionTalent,
                Types = dataJson.IT.Types,
                Weakness = dataJson.IT.Weakness,
                Evolutions = dataJson.IT.Evolutions,
                WhenEvolution = dataJson.IT.WhenEvolution,
                NextUrl = dataJson.IT.NextUrl
            };

            pokemonJson.DE = new()
            {
                Name = dataJson.DE.Name,
                DisplayName = dataJson.DE.DisplayName,
                DescriptionVx = dataJson.DE.DescriptionVx,
                DescriptionVy = dataJson.DE.DescriptionVy,
                Size = dataJson.DE.Size,
                Category = dataJson.DE.Category,
                Weight = dataJson.DE.Weight,
                Talent = dataJson.DE.Talent,
                DescriptionTalent = dataJson.DE.DescriptionTalent,
                Types = dataJson.DE.Types,
                Weakness = dataJson.DE.Weakness,
                Evolutions = dataJson.DE.Evolutions,
                WhenEvolution = dataJson.DE.WhenEvolution,
                NextUrl = dataJson.DE.NextUrl
            };

            pokemonJson.RU = new()
            {
                Name = dataJson.RU.Name,
                DisplayName = dataJson.RU.DisplayName,
                DescriptionVx = dataJson.RU.DescriptionVx,
                DescriptionVy = dataJson.RU.DescriptionVy,
                Size = dataJson.RU.Size,
                Category = dataJson.RU.Category,
                Weight = dataJson.RU.Weight,
                Talent = dataJson.RU.Talent,
                DescriptionTalent = dataJson.RU.DescriptionTalent,
                Types = dataJson.RU.Types,
                Weakness = dataJson.RU.Weakness,
                Evolutions = dataJson.RU.Evolutions,
                WhenEvolution = dataJson.RU.WhenEvolution,
                NextUrl = dataJson.RU.NextUrl
            };

            pokemonJson.CO = new()
            {
                Name = dataJson.CO.Name,
                DisplayName = dataJson.CO.DisplayName,
                DescriptionVx = dataJson.CO.DescriptionVx,
                DescriptionVy = dataJson.CO.DescriptionVy,
                Size = dataJson.CO.Size,
                Category = dataJson.CO.Category,
                Weight = dataJson.CO.Weight,
                Talent = dataJson.CO.Talent,
                DescriptionTalent = dataJson.CO.DescriptionTalent,
                Types = dataJson.CO.Types,
                Weakness = dataJson.CO.Weakness,
                Evolutions = dataJson.CO.Evolutions,
                WhenEvolution = dataJson.CO.WhenEvolution,
                NextUrl = dataJson.CO.NextUrl
            };

            pokemonJson.CN = new()
            {
                Name = dataJson.CN.Name,
                DisplayName = dataJson.CN.DisplayName,
                DescriptionVx = dataJson.CN.DescriptionVx,
                DescriptionVy = dataJson.CN.DescriptionVy,
                Size = dataJson.CN.Size,
                Category = dataJson.CN.Category,
                Weight = dataJson.CN.Weight,
                Talent = dataJson.CN.Talent,
                DescriptionTalent = dataJson.CN.DescriptionTalent,
                Types = dataJson.CN.Types,
                Weakness = dataJson.CN.Weakness,
                Evolutions = dataJson.CN.Evolutions,
                WhenEvolution = dataJson.CN.WhenEvolution,
                NextUrl = dataJson.CN.NextUrl
            };

            pokemonJson.JP = new()
            {
                Name = dataJson.JP.Name,
                DisplayName = dataJson.JP.DisplayName,
                DescriptionVx = dataJson.JP.DescriptionVx,
                DescriptionVy = dataJson.JP.DescriptionVy,
                Size = dataJson.JP.Size,
                Category = dataJson.JP.Category,
                Weight = dataJson.JP.Weight,
                Talent = dataJson.JP.Talent,
                DescriptionTalent = dataJson.JP.DescriptionTalent,
                Types = dataJson.JP.Types,
                Weakness = dataJson.JP.Weakness,
                Evolutions = dataJson.JP.Evolutions,
                WhenEvolution = dataJson.JP.WhenEvolution,
                NextUrl = dataJson.JP.NextUrl
            };

            pokemonJson.TypeEvolution = dataJson.TypeEvolution;

            pokemonJson.StatPv = dataJson.StatPv;

            pokemonJson.StatAttaque = dataJson.StatAttaque;

            pokemonJson.StatDefense = dataJson.StatDefense;

            pokemonJson.StatAttaqueSpe = dataJson.StatAttaqueSpe;

            pokemonJson.StatDefenseSpe = dataJson.StatDefenseSpe;

            pokemonJson.StatVitesse = dataJson.StatVitesse;

            pokemonJson.StatTotal = dataJson.StatTotal;

            pokemonJson.Generation = dataJson.Generation;

            return pokemonJson;
        }

        private static PokemonPokeBipJson MapToCopy(PokemonPokeBipJson dataJson)
        {
            PokemonPokeBipJson pokemonJson = new PokemonPokeBipJson();

            pokemonJson.Number = dataJson.Number;
            pokemonJson.Name = dataJson.Name;
            pokemonJson.DisplayName = dataJson.DisplayName;
            pokemonJson.Specie = dataJson.Specie;
            pokemonJson.EggMoves = dataJson.EggMoves;
            pokemonJson.CaptureRate = dataJson.CaptureRate;
            pokemonJson.BasicHappiness = dataJson.BasicHappiness;
            pokemonJson.HiddenSkill = dataJson.HiddenSkill;
            pokemonJson.AttackJsons = dataJson.AttackJsons;
            pokemonJson.NextUrl = dataJson.NextUrl;

            return pokemonJson;
        }
        #endregion
    }
}
