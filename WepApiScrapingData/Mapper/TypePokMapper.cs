using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class TypePokMapper : GenericMapper<TypePok, TypePokDto>
    {
        public override TypePokDto Map(TypePok source, string langue)
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
            var lang = langue?.ToUpper() ?? Constantes.FR;

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
    }
}
