using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Utils;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionRepository : Repository<Question>, IRepositoryExtendsQuestion<Question>
    {
        #region fields
        private readonly PokemonRepository _repositoryP;
        private readonly TypePokRepository _repositoryTP;
        private readonly TalentRepository _repositoryT;
        private readonly Pokemon_WeaknessRepository _repositoryPW;
        private readonly Pokemon_TalentRepository _repositoryPT;
        private readonly QuestionTypeRepository _repositoryQT;
        private readonly AnswerRepository _repositoryA;
        #endregion

        #region Constructor
        public QuestionRepository(ScrapingContext context, PokemonRepository repositoryP, TypePokRepository repositoryTP, TalentRepository repositoryT, Pokemon_WeaknessRepository repositoryPW, Pokemon_TalentRepository repositoryPT, QuestionTypeRepository repositoryQT, AnswerRepository repositoryA) : base(context) {
            _repositoryP = repositoryP;
            _repositoryTP = repositoryTP;
            _repositoryT = repositoryT;
            _repositoryPW = repositoryPW;
            _repositoryPT = repositoryPT;
            _repositoryQT = repositoryQT;
            _repositoryA = repositoryA;
        }
        #endregion

        #region Public Methods
        public async Task<List<Quizz_Question>> GenerateQuestions(bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, bool easy, bool normal, bool hard)
        {
            int nbQuestionMax = await GetNbQuestionByDifficulty(easy, normal, hard);

            List<Question_Answer> AnswersID;
            long DataObjectID = 0;
            List<Question> questions = new List<Question>();
            List<Pokemon> alreadyExistQTypPok = new List<Pokemon>();
            List<Pokemon> alreadyExistQTypTypPok = new List<Pokemon>();
            List<TypePok> alreadyExistQTypTyp = new List<TypePok>();
            List<Talent> alreadyExistQTypTalent = new List<Talent>();
            List<Pokemon> alreadyExistQTypPokStat = new List<Pokemon>();

            for (int nbQuestion = 0; nbQuestion < nbQuestionMax; nbQuestion++)
            {
                await Task.Run(async () =>
                {
                    QuestionType questionType = await _repositoryQT.GetQuestionTypeRandom(easy, normal, hard);

                    if (questionType.Code.Equals(Constantes.QTypPok_Code)
                    || questionType.Code.Equals(Constantes.QTypPokDescReverse_Code))
                    {
                        AnswersID = await GetAnswersID_QTypPok(questionType, gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypPok);
                        int answerID = int.Parse(AnswersID);
                        Answer answer = await _repositoryA.Get(answerID);
                        alreadyExistQTypPok.Add(await _repositoryP.Get(answer.IsCorrectID));
                        DataObjectID = answer.IsCorrectID;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypPokFamilyVarious_Code))
                    {
                        Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypTypPok);
                        AnswersID = await GetAnswersID_QTypPokFamily(questionType, pokemon);
                        alreadyExistQTypTypPok.Add(pokemon);
                        DataObjectID = pokemon.Id;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypPokTypVarious_Code))
                    {
                        TypePok typePok = await _repositoryTP.GetTypeRandom();
                        AnswersID = await GetAnswersID_QTypPok(questionType, gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, typePok, alreadyExistQTypTyp);
                        alreadyExistQTypTyp.Add(typePok);
                        DataObjectID = typePok.Id;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypTypPok_Code))
                    {
                        Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypTypPok);
                        AnswersID = await GetAnswersID_QTypTypPok(questionType, pokemon, false);
                        alreadyExistQTypTypPok.Add(pokemon);
                        DataObjectID = pokemon.Id;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypTypPokVarious_Code))
                    {
                        Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypTypPok);
                        AnswersID = await GetAnswersID_QTypTypPok(questionType, pokemon, true);
                        alreadyExistQTypTypPok.Add(pokemon);
                        DataObjectID = pokemon.Id;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypWeakPokVarious_Code))
                    {
                        Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypTypPok);
                        AnswersID = await GetAnswersID_QTypWeakPok(questionType, pokemon, true);
                        alreadyExistQTypTypPok.Add(pokemon);
                        DataObjectID = pokemon.Id;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypTyp_Code))
                    {
                        AnswersID = await GetAnswersID_QTypTyp(questionType, alreadyExistQTypTyp);
                        int answerID = int.Parse(AnswersID);
                        Answer answer = await _repositoryA.Get(answerID);
                        alreadyExistQTypTyp.Add(await _repositoryTP.Get(answer.IsCorrectID));
                        DataObjectID = answer.IsCorrectID;

                        if (alreadyExistQTypTyp.Count.Equals(18))
                            alreadyExistQTypTyp = new List<TypePok>();
                    }
                    else if (questionType.Code.Equals(Constantes.QTypPokDesc_Code))
                    {
                        AnswersID = await GetAnswersID_QTypPokDesc(questionType, gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypPok);
                        int answerID = int.Parse(AnswersID);
                        Answer answer = await _repositoryA.Get(answerID);
                        alreadyExistQTypPok.Add(await _repositoryP.Get(answer.IsCorrectID));
                        DataObjectID = answer.IsCorrectID;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypPokTalentVarious_Code))
                    {
                        Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypTypPok);
                        AnswersID = await GetAnswersID_QTypTalentPok(questionType, pokemon);
                        alreadyExistQTypTypPok.Add(pokemon);
                        DataObjectID = pokemon.Id;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypTalent_Code) || questionType.Code.Equals(Constantes.QTypTalentReverse_Code))
                    {
                        AnswersID = await GetAnswersID_QTypTalent(questionType, alreadyExistQTypTalent);
                        int answerID = int.Parse(AnswersID);
                        Answer answer = await _repositoryA.Get(answerID);
                        alreadyExistQTypTalent.Add(await _repositoryT.Get(answer.IsCorrectID));
                        DataObjectID = answer.IsCorrectID;
                    }
                    else if (questionType.Code.Equals(Constantes.QTypPokStat_Code))
                    {
                        AnswersID = await GetAnswersID_QTypPokStat(questionType, gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExistQTypPokStat);
                        int answerID = int.Parse(AnswersID);
                        Answer answer = await _repositoryA.Get(answerID);
                        alreadyExistQTypPokStat.Add(await _repositoryP.Get(answer.IsCorrectID));
                        DataObjectID = answer.IsCorrectID;
                    }

                    Question question = new Question()
                    {
                        Order = nbQuestion + 1,
                        Question_Answers = AnswersID,
                        DataObjectID = DataObjectID,
                        QuestionType = questionType,
                        Done = false
                    };

                    await Add(question);
                    questions.Add(question);
                    Debug.Write("Creation Question:" + questions.Count + "/" + nbQuestionMax);
                });
            }
            return await Task.FromResult(await GetQuestionsID(questions));
        }

        public async Task<int> GetNbQuestionByDifficulty(bool easy, bool normal, bool hard)
        {
            int nbQuestionMax = 0;
            if (easy)
                nbQuestionMax = 10;
            else if (normal)
                nbQuestionMax = 15;
            else if (hard)
                nbQuestionMax = 20;

            return await Task.FromResult(nbQuestionMax);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private async Task<string> GetQuestionsID(List<Question> questions)
        {
            StringBuilder result = new StringBuilder();
            int i = 0;
            foreach (int id in questions.Select(m => m.Id))
            {
                if (i == 0)
                {
                    result.Append(id.ToString());
                    i++;
                }
                else
                {
                    result.Append(',' + id.ToString());
                }
            }

            return await Task.FromResult(result.ToString());
        }

        private async Task<List<Question_Answer>> GetAnswersID_QTypPok(QuestionType questionType, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, List<Pokemon> alreadyExist)
        {
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                await Task.Run(async () =>
                {
                    Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExist);
                    Pokemon pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    while (pokemonExist != null)
                    {
                        pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExist);
                        pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    }
                    pokemonsAnswer.Add(pokemon);
                });
            }

            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, pokemonsAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypPokFamily(QuestionType questionType, Pokemon pokemon)
        {
            string AnswersID = string.Empty;
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();
            
            if (pokemon.Evolutions != null)
            {
                foreach (var item in pokemon.Evolutions.Split(','))
                {
                    if (!item.Equals(pokemon.Id.ToString()))
                        pokemonsAnswer.Add(await _repositoryP.Get(int.Parse(item)));
                }
            }

            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, pokemonsAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypPok(QuestionType questionType, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, TypePok typePok, List<TypePok> alreadyExistQTypTyp)
        {
            string AnswersID = string.Empty;
            Random random = new Random();
            int nbAnswerCorrectRandom = random.Next(questionType.NbAnswers);
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int i = 0; i < nbAnswerCorrectRandom; i++)
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, typePok, pokemonsAnswer);
                if (pokemonsAnswer.Find(m => m.EN.Name.Equals(pokemon.EN.Name)) == null)
                    pokemonsAnswer.Add(pokemon);
                else
                    break;
            }

            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, pokemonsAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypTypPok(QuestionType questionType, Pokemon pokemon, bool various)
        {
            string AnswersID = string.Empty;

            List<TypePok> typesAnswer = new List<TypePok>();
            List<Pokemon_TypePok> pokemonTypePoks = await _repositoryTP.GetTypesPokByPokemon(pokemon.Id);

            if (!various)
                typesAnswer.Add(await _repositoryTP.Get(pokemonTypePoks[0].TypePokId));
            else
                foreach (Pokemon_TypePok item in pokemonTypePoks)
                    typesAnswer.Add(await _repositoryTP.Get(item.TypePokId));

            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, typesAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypWeakPok(QuestionType questionType, Pokemon pokemon, bool various)
        {
            string AnswersID = string.Empty;

            List<TypePok> weaknessAnswer = new List<TypePok>();
            List<Pokemon_Weakness> pokemonWeaknesses = await _pokemonWeaknessService.GetWeaknessesByPokemon(pokemon.Id);

            if (!various)
                weaknessAnswer.Add(await _repositoryTP.Get(pokemonWeaknesses[0].TypePokId));
            else
                foreach (Pokemon_Weakness item in pokemonWeaknesses)
                    weaknessAnswer.Add(await _repositoryTP.Get(item.TypePokId));

            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, weaknessAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypTyp(QuestionType questionType, List<TypePok> alreadySelected)
        {
            string AnswersID = string.Empty;

            List<Task> tasks = new List<Task>();
            List<TypePok> typesAnswer = new List<TypePok>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                tasks.Add(
                    Task.Run(async () =>
                    {
                        typesAnswer.Add(await _repositoryTP.GetTypeRandom(alreadySelected));
                    })
                );
            }

            Task.WaitAll(tasks.ToArray());
            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, typesAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypPokDesc(QuestionType questionType, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, List<Pokemon> alreadyExist)
        {
            string AnswersID = string.Empty;

            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                await Task.Run(async () =>
                {
                    Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExist);
                    Pokemon pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    while (pokemonExist != null)
                    {
                        pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExist);
                        pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    }
                    pokemonsAnswer.Add(pokemon);
                });
            }

            AnswersID = await _repositoryA.GenerateCorrectAnswersDesc(questionType, pokemonsAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypTalentPok(QuestionType questionType, Pokemon pokemon)
        {
            string AnswersID = string.Empty;

            List<Talent> talentsAnswer = new List<Talent>();
            List<Pokemon_Talent> pokemonTalentServices = await _repositoryPT.GetTalentsByPokemon(pokemon.Id);

            if (pokemonTalentServices != null)
                foreach (Pokemon_Talent item in pokemonTalentServices)
                    talentsAnswer.Add(await _repositoryT.Get(item.TalentId));

            AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, talentsAnswer);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypTalent(QuestionType questionType, List<Talent> alreadySelected)
        {
            string AnswersID = string.Empty;

            List<Task> tasks = new List<Task>();
            List<Talent> talentsAnswer = new List<Talent>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                tasks.Add(
                    Task.Run(async () =>
                    {
                        talentsAnswer.Add(await _repositoryT.GetTalentRandom(alreadySelected));
                    })
                );
            }

            Task.WaitAll(tasks.ToArray());
            if (questionType.Code.Equals(Constantes.QTypTalent_Code))
                AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, talentsAnswer, false);
            else if (questionType.Code.Equals(Constantes.QTypTalentReverse_Code))
                AnswersID = await _repositoryA.GenerateCorrectAnswers(questionType, talentsAnswer, true);

            return await Task.FromResult(AnswersID);
        }

        private async Task<string> GetAnswersID_QTypPokStat(QuestionType questionType, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus, List<Pokemon> alreadyExist)
        {
            string AnswersID = string.Empty;

            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                await Task.Run(async () =>
                {
                    Pokemon pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExist);
                    Pokemon pokemonExist = alreadyExist.Find(m => m.Id.Equals(pokemon.Id));
                    while (pokemonExist != null)
                    {
                        pokemon = await _repositoryP.GetPokemonRandom(gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus, alreadyExist);
                        pokemonExist = alreadyExist.Find(m => m.Id.Equals(pokemon.Id));
                    }
                    pokemonsAnswer.Add(pokemon);
                });
            }

            string typeStat = GetRandomTypeStat();
            AnswersID = await _repositoryA.GenerateCorrectAnswersStat(questionType, pokemonsAnswer, typeStat);

            return await Task.FromResult(AnswersID);
        }

        private string GetRandomTypeStat()
        {
            Random random = new Random();
            int numberRandom = random.Next(6);

            string typeStat = "";

            switch (numberRandom)
            {
                case 0: typeStat = Constantes.Pv; break;
                case 1: typeStat = Constantes.Attaque; break;
                case 2: typeStat = Constantes.Defense; break;
                case 3: typeStat = Constantes.AttaqueSpe; break;
                case 4: typeStat = Constantes.DefenseSpe; break;
                case 5: typeStat = Constantes.Vitesse; break;
            }

            return typeStat;
        }
        #endregion
    }
}
