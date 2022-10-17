using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    public class QuizzDifficulty : Identity
    {
        public Quizz? Quizz { get; set; }
        
        public bool ImgEasy { get; set; }

        public bool ImgNormal { get; set; }

        public bool ImgHard { get; set; }

        public string? ResumeQuestion { get; set; }
    }
}
