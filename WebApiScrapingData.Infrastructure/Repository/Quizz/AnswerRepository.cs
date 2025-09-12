using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Utils;
using WebApiScrapingData.Infrastructure.Repository.Class;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class AnswerRepository : Repository<Answer>, IRepositoryExtendsAnswer<Answer>
    {
        #region fields
        private readonly PokemonRepository _repositoryP;
        private readonly TypePokRepository _repositoryTP;
        private readonly TalentRepository _repositoryT;
        #endregion

        #region Constructor
        public AnswerRepository(ScrapingContext context, PokemonRepository repositoryP, TypePokRepository repositoryTP, TalentRepository repositoryT) : base(context)
        {
            _repositoryP = repositoryP;
            _repositoryTP = repositoryTP;
            _repositoryT = repositoryT;
        }
        #endregion

        #region Public Methods
        #region Quizz
        public async Task<List<Answer>> GenerateAnswers(ClassQuizz.Quizz quizz, Question question, QuestionType questionType)
        {
            List<Answer> answers = new();
            List<Pokemon> alreadyExistQTypPok = new();
            List<Pokemon> alreadyExistQTypTypPok = new();
            List<TypePok> alreadyExistQTypTyp = new();
            List<Talent> alreadyExistQTypTalent = new();
            List<Pokemon> alreadyExistQTypPokStat = new();

            if (questionType.Code.Equals(Constantes.QTypPok_Code)
             || questionType.Code.Equals(Constantes.QTypPokDescReverse_Code))
            {
                answers = await GetAnswersID_QTypPok(quizz, questionType, alreadyExistQTypPok);
                
                foreach (Answer answer in answers)
                    alreadyExistQTypPok.Add(await _repositoryP.Get(answer.IsCorrectID));
                
                //question.DataObjectID = answer.IsCorrectID;
            }
            else if (questionType.Code.Equals(Constantes.QTypPokFamilyVarious_Code))
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExistQTypTypPok);
                answers = await GetAnswersID_QTypPokFamily(questionType, pokemon);
                alreadyExistQTypTypPok.Add(pokemon);
                question.DataObjectID = pokemon.Id;
            }
            else if (questionType.Code.Equals(Constantes.QTypPokTypVarious_Code))
            {
                TypePok typePok = await _repositoryTP.GetTypeRandom();
                answers = await GetAnswersID_QTypPok(quizz, questionType, typePok, alreadyExistQTypTyp);
                alreadyExistQTypTyp.Add(typePok);
                question.DataObjectID = typePok.Id;
            }
            else if (questionType.Code.Equals(Constantes.QTypTypPok_Code))
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExistQTypTypPok);
                answers = await GetAnswersID_QTypTypPok(questionType, pokemon, false);
                alreadyExistQTypTypPok.Add(pokemon);
                question.DataObjectID = pokemon.Id;
            }
            else if (questionType.Code.Equals(Constantes.QTypTypPokVarious_Code))
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExistQTypTypPok);
                answers = await GetAnswersID_QTypTypPok(questionType, pokemon, true);
                alreadyExistQTypTypPok.Add(pokemon);
                question.DataObjectID = pokemon.Id;
            }
            else if (questionType.Code.Equals(Constantes.QTypWeakPokVarious_Code))
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExistQTypTypPok);
                answers = await GetAnswersID_QTypWeakPok(questionType, pokemon, true);
                alreadyExistQTypTypPok.Add(pokemon);
                question.DataObjectID = pokemon.Id;
            }
            else if (questionType.Code.Equals(Constantes.QTypTyp_Code))
            {
                answers = await GetAnswersID_QTypTyp(questionType, alreadyExistQTypTyp);

                foreach (Answer answer in answers)
                    alreadyExistQTypTyp.Add(await _repositoryTP.Get(answer.IsCorrectID));
                
                //question.DataObjectID = answer.IsCorrectID;

                if (alreadyExistQTypTyp.Count.Equals(18))
                    alreadyExistQTypTyp = new List<TypePok>();
            }
            else if (questionType.Code.Equals(Constantes.QTypPokDesc_Code))
            {
                answers = await GetAnswersID_QTypPokDesc(quizz, questionType, alreadyExistQTypPok);

                foreach (Answer answer in answers)
                    alreadyExistQTypPok.Add(await _repositoryP.Get(answer.IsCorrectID));

                //question.DataObjectID = answer.IsCorrectID;
            }
            else if (questionType.Code.Equals(Constantes.QTypPokTalentVarious_Code))
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExistQTypTypPok);
                answers = await GetAnswersID_QTypTalentPok(questionType, pokemon);
                alreadyExistQTypTypPok.Add(pokemon);
                question.DataObjectID = pokemon.Id;
            }
            else if (questionType.Code.Equals(Constantes.QTypTalent_Code) || questionType.Code.Equals(Constantes.QTypTalentReverse_Code))
            {
                answers = await GetAnswersID_QTypTalent(questionType, alreadyExistQTypTalent);

                foreach (Answer answer in answers)
                    alreadyExistQTypTalent.Add(await _repositoryT.Get(answer.IsCorrectID));

                //question.DataObjectID = answer.IsCorrectID;
            }
            else if (questionType.Code.Equals(Constantes.QTypPokStat_Code))
            {
                answers = await GetAnswersID_QTypPokStat(quizz, questionType, alreadyExistQTypPokStat);

                foreach (Answer answer in answers)
                    alreadyExistQTypPokStat.Add(await _repositoryP.Get(answer.IsCorrectID));

                //question.DataObjectID = answer.IsCorrectID;
            }

            return await Task.FromResult(answers);
        }
        
        public async Task<List<Answer>> GenerateAnswers(ClassQuizz.Quizz quizz, QuestionType questionType, List<Answer> answers)
        {
            if (questionType.Code.Equals(Constantes.QTypPok_Code)
                || questionType.Code.Equals(Constantes.QTypPokDescReverse_Code)
                || questionType.Code.Equals(Constantes.QTypPokFamilyVarious_Code)
                || questionType.Code.Equals(Constantes.QTypPokTypVarious_Code))
            {
                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (Answer item in answers)
                {
                    pokemons.Add(await _repositoryP.Get(item.IsCorrectID));
                }

                int qMissing = questionType.NbAnswers - answers.Count;

                for (int i = 0; i < qMissing; i++)
                {
                    pokemons.Add(await _repositoryP.GetPokemonRandom(quizz, pokemons));
                }

                answers = await GenerateAnswers(questionType, pokemons, answers);
            }
            else if (questionType.Code.Equals(Constantes.QTypTypPok_Code)
                || questionType.Code.Equals(Constantes.QTypTyp_Code)
                || questionType.Code.Equals(Constantes.QTypTypPokVarious_Code)
                || questionType.Code.Equals(Constantes.QTypWeakPokVarious_Code))
            {
                Console.WriteLine("GenerateAnswers - QTypTypPok");

                List<TypePok> typePoks = new List<TypePok>();
                foreach (Answer item in answers)
                {
                    typePoks.Add(await _repositoryTP.Get(item.IsCorrectID));
                }

                int qMissing = questionType.NbAnswers - answers.Count;

                for (int i = 0; i < qMissing; i++)
                {
                    typePoks.Add(await _repositoryTP.GetTypeRandom(typePoks));
                }

                answers = await GenerateAnswers(questionType, typePoks, answers);
            }
            else if (questionType.Code.Equals(Constantes.QTypPokDesc_Code))
            {
                Console.WriteLine("GenerateAnswers - QTypPokDesc");

                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (Answer item in answers)
                {
                    pokemons.Add(await _repositoryP.Get(item.IsCorrectID));
                }

                int qMissing = questionType.NbAnswers - answers.Count;

                for (int i = 0; i < qMissing; i++)
                {
                    pokemons.Add(await _repositoryP.GetPokemonRandom(quizz, pokemons));
                }

                answers = await GenerateAnswersDesc(questionType, pokemons, answers);
            }
            else if (questionType.Code.Equals(Constantes.QTypTalent_Code)
                || questionType.Code.Equals(Constantes.QTypTalentReverse_Code)
                || questionType.Code.Equals(Constantes.QTypPokTalentVarious_Code))
            {
                Console.WriteLine("GenerateAnswers - QTypTalent");

                List<Talent> talents = new List<Talent>();
                foreach (Answer item in answers)
                {
                    talents.Add(await _repositoryT.Get(item.IsCorrectID));
                }

                int qMissing = questionType.NbAnswers - answers.Count;

                for (int i = 0; i < qMissing; i++)
                {
                    talents.Add(await _repositoryT.GetTalentRandom(talents));
                }

                if (questionType.Code.Equals(Constantes.QTypTalent_Code))
                    answers = await GenerateAnswers(questionType, talents, answers);
                else if (questionType.Code.Equals(Constantes.QTypTalentReverse_Code) || questionType.Code.Equals(Constantes.QTypPokTalentVarious_Code))
                    answers = await GenerateAnswers(questionType, talents, answers, true);
            }
            else if (questionType.Code.Equals(Constantes.QTypPokStat_Code))
            {
                Console.WriteLine("GenerateAnswers - QTypPokStat");

                Random random = new Random();
                List<Pokemon> pokemons = new List<Pokemon>();
                List<string> statAnswers = new List<string>();
                int stat = 0;
                string typeStat = "";

                foreach (Answer item in answers)
                {
                    typeStat = item.Type;
                    stat = int.Parse(item.Libelle);
                    statAnswers.Add(item.Libelle);
                    pokemons.Add(await _repositoryP.Get(item.IsCorrectID));
                }

                int minValue = 0;
                int maxValue = 255;

                if (stat - 50 > minValue)
                    minValue = stat - 50;

                if (stat + 50 < maxValue)
                    maxValue = stat + 50;

                int qMissing = questionType.NbAnswers - answers.Count;

                for (int i = 0; i < qMissing; i++)
                {
                    int x = 0;
                    while (x.Equals(0))
                    {
                        x = random.Next(minValue, maxValue);

                        if (statAnswers.Contains(x.ToString()))
                            x = 0;
                        else
                            statAnswers.Add(x.ToString());
                    }
                }

                answers = await GenerateAnswersStat(questionType, pokemons, answers, typeStat, statAnswers);
            }

            return await Task.FromResult(answers);
        }
        #endregion

        #region Quizz Pokemon
        public async Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<Pokemon> pokemonsAnswer)
        {
            List<Answer> answers = new List<Answer>();
            Random random = new Random();

            Dictionary<int, Pokemon> dic = new Dictionary<int, Pokemon>();
            foreach (var pokemon in pokemonsAnswer)
            {
                while (!dic.ContainsValue(pokemon))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, pokemon);
                }
            }

            foreach (KeyValuePair<int, Pokemon> pair in dic)
            {
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = true,
                    IsCorrectID = pair.Value.Id,
                    Libelle = pair.Value.FR.Name,
                    Order = pair.Key + 1
                };

                await AddAsync(answer);
                answers.Add(answer);
            }

            return await Task.FromResult(answers);
        }

        private async Task<List<Answer>> GenerateAnswers(QuestionType questionType, List<Pokemon> pokemons, List<Answer> pokemonsAnswer)
        {
            Random random = new Random();

            Dictionary<int, Pokemon> dic = new Dictionary<int, Pokemon>();

            foreach (var pokemon in pokemons)
            {
                while (!dic.ContainsValue(pokemon))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, pokemon);
                }
            }

            foreach (KeyValuePair<int, Pokemon> pair in dic)
            {
                Answer answerExist = pokemonsAnswer.Find(m => m.IsCorrectID.Equals(pair.Value.Id));
                if (answerExist == null)
                {
                    Answer answer = new Answer()
                    {
                        IsSelected = false,
                        IsCorrect = false,
                        IsCorrectID = -1,
                        Libelle = pair.Value.FR.Name,
                        Order = pair.Key + 1
                    };

                    pokemonsAnswer.Add(answer);
                }
                else
                {
                    answerExist.Order = pair.Key + 1;
                    await UpdateAsync(answerExist);
                }
            }

            return await Task.FromResult(pokemonsAnswer);
        }
        #endregion

        #region Quizz Type
        public async Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<TypePok> typesAnswer)
        {
            List<Answer> answers = new List<Answer>();
            Random random = new Random();

            Dictionary<int, TypePok> dic = new Dictionary<int, TypePok>();
            foreach (var type in typesAnswer)
            {
                while (!dic.ContainsValue(type))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, type);
                }
            }

            foreach (KeyValuePair<int, TypePok> pair in dic)
            {
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = true,
                    IsCorrectID = pair.Value.Id,
                    Libelle = pair.Value.Name_FR,
                    Order = pair.Key + 1
                };

                await AddAsync(answer);
                answers.Add(answer);
            }

            return await Task.FromResult(answers);
        }

        private async Task<List<Answer>> GenerateAnswers(QuestionType questionType, List<TypePok> typePoks, List<Answer> typesAnswer)
        {
            Random random = new Random();

            Dictionary<int, TypePok> dic = new Dictionary<int, TypePok>();

            foreach (var type in typePoks)
            {
                while (!dic.ContainsValue(type))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, type);
                }
            }

            foreach (KeyValuePair<int, TypePok> pair in dic)
            {
                Answer answerExist = typesAnswer.Find(m => m.IsCorrectID.Equals(pair.Value.Id));
                if (answerExist == null)
                {
                    Answer answer = new Answer()
                    {
                        IsSelected = false,
                        IsCorrect = false,
                        IsCorrectID = -1,
                        Libelle = pair.Value.Name_FR,
                        Order = pair.Key + 1
                    };

                    typesAnswer.Add(answer);
                }
                else
                {
                    answerExist.Order = pair.Key + 1;
                    await UpdateAsync(answerExist);
                }
            }

            return await Task.FromResult(typesAnswer);
        }
        #endregion

        #region Quizz Description
        public async Task<List<Answer>> GenerateCorrectAnswersDesc(QuestionType questionType, List<Pokemon> pokemonsAnswer)
        {
            List<Answer> answers = new List<Answer>();
            Random random = new Random();

            Dictionary<int, Pokemon> dic = new Dictionary<int, Pokemon>();
            foreach (var pokemon in pokemonsAnswer)
            {
                while (!dic.ContainsValue(pokemon))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, pokemon);
                }
            }

            foreach (KeyValuePair<int, Pokemon> pair in dic)
            {
                string description = await ConvertDescription(pair.Value);
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = true,
                    IsCorrectID = pair.Value.Id,
                    Libelle = description,
                    Order = pair.Key + 1
                };

                await AddAsync(answer);
                answers.Add(answer);
            }

            return await Task.FromResult(answers);
        }

        private async Task<List<Answer>> GenerateAnswersDesc(QuestionType questionType, List<Pokemon> pokemons, List<Answer> pokemonsAnswer)
        {
            Random random = new Random();

            Dictionary<int, Pokemon> dic = new Dictionary<int, Pokemon>();

            foreach (var pokemon in pokemons)
            {
                while (!dic.ContainsValue(pokemon))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, pokemon);
                }
            }

            foreach (KeyValuePair<int, Pokemon> pair in dic)
            {
                Answer answerExist = pokemonsAnswer.Find(m => m.IsCorrectID.Equals(pair.Value.Id));
                if (answerExist == null)
                {
                    string description = await ConvertDescription(pair.Value);
                    Answer answer = new Answer()
                    {
                        IsSelected = false,
                        IsCorrect = false,
                        IsCorrectID = -1,
                        Libelle = description,
                        Order = pair.Key + 1
                    };

                    pokemonsAnswer.Add(answer);
                }
                else
                {
                    answerExist.Order = pair.Key + 1;
                    await UpdateAsync(answerExist);
                }
            }

            return await Task.FromResult(pokemonsAnswer);
        }

        public async Task<string> ConvertDescription(Pokemon pokemon)
        {
            string description = "";
            if (pokemon.FR.DescriptionVx.Contains(pokemon.FR.DisplayName))
            {
                description = await CheckAndConvert(pokemon.FR.DescriptionVx, pokemon.FR.Evolutions);
            }
            else if (pokemon.FR.DescriptionVx.Contains(pokemon.FR.DisplayName))
            {
                description = await CheckAndConvert(pokemon.FR.DescriptionVy, pokemon.FR.Evolutions);
            }
            else
            {
                description = pokemon.FR.DescriptionVx;
            }

            return await Task.FromResult(description);
        }

        private async Task<string> CheckAndConvert(string description, string family)
        {
            try
            {
                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (string idPok in family.Split(','))
                {
                    int id = int.Parse(idPok);
                    pokemons.Add(await _repositoryP.Get(id));
                }
                
                foreach (Pokemon item in pokemons)
                {
                    description = description.Replace(item.FR.Name, "[...]");
                }

                return await Task.FromResult(description);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Quizz Talent
        public async Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer, bool Reverse)
        {
            List<Answer> answers = new List<Answer>();
            Random random = new Random();

            Dictionary<int, Talent> dic = new Dictionary<int, Talent>();
            foreach (var talent in talentsAnswer)
            {
                while (!dic.ContainsValue(talent))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, talent);
                }
            }

            foreach (KeyValuePair<int, Talent> pair in dic)
            {
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = true,
                    IsCorrectID = pair.Value.Id,
                    Libelle = Reverse ? pair.Value.Name_FR : pair.Value.Description_FR,
                    Order = pair.Key + 1
                };

                await AddAsync(answer);
                answers.Add(answer);
            }

            return await Task.FromResult(answers);
        }

        public async Task<List<Answer>> GenerateCorrectAnswers(QuestionType questionType, List<Talent> talentsAnswer)
        {
            List<Answer> answers = new List<Answer>();
            Random random = new Random();

            Dictionary<int, Talent> dic = new Dictionary<int, Talent>();
            foreach (var type in talentsAnswer)
            {
                while (!dic.ContainsValue(type))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, type);
                }
            }

            foreach (KeyValuePair<int, Talent> pair in dic)
            {
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = true,
                    IsCorrectID = pair.Value.Id,
                    Libelle = pair.Value.Name_FR,
                    Order = pair.Key + 1
                };

                await AddAsync(answer);
                answers.Add(answer);
            }

            return await Task.FromResult(answers);
        }

        private async Task<List<Answer>> GenerateAnswers(QuestionType questionType, List<Talent> talents, List<Answer> talentsAnswer, bool Reverse = false)
        {
            Random random = new Random();

            string result = string.Empty;
            Dictionary<int, Talent> dic = new Dictionary<int, Talent>();

            foreach (var talent in talents)
            {
                while (!dic.ContainsValue(talent))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, talent);
                }
            }

            foreach (KeyValuePair<int, Talent> pair in dic)
            {
                Answer answerExist = talentsAnswer.Find(m => m.IsCorrectID.Equals(pair.Value.Id));
                if (answerExist == null)
                {
                    Answer answer = new Answer()
                    {
                        IsSelected = false,
                        IsCorrect = false,
                        IsCorrectID = -1,
                        Libelle = Reverse ? pair.Value.Name_FR : pair.Value.Description_FR,
                        Order = pair.Key + 1
                    };

                    talentsAnswer.Add(answer);
                }
                else
                {
                    answerExist.Order = pair.Key + 1;
                    await UpdateAsync(answerExist);
                }
            }

            return await Task.FromResult(talentsAnswer);
        }
        #endregion

        #region Quizz Stat
        public async Task<List<Answer>> GenerateCorrectAnswersStat(QuestionType questionType, List<Pokemon> pokemonsAnswer, string typeStat)
        {
            List<Answer> answers = new List<Answer>();
            Random random = new Random();

            string result = string.Empty;
            Dictionary<int, Pokemon> dic = new Dictionary<int, Pokemon>();
            foreach (var pokemon in pokemonsAnswer)
            {
                while (!dic.ContainsValue(pokemon))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, pokemon);
                }
            }

            foreach (KeyValuePair<int, Pokemon> pair in dic)
            {
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = true,
                    IsCorrectID = pair.Value.Id,
                    Libelle = GetValueStat(pair.Value, typeStat),
                    Type = typeStat,
                    Order = pair.Key + 1
                };

                await AddAsync(answer);
                answers.Add(answer);
            }

            return await Task.FromResult(answers);
        }

        private string GetValueStat(Pokemon pokemon, string typeStat)
        {
            string libelle = string.Empty;
            if (typeStat.Equals(Constantes.Pv))
                libelle = pokemon.StatPv.ToString();
            else if (typeStat.Equals(Constantes.Attaque))
                libelle = pokemon.StatAttaque.ToString();
            else if (typeStat.Equals(Constantes.Defense))
                libelle = pokemon.StatDefense.ToString();
            else if (typeStat.Equals(Constantes.AttaqueSpe))
                libelle = pokemon.StatAttaqueSpe.ToString();
            else if (typeStat.Equals(Constantes.DefenseSpe))
                libelle = pokemon.StatDefenseSpe.ToString();
            else if (typeStat.Equals(Constantes.Vitesse))
                libelle = pokemon.StatVitesse.ToString();

            return libelle;
        }

        private async Task<List<Answer>> GenerateAnswersStat(QuestionType questionType, List<Pokemon> pokemons, List<Answer> pokemonsAnswer, string typeStat, List<string> statAnswers)
        {
            Random random = new Random();

            string result = string.Empty;
            Dictionary<int, Pokemon> dic = new Dictionary<int, Pokemon>();
            foreach (var pokemon in pokemons)
            {
                while (!dic.ContainsValue(pokemon))
                {
                    int numberRandom = random.Next(questionType.NbAnswers);

                    if (!dic.ContainsKey(numberRandom))
                        dic.Add(numberRandom, pokemon);
                }
            }

            foreach (KeyValuePair<int, Pokemon> pair in dic)
            {
                Answer answerExist = pokemonsAnswer.Find(m => m.IsCorrectID.Equals(pair.Value.Id));
                answerExist.Order = pair.Key + 1;
                await UpdateAsync(answerExist);

                statAnswers.Remove(GetValueStat(pair.Value, typeStat));
            }

            Dictionary<int, string> dicStat = new Dictionary<int, string>();
            foreach (var stat in statAnswers)
            {
                int numberRandom = random.Next(questionType.NbAnswers);
                while (!dicStat.ContainsValue(stat))
                {
                    numberRandom = random.Next(questionType.NbAnswers);
                    if (!dicStat.ContainsKey(numberRandom) && !dic.ContainsKey(numberRandom))
                        dicStat.Add(numberRandom, stat);
                }
            }

            foreach (KeyValuePair<int, string> pair in dicStat)
            {
                Answer answer = new Answer()
                {
                    IsSelected = false,
                    IsCorrect = false,
                    IsCorrectID = -1,
                    Libelle = pair.Value,
                    Type = typeStat,
                    Order = pair.Key + 1
                };

                pokemonsAnswer.Add(answer);
            }

            return await Task.FromResult(pokemonsAnswer);
        }
        #endregion

        #region Answer
        public async Task<List<Answer>> UpdateAfterValidation(List<Answer> answersSelected)
        {
            foreach (Answer answer in answersSelected)
            {
                if (answer.Id != 0)
                {
                    answer.IsSelected = true;
                    await UpdateAsync(answer);
                }
                else
                {
                    answer.IsSelected = true;
                    answer.IsCorrect = false;
                    await AddAsync(answer);
                }
            }

            return await Task.FromResult(answersSelected);
        }
        #endregion
        #endregion

        #region Private Methods
        private async Task<List<Answer>> GetAnswersID_QTypPok(ClassQuizz.Quizz quizz, QuestionType questionType, List<Pokemon> alreadyExist)
        {
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                await Task.Run(async () =>
                {
                    Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExist);
                    Pokemon pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    while (pokemonExist != null)
                    {
                        pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExist);
                        pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    }
                    pokemonsAnswer.Add(pokemon);
                });
            }

            return await Task.FromResult(GenerateCorrectAnswers(questionType, pokemonsAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypPokFamily(QuestionType questionType, Pokemon pokemon)
        {
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            if (pokemon.FR.Evolutions != null)
            {
                foreach (var item in pokemon.FR.Evolutions.Split(','))
                {
                    if (!item.Equals(pokemon.Id.ToString()))
                        pokemonsAnswer.Add(await _repositoryP.SingleOrDefault(m => m.FR.Name.Equals(item)));
                }
            }

            return await Task.FromResult(GenerateCorrectAnswers(questionType, pokemonsAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypPok(ClassQuizz.Quizz quizz, QuestionType questionType, TypePok typePok, List<TypePok> alreadyExistQTypTyp)
        {
            Random random = new Random();
            int nbAnswerCorrectRandom = random.Next(questionType.NbAnswers);
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int i = 0; i < nbAnswerCorrectRandom; i++)
            {
                Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, typePok, pokemonsAnswer);
                if (pokemonsAnswer.Find(m => m.EN.Name.Equals(pokemon.EN.Name)) == null)
                    pokemonsAnswer.Add(pokemon);
                else
                    break;
            }

            return await Task.FromResult(GenerateCorrectAnswers(questionType, pokemonsAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypTypPok(QuestionType questionType, Pokemon pokemon, bool various)
        {
            List<TypePok> typesAnswer = new List<TypePok>();
            List<Pokemon_TypePok> pokemonTypePoks = pokemon.Pokemon_TypePoks;

            if (!various)
                typesAnswer.Add(pokemonTypePoks[0].TypePok);
            else
                foreach (Pokemon_TypePok item in pokemonTypePoks)
                    typesAnswer.Add(item.TypePok);

            return await Task.FromResult(GenerateCorrectAnswers(questionType, typesAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypWeakPok(QuestionType questionType, Pokemon pokemon, bool various)
        {
            List<TypePok> weaknessAnswer = new List<TypePok>();
            List<Pokemon_Weakness> pokemonWeaknesses = pokemon.Pokemon_Weaknesses;

            if (!various)
                weaknessAnswer.Add(pokemonWeaknesses[0].TypePok);
            else
                foreach (Pokemon_Weakness item in pokemonWeaknesses)
                    weaknessAnswer.Add(item.TypePok);

            return await Task.FromResult(GenerateCorrectAnswers(questionType, weaknessAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypTyp(QuestionType questionType, List<TypePok> alreadySelected)
        {
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

            return await Task.FromResult(GenerateCorrectAnswers(questionType, typesAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypPokDesc(ClassQuizz.Quizz quizz, QuestionType questionType, List<Pokemon> alreadyExist)
        {
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                await Task.Run(async () =>
                {
                    Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExist);
                    Pokemon pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    while (pokemonExist != null)
                    {
                        pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExist);
                        pokemonExist = alreadyExist.SingleOrDefault(m => m.Id.Equals(pokemon.Id));
                    }
                    pokemonsAnswer.Add(pokemon);
                });
            }

            return await Task.FromResult(GenerateCorrectAnswersDesc(questionType, pokemonsAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypTalentPok(QuestionType questionType, Pokemon pokemon)
        {
            List<Talent> talentsAnswer = new List<Talent>();
            List<Pokemon_Talent> pokemonTalentServices = pokemon.Pokemon_Talents;

            if (pokemonTalentServices != null)
                foreach (Pokemon_Talent item in pokemonTalentServices)
                    talentsAnswer.Add(item.Talent);

            return await Task.FromResult(GenerateCorrectAnswers(questionType, talentsAnswer).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypTalent(QuestionType questionType, List<Talent> alreadySelected)
        {
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
                return await Task.FromResult(GenerateCorrectAnswers(questionType, talentsAnswer, false).Result);
            else
                return await Task.FromResult(GenerateCorrectAnswers(questionType, talentsAnswer, true).Result);
        }

        private async Task<List<Answer>> GetAnswersID_QTypPokStat(ClassQuizz.Quizz quizz, QuestionType questionType, List<Pokemon> alreadyExist)
        {
            List<Pokemon> pokemonsAnswer = new List<Pokemon>();

            for (int nbAnswer = 0; nbAnswer < questionType.NbAnswersPossible; nbAnswer++)
            {
                await Task.Run(async () =>
                {
                    Pokemon pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExist);
                    Pokemon pokemonExist = alreadyExist.Find(m => m.Id.Equals(pokemon.Id));
                    while (pokemonExist != null)
                    {
                        pokemon = await _repositoryP.GetPokemonRandom(quizz, alreadyExist);
                        pokemonExist = alreadyExist.Find(m => m.Id.Equals(pokemon.Id));
                    }
                    pokemonsAnswer.Add(pokemon);
                });
            }

            string typeStat = GetRandomTypeStat();
            return await Task.FromResult(GenerateCorrectAnswersStat(questionType, pokemonsAnswer, typeStat).Result);
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
