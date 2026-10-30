namespace WepApiScrapingData.Services.Game
{
    /// <summary>
    /// Réglages des duels (section "Duel" de appsettings.json). Modifiables sans recompiler, pris en compte au redémarrage de l'API.
    /// Une nouvelle durée par question ne s'applique qu'aux duels créés ensuite (elle est enregistrée sur chaque duel).
    /// </summary>
    public class DuelOptions
    {
        public const string Section = "Duel";

        /// <summary>Temps pour répondre à une question, en secondes, par code de difficulté.</summary>
        public Dictionary<string, int> QuestionSeconds { get; set; } = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Easy"] = 15,
            ["Normal"] = 20,
            ["Hard"] = 25
        };

        /// <summary>Points de classement d'une victoire, par code de difficulté.</summary>
        public Dictionary<string, int> WinPoints { get; set; } = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Easy"] = 1,
            ["Normal"] = 2,
            ["Hard"] = 3
        };

        /// <summary>Points de classement d'une victoire pour cette difficulté (1 si non configurée).</summary>
        public int WinPointsFor(string difficultyCode) =>
            Math.Max(0, WinPoints.TryGetValue(difficultyCode, out var points) ? points : 1);

        /// <summary>Compte à rebours avant la première question (temps réel).</summary>
        public int CountdownSeconds { get; set; } = 5;

        /// <summary>Pause entre deux questions, le temps de voir la correction (temps réel).</summary>
        public int PauseSeconds { get; set; } = 4;

        /// <summary>Tolérance réseau après la fin du chrono.</summary>
        public int GraceSeconds { get; set; } = 1;

        /// <summary>Durée de vie d'une recherche d'adversaire en temps réel.</summary>
        public int LiveSearchTimeoutMinutes { get; set; } = 2;

        /// <summary>Durée de vie d'un défi privé en temps réel.</summary>
        public int LiveInvitationTimeoutMinutes { get; set; } = 10;

        /// <summary>Durée de vie d'une recherche ou d'un défi à son rythme.</summary>
        public int AsyncTimeoutDays { get; set; } = 7;

        /// <summary>Durée d'une question pour cette difficulté, bornée entre 5 et 120 s (20 s si non configurée).</summary>
        public int SecondsFor(string difficultyCode) =>
            Math.Clamp(QuestionSeconds.TryGetValue(difficultyCode, out var seconds) ? seconds : 20, 5, 120);
    }
}
