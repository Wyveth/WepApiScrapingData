using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class Pokemon_AbilityDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsHidden { get; set; }
    }
}
