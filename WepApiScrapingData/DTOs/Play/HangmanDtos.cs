namespace WepApiScrapingData.DTOs.Play
{
    /// <summary>Nouvelle partie de pendu en solo (compte facultatif).</summary>
    public class HangmanCreateDto
    {
        /// <summary>1 à 9, 0 pour Hisui. Vide = toutes.</summary>
        public List<int> Generations { get; set; } = new();
        /// <summary>Langue des noms à deviner : FR, EN, ES, IT ou DE.</summary>
        public string? Lang { get; set; } = "FR";
    }

    /// <summary>Invitation à une partie de pendu en coop, via le code ami du partenaire.</summary>
    public class HangmanInviteDto : HangmanCreateDto
    {
        public string FriendCode { get; set; } = string.Empty;
    }

    public class HangmanGuessDto
    {
        /// <summary>Une lettre ; les accents sont ignorés (É = E).</summary>
        public string Letter { get; set; } = string.Empty;
    }

    public class HangmanLetterDto
    {
        public string Letter { get; set; } = string.Empty;
        public bool Correct { get; set; }
    }

    public class HangmanTypeHintDto
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
    }

    public class HangmanHintsDto
    {
        /// <summary>Types du Pokémon (débloqués après quelques erreurs).</summary>
        public List<HangmanTypeHintDto>? Types { get; set; }
        /// <summary>Génération (0 = Hisui).</summary>
        public int? Generation { get; set; }
        /// <summary>Description, nom du Pokémon masqué.</summary>
        public string? Description { get; set; }
        /// <summary>Si vrai, la silhouette s'obtient via GET Hangman/{id}/silhouette.</summary>
        public bool Silhouette { get; set; }
    }

    /// <summary>Pokémon précédent, révélé une fois trouvé (ou en fin de partie s'il n'a pas été trouvé).</summary>
    public class HangmanRevealDto
    {
        public string? Name { get; set; }
        /// <summary>Chemin relatif de l'image, à préfixer par img_root.</summary>
        public string? Image { get; set; }
        public bool Found { get; set; }
    }

    public class HangmanDto
    {
        public Guid Id { get; set; }
        /// <summary>Solo ou Coop.</summary>
        public string Mode { get; set; } = string.Empty;
        /// <summary>Invited, InProgress, Finished, Declined, Cancelled.</summary>
        public string Status { get; set; } = string.Empty;
        public string Lang { get; set; } = "FR";
        public List<int> Generations { get; set; } = new();

        public int Lives { get; set; }
        public int MaxLives { get; set; }
        /// <summary>Pokémon trouvés (score).</summary>
        public int Found { get; set; }

        /// <summary>Nom à deviner : lettres trouvées, "_" pour les lettres cachées ; espaces et signes visibles.</summary>
        public string? Mask { get; set; }
        public List<HangmanLetterDto> Letters { get; set; } = new();
        public int WrongCount { get; set; }
        public HangmanHintsDto Hints { get; set; } = new();
        /// <summary>Nombre d'erreurs (sur ce Pokémon) qui débloque le prochain indice, null s'il n'y en a plus.</summary>
        public int? NextHintAt { get; set; }

        public HangmanRevealDto? Last { get; set; }

        /// <summary>Coop : à moi de jouer (toujours vrai en solo pendant la partie).</summary>
        public bool IsMyTurn { get; set; }
        public string? TurnUserName { get; set; }
        /// <summary>Coop : nom du partenaire.</summary>
        public string? Partner { get; set; }
        /// <summary>Coop : vrai si j'ai envoyé l'invitation.</summary>
        public bool IsInviter { get; set; }

        /// <summary>Meilleur score du joueur connecté dans ce mode (parties terminées).</summary>
        public int? BestScore { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
