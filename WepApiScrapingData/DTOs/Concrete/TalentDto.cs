using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class TalentDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
