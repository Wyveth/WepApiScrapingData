using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class Answer : Identity
    {
        //Numéro d'ordre Des Réponses
        [DataMember(Name = DataMember.Order)]
        public int Order { get; set; }

        //Libellé des réponses
        [DataMember(Name = DataMember.Libelle)]
        public string? Libelle { get; set; }
        
        //Type des réponses
        [DataMember(Name = DataMember.Type)]
        public string? Type { get; set; }

        //Si la réponse est sélectionnée
        [DataMember(Name = DataMember.IsSelected)]
        public bool IsSelected { get; set; }

        //ID Correct
        public long IsCorrectID { get; set; }

        //Si réponse correcte
        [DataMember(Name = DataMember.IsCorrect)]
        public bool IsCorrect { get; set; }
    }
}
