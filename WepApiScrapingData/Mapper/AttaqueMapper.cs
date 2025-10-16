using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class AttaqueMapper : GenericMapper<Attaque, AttaqueDto>
    {
        public override AttaqueDto Map(Attaque source, string langue)
        {
            if (source == null) return null;

            var dto = new AttaqueDto
            {
                Id = source.Id,
                Power = source.Power,
                Precision = source.Precision,
                PP = source.PP
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? "FR";

            // Nom
            var nameProp = typeof(Attaque).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // Description
            var descProp = typeof(Attaque).GetProperty($"Description_{lang}");
            if (descProp != null)
                dto.Description = descProp.GetValue(source)?.ToString();

            if (source.TypePok != null)
            {
                var typeMapper = new TypePokMapper();
                dto.TypePok = typeMapper.Map(source.TypePok, lang);
            }

            if (source.TypeAttaque != null)
            {
                var typeAttaqueMapper = new TypeAttaqueMapper();
                dto.TypeAttaque = typeAttaqueMapper.Map(source.TypeAttaque, lang);
            }

            return dto;
        }
    }
}
