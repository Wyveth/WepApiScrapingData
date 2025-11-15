using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class Pokemon_EvolvesToDto : IdentityDto
    {
        public string? Name { get; set; }
        public string? WhenEvolution { get; set; }

        public string? PathImage { get; set; }
    }
}
