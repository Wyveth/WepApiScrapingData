using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class GameMapper
    {
        public GameDto Map(Game source, string langue)
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

        public Game MapReverse(GameDto dto, string langue)
        {
            if (dto == null) return null;

            var entity = new Game
            {
                Id = dto.Id
            };

            var lang = langue?.ToUpper() ?? "FR";

            var nameProp = typeof(Game).GetProperty($"Name_{lang}");
            if (nameProp != null)
                nameProp.SetValue(entity, dto.Name);

            return entity;
        }
    }
}
