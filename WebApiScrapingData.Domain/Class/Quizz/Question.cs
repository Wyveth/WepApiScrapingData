using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class Question : Identity
    {
        //Numéro d'ordre du Quizz
        [DataMember(Name = DataMember.Order)]
        public int Order { get; set; }

        //Data Object ID
        [DataMember(Name = DataMember.DataObjectID)]
        public int DataObjectID { get; set; }

        //Réponses
        [DataMember(Name = DataMember.Answers)]
        public List<Question_Answer>? Question_Answers { get; set; }

        //Type Question
        [DataMember(Name = DataMember.QuestionType)]
        public QuestionType? QuestionType { get; set; }

        //Savoir si la question est terminé
        [DataMember(Name = DataMember.Done)]
        public bool Done { get; set; }

        public Question()
        {
            Question_Answers = new List<Question_Answer>();
        }
    }
}
