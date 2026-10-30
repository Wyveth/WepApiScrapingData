using Microsoft.AspNetCore.Identity;

namespace WebApiScrapingData.Domain.Class.Hangman
{
    public enum HangmanMode
    {
        Solo = 0,
        /// <summary>Deux comptes, tour par tour, vies partagées.</summary>
        Coop = 1
    }

    public enum HangmanStatus
    {
        /// <summary>Coop : invitation envoyée, en attente de réponse.</summary>
        Invited = 0,
        InProgress = 1,
        Finished = 2,
        Declined = 3,
        Cancelled = 4
    }

    /// <summary>
    /// Partie de pendu : on enchaîne les Pokémon à deviner lettre par lettre tant qu'il reste des vies.
    /// Le nom à deviner n'est jamais envoyé au client avant d'être trouvé (ou la partie perdue).
    /// </summary>
    public class HangmanGame
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }

        public HangmanMode Mode { get; set; }
        public HangmanStatus Status { get; set; }

        /// <summary>Langue des noms à deviner et des indices (FR, EN, ES, IT, DE), fixée à la création.</summary>
        public string Lang { get; set; } = "FR";

        /// <summary>Générations choisies, séparées par des virgules (vide = toutes).</summary>
        public string? Generations { get; set; }

        /// <summary>Solo invité : null. Coop : celui qui invite.</summary>
        public string? Player1Id { get; set; }
        public IdentityUser? Player1 { get; set; }

        /// <summary>Coop : le partenaire invité.</summary>
        public string? Player2Id { get; set; }
        public IdentityUser? Player2 { get; set; }

        /// <summary>Coop : joueur qui doit proposer la prochaine lettre.</summary>
        public string? CurrentTurnUserId { get; set; }

        public int Lives { get; set; }
        public int MaxLives { get; set; }

        /// <summary>Nombre de Pokémon trouvés (score de la partie).</summary>
        public int Found { get; set; }

        /// <summary>Pokémon à deviner et état de la grille.</summary>
        public long? CurrentPokemonId { get; set; }
        /// <summary>Lettres proposées pour le Pokémon en cours (A à Z, sans séparateur).</summary>
        public string GuessedLetters { get; set; } = string.Empty;
        /// <summary>Erreurs sur le Pokémon en cours (débloquent les indices).</summary>
        public int WrongCount { get; set; }

        /// <summary>Pokémon déjà tirés (évite les répétitions), séparés par des virgules.</summary>
        public string? UsedPokemonIds { get; set; }

        /// <summary>Dernier Pokémon terminé (trouvé ou, en fin de partie, raté) pour l'afficher au joueur.</summary>
        public long? LastPokemonId { get; set; }
        public bool LastFound { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
