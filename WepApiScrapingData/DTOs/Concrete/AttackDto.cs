using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class AttackDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public TypeAttackDto? TypeAttaque { get; set; }
        
        public TypePokDto? TypePok { get; set; }

        public string? Power { get; set; }
        
        public string? Precision { get; set; }

        public string? PP { get; set; }
    }
}
