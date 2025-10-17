using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class Pokemon_AttaqueMapper : GenericMapper<Pokemon_Attaque, Pokemon_AttaqueDto>
    {
        public override Pokemon_AttaqueDto Map(Pokemon_Attaque source, string langue)
        {
            Attaque attaque = source.Attaque;

            if (attaque == null) return null;

            var dto = new Pokemon_AttaqueDto
            {
                Id = source.Id,
                CTCS = source.CTCS,
                Level = source.Level,
                TypeLearn = source.TypeLearn,
                Power = attaque.Power,
                Precision = attaque.Precision,
                PP = attaque.PP
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? Constantes.FR;

            // Nom
            var nameProp = typeof(Attaque).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(attaque)?.ToString();

            // Description
            var descProp = typeof(Attaque).GetProperty($"Description_{lang}");
            if (descProp != null)
                dto.Description = descProp.GetValue(attaque)?.ToString();

            if (attaque.TypePok != null)
            {
                var typeMapper = new TypePokMapper();
                dto.TypePok = typeMapper.Map(attaque.TypePok, lang);
            }

            if (attaque.TypeAttaque != null)
            {
                var typeAttaqueMapper = new TypeAttaqueMapper();
                dto.TypeAttaque = typeAttaqueMapper.Map(attaque.TypeAttaque, lang);
            }

            return dto;
        }
    }
}
