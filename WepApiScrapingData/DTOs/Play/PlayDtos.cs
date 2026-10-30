namespace WepApiScrapingData.DTOs.Play
{
    /// <summary>Options de création d'un quiz.</summary>
    public class QuizzCreateDto
    {
        /// <summary>Générations à inclure : 1 à 9, 0 pour Hisui (Légendes Arceus). Vide = toutes.</summary>
        public List<int> Generations { get; set; } = new();

        /// <summary>Codes de difficulté : "Easy", "Normal", "Hard". Vide = "Easy".</summary>
        public List<string> Difficulties { get; set; } = new();
    }

    public class AnswerSubmitDto
    {
        public List<long> AnswerIds { get; set; } = new();
    }

    public class QuizzSummaryDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<int> Generations { get; set; } = new();
        public List<string> Difficulties { get; set; } = new();
        public int NbQuestions { get; set; }
        public int NbAnswered { get; set; }
        public int NbCorrect { get; set; }
        public bool Done { get; set; }
        /// <summary>Ordre de la prochaine question sans réponse, null si le quiz est terminé.</summary>
        public int? NextOrder { get; set; }
        /// <summary>Vrai si le quiz appartient à un compte (faux pour un quiz invité).</summary>
        public bool IsOwned { get; set; }
    }

    public class QuestionSubjectDto
    {
        /// <summary>Pokemon, PokemonDescription, Type, Ability, AbilityDescription.</summary>
        public string Kind { get; set; } = string.Empty;
        /// <summary>Nom affiché, absent quand il constitue la réponse ou que l'image est masquée.</summary>
        public string? Name { get; set; }
        public string? Description { get; set; }
        /// <summary>Si vrai, l'image s'obtient via GET Play/{id}/questions/{order}/image.</summary>
        public bool HasImage { get; set; }
        /// <summary>Si vrai, le cri s'obtient via GET Play/{id}/questions/{order}/sound.</summary>
        public bool HasSound { get; set; }
    }

    public class AnswerChoiceDto
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public string? Label { get; set; }
    }

    public class AnswerResultDto
    {
        public bool IsCorrect { get; set; }
        public bool AlreadyAnswered { get; set; }
        public List<long> CorrectAnswerIds { get; set; } = new();
        public List<long> SelectedAnswerIds { get; set; } = new();
        public bool QuizzDone { get; set; }
        public int? NextOrder { get; set; }
    }

    public class QuestionDto
    {
        public Guid QuizzId { get; set; }
        public int Order { get; set; }
        public int Total { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Label { get; set; }
        public string? DifficultyCode { get; set; }
        public bool IsMultipleAnswers { get; set; }
        public bool IsBlurred { get; set; }
        public bool IsGrayscale { get; set; }
        public bool IsHide { get; set; }
        public QuestionSubjectDto Subject { get; set; } = new();
        public List<AnswerChoiceDto> Answers { get; set; } = new();
        public bool Done { get; set; }
        /// <summary>Correction, présente uniquement si la question a déjà reçu une réponse.</summary>
        public AnswerResultDto? Result { get; set; }
    }

    public class QuestionResultDto
    {
        public int Order { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Label { get; set; }
        public string? SubjectName { get; set; }
        public bool Done { get; set; }
        public bool IsCorrect { get; set; }
        public List<string?> CorrectAnswers { get; set; } = new();
        public List<string?> SelectedAnswers { get; set; } = new();
    }

    public class QuizzResultDto
    {
        public QuizzSummaryDto Summary { get; set; } = new();
        public List<QuestionResultDto> Questions { get; set; } = new();
    }
}
