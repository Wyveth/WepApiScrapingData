using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class QuestionType : Identity
    {
        //Code des Type de question (Type/Pokémon)
        [DataMember(Name = DataMember.Code)]
        public string? Code { get; set; }

        //Libellé de la Question
        [DataMember(Name = DataMember.Libelle)]
        public string? Libelle { get; set; }

        //Nombre de Réponse Disponible
        [DataMember(Name = DataMember.NbAnswers)]
        public int NbAnswers { get; set; }

        //Plusieurs réponse possible
        [DataMember(Name = DataMember.IsMultipleAnswers)]
        public bool IsMultipleAnswers { get; set; }

        //Nombre de bonne réponse
        [DataMember(Name = DataMember.NbAnswersPossible)]
        public int NbAnswersPossible { get; set; }

        //Si Image Floue
        [DataMember(Name = DataMember.IsBlurred)]
        public bool IsBlurred { get; set; }

        //Si Image Noir et Blanc
        [DataMember(Name = DataMember.IsGrayscale)]
        public bool IsGrayscale { get; set; }

        //Si Image Noir
        [DataMember(Name = DataMember.IsHide)]
        public bool IsHide { get; set; }

        //Si Réponse de Même Type
        [DataMember(Name = DataMember.IsSameType)]
        public bool IsSameType { get; set; }

        //Difficulté
        [DataMember(Name = DataMember.Difficulty)]
        public Difficulty? Difficulty { get; set; }
    }
}
