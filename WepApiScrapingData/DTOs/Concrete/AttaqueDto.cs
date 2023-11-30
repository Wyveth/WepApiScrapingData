using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class AttaqueDto : IdentityDto
    {
        public string? Name_FR { get; set; }

        public string? Description_FR { get; set; }
        
        public string? Name_EN { get; set; }
        
        public string? Description_EN { get; set; }
        
        public string? Name_ES { get; set; }
        
        public string? Description_ES { get; set; }
        
        public string? Name_IT { get; set; }

        public string? Description_IT { get; set; }
        
        public string? Name_DE { get; set; }
        
        public string? Description_DE { get; set; }

        public string? Name_RU { get; set; }

        public string? Description_RU { get; set; }

        public string? Name_CO { get; set; }

        public string? Description_CO { get; set; }

        public string? Name_CN { get; set; }

        public string? Description_CN { get; set; }

        public string? Name_JP { get; set; }

        public string? Description_JP { get; set; }

        public long? typeAttaque { get; set; }

        public long? typePok { get; set; }
        
        public string? Power { get; set; }
        
        public string? Precision { get; set; }

        public string? PP { get; set; }

        public List<long>? Pokemon_Attaques { get; set; }

        public AttaqueDto()
        {
            Pokemon_Attaques = new ();
        }
    }
}
