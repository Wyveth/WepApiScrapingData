using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class QuizzDifficultyDto : IdentityDto
    {
        public long Quizz { get; set; }
        
        public bool Easy { get; set; }

        public bool Normal { get; set; }

        public bool Hard { get; set; }

        public string? ResumeQuestion { get; set; }
    }
}
