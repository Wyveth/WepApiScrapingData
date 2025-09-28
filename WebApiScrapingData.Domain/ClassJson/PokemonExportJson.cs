namespace WebApiScrapingData.Domain.ClassJson
{
    public class PokemonExportJson
    {
        public string? Number { get; set; }
        public DataInfoExportJson? FR { get; set; }
        public DataInfoExportJson? EN { get; set; }
        public DataInfoExportJson? ES { get; set; }
        public DataInfoExportJson? IT { get; set; }
        public DataInfoExportJson? DE { get; set; }
        public DataInfoExportJson? RU { get; set; }
        public DataInfoExportJson? CO { get; set; }
        public DataInfoExportJson? CN { get; set; }
        public DataInfoExportJson? JP { get; set; }
        public List<TypesPokExportJson>? Types { get; set; }
        public List<TypesPokExportJson>? Weaknesses { get; set; }
        public List<TalentsExportJson>? Talents { get; set; }
        public List<AttaquesExportJson>? Attaques { get; set; }
        public string? TypeEvolution { get; set; }
        public string? StatPv { get; set; }
        public string? StatAttaque { get; set; }
        public string? StatDefense { get; set; }
        public string? StatAttaqueSpe { get; set; }
        public string? StatDefenseSpe { get; set; }
        public string? StatVitesse { get; set; }
        public string? StatTotal { get; set; }
        public string? EggMoves { get; set; }
        public string? CaptureRate { get; set; }
        public string? BasicHappiness { get; set; }
        public string? Generation { get; set; }
        public string? UrlImg { get; set; }
        public string? PathImgLegacy { get; set; }
        public string? PathImgNormal { get; set; }
        public string? PathImgShiny { get; set; }
        public string? PathAnimatedImg { get; set; }
        public string? PathAnimatedImgShiny { get; set; }
        public string? UrlSprite { get; set; }
        public string? PathSpriteLegacy { get; set; }
        public string? PathSpriteNormal { get; set; }
        public string? PathSpriteShiny { get; set; }
        public string? UrlSound { get; set; }
        public string? PathSound { get; set; }
        public string? PathSoundLegacy { get; set; }
        public string? PathSoundCurrent { get; set; }
        public GameExportJson? Game { get; set; }
    }

    public class DataInfoExportJson
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? DescriptionVx { get; set; }
        public string? DescriptionVy { get; set; }
        public string? Size { get; set; }
        public string? Category { get; set; }
        public string? Weight { get; set; }
        public string? Talent { get; set; }
        public string? DescriptionTalent { get; set; }
        public string? Types { get; set; }
        public string? Weakness { get; set; }
        public string? Evolutions { get; set; }
        public string? WhenEvolution { get; set; }
        public string? NextUrl { get; set; }
    }

    public class TypesPokExportJson
    {
        public TypePokExportJson? TypePok { get; set; }
    }
    
    public class TypePokExportJson
    {
        public string? Name_FR { get; set; }
        public string? UrlMiniHome_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? UrlMiniHome_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? UrlMiniHome_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? UrlMiniHome_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? UrlMiniHome_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? UrlMiniHome_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? UrlMiniHome_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? UrlMiniHome_CN { get; set; }
        public string? Name_JP { get; set; }
        public string? UrlMiniHome_JP { get; set; }
        public string? UrlMiniGo { get; set; }
        public string? UrlFondGo { get; set; }
        public string? UrlIconHome { get; set; }
        public string? UrlAutoHome { get; set; }
        public string? ImgColor { get; set; }
        public string? InfoColor { get; set; }
        public string? TypeColor { get; set; }
    }

    public class AttaquesExportJson
    {
        public AttaqueExportJson? Attaque { get; set; }
        public string? TypeLearn { get; set; }
        public string? Level { get; set; }
        public string? CTCS { get; set; }
    }

    public class AttaqueExportJson
    {
        public string? Name_FR { get; set; }
        public string? Description_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? Description_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? Description_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? Description_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? Description_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? Description_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? Description_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? Description_CN { get; set; }
        public string? Name_JP { get; set; }
        public string? Description_JP { get; set; }
        public TypeAttaqueExportJson? TypeAttaque { get; set; }
        public TypePokExportJson? Types { get; set; }
        public string? Puissance { get; set; }
        public string? Precision { get; set; }
        public string? PP { get; set; }
    }

    public class TypeAttaqueExportJson
    {
        public string? Name_FR { get; set; }
        public string? Description_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? Description_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? Description_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? Description_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? Description_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? Description_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? Description_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? Description_CN { get; set; }
        public string? Name_JP { get; set; }
        public string? Description_JP { get; set; }
        public string? UrlImg { get; set; }
    }

    public class TalentsExportJson
    {
        public TalentExportJson? Talent { get; set; }
        public bool IsHidden { get; set; }
    }

    public class TalentExportJson
    {
        public string? Name_FR { get; set; }
        public string? Description_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? Description_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? Description_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? Description_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? Description_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? Description_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? Description_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? Description_CN { get; set; }
        public string? Name_JP { get; set; }
        public string? Description_JP { get; set; }

    }

    public class GameExportJson
    {
        public string? Name_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? Name_JP { get; set; }
    }
}
