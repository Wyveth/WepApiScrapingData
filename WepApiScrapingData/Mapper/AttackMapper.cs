using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class AttackMapper : GenericMapper<Attack, AttackDto>
    {
        public override AttackDto Map(Attack source, string langue)
        {
            if (source == null) return null;

            var dto = new AttackDto
            {
                Id = source.Id,
                Power = source.Power,
                Precision = source.Precision,
                PP = source.PP
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? Constantes.FR;

            // Nom
            var nameProp = typeof(Attack).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // Description
            var descProp = typeof(Attack).GetProperty($"Description_{lang}");
            if (descProp != null)
                dto.Description = descProp.GetValue(source)?.ToString();

            if (source.TypePok != null)
            {
                var typeMapper = new TypePokMapper();
                dto.TypePok = typeMapper.Map(source.TypePok, lang);
            }

            if (source.TypeAttack != null)
            {
                var typeAttaqueMapper = new TypeAttackMapper();
                dto.TypeAttaque = typeAttaqueMapper.Map(source.TypeAttack, lang);
            }

            return dto;
        }
    }
}
