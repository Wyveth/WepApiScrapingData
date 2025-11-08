using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class Pokemon_AttackMapper : GenericMapper<Pokemon_Attack, Pokemon_AttackDto>
    {
        public override Pokemon_AttackDto Map(Pokemon_Attack source, string langue)
        {
            Attack attaque = source.Attack;

            if (attaque == null) return null;

            var dto = new Pokemon_AttackDto
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
            var nameProp = typeof(Attack).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(attaque)?.ToString();

            // Description
            var descProp = typeof(Attack).GetProperty($"Description_{lang}");
            if (descProp != null)
                dto.Description = descProp.GetValue(attaque)?.ToString();

            if (attaque.TypePok != null)
            {
                var typeMapper = new TypePokMapper();
                dto.TypePok = typeMapper.Map(attaque.TypePok, lang);
            }

            if (attaque.TypeAttack != null)
            {
                var typeAttaqueMapper = new TypeAttackMapper();
                dto.TypeAttaque = typeAttaqueMapper.Map(attaque.TypeAttack, lang);
            }

            return dto;
        }
    }
}
