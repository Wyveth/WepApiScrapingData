using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class Pokemon_TalentDto : IdentityDto
    {
        public long PokemonId { get; set; }

        public long TalentId { get; set; }

        public bool IsHidden { get; set; }
    }
}
