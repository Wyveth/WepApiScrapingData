using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    public class Pokemon : Identity
    {
        //Numéro du Pokémon
        public string? Number { get; set; }

        public long Id_FR { get; set; }
        [ForeignKey("Id_FR")]
        public DataInfo FR { get; set; }

        public long Id_EN { get; set; }
        [ForeignKey("Id_EN")]
        public DataInfo EN { get; set; }

        public long Id_ES { get; set; }
        [ForeignKey("Id_ES")]
        public DataInfo ES { get; set; }

        public long Id_IT { get; set; }
        [ForeignKey("Id_IT")]
        public DataInfo IT { get; set; }

        public long Id_DE { get; set; }
        [ForeignKey("Id_DE")]
        public DataInfo DE { get; set; }

        public long Id_RU { get; set; }
        [ForeignKey("Id_RU")]
        public DataInfo RU { get; set; }

        public long Id_CO { get; set; }
        [ForeignKey("Id_CO")]
        public DataInfo CO { get; set; }

        public long Id_CN { get; set; }
        [ForeignKey("Id_CN")]
        public DataInfo CN { get; set; }

        public long Id_JP { get; set; }
        [ForeignKey("Id_JP")]
        public DataInfo JP { get; set; }

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

        public Pokemon()
        {
            FR = new();
            EN = new();
            ES = new();
            IT = new();
            DE = new();
            RU = new();
            CO = new();
            CN = new();
            JP = new();
        }
    }
}