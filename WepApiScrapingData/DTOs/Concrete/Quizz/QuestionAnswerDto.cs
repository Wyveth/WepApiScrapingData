using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class QuestionAnswerDto : IdentityDto
    {
        public long Quizz { get; set; }
        
        public long Question { get; set; }
    }
}
