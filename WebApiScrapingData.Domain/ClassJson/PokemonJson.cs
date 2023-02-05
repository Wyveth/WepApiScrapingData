using System.Text.Json.Serialization;

namespace WebApiScrapingData.Domain.ClassJson
{
    [Serializable]
    public class PokemonJson
    {
        //Numéro du Pokémon
        public string? Number { get; set; }

        public DataInfoJson FR { get; set; }

        public DataInfoJson EN { get; set; }

        public DataInfoJson ES { get; set; }

        public DataInfoJson IT { get; set; }

        public DataInfoJson DE { get; set; }

        public DataInfoJson RU { get; set; }

        public DataInfoJson CO { get; set; }

        public DataInfoJson CN { get; set; }

        public DataInfoJson JP { get; set; }

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

        //Data de l'image
        [JsonIgnore]
        public byte? DataImg { get; set; }

        //Url du Sprite
        public string? UrlSprite { get; set; }

        //Data du Sprite
        [JsonIgnore]
        public byte[]? DataSprite { get; set; }

        public PokemonJson()
        {
            FR = new DataInfoJson();
            EN = new DataInfoJson();
            ES = new DataInfoJson();
            IT = new DataInfoJson();
            DE = new DataInfoJson();
            RU = new DataInfoJson();
            CO = new DataInfoJson();
            CN = new DataInfoJson();
            JP = new DataInfoJson();
        }
    }
}