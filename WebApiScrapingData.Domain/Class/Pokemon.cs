using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Constantes;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon : Identity
    {
        //Numéro du Pokémon
        [DataMember(Name = DataMember.Number)]
        public string? Number { get; set; }

        public long Id_FR { get; set; }
        [DataMember(Name = DataMember.FR)]
        [ForeignKey("Id_FR")]
        public DataInfo FR { get; set; }

        public long Id_EN { get; set; }
        [DataMember(Name = DataMember.EN)]
        [ForeignKey("Id_EN")]
        public DataInfo EN { get; set; }

        public long Id_ES { get; set; }
        [DataMember(Name = DataMember.ES)]
        [ForeignKey("Id_ES")]
        public DataInfo ES { get; set; }

        public long Id_IT { get; set; }
        [DataMember(Name = DataMember.IT)]
        [ForeignKey("Id_IT")]
        public DataInfo IT { get; set; }

        public long Id_DE { get; set; }
        [DataMember(Name = DataMember.DE)]
        [ForeignKey("Id_DE")]
        public DataInfo DE { get; set; }

        public long Id_RU { get; set; }
        [DataMember(Name = DataMember.RU)]
        [ForeignKey("Id_RU")]
        public DataInfo RU { get; set; }

        public long Id_CO { get; set; }
        [DataMember(Name = DataMember.CO)]
        [ForeignKey("Id_CO")]
        public DataInfo CO { get; set; }

        public long Id_CN { get; set; }
        [DataMember(Name = DataMember.CN)]
        [ForeignKey("Id_CN")]
        public DataInfo CN { get; set; }

        public long Id_JP { get; set; }
        [DataMember(Name = DataMember.JP)]
        [ForeignKey("Id_JP")]
        public DataInfo JP { get; set; }

        [DataMember(Name = "Types")]
        public List<Pokemon_TypePok> Pokemon_TypePoks { get; set; }
        [DataMember(Name = "Weaknesses")]
        public List<Pokemon_Weakness> Pokemon_Weaknesses { get; set; }
        [DataMember(Name = "Talents")]
        public List<Pokemon_Talent> Pokemon_Talents { get; set; }

        //Type Evolution : Normal, Méga, Gigamax, Alola, Galar, Hisui
        [DataMember(Name = DataMember.TypeEvolution)]
        public string? TypeEvolution { get; set; }

        //Statistique PV
        [DataMember(Name = DataMember.StatPv)]
        public int StatPv { get; set; }

        //Statistique Attaque
        [DataMember(Name = DataMember.StatAttaque)]
        public int StatAttaque { get; set; }

        //Statistique Défense
        [DataMember(Name = DataMember.StatDefense)]
        public int StatDefense { get; set; }

        //Statistique Attaque Spéciale
        [DataMember(Name = DataMember.StatAttaqueSpe)]
        public int StatAttaqueSpe { get; set; }

        //Statistique Défense Spéciale
        [DataMember(Name = DataMember.StatDefenseSpe)]
        public int StatDefenseSpe { get; set; }

        //Statistique Vitesse
        [DataMember(Name = DataMember.StatVitesse)]
        public int StatVitesse { get; set; }

        //Statistique Total
        [DataMember(Name = DataMember.StatTotal)]
        public int StatTotal { get; set; }

        //Generation Number
        [DataMember(Name = DataMember.Generation)]
        public int Generation { get; set; }
        
        //Url de l'Image
        public string? UrlImg { get; set; }

        //Url de l'Image Interne
        [DataMember(Name = DataMember.UrlImg)]
        public string? PathImg { get; set; }

        //Url du Sprite Scrap
        public string? UrlSprite { get; set; }
        
        //Url du Sprite
        [DataMember(Name = DataMember.UrlSprite)]
        public string? PathSprite { get; set; }
        
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

            Pokemon_TypePoks = new();
            Pokemon_Weaknesses = new();
            Pokemon_Talents = new();
        }
    }
}