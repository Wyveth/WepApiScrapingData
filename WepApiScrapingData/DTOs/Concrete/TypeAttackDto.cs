using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class TypeAttackDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? PathImg { get; set; }
    }
}
