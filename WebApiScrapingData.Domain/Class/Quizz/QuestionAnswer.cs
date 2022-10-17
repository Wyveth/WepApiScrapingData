using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    public class QuestionAnswer : Identity
    {
        public Quizz? Quizz { get; set; }
        public Question? Question { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}
