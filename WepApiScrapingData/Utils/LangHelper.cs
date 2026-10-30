using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;

namespace WepApiScrapingData.Utils
{
    /// <summary>
    /// Sélection des champs localisés (9 langues stockées en colonnes) pour les endpoints du jeu web.
    /// </summary>
    public static class LangHelper
    {
        public static readonly string[] Supported = ["FR", "EN", "ES", "IT", "DE", "RU", "CO", "CN", "JP"];

        public static string Normalize(string? lang)
        {
            var value = (lang ?? "FR").Trim().ToUpperInvariant();
            return Supported.Contains(value) ? value : "FR";
        }

        public static IQueryable<Pokemon> IncludeLang(this IQueryable<Pokemon> query, string lang) => lang switch
        {
            "EN" => query.Include(p => p.EN),
            "ES" => query.Include(p => p.ES),
            "IT" => query.Include(p => p.IT),
            "DE" => query.Include(p => p.DE),
            "RU" => query.Include(p => p.RU),
            "CO" => query.Include(p => p.CO),
            "CN" => query.Include(p => p.CN),
            "JP" => query.Include(p => p.JP),
            _ => query.Include(p => p.FR)
        };

        public static DataInfo Info(Pokemon pokemon, string lang) => lang switch
        {
            "EN" => pokemon.EN,
            "ES" => pokemon.ES,
            "IT" => pokemon.IT,
            "DE" => pokemon.DE,
            "RU" => pokemon.RU,
            "CO" => pokemon.CO,
            "CN" => pokemon.CN,
            "JP" => pokemon.JP,
            _ => pokemon.FR
        };

        public static string? PokemonName(Pokemon pokemon, string lang)
        {
            var info = Info(pokemon, lang);
            return string.IsNullOrWhiteSpace(info?.DisplayName) ? info?.Name : info.DisplayName;
        }

        public static string? TypeName(TypePok type, string lang) => lang switch
        {
            "EN" => type.Name_EN,
            "ES" => type.Name_ES,
            "IT" => type.Name_IT,
            "DE" => type.Name_DE,
            "RU" => type.Name_RU,
            "CO" => type.Name_CO,
            "CN" => type.Name_CN,
            "JP" => type.Name_JP,
            _ => type.Name_FR
        } ?? type.Name_FR;

        public static string? TypeImage(TypePok type, string lang) => lang switch
        {
            "EN" => type.PathMiniHome_EN,
            "ES" => type.PathMiniHome_ES,
            "IT" => type.PathMiniHome_IT,
            "DE" => type.PathMiniHome_DE,
            "RU" => type.PathMiniHome_RU,
            "CO" => type.PathMiniHome_CO,
            "CN" => type.PathMiniHome_CN,
            "JP" => type.PathMiniHome_JP,
            _ => type.PathMiniHome_FR
        } ?? type.PathMiniHome_FR;

        public static string? AbilityName(Ability ability, string lang) => lang switch
        {
            "EN" => ability.Name_EN,
            "ES" => ability.Name_ES,
            "IT" => ability.Name_IT,
            "DE" => ability.Name_DE,
            "RU" => ability.Name_RU,
            "CO" => ability.Name_CO,
            "CN" => ability.Name_CN,
            "JP" => ability.Name_JP,
            _ => ability.Name_FR
        } ?? ability.Name_FR;

        public static string? AbilityDescription(Ability ability, string lang) => lang switch
        {
            "EN" => ability.Description_EN,
            "ES" => ability.Description_ES,
            "IT" => ability.Description_IT,
            "DE" => ability.Description_DE,
            "RU" => ability.Description_RU,
            "CO" => ability.Description_CO,
            "CN" => ability.Description_CN,
            "JP" => ability.Description_JP,
            _ => ability.Description_FR
        } ?? ability.Description_FR;

        public static string? QuestionTypeLabel(QuestionType questionType, string lang) => lang switch
        {
            "EN" => questionType.Libelle_EN,
            "ES" => questionType.Libelle_ES,
            "IT" => questionType.Libelle_IT,
            "DE" => questionType.Libelle_DE,
            "RU" => questionType.Libelle_RU,
            "CO" => questionType.Libelle_CO,
            "CN" => questionType.Libelle_CN,
            "JP" => questionType.Libelle_JP,
            _ => questionType.Libelle_FR
        } ?? questionType.Libelle_FR;

        public static string? DifficultyLabel(Difficulty difficulty, string lang) => lang switch
        {
            "EN" => difficulty.Libelle_EN,
            "ES" => difficulty.Libelle_ES,
            "IT" => difficulty.Libelle_IT,
            "DE" => difficulty.Libelle_DE,
            "RU" => difficulty.Libelle_RU,
            "CO" => difficulty.Libelle_CO,
            "CN" => difficulty.Libelle_CN,
            "JP" => difficulty.Libelle_JP,
            _ => difficulty.Libelle_FR
        } ?? difficulty.Libelle_FR;
    }
}
