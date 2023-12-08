using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class QuizzDifficulty : Identity
    {
        [DataMember(Name = DataMember.Quizz)]
        public Quizz? Quizz { get; set; }

        [DataMember(Name = DataMember.Easy)]
        public bool Easy { get; set; }

        [DataMember(Name = DataMember.Normal)]
        public bool Normal { get; set; }

        [DataMember(Name = DataMember.Hard)]
        public bool Hard { get; set; }

        [DataMember(Name = DataMember.ResumeQuestion)]
        public string? ResumeQuestion { get; set; }
    }
}
