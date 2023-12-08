using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class QuestionDto: IdentityDto
    {
        public int Order { get; set; }

        public int DataObjectID { get; set; }

        public long? QuestionType { get; set; }

        public bool Done { get; set; }
    }
}
