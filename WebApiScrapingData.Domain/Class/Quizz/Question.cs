using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    public class Question : Identity
    {
        //Numéro d'ordre du Quizz
        public int Order { get; set; }

        //Data Object ID
        public int DataObjectID { get; set; }

        //Réponses
        public List<Answer>? Answers { get; set; }

        //Type Question
        public QuestionType? QuestionType { get; set; }

        //Savoir si la question est terminé
        public bool Done { get; set; }
    }
}
