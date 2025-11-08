using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Repository;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DatabaseController : ControllerBase
    {
        #region Fields
        private readonly PokemonRepository _repository;
        private readonly TypePokRepository _repositoryTP;
        private readonly AbilityRepository _repositoryTL;
        private readonly AttackRepository _repositoryAT;
        private readonly TypeAttackRepository _repositoryTA;
        private readonly GameRepository _repositoryG;
        private readonly Pokemon_TypePokRepository _repositoryPTP;
        private readonly Pokemon_WeaknessRepository _repositoryPWN;
        private readonly Pokemon_AbilityRepository _repositoryPTL;
        private readonly Pokemon_AttackRepository _repositoryPAT;
        private readonly DifficultyRepository _repositoryD;
        private readonly QuestionTypeRepository _repositoryQT;
        #endregion

        #region Constructors
        public DatabaseController(PokemonRepository repository, 
            TypePokRepository repositoryTP, 
            AbilityRepository repositoryTL, 
            AttackRepository repositoryAT, 
            TypeAttackRepository repositoryTA, 
            GameRepository repositoryG, 
            Pokemon_TypePokRepository repositoryPTP, 
            Pokemon_WeaknessRepository repositoryPWN, 
            Pokemon_AbilityRepository repositoryPTL, 
            Pokemon_AttackRepository repositoryPAT,
            DifficultyRepository repositoryD,
            QuestionTypeRepository repositoryQT)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryTL = repositoryTL;
            _repositoryAT = repositoryAT;
            _repositoryTA = repositoryTA;
            _repositoryG = repositoryG;
            _repositoryPTP = repositoryPTP;
            _repositoryPWN = repositoryPWN;
            _repositoryPTL = repositoryPTL;
            _repositoryPAT = repositoryPAT;
            _repositoryD = repositoryD;
            _repositoryQT = repositoryQT;
        }
        #endregion
        
        #region Public Methods
        [HttpGet]
        [Route("ExportDb")]
        public Task ExportDb()
        {
            List<Pokemon> pokemons = _repository.GetAll().Result.ToList();

            List<TypeAttack> typeAttacks = _repositoryTA.GetAll().Result.ToList();
            List<Attack> attacks = _repositoryAT.GetAll().Result.ToList();
            List<TypePok> typePoks = _repositoryTP.GetAll().Result.ToList();
            List<Ability> abilities = _repositoryTL.GetAll().Result.ToList();
            List<Game> games = _repositoryG.GetAll().Result.ToList();

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(pokemons, typePoks, abilities, attacks, typeAttacks, games);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            return Task.CompletedTask;
        }

        [HttpGet]
        [Route("ImportDb")]
        public async Task ImportDb()
        {
            string json;

            #region Game
            using (StreamReader r = new StreamReader(Constantes.pathExport + "GameDbToJson.json"))
            {
                json = r.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                {
                    List<GameExportJson> games = JsonConvert.DeserializeObject<List<GameExportJson>>(json);
                    foreach (GameExportJson gameJson in games)
                    {
                        Game game = new Game()
                        {
                            Name_FR = gameJson.Name_FR,
                            Name_EN = gameJson.Name_EN,
                            Name_ES = gameJson.Name_ES,
                            Name_IT = gameJson.Name_IT,
                            Name_DE = gameJson.Name_DE,
                            Name_RU = gameJson.Name_RU,
                            Name_CO = gameJson.Name_CO,
                            Name_CN = gameJson.Name_CN,
                            Name_JP = gameJson.Name_JP
                        };

                        await _repositoryG.AddAsync(game);
                    }
                }
            }
            #endregion

            #region TypeAttack
            using (StreamReader r = new StreamReader(Constantes.pathExport + "TypeAttaqueDbToJson.json"))
            {
               json = r.ReadToEnd();
               if (!string.IsNullOrEmpty(json))
               {
                   List<TypeAttaqueExportJson> typesAttackJson = JsonConvert.DeserializeObject<List<TypeAttaqueExportJson>>(json);
                   foreach (TypeAttaqueExportJson typeAttackJson in typesAttackJson)
                   {
                       TypeAttack typeAttack = new TypeAttack()
                       {
                           Name_FR = typeAttackJson.Name_FR,
                           Description_FR = typeAttackJson.Description_FR,
                           Name_EN = typeAttackJson.Name_EN,
                           Description_EN = typeAttackJson.Description_EN,
                           Name_ES = typeAttackJson.Name_ES,
                           Description_ES = typeAttackJson.Description_ES,
                           Name_IT = typeAttackJson.Name_IT,
                           Description_IT = typeAttackJson.Description_IT,
                           Name_DE = typeAttackJson.Name_DE,
                           Description_DE = typeAttackJson.Description_DE,
                           Name_RU = typeAttackJson.Name_RU,
                           Description_RU = typeAttackJson.Description_RU,
                           Name_CO = typeAttackJson.Name_CO,
                           Description_CO = typeAttackJson.Description_CO,
                           Name_CN = typeAttackJson.Name_CN,
                           Description_CN = typeAttackJson.Description_CN,
                           Name_JP = typeAttackJson.Name_JP,
                           Description_JP = typeAttackJson.Description_JP,
                           UrlImg = typeAttackJson.UrlImg
                       };

                       await _repositoryTA.AddAsync(typeAttack);
                   }
               }
            }
            #endregion

            #region TypePok
            using (StreamReader r = new StreamReader(Constantes.pathExport + "TypePokDbToJson.json"))
            {
               json = r.ReadToEnd();
               if (!string.IsNullOrEmpty(json))
               {
                   List<TypePokExportJson> typePoksJson = JsonConvert.DeserializeObject<List<TypePokExportJson>>(json);
                   foreach (TypePokExportJson typePokJson in typePoksJson)
                   {
                       TypePok typePok = new()
                       {
                           Name_FR = typePokJson.Name_FR,
                           PathMiniHome_FR = typePokJson.UrlMiniHome_FR,
                           Name_EN = typePokJson.Name_EN,
                           PathMiniHome_EN = typePokJson.UrlMiniHome_EN,
                           Name_ES = typePokJson.Name_ES,
                           PathMiniHome_ES = typePokJson.UrlMiniHome_ES,
                           Name_IT = typePokJson.Name_IT,
                           PathMiniHome_IT = typePokJson.UrlMiniHome_IT,
                           Name_DE = typePokJson.Name_DE,
                           PathMiniHome_DE = typePokJson.UrlMiniHome_DE,
                           Name_RU = typePokJson.Name_RU,
                           PathMiniHome_RU = typePokJson.UrlMiniHome_RU,
                           Name_CO = typePokJson.Name_CO,
                           PathMiniHome_CO = typePokJson.UrlMiniHome_CO,
                           Name_CN = typePokJson.Name_CN,
                           PathMiniHome_CN = typePokJson.UrlMiniHome_CN,
                           Name_JP = typePokJson.Name_JP,
                           PathMiniHome_JP = typePokJson.UrlMiniHome_JP,
                           UrlMiniGo = typePokJson.UrlMiniGo,
                           UrlFondGo = typePokJson.UrlFondGo,
                           UrlIconHome = typePokJson.UrlIconHome,
                           UrlAutoHome = typePokJson.UrlAutoHome,
                           ImgColor = typePokJson.ImgColor,
                           InfoColor = typePokJson.InfoColor,
                           TypeColor = typePokJson.TypeColor
                       };
                       await _repositoryTP.AddAsync(typePok);
                   }
               }
            }
            #endregion

            #region Talent
            using (StreamReader r = new StreamReader(Constantes.pathExport + "TalentDbToJson.json"))
            {
               json = r.ReadToEnd();
               if (!string.IsNullOrEmpty(json))
               {
                   List<TalentExportJson> abilitiesJson = JsonConvert.DeserializeObject<List<TalentExportJson>>(json);
                   foreach (TalentExportJson abilityJson in abilitiesJson)
                   {
                       Ability ability = new()
                       {
                           Name_FR = abilityJson.Name_FR,
                           Description_FR = abilityJson.Description_FR,
                           Name_EN = abilityJson.Name_EN,
                           Description_EN = abilityJson.Description_EN,
                           Name_ES = abilityJson.Name_ES,
                           Description_ES = abilityJson.Description_ES,
                           Name_IT = abilityJson.Name_IT,
                           Description_IT = abilityJson.Description_IT,
                           Name_DE = abilityJson.Name_DE,
                           Description_DE = abilityJson.Description_DE,
                           Name_RU = abilityJson.Name_RU,
                           Description_RU = abilityJson.Description_RU,
                           Name_CO = abilityJson.Name_CO,
                           Description_CO = abilityJson.Description_CO,
                           Name_CN = abilityJson.Name_CN,
                           Description_CN = abilityJson.Description_CN,
                           Name_JP = abilityJson.Name_JP,
                           Description_JP = abilityJson.Description_JP
                       };
                       await _repositoryTL.AddAsync(ability);
                   }
               }
            }
            #endregion

            #region Attack
            using (StreamReader r = new StreamReader(Constantes.pathExport + "AttaqueDbToJson.json"))
            {
               json = r.ReadToEnd();
               if (!string.IsNullOrEmpty(json))
               {
                   List<AttaqueExportJson> attacksJson = JsonConvert.DeserializeObject<List<AttaqueExportJson>>(json);
                   foreach (AttaqueExportJson attackJson in attacksJson)
                   {
                       TypePok typePok = (await _repositoryTP.Find(m => m.Name_EN.Equals(attackJson.Types.Name_EN))).FirstOrDefault();
                       TypeAttack typeAttack = (await _repositoryTA.Find(m => m.Name_EN.Equals(attackJson.TypeAttaque.Name_EN))).FirstOrDefault();
                       Attack attack = new()
                       {
                           Name_FR = attackJson.Name_FR,
                           Description_FR = attackJson.Description_FR,
                           Name_EN = attackJson.Name_EN,
                           Description_EN = attackJson.Description_EN,
                           Name_ES = attackJson.Name_ES,
                           Description_ES = attackJson.Description_ES,
                           Name_IT = attackJson.Name_IT,
                           Description_IT = attackJson.Description_IT,
                           Name_DE = attackJson.Name_DE,
                           Description_DE = attackJson.Description_DE,
                           Name_RU = attackJson.Name_RU,
                           Description_RU = attackJson.Description_RU,
                           Name_CO = attackJson.Name_CO,
                           Description_CO = attackJson.Description_CO,
                           Name_CN = attackJson.Name_CN,
                           Description_CN = attackJson.Description_CN,
                           Name_JP = attackJson.Name_JP,
                           Description_JP = attackJson.Description_JP,
                           TypeAttack = typeAttack,
                           TypePok = typePok,
                           Power = attackJson.Puissance,
                           Precision = attackJson.Precision,
                           PP = attackJson.PP
                       };
                       await _repositoryAT.AddAsync(attack);
                   }
               }
            }
            #endregion


            using (StreamReader sr = new StreamReader(Constantes.pathExport + "DbToJson.json"))
            {
                json = sr.ReadToEnd();
                await _repository.ImportJsonToDb(json);
            }
        }

        [HttpPost]
        [Route("SaveInDB")]
        public void SaveInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap/PokeScrap.json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("SaveInfoPokemonAttackInDB")]
        public void SaveInfoPokemonAttackInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap/PokeBipScrapGen.json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveInfoPokemonAttackInDB(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("SaveGenInDB/{gen}")]
        public void SaveGenInDB(int gen)
        {
            string json;
            using (StreamReader sr = new StreamReader("PokeScrap/PokeScrapGen" + gen + ".json"))
            {
                json = sr.ReadToEnd();
                _repository.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("AddGameInDB")]
        public async Task AddGameInDB()
        {
            List<Game> games = new List<Game>();

            Game game = new Game();
            game.Name_FR = Constantes.RedBlue_Name_FR;
            game.Name_EN = Constantes.RedBlue_Name_EN;
            game.Name_ES = Constantes.RedBlue_Name_ES;
            game.Name_IT = Constantes.RedBlue_Name_IT;
            game.Name_DE = Constantes.RedBlue_Name_DE;
            game.Name_RU = Constantes.RedBlue_Name_RU;
            game.Name_CO = Constantes.RedBlue_Name_CO;
            game.Name_CN = Constantes.RedBlue_Name_CN;
            game.Name_JP = Constantes.RedBlue_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);
            
            game = new Game();
            game.Name_FR = Constantes.Yellow_Name_FR;
            game.Name_EN = Constantes.Yellow_Name_EN;
            game.Name_ES = Constantes.Yellow_Name_ES;
            game.Name_IT = Constantes.Yellow_Name_IT;
            game.Name_DE = Constantes.Yellow_Name_DE;
            game.Name_RU = Constantes.Yellow_Name_RU;
            game.Name_CO = Constantes.Yellow_Name_CO;
            game.Name_CN = Constantes.Yellow_Name_CN;
            game.Name_JP = Constantes.Yellow_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.GoldSilver_Name_FR;
            game.Name_EN = Constantes.GoldSilver_Name_EN;
            game.Name_ES = Constantes.GoldSilver_Name_ES;
            game.Name_IT = Constantes.GoldSilver_Name_IT;
            game.Name_DE = Constantes.GoldSilver_Name_DE;
            game.Name_RU = Constantes.GoldSilver_Name_RU;
            game.Name_CO = Constantes.GoldSilver_Name_CO;
            game.Name_CN = Constantes.GoldSilver_Name_CN;
            game.Name_JP = Constantes.GoldSilver_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.Crystal_Name_FR;
            game.Name_EN = Constantes.Crystal_Name_EN;
            game.Name_ES = Constantes.Crystal_Name_ES;
            game.Name_IT = Constantes.Crystal_Name_IT;
            game.Name_DE = Constantes.Crystal_Name_DE;
            game.Name_RU = Constantes.Crystal_Name_RU;
            game.Name_CO = Constantes.Crystal_Name_CO;
            game.Name_CN = Constantes.Crystal_Name_CN;
            game.Name_JP = Constantes.Crystal_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.RubySapphire_Name_FR;
            game.Name_EN = Constantes.RubySapphire_Name_EN;
            game.Name_ES = Constantes.RubySapphire_Name_ES;
            game.Name_IT = Constantes.RubySapphire_Name_IT;
            game.Name_DE = Constantes.RubySapphire_Name_DE;
            game.Name_RU = Constantes.RubySapphire_Name_RU;
            game.Name_CO = Constantes.RubySapphire_Name_CO;
            game.Name_CN = Constantes.RubySapphire_Name_CN;
            game.Name_JP = Constantes.RubySapphire_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.Emerald_Name_FR;
            game.Name_EN = Constantes.Emerald_Name_EN;
            game.Name_ES = Constantes.Emerald_Name_ES;
            game.Name_IT = Constantes.Emerald_Name_IT;
            game.Name_DE = Constantes.Emerald_Name_DE;
            game.Name_RU = Constantes.Emerald_Name_RU;
            game.Name_CO = Constantes.Emerald_Name_CO;
            game.Name_CN = Constantes.Emerald_Name_CN;
            game.Name_JP = Constantes.Emerald_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.FireRedLeafGreen_Name_FR;
            game.Name_EN = Constantes.FireRedLeafGreen_Name_EN;
            game.Name_ES = Constantes.FireRedLeafGreen_Name_ES;
            game.Name_IT = Constantes.FireRedLeafGreen_Name_IT;
            game.Name_DE = Constantes.FireRedLeafGreen_Name_DE;
            game.Name_RU = Constantes.FireRedLeafGreen_Name_RU;
            game.Name_CO = Constantes.FireRedLeafGreen_Name_CO;
            game.Name_CN = Constantes.FireRedLeafGreen_Name_CN;
            game.Name_JP = Constantes.FireRedLeafGreen_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.DiamondPearl_Name_FR;
            game.Name_EN = Constantes.DiamondPearl_Name_EN;
            game.Name_ES = Constantes.DiamondPearl_Name_ES;
            game.Name_IT = Constantes.DiamondPearl_Name_IT;
            game.Name_DE = Constantes.DiamondPearl_Name_DE;
            game.Name_RU = Constantes.DiamondPearl_Name_RU;
            game.Name_CO = Constantes.DiamondPearl_Name_CO;
            game.Name_CN = Constantes.DiamondPearl_Name_CN;
            game.Name_JP = Constantes.DiamondPearl_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.Platinum_Name_FR;
            game.Name_EN = Constantes.Platinum_Name_EN;
            game.Name_ES = Constantes.Platinum_Name_ES;
            game.Name_IT = Constantes.Platinum_Name_IT;
            game.Name_DE = Constantes.Platinum_Name_DE;
            game.Name_RU = Constantes.Platinum_Name_RU;
            game.Name_CO = Constantes.Platinum_Name_CO;
            game.Name_CN = Constantes.Platinum_Name_CN;
            game.Name_JP = Constantes.Platinum_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.HeartGoldSoulSilver_Name_FR;
            game.Name_EN = Constantes.HeartGoldSoulSilver_Name_EN;
            game.Name_ES = Constantes.HeartGoldSoulSilver_Name_ES;
            game.Name_IT = Constantes.HeartGoldSoulSilver_Name_IT;
            game.Name_DE = Constantes.HeartGoldSoulSilver_Name_DE;
            game.Name_RU = Constantes.HeartGoldSoulSilver_Name_RU;
            game.Name_CO = Constantes.HeartGoldSoulSilver_Name_CO;
            game.Name_CN = Constantes.HeartGoldSoulSilver_Name_CN;
            game.Name_JP = Constantes.HeartGoldSoulSilver_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.BlackWhite_Name_FR;
            game.Name_EN = Constantes.BlackWhite_Name_EN;
            game.Name_ES = Constantes.BlackWhite_Name_ES;
            game.Name_IT = Constantes.BlackWhite_Name_IT;
            game.Name_DE = Constantes.BlackWhite_Name_DE;
            game.Name_RU = Constantes.BlackWhite_Name_RU;
            game.Name_CO = Constantes.BlackWhite_Name_CO;
            game.Name_CN = Constantes.BlackWhite_Name_CN;
            game.Name_JP = Constantes.BlackWhite_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.Black2White2_Name_FR;
            game.Name_EN = Constantes.Black2White2_Name_EN;
            game.Name_ES = Constantes.Black2White2_Name_ES;
            game.Name_IT = Constantes.Black2White2_Name_IT;
            game.Name_DE = Constantes.Black2White2_Name_DE;
            game.Name_RU = Constantes.Black2White2_Name_RU;
            game.Name_CO = Constantes.Black2White2_Name_CO;
            game.Name_CN = Constantes.Black2White2_Name_CN;
            game.Name_JP = Constantes.Black2White2_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.X_Y_Name_FR;
            game.Name_EN = Constantes.X_Y_Name_EN;
            game.Name_ES = Constantes.X_Y_Name_ES;
            game.Name_IT = Constantes.X_Y_Name_IT;
            game.Name_DE = Constantes.X_Y_Name_DE;
            game.Name_RU = Constantes.X_Y_Name_RU;
            game.Name_CO = Constantes.X_Y_Name_CO;
            game.Name_CN = Constantes.X_Y_Name_CN;
            game.Name_JP = Constantes.X_Y_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.SunMoon_Name_FR;
            game.Name_EN = Constantes.SunMoon_Name_EN;
            game.Name_ES = Constantes.SunMoon_Name_ES;
            game.Name_IT = Constantes.SunMoon_Name_IT;
            game.Name_DE = Constantes.SunMoon_Name_DE;
            game.Name_RU = Constantes.SunMoon_Name_RU;
            game.Name_CO = Constantes.SunMoon_Name_CO;
            game.Name_CN = Constantes.SunMoon_Name_CN;
            game.Name_JP = Constantes.SunMoon_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.UltraSunUltraMoon_Name_FR;
            game.Name_EN = Constantes.UltraSunUltraMoon_Name_EN;
            game.Name_ES = Constantes.UltraSunUltraMoon_Name_ES;
            game.Name_IT = Constantes.UltraSunUltraMoon_Name_IT;
            game.Name_DE = Constantes.UltraSunUltraMoon_Name_DE;
            game.Name_RU = Constantes.UltraSunUltraMoon_Name_RU;
            game.Name_CO = Constantes.UltraSunUltraMoon_Name_CO;
            game.Name_CN = Constantes.UltraSunUltraMoon_Name_CN;
            game.Name_JP = Constantes.UltraSunUltraMoon_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.LetsGoPikachuEvoli_Name_FR;
            game.Name_EN = Constantes.LetsGoPikachuEvoli_Name_EN;
            game.Name_ES = Constantes.LetsGoPikachuEvoli_Name_ES;
            game.Name_IT = Constantes.LetsGoPikachuEvoli_Name_IT;
            game.Name_DE = Constantes.LetsGoPikachuEvoli_Name_DE;
            game.Name_RU = Constantes.LetsGoPikachuEvoli_Name_RU;
            game.Name_CO = Constantes.LetsGoPikachuEvoli_Name_CO;
            game.Name_CN = Constantes.LetsGoPikachuEvoli_Name_CN;
            game.Name_JP = Constantes.LetsGoPikachuEvoli_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.SwordShield_Name_FR;
            game.Name_EN = Constantes.SwordShield_Name_EN;
            game.Name_ES = Constantes.SwordShield_Name_ES;
            game.Name_IT = Constantes.SwordShield_Name_IT;
            game.Name_DE = Constantes.SwordShield_Name_DE;
            game.Name_RU = Constantes.SwordShield_Name_RU;
            game.Name_CO = Constantes.SwordShield_Name_CO;
            game.Name_CN = Constantes.SwordShield_Name_CN;
            game.Name_JP = Constantes.SwordShield_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.ShiningDiamondShiningPearl_Name_FR;
            game.Name_EN = Constantes.ShiningDiamondShiningPearl_Name_EN;
            game.Name_ES = Constantes.ShiningDiamondShiningPearl_Name_ES;
            game.Name_IT = Constantes.ShiningDiamondShiningPearl_Name_IT;
            game.Name_DE = Constantes.ShiningDiamondShiningPearl_Name_DE;
            game.Name_RU = Constantes.ShiningDiamondShiningPearl_Name_RU;
            game.Name_CO = Constantes.ShiningDiamondShiningPearl_Name_CO;
            game.Name_CN = Constantes.ShiningDiamondShiningPearl_Name_CN;
            game.Name_JP = Constantes.ShiningDiamondShiningPearl_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.Arceus_Name_FR;
            game.Name_EN = Constantes.Arceus_Name_EN;
            game.Name_ES = Constantes.Arceus_Name_ES;
            game.Name_IT = Constantes.Arceus_Name_IT;
            game.Name_DE = Constantes.Arceus_Name_DE;
            game.Name_RU = Constantes.Arceus_Name_RU;
            game.Name_CO = Constantes.Arceus_Name_CO;
            game.Name_CN = Constantes.Arceus_Name_CN;
            game.Name_JP = Constantes.Arceus_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            game = new Game();
            game.Name_FR = Constantes.ScarletViolet_Name_FR;
            game.Name_EN = Constantes.ScarletViolet_Name_EN;
            game.Name_ES = Constantes.ScarletViolet_Name_ES;
            game.Name_IT = Constantes.ScarletViolet_Name_IT;
            game.Name_DE = Constantes.ScarletViolet_Name_DE;
            game.Name_RU = Constantes.ScarletViolet_Name_RU;
            game.Name_CO = Constantes.ScarletViolet_Name_CO;
            game.Name_CN = Constantes.ScarletViolet_Name_CN;
            game.Name_JP = Constantes.ScarletViolet_Name_JP;
            if (_repositoryG.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            await _repositoryG.AddRangeAsync(games);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("AddMissingTalent")]
        public async Task AddMissingTalent()
        {
            Ability ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Analytic_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Analytic_FR,
                    Description_FR = Constantes.Description_Analytic_FR,
                    Name_EN = Constantes.Name_Analytic_EN,
                    Description_EN = Constantes.Description_Analytic_EN,
                    Name_ES = Constantes.Name_Analytic_ES,
                    Description_ES = Constantes.Description_Analytic_ES,
                    Name_IT = Constantes.Name_Analytic_IT,
                    Description_IT = Constantes.Description_Analytic_IT,
                    Name_DE = Constantes.Name_Analytic_DE,
                    Description_DE = Constantes.Description_Analytic_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_PowerOfAlchemy_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_PowerOfAlchemy_FR,
                    Description_FR = Constantes.Description_PowerOfAlchemy_FR,
                    Name_EN = Constantes.Name_PowerOfAlchemy_EN,
                    Description_EN = Constantes.Description_PowerOfAlchemy_EN,
                    Name_ES = Constantes.Name_PowerOfAlchemy_ES,
                    Description_ES = Constantes.Description_PowerOfAlchemy_ES,
                    Name_IT = Constantes.Name_PowerOfAlchemy_IT,
                    Description_IT = Constantes.Description_PowerOfAlchemy_IT,
                    Name_DE = Constantes.Name_PowerOfAlchemy_DE,
                    Description_DE = Constantes.Description_PowerOfAlchemy_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Harvest_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Harvest_FR,
                    Description_FR = Constantes.Description_Harvest_FR,
                    Name_EN = Constantes.Name_Harvest_EN,
                    Description_EN = Constantes.Description_Harvest_EN,
                    Name_ES = Constantes.Name_Harvest_ES,
                    Description_ES = Constantes.Description_Harvest_ES,
                    Name_IT = Constantes.Name_Harvest_IT,
                    Description_IT = Constantes.Description_Harvest_IT,
                    Name_DE = Constantes.Name_Harvest_DE,
                    Description_DE = Constantes.Description_Harvest_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Imposter_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Imposter_FR,
                    Description_FR = Constantes.Description_Imposter_FR,
                    Name_EN = Constantes.Name_Imposter_EN,
                    Description_EN = Constantes.Description_Imposter_EN,
                    Name_ES = Constantes.Name_Imposter_ES,
                    Description_ES = Constantes.Description_Imposter_ES,
                    Name_IT = Constantes.Name_Imposter_IT,
                    Description_IT = Constantes.Description_Imposter_IT,
                    Name_DE = Constantes.Name_Imposter_DE,
                    Description_DE = Constantes.Description_Imposter_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Multiscale_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Multiscale_FR,
                    Description_FR = Constantes.Description_Multiscale_FR,
                    Name_EN = Constantes.Name_Multiscale_EN,
                    Description_EN = Constantes.Description_Multiscale_EN,
                    Name_ES = Constantes.Name_Multiscale_ES,
                    Description_ES = Constantes.Description_Multiscale_ES,
                    Name_IT = Constantes.Name_Multiscale_IT,
                    Description_IT = Constantes.Description_Multiscale_IT,
                    Name_DE = Constantes.Name_Multiscale_DE,
                    Description_DE = Constantes.Description_Multiscale_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Moody_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Moody_FR,
                    Description_FR = Constantes.Description_Moody_FR,
                    Name_EN = Constantes.Name_Moody_EN,
                    Description_EN = Constantes.Description_Moody_EN,
                    Name_ES = Constantes.Name_Moody_ES,
                    Description_ES = Constantes.Description_Moody_ES,
                    Name_IT = Constantes.Name_Moody_IT,
                    Description_IT = Constantes.Description_Moody_IT,
                    Name_DE = Constantes.Name_Moody_DE,
                    Description_DE = Constantes.Description_Moody_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_ToxicBoost_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_ToxicBoost_FR,
                    Description_FR = Constantes.Description_ToxicBoost_FR,
                    Name_EN = Constantes.Name_ToxicBoost_EN,
                    Description_EN = Constantes.Description_ToxicBoost_EN,
                    Name_ES = Constantes.Name_ToxicBoost_ES,
                    Description_ES = Constantes.Description_ToxicBoost_ES,
                    Name_IT = Constantes.Name_ToxicBoost_IT,
                    Description_IT = Constantes.Description_ToxicBoost_IT,
                    Name_DE = Constantes.Name_ToxicBoost_DE,
                    Description_DE = Constantes.Description_ToxicBoost_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Protean_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Protean_FR,
                    Description_FR = Constantes.Description_Protean_FR,
                    Name_EN = Constantes.Name_Protean_EN,
                    Description_EN = Constantes.Description_Protean_EN,
                    Name_ES = Constantes.Name_Protean_ES,
                    Description_ES = Constantes.Description_Protean_ES,
                    Name_IT = Constantes.Name_Protean_IT,
                    Description_IT = Constantes.Description_Protean_IT,
                    Name_DE = Constantes.Name_Protean_DE,
                    Description_DE = Constantes.Description_Protean_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_FlareBoost_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_FlareBoost_FR,
                    Description_FR = Constantes.Description_FlareBoost_FR,
                    Name_EN = Constantes.Name_FlareBoost_EN,
                    Description_EN = Constantes.Description_FlareBoost_EN,
                    Name_ES = Constantes.Name_FlareBoost_ES,
                    Description_ES = Constantes.Description_FlareBoost_ES,
                    Name_IT = Constantes.Name_FlareBoost_IT,
                    Description_IT = Constantes.Description_FlareBoost_IT,
                    Name_DE = Constantes.Name_FlareBoost_DE,
                    Description_DE = Constantes.Description_FlareBoost_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_ZenMode_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_ZenMode_FR,
                    Description_FR = Constantes.Description_ZenMode_FR,
                    Name_EN = Constantes.Name_ZenMode_EN,
                    Description_EN = Constantes.Description_ZenMode_EN,
                    Name_ES = Constantes.Name_ZenMode_ES,
                    Description_ES = Constantes.Description_ZenMode_ES,
                    Name_IT = Constantes.Name_ZenMode_IT,
                    Description_IT = Constantes.Description_ZenMode_IT,
                    Name_DE = Constantes.Name_ZenMode_DE,
                    Description_DE = Constantes.Description_ZenMode_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_GaleWings_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_GaleWings_FR,
                    Description_FR = Constantes.Description_GaleWings_FR,
                    Name_EN = Constantes.Name_GaleWings_EN,
                    Description_EN = Constantes.Description_GaleWings_EN,
                    Name_ES = Constantes.Name_GaleWings_ES,
                    Description_ES = Constantes.Description_GaleWings_ES,
                    Name_IT = Constantes.Name_GaleWings_IT,
                    Description_IT = Constantes.Description_GaleWings_IT,
                    Name_DE = Constantes.Name_GaleWings_DE,
                    Description_DE = Constantes.Description_GaleWings_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Symbiosis_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Symbiosis_FR,
                    Description_FR = Constantes.Description_Symbiosis_FR,
                    Name_EN = Constantes.Name_Symbiosis_EN,
                    Description_EN = Constantes.Description_Symbiosis_EN,
                    Name_ES = Constantes.Name_Symbiosis_ES,
                    Description_ES = Constantes.Description_Symbiosis_ES,
                    Name_IT = Constantes.Name_Symbiosis_IT,
                    Description_IT = Constantes.Description_Symbiosis_IT,
                    Name_DE = Constantes.Name_Symbiosis_DE,
                    Description_DE = Constantes.Description_Symbiosis_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_GrassPelt_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_GrassPelt_FR,
                    Description_FR = Constantes.Description_GrassPelt_FR,
                    Name_EN = Constantes.Name_GrassPelt_EN,
                    Description_EN = Constantes.Description_GrassPelt_EN,
                    Name_ES = Constantes.Name_GrassPelt_ES,
                    Description_ES = Constantes.Description_GrassPelt_ES,
                    Name_IT = Constantes.Name_GrassPelt_IT,
                    Description_IT = Constantes.Description_GrassPelt_IT,
                    Name_DE = Constantes.Name_GrassPelt_DE,
                    Description_DE = Constantes.Description_GrassPelt_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_LongReach_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_LongReach_FR,
                    Description_FR = Constantes.Description_LongReach_FR,
                    Name_EN = Constantes.Name_LongReach_EN,
                    Description_EN = Constantes.Description_LongReach_EN,
                    Name_ES = Constantes.Name_LongReach_ES,
                    Description_ES = Constantes.Description_LongReach_ES,
                    Name_IT = Constantes.Name_LongReach_IT,
                    Description_IT = Constantes.Description_LongReach_IT,
                    Name_DE = Constantes.Name_LongReach_DE,
                    Description_DE = Constantes.Description_LongReach_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_LiquidVoice_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_LiquidVoice_FR,
                    Description_FR = Constantes.Description_LiquidVoice_FR,
                    Name_EN = Constantes.Name_LiquidVoice_EN,
                    Description_EN = Constantes.Description_LiquidVoice_EN,
                    Name_ES = Constantes.Name_LiquidVoice_ES,
                    Description_ES = Constantes.Description_LiquidVoice_ES,
                    Name_IT = Constantes.Name_LiquidVoice_IT,
                    Description_IT = Constantes.Description_LiquidVoice_IT,
                    Name_DE = Constantes.Name_LiquidVoice_DE,
                    Description_DE = Constantes.Description_LiquidVoice_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Libero_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Libero_FR,
                    Description_FR = Constantes.Description_Libero_FR,
                    Name_EN = Constantes.Name_Libero_EN,
                    Description_EN = Constantes.Description_Libero_EN,
                    Name_ES = Constantes.Name_Libero_ES,
                    Description_ES = Constantes.Description_Libero_ES,
                    Name_IT = Constantes.Name_Libero_IT,
                    Description_IT = Constantes.Description_Libero_IT,
                    Name_DE = Constantes.Name_Libero_DE,
                    Description_DE = Constantes.Description_Libero_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_MirrorArmor_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_MirrorArmor_FR,
                    Description_FR = Constantes.Description_MirrorArmor_FR,
                    Name_EN = Constantes.Name_MirrorArmor_EN,
                    Description_EN = Constantes.Description_MirrorArmor_EN,
                    Name_ES = Constantes.Name_MirrorArmor_ES,
                    Description_ES = Constantes.Description_MirrorArmor_ES,
                    Name_IT = Constantes.Name_MirrorArmor_IT,
                    Description_IT = Constantes.Description_MirrorArmor_IT,
                    Name_DE = Constantes.Name_MirrorArmor_DE,
                    Description_DE = Constantes.Description_MirrorArmor_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_PropellerTail_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_PropellerTail_FR,
                    Description_FR = Constantes.Description_PropellerTail_FR,
                    Name_EN = Constantes.Name_PropellerTail_EN,
                    Description_EN = Constantes.Description_PropellerTail_EN,
                    Name_ES = Constantes.Name_PropellerTail_ES,
                    Description_ES = Constantes.Description_PropellerTail_ES,
                    Name_IT = Constantes.Name_PropellerTail_IT,
                    Description_IT = Constantes.Description_PropellerTail_IT,
                    Name_DE = Constantes.Name_PropellerTail_DE,
                    Description_DE = Constantes.Description_PropellerTail_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_SteelySpirit_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_SteelySpirit_FR,
                    Description_FR = Constantes.Description_SteelySpirit_FR,
                    Name_EN = Constantes.Name_SteelySpirit_EN,
                    Description_EN = Constantes.Description_SteelySpirit_EN,
                    Name_ES = Constantes.Name_SteelySpirit_ES,
                    Description_ES = Constantes.Description_SteelySpirit_ES,
                    Name_IT = Constantes.Name_SteelySpirit_IT,
                    Description_IT = Constantes.Description_SteelySpirit_IT,
                    Name_DE = Constantes.Name_SteelySpirit_DE,
                    Description_DE = Constantes.Description_SteelySpirit_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_PerishBody_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_PerishBody_FR,
                    Description_FR = Constantes.Description_PerishBody_FR,
                    Name_EN = Constantes.Name_PerishBody_EN,
                    Description_EN = Constantes.Description_PerishBody_EN,
                    Name_ES = Constantes.Name_PerishBody_ES,
                    Description_ES = Constantes.Description_PerishBody_ES,
                    Name_IT = Constantes.Name_PerishBody_IT,
                    Description_IT = Constantes.Description_PerishBody_IT,
                    Name_DE = Constantes.Name_PerishBody_DE,
                    Description_DE = Constantes.Description_PerishBody_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_IceScales_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_IceScales_FR,
                    Description_FR = Constantes.Description_IceScales_FR,
                    Name_EN = Constantes.Name_IceScales_EN,
                    Description_EN = Constantes.Description_IceScales_EN,
                    Name_ES = Constantes.Name_IceScales_ES,
                    Description_ES = Constantes.Description_IceScales_ES,
                    Name_IT = Constantes.Name_IceScales_IT,
                    Description_IT = Constantes.Description_IceScales_IT,
                    Name_DE = Constantes.Name_IceScales_DE,
                    Description_DE = Constantes.Description_IceScales_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Stalwart_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Stalwart_FR,
                    Description_FR = Constantes.Description_Stalwart_FR,
                    Name_EN = Constantes.Name_Stalwart_EN,
                    Description_EN = Constantes.Description_Stalwart_EN,
                    Name_ES = Constantes.Name_Stalwart_ES,
                    Description_ES = Constantes.Description_Stalwart_ES,
                    Name_IT = Constantes.Name_Stalwart_IT,
                    Description_IT = Constantes.Description_Stalwart_IT,
                    Name_DE = Constantes.Name_Stalwart_DE,
                    Description_DE = Constantes.Description_Stalwart_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Sharpness_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Sharpness_FR,
                    Description_FR = Constantes.Description_Sharpness_FR,
                    Name_EN = Constantes.Name_Sharpness_EN,
                    Description_EN = Constantes.Description_Sharpness_EN,
                    Name_ES = Constantes.Name_Sharpness_ES,
                    Description_ES = Constantes.Description_Sharpness_ES,
                    Name_IT = Constantes.Name_Sharpness_IT,
                    Description_IT = Constantes.Description_Sharpness_IT,
                    Name_DE = Constantes.Name_Sharpness_DE,
                    Description_DE = Constantes.Description_Sharpness_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_RockyPayload_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_RockyPayload_FR,
                    Description_FR = Constantes.Description_RockyPayload_FR,
                    Name_EN = Constantes.Name_RockyPayload_EN,
                    Description_EN = Constantes.Description_RockyPayload_EN,
                    Name_ES = Constantes.Name_RockyPayload_ES,
                    Description_ES = Constantes.Description_RockyPayload_ES,
                    Name_IT = Constantes.Name_RockyPayload_IT,
                    Description_IT = Constantes.Description_RockyPayload_IT,
                    Name_DE = Constantes.Name_RockyPayload_DE,
                    Description_DE = Constantes.Description_RockyPayload_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            ability = (await _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Costar_FR))).FirstOrDefault();
            if (ability == null)
            {
                ability = new()
                {
                    Name_FR = Constantes.Name_Costar_FR,
                    Description_FR = Constantes.Description_Costar_FR,
                    Name_EN = Constantes.Name_Costar_EN,
                    Description_EN = Constantes.Description_Costar_EN,
                    Name_ES = Constantes.Name_Costar_ES,
                    Description_ES = Constantes.Description_Costar_ES,
                    Name_IT = Constantes.Name_Costar_IT,
                    Description_IT = Constantes.Description_Costar_IT,
                    Name_DE = Constantes.Name_Costar_DE,
                    Description_DE = Constantes.Description_Costar_DE
                };
                await _repositoryTL.AddAsync(ability);
            }

            _repositoryTL.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("AddDifficultyInDB")]
        public async Task AddDifficultyInDB()
        {
            List<Difficulty> difficulties = new List<Difficulty>();

            Difficulty difficulty = new();

            difficulty.Code = Constantes.Easy_Code;
            difficulty.Libelle_FR = Constantes.Easy_Libelle_FR;
            difficulty.Libelle_EN = Constantes.Easy_Libelle_EN;
            difficulty.Libelle_ES = Constantes.Easy_Libelle_ES;
            difficulty.Libelle_IT = Constantes.Easy_Libelle_IT;
            difficulty.Libelle_DE = Constantes.Easy_Libelle_DE;
            difficulty.Libelle_RU = Constantes.Easy_Libelle_RU;
            difficulty.Libelle_CO = Constantes.Easy_Libelle_CO;
            difficulty.Libelle_CN = Constantes.Easy_Libelle_CN;
            difficulty.Libelle_JP = Constantes.Easy_Libelle_JP;
            
            if (_repositoryD.Find(m => m.Code.Equals(Constantes.Easy_Code)).Result.Count() == 0)
                difficulties.Add(difficulty);

            difficulty = new Difficulty();
            difficulty.Code = Constantes.Normal_Code;
            difficulty.Libelle_FR = Constantes.Normal_Libelle_FR;
            difficulty.Libelle_EN = Constantes.Normal_Libelle_EN;
            difficulty.Libelle_ES = Constantes.Normal_Libelle_ES;
            difficulty.Libelle_IT = Constantes.Normal_Libelle_IT;
            difficulty.Libelle_DE = Constantes.Normal_Libelle_DE;
            difficulty.Libelle_RU = Constantes.Normal_Libelle_RU;
            difficulty.Libelle_CO = Constantes.Normal_Libelle_CO;
            difficulty.Libelle_CN = Constantes.Normal_Libelle_CN;
            difficulty.Libelle_JP = Constantes.Normal_Libelle_JP;

            if (_repositoryD.Find(m => m.Code.Equals(Constantes.Normal_Code)).Result.Count() == 0)
                difficulties.Add(difficulty);

            difficulty = new Difficulty();
            difficulty.Code = Constantes.Hard_Code;
            difficulty.Libelle_FR = Constantes.Hard_Libelle_FR;
            difficulty.Libelle_EN = Constantes.Hard_Libelle_EN;
            difficulty.Libelle_ES = Constantes.Hard_Libelle_ES;
            difficulty.Libelle_IT = Constantes.Hard_Libelle_IT;
            difficulty.Libelle_DE = Constantes.Hard_Libelle_DE;
            difficulty.Libelle_RU = Constantes.Hard_Libelle_RU;
            difficulty.Libelle_CO = Constantes.Hard_Libelle_CO;
            difficulty.Libelle_CN = Constantes.Hard_Libelle_CN;
            difficulty.Libelle_JP = Constantes.Hard_Libelle_JP;

            if (_repositoryD.Find(m => m.Code.Equals(Constantes.Hard_Code)).Result.Count() == 0)
                difficulties.Add(difficulty);

            difficulty = new Difficulty();
            difficulty.Code = Constantes.Expert_Code;
            difficulty.Libelle_FR = Constantes.Expert_Libelle_FR;
            difficulty.Libelle_EN = Constantes.Expert_Libelle_EN;
            difficulty.Libelle_ES = Constantes.Expert_Libelle_ES;
            difficulty.Libelle_IT = Constantes.Expert_Libelle_IT;
            difficulty.Libelle_DE = Constantes.Expert_Libelle_DE;
            difficulty.Libelle_RU = Constantes.Expert_Libelle_RU;
            difficulty.Libelle_CO = Constantes.Expert_Libelle_CO;
            difficulty.Libelle_CN = Constantes.Expert_Libelle_CN;
            difficulty.Libelle_JP = Constantes.Expert_Libelle_JP;

            if (_repositoryD.Find(m => m.Code.Equals(Constantes.Expert_Code)).Result.Count() == 0)
                difficulties.Add(difficulty);

            await _repositoryD.AddRangeAsync(difficulties);
            _repository.UnitOfWork.SaveChanges();
        }
        
        [HttpPost]
        [Route("AddQuestionTypeInDB")]
        public async Task AddQuestionTypeInDB()
        {
            List<QuestionType> questionTypes = new List<QuestionType>();
            
            QuestionType questionType;
            
            #region Easy
            Difficulty difficultyEasy = await _repositoryD.SingleOrDefault(m => m.Code.Equals(Constantes.Easy_Code));

            #region QTypPok
            questionType = new QuestionType(){
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokDesc
            questionType = new QuestionType()
                {
                Code = Constantes.QTypPokDesc_Code,
                Libelle_FR = Constantes.QTypPokDesc_Libelle_FR,
                Libelle_EN = Constantes.QTypPokDesc_Libelle_EN,
                Libelle_ES = Constantes.QTypPokDesc_Libelle_ES,
                Libelle_IT = Constantes.QTypPokDesc_Libelle_IT,
                Libelle_DE = Constantes.QTypPokDesc_Libelle_DE,
                Libelle_RU = Constantes.QTypPokDesc_Libelle_RU,
                Libelle_CO = Constantes.QTypPokDesc_Libelle_CO,
                Libelle_CN = Constantes.QTypPokDesc_Libelle_CN,
                Libelle_JP = Constantes.QTypPokDesc_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokDesc_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokDescReverse
            questionType = new QuestionType(){
                Code = Constantes.QTypPokDescReverse_Code,
                Libelle_FR = Constantes.QTypPokDescReverse_Libelle_FR,
                Libelle_EN = Constantes.QTypPokDescReverse_Libelle_EN,
                Libelle_ES = Constantes.QTypPokDescReverse_Libelle_ES,
                Libelle_IT = Constantes.QTypPokDescReverse_Libelle_IT,
                Libelle_DE = Constantes.QTypPokDescReverse_Libelle_DE,
                Libelle_RU = Constantes.QTypPokDescReverse_Libelle_RU,
                Libelle_CO = Constantes.QTypPokDescReverse_Libelle_CO,
                Libelle_CN = Constantes.QTypPokDescReverse_Libelle_CN,
                Libelle_JP = Constantes.QTypPokDescReverse_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokDescReverse_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTypPok
            questionType = new QuestionType(){
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 6,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTyp
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTyp_Code,
                Libelle_FR = Constantes.QTypTyp_Libelle_FR,
                Libelle_EN = Constantes.QTypTyp_Libelle_EN,
                Libelle_ES = Constantes.QTypTyp_Libelle_ES,
                Libelle_IT = Constantes.QTypTyp_Libelle_IT,
                Libelle_DE = Constantes.QTypTyp_Libelle_DE,
                Libelle_RU = Constantes.QTypTyp_Libelle_RU,
                Libelle_CO = Constantes.QTypTyp_Libelle_CO,
                Libelle_CN = Constantes.QTypTyp_Libelle_CN,
                Libelle_JP = Constantes.QTypTyp_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 6,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTyp_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTalent
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTalent_Code,
                Libelle_FR = Constantes.QTypTalent_Libelle_FR,
                Libelle_EN = Constantes.QTypTalent_Libelle_EN,
                Libelle_ES = Constantes.QTypTalent_Libelle_ES,
                Libelle_IT = Constantes.QTypTalent_Libelle_IT,
                Libelle_DE = Constantes.QTypTalent_Libelle_DE,
                Libelle_RU = Constantes.QTypTalent_Libelle_RU,
                Libelle_CO = Constantes.QTypTalent_Libelle_CO,
                Libelle_CN = Constantes.QTypTalent_Libelle_CN,
                Libelle_JP = Constantes.QTypTalent_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTalent_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTalentReverse
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTalentReverse_Code,
                Libelle_FR = Constantes.QTypTalentReverse_Libelle_FR,
                Libelle_EN = Constantes.QTypTalentReverse_Libelle_EN,
                Libelle_ES = Constantes.QTypTalentReverse_Libelle_ES,
                Libelle_IT = Constantes.QTypTalentReverse_Libelle_IT,
                Libelle_DE = Constantes.QTypTalentReverse_Libelle_DE,
                Libelle_RU = Constantes.QTypTalentReverse_Libelle_RU,
                Libelle_CO = Constantes.QTypTalentReverse_Libelle_CO,
                Libelle_CN = Constantes.QTypTalentReverse_Libelle_CN,
                Libelle_JP = Constantes.QTypTalentReverse_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTalentReverse_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokStat
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokStat_Code,
                Libelle_FR = Constantes.QTypPokStat_Libelle_FR,
                Libelle_EN = Constantes.QTypPokStat_Libelle_EN,
                Libelle_ES = Constantes.QTypPokStat_Libelle_ES,
                Libelle_IT = Constantes.QTypPokStat_Libelle_IT,
                Libelle_DE = Constantes.QTypPokStat_Libelle_DE,
                Libelle_RU = Constantes.QTypPokStat_Libelle_RU,
                Libelle_CO = Constantes.QTypPokStat_Libelle_CO,
                Libelle_CN = Constantes.QTypPokStat_Libelle_CN,
                Libelle_JP = Constantes.QTypPokStat_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokStat_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTypPokVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 6,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypWeakPokVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 6,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokTalentVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokTalentVarious_Code,
                Libelle_FR = Constantes.QTypPokTalentVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokTalentVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokTalentVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokTalentVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokTalentVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokTalentVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokTalentVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokTalentVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokTalentVarious_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokTalentVarious_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokFamilyVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokFamilyVarious_Code,
                Libelle_FR = Constantes.QTypPokFamilyVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokFamilyVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokFamilyVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokFamilyVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokFamilyVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokFamilyVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokFamilyVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokFamilyVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokFamilyVarious_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokFamilyVarious_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokTypVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokTypVarious_Code,
                Libelle_FR = Constantes.QTypPokTypVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokTypVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokTypVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokTypVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokTypVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokTypVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokTypVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokTypVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokTypVarious_Libelle_JP,
                Difficulty = difficultyEasy,
                NbAnswers = 4,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokTypVarious_Code) && m.Difficulty.Id.Equals(difficultyEasy.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion
            #endregion

            #region Normal
            Difficulty difficultyNormal = await _repositoryD.SingleOrDefault(m => m.Code.Equals(Constantes.Normal_Code));

            #region QTypPok
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && !m.IsBlurred && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyNormal,
                IsBlurred = true,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsBlurred).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyNormal,
                IsHide = true,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokDesc
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokDesc_Code,
                Libelle_FR = Constantes.QTypPokDesc_Libelle_FR,
                Libelle_EN = Constantes.QTypPokDesc_Libelle_EN,
                Libelle_ES = Constantes.QTypPokDesc_Libelle_ES,
                Libelle_IT = Constantes.QTypPokDesc_Libelle_IT,
                Libelle_DE = Constantes.QTypPokDesc_Libelle_DE,
                Libelle_RU = Constantes.QTypPokDesc_Libelle_RU,
                Libelle_CO = Constantes.QTypPokDesc_Libelle_CO,
                Libelle_CN = Constantes.QTypPokDesc_Libelle_CN,
                Libelle_JP = Constantes.QTypPokDesc_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 6,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokDesc_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokDescReverse
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokDescReverse_Code,
                Libelle_FR = Constantes.QTypPokDescReverse_Libelle_FR,
                Libelle_EN = Constantes.QTypPokDescReverse_Libelle_EN,
                Libelle_ES = Constantes.QTypPokDescReverse_Libelle_ES,
                Libelle_IT = Constantes.QTypPokDescReverse_Libelle_IT,
                Libelle_DE = Constantes.QTypPokDescReverse_Libelle_DE,
                Libelle_RU = Constantes.QTypPokDescReverse_Libelle_RU,
                Libelle_CO = Constantes.QTypPokDescReverse_Libelle_CO,
                Libelle_CN = Constantes.QTypPokDescReverse_Libelle_CN,
                Libelle_JP = Constantes.QTypPokDescReverse_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 6,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokDescReverse_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTypPok
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && !m.IsBlurred && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyNormal,
                IsBlurred = true,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsBlurred).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyNormal,
                IsHide = true,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTyp
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTyp_Code,
                Libelle_FR = Constantes.QTypTyp_Libelle_FR,
                Libelle_EN = Constantes.QTypTyp_Libelle_EN,
                Libelle_ES = Constantes.QTypTyp_Libelle_ES,
                Libelle_IT = Constantes.QTypTyp_Libelle_IT,
                Libelle_DE = Constantes.QTypTyp_Libelle_DE,
                Libelle_RU = Constantes.QTypTyp_Libelle_RU,
                Libelle_CO = Constantes.QTypTyp_Libelle_CO,
                Libelle_CN = Constantes.QTypTyp_Libelle_CN,
                Libelle_JP = Constantes.QTypTyp_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTyp_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTalent
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTalent_Code,
                Libelle_FR = Constantes.QTypTalent_Libelle_FR,
                Libelle_EN = Constantes.QTypTalent_Libelle_EN,
                Libelle_ES = Constantes.QTypTalent_Libelle_ES,
                Libelle_IT = Constantes.QTypTalent_Libelle_IT,
                Libelle_DE = Constantes.QTypTalent_Libelle_DE,
                Libelle_RU = Constantes.QTypTalent_Libelle_RU,
                Libelle_CO = Constantes.QTypTalent_Libelle_CO,
                Libelle_CN = Constantes.QTypTalent_Libelle_CN,
                Libelle_JP = Constantes.QTypTalent_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 6,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTalent_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTalentReverse
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTalentReverse_Code,
                Libelle_FR = Constantes.QTypTalentReverse_Libelle_FR,
                Libelle_EN = Constantes.QTypTalentReverse_Libelle_EN,
                Libelle_ES = Constantes.QTypTalentReverse_Libelle_ES,
                Libelle_IT = Constantes.QTypTalentReverse_Libelle_IT,
                Libelle_DE = Constantes.QTypTalentReverse_Libelle_DE,
                Libelle_RU = Constantes.QTypTalentReverse_Libelle_RU,
                Libelle_CO = Constantes.QTypTalentReverse_Libelle_CO,
                Libelle_CN = Constantes.QTypTalentReverse_Libelle_CN,
                Libelle_JP = Constantes.QTypTalentReverse_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 6,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTalentReverse_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokStat
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokStat_Code,
                Libelle_FR = Constantes.QTypPokStat_Libelle_FR,
                Libelle_EN = Constantes.QTypPokStat_Libelle_EN,
                Libelle_ES = Constantes.QTypPokStat_Libelle_ES,
                Libelle_IT = Constantes.QTypPokStat_Libelle_IT,
                Libelle_DE = Constantes.QTypPokStat_Libelle_DE,
                Libelle_RU = Constantes.QTypPokStat_Libelle_RU,
                Libelle_CO = Constantes.QTypPokStat_Libelle_CO,
                Libelle_CN = Constantes.QTypPokStat_Libelle_CN,
                Libelle_JP = Constantes.QTypPokStat_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokStat_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTypPokVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && !m.IsBlurred && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                IsBlurred = true,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsBlurred).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                IsHide = true,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypWeakPokVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && !m.IsBlurred && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                IsBlurred = true,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsBlurred).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                IsHide = true,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokTalentVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokTalentVarious_Code,
                Libelle_FR = Constantes.QTypPokTalentVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokTalentVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokTalentVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokTalentVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokTalentVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokTalentVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokTalentVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokTalentVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokTalentVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 8,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokTalentVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokFamilyVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokFamilyVarious_Code,
                Libelle_FR = Constantes.QTypPokFamilyVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokFamilyVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokFamilyVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokFamilyVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokFamilyVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokFamilyVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokFamilyVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokFamilyVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokFamilyVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 8,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokFamilyVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokTypVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokTypVarious_Code,
                Libelle_FR = Constantes.QTypPokTypVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokTypVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokTypVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokTypVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokTypVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokTypVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokTypVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokTypVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokTypVarious_Libelle_JP,
                Difficulty = difficultyNormal,
                NbAnswers = 8,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokTypVarious_Code) && m.Difficulty.Id.Equals(difficultyNormal.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion
            #endregion

            #region Hard
            Difficulty difficultyHard = await _repositoryD.SingleOrDefault(m => m.Code.Equals(Constantes.Hard_Code));

            #region QTypPok
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && !m.IsBlurred && !m.IsGrayscale && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsGrayscale = true,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsGrayscale).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 12,
                IsHide = true,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypPok_Code,
                Libelle_FR = Constantes.QTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsHide = true,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsHide && m.IsBlurred).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokDesc
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokDesc_Code,
                Libelle_FR = Constantes.QTypPokDesc_Libelle_FR,
                Libelle_EN = Constantes.QTypPokDesc_Libelle_EN,
                Libelle_ES = Constantes.QTypPokDesc_Libelle_ES,
                Libelle_IT = Constantes.QTypPokDesc_Libelle_IT,
                Libelle_DE = Constantes.QTypPokDesc_Libelle_DE,
                Libelle_RU = Constantes.QTypPokDesc_Libelle_RU,
                Libelle_CO = Constantes.QTypPokDesc_Libelle_CO,
                Libelle_CN = Constantes.QTypPokDesc_Libelle_CN,
                Libelle_JP = Constantes.QTypPokDesc_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokDesc_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokDescReverse
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokDescReverse_Code,
                Libelle_FR = Constantes.QTypPokDescReverse_Libelle_FR,
                Libelle_EN = Constantes.QTypPokDescReverse_Libelle_EN,
                Libelle_ES = Constantes.QTypPokDescReverse_Libelle_ES,
                Libelle_IT = Constantes.QTypPokDescReverse_Libelle_IT,
                Libelle_DE = Constantes.QTypPokDescReverse_Libelle_DE,
                Libelle_RU = Constantes.QTypPokDescReverse_Libelle_RU,
                Libelle_CO = Constantes.QTypPokDescReverse_Libelle_CO,
                Libelle_CN = Constantes.QTypPokDescReverse_Libelle_CN,
                Libelle_JP = Constantes.QTypPokDescReverse_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokDescReverse_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTypPok
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 18,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && !m.IsBlurred && !m.IsGrayscale && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsGrayscale = true,
                NbAnswers = 18,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsGrayscale).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                IsHide = true,
                NbAnswers = 18,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPok_Code,
                Libelle_FR = Constantes.QTypTypPok_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPok_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPok_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPok_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPok_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPok_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPok_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPok_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPok_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsHide = true,
                NbAnswers = 18,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPok_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTyp
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTyp_Code,
                Libelle_FR = Constantes.QTypTyp_Libelle_FR,
                Libelle_EN = Constantes.QTypTyp_Libelle_EN,
                Libelle_ES = Constantes.QTypTyp_Libelle_ES,
                Libelle_IT = Constantes.QTypTyp_Libelle_IT,
                Libelle_DE = Constantes.QTypTyp_Libelle_DE,
                Libelle_RU = Constantes.QTypTyp_Libelle_RU,
                Libelle_CO = Constantes.QTypTyp_Libelle_CO,
                Libelle_CN = Constantes.QTypTyp_Libelle_CN,
                Libelle_JP = Constantes.QTypTyp_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 18,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTyp_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTalent
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTalent_Code,
                Libelle_FR = Constantes.QTypTalent_Libelle_FR,
                Libelle_EN = Constantes.QTypTalent_Libelle_EN,
                Libelle_ES = Constantes.QTypTalent_Libelle_ES,
                Libelle_IT = Constantes.QTypTalent_Libelle_IT,
                Libelle_DE = Constantes.QTypTalent_Libelle_DE,
                Libelle_RU = Constantes.QTypTalent_Libelle_RU,
                Libelle_CO = Constantes.QTypTalent_Libelle_CO,
                Libelle_CN = Constantes.QTypTalent_Libelle_CN,
                Libelle_JP = Constantes.QTypTalent_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTalent_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTalentReverse
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTalentReverse_Code,
                Libelle_FR = Constantes.QTypTalentReverse_Libelle_FR,
                Libelle_EN = Constantes.QTypTalentReverse_Libelle_EN,
                Libelle_ES = Constantes.QTypTalentReverse_Libelle_ES,
                Libelle_IT = Constantes.QTypTalentReverse_Libelle_IT,
                Libelle_DE = Constantes.QTypTalentReverse_Libelle_DE,
                Libelle_RU = Constantes.QTypTalentReverse_Libelle_RU,
                Libelle_CO = Constantes.QTypTalentReverse_Libelle_CO,
                Libelle_CN = Constantes.QTypTalentReverse_Libelle_CN,
                Libelle_JP = Constantes.QTypTalentReverse_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 8,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTalentReverse_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokStat
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokStat_Code,
                Libelle_FR = Constantes.QTypPokStat_Libelle_FR,
                Libelle_EN = Constantes.QTypPokStat_Libelle_EN,
                Libelle_ES = Constantes.QTypPokStat_Libelle_ES,
                Libelle_IT = Constantes.QTypPokStat_Libelle_IT,
                Libelle_DE = Constantes.QTypPokStat_Libelle_DE,
                Libelle_RU = Constantes.QTypPokStat_Libelle_RU,
                Libelle_CO = Constantes.QTypPokStat_Libelle_CO,
                Libelle_CN = Constantes.QTypPokStat_Libelle_CN,
                Libelle_JP = Constantes.QTypPokStat_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 12,
                NbAnswersPossible = 1
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokStat_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypTypPokVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && !m.IsBlurred && !m.IsGrayscale && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsGrayscale = true,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsGrayscale).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                IsHide = true,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypTypPokVarious_Code,
                Libelle_FR = Constantes.QTypTypPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypTypPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypTypPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypTypPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypTypPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypTypPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypTypPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypTypPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypTypPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsHide = true,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypTypPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypWeakPokVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && !m.IsBlurred && !m.IsGrayscale && !m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsGrayscale = true,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsGrayscale).Result.Count() == 0)
                questionTypes.Add(questionType);
            
            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                IsHide = true,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);

            questionType = new QuestionType()
            {
                Code = Constantes.QTypWeakPokVarious_Code,
                Libelle_FR = Constantes.QTypWeakPokVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypWeakPokVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypWeakPokVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypWeakPokVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypWeakPokVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypWeakPokVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypWeakPokVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypWeakPokVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypWeakPokVarious_Libelle_JP,
                Difficulty = difficultyHard,
                IsBlurred = true,
                IsHide = true,
                NbAnswers = 18,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypWeakPokVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id) && m.IsBlurred && m.IsHide).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokTalentVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokTalentVarious_Code,
                Libelle_FR = Constantes.QTypPokTalentVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokTalentVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokTalentVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokTalentVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokTalentVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokTalentVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokTalentVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokTalentVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokTalentVarious_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokTalentVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokFamilyVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokFamilyVarious_Code,
                Libelle_FR = Constantes.QTypPokFamilyVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokFamilyVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokFamilyVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokFamilyVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokFamilyVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokFamilyVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokFamilyVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokFamilyVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokFamilyVarious_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokFamilyVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion

            #region QTypPokTypVarious
            questionType = new QuestionType()
            {
                Code = Constantes.QTypPokTypVarious_Code,
                Libelle_FR = Constantes.QTypPokTypVarious_Libelle_FR,
                Libelle_EN = Constantes.QTypPokTypVarious_Libelle_EN,
                Libelle_ES = Constantes.QTypPokTypVarious_Libelle_ES,
                Libelle_IT = Constantes.QTypPokTypVarious_Libelle_IT,
                Libelle_DE = Constantes.QTypPokTypVarious_Libelle_DE,
                Libelle_RU = Constantes.QTypPokTypVarious_Libelle_RU,
                Libelle_CO = Constantes.QTypPokTypVarious_Libelle_CO,
                Libelle_CN = Constantes.QTypPokTypVarious_Libelle_CN,
                Libelle_JP = Constantes.QTypPokTypVarious_Libelle_JP,
                Difficulty = difficultyHard,
                NbAnswers = 12,
                IsMultipleAnswers = true
            };

            if (_repositoryQT.Find(m => m.Code.Equals(Constantes.QTypPokTypVarious_Code) && m.Difficulty.Id.Equals(difficultyHard.Id)).Result.Count() == 0)
                questionTypes.Add(questionType);
            #endregion
            #endregion

            await _repositoryQT.AddRangeAsync(questionTypes);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("SaveTypeScrapInDB")]
        public void SaveTypeScrapInDB()
        {
            string json;
            using (StreamReader sr = new StreamReader("TypeScrap.json"))
            {
                json = sr.ReadToEnd();
                _repositoryTP.SaveJsonInDb(json);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("DlUpdateTypePokPathUrl")]
        public async Task DlUpdateTypePokPathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<TypePok> typesPok = await _repositoryTP.GetAll();
            foreach (TypePok typePok in typesPok)
            {
                typePok.PathMiniGo = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlMiniGo, typePok.Name_EN + "_" + Constantes.MiniGo, Constantes.MiniGo);
                typePok.PathFondGo = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlFondGo, typePok.Name_EN + "_" + Constantes.FondGo, Constantes.FondGo);
                typePok.PathMiniHome_FR = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlMiniHome, typePok.Name_EN + "_" + Constantes.MiniHome, Constantes.MiniHome);
                typePok.PathMiniHome_EN = "Content/Types/MiniHome/EN/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_ES = "Content/Types/MiniHome/ES/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_IT = "Content/Types/MiniHome/IT/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_DE = "Content/Types/MiniHome/DE/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_RU = "Content/Types/MiniHome/RU/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_CO = "Content/Types/MiniHome/CO/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_CN = "Content/Types/MiniHome/CN/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathMiniHome_JP = "Content/Types/MiniHome/JP/" + typePok.Name_EN + "_" + Constantes.MiniHome + ".png";
                typePok.PathIconHome = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlIconHome, typePok.Name_EN + "_" + Constantes.IconHome, Constantes.IconHome);
                typePok.PathAutoHome = await HttpClientUtils.DownloadFileTaskAsync(httpClient, typePok.UrlAutoHome, typePok.Name_EN + "_" + Constantes.AutoHome, Constantes.AutoHome);
            }

            _repositoryTP.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpPut]
        [Route("UpdateTalent")]
        public async Task UpdateTalent()
        {
            IEnumerable<Ability> abilitiesDB = await _repositoryTL.GetAll();
            List<Ability> abilities = abilitiesDB.ToList();
            List<Ability> newTalents = new List<Ability>();
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.ToList())
            {
                if (pokemon.FR.Talent != null)
                {
                    int count = pokemon.FR.Talent.Split(",").Length;

                    for (int i = 0; i < count; i++)
                    {
                        Ability ability = new Ability();
                        #region FR
                        if (pokemon.FR.Talent != null)
                        {
                            string[] Name = pokemon.FR.Talent.Split(",");
                            string[] Description = pokemon.FR.DescriptionTalent.Split(";");

                            ability.Name_FR = Name[i];
                            ability.Description_FR = Description[i];
                        }
                        #endregion

                        #region EN
                        if (pokemon.EN.Talent != null)
                        {
                            string[] Name = pokemon.EN.Talent.Split(",");
                            string[] Description = pokemon.EN.DescriptionTalent.Split(";");

                            ability.Name_EN = Name[i];
                            ability.Description_EN = Description[i];
                        }
                        #endregion

                        #region ES
                        if (pokemon.ES.Talent != null)
                        {
                            string[] Name = pokemon.ES.Talent.Split(",");
                            string[] Description = pokemon.ES.DescriptionTalent.Split(";");

                            ability.Name_ES = Name[i];
                            ability.Description_ES = Description[i];
                        }
                        #endregion

                        #region IT
                        if (pokemon.IT.Talent != null)
                        {
                            string[] Name = pokemon.IT.Talent.Split(",");
                            string[] Description = pokemon.IT.DescriptionTalent.Split(";");

                            ability.Name_IT = Name[i];
                            ability.Description_IT = Description[i];
                        }
                        #endregion

                        #region DE
                        if (pokemon.DE.Talent != null)
                        {
                            string[] Name = pokemon.DE.Talent.Split(",");
                            string[] Description = pokemon.DE.DescriptionTalent.Split(";");

                            ability.Name_DE = Name[i];
                            ability.Description_DE = Description[i];
                        }
                        #endregion

                        #region RU
                        if (pokemon.RU.Talent != null && pokemon.RU.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.RU.Talent.Split(",");
                            string[] Description = pokemon.RU.DescriptionTalent.Split(";");

                            ability.Name_RU = Name[i];
                            ability.Description_RU = Description[i];
                        }
                        #endregion

                        #region CO
                        if (pokemon.CO.Talent != null && pokemon.CO.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.CO.Talent.Split(",");
                            string[] Description = pokemon.CO.DescriptionTalent.Split(";");

                            ability.Name_CO = Name[i];
                            ability.Description_CO = Description[i];
                        }
                        #endregion

                        #region CN
                        if (pokemon.CN.Talent != null && pokemon.CN.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.CN.Talent.Split(",");
                            string[] Description = pokemon.CN.DescriptionTalent.Split(";");

                            ability.Name_CN = Name[i];
                            ability.Description_CN = Description[i];
                        }
                        #endregion

                        #region JP
                        if (pokemon.JP.Talent != null && pokemon.JP.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.JP.Talent.Split(",");
                            string[] Description = pokemon.JP.DescriptionTalent.Split(";");

                            ability.Name_JP = Name[i];
                            ability.Description_JP = Description[i];
                        }
                        #endregion

                        Ability abilityExist = abilities.Find(x => x.Name_FR == ability.Name_FR);
                        if (abilityExist == null)
                        {
                            Ability newTalentsExist = newTalents.Find(x => x.Name_FR == ability.Name_FR);
                            if (newTalentsExist == null)
                            {
                                newTalents.Add(ability);
                                Console.WriteLine(ability.Name_FR + ": " + ability.Description_FR);
                            }
                        }
                    }
                }
            }

            await _repositoryTL.AddRangeAsync(newTalents);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateGameInDB")]
        public async Task UpdateGameInDB()
        {
            Game game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.RedBlue_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.RedBlue_Name_FR;
            game.Name_EN = Constantes.RedBlue_Name_EN;
            game.Name_ES = Constantes.RedBlue_Name_ES;
            game.Name_IT = Constantes.RedBlue_Name_IT;
            game.Name_DE = Constantes.RedBlue_Name_DE;
            game.Name_RU = Constantes.RedBlue_Name_RU;
            game.Name_CO = Constantes.RedBlue_Name_CO;
            game.Name_CN = Constantes.RedBlue_Name_CN;
            game.Name_JP = Constantes.RedBlue_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Yellow_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.Yellow_Name_FR;
            game.Name_EN = Constantes.Yellow_Name_EN;
            game.Name_ES = Constantes.Yellow_Name_ES;
            game.Name_IT = Constantes.Yellow_Name_IT;
            game.Name_DE = Constantes.Yellow_Name_DE;
            game.Name_RU = Constantes.Yellow_Name_RU;
            game.Name_CO = Constantes.Yellow_Name_CO;
            game.Name_CN = Constantes.Yellow_Name_CN;
            game.Name_JP = Constantes.Yellow_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.GoldSilver_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.GoldSilver_Name_FR;
            game.Name_EN = Constantes.GoldSilver_Name_EN;
            game.Name_ES = Constantes.GoldSilver_Name_ES;
            game.Name_IT = Constantes.GoldSilver_Name_IT;
            game.Name_DE = Constantes.GoldSilver_Name_DE;
            game.Name_RU = Constantes.GoldSilver_Name_RU;
            game.Name_CO = Constantes.GoldSilver_Name_CO;
            game.Name_CN = Constantes.GoldSilver_Name_CN;
            game.Name_JP = Constantes.GoldSilver_Name_JP;
            await _repositoryG.UpdateAsync (game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Crystal_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.Crystal_Name_FR;
            game.Name_EN = Constantes.Crystal_Name_EN;
            game.Name_ES = Constantes.Crystal_Name_ES;
            game.Name_IT = Constantes.Crystal_Name_IT;
            game.Name_DE = Constantes.Crystal_Name_DE;
            game.Name_RU = Constantes.Crystal_Name_RU;
            game.Name_CO = Constantes.Crystal_Name_CO;
            game.Name_CN = Constantes.Crystal_Name_CN;
            game.Name_JP = Constantes.Crystal_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.RubySapphire_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.RubySapphire_Name_FR;
            game.Name_EN = Constantes.RubySapphire_Name_EN;
            game.Name_ES = Constantes.RubySapphire_Name_ES;
            game.Name_IT = Constantes.RubySapphire_Name_IT;
            game.Name_DE = Constantes.RubySapphire_Name_DE;
            game.Name_RU = Constantes.RubySapphire_Name_RU;
            game.Name_CO = Constantes.RubySapphire_Name_CO;
            game.Name_CN = Constantes.RubySapphire_Name_CN;
            game.Name_JP = Constantes.RubySapphire_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Emerald_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.Emerald_Name_FR;
            game.Name_EN = Constantes.Emerald_Name_EN;
            game.Name_ES = Constantes.Emerald_Name_ES;
            game.Name_IT = Constantes.Emerald_Name_IT;
            game.Name_DE = Constantes.Emerald_Name_DE;
            game.Name_RU = Constantes.Emerald_Name_RU;
            game.Name_CO = Constantes.Emerald_Name_CO;
            game.Name_CN = Constantes.Emerald_Name_CN;
            game.Name_JP = Constantes.Emerald_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.FireRedLeafGreen_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.FireRedLeafGreen_Name_FR;
            game.Name_EN = Constantes.FireRedLeafGreen_Name_EN;
            game.Name_ES = Constantes.FireRedLeafGreen_Name_ES;
            game.Name_IT = Constantes.FireRedLeafGreen_Name_IT;
            game.Name_DE = Constantes.FireRedLeafGreen_Name_DE;
            game.Name_RU = Constantes.FireRedLeafGreen_Name_RU;
            game.Name_CO = Constantes.FireRedLeafGreen_Name_CO;
            game.Name_CN = Constantes.FireRedLeafGreen_Name_CN;
            game.Name_JP = Constantes.FireRedLeafGreen_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.DiamondPearl_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.DiamondPearl_Name_FR;
            game.Name_EN = Constantes.DiamondPearl_Name_EN;
            game.Name_ES = Constantes.DiamondPearl_Name_ES;
            game.Name_IT = Constantes.DiamondPearl_Name_IT;
            game.Name_DE = Constantes.DiamondPearl_Name_DE;
            game.Name_RU = Constantes.DiamondPearl_Name_RU;
            game.Name_CO = Constantes.DiamondPearl_Name_CO;
            game.Name_CN = Constantes.DiamondPearl_Name_CN;
            game.Name_JP = Constantes.DiamondPearl_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Platinum_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.Platinum_Name_FR;
            game.Name_EN = Constantes.Platinum_Name_EN;
            game.Name_ES = Constantes.Platinum_Name_ES;
            game.Name_IT = Constantes.Platinum_Name_IT;
            game.Name_DE = Constantes.Platinum_Name_DE;
            game.Name_RU = Constantes.Platinum_Name_RU;
            game.Name_CO = Constantes.Platinum_Name_CO;
            game.Name_CN = Constantes.Platinum_Name_CN;
            game.Name_JP = Constantes.Platinum_Name_JP;
            await _repositoryG.UpdateAsync(game);

            game = (await _repositoryG.Find(m => m.Name_FR.Equals(Constantes.HeartGoldSoulSilver_Name_FR))).FirstOrDefault();
            game.Name_FR = Constantes.HeartGoldSoulSilver_Name_FR;
            game.Name_EN = Constantes.HeartGoldSoulSilver_Name_EN;
            game.Name_ES = Constantes.HeartGoldSoulSilver_Name_ES;
            game.Name_IT = Constantes.HeartGoldSoulSilver_Name_IT;
            game.Name_DE = Constantes.HeartGoldSoulSilver_Name_DE;
            game.Name_RU = Constantes.HeartGoldSoulSilver_Name_RU;
            game.Name_CO = Constantes.HeartGoldSoulSilver_Name_CO;
            game.Name_CN = Constantes.HeartGoldSoulSilver_Name_CN;
            game.Name_JP = Constantes.HeartGoldSoulSilver_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.BlackWhite_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.BlackWhite_Name_FR;
            game.Name_EN = Constantes.BlackWhite_Name_EN;
            game.Name_ES = Constantes.BlackWhite_Name_ES;
            game.Name_IT = Constantes.BlackWhite_Name_IT;
            game.Name_DE = Constantes.BlackWhite_Name_DE;
            game.Name_RU = Constantes.BlackWhite_Name_RU;
            game.Name_CO = Constantes.BlackWhite_Name_CO;
            game.Name_CN = Constantes.BlackWhite_Name_CN;
            game.Name_JP = Constantes.BlackWhite_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Black2White2_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Black2White2_Name_FR;
            game.Name_EN = Constantes.Black2White2_Name_EN;
            game.Name_ES = Constantes.Black2White2_Name_ES;
            game.Name_IT = Constantes.Black2White2_Name_IT;
            game.Name_DE = Constantes.Black2White2_Name_DE;
            game.Name_RU = Constantes.Black2White2_Name_RU;
            game.Name_CO = Constantes.Black2White2_Name_CO;
            game.Name_CN = Constantes.Black2White2_Name_CN;
            game.Name_JP = Constantes.Black2White2_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.X_Y_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.X_Y_Name_FR;
            game.Name_EN = Constantes.X_Y_Name_EN;
            game.Name_ES = Constantes.X_Y_Name_ES;
            game.Name_IT = Constantes.X_Y_Name_IT;
            game.Name_DE = Constantes.X_Y_Name_DE;
            game.Name_RU = Constantes.X_Y_Name_RU;
            game.Name_CO = Constantes.X_Y_Name_CO;
            game.Name_CN = Constantes.X_Y_Name_CN;
            game.Name_JP = Constantes.X_Y_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.SunMoon_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.SunMoon_Name_FR;
            game.Name_EN = Constantes.SunMoon_Name_EN;
            game.Name_ES = Constantes.SunMoon_Name_ES;
            game.Name_IT = Constantes.SunMoon_Name_IT;
            game.Name_DE = Constantes.SunMoon_Name_DE;
            game.Name_RU = Constantes.SunMoon_Name_RU;
            game.Name_CO = Constantes.SunMoon_Name_CO;
            game.Name_CN = Constantes.SunMoon_Name_CN;
            game.Name_JP = Constantes.SunMoon_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.UltraSunUltraMoon_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.UltraSunUltraMoon_Name_FR;
            game.Name_EN = Constantes.UltraSunUltraMoon_Name_EN;
            game.Name_ES = Constantes.UltraSunUltraMoon_Name_ES;
            game.Name_IT = Constantes.UltraSunUltraMoon_Name_IT;
            game.Name_DE = Constantes.UltraSunUltraMoon_Name_DE;
            game.Name_RU = Constantes.UltraSunUltraMoon_Name_RU;
            game.Name_CO = Constantes.UltraSunUltraMoon_Name_CO;
            game.Name_CN = Constantes.UltraSunUltraMoon_Name_CN;
            game.Name_JP = Constantes.UltraSunUltraMoon_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.LetsGoPikachuEvoli_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.LetsGoPikachuEvoli_Name_FR;
            game.Name_EN = Constantes.LetsGoPikachuEvoli_Name_EN;
            game.Name_ES = Constantes.LetsGoPikachuEvoli_Name_ES;
            game.Name_IT = Constantes.LetsGoPikachuEvoli_Name_IT;
            game.Name_DE = Constantes.LetsGoPikachuEvoli_Name_DE;
            game.Name_RU = Constantes.LetsGoPikachuEvoli_Name_RU;
            game.Name_CO = Constantes.LetsGoPikachuEvoli_Name_CO;
            game.Name_CN = Constantes.LetsGoPikachuEvoli_Name_CN;
            game.Name_JP = Constantes.LetsGoPikachuEvoli_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.SwordShield_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.SwordShield_Name_FR;
            game.Name_EN = Constantes.SwordShield_Name_EN;
            game.Name_ES = Constantes.SwordShield_Name_ES;
            game.Name_IT = Constantes.SwordShield_Name_IT;
            game.Name_DE = Constantes.SwordShield_Name_DE;
            game.Name_RU = Constantes.SwordShield_Name_RU;
            game.Name_CO = Constantes.SwordShield_Name_CO;
            game.Name_CN = Constantes.SwordShield_Name_CN;
            game.Name_JP = Constantes.SwordShield_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.ShiningDiamondShiningPearl_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.ShiningDiamondShiningPearl_Name_FR;
            game.Name_EN = Constantes.ShiningDiamondShiningPearl_Name_EN;
            game.Name_ES = Constantes.ShiningDiamondShiningPearl_Name_ES;
            game.Name_IT = Constantes.ShiningDiamondShiningPearl_Name_IT;
            game.Name_DE = Constantes.ShiningDiamondShiningPearl_Name_DE;
            game.Name_RU = Constantes.ShiningDiamondShiningPearl_Name_RU;
            game.Name_CO = Constantes.ShiningDiamondShiningPearl_Name_CO;
            game.Name_CN = Constantes.ShiningDiamondShiningPearl_Name_CN;
            game.Name_JP = Constantes.ShiningDiamondShiningPearl_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Arceus_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Arceus_Name_FR;
            game.Name_EN = Constantes.Arceus_Name_EN;
            game.Name_ES = Constantes.Arceus_Name_ES;
            game.Name_IT = Constantes.Arceus_Name_IT;
            game.Name_DE = Constantes.Arceus_Name_DE;
            game.Name_RU = Constantes.Arceus_Name_RU;
            game.Name_CO = Constantes.Arceus_Name_CO;
            game.Name_CN = Constantes.Arceus_Name_CN;
            game.Name_JP = Constantes.Arceus_Name_JP;
            _repositoryG.UpdateAsync(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.ScarletViolet_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.ScarletViolet_Name_FR;
            game.Name_EN = Constantes.ScarletViolet_Name_EN;
            game.Name_ES = Constantes.ScarletViolet_Name_ES;
            game.Name_IT = Constantes.ScarletViolet_Name_IT;
            game.Name_DE = Constantes.ScarletViolet_Name_DE;
            game.Name_RU = Constantes.ScarletViolet_Name_RU;
            game.Name_CO = Constantes.ScarletViolet_Name_CO;
            game.Name_CN = Constantes.ScarletViolet_Name_CN;
            game.Name_JP = Constantes.ScarletViolet_Name_JP;
            _repositoryG.UpdateAsync(game);

            _repositoryG.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateTypePokInDB")]
        public async Task UpdateTypePokInDB()
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.ToList())
            {
                List<Pokemon_TypePok> pokemon_TypePoks = new();

                foreach (string type in pokemon.FR.Types.Split(','))
                {
                    TypePok typePok = await _repositoryTP.SingleOrDefault(x => x.Name_FR.Equals(type));
                    Pokemon_TypePok pokemon_TypePok = new()
                    {
                        PokemonId = pokemon.Id,
                        TypePokId = typePok.Id
                    };

                    pokemon_TypePoks.Add(pokemon_TypePok);
                }

                _repositoryPTP.AddRangeAsync(pokemon_TypePoks);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateWeaknessInDB")]
        public async Task UpdateWeaknessInDB()
        {
            try
            {
                List<Pokemon> pokemons = _repository.GetAll().Result.ToList();
                foreach (Pokemon pokemon in pokemons)
                {
                    List<Pokemon_Weakness> pokemon_Weaknesses = new();

                    foreach (string weakness in pokemon.FR.Weakness.Split(','))
                    {
                        TypePok typePok = await _repositoryTP.SingleOrDefault(m => m.Name_FR.Equals(weakness));
                        Pokemon_Weakness pokemon_Weakness = new()
                        {
                            PokemonId = pokemon.Id,
                            TypePokId = typePok.Id
                        };

                        pokemon_Weaknesses.Add(pokemon_Weakness);
                    }

                    await _repositoryPWN.AddRangeAsync(pokemon_Weaknesses);
                }

                _repository.UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateTalentInDB")]
        public async Task UpdateTalentInDB()
        {
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.ToList())
            {
                List<Pokemon_Ability> pokemon_Abilities = new();

                foreach (string type in pokemon.FR.Talent.Split(','))
                {
                    Ability abilityPok = await _repositoryTL.SingleOrDefault(x => x.Name_FR.Equals(type));
                    Pokemon_Ability pokemon_Ability = new()
                    {
                        PokemonId = pokemon.Id,
                        AbilityId = abilityPok.Id
                    };

                    pokemon_Abilities.Add(pokemon_Ability);
                }

                _repositoryPTL.AddRangeAsync(pokemon_Abilities);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateTypeAttaqueInDB")]
        public async Task UpdateTypeAttaqueInDB()
        {
            List<TypeAttack> typeAttacks = new List<TypeAttack>();

            TypeAttack typeAttack = new TypeAttack();
            typeAttack.Name_FR = Constantes.Physical_Name_FR;
            typeAttack.Description_FR = Constantes.Physical_Description_FR;
            typeAttack.Name_EN = Constantes.Physical_Name_EN;
            typeAttack.Description_EN = Constantes.Physical_Description_EN;
            typeAttack.Name_ES = Constantes.Physical_Name_ES;
            typeAttack.Description_ES = Constantes.Physical_Description_ES;
            typeAttack.Name_IT = Constantes.Physical_Name_IT;
            typeAttack.Description_IT = Constantes.Physical_Description_IT;
            typeAttack.Name_DE = Constantes.Physical_Name_DE;
            typeAttack.Description_DE = Constantes.Physical_Description_DE;
            typeAttack.Name_RU = Constantes.Physical_Name_RU;
            typeAttack.Description_RU = Constantes.Physical_Description_RU;
            typeAttack.Name_CO = Constantes.Physical_Name_CO;
            typeAttack.Description_CO = Constantes.Physical_Description_CO;
            typeAttack.Name_CN = Constantes.Physical_Name_CN;
            typeAttack.Description_CN = Constantes.Physical_Description_CN;
            typeAttack.Name_JP = Constantes.Physical_Name_JP;
            typeAttack.Description_JP = Constantes.Physical_Description_JP;
            typeAttack.UrlImg = Constantes.Physical_UrlImg;
            if (_repositoryTA.Find(m => m.Name_FR.Equals(typeAttack.Name_FR)).Result.Count() == 0)
                typeAttacks.Add(typeAttack);

            typeAttack = new TypeAttack();
            typeAttack.Name_FR = Constantes.Special_Name_FR;
            typeAttack.Description_FR = Constantes.Special_Description_FR;
            typeAttack.Name_EN = Constantes.Special_Name_EN;
            typeAttack.Description_EN = Constantes.Special_Description_EN;
            typeAttack.Name_ES = Constantes.Special_Name_ES;
            typeAttack.Description_ES = Constantes.Special_Description_ES;
            typeAttack.Name_IT = Constantes.Special_Name_IT;
            typeAttack.Description_IT = Constantes.Special_Description_IT;
            typeAttack.Name_DE = Constantes.Special_Name_DE;
            typeAttack.Description_DE = Constantes.Special_Description_DE;
            typeAttack.Name_RU = Constantes.Special_Name_RU;
            typeAttack.Description_RU = Constantes.Special_Description_RU;
            typeAttack.Name_CO = Constantes.Special_Name_CO;
            typeAttack.Description_CO = Constantes.Special_Description_CO;
            typeAttack.Name_CN = Constantes.Special_Name_CN;
            typeAttack.Description_CN = Constantes.Special_Description_CN;
            typeAttack.Name_JP = Constantes.Special_Name_JP;
            typeAttack.Description_JP = Constantes.Special_Description_JP;
            typeAttack.UrlImg = Constantes.Special_UrlImg;
            if (_repositoryTA.Find(m => m.Name_FR.Equals(typeAttack.Name_FR)).Result.Count() == 0)
                typeAttacks.Add(typeAttack);

            typeAttack = new TypeAttack();
            typeAttack.Name_FR = Constantes.Status_Name_FR;
            typeAttack.Description_FR = Constantes.Status_Description_FR;
            typeAttack.Name_EN = Constantes.Status_Name_EN;
            typeAttack.Description_EN = Constantes.Status_Description_EN;
            typeAttack.Name_ES = Constantes.Status_Name_ES;
            typeAttack.Description_ES = Constantes.Status_Description_ES;
            typeAttack.Name_IT = Constantes.Status_Name_IT;
            typeAttack.Description_IT = Constantes.Status_Description_IT;
            typeAttack.Name_DE = Constantes.Status_Name_DE;
            typeAttack.Description_DE = Constantes.Status_Description_DE;
            typeAttack.Name_RU = Constantes.Status_Name_RU;
            typeAttack.Description_RU = Constantes.Status_Description_RU;
            typeAttack.Name_CO = Constantes.Status_Name_CO;
            typeAttack.Description_CO = Constantes.Status_Description_CO;
            typeAttack.Name_CN = Constantes.Status_Name_CN;
            typeAttack.Description_CN = Constantes.Status_Description_CN;
            typeAttack.Name_JP = Constantes.Status_Name_JP;
            typeAttack.Description_JP = Constantes.Status_Description_JP;
            typeAttack.UrlImg = Constantes.Status_UrlImg;
            if (_repositoryTA.Find(m => m.Name_FR.Equals(typeAttack.Name_FR)).Result.Count() == 0)
                typeAttacks.Add(typeAttack);

            await _repositoryTA.AddRangeAsync(typeAttacks);
            _repositoryTA.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("DlUpdateTypeAttaquePathUrl")]
        public async Task DlUpdateTypeAttaquePathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<TypeAttack> typeAttacks = await _repositoryTA.GetAll();
            foreach (TypeAttack typeAttack in typeAttacks)
            {
                StringBuilder nameBuilder = new StringBuilder();
                switch (typeAttack.Name_FR)
                {
                    case Constantes.Physical_Name_FR:
                        nameBuilder.Append(Constantes.Physical);
                        break;
                    case Constantes.Special_Name_FR:
                        nameBuilder.Append(Constantes.Special);
                        break;
                    case Constantes.Status_Name_FR:
                        nameBuilder.Append(Constantes.Status);
                        break;
                }
                typeAttack.PathImg = await HttpClientUtils.DownloadTypeAttackFileTaskAsync(httpClient, typeAttack.UrlImg, nameBuilder.ToString());
            }

            _repositoryTA.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpPut]
        [Route("DlUpdatePokemonPathUrl")]
        public async Task DlUpdatePokemonPathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.Where(m => m.PathImgLegacy == null))
            {
                pokemon.PathImgLegacy = await HttpClientUtils.DownloadFileTaskAsync(httpClient, pokemon.UrlImg, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation);
                pokemon.PathSpriteLegacy = await HttpClientUtils.DownloadFileTaskAsync(httpClient, pokemon.UrlSprite, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation, true);
            }

            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpPut]
        [Route("UpdateSprite")]
        public async Task UpdateSprite()
        {
            string response = HttpClientUtils.CallUrl(Constantes.urlAllSprites).Result;
            ScrapingDataUtils.GetUrlsMini(response, _repository);
        }

        [HttpPut]
        [Route("UpdateAnimatedImg")]
        public async Task UpdateAnimatedImg()
        {
            var pokemons = (await _repository.GetAll()).ToList();

            foreach (var pokemon in pokemons)
            {
                pokemon.PathAnimatedImg =
                    $"Content/AnimatedImages/G{pokemon.Generation}/Normal/{pokemon.EN.Name.Replace(" ", "_")}.gif";
                pokemon.PathAnimatedImgShiny =
                    $"Content/AnimatedImages/G{pokemon.Generation}/Shiny/{pokemon.EN.Name.Replace(" ", "_")}.gif";
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdatePathPokeApi")]
        public async Task UpdatePathPokeApi()
        {
            var pokemons = (await _repository.GetAll()).ToList();

            foreach (var pokemon in pokemons)
            {
                pokemon.PathImgNormal =
                    $"Content/Images/G{pokemon.Generation}/Normal/{pokemon.EN.Name.Replace(" ", "_")}.png";
                pokemon.PathImgShiny =
                    $"Content/Images/G{pokemon.Generation}/Shiny/{pokemon.EN.Name.Replace(" ", "_")}.png";
                pokemon.PathSpriteNormal =
                    $"Content/Sprites/G{pokemon.Generation}/Normal/{pokemon.EN.Name.Replace(" ", "_")}.png";
                pokemon.PathSpriteShiny =
                    $"Content/Sprites/G{pokemon.Generation}/Shiny/{pokemon.EN.Name.Replace(" ", "_")}.png";
                pokemon.PathSoundLegacy =
                    $"Content/Sound/G{pokemon.Generation}/Normal/{pokemon.EN.Name.Replace(" ", "_")}.ogg";
                pokemon.PathSoundCurrent =
                    $"Content/Sound/G{pokemon.Generation}/Shiny/{pokemon.EN.Name.Replace(" ", "_")}.ogg";
            }

            _repository.UnitOfWork.SaveChanges();
        }


        [HttpPut]
        [Route("DlUpdatePathUrlSound")]
        public async Task DlUpdatePathUrlSound()
        {
            var httpClient = new HttpClient();
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons)
            {
                pokemon.PathSound = await HttpClientUtils.DownloadSoundFileTaskAsync(httpClient, pokemon.UrlSound, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation);
            }

            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }

        [HttpPut]
        [Route("UpdateSound")]
        public async Task UpdateSound()
        {
            string response = HttpClientUtils.CallUrl(Constantes.urlAllSprites).Result;
            ScrapingDataUtils.GetUrlSound(response, _repository);
        }

        [HttpPut]
        [Route("UpdateSoundGen9")]
        public async Task UpdateSoundGen9()
        {
            string response = HttpClientUtils.CallUrl(Constantes.urlAllSpritesOld).Result;
            ScrapingDataUtils.GetUrlSoundGen9(response, _repository);
        }

        [HttpPut]
        [Route("UpdateGlobale")]
        public async Task UpdateGlobale()
        {
            List<Ability> abilities = this._repositoryTL.GetAll().Result.ToList();
            foreach (var item in abilities)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            await this._repositoryTL.UpdateRangeAsync(abilities);
            _repositoryTL.UnitOfWork.SaveChanges();

            List<Attack> attacks = this._repositoryAT.GetAll().Result.ToList();
            foreach (var item in attacks)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            await this._repositoryAT.UpdateRangeAsync(attacks);
            _repositoryAT.UnitOfWork.SaveChanges();

            List<Pokemon_Attack> pokemon_Attaques = this._repositoryPAT.GetAll().Result.ToList();
            foreach (var item in pokemon_Attaques)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            await this._repositoryPAT.UpdateRangeAsync(pokemon_Attaques);
            _repositoryPAT.UnitOfWork.SaveChanges();

            List<Pokemon_Ability> pokemon_Abilities = this._repositoryPTL.GetAll().Result.ToList();
            foreach (var item in pokemon_Abilities)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            await this._repositoryPTL.UpdateRangeAsync(pokemon_Abilities);
            _repositoryPTL.UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
