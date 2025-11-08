using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class AbilityMapper : GenericMapper<Ability, AbilityDto>
    {
        public override AbilityDto Map(Ability source, string langue)
        {
            if (source == null) return null;

            var dto = new AbilityDto
            {
                Id = source.Id
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? Constantes.FR;

            // Nom
            var nameProp = typeof(Ability).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // Description
            var pathProp = typeof(Ability).GetProperty($"Description_{lang}");
            if (pathProp != null)
                dto.Description = pathProp.GetValue(source)?.ToString();

            return dto;
        }
    }
}
