using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class DataInfoDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? DescriptionVx { get; set; }

        public string? DescriptionVy { get; set; }

        public string? Size { get; set; }

        public string? Category { get; set; }

        public string? Weight { get; set; }

        public string? Talent { get; set; }

        public string? DescriptionTalent { get; set; }

        public string? Types { get; set; }

        public string? Weakness { get; set; }

        public string? Evolutions { get; set; }

        public string? WhenEvolution { get; set; }
    }
}
