namespace WepApiScrapingData.DTOs.Play
{
    /// <summary>Recherche d'un adversaire (matchmaking par difficulté).</summary>
    public class DuelSearchDto
    {
        /// <summary>"Live" (temps réel) ou "Async" (chacun à son rythme).</summary>
        public string Mode { get; set; } = "Live";
        /// <summary>"Easy", "Normal" ou "Hard".</summary>
        public string Difficulty { get; set; } = "Easy";
    }

    /// <summary>Défi privé envoyé à un joueur via son code ami.</summary>
    public class DuelChallengeDto
    {
        public string FriendCode { get; set; } = string.Empty;
        public string Mode { get; set; } = "Live";
        public string Difficulty { get; set; } = "Easy";
        /// <summary>1 à 9, 0 pour Hisui. Vide = toutes.</summary>
        public List<int> Generations { get; set; } = new();
    }

    public class FriendCodeDto
    {
        /// <summary>Code au format ABCD-EFGH.</summary>
        public string Code { get; set; } = string.Empty;
    }

    public class DuelPlayerDto
    {
        public string? UserName { get; set; }
        public int Score { get; set; }
        public int Answered { get; set; }
        public bool Done { get; set; }
    }

    public class DuelDto
    {
        public Guid Id { get; set; }
        /// <summary>Live ou Async.</summary>
        public string Mode { get; set; } = string.Empty;
        /// <summary>Searching, Invited, InProgress, Finished, Declined, Cancelled, Expired.</summary>
        public string Status { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public List<int> Generations { get; set; } = new();
        public bool IsPrivate { get; set; }
        /// <summary>Vrai si je suis à l'origine du duel (recherche ou défi).</summary>
        public bool IsChallenger { get; set; }

        public DuelPlayerDto Me { get; set; } = new();
        public DuelPlayerDto? Opponent { get; set; }

        public int Total { get; set; }
        /// <summary>Prochaine question à jouer pour moi (null si j'ai terminé).</summary>
        public int? NextOrder { get; set; }
        /// <summary>Vrai si je peux jouer une question maintenant.</summary>
        public bool CanPlay { get; set; }

        /// <summary>Temps réel : question en cours et son chrono (UTC).</summary>
        public int CurrentOrder { get; set; }
        public DateTime? QuestionStartsAt { get; set; }
        public DateTime? QuestionEndsAt { get; set; }
        public int QuestionSeconds { get; set; }
        /// <summary>Heure du serveur, pour corriger le décalage d'horloge du client.</summary>
        public DateTime ServerTime { get; set; }

        /// <summary>Win, Loss ou Draw une fois le duel terminé.</summary>
        public string? Result { get; set; }
        /// <summary>Vrai si le duel s'est terminé par un abandon.</summary>
        public bool Forfeit { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }

    /// <summary>Réglages des duels exposés au client (appsettings.json, section "Duel").</summary>
    public class DuelSettingsDto
    {
        /// <summary>Secondes pour répondre, par code de difficulté (Easy, Normal, Hard).</summary>
        public Dictionary<string, int> QuestionSeconds { get; set; } = new();
        public int CountdownSeconds { get; set; }
        public int PauseSeconds { get; set; }
        /// <summary>Points de classement d'une victoire, par code de difficulté.</summary>
        public Dictionary<string, int> WinPoints { get; set; } = new();
    }

    public class LeaderboardEntryDto
    {
        public int Rank { get; set; }
        public string? UserName { get; set; }
        public bool IsMe { get; set; }
        /// <summary>Connecté en ce moment (site ouvert, compte connecté).</summary>
        public bool IsOnline { get; set; }
        /// <summary>Victoires pondérées par la difficulté (1/2/3 par défaut).</summary>
        public int RankingPoints { get; set; }
        /// <summary>Somme des points marqués en duel (bonnes réponses + rapidité), victoires et défaites comprises.</summary>
        public int DuelPoints { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }

    public class LeaderboardDto
    {
        /// <summary>"all" (général) ou "month" (mois en cours, UTC).</summary>
        public string Period { get; set; } = "all";
        /// <summary>"wins" (points de classement, puis points marqués) ou "points" (points marqués, puis points de classement).</summary>
        public string Sort { get; set; } = "wins";
        public DateTime? Since { get; set; }
        public List<LeaderboardEntryDto> Entries { get; set; } = new();
        /// <summary>Ma ligne, si je suis connecté et classé hors du top affiché.</summary>
        public LeaderboardEntryDto? Me { get; set; }
    }

    public class DuelStatsDto
    {
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}
