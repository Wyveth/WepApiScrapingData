using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Class;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsAnswer<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Pokemon> pokemonsAnswer);
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<TypePok> typesAnswer);
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer, bool Reverse);
        Task<string> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer);
        Task<string> GenerateCorrectAnswersDesc(QuestionType questionType, List<Pokemon> pokemonsAnswer);
        Task<string> GenerateCorrectAnswersStat(QuestionType questionType, List<Pokemon> pokemonsAnswer, string typeStat);
        Task<List<Answer>> GenerateAnswers(Quizz quizz, QuestionType questionType, List<Answer> answers);
        Task<string> ConvertDescription(Pokemon pokemon);
    }
}
