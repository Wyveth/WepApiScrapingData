using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class Pokemon_EvolvesToMapper : GenericMapper<Pokemon_EvolvesTo, Pokemon_EvolvesToDto>
    {
        public Pokemon_EvolvesToDto Map(Pokemon_EvolvesTo source, string langue,bool from = false)
        {
            if (source == null) return new Pokemon_EvolvesToDto
            {
                WhenEvolution = langue switch
                {
                    "FR" => "Base",
                    "EN" => "Base",
                    "ES" => "Base",
                    "IT" => "Base",
                    "DE" => "Base",
                    "RU" => "База",
                    "CO" => "베이스",
                    "CN" => "根據",
                    "JP" => "ベース",
                    _ => "Base",
                }
            };

            var dto = new Pokemon_EvolvesToDto
            {
                Id = source.EvolveToId,
                Name = GetNameByLang(from ? source.Pokemon : source.EvolveTo, langue),
                PathImage = GetImagePath(from ? source.Pokemon : source.EvolveTo, langue),
                WhenEvolution = langue switch
                {
                    "FR" => source.WhenEvolutionFR,
                    "EN" => source.WhenEvolutionEN,
                    "ES" => source.WhenEvolutionES,
                    "IT" => source.WhenEvolutionIT,
                    "DE" => source.WhenEvolutionDE,
                    "RU" => source.WhenEvolutionRU,
                    "CO" => source.WhenEvolutionCO,
                    "CN" => source.WhenEvolutionCN,
                    "JP" => source.WhenEvolutionJP,
                    _ => source.WhenEvolutionEN,
                }
            };

            return dto;
        }

        private string? GetNameByLang(Pokemon pokemon, string langue = "FR")
        {
            return langue switch
            {
                "FR" => pokemon.FR.Name,
                "EN" => pokemon.EN.Name,
                "ES" => pokemon.ES.Name,
                "IT" => pokemon.IT.Name,
                "DE" => pokemon.DE.Name,
                "RU" => pokemon.RU.Name,
                "CO" => pokemon.CO.Name,
                "CN" => pokemon.CN.Name,
                "JP" => pokemon.JP.Name,
                _ => pokemon.EN.Name,
            };
        }

        private string? GetImagePath(Pokemon pokemon, string langue = "FR")
        {
            return pokemon.PathImgNormal;
        }
    }
}
