namespace WepApiScrapingData.Services.Game
{
    /// <summary>Réglages du pendu (section "Hangman" de appsettings.json), pris en compte au redémarrage de l'API.</summary>
    public class HangmanOptions
    {
        public const string Section = "Hangman";

        /// <summary>Vies au départ.</summary>
        public int StartLives { get; set; } = 7;
        /// <summary>Vies rendues à chaque Pokémon trouvé.</summary>
        public int LivesPerFound { get; set; } = 2;
        /// <summary>Plafond de vies.</summary>
        public int MaxLives { get; set; } = 10;

        /// <summary>Nombre d'erreurs sur le Pokémon en cours qui débloque chaque indice (difficulté Facile uniquement).</summary>
        public int TypeHintAt { get; set; } = 1;
        public int GenerationHintAt { get; set; } = 2;
        public int DescriptionHintAt { get; set; } = 4;
        public int SilhouetteHintAt { get; set; } = 6;
    }
}
