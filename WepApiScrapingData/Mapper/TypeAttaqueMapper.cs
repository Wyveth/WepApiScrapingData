using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class TypeAttaqueMapper : GenericMapper<TypeAttaque, TypeAttaqueDto>
    {
        public override TypeAttaqueDto Map(TypeAttaque source, string langue)
        {
            if (source == null) return null;

            var dto = new TypeAttaqueDto
            {
                Id = source.Id,
                PathImg = source.PathImg
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? "FR";

            // Nom
            var nameProp = typeof(TypeAttaque).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // Description
            var pathProp = typeof(TypeAttaque).GetProperty($"Description_{lang}");
            if (pathProp != null)
                dto.Description = pathProp.GetValue(source)?.ToString();

            return dto;
        }
    }
}
