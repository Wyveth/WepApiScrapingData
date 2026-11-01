using Microsoft.AspNetCore.Identity;
using QuizzEntity = WebApiScrapingData.Domain.Class.Quizz.Quizz;

namespace WebApiScrapingData.Domain.Class.Duels
{
    /// <summary>Déroulement d'un duel.</summary>
    public enum DuelMode
    {
        /// <summary>Les deux joueurs répondent en même temps à chaque question, avec un chrono commun.</summary>
        Live = 0,
        /// <summary>Chaque joueur répond au même quiz à son rythme ; les scores sont comparés à la fin.</summary>
        Async = 1
    }

    public enum DuelStatus
    {
        /// <summary>Matchmaking : en attente d'un adversaire.</summary>
        Searching = 0,
        /// <summary>Défi privé envoyé, en attente de la réponse de l'adversaire.</summary>
        Invited = 1,
        InProgress = 2,
        Finished = 3,
        Declined = 4,
        Cancelled = 5,
        Expired = 6
    }

    /// <summary>
    /// Duel de quiz entre deux comptes. Chaque joueur a son propre quiz (copie identique),
    /// ce qui réutilise la correction et les sélections du jeu solo.
    /// </summary>
    public class Duel
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }

        public DuelMode Mode { get; set; }
        public DuelStatus Status { get; set; }

        /// <summary>Code de difficulté : Easy, Normal ou Hard.</summary>
        public string DifficultyCode { get; set; } = string.Empty;

        /// <summary>Générations choisies, séparées par des virgules (vide = toutes).</summary>
        public string? Generations { get; set; }

        /// <summary>Défi par code ami (exclu du matchmaking).</summary>
        public bool IsPrivate { get; set; }

        public string Player1Id { get; set; } = string.Empty;
        public IdentityUser? Player1 { get; set; }

        public string? Player2Id { get; set; }
        public IdentityUser? Player2 { get; set; }

        public long? Quizz1Id { get; set; }
        public QuizzEntity? Quizz1 { get; set; }

        public long? Quizz2Id { get; set; }
        public QuizzEntity? Quizz2 { get; set; }

        public int Score1 { get; set; }
        public int Score2 { get; set; }

        /// <summary>Gagnant ; null en cas d'égalité ou tant que le duel n'est pas terminé.</summary>
        public string? WinnerId { get; set; }

        /// <summary>Joueur ayant abandonné, le cas échéant.</summary>
        public string? ForfeitedById { get; set; }

        /// <summary>Temps réel : question en cours (1 à N) et début de son chrono.</summary>
        public int CurrentOrder { get; set; }
        public DateTime? QuestionStartedAt { get; set; }

        /// <summary>Durée de réponse d'une question, en secondes.</summary>
        public int QuestionSeconds { get; set; } = 20;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
