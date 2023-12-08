using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class QuestionTypeDto : IdentityDto
    {
        public string? Code { get; set; }

        public string? Libelle { get; set; }

        public int NbAnswers { get; set; }

        public bool IsMultipleAnswers { get; set; }

        public int NbAnswersPossible { get; set; }

        public bool IsBlurred { get; set; }

        public bool IsGrayscale { get; set; }

        public bool IsHide { get; set; }

        public bool IsSameType { get; set; }

        public long Difficulty { get; set; }
    }
}
