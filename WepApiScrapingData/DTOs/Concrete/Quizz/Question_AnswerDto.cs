using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class Question_AnswerDto : IdentityDto
    {
        public long QuestionId { get; set; }

        public long AnswerId { get; set; }
    }
}
