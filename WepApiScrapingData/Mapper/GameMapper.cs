using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class GameMapper : GenericMapper<Game, GameDto>
    {
        public override GameDto Map(Game source, string langue)
        {
            if (source == null) return null;

            var dto = new GameDto
            {
                Id = source.Id
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? "FR";

            // Nom
            var nameProp = typeof(Game).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            return dto;
        }
    }
}
