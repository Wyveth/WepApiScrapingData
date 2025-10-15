using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class AttaqueDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public TypeAttaqueDto? TypeAttaque { get; set; }
        
        public TypePokDto? TypePok { get; set; }

        public string? TypeLearn { get; set; }

        public string? Level { get; set; }

        public string? CTCS { get; set; }

        public string? Power { get; set; }
        
        public string? Precision { get; set; }

        public string? PP { get; set; }
    }
}
