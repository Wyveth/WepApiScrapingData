using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Resources;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete.Quizz
{
    public class QuizzDto : IdentityDto
    {
        public bool Gen1 { get; set; }
        
        public bool Gen2 { get; set; }

        public bool Gen3 { get; set; }

        public bool Gen4 { get; set; }

        public bool Gen5 { get; set; }

        public bool Gen6 { get; set; }
        
        public bool Gen7 { get; set; }

        public bool Gen8 { get; set; }

        public bool GenArceus { get; set; }

        public bool Easy { get; set; }

        public bool Normal { get; set; }

        public bool Hard { get; set; }

        public bool Done { get; set; }

        public long IdentityUser { get; set; }
    }
}
