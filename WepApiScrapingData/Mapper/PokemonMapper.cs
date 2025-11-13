using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class PokemonMapper : GenericMapper<Pokemon, PokemonDto>
    {
        private readonly GenericMapper<DataInfo, DataInfoDto> _dataInfoMapper;
        private readonly TypePokMapper _typeMapper;
        private readonly Pokemon_AbilityMapper _talentMapper;
        private readonly Pokemon_AttackMapper _attaqueMapper;
        private readonly GameMapper _gameMapper;
        private readonly Pokemon_EvolvesToMapper _evolveToMapper = new();

        public PokemonMapper(
            GenericMapper<DataInfo, DataInfoDto> dataInfoMapper,
            TypePokMapper typeMapper,
            Pokemon_AbilityMapper talentMapper,
            Pokemon_AttackMapper attaqueMapper,
            Pokemon_EvolvesToMapper evolveToMapper,
            GameMapper gameMapper)
        {
            _dataInfoMapper = dataInfoMapper;
            _typeMapper = typeMapper;
            _talentMapper = talentMapper;
            _attaqueMapper = attaqueMapper;
            _evolveToMapper = evolveToMapper;
            _gameMapper = gameMapper;
        }

        public override PokemonDto Map(Pokemon source, string lang)
        {
            if (source == null)
                return null;

            // 🧱 Étape 1 — Mapper les propriétés de base (grâce au GenericMapper)
            var dto = base.Map(source);

            // 🧩 Étape 2 — Choisir la DataInfo selon la langue
            var langueKey = (lang ?? Constantes.FR).ToUpperInvariant();
            var prop = typeof(Pokemon).GetProperty(langueKey);

            if (prop != null && prop.GetValue(source) is DataInfo dataInfo)
            {
                dto.DataInfo = _dataInfoMapper.Map(dataInfo);
            }

            // 🧩 Étape 3 — Mapper les relations (Types, Faiblesses, Talents, Attaques)
            // 🔹 Types
            if (source.Pokemon_TypePoks?.Any() == true)
            {
                dto.TypePoks = source.Pokemon_TypePoks
                    .Select(t => _typeMapper.Map(t.TypePok, langueKey))
                    .ToList();
            }

            // 🔹 Faiblesses
            if (source.Pokemon_Weaknesses?.Any() == true)
            {
                dto.Weaknesses = source.Pokemon_Weaknesses
                    .Select(w => _typeMapper.Map(w.TypePok, langueKey))
                    .ToList();
            }

            // 🔹 Talents
            if (source.Pokemon_Abilities?.Any() == true)
            {
                dto.Abilities = source.Pokemon_Abilities
                    .Select(t => _talentMapper.Map(t, langueKey))
                    .ToList();
            }

            // 🔹 Attaques
            if (source.Pokemon_Attacks?.Any() == true)
            {
                dto.Attacks = source.Pokemon_Attacks
                    .Select(a => _attaqueMapper.Map(a, langueKey))
                    .ToList();
            }

            dto.EvolveFrom = _evolveToMapper.Map(source.EvolvesFrom, langueKey);

            if (source.Pokemon_TypePoks?.Any() == true)
            {
                dto.EvolvesTo = source.Pokemons_EvolvesTo
                    .Select(t => _evolveToMapper.Map(t, langueKey))
                    .ToList();
            }

            if (source.Game != null)
                dto.Game = _gameMapper.Map(source.Game, langueKey);

            return dto;
        }

        public PokemonLightDto MapLight(Pokemon source, string lang)
        {
            if (source == null)
                return null;

            // 🧱 Étape 1 — Mapper les propriétés de base (grâce au GenericMapper)
            var dto = base.Map(source);

            // 🧩 Étape 2 — Choisir la DataInfo selon la langue
            var langueKey = (lang ?? Constantes.FR).ToUpperInvariant();
            var prop = typeof(Pokemon).GetProperty(langueKey);

            if (prop != null && prop.GetValue(source) is DataInfo dataInfo)
            {
                dto.DataInfo = _dataInfoMapper.Map(dataInfo);
            }

            return new PokemonLightDto()
            {
                Id = dto.Id,
                Number = dto.Number,
                DataInfo = dto.DataInfo,
                TypePoks = source.Pokemon_TypePoks?.Any() == true ? source.Pokemon_TypePoks
                    .Select(t => _typeMapper.MapLight(t.TypePok, langueKey))
                    .ToList() : null,
                TypeEvolution = dto.TypeEvolution,
                PathImgLegacy = dto.PathImgLegacy,
                PathImgNormal = dto.PathImgNormal,
                PathImgShiny = dto.PathImgShiny,
                PathSpriteLegacy = dto.PathSpriteLegacy,
                PathSpriteNormal = dto.PathSpriteNormal,
                PathSpriteShiny = dto.PathSpriteShiny,
                PathAnimatedImg = dto.PathAnimatedImg,
                PathAnimatedImgShiny = dto.PathAnimatedImgShiny,
            };
        }

        public FamilyDto MapFamily(Pokemon source, string lang)
        {
            if (source == null)
                return null;

            // 🧱 Étape 1 — Mapper les propriétés de base (grâce au GenericMapper)
            var dto = base.Map(source);

            // 🧩 Étape 2 — Choisir la DataInfo selon la langue
            var langueKey = (lang ?? Constantes.FR).ToUpperInvariant();
            var prop = typeof(Pokemon).GetProperty(langueKey);

            if (prop != null && prop.GetValue(source) is DataInfo dataInfo)
            {
                dto.DataInfo = _dataInfoMapper.Map(dataInfo);
            }

            return new FamilyDto()
            {
                Id = dto.Id,
                Number = dto.Number,
                DataInfo = dto.DataInfo,
                TypePoks = source.Pokemon_TypePoks?.Any() == true ? source.Pokemon_TypePoks
                    .Select(t => _typeMapper.MapLight(t.TypePok, langueKey))
                    .ToList() : null,
                TypeEvolution = dto.TypeEvolution,
                WhenEvolution = _evolveToMapper.Map(source.EvolvesFrom, langueKey).WhenEvolution ?? "",
                PathImgNormal = dto.PathImgNormal,
                PathSpriteNormal = dto.PathSpriteNormal
            };
        }

        public Pokemon MapReverse(PokemonDto dto, ScrapingContext context, string lang)
        {
            if (dto == null) return null;

            // Étape 1 — Mapper les propriétés communes
            var entity = base.MapReverse(dto, context);

            // Étape 2 — Rattacher la DataInfo selon la langue
            var languePropertyName = lang?.ToUpper();

            if (!string.IsNullOrEmpty(languePropertyName) && dto.DataInfo != null)
            {
                var property = typeof(Pokemon).GetProperty(languePropertyName);
                if (property != null)
                {
                    var dataInfoMapper = new GenericMapper<DataInfo, DataInfoDto>();
                    //var dataInfoEntity = dataInfoMapper.Map(dto.DataInfo);
                    //property.SetValue(entity, dataInfoEntity);
                }
            }

            return entity;
        }
    }
}
