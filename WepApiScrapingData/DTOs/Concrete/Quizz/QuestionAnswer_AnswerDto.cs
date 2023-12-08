using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class QuestionAnswer_AnswerDto : IdentityDto
    {
        public long QuestionAnswerId { get; set; }

        public long AnswerId { get; set; }
    }
}
