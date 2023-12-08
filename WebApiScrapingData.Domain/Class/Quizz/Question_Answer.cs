using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class Question_Answer : Identity
    {
        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        [DataMember]
        public virtual Question? Question { get; set; }

        public long AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        [DataMember]
        public virtual Answer? Answer { get; set; }
    }
}
