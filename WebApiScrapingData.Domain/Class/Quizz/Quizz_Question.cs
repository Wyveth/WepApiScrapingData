using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class Quizz_Question: Identity
    {
        public long QuizzId { get; set; }
        [ForeignKey("QuizzId")]
        [DataMember]
        public virtual Quizz? Quizz { get; set; }

        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        [DataMember]
        public virtual Question? Question { get; set; }
    }
}
