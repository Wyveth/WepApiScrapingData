﻿namespace WepApiScrapingData.Utils
{
    public static class Constantes
    {
        #region Configuration
        public const string SecurityOption = "Jwt";
        public const string CorsOption = "Cors";
        #endregion

        #region Pokepedia
        public const string urlPokepedia = "https://www.pokepedia.fr";
        public const string urlAllSpritesOld = urlPokepedia + "/Liste_des_Pok%C3%A9mon_dans_l'ordre_du_Pok%C3%A9dex_National";
        public const string urlAllSprites = urlPokepedia + "/Liste_des_miniatures_par_Pokémon";
        public const string pathExport = "Export/";
        #endregion

        #region Pokébip
        public const string urlStatsPB = "https://www.pokebip.com/pokedex/pokemon/";
        #endregion

        #region Api Pokemon
        public const string urlPath = "https://api.pokemon.com";
        public const string urlPathFR = "https://api.pokemon.com/fr/pokedex/";
        public const string urlPathEN = "https://api.pokemon.com/us/pokedex/";
        public const string urlPathES = "https://api.pokemon.com/es/pokedex/";
        public const string urlPathIT = "https://api.pokemon.com/it/pokedex/";
        public const string urlPathDE = "https://api.pokemon.com/de/pokedex/";
        public const string urlPathRU = "https://pokemon.fandom.com/ru/wiki/";
        public const string urlPathCO = "https://pokemonkorea.co.kr/pokedextemp/view/";
        public const string urlPathCN = "https://www.pokemon.cn/play/pokedex/";
        public const string urlPathJP = "https://zukan.pokemon.co.jp/detail/";

        public const string urlStartFR = urlPathFR + "bulbizarre";
        public const string urlStartEN = urlPathEN + "bulbasaur";
        public const string urlStartES = urlPathES + "bulbasaur";
        public const string urlStartIT = urlPathIT + "bulbasaur";
        public const string urlStartDE = urlPathDE + "bisasam";
        public const string urlStartRU = urlPathRU + "Бульбазавр";
        public const string urlStartCO = urlPathCO + "1";
        public const string urlStartCN = urlPathCN + "001";
        public const string urlStartJP = urlPathJP + "001";

        public const string urlTestWeakness = urlPathFR + "paras";
        public const string urlTestEvolution = urlPathFR + "aquali";
        public const string urlTestTypeEvolution = urlPathFR + "dracaufeu";
        public const string urlTestMime = urlPathFR + "m-mime";
        public const string urlTestDoubleTalent = urlPathFR + "mistigrix";
        public const string urlTestMissInfo = urlPathFR + "salarsen";
        public const string urlTestGetSprite = urlPathFR + "deoxys";
        public const string urlTestErrorType = urlPathFR + "type0";
        public const string urlTestShifours = urlPathFR + "shifours";
        public const string urlTestSylveroy = urlPathFR + "sylveroy";
        public const string urlTestCanarticho = urlPathFR + "canarticho";
        public const string urlTestCerbyllin = urlPathFR + "cerbyllin";
        public const int lastPokemonNumber = 1008;

        public const string urlTestSimulate_FR = urlPathFR + "necrozma";
        public const string urlTestSimulate_EN = urlPathEN + "necrozma";
        public const string urlTestSimulate_ES = urlPathES + "necrozma";
        public const string urlTestSimulate_IT = urlPathIT + "necrozma";
        public const string urlTestSimulate_DE = urlPathDE + "necrozma";
        public const string urlTestSimulate_RU = urlPathRU + "necrozma";

        #region Url Start Gen Arceus
        public const string urlStart0Gen_FR = urlPathFR + "cerbyllin";
        public const string urlStart0Gen_EN = urlPathEN + "wyrdeer";
        public const string urlStart0Gen_ES = urlPathES + "wyrdeer";
        public const string urlStart0Gen_IT = urlPathIT + "wyrdeer";
        public const string urlStart0Gen_DE = urlPathDE + "damythir";
        public const string urlStart0Gen_RU = urlPathRU + "Вирдир";
        public const string urlStart0Gen_CO = urlPathCO + "1097";
        public const string urlStart0Gen_CN = urlPathCN + "899";
        public const string urlStart0Gen_JP = urlPathJP + "899";
        #endregion

        #region Url Start Gen 1
        public const string urlStart1Gen_FR = urlPathFR + "bulbizarre";
        public const string urlStart1Gen_EN = urlPathEN + "bulbasaur";
        public const string urlStart1Gen_ES = urlPathES + "bulbasaur";
        public const string urlStart1Gen_IT = urlPathIT + "bulbasaur";
        public const string urlStart1Gen_DE = urlPathDE + "bisasam";
        public const string urlStart1Gen_RU = urlPathRU + "Бульбазавр";
        public const string urlStart1Gen_CO = urlPathCO + "1";
        public const string urlStart1Gen_CN = urlPathCN + "001";
        public const string urlStart1Gen_JP = urlPathJP + "001";
        #endregion

        #region Url Start Gen 2
        public const string urlStart2Gen_FR = urlPathFR + "germignon";
        public const string urlStart2Gen_EN = urlPathEN + "chikorita";
        public const string urlStart2Gen_ES = urlPathES + "chikorita";
        public const string urlStart2Gen_IT = urlPathIT + "chikorita";
        public const string urlStart2Gen_DE = urlPathDE + "endivie";
        public const string urlStart2Gen_RU = urlPathRU + "Чикорита";
        public const string urlStart2Gen_CO = urlPathCO + "209";
        public const string urlStart2Gen_CN = urlPathCN + "152";
        public const string urlStart2Gen_JP = urlPathJP + "152";
        #endregion

        #region Url Start Gen 3
        public const string urlStart3Gen_FR = urlPathFR + "arcko";
        public const string urlStart3Gen_EN = urlPathEN + "treecko";
        public const string urlStart3Gen_ES = urlPathES + "treecko";
        public const string urlStart3Gen_IT = urlPathIT + "treecko";
        public const string urlStart3Gen_DE = urlPathDE + "geckarbor";
        public const string urlStart3Gen_RU = urlPathRU + "Трико";
        public const string urlStart3Gen_CO = urlPathCO + "319";
        public const string urlStart3Gen_CN = urlPathCN + "252";
        public const string urlStart3Gen_JP = urlPathJP + "252";
        #endregion

        #region Url Start Gen 4
        public const string urlStart4Gen_FR = urlPathFR + "tortipouss";
        public const string urlStart4Gen_EN = urlPathEN + "turtwig";
        public const string urlStart4Gen_ES = urlPathES + "turtwig";
        public const string urlStart4Gen_IT = urlPathIT + "turtwig";
        public const string urlStart4Gen_DE = urlPathDE + "chelast";
        public const string urlStart4Gen_RU = urlPathRU + "Тортвиг";
        public const string urlStart4Gen_CO = urlPathCO + "481";
        public const string urlStart4Gen_CN = urlPathCN + "387";
        public const string urlStart4Gen_JP = urlPathJP + "387";
        #endregion

        #region Url Start Gen 5
        public const string urlStart5Gen_FR = urlPathFR + "vipelierre";
        public const string urlStart5Gen_EN = urlPathEN + "snivy";
        public const string urlStart5Gen_ES = urlPathES + "snivy";
        public const string urlStart5Gen_IT = urlPathIT + "snivy";
        public const string urlStart5Gen_DE = urlPathDE + "serpifeu";
        public const string urlStart5Gen_RU = urlPathRU + "Снайви";
        public const string urlStart5Gen_CO = urlPathCO + "610";
        public const string urlStart5Gen_CN = urlPathCN + "495";
        public const string urlStart5Gen_JP = urlPathJP + "495";
        #endregion

        #region Url Start Gen 6
        public const string urlStart6Gen_FR = urlPathFR + "marisson";
        public const string urlStart6Gen_EN = urlPathEN + "chespin";
        public const string urlStart6Gen_ES = urlPathES + "chespin";
        public const string urlStart6Gen_IT = urlPathIT + "chespin";
        public const string urlStart6Gen_DE = urlPathDE + "igamaro";
        public const string urlStart6Gen_RU = urlPathRU + "Чеспин";
        public const string urlStart6Gen_CO = urlPathCO + "796";
        public const string urlStart6Gen_CN = urlPathCN + "650";
        public const string urlStart6Gen_JP = urlPathJP + "650";
        #endregion

        #region Url Start Gen 7
        public const string urlStart7Gen_FR = urlPathFR + "brindibou";
        public const string urlStart7Gen_EN = urlPathEN + "rowlet";
        public const string urlStart7Gen_ES = urlPathES + "rowlet";
        public const string urlStart7Gen_IT = urlPathIT + "rowlet";
        public const string urlStart7Gen_DE = urlPathDE + "bauz";
        public const string urlStart7Gen_RU = urlPathRU + "Роулет";
        public const string urlStart7Gen_CO = urlPathCO + "878";
        public const string urlStart7Gen_CN = urlPathCN + "722";
        public const string urlStart7Gen_JP = urlPathJP + "722";
        #endregion

        #region Url Start Gen 8
        public const string urlStart8Gen_FR = urlPathFR + "ouistempo";
        public const string urlStart8Gen_EN = urlPathEN + "grookey";
        public const string urlStart8Gen_ES = urlPathES + "grookey";
        public const string urlStart8Gen_IT = urlPathIT + "grookey";
        public const string urlStart8Gen_DE = urlPathDE + "chimpep";
        public const string urlStart8Gen_RU = urlPathRU + "Груки";
        public const string urlStart8Gen_CO = urlPathCO + "977";
        public const string urlStart8Gen_CN = urlPathCN + "810";
        public const string urlStart8Gen_JP = urlPathJP + "810";
        #endregion

        #region Url Start Gen 9
        public const string urlStart9Gen_FR = urlPathFR + "poussacha";
        public const string urlStart9Gen_EN = urlPathEN + "sprigatito";
        public const string urlStart9Gen_ES = urlPathES + "sprigatito";
        public const string urlStart9Gen_IT = urlPathIT + "sprigatito";
        public const string urlStart9Gen_DE = urlPathDE + "felori";
        public const string urlStart9Gen_RU = urlPathRU + "";
        public const string urlStart9Gen_CO = urlPathCO + "1106";
        public const string urlStart9Gen_CN = urlPathCN + "906";
        public const string urlStart9Gen_JP = urlPathJP + "906";

        public const string urlStart9GenTest_FR = urlPathFR + "famignol";
        public const string urlStart9GenTest_EN = urlPathEN + "maushold";
        public const string urlStart9GenTest_ES = urlPathES + "maushold";
        public const string urlStart9GenTest_IT = urlPathIT + "maushold";
        public const string urlStart9GenTest_DE = urlPathDE + "famieps";
        public const string urlStart9GenTest_RU = urlPathRU + "";
        public const string urlStart9GenTest_CO = urlPathCO + "1106";
        public const string urlStart9GenTest_CN = urlPathCN + "906";
        public const string urlStart9GenTest_JP = urlPathJP + "906";
        #endregion

        public const string MegaEvolution = "Méga";
        public const string MegaLevel = "Mega";
        public const string GigaEvolution = "Gigamax";
        public const string GigaLevel = "Gigamax";
        public const string NormalEvolution = "Normal";
        public const string Alola = "Alola";
        public const string Galar = "Galar";
        public const string Femelle = "Femelle";
        public const string VarianteSexe = "VarianteSexe";
        public const string Variant = "Variant";
        public const string Hisui = "Hisui";
        public const string Paldea = "Paldea";

        public const string regionAlola_FR = "d'Alola";
        public const string regionGalar_FR = "de Galar";
        public const string regionHisui_FR = "de Hisui";

        public const string regionAlola_EN = "Alolan";
        public const string regionGalar_EN = "Galarian";
        public const string regionHisui_EN = "Hisuian";

        public const string regionAlola_ES = "de Alola";
        public const string regionGalar_ES = "de Galar";
        public const string regionHisui_ES = "de Hisui";

        public const string regionAlola_IT = "di Alola";
        public const string regionGalar_IT = "di Galar";
        public const string regionHisui_IT = "di Hisui";

        public const string regionAlola_DE = "von Alola";
        public const string regionGalar_DE = "von Galar";
        public const string regionHisui_DE = "von Hisui";

        public const string regionAlola_RU = "от Алола";
        public const string regionGalar_RU = "от галарского";
        public const string regionHisui_RU = "от Хисуи";

        public const string Ultra = "Ultra";
        public const string Meganium = "Méganium";
        public const string Megapagos = "Mégapagos";
        public const string Prismillon = "Prismillon";
        public const string Couafarel = "Couafarel";

        public const string urlBulbapedia = "https://bulbapedia.bulbagarden.net/wiki/"; //EN
        public const string url_move = "_(move)";
        #endregion

        #region PokemonDB
        public const string spitesPkmDB = "https://pokemondb.net/sprites";
        #endregion

        #region Color
        public const string Steel_FR = "Acier";
        public const string Steel_EN = "Steel";
        public const string Steel_ES = "Acero";
        public const string Steel_IT = "Acciaio";
        public const string Steel_DE = "Stahl";
        public const string Steel_RU = "стали";
        public const string Steel_JP = "はがね";
        public const string Steel_CO = "강철";
        public const string Steel_CN = "钢";
        public const string ImgColorSteel = "#0B334C";
        public const string InfoColorSteel = "#6399AC";
        public const string TypeColorSteel = "#5A8EA1";

        public const string Fighting_FR = "Combat";
        public const string Fighting_EN = "Fighting";
        public const string Fighting_ES = "Lucha";
        public const string Fighting_IT = "Lotta";
        public const string Fighting_DE = "Kampf";
        public const string Fighting_RU = "Борьба";
        public const string Fighting_JP = "かくとう";
        public const string Fighting_CO = "격투";
        public const string Fighting_CN = "格斗";
        public const string ImgColorFighting = "#47362E";
        public const string InfoColorFighting = "#D94A73";
        public const string TypeColorFighting = "#CE4069";

        public const string Dragon_FR = "Dragon";
        public const string Dragon_EN = "Dragon";
        public const string Dragon_ES = "Dragón";
        public const string Dragon_IT = "Drago";
        public const string Dragon_DE = "Drache";
        public const string Dragon_RU = "Дракон";
        public const string Dragon_JP = "ドラゴン";
        public const string Dragon_CO = "드래곤";
        public const string Dragon_CN = "龙";
        public const string ImgColorDragon = "#738B62";
        public const string InfoColorDragon = "#1477D2";
        public const string TypeColorDragon = "#096DC4";

        public const string Water_FR = "Eau";
        public const string Water_EN = "Water";
        public const string Water_ES = "Aqua";
        public const string Water_IT = "Acqua";
        public const string Water_DE = "Wasser";
        public const string Water_RU = "Вода";
        public const string Water_JP = "みず";
        public const string Water_CO = "물";
        public const string Water_CN = "水";
        public const string ImgColorWater = "#0E9FB0";
        public const string InfoColorWater = "#579BE1";
        public const string TypeColorWater = "#4D90D5";

        public const string Electric_FR = "Électrik";
        public const string Electric_EN = "Electric";
        public const string Electric_ES = "Eléctrico";
        public const string Electric_IT = "Elettro";
        public const string Electric_DE = "Elektro";
        public const string Electric_RU = "Электрический";
        public const string Electric_JP = "でんき";
        public const string Electric_CO = "전기";
        public const string Electric_CN = "电";
        public const string ImgColorElectric = "#0A4A6B";
        public const string InfoColorElectric = "#FDDD46";
        public const string TypeColorElectric = "#F3D23B";

        public const string Fairy_FR = "Fée";
        public const string Fairy_EN = "Fairy";
        public const string Fairy_ES = "Hada";
        public const string Fairy_IT = "Folletto";
        public const string Fairy_DE = "Fee";
        public const string Fairy_RU = "фея";
        public const string Fairy_JP = "フェアリー";
        public const string Fairy_CO = "페어리";
        public const string Fairy_CN = "妖精";
        public const string ImgColorFairy = "#6254BF";
        public const string InfoColorFairy = "#F999F1";
        public const string TypeColorFairy = "#EC8FE6";

        public const string Fire_FR = "Feu";
        public const string Fire_EN = "Fire";
        public const string Fire_ES = "Fuego";
        public const string Fire_IT = "Fueco";
        public const string Fire_DE = "Feuer";
        public const string Fire_RU = "Огонь";
        public const string Fire_JP = "ほのお";
        public const string Fire_CO = "불꽃";
        public const string Fire_CN = "火";
        public const string ImgColorFire = "#823C42";
        public const string InfoColorFire = "#FFA75F";
        public const string TypeColorFire = "#FF9C54";

        public const string Ice_FR = "Glace";
        public const string Ice_EN = "Ice";
        public const string Ice_ES = "Hielo";
        public const string Ice_IT = "Ghiaccio";
        public const string Ice_DE = "Eis";
        public const string Ice_RU = "Лед";
        public const string Ice_JP = "こおり";
        public const string Ice_CO = "얼음";
        public const string Ice_CN = "冰";
        public const string ImgColorIce = "#1E7EA2";
        public const string InfoColorIce = "#7BD9C8";
        public const string TypeColorIce = "#74CEC0";

        public const string Bug_FR = "Insecte";
        public const string Bug_EN = "Bug";
        public const string Bug_ES = "Bicho";
        public const string Bug_IT = "Coleottero";
        public const string Bug_DE = "Käfer";
        public const string Bug_RU = "насекомое";
        public const string Bug_JP = "むし";
        public const string Bug_CO = "벌레";
        public const string Bug_CN = "虫";
        public const string ImgColorBug = "#35995A";
        public const string InfoColorBug = "#9BCC38";
        public const string TypeColorBug = "#90C12C";

        public const string Normal_FR = "Normal";
        public const string Normal_EN = "Normal";
        public const string Normal_ES = "Normal";
        public const string Normal_IT = "Normale";
        public const string Normal_DE = "Normal";
        public const string Normal_RU = "Обычный";
        public const string Normal_JP = "ノーマル";
        public const string Normal_CO = "노말";
        public const string Normal_CN = "一般";
        public const string ImgColorNormal = "#D1C097";
        public const string InfoColorNormal = "#9BA3AB";
        public const string TypeColorNormal = "#9099A1";

        public const string Grass_FR = "Plante";
        public const string Grass_EN = "Grass";
        public const string Grass_ES = "Planta";
        public const string Grass_IT = "Erba";
        public const string Grass_DE = "Pflanze";
        public const string Grass_RU = "Трава";
        public const string Grass_JP = "くさ";
        public const string Grass_CO = "풀";
        public const string Grass_CN = "草";
        public const string ImgColorGrass = "#B4D553";
        public const string InfoColorGrass = "#6BC563";
        public const string TypeColorGrass = "#63BB5B";

        public const string Poison_FR = "Poison";
        public const string Poison_EN = "Poison";
        public const string Poison_ES = "Venemo";
        public const string Poison_IT = "Veleno";
        public const string Poison_DE = "Gift";
        public const string Poison_RU = "Яд";
        public const string Poison_JP = "どく";
        public const string Poison_CO = "독";
        public const string Poison_CN = "毒";
        public const string ImgColorPoison = "#371880";
        public const string InfoColorPoison = "#B474D1";
        public const string TypeColorPoison = "#AB6AC8";

        public const string Psychic_FR = "Psy";
        public const string Psychic_EN = "Psychic";
        public const string Psychic_ES = "Psíquico";
        public const string Psychic_IT = "Psico";
        public const string Psychic_DE = "Psycho";
        public const string Psychic_RU = "психический";
        public const string Psychic_JP = "エスパー";
        public const string Psychic_CO = "에스퍼";
        public const string Psychic_CN = "超能力";
        public const string ImgColorPsychic = "#482769";
        public const string InfoColorPsychic = "#FF7A7F";
        public const string TypeColorPsychic = "#F97176";

        public const string Rock_FR = "Roche";
        public const string Rock_EN = "Rock";
        public const string Rock_ES = "Roca";
        public const string Rock_IT = "Roccia";
        public const string Rock_DE = "Gestein";
        public const string Rock_RU = "Гибралтар";
        public const string Rock_JP = "いわ";
        public const string Rock_CO = "바위";
        public const string Rock_CN = "岩石";
        public const string ImgColorRock = "#96969E";
        public const string InfoColorRock = "#D1C194";
        public const string TypeColorRock = "#C7B78B";

        public const string Ground_FR = "Sol";
        public const string Ground_EN = "Ground";
        public const string Ground_ES = "Tierra";
        public const string Ground_IT = "Terra";
        public const string Ground_DE = "Boden";
        public const string Ground_RU = "Земля";
        public const string Ground_JP = "じめん";
        public const string Ground_CO = "땅";
        public const string Ground_CN = "地面";
        public const string ImgColorGround = "#D2A97F";
        public const string InfoColorGround = "#E5804F";
        public const string TypeColorGround = "#D97746";

        public const string Ghost_FR = "Spectre";
        public const string Ghost_EN = "Ghost";
        public const string Ghost_ES = "Fantasma";
        public const string Ghost_IT = "Spettro";
        public const string Ghost_DE = "Geist";
        public const string Ghost_RU = "Призрак";
        public const string Ghost_JP = "ゴースト";
        public const string Ghost_CO = "고스트";
        public const string Ghost_CN = "幽灵";
        public const string ImgColorGhost = "#153D67";
        public const string InfoColorGhost = "#5F72B4";
        public const string TypeColorGhost = "#5269AC";

        public const string Dark_FR = "Ténèbres";
        public const string Dark_EN = "Dark";
        public const string Dark_ES = "Siniestro";
        public const string Dark_IT = "Buio";
        public const string Dark_DE = "Unlicht";
        public const string Dark_RU = "темно";
        public const string Dark_JP = "あく";
        public const string Dark_CO = "악";
        public const string Dark_CN = "恶";
        public const string ImgColorDark = "#0C1435";
        public const string InfoColorDark = "#635C6F";
        public const string TypeColorDark = "#5A5366";

        public const string Flying_FR = "Vol";
        public const string Flying_EN = "Flying";
        public const string Flying_ES = "Volador";
        public const string Flying_IT = "Volante";
        public const string Flying_DE = "Flug";
        public const string Flying_RU = "Летающий";
        public const string Flying_JP = "ひこう";
        public const string Flying_CO = "비행";
        public const string Flying_CN = "飞行";
        public const string ImgColorFlying = "#D4C49A";
        public const string InfoColorFlying = "#98B3E9";
        public const string TypeColorFlying = "#92AADE";
        #endregion

        #region WhenEvolution
        #region Niveau //OK
        public const string Level_Format_EN = "Level {0}";
        public const string Level_Format_ES = "Nivel {0}";
        public const string Level_Format_IT = "Livello {0}";
        public const string Level_Format_DE = "Eben {0}";
        public const string Level_Format_RU = "{0} Yровень";
        public const string Level_Format_CO = "수준 {0}";
        public const string Level_Format_CN = "{0} 等级";
        public const string Level_Format_JP = "レベル {0}";

        public const string PID_FR = "dépend d'un numéro aléatoire calculé à la capture, le PID";
        public const string PID_EN = "depends on a random number calculated on capture, the PID";
        public const string PID_ES = "depende de un número aleatorio calculado en la captura, el PID";
        public const string PID_IT = "dipende da un numero casuale calcolato sulla cattura, il PID";
        public const string PID_DE = "hängt von einer Zufallszahl ab, die bei der Erfassung berechnet wird, der PID";
        public const string PID_RU = "зависит от случайного числа, вычисляемого при захвате, PID";
        public const string PID_CO = "캡처 시 계산된 난수, PID에 따라 다름";
        public const string PID_CN = "取决于捕获时计算的随机数，PID";
        public const string PID_JP = "キャプチャ時に計算された乱数、PID に依存します";

        public const string ASD_FR = "si Attaque > Défense";
        public const string ASD_EN = "if Attack > Defense";
        public const string ASD_ES = "si Ataque > Defensa";
        public const string ASD_IT = "se Attacco > Difesa";
        public const string ASD_DE = "wenn Angriff > Verteidigung";
        public const string ASD_RU = "если атака > защита";
        public const string ASD_CO = "공격 > 방어라면";
        public const string ASD_CN = "如果攻击 > 防御";
        public const string ASD_JP = "攻撃 > 防御の場合";

        public const string AID_FR = "si Attaque < Défense";
        public const string AID_EN = "if Attack < Defense";
        public const string AID_ES = "si Ataque < Defensa";
        public const string AID_IT = "se Attacco < Difesa";
        public const string AID_DE = "wenn Angriff < Verteidigung";
        public const string AID_RU = "если атака < защита";
        public const string AID_CO = "공격 < 방어라면";
        public const string AID_CN = "如果攻击 < 防御";
        public const string AID_JP = "攻撃 < 防御の場合";

        public const string AED_FR = "si Défense = l'Attaque";
        public const string AED_EN = "if Attack = Defense";
        public const string AED_ES = "si Ataque = Defensa";
        public const string AED_IT = "se Attacco = Difesa";
        public const string AED_DE = "wenn Angriff = Verteidigung";
        public const string AED_RU = "если атака = защита";
        public const string AED_CO = "공격 = 방어라면";
        public const string AED_CN = "如果攻击 = 防御";
        public const string AED_JP = "攻撃 = 防御の場合";

        public const string PIB_FR = "avec un emplacement libre dans l'équipe et une Pokéball dans le sac";
        public const string PIB_EN = "with a free slot in the team and a Pokeball in the bag";
        public const string PIB_ES = "con un espacio libre en el equipo y una Pokébola en la bolsa";
        public const string PIB_IT = "con uno spazio libero nella squadra e una Pokeball nella borsa";
        public const string PIB_DE = "mit einem freien Platz im Team und einem Pokeball in der Tasche";
        public const string PIB_RU = "со свободным слотом в команде и покеболом в сумке";
        public const string PIB_CO = "팀에 무료 슬롯이 있고 가방에 포켓볼이 있습니다.";
        public const string PIB_CN = "队伍中有一个空位，袋子里有一个精灵球";
        public const string PIB_JP = "チームに空きスロットがあり、バッグにポケボールが入っています";

        public const string FM_FR = "femelle";
        public const string FM_EN = "female";
        public const string FM_ES = "femenino";
        public const string FM_IT = "femmina";
        public const string FM_DE = "weiblich";
        public const string FM_RU = "женский";
        public const string FM_CO = "여자";
        public const string FM_CN = "女性";
        public const string FM_JP = "女性";

        public const string M_FR = "mâle";
        public const string M_EN = "male";
        public const string M_ES = "masculino";
        public const string M_IT = "maschio";
        public const string M_DE = "männlich";
        public const string M_RU = "мужчина";
        public const string M_CO = "남성";
        public const string M_CN = "男性";
        public const string M_JP = "男";

        public const string D_FR = "le jour";
        public const string D_EN = "the day";
        public const string D_ES = "el día";
        public const string D_IT = "il giorno";
        public const string D_DE = "der Tag";
        public const string D_RU = "день";
        public const string D_CO = "그 날";
        public const string D_CN = "那天";
        public const string D_JP = "その日";

        public const string N_FR = "la nuit";
        public const string N_EN = "the night";
        public const string N_ES = "la noche";
        public const string N_IT = "la notte";
        public const string N_DE = "die Nacht";
        public const string N_RU = "ночь";
        public const string N_CO = "밤";
        public const string N_CN = "夜晚";
        public const string N_JP = "夜";

        public const string DLUL_FR = "le jour(sauf Lune et Ultra-Lune)";
        public const string DLUL_EN = "the day (except Moon and Ultra-Moon)";
        public const string DLUL_ES = "el día (excepto Luna y Ultraluna)";
        public const string DLUL_IT = "il giorno (tranne Luna e Ultraluna)";
        public const string DLUL_DE = "der Tag (außer Mond und Ultra-Mond)";
        public const string DLUL_RU = "день (кроме Луны и Ультра-Луны)";
        public const string DLUL_CO = "그 날 (문 및 울트라문 제외)";
        public const string DLUL_CN = "那天 (除了月球和超月球)";
        public const string DLUL_JP = "その日 (ムーンとウルトラムーンを除く)";

        public const string NSUS_FR = "la nuit(sauf Soleil et Ultra-Soleil)";
        public const string NSUS_EN = "at night (except Sun and Ultra-Sun)";
        public const string NSUS_ES = "la noche (excepto Sol y Ultra-Sol)";
        public const string NSUS_IT = "la notte (tranne Sole e Ultrasole)";
        public const string NSUS_DE = "die Nacht (außer Sonne und Ultra-Sonne)";
        public const string NSUS_RU = "ночью (кроме Солнце и Ультра-Солнце)";
        public const string NSUS_CO = "밤 (썬 및 울트라썬 제외)";
        public const string NSUS_CN = "夜晚 (太阳和超太阳除外)";
        public const string NSUS_JP = "夜 (サンとウルトラサンを除く)";

        public const string CR_FR = "au crépuscule(Rocabot avec Tempo Perso uniquement)";
        public const string CR_EN = "at dusk (Rockruff with Own Tempo only)";
        public const string CR_ES = "al anochecer (Rockruff solo con Ritmo Propio)";
        public const string CR_IT = "al tramonto (Rockruff con Mente Locale)";
        public const string CR_DE = "in der Dämmerung (Wuffels mit Tempomacher)";
        public const string CR_RU = "в сумерках (Рокрафф с Своими Темпами)";
        public const string CR_CO = "황혼(마이페이스 가 있는 암멍이 만 해당)";
        public const string CR_CN = "黄昏时（仅限 我行我素 的 岩狗狗）";
        public const string CR_JP = "夕暮れ時 (イワンコ と マイペース のみ)";

        public const string OSA_FR = "en dehors d'Alola";
        public const string OSA_EN = "outside of Alola";
        public const string OSA_ES = "fuera de Alola";
        public const string OSA_IT = "fuori Alola";
        public const string OSA_DE = "außerhalb von Alola";
        public const string OSA_RU = "за пределами Алолы";
        public const string OSA_CO = "알로라 외부";
        public const string OSA_CN = "阿罗拉以外";
        public const string OSA_JP = "アローラの外";

        public const string CB_FR = "avec la console retournée";
        public const string CB_EN = "with the console turned over";
        public const string CB_ES = "con la consola volteada";
        public const string CB_IT = "con la console capovolta";
        public const string CB_DE = "bei umgedrehter Konsole";
        public const string CB_RU = "с перевернутой консолью";
        public const string CB_CO = "콘솔을 뒤집은 채로";
        public const string CB_CN = "将控制台翻过来";
        public const string CB_JP = "コンソールをひっくり返した状態";

        public const string SPN_FR = "si de nature Pudique, Assuré, Calme, Prudent, Gentil, Solo, Doux, Modeste, Discret, Relax, Sérieux ou Timide";
        public const string SPN_EN = "if by nature Modest, Assertive, Calm, Cautious, Kind, Solo, Gentle, Modest, Discreet, Relaxed, Serious or Shy";
        public const string SPN_ES = "si por naturaleza Modesto, Asertivo, Calmado, Cauteloso, Amable, Solitario, Amable, Modesto, Discreto, Relajado, Serio o Tímido";
        public const string SPN_IT = "se per natura modesto, assertivo, calmo, cauto, gentile, solitario, gentile, modesto, discreto, rilassato, serio o timido";
        public const string SPN_DE = "ob von Natur aus bescheiden, durchsetzungsfähig, ruhig, vorsichtig, freundlich, solo, sanft, bescheiden, diskret, entspannt, ernst oder schüchtern";
        public const string SPN_RU = "если по натуре Скромный, Напористый, Спокойный, Осторожный, Добрый, Соло, Нежный, Скромный, Сдержанный, Расслабленный, Серьезный или Застенчивый";
        public const string SPN_CO = "천성적으로 겸손하고, 독단적이며, 침착하고, 신중하고, 친절하고, 혼자 있고, 온화하고, 겸손하고, 신중하고, 편안하고, 진지하거나 수줍음";
        public const string SPN_CN = "如果天生谦虚、自信、冷静、谨慎、善良、独奏、温柔、谦虚、谨慎、放松、严肃或害羞";
        public const string SPN_JP = "本質的に謙虚、自己主張、冷静、用心深い、親切、独善的、穏やか、謙虚、控えめ、リラックス、真面目、または恥ずかしがり屋の場合";

        public const string WN_FR = "si de nature Bizarre, Brave, Docile, Foufou, Hardi, Jovial, Lâche, Malin, Malpoli, Mauvais, Naïf, Pressé ou Rigide";
        public const string WN_EN = "if by nature Bizarre, Brave, Docile, Foufou, Bold, Jovial, Cowardly, Clever, Rude, Bad, Naive, Hasty or Rigid by nature";
        public const string WN_ES = "si es bizarro, valiente, dócil, tonto, audaz, jovial, cobarde, inteligente, grosero, malo, ingenuo, apresurado o rígido por naturaleza";
        public const string WN_IT = "se bizzarro, coraggioso, docile, foufou, audace, gioviale, codardo, intelligente, maleducato, cattivo, ingenuo, frettoloso o rigido per natura";
        public const string WN_DE = "ob sie von Natur aus bizarr, mutig, fügsam, Foufou, mutig, fröhlich, feige, schlau, unhöflich, schlecht, naiv, hastig oder starr sind";
        public const string WN_RU = "если по натуре Странный, Храбрый, Послушный, Шустрый, Смелый, Веселый, Трусливый, Умный, Грубый, Плохой, Наивный, Поспешный или Жесткий";
        public const string WN_CO = "기괴하고, 용감하고, 유순하고, 푸푸하고, 대담하고, 유쾌하고, 겁쟁이, 영리하고, 무례하고, 나쁩니다, 천성적으로 순진하거나 성급하거나 완고한 경우";
        public const string WN_CN = "如果生性古怪、勇敢、温顺、富夫、大胆、快活、懦弱、聪明、粗鲁、坏、天真、仓促或刚硬";
        public const string WN_JP = "奇妙、勇敢、従順、ふざけた、大胆、陽気、臆病、賢い、無礼、悪い、純真、せっかち、硬直の場合";

        public const string DPIT_FR = "avec un Pokémon Ténèbres dans l'équipe";
        public const string DPIT_EN = "with a Dark Pokemon on the team";
        public const string DPIT_ES = "con un Pokemon Oscuro en el equipo";
        public const string DPIT_IT = "con un Pokemon Oscuro nella squadra";
        public const string DPIT_DE = "mit einem dunklen Pokemon im Team";
        public const string DPIT_RU = "с темным покемоном в команде";
        public const string DPIT_CO = "팀에 다크 포켓몬과 함께";
        public const string DPIT_CN = "队伍中有一只黑暗宝可梦";
        public const string DPIT_JP = "チームにダークポケモンがいる";

        public const string OSG_FR = "en dehors de Galar";
        public const string OSG_EN = "outside Galar";
        public const string OSG_ES = "fuera de Galar";
        public const string OSG_IT = "fuori Galar";
        public const string OSG_DE = "außerhalb von Galar";
        public const string OSG_RU = "за пределами Галара";
        public const string OSG_CO = "갈라르 외부";
        public const string OSG_CN = "伽勒尔外";
        public const string OSG_JP = "ガラル外";

        public const string AG_FR = "à Galar";
        public const string AG_EN = "in Galar";
        public const string AG_ES = "en Galar";
        public const string AG_IT = "a Galar";
        public const string AG_DE = "in Galar";
        public const string AG_RU = "в Галаре";
        public const string AG_CO = "갈라르에서";
        public const string AG_CN = "在伽勒尔";
        public const string AG_JP = "ガラルで";

        public const string NR_FR = "dans un lieu avec de la pluie naturelle";
        public const string NR_EN = "in a place with natural rain";
        public const string NR_ES = "en un lugar con lluvia natural";
        public const string NR_IT = "in un luogo con pioggia naturale";
        public const string NR_DE = "an einem Ort mit natürlichem Regen";
        public const string NR_RU = "в месте с естественным дождем";
        public const string NR_CO = "자연비가 내리는 곳";
        public const string NR_CN = "在有自然降雨的地方";
        public const string NR_JP = "自然の雨が降る場所で";

        public const string SUSE_FR = "Soleil, Ultra-Soleil et Épée";
        public const string SUSE_EN = "Sun, Ultra-Sun and Sword";
        public const string SUSE_ES = "Sol, Ultra-Sol y Espada";
        public const string SUSE_IT = "Sole, Ultrasole e Spada";
        public const string SUSE_DE = "Sonne, Ultra-Sonne und Schwert";
        public const string SUSE_RU = "Солнце, Ультра-Солнце и Меч";
        public const string SUSE_CO = "썬, 울트라썬, 소드";
        public const string SUSE_CN = "太阳、超太阳和剑";
        public const string SUSE_JP = "サン、ウルトラサン、ソード";

        public const string LULB_FR = "Lune, Ultra-Lune et Bouclier";
        public const string LULB_EN = "Moon, Ultra-Moon and Shield";
        public const string LULB_ES = "Luna, Ultraluna y Escudo";
        public const string LULB_IT = "Luna, Ultraluna e Scudo";
        public const string LULB_DE = "Mond, Ultramond und Schild";
        public const string LULB_RU = "Луна, Ультра-Луна и Щит";
        public const string LULB_CO = "문, 울트라문 및 실드";
        public const string LULB_CN = "月亮，超月亮和盾牌";
        public const string LULB_JP = "ムーン、ウルトラムーン、シールド";
        #endregion

        #region Méga-Evolution //OK
        public const string Venusaurite_FR = "Florizarrite";
        public const string Venusaurite_URL = "https://bulbapedia.bulbagarden.net/wiki/Venusaurite";
        public const string Venusaurite_RU = "Венузаврит";

        public const string Charizardite_X_FR = "Dracaufite X";
        public const string Charizardite_X_URL = "https://bulbapedia.bulbagarden.net/wiki/Charizardite_X";
        public const string Charizardite_X_RU = "Чаризардит X";

        public const string Charizardite_Y_FR = "Dracaufite Y";
        public const string Charizardite_Y_URL = "https://bulbapedia.bulbagarden.net/wiki/Charizardite_Y";
        public const string Charizardite_Y_RU = "Чаризардит Y";

        public const string Blastoisinite_FR = "Tortankite";
        public const string Blastoisinite_URL = "https://bulbapedia.bulbagarden.net/wiki/Blastoisinite";
        public const string Blastoisinite_RU = "Бластойзит";

        public const string Beedrillite_FR = "Dardargnite";
        public const string Beedrillite_URL = "https://bulbapedia.bulbagarden.net/wiki/Beedrillite";
        public const string Beedrillite_RU = "Бидриллит";

        public const string Pidgeotite_FR = "Roucarnagite";
        public const string Pidgeotite_URL = "https://bulbapedia.bulbagarden.net/wiki/Pidgeotite";
        public const string Pidgeotite_RU = "Пиджитит";

        public const string Alakazite_FR = "Alakazamite";
        public const string Alakazite_URL = "https://bulbapedia.bulbagarden.net/wiki/Alakazite";
        public const string Alakazite_RU = "Алаказит";

        public const string Slowbronite_FR = "Flagadossite";
        public const string Slowbronite_URL = "https://bulbapedia.bulbagarden.net/wiki/Slowbronite";
        public const string Slowbronite_RU = "Слоубронит";

        public const string Gengarite_FR = "Ectoplasmite";
        public const string Gengarite_URL = "https://bulbapedia.bulbagarden.net/wiki/Gengarite";
        public const string Gengarite_RU = "Генгарит";

        public const string Kangaskhanite_FR = "Kangourexite";
        public const string Kangaskhanite_URL = "https://bulbapedia.bulbagarden.net/wiki/Kangaskhanite";
        public const string Kangaskhanite_RU = "Кангасханит";

        public const string Pinsirite_FR = "Scarabruite";
        public const string Pinsirite_URL = "https://bulbapedia.bulbagarden.net/wiki/Pinsirite";
        public const string Pinsirite_RU = "Пинсирит";

        public const string Gyaradosite_FR = "Léviatorite";
        public const string Gyaradosite_URL = "https://bulbapedia.bulbagarden.net/wiki/Gyaradosite";
        public const string Gyaradosite_RU = "Гаярадосит";

        public const string Aerodactylite_FR = "Ptéraïte";
        public const string Aerodactylite_URL = "https://bulbapedia.bulbagarden.net/wiki/Aerodactylite";
        public const string Aerodactylite_RU = "Аэродактилит";

        public const string Mewtwonite_X_FR = "Mewtwoïte X";
        public const string Mewtwonite_X_URL = "https://bulbapedia.bulbagarden.net/wiki/Mewtwonite_X";
        public const string Mewtwonite_X_RU = "Мьютунит X";

        public const string Mewtwonite_Y_FR = "Mewtwoïte Y";
        public const string Mewtwonite_Y_URL = "https://bulbapedia.bulbagarden.net/wiki/Mewtwonite_Y";
        public const string Mewtwonite_Y_RU = "Мьютунит Y";

        public const string Ampharosite_FR = "Pharampite";
        public const string Ampharosite_URL = "https://bulbapedia.bulbagarden.net/wiki/Ampharosite";
        public const string Ampharosite_RU = "Амфаросит";

        public const string Steelixite_FR = "Steelixite";
        public const string Steelixite_URL = "https://bulbapedia.bulbagarden.net/wiki/Steelixite";
        public const string Steelixite_RU = "Стиликсит";

        public const string Scizorite_FR = "Cizayoxite";
        public const string Scizorite_URL = "https://bulbapedia.bulbagarden.net/wiki/Scizorite";
        public const string Scizorite_RU = "Скизорит";

        public const string Heracronite_FR = "Scarhinoïte";
        public const string Heracronite_URL = "https://bulbapedia.bulbagarden.net/wiki/Heracronite";
        public const string Heracronite_RU = "Геракронит";

        public const string Houndoominite_FR = "Démolossite";
        public const string Houndoominite_URL = "https://bulbapedia.bulbagarden.net/wiki/Houndoominite";
        public const string Houndoominite_RU = "Хундумит";

        public const string Tyranitarite_FR = "Tyranocivite";
        public const string Tyranitarite_URL = "https://bulbapedia.bulbagarden.net/wiki/Tyranitarite";
        public const string Tyranitarite_RU = "Тиранитарит";

        public const string Sceptilite_FR = "Jungkite";
        public const string Sceptilite_URL = "https://bulbapedia.bulbagarden.net/wiki/Sceptilite";
        public const string Sceptilite_RU = "Септайлит";

        public const string Blazikenite_FR = "Braségalite";
        public const string Blazikenite_URL = "https://bulbapedia.bulbagarden.net/wiki/Blazikenite";
        public const string Blazikenite_RU = "Блэйзикенит";

        public const string Swampertite_FR = "Laggronite";
        public const string Swampertite_URL = "https://bulbapedia.bulbagarden.net/wiki/Swampertite";
        public const string Swampertite_RU = "Свамперит";

        public const string Gardevoirite_FR = "Gardevoirite";
        public const string Gardevoirite_URL = "https://bulbapedia.bulbagarden.net/wiki/Gardevoirite";
        public const string Gardevoirite_RU = "Гардевуарит";

        public const string Sablenite_FR = "Ténéfixite";
        public const string Sablenite_URL = "https://bulbapedia.bulbagarden.net/wiki/Sablenite";
        public const string Sablenite_RU = "Сабленит";

        public const string Mawilite_FR = "Mysdibulite";
        public const string Mawilite_URL = "https://bulbapedia.bulbagarden.net/wiki/Mawilite";
        public const string Mawilite_RU = "Мавалит";

        public const string Aggronite_FR = "Galekingite";
        public const string Aggronite_URL = "https://bulbapedia.bulbagarden.net/wiki/Aggronite";
        public const string Aggronite_RU = "Агронит";

        public const string Medichamite_FR = "Charminite";
        public const string Medichamite_URL = "https://bulbapedia.bulbagarden.net/wiki/Medichamite";
        public const string Medichamite_RU = "Медичамит";

        public const string Manectite_FR = "Élecsprintite";
        public const string Manectite_URL = "https://bulbapedia.bulbagarden.net/wiki/Manectite";
        public const string Manectite_RU = "Манектит";

        public const string Sharpedonite_FR = "Sharpedite";
        public const string Sharpedonite_URL = "https://bulbapedia.bulbagarden.net/wiki/Sharpedonite";
        public const string Sharpedonite_RU = "Шарпедонит";

        public const string Cameruptite_FR = "Caméruptite";
        public const string Cameruptite_URL = "https://bulbapedia.bulbagarden.net/wiki/Cameruptite";
        public const string Cameruptite_RU = "Камераптит";

        public const string Altarianite_FR = "Altarïte";
        public const string Altarianite_URL = "https://bulbapedia.bulbagarden.net/wiki/Altarianite";
        public const string Altarianite_RU = "Алтарианит";

        public const string Banettite_FR = "Branettite";
        public const string Banettite_URL = "https://bulbapedia.bulbagarden.net/wiki/Banettite";
        public const string Banettite_RU = "Банеттит";

        public const string Absolite_FR = "Absolite";
        public const string Absolite_URL = "https://bulbapedia.bulbagarden.net/wiki/Absolite";
        public const string Absolite_RU = "Абсолит";

        public const string Glalitite_FR = "Oniglalite";
        public const string Glalitite_URL = "https://bulbapedia.bulbagarden.net/wiki/Glalitite";
        public const string Glalitite_RU = "Глэйлитит";

        public const string Salamencite_FR = "Drattakite";
        public const string Salamencite_URL = "https://bulbapedia.bulbagarden.net/wiki/Salamencite";
        public const string Salamencite_RU = "Саламенсит";

        public const string Metagrossite_FR = "Métalossite";
        public const string Metagrossite_URL = "https://bulbapedia.bulbagarden.net/wiki/Metagrossite";
        public const string Metagrossite_RU = "Метагроссит";

        public const string Latiasite_FR = "Latiasite";
        public const string Latiasite_URL = "https://bulbapedia.bulbagarden.net/wiki/Latiasite";
        public const string Latiasite_RU = "Латиасит";

        public const string Latiosite_FR = "Latiosite";
        public const string Latiosite_URL = "https://bulbapedia.bulbagarden.net/wiki/Latiosite";
        public const string Latiosite_RU = "Латиосит";

        public const string Lopunnite_FR = "Lockpinite";
        public const string Lopunnite_URL = "https://bulbapedia.bulbagarden.net/wiki/Lopunnite";
        public const string Lopunnite_RU = "Лопаннит";

        public const string Garchompite_FR = "Carchacrokite";
        public const string Garchompite_URL = "https://bulbapedia.bulbagarden.net/wiki/Garchompite";
        public const string Garchompite_RU = "Гарчомпит";

        public const string Lucarionite_FR = "Lucarite";
        public const string Lucarionite_URL = "https://bulbapedia.bulbagarden.net/wiki/Lucarionite";
        public const string Lucarionite_RU = "Лукарионит";

        public const string Abomasite_FR = "Blizzarite";
        public const string Abomasite_URL = "https://bulbapedia.bulbagarden.net/wiki/Abomasite";
        public const string Abomasite_RU = "Абомасит";

        public const string Galladite_FR = "Gallamïte";
        public const string Galladite_URL = "https://bulbapedia.bulbagarden.net/wiki/Galladite";
        public const string Galladite_RU = "Галлейдит";

        public const string Audinite_FR = "Nanméouïte";//Méga-Nanméouïe
        public const string Audinite_URL = "https://bulbapedia.bulbagarden.net/wiki/Audinite";
        public const string Audinite_RU = "Аудинит";

        public const string Diancite_FR = "Diancite";
        public const string Diancite_URL = "https://bulbapedia.bulbagarden.net/wiki/Diancite";
        public const string Diancite_RU = "Диансит";
        #endregion

        #region Pierre d'évolution //OK
        public const string MoonStone_FR = "Pierre Lune";
        public const string MoonStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Moon_Stone";
        public const string MoonStone_RU = "Лунный камень";

        public const string FireStone_FR = "Pierre Feu";
        public const string FireStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Fire_Stone";
        public const string FireStone_RU = "Огненный Камень";

        public const string LeafStone_FR = "Pierre Plante";
        public const string LeafStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Leaf_Stone";
        public const string LeafStone_RU = "Лиственный Камень";

        public const string WaterStone_FR = "Pierre Eau";
        public const string WaterStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Water_Stone";
        public const string WaterStone_RU = "Водный камень";

        public const string ThunderStone_FR = "Pierre Foudre";
        public const string ThunderStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Thunder_Stone";
        public const string ThunderStone_RU = "Громовой Камень";

        public const string SunStone_FR = "Pierre Soleil";
        public const string SunStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Sun_Stone";
        public const string SunStone_RU = "Солнечный камень";

        public const string ShinyStone_FR = "Pierre Éclat";
        public const string ShinyStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Shiny_Stone";
        public const string ShinyStone_RU = "Блестящий камень";

        public const string DuckStone_FR = "Pierre Nuit";
        public const string DuckStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Dusk_Stone";
        public const string DuckStone_RU = "Камень заката";

        public const string IceStone_FR = "Pierre Glace";
        public const string IceStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Ice_Stone";
        public const string IceStone_RU = "Ледяной камень";

        public const string DawnStone_FR = "Pierre Aube";
        public const string DawnStone_URL = "https://bulbapedia.bulbagarden.net/wiki/Dawn_Stone";
        public const string DawnStone_RU = "Камень рассвета";
        #endregion

        #region Echange //OK
        public const string Kings_Rock_FR = "Roche Royale";
        public const string Kings_Rock_URL = "https://bulbapedia.bulbagarden.net/wiki/King%27s_Rock";
        public const string Kings_Rock_RU = "Камень королей";

        public const string Metal_Coat_FR = "Peau Métal";
        public const string Metal_Coat_URL = "https://bulbapedia.bulbagarden.net/wiki/Metal_Coat";
        public const string Metal_Coat_RU = "Металическая оболочка";

        public const string Dragon_Scale_FR = "Écaille Draco";
        public const string Dragon_Scale_URL = "https://bulbapedia.bulbagarden.net/wiki/Dragon_Scale";
        public const string Dragon_Scale_RU = "Чешуя дракона";

        public const string Upgrade_FR = "Améliorator";
        public const string Upgrade_URL = "https://bulbapedia.bulbagarden.net/wiki/Upgrade";
        public const string Upgrade_RU = "Обновление";

        public const string Prism_Scale_FR = "Bel'Écaille / Monter un niveau avec 170 de Beauté (RSE / DPP / HGSS / ROSA / DEPS)";
        public const string Prism_Scale_URL = "https://bulbapedia.bulbagarden.net/wiki/Prism_Scale";
        public const string Prism_Scale_RU = "Призматическая чешуйка";

        public const string Deep_Sea_Tooth_FR = "Dent Océan";
        public const string Deep_Sea_Tooth_URL = "https://bulbapedia.bulbagarden.net/wiki/Deep_Sea_Tooth";
        public const string Deep_Sea_Tooth_RU = "Зуб со дна моря";

        public const string Deep_Sea_Scale_FR = "Écaille Océan";
        public const string Deep_Sea_Scale_URL = "https://bulbapedia.bulbagarden.net/wiki/Deep_Sea_Scale";
        public const string Deep_Sea_Scale_RU = "Чешуя со дна моря";

        public const string Shelmet_FR = "Escargaume";
        public const string Shelmet_URL = "https://bulbapedia.bulbagarden.net/wiki/Shelmet";
        public const string Shelmet_RU = "Шелмет";

        public const string Karrablast_FR = "Carabing";
        public const string Karrablast_URL = "https://bulbapedia.bulbagarden.net/wiki/Karrablast";
        public const string Karrablast_RU = "Каррабласт";

        public const string Sachet_FR = "Sachet Senteur";
        public const string Sachet_URL = "https://bulbapedia.bulbagarden.net/wiki/Sachet";
        public const string Sachet_RU = "Саше с ароматом";

        public const string Whipped_Dream_FR = "Chantibonbon";
        public const string Whipped_Dream_URL = "https://bulbapedia.bulbagarden.net/wiki/Whipped_Dream";
        public const string Whipped_Dream_RU = "Шантибонбон";

        public const string Protector_FR = "Protecteur";
        public const string Protector_URL = "https://bulbapedia.bulbagarden.net/wiki/Protector";
        public const string Protector_RU = "Протектор";

        public const string Electirizer_FR = "Électriseur";
        public const string Electirizer_URL = "https://bulbapedia.bulbagarden.net/wiki/Electirizer";
        public const string Electirizer_RU = "Электризатор";

        public const string Magmarizer_FR = "Magmariseur";
        public const string Magmarizer_URL = "https://bulbapedia.bulbagarden.net/wiki/Magmarizer";
        public const string Magmarizer_RU = "Магмаризатор";

        public const string Dubious_Disc_FR = "CD Douteux";
        public const string Dubious_Disc_URL = "https://bulbapedia.bulbagarden.net/wiki/Dubious_Disc";
        public const string Dubious_Disc_RU = "Сомнительный диск";

        public const string Reaper_Cloth_FR = "Tissu Fauche";
        public const string Reaper_Cloth_URL = "https://bulbapedia.bulbagarden.net/wiki/Reaper_Cloth";
        public const string Reaper_Cloth_RU = "Ткань жнеца";
        #endregion

        #region LevelUp //OK
        public const string LvlUpWith_FR = "Monter un niveau avec";
        public const string LvlUpWith_Format_EN = "Level up with {0}";
        public const string LvlUpWith_Format_ES = "Subir de nivel con {0}";
        public const string LvlUpWith_Format_IT = "Sali di livello con {0}";
        public const string LvlUpWith_Format_DE = "Steige mit {0} auf";
        public const string LvlUpWith_Format_RU = "Повысьте уровень с {0}";
        public const string LvlUpWith_Format_CO = "{0} 레벨업";
        public const string LvlUpWith_Format_CN = "使用 {0} 升級";
        public const string LvlUpWith_Format_JP = "{0}でレベルアップ";

        public const string LvlUpWith_Format_2_EN = "Level up with {0}, {1}";
        public const string LvlUpWith_Format_2_ES = "Subir de nivel con {0}, {1}";
        public const string LvlUpWith_Format_2_IT = "Sali di livello con {0}, {1}";
        public const string LvlUpWith_Format_2_DE = "Level aufsteigen mit {0}, {1}";
        public const string LvlUpWith_Format_2_RU = "Повысьте уровень с {0}, {1}";
        public const string LvlUpWith_Format_2_CO = "{0}, {1} 로 레벨 업";
        public const string LvlUpWith_Format_2_CN = "使用 {0}、{1} 升級";
        public const string LvlUpWith_Format_2_JP = "{0}、{1}でレベルアップ";

        public const string HighHappiness_FR = "bonheur élevé";
        public const string HighHappiness_EN = "hight happiness";
        public const string HighHappiness_ES = "alta felicidad";
        public const string HighHappiness_IT = "alta felicità";
        public const string HighHappiness_DE = "hohes Glück";
        public const string HighHappiness_RU = "высокое счастье";
        public const string HighHappiness_CO = "높은 행복";
        public const string HighHappiness_CN = "高幸福";
        public const string HighHappiness_JP = "高い幸福";

        public const string HighHappinessDay_FR = "bonheur élevé, le jour";
        public const string HighHappinessNight_FR = "bonheur élevé, la nuit";

        public const string InTeam_FR = "Rémoraid dans l'équipe";
        public const string Remoraid_URL = "https://bulbapedia.bulbagarden.net/wiki/Remoraid";
        public const string InTeam_Format_EN = "{0} in team";
        public const string InTeam_Format_ES = "{0} en equipo";
        public const string InTeam_Format_IT = "{0} in squadra";
        public const string InTeam_Format_DE = "{0} im Team";
        public const string InTeam_Format_RU = "{0} в команде";
        public const string InTeam_Format_CO = "팀에서 {0}";
        public const string InTeam_Format_CN = "{0} 在團隊中";
        public const string InTeam_Format_JP = "チームの{0}";

        public const string LvlUpLearn_FR = "Monter un niveau en ayant appris";
        public const string LvlUpLearn_Format_EN = "Level up having learned {0}";
        public const string LvlUpLearn_Format_ES = "Sube de nivel habiendo aprendido {0}";
        public const string LvlUpLearn_Format_IT = "Sali di livello dopo aver imparato {0}";
        public const string LvlUpLearn_Format_DE = "Steige auf, nachdem du {0} gelernt hast";
        public const string LvlUpLearn_Format_RU = "Повысьте уровень, изучив {0}";
        public const string LvlUpLearn_Format_CO = "{0}을(를) 배우고 레벨 업";
        public const string LvlUpLearn_Format_CN = "學會了升級{0}";
        public const string LvlUpLearn_Format_JP = "{0} を習得してレベルアップ";

        public const string MimicOutsideGalar_FR = "Copie(hors Galar)";
        public const string Mimic_FR = "l'attaque Copie";
        public const string Mimic_URL = "https://bulbapedia.bulbagarden.net/wiki/Mimic_(move)";
        public const string Mimic_RU = "Копия";

        public const string Double_Hit_FR = "Coup Double";
        public const string Double_Hit_URL = "https://bulbapedia.bulbagarden.net/wiki/Double_Hit_(move)";
        public const string Double_Hit_RU = "Coup Double";

        public const string Rollout_FR = "Roulade";
        public const string Rollout_URL = "https://bulbapedia.bulbagarden.net/wiki/Rollout_(move)";
        public const string Rollout_RU = "Roulade";

        public const string Ancient_Power_FR = "Pouvoir Antique";
        public const string Ancient_Power_URL = "https://bulbapedia.bulbagarden.net/wiki/Ancient_Power_(move)";
        public const string Ancient_Power_RU = "Pouvoir Antique";

        public const string Stomp_FR = "Écrasement";
        public const string Stomp_URL = "https://bulbapedia.bulbagarden.net/wiki/Stomp_(move)";
        public const string Stomp_RU = "Écrasement";

        public const string Dragon_Pulse_FR = "Dracochoc";
        public const string Dragon_Pulse_URL = "https://bulbapedia.bulbagarden.net/wiki/Dragon_Pulse_(move)";
        public const string Dragon_Pulse_RU = "Dracochoc";

        public const string Taunt_FR = "Provoc";
        public const string Taunt_URL = "https://bulbapedia.bulbagarden.net/wiki/Taunt_(move)";
        public const string Taunt_RU = "Provoc";

        public const string LvlUpWH_FR = "Monter un niveau en tenant";

        public const string Oval_Stone_Format_EN = "Level up while holding an {0} during the day";
        public const string Oval_Stone_Format_ES = "Sube de nivel mientras mantienes un {0} durante el día";
        public const string Oval_Stone_Format_IT = "Sali di livello tenendo premuto un {0} durante il giorno";
        public const string Oval_Stone_Format_DE = "Steige im Level auf, während du einen {0} Tag hältst";
        public const string Oval_Stone_Format_RU = "Повышайте уровень, удерживая {0} в течение дня";
        public const string Oval_Stone_Format_CO = "{0} 일을 잡고 레벨업";
        public const string Oval_Stone_Format_CN = "白天按住 {0} 升級";
        public const string Oval_Stone_Format_JP = "日中に{0}を押しながらレベルアップ";

        public const string Oval_Stone_FR = "Monter un niveau en tenant une Pierre Ovale le jour";
        public const string Oval_Stone_URL = "https://bulbapedia.bulbagarden.net/wiki/Oval_Stone";
        public const string Oval_Stone_RU = "Овальный камень";

        public const string Razor_Format_EN = "Level up while holding {0} at night";
        public const string Razor_Format_ES = "Sube de nivel mientras mantienes {0} en la noche";
        public const string Razor_Format_IT = "Sali di livello tenendo premuto {0} di notte";
        public const string Razor_Format_DE = "Steige im Level auf, während du nachts {0} gedrückt hältst";
        public const string Razor_Format_RU = "Повышайте уровень, удерживая {0} ночью";
        public const string Razor_Format_CO = "밤에 {0} 키를 누른 상태에서 레벨 올리기";
        public const string Razor_Format_CN = "晚上按住 {0} 升級";
        public const string Razor_Format_JP = "夜に{0}を押しながらレベルアップ";

        public const string Razor_Claw_FR = "Monter un niveau en tenant Griffe Rasoir la nuit";
        public const string Razor_Claw_URL = "https://bulbapedia.bulbagarden.net/wiki/Razor_Claw";
        public const string Razor_Claw_RU = "Коготь бритвы";

        public const string Razor_Fang_FR = "Monter un niveau en tenant Croc Rasoir la nuit";
        public const string Razor_Fang_URL = "https://bulbapedia.bulbagarden.net/wiki/Razor_Fang";
        public const string Razor_Fang_RU = "Клык бритвы";

        public const string UseA_Format_EN = "Use a {0}";
        public const string UseA_Format_ES = "Usa un {0}";
        public const string UseA_Format_IT = "Usa un {0}";
        public const string UseA_Format_DE = "Verwenden Sie ein {0}";
        public const string UseA_Format_RU = "Используйте {0}";
        public const string UseA_Format_CO = "{0} 사용";
        public const string UseA_Format_CN = "使用 {0}";
        public const string UseA_Format_JP = "{0} を使用";

        public const string Galarica_Cuff_FR = "Utiliser un Bracelet Galanoa";
        public const string Galarica_Cuff_URL = "https://bulbapedia.bulbagarden.net/wiki/Galarica_Cuff";
        public const string Galarica_Cuff_RU = "Галарическая манжета";

        public const string Galarica_Wreath_FR = "Utiliser une Couronne Galanoa";
        public const string Galarica_Wreath_URL = "https://bulbapedia.bulbagarden.net/wiki/Galarica_Wreath";
        public const string Galarica_Wreath_RU = "Галарический венок";

        public const string NotEvolve_Format_EN = "{0} does not evolve";
        public const string NotEvolve_Format_ES = "{0} no evoluciona";
        public const string NotEvolve_Format_IT = "{0} non si evolve";
        public const string NotEvolve_Format_DE = "{0} entwickelt sich nicht";
        public const string NotEvolve_Format_RU = "{0} не развивается";
        public const string NotEvolve_Format_CO = "{0}은(는) 진화하지 않습니다.";
        public const string NotEvolve_Format_CN = "{0} 沒有進化";
        public const string NotEvolve_Format_JP = "{0} は進化しません";
        public const string Phione_FR = "Phione";
        public const string Phione_URL = "https://bulbapedia.bulbagarden.net/wiki/Phione_(Pok%C3%A9mon)";
        public const string Phione_RU = "Фион";

        public const string LvlUpLanakila_FR = "7G : Monter un niveau au Mont Lanakila";
        public const string LvlUpLanakila_EN = "7G : Level up on Mount Lanakila";
        public const string LvlUpLanakila_ES = "7G : Sube de nivel en el monte Lanakila";
        public const string LvlUpLanakila_IT = "7G : Sali di livello sul Monte Lanakila";
        public const string LvlUpLanakila_DE = "7G : Steige auf dem Berg Lanakila auf";
        public const string LvlUpLanakila_RU = "7G : Повышение уровня на горе Ланакила";
        public const string LvlUpLanakila_CO = "7G : 라나킬라 산에서 레벨업";
        public const string LvlUpLanakila_CN = "7G : 在拉納基拉山升級";
        public const string LvlUpLanakila_JP = "7G : ラナキラ山でレベルアップ";

        public const string Tart_Apple_FR = "Pomme Acidulée";
        public const string Tart_Apple_URL = "https://bulbapedia.bulbagarden.net/wiki/Tart_Apple";
        public const string Tart_Apple_RU = "терпкое яблоко";

        public const string Sweet_Apple_FR = "Pomme Sucrée";
        public const string Sweet_Apple_URL = "https://bulbapedia.bulbagarden.net/wiki/Sweet_Apple";
        public const string Sweet_Apple_RU = "сладкое яблоко";

        public const string Cracked_Pot_FR = "Théière Fêlée";
        public const string Cracked_Pot_URL = "https://bulbapedia.bulbagarden.net/wiki/Cracked_Pot";
        public const string Cracked_Pot_RU = "Треснувший горшок";

        public const string Critical_Hits_FR = "Faire 3 coups critiques en un seul combat";
        public const string Critical_Hits_EN = "Get 3 critical hits in a single fight";
        public const string Critical_Hits_ES = "Consigue 3 golpes críticos en una sola pelea";
        public const string Critical_Hits_IT = "Ottieni 3 colpi critici in un singolo combattimento";
        public const string Critical_Hits_DE = "Erziele 3 kritische Treffer in einem einzigen Kampf";
        public const string Critical_Hits_RU = "Получите 3 критических удара в одном бою";
        public const string Critical_Hits_CO = "한 전투에서 치명타 3회 달성";
        public const string Critical_Hits_CN = "在一場戰鬥中獲得 3 次暴擊";
        public const string Critical_Hits_JP = "1回の戦闘で3回クリティカルヒットを出す";

        public const string Mr_Mime = "Mime de Galar)";
        public const string Mr_Mime_FR = "Mr. Mime de Galar";
        public const string Mr_Mime_EN = "Galarian Mr. Mime";
        public const string Mr_Mime_ES = "Mr. Mime de Galar";
        public const string Mr_Mime_IT = "Mr. Mime di Galar";
        public const string Mr_Mime_DE = "Herr Pantomime von Galar";
        public const string Mr_Mime_RU = "Мистер Мим из Галара";
        public const string Mr_Mime_CO = "마임 드 갈라 씨";
        public const string Mr_Mime_CN = "加拉爾的啞劇先生";
        public const string Mr_Mime_JP = "ミメ・ド・ガラル氏";

        public const string SavageLands_FR = "Perdre 49 PV ou plus et passer sous l'arche en pierre dans la Fosse des Sables (Terres Sauvages)";
        public const string SavageLands_EN = "Lose 49 or more HP and pass under the stone archway in the Pit of the Sands(Savage Lands)";
        public const string SavageLands_ES = "Pierde 49 o más HP y pasa por debajo del arco de piedra en Pit of the Sands(Savage Lands)";
        public const string SavageLands_IT = "Perdi 49 o più HP e passa sotto l'arco di pietra nel Sand Pit (Savage Lands)";
        public const string SavageLands_DE = "Verliere 49 oder mehr HP und gehe unter dem steinernen Torbogen in der Sandgrube(Wilde Lande) hindurch.";
        public const string SavageLands_RU = "Потеряйте 49 или более HP и пройдите под каменной аркой в ​​Яме песков(Дикие земли).";
        public const string SavageLands_CO = "49 이상의 HP를 잃고 Pit of the Sands(Savage Lands)의 돌 아치 밑으로 통과";
        public const string SavageLands_CN = "失去 49 或更多生命值並通過沙坑（野蠻之地）的石拱門下";
        public const string SavageLands_JP = "49 以上の HP を失い、Pit of the Sands(Savage Lands) の石のアーチの下を通過する";

        public const string AfterFightOnePercent_FR = "après un combat (1 chance sur 100)";
        public const string AfterFightOnePercent_EN = "after a fight (1 in 100 chance)";
        public const string AfterFightOnePercent_ES = "después de una pelea (1 en 100 posibilidades)";
        public const string AfterFightOnePercent_IT = "dopo un litigio (1 possibilità su 100)";
        public const string AfterFightOnePercent_DE = "nach einem Kampf (1 zu 100 Chance)";
        public const string AfterFightOnePercent_RU = "после боя (1 шанс из 100)";
        public const string AfterFightOnePercent_CO = "싸움 후(1/100 확률)";
        public const string AfterFightOnePercent_CN = "打架后（100 分之一的机会）";
        public const string AfterFightOnePercent_JP = "戦いの後（100分の1の確率）";

        public const string AfterFightNineNinePercent_FR = "après un combat (99 chances sur 100)";
        public const string AfterFightNineNinePercent_EN = "after a fight (99 out of 100 chance)";
        public const string AfterFightNineNinePercent_ES = "después de una pelea (99 de 100 posibilidades)";
        public const string AfterFightNineNinePercent_IT = "dopo un litigio (99 possibilità su 100)";
        public const string AfterFightNineNinePercent_DE = "nach einem Kampf (99 von 100 Chance)";
        public const string AfterFightNineNinePercent_RU = "после боя (шанс 99 из 100)";
        public const string AfterFightNineNinePercent_CO = "싸움 후 (100중 99 확률)";
        public const string AfterFightNineNinePercent_CN = "战斗后（100 次机会中有 99 次";
        public const string AfterFightNineNinePercent_JP = "戦いの後（100分の99の確率";

        public const string LevelUpWorld_FR = "Faire monter d'un niveau alors que vous êtes plusieurs dans le même monde";
        public const string LevelUpWorld_EN = "Leveling up when there are several of you in the same world";
        public const string LevelUpWorld_ES = "Subir de nivel cuando sois varios en el mismo mundo";
        public const string LevelUpWorld_IT = "Salire di livello quando ci sono molti di voi nello stesso mondo";
        public const string LevelUpWorld_DE = "Steigen Sie auf, wenn mehrere von Ihnen in derselben Welt sind";
        public const string LevelUpWorld_RU = "Повышение уровня, когда вас несколько в одном мире";
        public const string LevelUpWorld_CO = "같은 세계에 여러 명이 있을 때 레벨 업";
        public const string LevelUpWorld_CN = "当你们中有几个人在同一个世界时升级";
        public const string LevelUpWorld_JP = "同じ世界に複数いる場合のレベルアップ";

        public const string LevelUpHyperdrill_FR = "Monter un niveau en ayant appris l'attaque Hyperceuse (99 chance sur 100)";
        public const string LevelUpHyperdrill_EN = "Level up having learned the Hyperdrill attack (99 out of 100 chance)";
        public const string LevelUpHyperdrill_ES = "Sube de nivel habiendo aprendido el ataque Hyperdrill (99 de 100 posibilidades)";
        public const string LevelUpHyperdrill_IT = "Sali di livello dopo aver appreso l'attacco Hyperdrill (99 possibilità su 100)";
        public const string LevelUpHyperdrill_DE = "Erreiche ein Level, nachdem du den Hyperdrill-Angriff erlernt hast (Chance von 99 von 100)";
        public const string LevelUpHyperdrill_RU = "Повысьте уровень, изучив атаку Hyperdrill (шанс 99 из 100)";
        public const string LevelUpHyperdrill_CO = "하이퍼드릴 공격을 배워서 레벨업 (99/100 확률)";
        public const string LevelUpHyperdrill_CN = "学习 超钻 攻击后升级（100 次机会中有 99 次）";
        public const string LevelUpHyperdrill_JP = "ハイパードリルの攻撃を習得してレベルアップ (99/100 の確率)";

        public const string LevelUpHyperdrillOnePercent_FR = "Monter un niveau en ayant appris l'attaque Hyperceuse (1 chance sur 100)";
        public const string LevelUpHyperdrillOnePercent_EN = "Level up having learned the Hyperdrill attack (1 in 100 chance)";
        public const string LevelUpHyperdrillOnePercent_ES = "Sube de nivel habiendo aprendido el ataque Hyperdrill (1 en 100 posibilidades)";
        public const string LevelUpHyperdrillOnePercent_IT = "Sali di livello dopo aver appreso l'attacco Hyperdrill (1 possibilità su 100)";
        public const string LevelUpHyperdrillOnePercent_DE = "Erreiche ein Level, nachdem du den Hyperdrill-Angriff erlernt hast (1 zu 100 Chance)";
        public const string LevelUpHyperdrillOnePercent_RU = "Повысьте уровень, изучив атаку Hyperdrill (1 шанс из 100)";
        public const string LevelUpHyperdrillOnePercent_CO = "하이퍼드릴 공격을 배워서 레벨업 (1/100 확률)";
        public const string LevelUpHyperdrillOnePercent_CN = "学习 超钻 攻击后升级（100 分之一的机会）";
        public const string LevelUpHyperdrillOnePercent_JP = "ハイパードリルの攻撃を習得してレベルアップ (100分の1の確率)";

        public const string WalkOneHundred_FR = "Marcher 1000 pas en mode En avant avec votre Pokémon puis le faire monter d'un niveau";
        public const string WalkOneHundred_EN = "Walk 1000 steps in Forward mode with your Pokémon and then level it up";
        public const string WalkOneHundred_ES = "Camina 1000 pasos en modo Adelante con tu Pokémon y luego sube de nivel";
        public const string WalkOneHundred_IT = "Fai 1000 passi in modalità Avanti con i tuoi Pokémon e poi sali di livello";
        public const string WalkOneHundred_DE = "Gehen Sie mit Ihrem Pokémon 1000 Schritte im Vorwärtsmodus und steigen Sie dann auf";
        public const string WalkOneHundred_RU = "Пройдите 1000 шагов в режиме «Вперед» со своим покемоном, а затем повысьте его уровень.";
        public const string WalkOneHundred_CO = "포워드 모드에서 포켓몬과 함께 1000걸음 걸은 후 레벨 업";
        public const string WalkOneHundred_CN = "与你的宝可梦一起在前进模式中行走 1000 步，然后升级";
        public const string WalkOneHundred_JP = "ポケモンと一緒にフォワード モードで 1000 歩歩き、レベルアップします。";

        public const string ArmorOfFortune_FR = "Armure de la Fortune";
        public const string ArmorOfFortune_EN = "Armor of Fortune";
        public const string ArmorOfFortune_ES = "Armadura de la fortuna";
        public const string ArmorOfFortune_IT = "Armatura della fortuna";
        public const string ArmorOfFortune_DE = "Rüstung des Glücks";
        public const string ArmorOfFortune_RU = "Броня удачи";
        public const string ArmorOfFortune_CO = "행운의 갑옷";
        public const string ArmorOfFortune_CN = "幸运之甲";
        public const string ArmorOfFortune_JP = "運命の鎧";

        public const string GrudgeArmor_FR = "Armure de la Rancune";
        public const string GrudgeArmor_EN = "Grudge Armor";
        public const string GrudgeArmor_ES = "Armadura de rencor";
        public const string GrudgeArmor_IT = "Armatura del rancore";
        public const string GrudgeArmor_DE = "Groll-Rüstung";
        public const string GrudgeArmor_RU = "Броня обиды";
        public const string GrudgeArmor_CO = "원한 갑옷";
        public const string GrudgeArmor_CN = "怨恨盔甲";
        public const string GrudgeArmor_JP = "グラッジアーマー";

        public const string RageFist_FR = "Faire monter d'un niveau après avoir utilisé 20 fois l'attaque Poing de Colère";
        public const string RageFist_EN = "Level up after using the Rage Fist attack 20 times";
        public const string RageFist_ES = "Sube de nivel después de usar el ataque Puño Furia 20 veces";
        public const string RageFist_IT = "Sali di livello dopo aver usato l'attacco Pugno Furibondo rabbia 20 volte";
        public const string RageFist_DE = "Erreiche eine Stufe, nachdem du den Angriff Zornesfaust 20 Mal verwendet hast";
        public const string RageFist_RU = "Повышение уровня после использования атаки «Кулак гнева» 20 раз";
        public const string RageFist_CO = "분노의 주먹 공격 20회 사용 후 레벨업";
        public const string RageFist_CN = "使用愤怒之拳攻击 20 次后升级";
        public const string RageFist_JP = "怒りの拳攻撃を20回使用してレベルアップ";

        public const string TwinBeam_FR = "Monter un niveau en ayant appris l'attaque Double Laser";
        public const string TwinBeam_EN = "Level up having learned the Twin Beam attack";
        public const string TwinBeam_ES = "Sube de nivel habiendo aprendido el ataque Láser Doble";
        public const string TwinBeam_IT = "Sali di livello dopo aver appreso l'attacco Doppioraggio";
        public const string TwinBeam_DE = "Steige im Level auf, nachdem du den Doppelstrahl Angriff erlernt hast";
        public const string TwinBeam_RU = "Повышайте уровень, изучив атаку Двойного лазера";
        public const string TwinBeam_CO = "더블 레이저 공격을 배워서 레벨 업";
        public const string TwinBeam_CN = "学习双激光攻击后升级";
        public const string TwinBeam_JP = "ダブルレーザー攻撃を習得してレベルアップ";

        public const string PawniardEvol_FR = "Faire monter d'un niveau en ayant tué 3 Sclaproie entourés de Scalpion";
        public const string PawniardEvol_EN = "Level up after killing 3 Bisharp surrounded by Pawniard";
        public const string PawniardEvol_ES = "Sube de nivel después de matar a 3 Bisharp rodeados por Pawniard";
        public const string PawniardEvol_IT = "Sali di livello dopo aver ucciso 3 Bisharp circondati da Pawniard";
        public const string PawniardEvol_DE = "Steige im Level auf, nachdem du 3 Caesurio getötet hast, die von Gladiantri umgeben sind";
        public const string PawniardEvol_RU = "Повышение уровня после убийства 3 Бишарп в окружении Скальпионов.";
        public const string PawniardEvol_CO = "자망칼에 둘러싸인 3 절각참를 죽인 후 레벨 업";
        public const string PawniardEvol_CN = "杀死 3 个被 駒刀小兵 包围的 劈斬司令 后升级";
        public const string PawniardEvol_JP = "スカルピオンに囲まれたスクラプロイを 3 体倒してレベルアップ";

        public const string GimmighoulEvol_FR = "Faire monter d'un niveau en ayant 999 pièces de Mordudor";
        public const string GimmighoulEvol_EN = "Level up by having 999 pieces of Gimmighoul";
        public const string GimmighoulEvol_ES = "Sube de nivel al tener 999 piezas de Gimmighoul";
        public const string GimmighoulEvol_IT = "Sali di livello avendo 999 pezzi di Gimmighoul";
        public const string GimmighoulEvol_DE = "Steige im Level auf, indem du 999 Gierspenst hast";
        public const string GimmighoulEvol_RU = "Повышайте уровень, имея 999 частей Мордудора.";
        public const string GimmighoulEvol_CO = "모르두도르 조각 999개로 레벨 업";
        public const string GimmighoulEvol_CN = "拥有 999 件魔都多来升级";
        public const string GimmighoulEvol_JP = "モルデュドールを999個持ってレベルアップ";
        #endregion

        #region Reproduction //OK
        public const string Reproduction_Format_1_EN = "Reproduction of {0}";
        public const string Reproduction_Format_1_ES = "Reproducción de {0}";
        public const string Reproduction_Format_1_IT = "Riproduzione di {0}";
        public const string Reproduction_Format_1_DE = "Reproduktion von {0}";
        public const string Reproduction_Format_1_RU = "Размножение {0}";
        public const string Reproduction_Format_1_CO = "{0}의 복제";
        public const string Reproduction_Format_1_CN = "{0}的複制";
        public const string Reproduction_Format_1_JP = "{0}の複製";

        public const string Jynx_FR = "Lippoutou";
        public const string Jynx_URL = "https://bulbapedia.bulbagarden.net/wiki/Jynx_(Pok%C3%A9mon)";

        public const string Lucario_FR = "Lucario";
        public const string Lucario_URL = "https://bulbapedia.bulbagarden.net/wiki/Lucario_(Pok%C3%A9mon)";

        public const string Toxtricity_FR = "Salarsen";
        public const string Toxtricity_URL = "https://bulbapedia.bulbagarden.net/wiki/Toxtricity_(Pok%C3%A9mon)";
        public const string Toxtricity_RU = "Токстрисити";

        public const string Reproduction_Format_2_EN = "Reproduction of {0} or {1}";
        public const string Reproduction_Format_2_ES = "Reproducción de {0} o {1}";
        public const string Reproduction_Format_2_IT = "Riproduzione di {0} o {1}";
        public const string Reproduction_Format_2_DE = "Reproduktion von {0} oder {1}";
        public const string Reproduction_Format_2_RU = "Размножение {0} или {1}";
        public const string Reproduction_Format_2_CO = "{0} 또는 {1} 의 복제";
        public const string Reproduction_Format_2_CN = "{0} 或 {1} 的複制";
        public const string Reproduction_Format_2_JP = "{0} または {1} の複製";

        public const string PikachuRaichu_FR = "Pikachu ou Raichu";
        public const string Pikachu_URL = "https://bulbapedia.bulbagarden.net/wiki/Pikachu_(Pok%C3%A9mon)";
        public const string Raichu_URL = "https://bulbapedia.bulbagarden.net/wiki/Raichu_(Pok%C3%A9mon)";

        public const string JigglypuffWigglytuff_FR = "Rondoudou ou Grodoudou";
        public const string Jigglypuff_URL = "https://bulbapedia.bulbagarden.net/wiki/Jigglypuff_(Pok%C3%A9mon)";
        public const string Wigglytuff_URL = "https://bulbapedia.bulbagarden.net/wiki/Wigglytuff_(Pok%C3%A9mon)";

        public const string TogepiTogetic_FR = "Togetic ou Togekiss";
        public const string Togepi_URL = "https://bulbapedia.bulbagarden.net/wiki/Togepi_(Pok%C3%A9mon)";
        public const string Togetic_URL = "https://bulbapedia.bulbagarden.net/wiki/Togetic_(Pok%C3%A9mon)";

        public const string ElectabuzzElectivire_FR = "Élektek ou Élekable";
        public const string Electabuzz_URL = "https://bulbapedia.bulbagarden.net/wiki/Electabuzz_(Pok%C3%A9mon)";
        public const string Electivire_URL = "https://bulbapedia.bulbagarden.net/wiki/Electivire_(Pok%C3%A9mon)";
        public const string Electivire_RU = "Элективайр";

        public const string MagmarMagmortar_FR = "Magmar ou Maganon";
        public const string Magmar_URL = "https://bulbapedia.bulbagarden.net/wiki/Magmar_(Pok%C3%A9mon)";
        public const string Magmortar_URL = "https://bulbapedia.bulbagarden.net/wiki/Magmortar_(Pok%C3%A9mon)";
        public const string Magmortar_RU = "Магмортар";

        public const string Reproduction_Format_3_EN = "Reproduction of {0}, {1} or {2}";
        public const string Reproduction_Format_3_ES = "Reproducción de {0}, {1} o {2}";
        public const string Reproduction_Format_3_IT = "Riproduzione di {0}, {1} o {2}";
        public const string Reproduction_Format_3_DE = "Reproduktion von {0}, {1} order {2}";
        public const string Reproduction_Format_3_RU = "Размножение {0}, {1} или {2}";
        public const string Reproduction_Format_3_CO = "{0}, {1} 또는 {2}의 복제";
        public const string Reproduction_Format_3_CN = "{0}、{1} 或 {2} 的複制";
        public const string Reproduction_Format_3_JP = "{0}、{1}、または {2} の複製";

        public const string HitmonchanHitmonleeHitmontop_FR = "Tygnon, Kicklee ou Kapoera";
        public const string Hitmonchan_URL = "https://bulbapedia.bulbagarden.net/wiki/Hitmonchan_(Pok%C3%A9mon)";
        public const string Hitmonlee_URL = "https://bulbapedia.bulbagarden.net/wiki/Hitmonlee_(Pok%C3%A9mon)";
        public const string Hitmontop_URL = "https://bulbapedia.bulbagarden.net/wiki/Hitmontop_(Pok%C3%A9mon)";

        public const string ReproductionWithHeldByParent_Format_EN = "Reproduction with {0} held by a parent";
        public const string ReproductionWithHeldByParent_Format_ES = "Reproducción con {0} en manos de uno de los padres";
        public const string ReproductionWithHeldByParent_Format_IT = "Riproduzione con {0} detenuto da un genitore";
        public const string ReproductionWithHeldByParent_Format_DE = "Reproduktion mit {0} im Besitz eines Elternteils";
        public const string ReproductionWithHeldByParent_Format_RU = "Репродукция с {0} в руках родителей.";
        public const string ReproductionWithHeldByParent_Format_CO = "부모가 보유한 {0} 복제";
        public const string ReproductionWithHeldByParent_Format_CN = "與父母持有的 {0} 的複制";
        public const string ReproductionWithHeldByParent_Format_JP = "親が持つ{0}での繁殖";

        public const string Sea_Incense_FR = "Encens Mer";
        public const string Sea_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Sea_Incense";
        public const string Sea_Incense_RU = "Морские благовония";

        public const string Lax_Incense_FR = "Encens Doux";
        public const string Lax_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Lax_Incense";
        public const string Lax_Incense_RU = "Слабый ладан";

        public const string Pure_Incense_FR = "Encens Pur";
        public const string Pure_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Pure_Incense";
        public const string Pure_Incense_RU = "Чистый ладан";

        public const string Rock_Incense_FR = "Encens Roc";
        public const string Rock_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Rock_Incense";
        public const string Rock_Incense_RU = "Рок Ладан";

        public const string Odd_Incense_FR = "Encens Bizarre";
        public const string Odd_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Odd_Incense";
        public const string Odd_Incense_RU = "Странные благовония";

        public const string Luck_Incense_FR = "Encens Veine";
        public const string Luck_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Luck_Incense";
        public const string Luck_Incense_RU = "Ладан удачи";

        public const string Full_Incense_FR = "Encens Plein";
        public const string Full_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Full_Incense";
        public const string Full_Incense_RU = "Полный ладан";

        public const string Wave_Incense_FR = "Encens Vague";
        public const string Wave_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Wave_Incense";
        public const string Wave_Incense_RU = "Ладан волны";

        public const string Rose_Incense_FR = "Encens Fleur";
        public const string Rose_Incense_URL = "https://bulbapedia.bulbagarden.net/wiki/Rose_Incense";
        public const string Rose_Incense_RU = "Розовые благовония";
        #endregion
        #endregion

        #region Translation
        public const string Base_EU = "Base";
        public const string Base_RU = "База";

        public const string Level_FR = "Niveau";
        public const string Level_EN = "Level";
        public const string Level_ES = "Nivel";
        public const string Level_IT = "Livello";
        public const string Level_DE = "Eben";
        public const string Level_RU = "Yровень";
        public const string Level_CO = "수준";
        public const string Level_CN = "等级";
        public const string Level_JP = "レベル";

        public const string BasicData_FR = "Données de Base";
        public const string BasicData_EN = "Basic data";
        public const string BasicData_ES = "Base de datos";
        public const string BasicData_IT = "Dati di base";
        public const string BasicData_DE = "Grundinformationen";
        public const string BasicData_RU = "Основные данные";
        public const string BasicData_CO = "기본적인 정보";
        public const string BasicData_CN = "基本数据";
        public const string BasicData_JP = "基本データ";

        public const string SpecialAbility_FR = "Capacité Spéciale";
        public const string SpecialAbility_EN = "Special Ability";
        public const string SpecialAbility_ES = "Habilidad especial";
        public const string SpecialAbility_IT = "Abilità speciale";
        public const string SpecialAbility_DE = "Besondere Fähigkeit";
        public const string SpecialAbility_RU = "Специальные возможности";
        public const string SpecialAbility_CO = "특수 능력";
        public const string SpecialAbility_CN = "特殊能力";
        public const string SpecialAbility_JP = "特別な能力";

        public const string Statistics_FR = "Statistiques";
        public const string Statistics_EN = "Statistics";
        public const string Statistics_ES = "Estadísticas";
        public const string Statistics_IT = "Statistiche";
        public const string Statistics_DE = "Statistiken";
        public const string Statistics_RU = "Статистика";
        public const string Statistics_CO = "통계";
        public const string Statistics_CN = "能力";
        public const string Statistics_JP = "統計学";

        public const string Category_FR = "Catégorie";
        public const string Category_EN = "Category";
        public const string Category_ES = "Categoría";
        public const string Category_IT = "Categoria";
        public const string Category_DE = "Kategorie";
        public const string Category_RU = "категория";
        public const string Category_CO = "분류";
        public const string Category_CN = "分类";
        public const string Category_JP = "特性";

        public const string Height_FR = "Taille";
        public const string Height_EN = "Height";
        public const string Height_ES = "Altura";
        public const string Height_IT = "Altezza";
        public const string Height_DE = "Größe";
        public const string Height_RU = "высота";
        public const string Height_CO = "키";
        public const string Height_CN = "身高";
        public const string Height_JP = "高さ";

        public const string Weight_FR = "Poids";
        public const string Weight_EN = "Weight";
        public const string Weight_ES = "Peso";
        public const string Weight_IT = "Peso";
        public const string Weight_DE = "Gewicht";
        public const string Weight_RU = "Масса";
        public const string Weight_CO = "몸무게";
        public const string Weight_CN = "体重";
        public const string Weight_JP = "重さ";

        public const string Skills_FR = "Talents";
        public const string Skills_EN = "Skills";
        public const string Skills_ES = "Habilidades";
        public const string Skills_IT = "Abilità";
        public const string Skills_DE = "Fähigkeiten";
        public const string Skills_RU = "Способности";
        public const string Skills_CO = "특성";
        public const string Skills_CN = "特性";
        public const string Skills_JP = "スキル";

        public const string HP_FR = "PV";
        public const string HP_EN = "HP";
        public const string HP_ES = "PS";
        public const string HP_IT = "PS";
        public const string HP_DE = "KP";
        public const string HP_RU = "HP";
        public const string HP_CO = "HP";
        public const string HP_CN = "HP";
        public const string HP_JP = "HP";

        public const string Atk_FR = "Att.";
        public const string Atk_EN = "Atk";
        public const string Atk_ES = "At.";
        public const string Atk_IT = "Att.";
        public const string Atk_DE = "Angr.";
        public const string Atk_RU = "Atk";
        public const string Atk_CO = "공격";
        public const string Atk_CN = "攻击";
        public const string Atk_JP = "こうげき";

        public const string Def_FR = "Def.";
        public const string Def_EN = "Def";
        public const string Def_ES = "Def.";
        public const string Def_IT = "Dif.";
        public const string Def_DE = "Vert.";
        public const string Def_RU = "Def";
        public const string Def_CO = "방어";
        public const string Def_CN = "防御";
        public const string Def_JP = "ぼうぎょ";

        public const string SpAtk_FR = "Att. Sp.";
        public const string SpAtk_EN = "Sp. Atk";
        public const string SpAtk_ES = "At. Esp.";
        public const string SpAtk_IT = "Att. Sp.";
        public const string SpAtk_DE = "Sp. Ang.";
        public const string SpAtk_RU = "Sp. Atk";
        public const string SpAtk_CO = "특수 공격";
        public const string SpAtk_CN = "特攻";
        public const string SpAtk_JP = "とくこう";

        public const string SpDef_FR = "Def. Sp.";
        public const string SpDef_EN = "Sp. Def";
        public const string SpDef_ES = "Def. Esp.";
        public const string SpDef_IT = "Dif. Sp.";
        public const string SpDef_DE = "Sp. Ver.";
        public const string SpDef_RU = "Sp. Def";
        public const string SpDef_CO = "특수방어";
        public const string SpDef_CN = "特防";
        public const string SpDef_JP = "とくぼう";

        public const string Speed_FR = "Vit.";
        public const string Speed_EN = "Speed";
        public const string Speed_ES = "Vel.";//Velocidad
        public const string Speed_IT = "Vel.";//Velocità
        public const string Speed_DE = "Init.";
        public const string Speed_RU = "Speed";
        public const string Speed_CO = "속도";
        public const string Speed_CN = "速度";
        public const string Speed_JP = "すばやさ";

        public const string Evolution_FR = "Evolution";
        public const string Evolution_EN = "Evolution";
        public const string Evolution_ES = "Evolución";
        public const string Evolution_IT = "Evoluzione";
        public const string Evolution_DE = "Evolution";
        public const string Evolution_RU = "Эволюции";
        public const string Evolution_CO = "진화";
        public const string Evolution_CN = "进化";
        public const string Evolution_JP = "進化";

        public const string Weaknesses_FR = "Faiblesses";
        public const string Weaknesses_EN = "Weaknesses";
        public const string Weaknesses_ES = "Debilidades";
        public const string Weaknesses_IT = "Debolezze";
        public const string Weaknesses_DE = "Schwächen";
        public const string Weaknesses_RU = "Слабость";
        public const string Weaknesses_CO = "약점";
        public const string Weaknesses_CN = "弱点";
        public const string Weaknesses_JP = "弱点";

        public const string MegaEvolution_FR = "Méga Evolution";
        public const string MegaEvolution_EN = "Mega Evolution";
        public const string MegaEvolution_ES = "Megaevolución";
        public const string MegaEvolution_IT = "Megaevoluzione";
        public const string MegaEvolution_DE = "Mega-Entwicklung";
        public const string MegaEvolution_RU = "Мегаэволюция";
        public const string MegaEvolution_CO = "메가진화";
        public const string MegaEvolution_CN = "超级进化";
        public const string MegaEvolution_JP = "メガ進化";

        public const string GigantamaxForm_FR = "Forme Gigamax";
        public const string GigantamaxForm_EN = "Gigantamax Form";
        public const string GigantamaxForm_ES = "Forma Gigamax";
        public const string GigantamaxForm_IT = "Forma Gigamax";
        public const string GigantamaxForm_DE = "GigaDynamax-Form";
        public const string GigantamaxForm_RU = "гигамакс форма";
        public const string GigantamaxForm_CO = "기간타맥스 폼";
        public const string GigantamaxForm_CN = "巨无霸形态";
        public const string GigantamaxForm_JP = "ギガンタマックスフォーム";

        public const string AlolanForm_FR = "Forme d'Alola";
        public const string AlolanForm_EN = "Alolan Form";
        public const string AlolanForm_ES = "Forma De Alola";
        public const string AlolanForm_IT = "Forma Di Alola";
        public const string AlolanForm_DE = "Alola-Form";
        public const string AlolanForm_RU = "алола форма";
        public const string AlolanForm_CO = "알로라의 모습";
        public const string AlolanForm_CN = "阿罗拉的形态";
        public const string AlolanForm_JP = "アローラの姿";

        public const string GalarsForm_FR = "Forme de Galar";
        public const string GalarsForm_EN = "Galar's Form";
        public const string GalarsForm_ES = "Forma De Galar";
        public const string GalarsForm_IT = "Forma Di Galar";
        public const string GalarsForm_DE = "Galar-Form";
        public const string GalarsForm_RU = "галар форма";
        public const string GalarsForm_CO = "가라르의 모습";
        public const string GalarsForm_CN = "伽勒尔的形式";
        public const string GalarsForm_JP = "ガラルの姿";

        public const string HisuisForm_FR = "Forme D'Hisui";
        public const string HisuisForm_EN = "Hisui's Form";
        public const string HisuisForm_ES = "Forma De Hisui";
        public const string HisuisForm_IT = "Forma Di Hisui";
        public const string HisuisForm_DE = "Hisui-Form";
        public const string HisuisForm_RU = "Хисуи форма";
        public const string HisuisForm_CO = "히스이의 모습";
        public const string HisuisForm_CN = "翡翠的形态";
        public const string HisuisForm_JP = "ヒスイのフォーム";

        public const string Type_FR = "Type";
        public const string Type_EN = "Type";
        public const string Type_ES = "Tipo";
        public const string Type_IT = "Tipo";
        public const string Type_DE = "Typ";
        public const string Type_RU = "Тип";
        public const string Type_CO = "타입";
        public const string Type_CN = "属性";
        public const string Type_JP = "タイプ";

        public const string Gen_FR = "Gen";
        public const string Gen_EN = "Gen";
        public const string Gen_ES = "Gen";
        public const string Gen_IT = "Gen";
        public const string Gen_DE = "Gen";
        public const string Gen_RU = "Поколение";
        public const string Gen_CO = "세대";
        public const string Gen_CN = "创";
        public const string Gen_JP = "ゲン";

        public const string FilterShare_FR = "Filtrer Part";
        public const string FilterShare_EN = "Filter Share";
        public const string FilterShare_ES = "Compartir Filtro";
        public const string FilterShare_IT = "Filtra Condividi";
        public const string FilterShare_DE = "Teilen filtern";
        public const string FilterShare_RU = "Поделиться фильтром";
        public const string FilterShare_CO = "필터 공유";
        public const string FilterShare_CN = "过滤分享";
        public const string FilterShare_JP = "フィルタ共有";

        public const string Return_FR = "Retour";
        public const string Return_EN = "Return";
        public const string Return_ES = "Devolver";
        public const string Return_IT = "Ritorno";
        public const string Return_DE = "Zurückkehren";
        public const string Return_RU = "Возвращаться";
        public const string Return_CO = "반품";
        public const string Return_CN = "返回";
        public const string Return_JP = "戻る";

        public const string Filter_FR = "Filtrer";
        public const string Filter_EN = "Filter";
        public const string Filter_ES = "Filtrar";
        public const string Filter_IT = "Filtro";
        public const string Filter_DE = "Filter";
        public const string Filter_RU = "Фильтр";
        public const string Filter_CO = "필터";
        public const string Filter_CN = "筛选";
        public const string Filter_JP = "フィルター";

        public const string Female_FR = "Femelle";
        public const string Female_EN = "Female";
        public const string Female_ES = "Femenino";
        public const string Female_IT = "Femmina";
        public const string Female_DE = "Weiblich";
        public const string Female_RU = "женский";
        public const string Female_CO = "여성";
        public const string Female_CN = "女性";
        public const string Female_JP = "女性";

        public const string VariantGender_FR = "Variante Sexe";
        public const string VariantGender_EN = "Variant Gender";
        public const string VariantGender_ES = "Género Variante";
        public const string VariantGender_IT = "Genere Variante";
        public const string VariantGender_DE = "Variante Geschlecht";
        public const string VariantGender_RU = "Вариант пола";
        public const string VariantGender_CO = "변형 성별";
        public const string VariantGender_CN = "变体性别";
        public const string VariantGender_JP = "バリアント性別";

        public const string Variant_FR = "Variant";
        public const string Variant_EN = "Variant";
        public const string Variant_ES = "Variante";
        public const string Variant_IT = "Variante";
        public const string Variant_DE = "Variante";
        public const string Variant_RU = "Вариант";
        public const string Variant_CO = "변종";
        public const string Variant_CN = "变体";
        public const string Variant_JP = "変異体";

        public const string Stone_FR = "Pierre";
        public const string Stone_EN = "Stone";
        public const string Stone_ES = "Piedra";
        public const string Stone_IT = "Pietra";
        public const string Stone_DE = "Stein";
        public const string Stone_RU = "Камень";
        public const string Stone_CO = "결석";
        public const string Stone_CN = "结石";
        public const string Stone_JP = "結石";

        public const string Exchange_FR = "Echange";
        public const string Exchange_EN = "Exchange";
        public const string Exchange_ES = "Intercambio";
        public const string Exchange_IT = "Scambio";
        public const string Exchange_DE = "Austausch";
        public const string Exchange_RU = "Обмен";
        public const string Exchange_CO = "교환";
        public const string Exchange_CN = "交换";
        public const string Exchange_JP = "交換";

        public const string SwapWH_FR = "Échange en tenant";
        public const string SwapWH_EN = "Swap while holding";
        public const string SwapWH_ES = "Intercambiar mientras mantiene";
        public const string SwapWH_IT = "Scambia mentre tieni premuto";
        public const string SwapWH_DE = "Tauschen Sie, während Sie gedrückt halten";
        public const string SwapWH_RU = "Обмен, удерживая";
        public const string SwapWH_CO = "누른 상태에서 교환";
        public const string SwapWH_CN = "持有時交換";
        public const string SwapWH_JP = "押したまま交換";

        public const string ExchangeA_FR = "Échange contre";
        public const string ExchangeA_EN = "Exchange against";
        public const string ExchangeA_ES = "Intercambio contra";
        public const string ExchangeA_IT = "Scambio contro";
        public const string ExchangeA_DE = "Austausch gegen";
        public const string ExchangeA_RU = "Обмен против";
        public const string ExchangeA_CO = "교환 대상";
        public const string ExchangeA_CN = "交換反對";
        public const string ExchangeA_JP = "との交換";

        public const string LevelUp_FR = "Monter un niveau";
        public const string LevelUp_EN = "Level Up";
        public const string LevelUp_ES = "Elevar a mismo nivel";
        public const string LevelUp_IT = "Sali di livello";
        public const string LevelUp_DE = "Aufleveln";
        public const string LevelUp_RU = "Уровень повышен";
        public const string LevelUp_CO = "레벨 업";
        public const string LevelUp_CN = "升级";
        public const string LevelUp_JP = "レベルアップ";

        public const string Reproduction_FR = "Reproduction";
        public const string Reproduction_EN = "Reproduction";
        public const string Reproduction_ES = "Reproducción";
        public const string Reproduction_IT = "Riproduzione";
        public const string Reproduction_DE = "Reproduktion";
        public const string Reproduction_RU = "Воспроизведение";
        public const string Reproduction_CO = "생식";
        public const string Reproduction_CN = "再生产";
        public const string Reproduction_JP = "再生";

        public const string MegaEvolutionWith_FR = "Méga-Évolution avec une";
        public const string MegaEvolutionWith_EN = "Mega Evolution with"; //Avant
        public const string MegaEvolutionWith_ES = "Mega Evolución con"; //Avant
        public const string MegaEvolutionWith_IT = "Megaevoluzione con"; //Avant
        public const string MegaEvolutionWith_DE = "Mega Evolution mit"; //Avant
        public const string MegaEvolutionWith_RU = "Mega Evolyutsiya s"; //Avant
        public const string MegaEvolutionWith_CO = "함께하는 메가진화"; //Après
        public const string MegaEvolutionWith_CN = "超級進化"; //Après
        public const string MegaEvolutionWith_JP = "でメガ進化"; //Après

        public const string GigantamaxFormOf_FR = "Forme Gigamax de";
        public const string GigantamaxFormOf_EN = "Gigantamax Form of";
        public const string GigantamaxFormOf_ES = "Forma Gigamax de";
        public const string GigantamaxFormOf_IT = "Forma Gigamax di";
        public const string GigantamaxFormOf_DE = "GigaDynamax-Form von";
        public const string GigantamaxFormOf_RU = "Гигантамаксная форма";
        public const string GigantamaxFormOf_CO = "기간타맥스 형식";
        public const string GigantamaxFormOf_CN = "巨无霸形态";
        public const string GigantamaxFormOf_JP = "ギガンタマックス形態";
        #endregion

        #region Language
        public const string FR = "FR";
        public const string FR_Lib = "French";
        public const string EN = "EN";
        public const string EN_Lib = "English";
        public const string ES = "ES";
        public const string ES_Lib = "Spain";
        public const string ES_Lib_v2 = "Spanish";
        public const string IT = "IT";
        public const string IT_Lib = "Italian";
        public const string DE = "DE";
        public const string DE_Lib = "German";
        public const string RU = "RU";
        public const string RU_Lib = "Russian";
        public const string JP = "JP";
        public const string JP_Lib = "Japanese";
        public const string CO = "CO";
        public const string CO_Lib = "Korean";
        public const string CN = "CN";
        public const string CN_Lib = "Mandarin";
        #endregion

        #region Prismillon
        #region Archipel
        public const string Vivillon_Archipelago_Pattern_FR = "Prismillon Motif Archipel";
        public const string Vivillon_Archipelago_Pattern_EN = "Vivillon Archipelago Pattern";
        public const string Vivillon_Archipelago_Pattern_ES = "Vivillon Motivo Isleño";
        public const string Vivillon_Archipelago_Pattern_IT = "Vivillon Motivo Arcipelago";
        public const string Vivillon_Archipelago_Pattern_DE = "Vivillon Archipelmuster";
        public const string Vivillon_Archipelago_Pattern_RU = "Вивийон архипелаг рисунок";
        public const string Vivillon_Archipelago_Pattern_CO = "비비용 군도의 모양";
        public const string Vivillon_Archipelago_Pattern_CN = "彩粉蝶群島花紋";
        public const string Vivillon_Archipelago_Pattern_JP = "ビビヨン ぐんとうのもよう";
        public const string Vivillon_Archipelago_Pattern_UrlImg = "https://www.pokepedia.fr/images/1/12/Sprite_666_Archipel_HOME.png";
        public const string Vivillon_Archipelago_Pattern_UrlSprite = "https://www.pokepedia.fr/images/2/2d/Miniature_666_Archipel_XY.png";
        #endregion

        #region Banquise
        public const string Vivillon_Polar_Pattern_FR = "Prismillon Motif Banquise";
        public const string Vivillon_Polar_Pattern_EN = "Vivillon Polar Pattern";
        public const string Vivillon_Polar_Pattern_ES = "Vivillon Motivo Taiga";
        public const string Vivillon_Polar_Pattern_IT = "Vivillon Motivo Nordico";
        public const string Vivillon_Polar_Pattern_DE = "Vivillon Schneefeldmuster";
        public const string Vivillon_Polar_Pattern_RU = "Вивийон Полярный рисунок";
        public const string Vivillon_Polar_Pattern_CO = "비비용 설국의 모양";
        public const string Vivillon_Polar_Pattern_CN = "彩粉蝶雪國花紋";
        public const string Vivillon_Polar_Pattern_JP = "ビビヨン ゆきぐにのもよう";
        public const string Vivillon_Polar_Pattern_UrlImg = "https://www.pokepedia.fr/images/5/5d/Sprite_666_Banquise_HOME.png";
        public const string Vivillon_Polar_Pattern_UrlSprite = "https://www.pokepedia.fr/images/1/1c/Miniature_666_Banquise_XY.png";
        #endregion

        #region Blizzard
        public const string Vivillon_IceSnow_Pattern_FR = "Prismillon Motif Blizzard";
        public const string Vivillon_IceSnow_Pattern_EN = "Vivillon Icy Snow Pattern";
        public const string Vivillon_IceSnow_Pattern_ES = "Vivillon Motivo Polar";
        public const string Vivillon_IceSnow_Pattern_IT = "Vivillon Motivo Nevi Perenni";
        public const string Vivillon_IceSnow_Pattern_DE = "Vivillon Frostmuster";
        public const string Vivillon_IceSnow_Pattern_RU = "Вивийон Ледяной снег рисунок";
        public const string Vivillon_IceSnow_Pattern_CO = "비비용 빙설의 모양";
        public const string Vivillon_IceSnow_Pattern_CN = "彩粉蝶冰雪花紋";
        public const string Vivillon_IceSnow_Pattern_JP = "ビビヨン ひょうせつのもよう";
        public const string Vivillon_IceSnow_Pattern_UrlImg = "https://www.pokepedia.fr/images/d/d7/Sprite_666_Blizzard_HOME.png";
        public const string Vivillon_IceSnow_Pattern_UrlSprite = "https://www.pokepedia.fr/images/8/85/Miniature_666_Blizzard_XY.png";
        #endregion

        #region Cyclone
        public const string Vivillon_Monsoon_Pattern_FR = "Prismillon Motif Cyclone";
        public const string Vivillon_Monsoon_Pattern_EN = "Vivillon Monsoon Pattern";
        public const string Vivillon_Monsoon_Pattern_ES = "Vivillon Motivo Monzón";
        public const string Vivillon_Monsoon_Pattern_IT = "Vivillon Motivo Pluviale";
        public const string Vivillon_Monsoon_Pattern_DE = "Vivillon Monsunmuster";
        public const string Vivillon_Monsoon_Pattern_RU = "Вивийон муссон рисунок";
        public const string Vivillon_Monsoon_Pattern_CO = "비비용 스콜의 모양";
        public const string Vivillon_Monsoon_Pattern_CN = "彩粉蝶驟雨花紋";
        public const string Vivillon_Monsoon_Pattern_JP = "ビビヨン スコールのもよう";
        public const string Vivillon_Monsoon_Pattern_UrlImg = "https://www.pokepedia.fr/images/6/68/Sprite_666_Cyclone_HOME.png";
        public const string Vivillon_Monsoon_Pattern_UrlSprite = "https://www.pokepedia.fr/images/2/2a/Miniature_666_Cyclone_XY.png";
        #endregion

        #region Glace
        public const string Vivillon_Tundra_Pattern_FR = "Prismillon Motif Glace";
        public const string Vivillon_Tundra_Pattern_EN = "Vivillon Tundra Pattern";
        public const string Vivillon_Tundra_Pattern_ES = "Vivillon Motivo Tundra";
        public const string Vivillon_Tundra_Pattern_IT = "Vivillon Motivo Manto di Neve";
        public const string Vivillon_Tundra_Pattern_DE = "Vivillon Flockenmuster";
        public const string Vivillon_Tundra_Pattern_RU = "Вивийон тундра рисунок";
        public const string Vivillon_Tundra_Pattern_CO = "비비용 설원의 모양";
        public const string Vivillon_Tundra_Pattern_CN = "彩粉蝶雪原花紋";
        public const string Vivillon_Tundra_Pattern_JP = "ビビヨン せつげんのもよう";
        public const string Vivillon_Tundra_Pattern_UrlImg = "https://www.pokepedia.fr/images/1/10/Sprite_666_Glace_HOME.png";
        public const string Vivillon_Tundra_Pattern_UrlSprite = "https://www.pokepedia.fr/images/6/66/Miniature_666_Glace_XY.png";
        #endregion

        #region Jungle
        public const string Vivillon_Jungle_Pattern_FR = "Prismillon Motif Jungle";
        public const string Vivillon_Jungle_Pattern_EN = "Vivillon Jungle Pattern";
        public const string Vivillon_Jungle_Pattern_ES = "Vivillon Motivo Jungla";
        public const string Vivillon_Jungle_Pattern_IT = "Vivillon Motivo Giungla";
        public const string Vivillon_Jungle_Pattern_DE = "Vivillon Dschungelmuster";
        public const string Vivillon_Jungle_Pattern_RU = "Вивийон джунгли рисунок";
        public const string Vivillon_Jungle_Pattern_CO = "비비용 정글의 모양";
        public const string Vivillon_Jungle_Pattern_CN = "彩粉蝶熱帶雨林花紋";
        public const string Vivillon_Jungle_Pattern_JP = "ビビヨン ジャングルのもよう";
        public const string Vivillon_Jungle_Pattern_UrlImg = "https://www.pokepedia.fr/images/6/68/Sprite_666_Jungle_HOME.png";
        public const string Vivillon_Jungle_Pattern_UrlSprite = "https://www.pokepedia.fr/images/2/20/Miniature_666_Jungle_XY.png";
        #endregion

        #region Mangrove
        public const string Vivillon_Savanna_Pattern_FR = "Prismillon Motif Mangrove";
        public const string Vivillon_Savanna_Pattern_EN = "Vivillon Savanna Pattern";
        public const string Vivillon_Savanna_Pattern_ES = "Vivillon Motivo Pantano";
        public const string Vivillon_Savanna_Pattern_IT = "Vivillon Motivo Savana";
        public const string Vivillon_Savanna_Pattern_DE = "Vivillon Savannenmuster";
        public const string Vivillon_Savanna_Pattern_RU = "Вивийон саванна рисунок";
        public const string Vivillon_Savanna_Pattern_CO = "비비용 사바나의 모양";
        public const string Vivillon_Savanna_Pattern_CN = "彩粉蝶熱帶草原花紋";
        public const string Vivillon_Savanna_Pattern_JP = "ビビヨン サバンナのもよう";
        public const string Vivillon_Savanna_Pattern_UrlImg = "https://www.pokepedia.fr/images/8/80/Sprite_666_Mangrove_HOME.png";
        public const string Vivillon_Savanna_Pattern_UrlSprite = "https://www.pokepedia.fr/images/6/67/Miniature_666_Mangrove_XY.png";
        #endregion

        #region Métropole
        public const string Vivillon_Modern_Pattern_FR = "Prismillon Motif Métropole";
        public const string Vivillon_Modern_Pattern_EN = "Vivillon Modern Pattern";
        public const string Vivillon_Modern_Pattern_ES = "Vivillon Motivo Moderno";
        public const string Vivillon_Modern_Pattern_IT = "Vivillon Motivo Trendy";
        public const string Vivillon_Modern_Pattern_DE = "Vivillon Innovationsmuster";
        public const string Vivillon_Modern_Pattern_RU = "Вивийон современный рисунок";
        public const string Vivillon_Modern_Pattern_CO = "비비용 모던한 모양";
        public const string Vivillon_Modern_Pattern_CN = "彩粉蝶摩登花紋";
        public const string Vivillon_Modern_Pattern_JP = "ビビヨン モダンなもよう";
        public const string Vivillon_Modern_Pattern_UrlImg = "https://www.pokepedia.fr/images/c/c1/Sprite_666_M%C3%A9tropole_HOME.png";
        public const string Vivillon_Modern_Pattern_UrlSprite = "https://www.pokepedia.fr/images/1/15/Miniature_666_M%C3%A9tropole_XY.png";
        #endregion

        #region Sable
        public const string Vivillon_Sandstorm_Pattern_FR = "Prismillon Motif Sable";
        public const string Vivillon_Sandstorm_Pattern_EN = "Vivillon Sandstorm Pattern";
        public const string Vivillon_Sandstorm_Pattern_ES = "Vivillon Motivo Desierto";
        public const string Vivillon_Sandstorm_Pattern_IT = "Vivillon Motivo Sabbia";
        public const string Vivillon_Sandstorm_Pattern_DE = "Vivillon Sandmuster";
        public const string Vivillon_Sandstorm_Pattern_RU = "Вивийон песчаная буря рисунок";
        public const string Vivillon_Sandstorm_Pattern_CO = "비비용 사진의 모양";
        public const string Vivillon_Sandstorm_Pattern_CN = "彩粉蝶沙塵花紋";
        public const string Vivillon_Sandstorm_Pattern_JP = "ビビヨン さじんのもよう";
        public const string Vivillon_Sandstorm_Pattern_UrlImg = "https://www.pokepedia.fr/images/c/c9/Sprite_666_Sable_HOME.png";
        public const string Vivillon_Sandstorm_Pattern_UrlSprite = "https://www.pokepedia.fr/images/9/93/Miniature_666_Sable_XY.png";
        #endregion

        #region Soleil Levant
        public const string Vivillon_Ocean_Pattern_FR = "Prismillon Motif Soleil Levant";
        public const string Vivillon_Ocean_Pattern_EN = "Vivillon Ocean Pattern";
        public const string Vivillon_Ocean_Pattern_ES = "Vivillon Motivo Océano";
        public const string Vivillon_Ocean_Pattern_IT = "Vivillon Motivo Oceanico";
        public const string Vivillon_Ocean_Pattern_DE = "Vivillon Ozeanmuster";
        public const string Vivillon_Ocean_Pattern_RU = "Вивийон океан рисунок";
        public const string Vivillon_Ocean_Pattern_CO = "비비용 오션의 모양";
        public const string Vivillon_Ocean_Pattern_CN = "彩粉蝶大洋花紋";
        public const string Vivillon_Ocean_Pattern_JP = "ビビヨン オーシャンのもよう";
        public const string Vivillon_Ocean_Pattern_UrlImg = "https://www.pokepedia.fr/images/2/25/Sprite_666_Soleil_Levant_HOME.png";
        public const string Vivillon_Ocean_Pattern_UrlSprite = "https://www.pokepedia.fr/images/f/f0/Miniature_666_Soleil_Levant_XY.png";
        #endregion

        #region Zénith
        public const string Vivillon_Sun_Pattern_FR = "Prismillon Motif Zénith";
        public const string Vivillon_Sun_Pattern_EN = "Vivillon Sun Pattern";
        public const string Vivillon_Sun_Pattern_ES = "Vivillon Motivo Solar";
        public const string Vivillon_Sun_Pattern_IT = "Vivillon Motivo Solare";
        public const string Vivillon_Sun_Pattern_DE = "Vivillon Sonnenmuster";
        public const string Vivillon_Sun_Pattern_RU = "Вивийон солнце рисунок";
        public const string Vivillon_Sun_Pattern_CO = "비비용 태양의 모양";
        public const string Vivillon_Sun_Pattern_CN = "彩粉蝶太陽花紋";
        public const string Vivillon_Sun_Pattern_JP = "ビビヨン たいようのもよう";
        public const string Vivillon_Sun_Pattern_UrlImg = "https://www.pokepedia.fr/images/5/53/Sprite_666_Z%C3%A9nith_HOME.png";
        public const string Vivillon_Sun_Pattern_UrlSprite = "https://www.pokepedia.fr/images/9/9c/Miniature_666_Z%C3%A9nith_XY.png";
        #endregion

        #region Fantaisie
        public const string Vivillon_Fancy_Pattern_FR = "Prismillon Motif Fantaisie";
        public const string Vivillon_Fancy_Pattern_EN = "Vivillon Fancy Pattern";
        public const string Vivillon_Fancy_Pattern_ES = "Vivillon Motivo Fantasía";
        public const string Vivillon_Fancy_Pattern_IT = "Vivillon Motivo Sbarazzino";
        public const string Vivillon_Fancy_Pattern_DE = "Vivillon Fantasiemuster";
        public const string Vivillon_Fancy_Pattern_RU = "Вивийон фантазия рисунок";
        public const string Vivillon_Fancy_Pattern_CO = "비비용 팬시한 모양";
        public const string Vivillon_Fancy_Pattern_CN = "彩粉蝶 幻彩花紋";
        public const string Vivillon_Fancy_Pattern_JP = "ビビヨン ファンシーなもよう";
        public const string Vivillon_Fancy_Pattern_UrlImg = "https://www.pokepedia.fr/images/0/00/Sprite_666_Fantaisie_HOME.png";
        public const string Vivillon_Fancy_Pattern_UrlSprite = "https://www.pokepedia.fr/images/c/c1/Miniature_666_Fantaisie_XY.png";
        #endregion

        #region Pokéball
        public const string Vivillon_PokeBall_Pattern_FR = "Prismillon Motif PokéBall";
        public const string Vivillon_PokeBall_Pattern_EN = "Vivillon PokeBall Pattern";
        public const string Vivillon_PokeBall_Pattern_ES = "Vivillon Motivo Poké Ball";
        public const string Vivillon_PokeBall_Pattern_IT = "Vivillon Motivo Poké Ball";
        public const string Vivillon_PokeBall_Pattern_DE = "Vivillon Pokéball-Muster";
        public const string Vivillon_PokeBall_Pattern_RU = "Вивийон покебол рисунок";
        public const string Vivillon_PokeBall_Pattern_CO = "비비용 볼의 모양";
        public const string Vivillon_PokeBall_Pattern_CN = "彩粉蝶球球花紋";
        public const string Vivillon_PokeBall_Pattern_JP = "ビビヨン ボールのもよう";
        public const string Vivillon_PokeBall_Pattern_UrlImg = "https://www.pokepedia.fr/images/4/43/Sprite_666_Pok%C3%A9_Ball_HOME.png";
        public const string Vivillon_PokeBall_Pattern_UrlSprite = "https://www.pokepedia.fr/images/1/11/Miniature_666_Pok%C3%A9_Ball_XY.png";
        #endregion
        #endregion

        #region Couafarel
        #region Demoiselle
        public const string Furfrou_Debutante_Trim_FR = "Couafarel Coupe Demoiselle";
        public const string Furfrou_Debutante_Trim_EN = "Furfrou Debutante Trim";
        public const string Furfrou_Debutante_Trim_ES = "Furfrou Corte Señorita";
        public const string Furfrou_Debutante_Trim_IT = "Furfrou Taglio Signorina";
        public const string Furfrou_Debutante_Trim_DE = "Coiffwaff Fräuleinschnitt";
        public const string Furfrou_Debutante_Trim_RU = "Фурфу Дебютантка Трим";
        public const string Furfrou_Debutante_Trim_CO = "트리미앙 레이디컷";
        public const string Furfrou_Debutante_Trim_CN = "多麗米亞 淑女造型";
        public const string Furfrou_Debutante_Trim_JP = "トリミアン レディカット";
        public const string Furfrou_Debutante_Trim_UrlImg = "https://www.pokepedia.fr/images/9/9e/Sprite_676_Demoiselle_HOME.png";
        public const string Furfrou_Debutante_Trim_UrlSprite = "https://www.pokepedia.fr/images/8/8b/Miniature_676_Demoiselle_XY.png";
        #endregion

        #region Madame
        public const string Furfrou_Matron_Trim_FR = "Couafarel Coupe Madame";
        public const string Furfrou_Matron_Trim_EN = "Furfrou Matron Trim";
        public const string Furfrou_Matron_Trim_ES = "Furfrou Corte Dama";
        public const string Furfrou_Matron_Trim_IT = "Furfrou Taglio Gentildonna";
        public const string Furfrou_Matron_Trim_DE = "Coiffwaff Damenschnitt";
        public const string Furfrou_Matron_Trim_RU = "Фурфу Матрона Трим";
        public const string Furfrou_Matron_Trim_CO = "트리미앙 마담컷";
        public const string Furfrou_Matron_Trim_CN = "多麗米亞 貴婦造型";
        public const string Furfrou_Matron_Trim_JP = "トリミアン マダムカット";
        public const string Furfrou_Matron_Trim_UrlImg = "https://www.pokepedia.fr/images/0/09/Sprite_676_Madame_HOME.png";
        public const string Furfrou_Matron_Trim_UrlSprite = "https://www.pokepedia.fr/images/c/c7/Miniature_676_Madame_XY.png";
        #endregion

        #region Monsieur
        public const string Furfrou_Dandy_Trim_FR = "Couafarel Coupe Monsieur";
        public const string Furfrou_Dandy_Trim_EN = "Furfrou Dandy Trim";
        public const string Furfrou_Dandy_Trim_ES = "Furfrou Corte Caballero";
        public const string Furfrou_Dandy_Trim_IT = "Furfrou Taglio Gentiluomo";
        public const string Furfrou_Dandy_Trim_DE = "Coiffwaff Kavaliersschnitt";
        public const string Furfrou_Dandy_Trim_RU = "Фурфу Денди Трим";
        public const string Furfrou_Dandy_Trim_CO = "트리미앙 젠틀컷";
        public const string Furfrou_Dandy_Trim_CN = "多麗米亞 紳士造型";
        public const string Furfrou_Dandy_Trim_JP = "トリミアン ジェントルカット";
        public const string Furfrou_Dandy_Trim_UrlImg = "https://www.pokepedia.fr/images/a/a2/Sprite_676_Monsieur_HOME.png";
        public const string Furfrou_Dandy_Trim_UrlSprite = "https://www.pokepedia.fr/images/b/b7/Miniature_676_Monsieur_XY.png";
        #endregion

        #region Reine
        public const string Furfrou_Queen_Trim_FR = "Couafarel Coupe Reine";
        public const string Furfrou_Queen_Trim_EN = "Furfrou Queen Trim";
        public const string Furfrou_Queen_Trim_ES = "Furfrou Corte Aristocrático";
        public const string Furfrou_Queen_Trim_IT = "Furfrou Taglio Regina";
        public const string Furfrou_Queen_Trim_DE = "Coiffwaff Königinnenschnitt";
        public const string Furfrou_Queen_Trim_RU = "Фурфу Королева Трим";
        public const string Furfrou_Queen_Trim_CO = "트리미앙 퀸컷";
        public const string Furfrou_Queen_Trim_CN = "多麗米亞 女王造型";
        public const string Furfrou_Queen_Trim_JP = "トリミアン クイーンカット";
        public const string Furfrou_Queen_Trim_UrlImg = "https://www.pokepedia.fr/images/2/23/Sprite_676_Reine_HOME.png";
        public const string Furfrou_Queen_Trim_UrlSprite = "https://www.pokepedia.fr/images/c/cc/Miniature_676_Reine_XY.png";
        #endregion

        #region Kabuki
        public const string Furfrou_Kabuki_Trim_FR = "Couafarel Coupe Kabuki";
        public const string Furfrou_Kabuki_Trim_EN = "Furfrou Kabuki Trim";
        public const string Furfrou_Kabuki_Trim_ES = "Furfrou Corte Kabuki";
        public const string Furfrou_Kabuki_Trim_IT = "Furfrou Taglio Kabuki";
        public const string Furfrou_Kabuki_Trim_DE = "Coiffwaff Kabuki-Schnitt";
        public const string Furfrou_Kabuki_Trim_RU = "Фурфу Кабуки Трим";
        public const string Furfrou_Kabuki_Trim_CO = "트리미앙 가부키컷";
        public const string Furfrou_Kabuki_Trim_CN = "多麗米亞 歌舞伎造型";
        public const string Furfrou_Kabuki_Trim_JP = "トリミアン カブキカット";
        public const string Furfrou_Kabuki_Trim_UrlImg = "https://www.pokepedia.fr/images/7/77/Sprite_676_Kabuki_HOME.png";
        public const string Furfrou_Kabuki_Trim_UrlSprite = "https://www.pokepedia.fr/images/9/92/Miniature_676_Kabuki_XY.png";
        #endregion

        #region Pharaon
        public const string Furfrou_Pharaoh_Trim_FR = "Couafarel Coupe Pharaon";
        public const string Furfrou_Pharaoh_Trim_EN = "Furfrou Pharaoh Trim";
        public const string Furfrou_Pharaoh_Trim_ES = "Furfrou Corte Faraónico";
        public const string Furfrou_Pharaoh_Trim_IT = "Furfrou Taglio Faraone";
        public const string Furfrou_Pharaoh_Trim_DE = "Coiffwaff Herrscherschnitt";
        public const string Furfrou_Pharaoh_Trim_RU = "Фурфу Фараон Трим";
        public const string Furfrou_Pharaoh_Trim_CO = "트리미앙 킹덤컷";
        public const string Furfrou_Pharaoh_Trim_CN = "多麗米亞 國王造型";
        public const string Furfrou_Pharaoh_Trim_JP = "トリミアン キングダムカット";
        public const string Furfrou_Pharaoh_Trim_UrlImg = "https://www.pokepedia.fr/images/5/5d/Sprite_676_Pharaon_HOME.png";
        public const string Furfrou_Pharaoh_Trim_UrlSprite = "https://www.pokepedia.fr/images/7/7f/Miniature_676_Pharaon_XY.png";
        #endregion
        #endregion

        #region Pikachu
        #region Pikachu Rockeur
        public const string Pikachu_Rocker_FR = "Pikachu Rockeur";
        public const string Pikachu_Rocker_EN = "Pikachu Rocker";
        public const string Pikachu_Rocker_ES = "";
        public const string Pikachu_Rocker_IT = "";
        public const string Pikachu_Rocker_DE = "";
        public const string Pikachu_Rocker_RU = "";
        public const string Pikachu_Rocker_CO = "";
        public const string Pikachu_Rocker_CN = "";
        public const string Pikachu_Rocker_JP = "";
        public const string Pikachu_Rocker_UrlImg = "https://www.pokepedia.fr/images/c/c5/Pikachu_%28Rockeur%29-ROSA.png";
        public const string Pikachu_Rocker_UrlSprite = "https://www.pokepedia.fr/images/1/1b/Miniature_025_Rockeur_ROSA.png";
        #endregion
        #region Pikachu Lady
        public const string Pikachu_Lady_FR = "Pikachu Lady";
        public const string Pikachu_Lady_EN = "Pikachu Lady";
        public const string Pikachu_Lady_ES = "";
        public const string Pikachu_Lady_IT = "";
        public const string Pikachu_Lady_DE = "";
        public const string Pikachu_Lady_RU = "";
        public const string Pikachu_Lady_CO = "";
        public const string Pikachu_Lady_CN = "";
        public const string Pikachu_Lady_JP = "";
        public const string Pikachu_Lady_UrlImg = "https://www.pokepedia.fr/images/2/2a/Pikachu_%28Lady%29-ROSA.png";
        public const string Pikachu_Lady_UrlSprite = "https://www.pokepedia.fr/images/b/bb/Miniature_025_Lady_ROSA.png";
        #endregion
        #region Pikachu Star
        public const string Pikachu_Star_FR = "Pikachu Star";
        public const string Pikachu_Star_EN = "Pikachu Star";
        public const string Pikachu_Star_ES = "";
        public const string Pikachu_Star_IT = "";
        public const string Pikachu_Star_DE = "";
        public const string Pikachu_Star_RU = "";
        public const string Pikachu_Star_CO = "";
        public const string Pikachu_Star_CN = "";
        public const string Pikachu_Star_JP = "";
        public const string Pikachu_Star_UrlImg = "https://www.pokepedia.fr/images/8/84/Pikachu_%28Star%29-ROSA.png";
        public const string Pikachu_Star_UrlSprite = "https://www.pokepedia.fr/images/0/05/Miniature_025_Star_ROSA.png";
        #endregion
        #region Pikachu Docteur
        public const string Pikachu_Doctor_FR = "Pikachu Docteur";
        public const string Pikachu_Doctor_EN = "Pikachu Doctor";
        public const string Pikachu_Doctor_ES = "";
        public const string Pikachu_Doctor_IT = "";
        public const string Pikachu_Doctor_DE = "";
        public const string Pikachu_Doctor_RU = "";
        public const string Pikachu_Doctor_CO = "";
        public const string Pikachu_Doctor_CN = "";
        public const string Pikachu_Doctor_JP = "";
        public const string Pikachu_Doctor_UrlImg = "https://www.pokepedia.fr/images/5/53/Pikachu_%28Docteur%29-ROSA.png";
        public const string Pikachu_Doctor_UrlSprite = "https://www.pokepedia.fr/images/d/d5/Miniature_025_Docteur_ROSA.png";
        #endregion
        #region Pikachu Catcheur
        public const string Pikachu_Wrestler_FR = "Pikachu Catcheur";
        public const string Pikachu_Wrestler_EN = "Pikachu Wrestler";
        public const string Pikachu_Wrestler_ES = "";
        public const string Pikachu_Wrestler_IT = "";
        public const string Pikachu_Wrestler_DE = "";
        public const string Pikachu_Wrestler_RU = "";
        public const string Pikachu_Wrestler_CO = "";
        public const string Pikachu_Wrestler_CN = "";
        public const string Pikachu_Wrestler_JP = "";
        public const string Pikachu_Wrestler_UrlImg = "https://www.pokepedia.fr/images/f/f2/Pikachu_%28Catcheur%29-ROSA.png";
        public const string Pikachu_Wrestler_UrlSprite = "https://www.pokepedia.fr/images/3/31/Miniature_025_Catcheur_ROSA.png";
        #endregion
        #region Pikachu Casquette Originale
        public const string Pikachu_Original_Cap_FR = "Pikachu (Casquette Originale)";
        public const string Pikachu_Original_Cap_EN = "Pikachu (Original Cap)";
        public const string Pikachu_Original_Cap_ES = "";
        public const string Pikachu_Original_Cap_IT = "";
        public const string Pikachu_Original_Cap_DE = "";
        public const string Pikachu_Original_Cap_RU = "";
        public const string Pikachu_Original_Cap_CO = "";
        public const string Pikachu_Original_Cap_CN = "";
        public const string Pikachu_Original_Cap_JP = "";
        public const string Pikachu_Original_Cap_UrlImg = "https://www.pokepedia.fr/images/1/15/Pikachu_%28Casquette_Originale%29-SL.png";
        public const string Pikachu_Original_Cap_UrlSprite = "https://www.pokepedia.fr/images/7/7a/Miniature_025_Casquette_Originale_SL.png";
        #endregion
        #region Pikachu Casquette de Hoenn
        public const string Pikachu_Hoenn_Cap_FR = "Pikachu (Casquette de Hoenn)";
        public const string Pikachu_Hoenn_Cap_EN = "Pikachu (Hoenn Cap)";
        public const string Pikachu_Hoenn_Cap_ES = "";
        public const string Pikachu_Hoenn_Cap_IT = "";
        public const string Pikachu_Hoenn_Cap_DE = "";
        public const string Pikachu_Hoenn_Cap_RU = "";
        public const string Pikachu_Hoenn_Cap_CO = "";
        public const string Pikachu_Hoenn_Cap_CN = "";
        public const string Pikachu_Hoenn_Cap_JP = "";
        public const string Pikachu_Hoenn_Cap_UrlImg = "https://www.pokepedia.fr/images/b/b3/Pikachu_%28Casquette_de_Hoenn%29-SL.png";
        public const string Pikachu_Hoenn_Cap_UrlSprite = "https://www.pokepedia.fr/images/1/17/Miniature_025_Casquette_de_Hoenn_SL.png";
        #endregion
        #region Pikachu Casquette de Sinnoh
        public const string Pikachu_Sinnoh_Cap_FR = "Pikachu (Casquette de Sinnoh)";
        public const string Pikachu_Sinnoh_Cap_EN = "Pikachu (Sinnoh Cap)";
        public const string Pikachu_Sinnoh_Cap_ES = "";
        public const string Pikachu_Sinnoh_Cap_IT = "";
        public const string Pikachu_Sinnoh_Cap_DE = "";
        public const string Pikachu_Sinnoh_Cap_RU = "";
        public const string Pikachu_Sinnoh_Cap_CO = "";
        public const string Pikachu_Sinnoh_Cap_CN = "";
        public const string Pikachu_Sinnoh_Cap_JP = "";
        public const string Pikachu_Sinnoh_Cap_UrlImg = "https://www.pokepedia.fr/images/1/1b/Pikachu_%28Casquette_de_Sinnoh%29-SL.png";
        public const string Pikachu_Sinnoh_Cap_UrlSprite = "https://www.pokepedia.fr/images/4/4b/Miniature_025_Casquette_de_Sinnoh_SL.png";
        #endregion
        #region Pikachu Casquette d'Unys
        public const string Pikachu_Unys_Cap_FR = "Pikachu (Casquette d'Unys)";
        public const string Pikachu_Unys_Cap_EN = "Pikachu (Unys Cap)";
        public const string Pikachu_Unys_Cap_ES = "";
        public const string Pikachu_Unys_Cap_IT = "";
        public const string Pikachu_Unys_Cap_DE = "";
        public const string Pikachu_Unys_Cap_RU = "";
        public const string Pikachu_Unys_Cap_CO = "";
        public const string Pikachu_Unys_Cap_CN = "";
        public const string Pikachu_Unys_Cap_JP = "";
        public const string Pikachu_Unys_Cap_UrlImg = "https://www.pokepedia.fr/images/3/37/Pikachu_%28Casquette_d%27Unys%29-SL.png";
        public const string Pikachu_Unys_Cap_UrlSprite = "https://www.pokepedia.fr/images/e/ec/Miniature_025_Casquette_d%27Unys_SL.png";
        #endregion
        #region Pikachu Casquette de Kalos
        public const string Pikachu_Kalos_Cap_FR = "Pikachu (Casquette de Kalos)";
        public const string Pikachu_Kalos_Cap_EN = "Pikachu (Kalos Cap)";
        public const string Pikachu_Kalos_Cap_ES = "";
        public const string Pikachu_Kalos_Cap_IT = "";
        public const string Pikachu_Kalos_Cap_DE = "";
        public const string Pikachu_Kalos_Cap_RU = "";
        public const string Pikachu_Kalos_Cap_CO = "";
        public const string Pikachu_Kalos_Cap_CN = "";
        public const string Pikachu_Kalos_Cap_JP = "";
        public const string Pikachu_Kalos_Cap_UrlImg = "https://www.pokepedia.fr/images/1/10/Pikachu_%28Casquette_de_Kalos%29-SL.png";
        public const string Pikachu_Kalos_Cap_UrlSprite = "https://www.pokepedia.fr/images/6/6a/Miniature_025_Casquette_de_Kalos_SL.png";
        #endregion
        #region Pikachu Casquette d'Alola
        public const string Pikachu_Alola_Cap_FR = "Pikachu (Casquette d'Alola)";
        public const string Pikachu_Alola_Cap_EN = "Pikachu (Alola Cap)";
        public const string Pikachu_Alola_Cap_ES = "";
        public const string Pikachu_Alola_Cap_IT = "";
        public const string Pikachu_Alola_Cap_DE = "";
        public const string Pikachu_Alola_Cap_RU = "";
        public const string Pikachu_Alola_Cap_CO = "";
        public const string Pikachu_Alola_Cap_CN = "";
        public const string Pikachu_Alola_Cap_JP = "";
        public const string Pikachu_Alola_Cap_UrlImg = "https://www.pokepedia.fr/images/a/a7/Pikachu_%28Casquette_d%27Alola%29-SL.png";
        public const string Pikachu_Alola_Cap_UrlSprite = "https://www.pokepedia.fr/images/2/2f/Miniature_025_Casquette_d%27Alola_SL.png";
        #endregion
        #region Pikachu Casquette Partenaire
        public const string Pikachu_Partner_Cap_FR = "Pikachu (Casquette Partenaire)";
        public const string Pikachu_Partner_Cap_EN = "Pikachu (Partner Cap)";
        public const string Pikachu_Partner_Cap_ES = "";
        public const string Pikachu_Partner_Cap_IT = "";
        public const string Pikachu_Partner_Cap_DE = "";
        public const string Pikachu_Partner_Cap_RU = "";
        public const string Pikachu_Partner_Cap_CO = "";
        public const string Pikachu_Partner_Cap_CN = "";
        public const string Pikachu_Partner_Cap_JP = "";
        public const string Pikachu_Partner_Cap_UrlImg = "https://www.pokepedia.fr/images/8/87/Pikachu_%28Casquette_Partenaire%29-USUL.png";
        public const string Pikachu_Partner_Cap_UrlSprite = "https://www.pokepedia.fr/images/2/23/Miniature_025_Casquette_Partenaire_USUL.png";
        #endregion
        #region Pikachu Casquette Monde
        public const string Pikachu_World_Cap_FR = "Pikachu (Casquette Monde)";
        public const string Pikachu_World_Cap_EN = "Pikachu (World Cap)";
        public const string Pikachu_World_Cap_ES = "";
        public const string Pikachu_World_Cap_IT = "";
        public const string Pikachu_World_Cap_DE = "";
        public const string Pikachu_World_Cap_RU = "";
        public const string Pikachu_World_Cap_CO = "";
        public const string Pikachu_World_Cap_CN = "";
        public const string Pikachu_World_Cap_JP = "";
        public const string Pikachu_World_Cap_UrlImg = "https://www.pokepedia.fr/images/e/ed/Pikachu_%28Casquette_Monde%29-EB.png";
        public const string Pikachu_World_Cap_UrlSprite = "https://www.pokepedia.fr/images/4/4c/Miniature_025_Casquette_Monde_EB.png";
        #endregion
        #endregion

        #region Type Image
        public const string MiniGo = "MiniGo";
        public const string FondGo = "FondGo";
        public const string MiniHome = "MiniHome";
        public const string IconHome = "IconHome";
        public const string AutoHome = "AutoHome";
        #endregion

        #region Type Attaque
        public const string PhysicalPokeBip = "Physique";
        public const string Physical = "Physical";
        public const string Physical_Name_FR = "Capacités Physiques";
        public const string Physical_Description_FR = "Les capacités physiques utilisent les statistiques physiques (attaque et défense)";
        public const string Physical_Name_EN = "Physical Abilities";
        public const string Physical_Description_EN = "Physical abilities use physical stats (attack & defense)";
        public const string Physical_Name_ES = "Habilidades Fisicas";
        public const string Physical_Description_ES = "Las habilidades físicas usan estadísticas físicas (ataque y defensa)";
        public const string Physical_Name_IT = "Abilità Fisiche";
        public const string Physical_Description_IT = "Le abilità fisiche usano le statistiche fisiche (attacco e difesa)";
        public const string Physical_Name_DE = "Körperliche Fähigkeiten";
        public const string Physical_Description_DE = "Physische Fähigkeiten verwenden physische Werte (angriff & verteidigung)";
        public const string Physical_Name_RU = "Физические способности";
        public const string Physical_Description_RU = "Физические способности используют физические характеристики (атака и защита)";
        public const string Physical_Name_CO = "신체적 능력";
        public const string Physical_Description_CO = "신체 능력은 신체 능력치(공격 및 방어)를 사용합니다";
        public const string Physical_Name_CN = "体能";
        public const string Physical_Description_CN = "物理能力使用物理统计（攻击和防御）";
        public const string Physical_Name_JP = "身体能力";
        public const string Physical_Description_JP = "物理アビリティは物理ステータスを使用します (攻撃と防御)";
        public const string Physical_UrlImg = "https://www.pokepedia.fr/images/1/11/Miniature_Cat%C3%A9gorie_Physique_DEPS.png";

        public const string SpecialPokeBip = "Spéciale";
        public const string Special = "Special";
        public const string Special_Name_FR = "Capacités Spéciales";
        public const string Special_Description_FR = "Les capacités spéciales utilisent les statistiques spéciales (attaque spéciale et défense spéciale, ou uniquement le spécial dans la première génération)";
        public const string Special_Name_EN = "Special Abilities";
        public const string Special_Description_EN = "Special abilities use special stats (special attack and special defense, or only the special in the first generation)";
        public const string Special_Name_ES = "Habilidades Especiales";
        public const string Special_Description_ES = "Las habilidades especiales usan estadísticas especiales (ataque especial y defensa especial, o solo las especiales en la primera generación)";
        public const string Special_Name_IT = "Abilità Speciali";
        public const string Special_Description_IT = "Le abilità speciali usano statistiche speciali (attacco speciale e difesa speciale, o solo lo speciale nella prima generazione)";
        public const string Special_Name_DE = "Spezielle Fähigkeiten";
        public const string Special_Description_DE = "Spezialfähigkeiten verwenden Spezialstatistiken (Spezialangriff und Spezialverteidigung oder nur das Spezial in der ersten Generation)";
        public const string Special_Name_RU = "Специальные возможности";
        public const string Special_Description_RU = "Специальные способности используют специальные характеристики (специальная атака и специальная защита или только специальные в первом поколении)";
        public const string Special_Name_CO = "특수 능력";
        public const string Special_Description_CO = "특수 능력은 특수 능력치를 사용합니다(특수 공격과 특수 방어, 또는 1세대에서 특수만 사용)";
        public const string Special_Name_CN = "特殊的能力";
        public const string Special_Description_CN = "特殊能力使用特殊属性（特攻特防，或者只有初代特攻）";
        public const string Special_Name_JP = "特殊能力";
        public const string Special_Description_JP = "特殊能力は特殊な統計を使用します（特殊攻撃と特殊防御、または第一世代の特殊のみ）";
        public const string Special_UrlImg = "https://www.pokepedia.fr/images/d/da/Miniature_Cat%C3%A9gorie_Sp%C3%A9ciale_DEPS.png";

        public const string StatusPokeBip = "Statut";
        public const string Status = "Status";
        public const string Status_Name_FR = "Capacités de Statut";
        public const string Status_Description_FR = "Les capacités de statut sont toutes les autres capacités, celles qui ne provoquent pas de dégâts selon la formule habituelle (dégâts fixes, augmentation de statistiques, changement de statut, etc.)";
        public const string Status_Name_EN = "Status Abilities";
        public const string Status_Description_EN = "Status abilities are all other abilities, those that don't deal damage in the usual formula (fixed damage, stat increase, status change, etc.)";
        public const string Status_Name_ES = "Habilidades de Estado";
        public const string Status_Description_ES = "Las habilidades de estado son todas las demás habilidades, aquellas que no infligen daño en la fórmula habitual (daño fijo, aumento de estadísticas, cambio de estado, etc.)";
        public const string Status_Name_IT = "Abilità di Stato";
        public const string Status_Description_IT = "Le abilità di stato sono tutte le altre abilità, quelle che non infliggono danno nella solita formula (danno fisso, aumento delle statistiche, cambio di stato, ecc.)";
        public const string Status_Name_DE = "Statusfähigkeiten";
        public const string Status_Description_DE = "Statusfähigkeiten sind alle anderen Fähigkeiten, die keinen Schaden in der üblichen Formel (fester Schaden, Statuserhöhung, Statusänderung usw.)";
        public const string Status_Name_RU = "Способности статуса";
        public const string Status_Description_RU = "Статусные способности — это все остальные способности, которые не наносят урон в обычной формуле (фиксированный урон, повышение характеристик, изменение статуса и т.д.)";
        public const string Status_Name_CO = "상태 능력";
        public const string Status_Description_CO = "스테이터스 능력은 일반적인 공식(데미지 고정, 스탯 증가, 스테이터스 변경 등)으로 피해를 입히지 않는 다른 모든 능력입니다.";
        public const string Status_Name_CN = "状态能力";
        public const string Status_Description_CN = "状态能力是所有其他能力，那些在通常的公式中不造成伤害的能力（固定伤害、属性增加、状态改变等）";
        public const string Status_Name_JP = "ステータスアビリティ";
        public const string Status_Description_JP = "ステータスアビリティは、通常の公式ではダメージを与えないアビリティ（固定ダメージ、ステータス上昇、ステータス変化など）です。";
        public const string Status_UrlImg = "https://www.pokepedia.fr/images/b/b5/Miniature_Cat%C3%A9gorie_Statut_DEPS.png";
        #endregion

        #region TypeLearn
        public const string LearnEvolution = "Evolution";
        public const string TypeLearnEvolution_Name_FR = "Attaques apprises par évolution";
        public const string TypeLearnEvolution_Name_EN = "Attacks learned by evolution";
        public const string TypeLearnEvolution_Name_ES = "Ataques aprendidos por evolución";
        public const string TypeLearnEvolution_Name_IT = "Attacchi appresi per evoluzione";
        public const string TypeLearnEvolution_Name_DE = "Angriffe, die durch Evolution gelernt werden";

        public const string LearnLevel = "Niveau";
        public const string TypeLearn_Name_FR = "Attaques apprises par niveau";
        public const string TypeLearn_Name_EN = "Attacks learned by level";
        public const string TypeLearn_Name_ES = "Ataques aprendidos por nivel";
        public const string TypeLearn_Name_IT = "Attacchi appresi per livello";
        public const string TypeLearn_Name_DE = "Angriffe, die durch Leveln gelernt werden";

        public const string LearnCTCS = "CT/CS";
        public const string TypeLearnCTCS_Name_FR = "Attaques apprises par CT/CS";
        public const string TypeLearnCTCS_Name_EN = "Attacks learned by TM/TR";
        public const string TypeLearnCTCS_Name_ES = "Ataques aprendidos por MT/CT";
        public const string TypeLearnCTCS_Name_IT = "Attacchi appresi tramite MT/CT";
        public const string TypeLearnCTCS_Name_DE = "Angriffe, die durch TM/TR gelernt werden";

        public const string LearnMoveTutor = "Maître des Capacités";
        public const string TypeLearnMoveTutor_Name_FR = "Attaques apprises par Maître des Capacités";
        public const string TypeLearnMoveTutor_Name_EN = "Attacks learned by Move Tutor";
        public const string TypeLearnMoveTutor_Name_ES = "Ataques aprendidos por Move Tutor";
        public const string TypeLearnMoveTutor_Name_IT = "Attacchi appresi tramite Move Tutor";
        public const string TypeLearnMoveTutor_Name_DE = "Angriffe, die durch Move Tutor gelernt werden";

        public const string LearnEgg = "Reproduction";
        public const string TypeLearnEgg_Name_FR = "Attaques apprises par reproduction";
        public const string TypeLearnEgg_Name_EN = "Attacks learned by breeding";
        public const string TypeLearnEgg_Name_ES = "Ataques aprendidos por reproducción";
        public const string TypeLearnEgg_Name_IT = "Attacchi appresi tramite incrocio";
        public const string TypeLearnEgg_Name_DE = "Angriffe, die durch Zucht gelernt werden";
        #endregion

        #region Game
        public const string RedBlueUrl = "rouge-bleu";
        public const string RedBlue_Name_FR = "Rouge et Bleu";
        public const string RedBlue_Name_EN = "Red and Blue";
        public const string RedBlue_Name_ES = "Rojo y Azul";
        public const string RedBlue_Name_IT = "Rosso e Blu";
        public const string RedBlue_Name_DE = "Rot und Blau";
        public const string RedBlue_Name_RU = "Красный и Синий";
        public const string RedBlue_Name_CO = "빨강과 파랑";
        public const string RedBlue_Name_CN = "红色和蓝色";
        public const string RedBlue_Name_JP = "赤と青";

        public const string YellowUrl = "jaune";
        public const string Yellow_Name_FR = "Jaune";
        public const string Yellow_Name_EN = "Yellow";
        public const string Yellow_Name_ES = "Amarillo";
        public const string Yellow_Name_IT = "Giallo";
        public const string Yellow_Name_DE = "Gelb";
        public const string Yellow_Name_RU = "Желтый";
        public const string Yellow_Name_CO = "노란색";
        public const string Yellow_Name_CN = "黄色";
        public const string Yellow_Name_JP = "黄";

        public const string GoldSilverUrl = "or-argent";
        public const string GoldSilver_Name_FR = "Or et Argent";
        public const string GoldSilver_Name_EN = "Gold and Silver";
        public const string GoldSilver_Name_ES = "Oro y Plata";
        public const string GoldSilver_Name_IT = "Oro e Argento";
        public const string GoldSilver_Name_DE = "Gold und Silber";
        public const string GoldSilver_Name_RU = "Золотой и Серебряный";
        public const string GoldSilver_Name_CO = "금과은";
        public const string GoldSilver_Name_CN = "黄金和白银";
        public const string GoldSilver_Name_JP = "黄金銀";

        public const string CrystalUrl = "cristal";
        public const string Crystal_Name_FR = "Crystal";
        public const string Crystal_Name_EN = "Crystal";
        public const string Crystal_Name_ES = "Cristal";
        public const string Crystal_Name_IT = "Cristallo";
        public const string Crystal_Name_DE = "Kristall";
        public const string Crystal_Name_RU = "Кристалл";
        public const string Crystal_Name_CO = "결정";
        public const string Crystal_Name_CN = "水晶";
        public const string Crystal_Name_JP = "結晶";

        public const string RubySapphireUrl = "rubis-saphir";
        public const string RubySapphire_Name_FR = "Rubis et Saphir";
        public const string RubySapphire_Name_EN = "Ruby and Sapphire";
        public const string RubySapphire_Name_ES = "Rubí y Zafiro";
        public const string RubySapphire_Name_IT = "Rubino e Zaffiro";
        public const string RubySapphire_Name_DE = "Rubin und Saphir";
        public const string RubySapphire_Name_RU = "Рубин и Сапфир";
        public const string RubySapphire_Name_CO = "루비와 사파이어";
        public const string RubySapphire_Name_CN = "红宝石和蓝宝石";
        public const string RubySapphire_Name_JP = "ルビーとサファイア";

        public const string EmeraldUrl = "emeraude";
        public const string Emerald_Name_FR = "Emeraude";
        public const string Emerald_Name_EN = "Emerald";
        public const string Emerald_Name_ES = "Esmeralda";
        public const string Emerald_Name_IT = "Smeraldo";
        public const string Emerald_Name_DE = "Smaragd";
        public const string Emerald_Name_RU = "Изумруд";
        public const string Emerald_Name_CO = "에메랄드";
        public const string Emerald_Name_CN = "翠";
        public const string Emerald_Name_JP = "エメラルド";

        public const string FireRedLeafGreenUrl = "rouge-feu-vert-feuille";
        public const string FireRedLeafGreen_Name_FR = "Feuille Rouge et Vert Feuille";
        public const string FireRedLeafGreen_Name_EN = "FireRed and LeafGreen";
        public const string FireRedLeafGreen_Name_ES = "Fuego Rojo y Hoja Verde";
        public const string FireRedLeafGreen_Name_IT = "Rosso Fuoco e Verde Foglia";
        public const string FireRedLeafGreen_Name_DE = "Feuerrot und Blattgrün";
        public const string FireRedLeafGreen_Name_RU = "Огненно-Красный и Листовой Зеленый";
        public const string FireRedLeafGreen_Name_CO = "파이어 레드와 리프 그린";
        public const string FireRedLeafGreen_Name_CN = "火红叶绿";
        public const string FireRedLeafGreen_Name_JP = "ファイアレッドとリーフグリーン";

        public const string DiamondPearlUrl = "diamant-perle";
        public const string DiamondPearl_Name_FR = "Diamant et Perle";
        public const string DiamondPearl_Name_EN = "Diamond and Pearl";
        public const string DiamondPearl_Name_ES = "Diamante y Perla";
        public const string DiamondPearl_Name_IT = "Diamante e Perla";
        public const string DiamondPearl_Name_DE = "Diamant und Perl";
        public const string DiamondPearl_Name_RU = "Алмаз и Жемчуг";
        public const string DiamondPearl_Name_CO = "다이아몬드와 진주";
        public const string DiamondPearl_Name_CN = "钻石和珍珠";
        public const string DiamondPearl_Name_JP = "ダイヤモンドとパール";

        public const string PlatinumUrl = "platine";
        public const string Platinum_Name_FR = "Platine";
        public const string Platinum_Name_EN = "Platinum";
        public const string Platinum_Name_ES = "Platino";
        public const string Platinum_Name_IT = "Platino";
        public const string Platinum_Name_DE = "Platin";
        public const string Platinum_Name_RU = "Платина";
        public const string Platinum_Name_CO = "백금";
        public const string Platinum_Name_CN = "铂";
        public const string Platinum_Name_JP = "白金";

        public const string HeartGoldSoulSilverUrl = "or-heartgold-argent-soulsilver";
        public const string HeartGoldSoulSilver_Name_FR = "Or Heart et Argent Soul";
        public const string HeartGoldSoulSilver_Name_EN = "HeartGold and SoulSilver";
        public const string HeartGoldSoulSilver_Name_ES = "Oro Corazón y Plata Alma";
        public const string HeartGoldSoulSilver_Name_IT = "Oro Cuore e Argento Anima";
        public const string HeartGoldSoulSilver_Name_DE = "Herzgold und Seelensilber";
        public const string HeartGoldSoulSilver_Name_RU = "Золотой Сердце и Серебряный Душа";
        public const string HeartGoldSoulSilver_Name_CO = "하트골드와 소울실버";
        public const string HeartGoldSoulSilver_Name_CN = "心金魂银";
        public const string HeartGoldSoulSilver_Name_JP = "ハートゴールドとソウルシルバー";

        public const string BlackWhiteUrl = "noir-blanc";
        public const string BlackWhite_Name_FR = "Noir et Blanc";
        public const string BlackWhite_Name_EN = "Black and White";
        public const string BlackWhite_Name_ES = "Negro y Blanco";
        public const string BlackWhite_Name_IT = "Nero e Bianco";
        public const string BlackWhite_Name_DE = "Schwarz und Weiß";
        public const string BlackWhite_Name_RU = "Черный и Белый";
        public const string BlackWhite_Name_CO = "검정색과 흰색";
        public const string BlackWhite_Name_CN = "黑和白";
        public const string BlackWhite_Name_JP = "黒と白";

        public const string Black2White2Url = "noir-2-blanc-2";
        public const string Black2White2_Name_FR = "Noir 2 et Blanc 2";
        public const string Black2White2_Name_EN = "Black 2 and White 2";
        public const string Black2White2_Name_ES = "Negro 2 y Blanco 2";
        public const string Black2White2_Name_IT = "Nero 2 e Bianco 2";
        public const string Black2White2_Name_DE = "Schwarz 2 und Weiß 2";
        public const string Black2White2_Name_RU = "Черный 2 и Белый 2";
        public const string Black2White2_Name_CO = "블랙 2와 화이트 2";
        public const string Black2White2_Name_CN = "黑2和白2";
        public const string Black2White2_Name_JP = "黒2と白2";

        public const string X_YUrl = "x-y";
        public const string X_Y_Name_FR = "X et Y";
        public const string X_Y_Name_EN = "X and Y";
        public const string X_Y_Name_ES = "X y Y";
        public const string X_Y_Name_IT = "X e Y";
        public const string X_Y_Name_DE = "X und Y";
        public const string X_Y_Name_RU = "Х и У";
        public const string X_Y_Name_CO = "X와 Y";
        public const string X_Y_Name_CN = "X和Y";
        public const string X_Y_Name_JP = "XとY";

        public const string OmegaRubyAlphaSapphireUrl = "rubis-omega-saphir-alpha";
        public const string OmegaRubyAlphaSapphire_Name_FR = "Rubis Oméga et Saphir Alpha";
        public const string OmegaRubyAlphaSapphire_Name_EN = "Omega Ruby and Alpha Sapphire";
        public const string OmegaRubyAlphaSapphire_Name_ES = "Rubí Omega y Zafiro Alfa";
        public const string OmegaRubyAlphaSapphire_Name_IT = "Rubino Omega e Zaffiro Alfa";
        public const string OmegaRubyAlphaSapphire_Name_DE = "Omegarubin und Alphasaphir";
        public const string OmegaRubyAlphaSapphire_Name_RU = "Омега Рубин и Альфа Сапфир";
        public const string OmegaRubyAlphaSapphire_Name_CO = "오메가 루비와 알파 사파이어";
        public const string OmegaRubyAlphaSapphire_Name_CN = "欧米茄红宝石和阿尔法蓝宝石";
        public const string OmegaRubyAlphaSapphire_Name_JP = "オメガルビーとアルファサファイア";

        public const string SunMoonUrl = "soleil-lune";
        public const string SunMoon_Name_FR = "Soleil et Lune";
        public const string SunMoon_Name_EN = "Sun and Moon";
        public const string SunMoon_Name_ES = "Sol y Luna";
        public const string SunMoon_Name_IT = "Sole e Luna";
        public const string SunMoon_Name_DE = "Sonne und Mond";
        public const string SunMoon_Name_RU = "Солнце и Луна";
        public const string SunMoon_Name_CO = "해와 달";
        public const string SunMoon_Name_CN = "太阳和月亮";
        public const string SunMoon_Name_JP = "太陽と月";

        public const string UltraSunUltraMoonUrl = "ultra-soleil-ultra-lune";
        public const string UltraSunUltraMoon_Name_FR = "Ultra Soleil et Ultra Lune";
        public const string UltraSunUltraMoon_Name_EN = "Ultra Sun and Ultra Moon";
        public const string UltraSunUltraMoon_Name_ES = "Ultra Sol y Ultra Luna";
        public const string UltraSunUltraMoon_Name_IT = "Ultra Sole e Ultra Luna";
        public const string UltraSunUltraMoon_Name_DE = "Ultra Sonne und Ultra Mond";
        public const string UltraSunUltraMoon_Name_RU = "Ультра Солнце и Ультра Луна";
        public const string UltraSunUltraMoon_Name_CO = "울트라썬과 울트라문";
        public const string UltraSunUltraMoon_Name_CN = "究极太阳和究极月亮";
        public const string UltraSunUltraMoon_Name_JP = "ウルトラサンとウルトラムーン";

        public const string LetsGoPikachuEvoliUrl = "lets-go-pikachu-evoli";
        public const string LetsGoPikachuEvoli_Name_FR = "Let's Go, Pikachu et Let's Go, Évoli";
        public const string LetsGoPikachuEvoli_Name_EN = "Let's Go, Pikachu and Let's Go, Eevee";
        public const string LetsGoPikachuEvoli_Name_ES = "Vamos, Pikachu y Vamos, Eevee";
        public const string LetsGoPikachuEvoli_Name_IT = "Andiamo, Pikachu e Andiamo, Eevee";
        public const string LetsGoPikachuEvoli_Name_DE = "Los geht's, Pikachu und Los geht's, Evoli";
        public const string LetsGoPikachuEvoli_Name_RU = "Давай, Пикачу и Давай, Эвии";
        public const string LetsGoPikachuEvoli_Name_CO = "가자, 피카츄와 가자, 이브이";
        public const string LetsGoPikachuEvoli_Name_CN = "走吧，皮卡丘和走吧，伊布";
        public const string LetsGoPikachuEvoli_Name_JP = "Let's Go ピカチュウと Let's Go イーブイ";

        public const string SwordShieldUrl = "epee-bouclier";
        public const string SwordShield_Name_FR = "Epée et Bouclier";
        public const string SwordShield_Name_EN = "Sword and Shield";
        public const string SwordShield_Name_ES = "Espada y Escudo";
        public const string SwordShield_Name_IT = "Spada e Scudo";
        public const string SwordShield_Name_DE = "Schwert und Schild";
        public const string SwordShield_Name_RU = "Меч и Щит";
        public const string SwordShield_Name_CO = "검과 방패";
        public const string SwordShield_Name_CN = "剑与盾";
        public const string SwordShield_Name_JP = "剣と盾";

        public const string ShiningDiamondShiningPearlUrl = "diamant-etincelant-perle-scintillante";
        public const string ShiningDiamondShiningPearl_Name_FR = "Diamant Étincelant et Perle Scintillante";
        public const string ShiningDiamondShiningPearl_Name_EN = "Shining Diamond and Shining Pearl";
        public const string ShiningDiamondShiningPearl_Name_ES = "Diamante Brillante y Perla Brillante";
        public const string ShiningDiamondShiningPearl_Name_IT = "Diamante Scintillante e Perla Scintillante";
        public const string ShiningDiamondShiningPearl_Name_DE = "Glitzerndes Diamant und Glitzernde Perle";
        public const string ShiningDiamondShiningPearl_Name_RU = "Сияющий Алмаз и Сияющая Жемчужина";
        public const string ShiningDiamondShiningPearl_Name_CO = "빛나는 다이아몬드와 빛나는 진주";
        public const string ShiningDiamondShiningPearl_Name_CN = "闪亮的钻石和闪亮的珍珠";
        public const string ShiningDiamondShiningPearl_Name_JP = "輝くダイヤモンドと輝くパール";

        public const string ArceusUrl = "legendes-arceus";
        public const string Arceus_Name_FR = "Arceus";
        public const string Arceus_Name_EN = "Arceus";
        public const string Arceus_Name_ES = "Arceus";
        public const string Arceus_Name_IT = "Arceus";
        public const string Arceus_Name_DE = "Arceus";
        public const string Arceus_Name_RU = "Арцеус";
        public const string Arceus_Name_CO = "아르세우스";
        public const string Arceus_Name_CN = "阿尔宙斯";
        public const string Arceus_Name_JP = "アルセウス";

        public const string ScarletVioletUrl = "ecarlate-violet";
        public const string ScarletViolet_Name_FR = "Écarlate et Violet";
        public const string ScarletViolet_Name_EN = "Scarlet and Violet";
        public const string ScarletViolet_Name_ES = "Escarlata y Violeta";
        public const string ScarletViolet_Name_IT = "Escarlatta e Violaceo";
        public const string ScarletViolet_Name_DE = "Scharlach und Violett";
        public const string ScarletViolet_Name_RU = "Багровый и Фиолетовый";
        public const string ScarletViolet_Name_CO = "스칼렛과 바이올렛";
        public const string ScarletViolet_Name_CN = "猩红色和紫色";
        public const string ScarletViolet_Name_JP = "スカーレットとバイオレット";
        #endregion

        #region Hidden Abilities
        public const string Name_Analytic_FR = "Analyste";
        public const string Description_Analytic_FR = "Augmente la puissance des capacités du Pokémon s’il attaque en dernier.";
        public const string Name_Analytic_EN = "Analytic";
        public const string Description_Analytic_EN = "Boosts the power of the Pokémon's move if it is the last to act that turn.";
        public const string Name_Analytic_ES = "Cálculo Final";
        public const string Description_Analytic_ES = "Aumenta la potencia del movimiento del Pokémon si es el último en actuar en ese turno.";
        public const string Name_Analytic_IT = "Ponderazione";
        public const string Description_Analytic_IT = "Aumenta la potenza della mossa del Pokémon se è l'ultimo ad agire in quel turno.";
        public const string Name_Analytic_DE = "Analyse";
        public const string Description_Analytic_DE = "Erhöht die Kraft der Attacke des Pokémon, wenn es das letzte ist, das in dieser Runde handelt.";

        public const string Name_PowerOfAlchemy_FR = "Osmose";
        public const string Description_PowerOfAlchemy_FR = "Le Pokémon acquiert le talent d’un allié mis K.O.";
        public const string Name_PowerOfAlchemy_EN = "Power of Alchemy";
        public const string Description_PowerOfAlchemy_EN = "The Pokémon copies the Ability of a defeated ally.";
        public const string Name_PowerOfAlchemy_ES = "Reacción Química";
        public const string Description_PowerOfAlchemy_ES = "El Pokémon copia la Habilidad de un aliado derrotado.";
        public const string Name_PowerOfAlchemy_IT = "Forza Chimica";
        public const string Description_PowerOfAlchemy_IT = "Il Pokémon copia l'abilità di un alleato sconfitto.";
        public const string Name_PowerOfAlchemy_DE = "Chemiekraft";
        public const string Description_PowerOfAlchemy_DE = "Das Pokémon kopiert die Fähigkeit eines besiegten Verbündeten.";

        public const string Name_Harvest_FR = "Récolte";
        public const string Description_Harvest_FR = "Permet de réutiliser une même Baie plusieurs fois.";
        public const string Name_Harvest_EN = "Harvest";
        public const string Description_Harvest_EN = "May create another Berry after one is used.";
        public const string Name_Harvest_ES = "Cosecha";
        public const string Description_Harvest_ES = "Puede crear otra Berry después de usar una.";
        public const string Name_Harvest_IT = "Coglibacche";
        public const string Description_Harvest_IT = "Può creare un'altra bacca dopo averne usata una.";
        public const string Name_Harvest_DE = "Reiche Ernte";
        public const string Description_Harvest_DE = "Kann eine weitere Beere erstellen, nachdem eine verwendet wurde.";

        public const string Name_Imposter_FR = "Imposteur";
        public const string Description_Imposter_FR = "Le Pokémon prend l’apparence du Pokémon adverse.";
        public const string Name_Imposter_EN = "Imposter";
        public const string Description_Imposter_EN = "The Pokémon transforms itself into the Pokémon it's facing.";
        public const string Name_Imposter_ES = "Impostor";
        public const string Description_Imposter_ES = "El Pokémon se transforma en el Pokémon al que se enfrenta.";
        public const string Name_Imposter_IT = "Sosia";
        public const string Description_Imposter_IT = "Il Pokémon si trasforma nel Pokémon che sta affrontando.";
        public const string Name_Imposter_DE = "Doppelgänger";
        public const string Description_Imposter_DE = "Das Pokémon verwandelt sich in das Pokémon, dem es gegenübersteht.";

        public const string Name_Multiscale_FR = "Multiécaille";
        public const string Description_Multiscale_FR = "Le Pokémon subit moins de dégâts quand ses PV sont au maximum.";
        public const string Name_Multiscale_EN = "Multiscale";
        public const string Description_Multiscale_EN = "Reduces the amount of damage the Pokémon takes while its HP is full.";
        public const string Name_Multiscale_ES = "Multiescamas";
        public const string Description_Multiscale_ES = "Reduce la cantidad de daño que recibe el Pokémon mientras su HP está lleno.";
        public const string Name_Multiscale_IT = "Multisquame";
        public const string Description_Multiscale_IT = "Riduce la quantità di danno che il Pokémon subisce mentre i suoi HP sono pieni.";
        public const string Name_Multiscale_DE = "Multischuppe";
        public const string Description_Multiscale_DE = "Reduziert den Schaden, den das Pokémon erleidet, während seine KP voll sind.";

        public const string Name_Moody_FR = "Lunatique";
        public const string Description_Moody_FR = "Augmente beaucoup une stat du Pokémon et en baisse une autre au hasard à chaque tour.";
        public const string Name_Moody_EN = "Moody";
        public const string Description_Moody_EN = "Every turn, one of the Pokémon's stats will be boosted sharply but another stat will be lowered.";
        public const string Name_Moody_ES = "Veleta";
        public const string Description_Moody_ES = "Cada turno, una de las estadísticas de Pokémon aumentará considerablemente, pero otra estadística se reducirá.";
        public const string Name_Moody_IT = "Altalena";
        public const string Description_Moody_IT = "Ad ogni turno, una delle statistiche del Pokémon verrà notevolmente aumentata ma un'altra statistica verrà abbassata.";
        public const string Name_Moody_DE = "Gefühlswippe";
        public const string Description_Moody_DE = "In jeder Runde wird eine der Statistiken des Pokémon stark erhöht, aber eine andere Statistik wird gesenkt.";

        public const string Name_ToxicBoost_FR = "Rage Poison";
        public const string Description_ToxicBoost_FR = "Augmente la puissance des capacités physiques quand le Pokémon est empoisonné.";
        public const string Name_ToxicBoost_EN = "Toxic Boost";
        public const string Description_ToxicBoost_EN = "Powers up physical moves when the Pokémon is poisoned.";
        public const string Name_ToxicBoost_ES = "Ímpetu Tóxico";
        public const string Description_ToxicBoost_ES = "Potencia los movimientos físicos cuando el Pokémon está envenenado.";
        public const string Name_ToxicBoost_IT = "Velenimpeto";
        public const string Description_ToxicBoost_IT = "Potenzia le mosse fisiche quando il Pokémon è avvelenato.";
        public const string Name_ToxicBoost_DE = "Giftwahn";
        public const string Description_ToxicBoost_DE = "Aktiviert physische Bewegungen, wenn das Pokémon vergiftet ist.";

        public const string Name_Protean_FR = "Protéen";
        public const string Description_Protean_FR = "Le Pokémon prend le type de la capacité qu’il utilise. Ce talent ne peut se déclencher qu’une fois par entrée au combat du Pokémon.";
        public const string Name_Protean_EN = "Protean";
        public const string Description_Protean_EN = "Changes the Pokémon's type to the type of the move it's about to use. This works only once each time the Pokémon enters battle.";
        public const string Name_Protean_ES = "Mutatipo";
        public const string Description_Protean_ES = "Cambia el tipo del Pokémon al tipo del movimiento que está a punto de usar.Esto funciona solo una vez cada vez que el Pokémon entra en batalla.";
        public const string Name_Protean_IT = "Mutatipo";
        public const string Description_Protean_IT = "Cambia il tipo del Pokémon nel tipo della mossa che sta per usare.Funziona solo una volta ogni volta che il Pokémon entra in battaglia.";
        public const string Name_Protean_DE = "Wandlungskunst";
        public const string Description_Protean_DE = "Ändert den Typ des Pokémon in den Typ der Attacke, die es verwenden wird. Dies funktioniert nur einmal jedes Mal, wenn das Pokémon in den Kampf eintritt.";

        public const string Name_FlareBoost_FR = "Rage Brûlure";
        public const string Description_FlareBoost_FR = "Augmente la puissance des capacités spéciales quand le Pokémon est brûlé.";
        public const string Name_FlareBoost_EN = "Flare Boost";
        public const string Description_FlareBoost_EN = "Powers up special attacks when the Pokémon is burned.";
        public const string Name_FlareBoost_ES = "Ímpetu Ardiente";
        public const string Description_FlareBoost_ES = "Potencia los ataques especiales cuando el Pokémon está quemado.";
        public const string Name_FlareBoost_IT = "Bruciaimpeto";
        public const string Description_FlareBoost_IT = "Potenzia gli attacchi speciali quando il Pokémon viene bruciato.";
        public const string Name_FlareBoost_DE = "Hitzewahn";
        public const string Description_FlareBoost_DE = "Aktiviert Spezialangriffe, wenn das Pokémon verbrannt wird.";

        public const string Name_ZenMode_FR = "Mode Transe";
        public const string Description_ZenMode_FR = "Change le Pokémon de forme si ses PV tombent à 50% ou moins. Cela change son type et ses statistiques.";
        public const string Name_ZenMode_EN = "Zen Mode";
        public const string Description_ZenMode_EN = "Changes the Pokémon's shape when its HP drops to half or less.";
        public const string Name_ZenMode_ES = "Modo Daruma";
        public const string Description_ZenMode_ES = "Cambia la forma del Pokémon cuando su HP cae a la mitad o menos.";
        public const string Name_ZenMode_IT = "Stato Zen";
        public const string Description_ZenMode_IT = "Cambia la forma del Pokémon quando i suoi HP scendono a metà o meno.";
        public const string Name_ZenMode_DE = "Trance-Modus";
        public const string Description_ZenMode_DE = "Ändert die Form des Pokémon, wenn seine KP auf die Hälfte oder weniger sinken.";

        public const string Name_GaleWings_FR = "Ailes Bourrasque";
        public const string Description_GaleWings_FR = "Quand les PV du Pokémon sont au maximum, ses capacités de type Vol sont prioritaires.";
        public const string Name_GaleWings_EN = "Gale Wings";
        public const string Description_GaleWings_EN = "Gives priority to the Pokémon's Flying-type moves while its HP is full.";
        public const string Name_GaleWings_ES = "Alas Vendaval";
        public const string Description_GaleWings_ES = "Da prioridad a los movimientos de tipo Volador del Pokémon mientras su HP está lleno.";
        public const string Name_GaleWings_IT = "Aliraffica";
        public const string Description_GaleWings_IT = "Dà priorità alle mosse di tipo Volante del Pokémon mentre i suoi PS sono al massimo.";
        public const string Name_GaleWings_DE = "Orkanschwingen";
        public const string Description_GaleWings_DE = "Gibt den Flug-Attacken des Pokémon Vorrang, solange seine KP voll sind.";

        public const string Name_Symbiosis_FR = "Symbiose";
        public const string Description_Symbiosis_FR = "Quand les alliés utilisent l’objet qu’ils tiennent, le Pokémon leur donne l’objet qu’il tient en remplacement.";
        public const string Name_Symbiosis_EN = "Symbiosis";
        public const string Description_Symbiosis_EN = "The Pokémon passes its held item to an ally that has used up an item.";
        public const string Name_Symbiosis_ES = "Simbiosis";
        public const string Description_Symbiosis_ES = "El Pokémon pasa su objeto retenido a un aliado que ha usado un objeto.";
        public const string Name_Symbiosis_IT = "Simbiosi";
        public const string Description_Symbiosis_IT = "Il Pokémon passa il suo oggetto tenuto a un alleato che ha usato uno strumento.";
        public const string Name_Symbiosis_DE = "Nutznießer";
        public const string Description_Symbiosis_DE = "Das Pokémon gibt sein getragenes Item an einen Verbündeten weiter, der ein Item aufgebraucht hat.";

        public const string Name_GrassPelt_FR = "Toison Herbue";
        public const string Description_GrassPelt_FR = "Augmente la Défense du Pokémon si un champ herbu est actif.";
        public const string Name_GrassPelt_EN = "Grass Pelt";
        public const string Description_GrassPelt_EN = "Boosts the Pokémon's Defense stat on Grassy Terrain.";
        public const string Name_GrassPelt_ES = "Manto Frondoso";
        public const string Description_GrassPelt_ES = "Aumenta la estadística de defensa del Pokémon en terreno herboso.";
        public const string Name_GrassPelt_IT = "Peloderba";
        public const string Description_GrassPelt_IT = "Aumenta la statistica di Difesa del Pokémon su terreno erboso.";
        public const string Name_GrassPelt_DE = "Pflanzenpelz";
        public const string Description_GrassPelt_DE = "Erhöht den Verteidigungswert des Pokémon auf Grasland.";
        
        public const string Name_LongReach_FR = "Longue Portée";
        public const string Description_LongReach_FR = "Le Pokémon est capable d’utiliser toutes ses capacités sans entrer en contact direct avec sa cible.";
        public const string Name_LongReach_EN = "Long Reach";
        public const string Description_LongReach_EN = "The Pokémon uses its moves without making contact with the target.";
        public const string Name_LongReach_ES = "Remoto";
        public const string Description_LongReach_ES = "El Pokémon usa sus movimientos sin hacer contacto con el objetivo.";
        public const string Name_LongReach_IT = "Distacco";
        public const string Description_LongReach_IT = "Il Pokémon usa le sue mosse senza entrare in contatto con il bersaglio.";
        public const string Name_LongReach_DE = "Langstrecke";
        public const string Description_LongReach_DE = "Das Pokémon setzt seine Attacken ein, ohne das Ziel zu berühren.";
        
        public const string Name_LiquidVoice_FR = "Hydrata-Son";
        public const string Description_LiquidVoice_FR = "Les capacités \"sonores\" du Pokémon deviennent de type Eau.";
        public const string Name_LiquidVoice_EN = "Liquid Voice";
        public const string Description_LiquidVoice_EN = "Sound-based moves become Water-type moves.";
        public const string Name_LiquidVoice_ES = "Voz Fluida";
        public const string Description_LiquidVoice_ES = "Los movimientos basados ​​en sonido se convierten en movimientos de tipo agua.";
        public const string Name_LiquidVoice_IT = "Idrovoce";
        public const string Description_LiquidVoice_IT = "Le mosse basate sul suono diventano mosse di tipo Acqua.";
        public const string Name_LiquidVoice_DE = "Plätscherstimme";
        public const string Description_LiquidVoice_DE = "Geräuschbasierte Bewegungen werden zu Bewegungen vom Typ Wasser.";

        public const string Name_Libero_FR = "Libéro";
        public const string Description_Libero_FR = "Le Pokémon prend le type de la capacité qu’il utilise. Ce talent ne peut se déclencher qu’une fois par entrée au combat du Pokémon.";
        public const string Name_Libero_EN = "Libero";
        public const string Description_Libero_EN = "Changes the Pokémon's type to the type of the move it's about to use. This works only once each time the Pokémon enters battle.";
        public const string Name_Libero_ES = "Líbero";
        public const string Description_Libero_ES = "Cambia el tipo del Pokémon al tipo del movimiento que está a punto de usar.Esto funciona solo una vez cada vez que el Pokémon entra en batalla.";
        public const string Name_Libero_IT = "Libero";
        public const string Description_Libero_IT = "Cambia il tipo del Pokémon nel tipo della mossa che sta per usare.Funziona solo una volta ogni volta che il Pokémon entra in battaglia.";
        public const string Name_Libero_DE = "Libero";
        public const string Description_Libero_DE = "Ändert den Typ des Pokémon in den Typ der Attacke, die es verwenden wird. Dies funktioniert nur einmal jedes Mal, wenn das Pokémon in den Kampf eintritt.";

        public const string Name_MirrorArmor_FR = "Armure Miroir";
        public const string Description_MirrorArmor_FR = "Le Pokémon renvoie les effets réducteurs de stats qu’il reçoit.";
        public const string Name_MirrorArmor_EN = "Mirror Armor";
        public const string Description_MirrorArmor_EN = "Bounces back only the stat-lowering effects that the Pokémon receives.";
        public const string Name_MirrorArmor_ES = "Coraza Reflejo";
        public const string Description_MirrorArmor_ES = "Rebota solo los efectos de reducción de estadísticas que recibe el Pokémon.";
        public const string Name_MirrorArmor_IT = "Blindospecchio";
        public const string Description_MirrorArmor_IT = "Respinge solo gli effetti di riduzione delle statistiche ricevuti dal Pokémon.";
        public const string Name_MirrorArmor_DE = "Spiegelrüstung";
        public const string Description_MirrorArmor_DE = "Springt nur die wertesenkenden Effekte zurück, die das Pokémon erhält.";

        public const string Name_PropellerTail_FR = "Propulseur";
        public const string Description_PropellerTail_FR = "Permet d’ignorer l’effet des capacités ou des talents qui attirent les capacités.";
        public const string Name_PropellerTail_EN = "Propeller Tail";
        public const string Description_PropellerTail_EN = "Ignores the effects of opposing Pokémon's Abilities and moves that draw in moves.";
        public const string Name_PropellerTail_ES = "Hélice Caudal";
        public const string Description_PropellerTail_ES = "Ignora los efectos de las habilidades y movimientos de los Pokémon opuestos que atraen movimientos.";
        public const string Name_PropellerTail_IT = "Elicopinna";
        public const string Description_PropellerTail_IT = "Ignora gli effetti delle abilità e delle mosse dei Pokémon avversari che attirano le mosse.";
        public const string Name_PropellerTail_DE = "Schraubflosse";
        public const string Description_PropellerTail_DE = "Ignoriert die Effekte von Fähigkeiten und Attacken gegnerischer Pokémon, die Attacken einziehen.";

        public const string Name_SteelySpirit_FR = "Boost Acier";
        public const string Description_SteelySpirit_FR = "Augmente la puissance des attaques de type Acier du Pokémon et de ses alliés.";
        public const string Name_SteelySpirit_EN = "Steely Spirit";
        public const string Description_SteelySpirit_EN = "Powers up the Steel-type moves of the Pokémon and its allies.";
        public const string Name_SteelySpirit_ES = "Alma Acerada";
        public const string Description_SteelySpirit_ES = "Potencia los movimientos de tipo acero del Pokémon y sus aliados.";
        public const string Name_SteelySpirit_IT = "Spiritoferreo";
        public const string Description_SteelySpirit_IT = "Potenzia le mosse di tipo Acciaio del Pokémon e dei suoi alleati.";
        public const string Name_SteelySpirit_DE = "Stählerner Wille";
        public const string Description_SteelySpirit_DE = "Verstärkt die Stahl-Attacken des Pokémon und seiner Verbündeten.";

        public const string Name_PerishBody_FR = "Corps Condamné";
        public const string Description_PerishBody_FR = "Lorsque le Pokémon est touché par une capacité de contact, l'assaillant et lui tomberont K.O. dans 3 tours, à moins qu'ils soient retirés du combat.";
        public const string Name_PerishBody_EN = "Perish Body";
        public const string Description_PerishBody_EN = "When hit by a move that makes direct contact, the Pokémon and the attacker will faint after three turns unless they switch out of battle.";
        public const string Name_PerishBody_ES = "Cuerpo Mortal";
        public const string Description_PerishBody_ES = "Cuando son golpeados por un movimiento que hace contacto directo, el Pokémon y el atacante se desmayarán después de tres turnos a menos que salgan de la batalla.";
        public const string Name_PerishBody_IT = "Ultimotocco";
        public const string Description_PerishBody_IT = "Quando vengono colpiti da una mossa che crea un contatto diretto, il Pokémon e l'attaccante svengono dopo tre turni a meno che non escano dalla lotta.";
        public const string Name_PerishBody_DE = "Unheilskörper";
        public const string Description_PerishBody_DE = "Wenn sie von einer Bewegung getroffen werden, die direkten Kontakt herstellt, werden das Pokémon und der Angreifer nach drei Runden ohnmächtig, es sei denn, sie wechseln aus dem Kampf.";

        public const string Name_IceScales_FR = "Écailles Glacées";
        public const string Description_IceScales_FR = "Le Pokémon est protégé par des écailles de glace.Les dégâts qu’il subit par des capacités spéciales sont divisés par deux.";
        public const string Name_IceScales_EN = "Ice Scales";
        public const string Description_IceScales_EN = "The Pokémon is protected by ice scales, which halve the damage taken from special moves.";
        public const string Name_IceScales_ES = "Escama de Hielo";
        public const string Description_IceScales_ES = "El Pokémon está protegido por escamas de hielo, que reducen a la mitad el daño recibido por movimientos especiales.";
        public const string Name_IceScales_IT = "Geloscaglie";
        public const string Description_IceScales_IT = "Il Pokémon è protetto da scaglie di ghiaccio, che dimezzano i danni subiti dalle mosse speciali.";
        public const string Name_IceScales_DE = "Eisflügelstaub";
        public const string Description_IceScales_DE = "Das Pokémon wird durch Eisschuppen geschützt, die den durch Spezialbewegungen erlittenen Schaden halbieren.";

        public const string Name_Stalwart_FR = "Nerfs d'Acier";
        public const string Description_Stalwart_FR = "Permet d'ignorer l'effet des capacités ou des talents qui attirent les capacités.";
        public const string Name_Stalwart_EN = "Stalwart";
        public const string Description_Stalwart_EN = "Ignores the effects of opposing Pokémon's Abilities and moves that draw in moves.";
        public const string Name_Stalwart_ES = "Acérrimo";
        public const string Description_Stalwart_ES = "Ignora los efectos de las habilidades y movimientos de los Pokémon opuestos que atraen movimientos.";
        public const string Name_Stalwart_IT = "Volontà di Ferro";
        public const string Description_Stalwart_IT = "Ignora gli effetti delle abilità e delle mosse dei Pokémon avversari che attirano le mosse.";
        public const string Name_Stalwart_DE = "Stahlrückgrat";
        public const string Description_Stalwart_DE = "Ignoriert die Effekte von Fähigkeiten und Attacken gegnerischer Pokémon, die Attacken einziehen.";

        public const string Name_Sharpness_FR = "Incisif";
        public const string Description_Sharpness_FR = "Augmente la puissance des capacités tranchantes.";
        public const string Name_Sharpness_EN = "Sharpness";
        public const string Description_Sharpness_EN = "Powers up slicing moves.";
        public const string Name_Sharpness_ES = "Cortante";
        public const string Description_Sharpness_ES = "Potencia los movimientos de corte.";
        public const string Name_Sharpness_IT = "Affilama";
        public const string Description_Sharpness_IT = "Potenzia le mosse taglienti.";
        public const string Name_Sharpness_DE = "Scharfkantig";
        public const string Description_Sharpness_DE = "Verbessert Slicing-Moves.";

        public const string Name_RockyPayload_FR = "Porte-Roche";
        public const string Description_RockyPayload_FR = "Augmente la puissance des capacités de type Roche.";
        public const string Name_RockyPayload_EN = "Rocky Payload";
        public const string Description_RockyPayload_EN = "Powers up Rock-type moves.";
        public const string Name_RockyPayload_ES = "Transportarrocas";
        public const string Description_RockyPayload_ES = "Potencia los movimientos tipo roca.";
        public const string Name_RockyPayload_IT = "Portamassi";
        public const string Description_RockyPayload_IT = "Potenzia le mosse di tipo Roccia.";
        public const string Name_RockyPayload_DE = "Steinträger";
        public const string Description_RockyPayload_DE = "Verstärkt Rock-Attacken.";

        public const string Name_Costar_FR = "Collab";
        public const string Description_Costar_FR = "Quand le Pokémon entre sur le terrain, il copie les changements de stats de son allié.";
        public const string Name_Costar_EN = "Costar";
        public const string Description_Costar_EN = "When the Pokémon enters a battle, it copies an ally's stat changes.";
        public const string Name_Costar_ES = "Unísono";
        public const string Description_Costar_ES = "Cuando el Pokémon entra en una batalla, copia los cambios de estadísticas de un aliado.";
        public const string Name_Costar_IT = "Coprotagonismo";
        public const string Description_Costar_IT = "Quando il Pokémon entra in battaglia, copia le modifiche alle statistiche di un alleato.";
        public const string Name_Costar_DE = "Synchronauftritt";
        public const string Description_Costar_DE = "Wenn das Pokémon in einen Kampf eintritt, kopiert es die Statusänderungen eines Verbündeten.";
        #endregion

        #region Attacks
        public const string King_s_Shield_EN = "King’s_Shield";
        public const string King_s_Shield_EN_New = "King%27s_Shield";
        public const string Will_EN = "Will";
        public const string Will_EN_New = "Will-O-Wisp";
        public const string V_EN = "V";
        public const string V_EN_New = "V-create";
        public const string X_EN = "X";
        public const string X_EN_New = "X-Scissor";
        public const string U_EN = "U";
        public const string U_EN_New = "U-turn";
        public const string Forest_s_Curse_EN = "Forest’s_Curse";
        public const string Forest_s_Curse_EN_New = "Forest%27s_Curse";
        public const string Mud_EN = "Mud";
        public const string Mud_EN_New = "Mud-Slap";
        public const string Land_s_Wrath_EN = "Land’s_Wrath";
        public const string Land_s_Wrath_EN_New = "Land%27s_Wrath";
        public const string Topsy_EN = "Topsy";
        public const string Topsy_EN_New = "Topsy-Turvy";
        public const string Power_EN = "Power";
        public const string Power_EN_New = "Power-Up_Punch";
        public const string Baby_EN = "Baby";
        public const string Baby_EN_New = "Baby-Doll_Eyes";
        public const string Nature_s_Madness_EN = "Nature’s_Madness";
        public const string Nature_s_Madness_EN_New = "Nature%27s_Madness";
        public const string Freeze_EN = "Freeze";
        public const string Freeze_EN_New = "Freeze-Dry";
        public const string Soft_EN = "Soft";
        public const string Soft_EN_New = "Soft-Boiled";
        public const string Lock_EN = "Lock";
        public const string Lock_EN_New = "Lock-On";
        public const string Self_EN = "Self";
        public const string Self_EN_New = "Hyper_Beam";
        public const string Multi_EN = "Multi";
        public const string Multi_EN_New = "Multi-Attack";
        public const string Double_EN = "Double";
        public const string Double_EN_New = "Double-Edge";
        #endregion
    }
}
