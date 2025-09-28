using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Utils;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionTypeRepository : Repository<QuestionType>, IRepositoryExtendsQuestionType<QuestionType>
    {
        #region fields
        private readonly DifficultyRepository _repositoryD;
        #endregion
        
        #region Constructor
        public QuestionTypeRepository(ScrapingContext context, DifficultyRepository repositoryD) : base(context) {
            _repositoryD = repositoryD;
        }
        #endregion

        #region Public Methods
        public async Task<QuestionType> GetQuestionTypeRandom(bool easy, bool normal, bool hard)
        {
            List<QuestionType> result = GetAll().Result.ToList();
            List<QuestionType> resultFilterDifficulty = GetQuestionTypesWithFilterDifficulty(result, easy, normal, hard).Result;

            return await Task.FromResult(await GetQuestionTypeRandomBySelectedDifficulty(resultFilterDifficulty, easy, normal, hard));
        }

        public async override Task<IEnumerable<QuestionType>> GetAll()
        {
            return await _context.QuestionTypes.Include(qt => qt.Difficulty).ToListAsync();
        }
        #endregion

        #region Private Methods
        private async Task<List<QuestionType>> GetQuestionTypesWithFilterDifficulty(List<QuestionType> result, bool easy, bool normal, bool hard)
        {
            List<QuestionType> resultFilterDifficulty = new List<QuestionType>();
            Difficulty difficulty = new Difficulty();

            if (easy)
            {
                difficulty = await _repositoryD.Single(m => m.Code.Equals(Constantes.Easy_Code));
                resultFilterDifficulty.AddRange(result.FindAll(m => m.Difficulty.Id.Equals(difficulty.Id)));
            }

            if (normal)
            {
                difficulty = await _repositoryD.Single(m => m.Code.Equals(Constantes.Normal_Code));
                resultFilterDifficulty.AddRange(result.FindAll(m => m.Difficulty.Id.Equals(difficulty.Id)));
            }

            if (hard)
            {
                difficulty = await _repositoryD.Single(m => m.Code.Equals(Constantes.Hard_Code));
                resultFilterDifficulty.AddRange(result.FindAll(m => m.Difficulty.Id.Equals(difficulty.Id)));
            }

            if (resultFilterDifficulty.Count.Equals(0))
                resultFilterDifficulty = result;

            return await Task.FromResult(resultFilterDifficulty);
        }

        private async Task<QuestionType> GetQuestionTypeRandomBySelectedDifficulty(List<QuestionType> resultFilterDifficulty, bool easy, bool normal, bool hard)
        {
            QuestionType questionTypeSelected = new QuestionType();
            List<string> DifficultSelected = new List<string>();

            Random random = new Random();

            if (easy)
                DifficultSelected.Add(Constantes.Easy_Code);
            if (normal)
                DifficultSelected.Add(Constantes.Normal_Code);
            if (hard)
                DifficultSelected.Add(Constantes.Hard_Code);

            if (DifficultSelected.Count.Equals(1))
            {
                if (easy)
                    questionTypeSelected = await GetQuestionTypeRandomByEasyDifficulty(resultFilterDifficulty);
                else if (normal)
                    questionTypeSelected = await GetQuestionTypeRandomByNormalDifficulty(resultFilterDifficulty);
                else if (hard)
                    questionTypeSelected = await GetQuestionTypeRandomByHardDifficulty(resultFilterDifficulty);
            }
            else
            {
                int difficultyRandom = random.Next(DifficultSelected.Count);
                if (DifficultSelected[difficultyRandom].Equals(Constantes.Easy_Code))
                    questionTypeSelected = await GetQuestionTypeRandomByEasyDifficulty(resultFilterDifficulty);
                else if (DifficultSelected[difficultyRandom].Equals(Constantes.Normal_Code))
                    questionTypeSelected = await GetQuestionTypeRandomByNormalDifficulty(resultFilterDifficulty);
                else if (DifficultSelected[difficultyRandom].Equals(Constantes.Hard_Code))
                    questionTypeSelected = await GetQuestionTypeRandomByHardDifficulty(resultFilterDifficulty);
            }

            return await Task.FromResult(questionTypeSelected);
        }

        private async Task<QuestionType> GetQuestionTypeRandomByEasyDifficulty(List<QuestionType> resultFilterDifficulty)
        {
            List<QuestionType> questionTypes;
            Random random = new Random();
            int numberRandom = random.Next(100);

            if (numberRandom <= 3)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTyp_Code));
            else if (numberRandom <= 6)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTalent_Code) || m.Code.Equals(Constantes.QTypTalentReverse_Code));
            else if (numberRandom <= 9)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokDesc_Code) || m.Code.Equals(Constantes.QTypPokDescReverse_Code));
            else if (numberRandom <= 12)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokFamilyVarious_Code));
            else if (numberRandom <= 15)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTypPok_Code));
            else
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPok_Code));

            int numberTypeQuestion = random.Next(questionTypes.Count);
            return await Task.FromResult(questionTypes[numberTypeQuestion]);
        }

        private async Task<QuestionType> GetQuestionTypeRandomByNormalDifficulty(List<QuestionType> resultFilterDifficulty)
        {
            List<QuestionType> questionTypes;
            Random random = new Random();
            int numberRandom = random.Next(100);

            if (numberRandom <= 3)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTyp_Code));
            else if (numberRandom <= 6)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTalent_Code) || m.Code.Equals(Constantes.QTypTalentReverse_Code) || m.Code.Equals(Constantes.QTypPokTalentVarious_Code));
            else if (numberRandom <= 9)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokDesc_Code) || m.Code.Equals(Constantes.QTypPokDescReverse_Code));
            else if (numberRandom <= 12)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokFamilyVarious_Code) || m.Code.Equals(Constantes.QTypPokTypVarious_Code));
            else if (numberRandom <= 15)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTypPok_Code) || m.Code.Equals(Constantes.QTypTypPokVarious_Code));
            else
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPok_Code));

            int numberTypeQuestion = random.Next(questionTypes.Count);
            return await Task.FromResult(questionTypes[numberTypeQuestion]);
        }

        private async Task<QuestionType> GetQuestionTypeRandomByHardDifficulty(List<QuestionType> resultFilterDifficulty)
        {
            List<QuestionType> questionTypes;
            Random random = new Random();
            int numberRandom = random.Next(100);

            if (numberRandom <= 3)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokStat_Code));
            else if (numberRandom <= 6)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTyp_Code));
            else if (numberRandom <= 9)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTalent_Code) || m.Code.Equals(Constantes.QTypTalentReverse_Code) || m.Code.Equals(Constantes.QTypPokTalentVarious_Code));
            else if (numberRandom <= 12)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokDesc_Code) || m.Code.Equals(Constantes.QTypPokDescReverse_Code));
            else if (numberRandom <= 15)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPokFamilyVarious_Code) || m.Code.Equals(Constantes.QTypPokTypVarious_Code));
            else if (numberRandom <= 20)
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypTypPok_Code) || m.Code.Equals(Constantes.QTypTypPokVarious_Code) || m.Code.Equals(Constantes.QTypWeakPokVarious_Code));
            else
                questionTypes = resultFilterDifficulty.FindAll(m => m.Code.Equals(Constantes.QTypPok_Code));

            int numberTypeQuestion = random.Next(questionTypes.Count);
            return await Task.FromResult(questionTypes[numberTypeQuestion]);
        }
        #endregion
    }
}
