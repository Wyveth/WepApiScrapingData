using HotChocolate.Language;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class AttaqueMapper
    {
        public AttaqueDto Map(Pokemon_Attaque source, string langue)
        {
            Attaque attaque = source.Attaque;

            if (source.Attaque == null) return null;

            var dto = new AttaqueDto
            {
                Id = source.Id,
                CTCS = source.CTCS,
                Level = source.Level,
                TypeLearn = source.TypeLearn,
                Power = source.Attaque.Power,
                Precision = source.Attaque.Precision,
                PP = source.Attaque.PP
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? "FR";

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

        public Attaque MapReverse(AttaqueDto dto, string langue)
        {
            if (dto == null) return null;

            var entity = new Attaque
            {
                Id = dto.Id,
                Power = dto.Power,
                Precision = dto.Precision,
                PP = dto.PP
            };

            var lang = langue?.ToUpper() ?? "FR";

            var nameProp = typeof(Attaque).GetProperty($"Name_{lang}");
            if (nameProp != null)
                nameProp.SetValue(entity, dto.Name);

            var descProp = typeof(Attaque).GetProperty($"Description_{lang}");
            if (descProp != null)
                descProp.SetValue(entity, dto.Description);


            return entity;
        }
    }
}
