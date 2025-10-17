using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class TalentMapper : GenericMapper<Talent, TalentDto>
    {
        public override TalentDto Map(Talent source, string langue)
        {
            if (source == null) return null;

            var dto = new TalentDto
            {
                Id = source.Id
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? Constantes.FR;

            // Nom
            var nameProp = typeof(Talent).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // Description
            var pathProp = typeof(Talent).GetProperty($"Description_{lang}");
            if (pathProp != null)
                dto.Description = pathProp.GetValue(source)?.ToString();

            return dto;
        }
    }
}
