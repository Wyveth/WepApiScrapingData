using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class TypeAttackMapper : GenericMapper<TypeAttack, TypeAttackDto>
    {
        public override TypeAttackDto Map(TypeAttack source, string langue)
        {
            if (source == null) return null;

            var dto = new TypeAttackDto
            {
                Id = source.Id,
                PathImg = source.PathImg
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? Constantes.FR;

            // Nom
            var nameProp = typeof(TypeAttack).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // Description
            var pathProp = typeof(TypeAttack).GetProperty($"Description_{lang}");
            if (pathProp != null)
                dto.Description = pathProp.GetValue(source)?.ToString();

            return dto;
        }
    }
}
