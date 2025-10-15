using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class TypePokMapper
    {
        public TypePokDto Map(TypePok source, string langue)
        {
            if (source == null) return null;

            var dto = new TypePokDto
            {
                Id = source.Id,
                PathMiniGo = source.PathMiniGo,
                PathFondGo = source.PathFondGo,
                PathIconHome = source.PathIconHome,
                PathAutoHome = source.PathAutoHome,
                ImgColor = source.ImgColor,
                InfoColor = source.InfoColor,
                TypeColor = source.TypeColor,
            };

            // Déterminer les propriétés dynamiquement selon la langue
            var lang = langue?.ToUpper() ?? "FR";

            // Nom
            var nameProp = typeof(TypePok).GetProperty($"Name_{lang}");
            if (nameProp != null)
                dto.Name = nameProp.GetValue(source)?.ToString();

            // PathMiniHome
            var pathProp = typeof(TypePok).GetProperty($"PathMiniHome_{lang}");
            if (pathProp != null)
                dto.PathMiniHome = pathProp.GetValue(source)?.ToString();

            return dto;
        }

        public TypePok MapReverse(TypePokDto dto, string langue)
        {
            if (dto == null) return null;

            var entity = new TypePok
            {
                Id = dto.Id,
                PathMiniGo = dto.PathMiniGo,
                PathFondGo = dto.PathFondGo,
                PathIconHome = dto.PathIconHome,
                PathAutoHome = dto.PathAutoHome,
                ImgColor = dto.ImgColor,
                InfoColor = dto.InfoColor,
                TypeColor = dto.TypeColor,
            };

            var lang = langue?.ToUpper() ?? "FR";

            var nameProp = typeof(TypePok).GetProperty($"Name_{lang}");
            if (nameProp != null)
                nameProp.SetValue(entity, dto.Name);

            var pathProp = typeof(TypePok).GetProperty($"PathMiniHome_{lang}");
            if (pathProp != null)
                pathProp.SetValue(entity, dto.PathMiniHome);

            return entity;
        }
    }
}
