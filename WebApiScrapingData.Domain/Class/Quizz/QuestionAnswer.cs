using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class QuestionAnswer : Identity
    {
        [DataMember(Name = DataMember.Quizz)]
        public Quizz? Quizz { get; set; }

        [DataMember(Name = DataMember.Question)]
        public Question? Question { get; set; }

        [DataMember(Name = DataMember.Answers)]
        public List<QuestionAnswer_Answer> QuestionAnswer_Answers { get; set; }

        public QuestionAnswer()
        {
            QuestionAnswer_Answers = new List<QuestionAnswer_Answer>();
        }
    }
}
