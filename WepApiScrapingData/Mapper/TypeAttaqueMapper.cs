using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class TypeAttaqueMapper
    {
        public TypeAttaqueDto Map(TypeAttaque source, string langue)
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

        public TypeAttaque MapReverse(TypeAttaqueDto dto, string langue)
        {
            if (dto == null) return null;

            var entity = new TypeAttaque
            {
                Id = dto.Id
            };

            var lang = langue?.ToUpper() ?? "FR";

            var nameProp = typeof(TypeAttaque).GetProperty($"Name_{lang}");
            if (nameProp != null)
                nameProp.SetValue(entity, dto.Name);

            var pathProp = typeof(TypeAttaque).GetProperty($"Description_{lang}");
            if (pathProp != null)
                pathProp.SetValue(entity, dto.Description);

            return entity;
        }
    }
}
