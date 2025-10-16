using HtmlAgilityPack;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Mapper;
using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.Mapper
{
    public class PokemonMapper : GenericMapper<Pokemon, PokemonDto>
    {
        private readonly GenericMapper<DataInfo, DataInfoDto> _dataInfoMapper;
        private readonly TypePokMapper _typeMapper;
        private readonly TalentMapper _talentMapper;
        private readonly AttaqueMapper _attaqueMapper;
        private readonly GameMapper _gameMapper;

        public PokemonMapper(
            GenericMapper<DataInfo, DataInfoDto> dataInfoMapper,
            TypePokMapper typeMapper,
            TalentMapper talentMapper,
            AttaqueMapper attaqueMapper,
            GameMapper gameMapper)
        {
            _dataInfoMapper = dataInfoMapper;
            _typeMapper = typeMapper;
            _talentMapper = talentMapper;
            _attaqueMapper = attaqueMapper;
            _gameMapper = gameMapper;
        }

        public override PokemonDto Map(Pokemon source, string lang)
        {
            if (source == null)
                return null;

            // 🧱 Étape 1 — Mapper les propriétés de base (grâce au GenericMapper)
            var dto = base.Map(source);

            // 🧩 Étape 2 — Choisir la DataInfo selon la langue
            var langueKey = (lang ?? "FR").ToUpperInvariant();
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
            if (source.Pokemon_Talents?.Any() == true)
            {
                dto.Talents = source.Pokemon_Talents
                    .Select(t => _talentMapper.Map(t, langueKey))
                    .ToList();
            }

            // 🔹 Attaques
            if (source.Pokemon_Attaques?.Any() == true)
            {
                dto.Attaques = source.Pokemon_Attaques
                    .Select(a => _attaqueMapper.Map(a, langueKey))
                    .ToList();
            }

            if(source.Game != null)
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
            var langueKey = (lang ?? "FR").ToUpperInvariant();
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

            return new PokemonLightDto()
            {
                Id = dto.Id,
                Number = dto.Number,
                DataInfo = dto.DataInfo,
                TypePoks = dto.TypePoks,
                PathImgLegacy = dto.PathImgLegacy,
                PathImgNormal = dto.PathImgNormal,
                PathImgShiny = dto.PathImgShiny,
                PathSpriteLegacy = dto.PathSpriteLegacy,
                PathSpriteNormal = dto.PathSpriteNormal,
                PathSpriteShiny = dto.PathSpriteShiny,
                PathSound = dto.PathSound,
                PathSoundLegacy = dto.PathSoundLegacy,
                PathSoundCurrent = dto.PathSoundCurrent,
                PathAnimatedImg = dto.PathAnimatedImg,
                PathAnimatedImgShiny = dto.PathAnimatedImgShiny,
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
