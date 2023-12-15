using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class Quizz : Identity
    {
        //Identifiant des questions
        [DataMember(Name = DataMember.Questions)]
        public List<Quizz_Question> Quizz_Questions { get; set; }

        //Si Filtre Gen 1 Active
        [DataMember(Name = DataMember.Gen1)]
        public bool Gen1 { get; set; }

        //Si Filtre Gen 2 Active
        [DataMember(Name = DataMember.Gen2)]
        public bool Gen2 { get; set; }

        //Si Filtre Gen 3 Active
        [DataMember(Name = DataMember.Gen3)]
        public bool Gen3 { get; set; }

        //Si Filtre Gen 4 Active
        [DataMember(Name = DataMember.Gen4)]
        public bool Gen4 { get; set; }

        //Si Filtre Gen 5 Active
        [DataMember(Name = DataMember.Gen5)]
        public bool Gen5 { get; set; }

        //Si Filtre Gen 6
        [DataMember(Name = DataMember.Gen6)]
        public bool Gen6 { get; set; }

        //Si Filtre Gen 7 Active
        [DataMember(Name = DataMember.Gen7)]
        public bool Gen7 { get; set; }

        //Si Filtre Gen 8 Active
        [DataMember(Name = DataMember.Gen8)]
        public bool Gen8 { get; set; }

        //Si Filtre Gen 9 Active
        [DataMember(Name = DataMember.Gen9)]
        public bool Gen9 { get; set; }

        //Si Filtre Gen Arceus Active
        [DataMember(Name = DataMember.GenArceus)]
        public bool GenArceus { get; set; }

        //Si Question Facile
        [DataMember(Name = DataMember.Easy)]
        public bool Easy { get; set; }

        //Si Question Normal
        [DataMember(Name = DataMember.Normal)]
        public bool Normal { get; set; }

        //Si Question Hard
        [DataMember(Name = DataMember.Hard)]
        public bool Hard { get; set; }

        //Savoir si le questionnaire est terminé
        [DataMember(Name = DataMember.Done)]
        public bool Done { get; set; }

        //Identifiant du Profil
        public IdentityUser? IdentityUser { get; set; }

        public Quizz()
        {
            Quizz_Questions = new List<Quizz_Question>();
        }
    }
}
