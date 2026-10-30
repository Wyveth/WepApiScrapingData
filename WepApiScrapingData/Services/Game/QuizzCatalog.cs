using System.Reflection;
using WebApiScrapingData.Infrastructure.Utils;

namespace WepApiScrapingData.Services.Game
{
    /// <summary>
    /// Définition d'un type de question (porté depuis QuestionTypeService.Populate de l'application Xamarin).
    /// </summary>
    public sealed record QuestionTypeDefinition(
        string Code,
        string DifficultyCode,
        int NbAnswers,
        int NbAnswersPossible,
        bool IsMultipleAnswers = false,
        bool IsBlurred = false,
        bool IsGrayscale = false,
        bool IsHide = false);

    /// <summary>
    /// Catalogue des types de questions et des difficultés du quiz.
    /// Il sert de référence pour alimenter les tables Difficulty et QuestionType si elles sont vides.
    /// </summary>
    public static class QuizzCatalog
    {
        public static readonly string[] DifficultyCodes = [Constantes.Easy_Code, Constantes.Normal_Code, Constantes.Hard_Code];

        public static readonly IReadOnlyList<QuestionTypeDefinition> QuestionTypes = BuildQuestionTypes();

        /// <summary>Nombre de questions par quiz selon la difficulté la plus facile cochée (règle Xamarin).</summary>
        public static int NbQuestions(bool easy, bool normal, bool hard) => easy ? 10 : normal ? 15 : hard ? 20 : 10;

        /// <summary>
        /// Libellé localisé lu dans Constantes : {Prefix}_Libelle_{LANG}. Les codes Blurred/Black réutilisent le libellé "Pok".
        /// </summary>
        public static string? Label(string prefix, string lang)
        {
            var field = typeof(Constantes).GetField($"{prefix}_Libelle_{lang}", BindingFlags.Public | BindingFlags.Static);
            return field?.GetValue(null) as string;
        }

        public static string QuestionTypePrefix(string code)
        {
            foreach (var field in typeof(Constantes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.Name.StartsWith("QTyp") && field.Name.EndsWith("_Code") && Equals(field.GetValue(null), code))
                    return field.Name[..^"_Code".Length];
            }
            return "QTypPok";
        }

        /// <summary>Libellé court des statistiques (PV, Att., ...) pour la question PokStat.</summary>
        public static string StatLabel(string statKey, string lang)
        {
            var prefix = statKey switch
            {
                Constantes.Pv => "HP",
                Constantes.Attack => "Atk",
                Constantes.Defense => "Def",
                Constantes.AttackSpe => "SpAtk",
                Constantes.DefenseSpe => "SpDef",
                _ => "Speed"
            };
            var field = typeof(Constantes).GetField($"{prefix}_{lang}", BindingFlags.Public | BindingFlags.Static)
                     ?? typeof(Constantes).GetField($"{prefix}_EN", BindingFlags.Public | BindingFlags.Static);
            return field?.GetValue(null) as string ?? statKey;
        }

        public static readonly string[] StatKeys =
            [Constantes.Pv, Constantes.Attack, Constantes.Defense, Constantes.AttackSpe, Constantes.DefenseSpe, Constantes.Speed];

        private static List<QuestionTypeDefinition> BuildQuestionTypes()
        {
            const string E = Constantes.Easy_Code, N = Constantes.Normal_Code, H = Constantes.Hard_Code;
            return
            [
                // Facile
                new(Constantes.QTypPok_Code, E, 4, 1),
                new(Constantes.QTypPokDesc_Code, E, 4, 1),
                new(Constantes.QTypPokDescReverse_Code, E, 4, 1),
                new(Constantes.QTypTypPok_Code, E, 6, 1),
                new(Constantes.QTypTyp_Code, E, 6, 1),
                new(Constantes.QTypTalent_Code, E, 4, 1),
                new(Constantes.QTypTalentReverse_Code, E, 4, 1),
                new(Constantes.QTypPokStat_Code, E, 4, 1),
                new(Constantes.QTypTypPokVarious_Code, E, 6, 0, IsMultipleAnswers: true),
                new(Constantes.QTypWeakPokVarious_Code, E, 6, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokTalentVarious_Code, E, 4, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokFamilyVarious_Code, E, 4, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokTypVarious_Code, E, 4, 0, IsMultipleAnswers: true),

                // Normal
                new(Constantes.QTypPok_Code, N, 8, 1),
                new(Constantes.QTypPok_Code, N, 8, 1, IsBlurred: true),
                new(Constantes.QTypPok_Code, N, 8, 1, IsHide: true),
                new(Constantes.QTypPokDesc_Code, N, 6, 1),
                new(Constantes.QTypPokDescReverse_Code, N, 6, 1),
                new(Constantes.QTypTypPok_Code, N, 12, 1),
                new(Constantes.QTypTypPok_Code, N, 12, 1, IsBlurred: true),
                new(Constantes.QTypTypPok_Code, N, 12, 1, IsHide: true),
                new(Constantes.QTypTyp_Code, N, 12, 1),
                new(Constantes.QTypTalent_Code, N, 6, 1),
                new(Constantes.QTypTalentReverse_Code, N, 6, 1),
                new(Constantes.QTypPokStat_Code, N, 8, 1),
                new(Constantes.QTypTypPokVarious_Code, N, 12, 0, IsMultipleAnswers: true),
                new(Constantes.QTypTypPokVarious_Code, N, 12, 0, IsMultipleAnswers: true, IsBlurred: true),
                new(Constantes.QTypTypPokVarious_Code, N, 12, 0, IsMultipleAnswers: true, IsHide: true),
                new(Constantes.QTypWeakPokVarious_Code, N, 12, 0, IsMultipleAnswers: true),
                new(Constantes.QTypWeakPokVarious_Code, N, 12, 0, IsMultipleAnswers: true, IsBlurred: true),
                new(Constantes.QTypWeakPokVarious_Code, N, 12, 0, IsMultipleAnswers: true, IsHide: true),
                new(Constantes.QTypPokTalentVarious_Code, N, 8, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokFamilyVarious_Code, N, 8, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokTypVarious_Code, N, 8, 0, IsMultipleAnswers: true),

                // Difficile
                new(Constantes.QTypPok_Code, H, 12, 1),
                new(Constantes.QTypPok_Code, H, 12, 1, IsBlurred: true, IsGrayscale: true),
                new(Constantes.QTypPok_Code, H, 12, 1, IsHide: true),
                new(Constantes.QTypPok_Code, H, 12, 1, IsBlurred: true, IsHide: true),
                new(Constantes.QTypPokDesc_Code, H, 8, 1),
                new(Constantes.QTypPokDescReverse_Code, H, 8, 1),
                new(Constantes.QTypTypPok_Code, H, 18, 1),
                new(Constantes.QTypTypPok_Code, H, 18, 1, IsBlurred: true, IsGrayscale: true),
                new(Constantes.QTypTypPok_Code, H, 18, 1, IsHide: true),
                new(Constantes.QTypTypPok_Code, H, 18, 1, IsBlurred: true, IsHide: true),
                new(Constantes.QTypTyp_Code, H, 18, 1),
                new(Constantes.QTypTalent_Code, H, 8, 1),
                new(Constantes.QTypTalentReverse_Code, H, 8, 1),
                new(Constantes.QTypPokStat_Code, H, 12, 1),
                new(Constantes.QTypTypPokVarious_Code, H, 18, 0, IsMultipleAnswers: true),
                new(Constantes.QTypTypPokVarious_Code, H, 18, 0, IsMultipleAnswers: true, IsBlurred: true, IsGrayscale: true),
                new(Constantes.QTypTypPokVarious_Code, H, 18, 0, IsMultipleAnswers: true, IsHide: true),
                new(Constantes.QTypTypPokVarious_Code, H, 18, 0, IsMultipleAnswers: true, IsBlurred: true, IsHide: true),
                new(Constantes.QTypWeakPokVarious_Code, H, 18, 0, IsMultipleAnswers: true),
                new(Constantes.QTypWeakPokVarious_Code, H, 18, 0, IsMultipleAnswers: true, IsBlurred: true, IsGrayscale: true),
                new(Constantes.QTypWeakPokVarious_Code, H, 18, 0, IsMultipleAnswers: true, IsHide: true),
                new(Constantes.QTypWeakPokVarious_Code, H, 18, 0, IsMultipleAnswers: true, IsBlurred: true, IsHide: true),
                new(Constantes.QTypPokTalentVarious_Code, H, 12, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokFamilyVarious_Code, H, 12, 0, IsMultipleAnswers: true),
                new(Constantes.QTypPokTypVarious_Code, H, 12, 0, IsMultipleAnswers: true),
                // Difficile uniquement : reconnaître le Pokémon à son cri (sans image)
                new(Constantes.QTypPokCry_Code, H, 8, 1),
            ];
        }

        /// <summary>
        /// Groupes de codes tirés au sort selon la difficulté, avec leur poids (sur 100).
        /// Reprend la répartition de QuestionTypeRepository.GetQuestionTypeRandomBy*Difficulty.
        /// </summary>
        public static (int UpperBound, string[] Codes)[] Weights(string difficultyCode)
        {
            if (difficultyCode == Constantes.Hard_Code)
                return new (int, string[])[]
                {
                    (3, new[] { Constantes.QTypPokStat_Code }),
                    (6, new[] { Constantes.QTypTyp_Code }),
                    (9, new[] { Constantes.QTypTalent_Code, Constantes.QTypTalentReverse_Code, Constantes.QTypPokTalentVarious_Code }),
                    (12, new[] { Constantes.QTypPokDesc_Code, Constantes.QTypPokDescReverse_Code }),
                    (15, new[] { Constantes.QTypPokFamilyVarious_Code, Constantes.QTypPokTypVarious_Code }),
                    (20, new[] { Constantes.QTypTypPok_Code, Constantes.QTypTypPokVarious_Code, Constantes.QTypWeakPokVarious_Code }),
                    (25, new[] { Constantes.QTypPokCry_Code }),
                    (99, new[] { Constantes.QTypPok_Code }),
                };

            if (difficultyCode == Constantes.Normal_Code)
                return new (int, string[])[]
                {
                    (3, new[] { Constantes.QTypTyp_Code }),
                    (6, new[] { Constantes.QTypTalent_Code, Constantes.QTypTalentReverse_Code, Constantes.QTypPokTalentVarious_Code }),
                    (9, new[] { Constantes.QTypPokDesc_Code, Constantes.QTypPokDescReverse_Code }),
                    (12, new[] { Constantes.QTypPokFamilyVarious_Code, Constantes.QTypPokTypVarious_Code }),
                    (15, new[] { Constantes.QTypTypPok_Code, Constantes.QTypTypPokVarious_Code }),
                    (99, new[] { Constantes.QTypPok_Code }),
                };

            return new (int, string[])[]
            {
                (3, new[] { Constantes.QTypTyp_Code }),
                (6, new[] { Constantes.QTypTalent_Code, Constantes.QTypTalentReverse_Code }),
                (9, new[] { Constantes.QTypPokDesc_Code, Constantes.QTypPokDescReverse_Code }),
                (12, new[] { Constantes.QTypPokFamilyVarious_Code }),
                (15, new[] { Constantes.QTypTypPok_Code }),
                (99, new[] { Constantes.QTypPok_Code }),
            };
        }
    }
}
