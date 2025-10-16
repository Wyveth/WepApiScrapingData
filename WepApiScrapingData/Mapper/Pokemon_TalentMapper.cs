using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class Pokemon_TalentMapper : GenericMapper<Pokemon_Talent, Pokemon_TalentDto>
    {
        public override Pokemon_TalentDto Map(Pokemon_Talent source, string langue)
        {
            Talent talent = source.Talent;

            if (source.Talent == null) return null;

            var dto = new Pokemon_TalentDto
            {
                Id = source.Talent.Id,
                IsHidden = source.IsHidden
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? "FR";

            // Nom
            var nameProp = typeof(Talent).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(talent)?.ToString();

            // Description
            var pathProp = typeof(Talent).GetProperty($"Description_{lang}");
            if (pathProp != null)
                dto.Description = pathProp.GetValue(talent)?.ToString();

            return dto;
        }
    }
}
