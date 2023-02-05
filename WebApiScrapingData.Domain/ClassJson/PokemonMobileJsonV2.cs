namespace WebApiScrapingData.Domain.ClassJson
{
    [Serializable]
    public class PokemonMobileJsonV2
    {
        //Numéro du Pokémon
        public string? Number { get; set; }

        public DataInfoJson FR { get; set; }

        public DataInfoJson EN { get; set; }

        public DataInfoJson ES { get; set; }

        public DataInfoJson IT { get; set; }

        public DataInfoJson DE { get; set; }

        //Type Evolution : Normal, Méga, Gigamax, Alola, Galar, Hisui
        public string? TypeEvolution { get; set; }

        //Statistique PV
        public int StatPv { get; set; }

        //Statistique Attaque
        public int StatAttaque { get; set; }

        //Statistique Défense
        public int StatDefense { get; set; }

        //Statistique Attaque Spéciale
        public int StatAttaqueSpe { get; set; }

        //Statistique Défense Spéciale
        public int StatDefenseSpe { get; set; }

        //Statistique Vitesse
        public int StatVitesse { get; set; }

        //Statistique Total
        public int StatTotal { get; set; }

        //Generation Number
        public int Generation { get; set; }

        //Url de l'Image
        public string? UrlImg { get; set; }

        //Url du Sprite
        public string? UrlSprite { get; set; }

        //Url du Son
        public string? UrlSound { get; set; }

        public PokemonMobileJsonV2()
        {
            FR = new DataInfoJson();
            EN = new DataInfoJson();
            ES = new DataInfoJson();
            IT = new DataInfoJson();
            DE = new DataInfoJson();
        }
    }
}