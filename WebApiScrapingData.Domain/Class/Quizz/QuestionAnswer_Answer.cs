using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class QuestionAnswer_Answer : Identity
    {
        public long QuestionAnswerId { get; set; }
        [ForeignKey("QuestionAnswerId")]
        [DataMember]
        public virtual QuestionAnswer? QuestionAnswer { get; set; }

        public long AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        [DataMember]
        public virtual Answer? Answer { get; set; }
    }
}
