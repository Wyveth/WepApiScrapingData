using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class PokemonDto : IdentityDto
    {
        public string? Number { get; set; }

        public DataInfo FR { get; set; }
        public DataInfo EN { get; set; }
        public DataInfo ES { get; set; }
        public DataInfo IT { get; set; }
        public DataInfo DE { get; set; }
        public DataInfo RU { get; set; }
        public DataInfo CO { get; set; }
        public DataInfo CN { get; set; }
        public DataInfo JP { get; set; }

        public List<long>? Pokemon_TypePoks { get; set; }
        public List<long>? Pokemon_Weaknesses { get; set; }
        public List<long>? Pokemon_Talents { get; set; }
        public List<long>? Pokemon_Attaques { get; set; }

        public string? TypeEvolution { get; set; }

        public int StatPv { get; set; }

        public int StatAttaque { get; set; }

        public int StatDefense { get; set; }

        public int StatAttaqueSpe { get; set; }

        public int StatDefenseSpe { get; set; }

        public int StatVitesse { get; set; }

        public int StatTotal { get; set; }

        public string? EggMoves { get; set; }

        public string? CaptureRate { get; set; }

        public string? BasicHappiness { get; set; }

        public int Generation { get; set; }

        public string? UrlImg { get; set; }

        public string? PathImg { get; set; }

        public string? UrlSprite { get; set; }

        public string? PathSprite { get; set; }

        public string? UrlSound { get; set; }

        public string? PathSound { get; set; }

        public long Game { get; set; }
        
        public PokemonDto()
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
            
            Pokemon_TypePoks = new ();
            Pokemon_Weaknesses = new ();
            Pokemon_Talents = new ();
            Pokemon_Attaques = new ();
        }
    }
}
