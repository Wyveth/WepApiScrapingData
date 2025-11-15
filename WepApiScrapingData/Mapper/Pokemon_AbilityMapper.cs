using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class Pokemon_AbilityMapper : GenericMapper<Pokemon_Ability, Pokemon_AbilityDto>
    {
        public override Pokemon_AbilityDto Map(Pokemon_Ability source, string langue)
        {
            Ability talent = source.Ability;

            if (source.Ability == null) return null;

            var dto = new Pokemon_AbilityDto
            {
                Id = source.Ability.Id,
                IsHidden = source.IsHidden
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? Constantes.FR;

            // Nom
            var nameProp = typeof(Ability).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(talent)?.ToString();

            // Description
            var pathProp = typeof(Ability).GetProperty($"Description_{lang}");
            if (pathProp != null)
                dto.Description = pathProp.GetValue(talent)?.ToString();

            return dto;
        }
    }
}
