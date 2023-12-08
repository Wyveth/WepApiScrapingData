using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class Quizz_QuestionDto : IdentityDto
    {
        public long QuizzId { get; set; }

        public long QuestionId { get; set; }
    }
}
