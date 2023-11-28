﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class DatabaseController : ControllerBase
    {
        #region Fields
        private readonly PokemonRepository _repository;
        private readonly Repository<TypePok> _repositoryTP;
        private readonly Repository<Talent> _repositoryTL;
        private readonly Repository<Attaque> _repositoryAT;
        private readonly Repository<TypeAttaque> _repositoryTA;
        private readonly Repository<Game> _repositoryG;
        private readonly Repository<Pokemon_TypePok> _repositoryPTP;
        private readonly Repository<Pokemon_Weakness> _repositoryPWN;
        private readonly Repository<Pokemon_Talent> _repositoryPTL;
        private readonly Repository<Pokemon_Attaque> _repositoryPAT;
        #endregion

        #region Constructors
        public DatabaseController(PokemonRepository repository, Repository<TypePok> repositoryTP, Repository<Talent> repositoryTL, Repository<Attaque> repositoryAT, Repository<TypeAttaque> repositoryTA, Repository<Game> repositoryG, Repository<Pokemon_TypePok> repositoryPTP, Repository<Pokemon_Weakness> repositoryPWN, Repository<Pokemon_Talent> repositoryPTL, Repository<Pokemon_Attaque> repositoryPAT)
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
        }
        #endregion
        
        #region Public Methods
        [HttpGet]
        [Route("ExportDb")]
        public Task ExportDb()
        {
            List<Pokemon> pokemons = _repository.GetAll().Result.ToList();

            List<TypeAttaque> typeAttaques = _repositoryTA.GetAll().Result.ToList();
            List<Attaque> attaques = _repositoryAT.GetAll().Result.ToList();
            List<TypePok> typePoks = _repositoryTP.GetAll().Result.ToList();
            List<Talent> talents = _repositoryTL.GetAll().Result.ToList();
            List<Game> games = _repositoryG.GetAll().Result.ToList();

            Debug.WriteLine("Start Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ScrapingDataUtils.WriteToJson(pokemons, typePoks, talents, attaques, typeAttaques, games);
            Debug.WriteLine("End Creation Json - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            return Task.CompletedTask;
        }

        [HttpGet]
        [Route("ImportDb")]
        public Task ImportDb()
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

                        _repositoryG.Add(game);
                    }
                }
            }
            #endregion

            #region TypeAttaque
            using (StreamReader r = new StreamReader(Constantes.pathExport + "TypeAttaqueDbToJson.json"))
            {
                json = r.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                {
                    List<TypeAttaqueExportJson> typesAttackJson = JsonConvert.DeserializeObject<List<TypeAttaqueExportJson>>(json);
                    foreach (TypeAttaqueExportJson typeAttackJson in typesAttackJson)
                    {
                        TypeAttaque typeAttaque = new TypeAttaque()
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

                        _repositoryTA.Add(typeAttaque);
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
                        _repositoryTP.Add(typePok);
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
                    List<TalentExportJson> talentsJson = JsonConvert.DeserializeObject<List<TalentExportJson>>(json);
                    foreach (TalentExportJson talentJson in talentsJson)
                    {
                        Talent talent = new()
                        {
                            Name_FR = talentJson.Name_FR,
                            Description_FR = talentJson.Description_FR,
                            Name_EN = talentJson.Name_EN,
                            Description_EN = talentJson.Description_EN,
                            Name_ES = talentJson.Name_ES,
                            Description_ES = talentJson.Description_ES,
                            Name_IT = talentJson.Name_IT,
                            Description_IT = talentJson.Description_IT,
                            Name_DE = talentJson.Name_DE,
                            Description_DE = talentJson.Description_DE,
                            Name_RU = talentJson.Name_RU,
                            Description_RU = talentJson.Description_RU,
                            Name_CO = talentJson.Name_CO,
                            Description_CO = talentJson.Description_CO,
                            Name_CN = talentJson.Name_CN,
                            Description_CN = talentJson.Description_CN,
                            Name_JP = talentJson.Name_JP,
                            Description_JP = talentJson.Description_JP
                        };
                        _repositoryTL.Add(talent);
                    }
                }
            }
            #endregion

            _repository.UnitOfWork.SaveChanges();

            #region Attaque
            using (StreamReader r = new StreamReader(Constantes.pathExport + "AttaqueDbToJson.json"))
            {
                json = r.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                {
                    List<AttaqueExportJson> attaquesJson = JsonConvert.DeserializeObject<List<AttaqueExportJson>>(json);
                    foreach (AttaqueExportJson attaqueJson in attaquesJson)
                    {
                        TypePok typePok = _repositoryTP.Find(m => m.Name_EN.Equals(attaqueJson.Types.Name_EN)).Result.FirstOrDefault();
                        TypeAttaque typeAttaque = _repositoryTA.Find(m => m.Name_EN.Equals(attaqueJson.TypeAttaque.Name_EN)).Result.FirstOrDefault();
                        Attaque attaque = new()
                        {
                            Name_FR = attaqueJson.Name_FR,
                            Description_FR = attaqueJson.Description_FR,
                            Name_EN = attaqueJson.Name_EN,
                            Description_EN = attaqueJson.Description_EN,
                            Name_ES = attaqueJson.Name_ES,
                            Description_ES = attaqueJson.Description_ES,
                            Name_IT = attaqueJson.Name_IT,
                            Description_IT = attaqueJson.Description_IT,
                            Name_DE = attaqueJson.Name_DE,
                            Description_DE = attaqueJson.Description_DE,
                            Name_RU = attaqueJson.Name_RU,
                            Description_RU = attaqueJson.Description_RU,
                            Name_CO = attaqueJson.Name_CO,
                            Description_CO = attaqueJson.Description_CO,
                            Name_CN = attaqueJson.Name_CN,
                            Description_CN = attaqueJson.Description_CN,
                            Name_JP = attaqueJson.Name_JP,
                            Description_JP = attaqueJson.Description_JP,
                            typeAttaque = typeAttaque,
                            typePok = typePok,
                            Power = attaqueJson.Puissance,
                            Precision = attaqueJson.Precision,
                            PP = attaqueJson.PP
                        };
                        _repositoryAT.Add(attaque);
                    }
                }
            }
            #endregion

            _repository.UnitOfWork.SaveChanges();

            using (StreamReader sr = new StreamReader(Constantes.pathExport + "DbToJson.json"))
            {
                json = sr.ReadToEnd();
                _repository.ImportJsonToDb(json);
            }

            return Task.CompletedTask;
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

            await _repositoryG.AddRange(games);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [Route("AddMissingTalent")]
        public async Task AddMissingTalent()
        {
            Talent talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Analytic_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_PowerOfAlchemy_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Harvest_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Imposter_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Multiscale_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Moody_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_ToxicBoost_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Protean_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_FlareBoost_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_ZenMode_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_GaleWings_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Symbiosis_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_GrassPelt_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_LongReach_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_LiquidVoice_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Libero_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_MirrorArmor_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_PropellerTail_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_SteelySpirit_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_PerishBody_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_IceScales_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Stalwart_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Sharpness_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_RockyPayload_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            talent = _repositoryTL.Find(m => m.Name_FR.Equals(Constantes.Name_Costar_FR)).Result.FirstOrDefault();
            if (talent == null)
            {
                talent = new()
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
                await _repositoryTL.Add(talent);
            }

            _repositoryTL.UnitOfWork.SaveChanges();
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
            IEnumerable<Talent> talentsDB = await _repositoryTL.GetAll();
            List<Talent> talents = talentsDB.ToList();
            List<Talent> newTalents = new List<Talent>();
            IEnumerable<Pokemon> pokemons = await _repository.GetAll();
            foreach (Pokemon pokemon in pokemons.ToList())
            {
                if (pokemon.FR.Talent != null)
                {
                    int count = pokemon.FR.Talent.Split(",").Length;

                    for (int i = 0; i < count; i++)
                    {
                        Talent talent = new Talent();
                        #region FR
                        if (pokemon.FR.Talent != null)
                        {
                            string[] Name = pokemon.FR.Talent.Split(",");
                            string[] Description = pokemon.FR.DescriptionTalent.Split(";");

                            talent.Name_FR = Name[i];
                            talent.Description_FR = Description[i];
                        }
                        #endregion

                        #region EN
                        if (pokemon.EN.Talent != null)
                        {
                            string[] Name = pokemon.EN.Talent.Split(",");
                            string[] Description = pokemon.EN.DescriptionTalent.Split(";");

                            talent.Name_EN = Name[i];
                            talent.Description_EN = Description[i];
                        }
                        #endregion

                        #region ES
                        if (pokemon.ES.Talent != null)
                        {
                            string[] Name = pokemon.ES.Talent.Split(",");
                            string[] Description = pokemon.ES.DescriptionTalent.Split(";");

                            talent.Name_ES = Name[i];
                            talent.Description_ES = Description[i];
                        }
                        #endregion

                        #region IT
                        if (pokemon.IT.Talent != null)
                        {
                            string[] Name = pokemon.IT.Talent.Split(",");
                            string[] Description = pokemon.IT.DescriptionTalent.Split(";");

                            talent.Name_IT = Name[i];
                            talent.Description_IT = Description[i];
                        }
                        #endregion

                        #region DE
                        if (pokemon.DE.Talent != null)
                        {
                            string[] Name = pokemon.DE.Talent.Split(",");
                            string[] Description = pokemon.DE.DescriptionTalent.Split(";");

                            talent.Name_DE = Name[i];
                            talent.Description_DE = Description[i];
                        }
                        #endregion

                        #region RU
                        if (pokemon.RU.Talent != null && pokemon.RU.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.RU.Talent.Split(",");
                            string[] Description = pokemon.RU.DescriptionTalent.Split(";");

                            talent.Name_RU = Name[i];
                            talent.Description_RU = Description[i];
                        }
                        #endregion

                        #region CO
                        if (pokemon.CO.Talent != null && pokemon.CO.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.CO.Talent.Split(",");
                            string[] Description = pokemon.CO.DescriptionTalent.Split(";");

                            talent.Name_CO = Name[i];
                            talent.Description_CO = Description[i];
                        }
                        #endregion

                        #region CN
                        if (pokemon.CN.Talent != null && pokemon.CN.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.CN.Talent.Split(",");
                            string[] Description = pokemon.CN.DescriptionTalent.Split(";");

                            talent.Name_CN = Name[i];
                            talent.Description_CN = Description[i];
                        }
                        #endregion

                        #region JP
                        if (pokemon.JP.Talent != null && pokemon.JP.DescriptionTalent != "")
                        {
                            string[] Name = pokemon.JP.Talent.Split(",");
                            string[] Description = pokemon.JP.DescriptionTalent.Split(";");

                            talent.Name_JP = Name[i];
                            talent.Description_JP = Description[i];
                        }
                        #endregion

                        Talent talentExist = talents.Find(x => x.Name_FR == talent.Name_FR);
                        if (talentExist == null)
                        {
                            Talent newTalentsExist = newTalents.Find(x => x.Name_FR == talent.Name_FR);
                            if (newTalentsExist == null)
                            {
                                newTalents.Add(talent);
                                Console.WriteLine(talent.Name_FR + ": " + talent.Description_FR);
                            }
                        }
                    }
                }
            }

            await _repositoryTL.AddRange(newTalents);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateGameInDB")]
        public async Task UpdateGameInDB()
        {
            Game game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.RedBlue_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.RedBlue_Name_FR;
            game.Name_EN = Constantes.RedBlue_Name_EN;
            game.Name_ES = Constantes.RedBlue_Name_ES;
            game.Name_IT = Constantes.RedBlue_Name_IT;
            game.Name_DE = Constantes.RedBlue_Name_DE;
            game.Name_RU = Constantes.RedBlue_Name_RU;
            game.Name_CO = Constantes.RedBlue_Name_CO;
            game.Name_CN = Constantes.RedBlue_Name_CN;
            game.Name_JP = Constantes.RedBlue_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Yellow_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Yellow_Name_FR;
            game.Name_EN = Constantes.Yellow_Name_EN;
            game.Name_ES = Constantes.Yellow_Name_ES;
            game.Name_IT = Constantes.Yellow_Name_IT;
            game.Name_DE = Constantes.Yellow_Name_DE;
            game.Name_RU = Constantes.Yellow_Name_RU;
            game.Name_CO = Constantes.Yellow_Name_CO;
            game.Name_CN = Constantes.Yellow_Name_CN;
            game.Name_JP = Constantes.Yellow_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.GoldSilver_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.GoldSilver_Name_FR;
            game.Name_EN = Constantes.GoldSilver_Name_EN;
            game.Name_ES = Constantes.GoldSilver_Name_ES;
            game.Name_IT = Constantes.GoldSilver_Name_IT;
            game.Name_DE = Constantes.GoldSilver_Name_DE;
            game.Name_RU = Constantes.GoldSilver_Name_RU;
            game.Name_CO = Constantes.GoldSilver_Name_CO;
            game.Name_CN = Constantes.GoldSilver_Name_CN;
            game.Name_JP = Constantes.GoldSilver_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Crystal_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Crystal_Name_FR;
            game.Name_EN = Constantes.Crystal_Name_EN;
            game.Name_ES = Constantes.Crystal_Name_ES;
            game.Name_IT = Constantes.Crystal_Name_IT;
            game.Name_DE = Constantes.Crystal_Name_DE;
            game.Name_RU = Constantes.Crystal_Name_RU;
            game.Name_CO = Constantes.Crystal_Name_CO;
            game.Name_CN = Constantes.Crystal_Name_CN;
            game.Name_JP = Constantes.Crystal_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.RubySapphire_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.RubySapphire_Name_FR;
            game.Name_EN = Constantes.RubySapphire_Name_EN;
            game.Name_ES = Constantes.RubySapphire_Name_ES;
            game.Name_IT = Constantes.RubySapphire_Name_IT;
            game.Name_DE = Constantes.RubySapphire_Name_DE;
            game.Name_RU = Constantes.RubySapphire_Name_RU;
            game.Name_CO = Constantes.RubySapphire_Name_CO;
            game.Name_CN = Constantes.RubySapphire_Name_CN;
            game.Name_JP = Constantes.RubySapphire_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Emerald_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Emerald_Name_FR;
            game.Name_EN = Constantes.Emerald_Name_EN;
            game.Name_ES = Constantes.Emerald_Name_ES;
            game.Name_IT = Constantes.Emerald_Name_IT;
            game.Name_DE = Constantes.Emerald_Name_DE;
            game.Name_RU = Constantes.Emerald_Name_RU;
            game.Name_CO = Constantes.Emerald_Name_CO;
            game.Name_CN = Constantes.Emerald_Name_CN;
            game.Name_JP = Constantes.Emerald_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.FireRedLeafGreen_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.FireRedLeafGreen_Name_FR;
            game.Name_EN = Constantes.FireRedLeafGreen_Name_EN;
            game.Name_ES = Constantes.FireRedLeafGreen_Name_ES;
            game.Name_IT = Constantes.FireRedLeafGreen_Name_IT;
            game.Name_DE = Constantes.FireRedLeafGreen_Name_DE;
            game.Name_RU = Constantes.FireRedLeafGreen_Name_RU;
            game.Name_CO = Constantes.FireRedLeafGreen_Name_CO;
            game.Name_CN = Constantes.FireRedLeafGreen_Name_CN;
            game.Name_JP = Constantes.FireRedLeafGreen_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.DiamondPearl_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.DiamondPearl_Name_FR;
            game.Name_EN = Constantes.DiamondPearl_Name_EN;
            game.Name_ES = Constantes.DiamondPearl_Name_ES;
            game.Name_IT = Constantes.DiamondPearl_Name_IT;
            game.Name_DE = Constantes.DiamondPearl_Name_DE;
            game.Name_RU = Constantes.DiamondPearl_Name_RU;
            game.Name_CO = Constantes.DiamondPearl_Name_CO;
            game.Name_CN = Constantes.DiamondPearl_Name_CN;
            game.Name_JP = Constantes.DiamondPearl_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.Platinum_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Platinum_Name_FR;
            game.Name_EN = Constantes.Platinum_Name_EN;
            game.Name_ES = Constantes.Platinum_Name_ES;
            game.Name_IT = Constantes.Platinum_Name_IT;
            game.Name_DE = Constantes.Platinum_Name_DE;
            game.Name_RU = Constantes.Platinum_Name_RU;
            game.Name_CO = Constantes.Platinum_Name_CO;
            game.Name_CN = Constantes.Platinum_Name_CN;
            game.Name_JP = Constantes.Platinum_Name_JP;
            _repositoryG.Update(game);

            game = _repositoryG.Find(m => m.Name_FR.Equals(Constantes.HeartGoldSoulSilver_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.HeartGoldSoulSilver_Name_FR;
            game.Name_EN = Constantes.HeartGoldSoulSilver_Name_EN;
            game.Name_ES = Constantes.HeartGoldSoulSilver_Name_ES;
            game.Name_IT = Constantes.HeartGoldSoulSilver_Name_IT;
            game.Name_DE = Constantes.HeartGoldSoulSilver_Name_DE;
            game.Name_RU = Constantes.HeartGoldSoulSilver_Name_RU;
            game.Name_CO = Constantes.HeartGoldSoulSilver_Name_CO;
            game.Name_CN = Constantes.HeartGoldSoulSilver_Name_CN;
            game.Name_JP = Constantes.HeartGoldSoulSilver_Name_JP;
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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
            _repositoryG.Update(game);

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

                _repositoryPTP.AddRange(pokemon_TypePoks);
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

                    await _repositoryPWN.AddRange(pokemon_Weaknesses);
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
                List<Pokemon_Talent> pokemon_Talents = new();

                foreach (string type in pokemon.FR.Talent.Split(','))
                {
                    Talent talentPok = await _repositoryTL.SingleOrDefault(x => x.Name_FR.Equals(type));
                    Pokemon_Talent pokemon_Talent = new()
                    {
                        PokemonId = pokemon.Id,
                        TalentId = talentPok.Id
                    };

                    pokemon_Talents.Add(pokemon_Talent);
                }

                _repositoryPTL.AddRange(pokemon_Talents);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateTypeAttaqueInDB")]
        public async Task UpdateTypeAttaqueInDB()
        {
            List<TypeAttaque> typeAttaques = new List<TypeAttaque>();

            TypeAttaque typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = Constantes.Physical_Name_FR;
            typeAttaque.Description_FR = Constantes.Physical_Description_FR;
            typeAttaque.Name_EN = Constantes.Physical_Name_EN;
            typeAttaque.Description_EN = Constantes.Physical_Description_EN;
            typeAttaque.Name_ES = Constantes.Physical_Name_ES;
            typeAttaque.Description_ES = Constantes.Physical_Description_ES;
            typeAttaque.Name_IT = Constantes.Physical_Name_IT;
            typeAttaque.Description_IT = Constantes.Physical_Description_IT;
            typeAttaque.Name_DE = Constantes.Physical_Name_DE;
            typeAttaque.Description_DE = Constantes.Physical_Description_DE;
            typeAttaque.Name_RU = Constantes.Physical_Name_RU;
            typeAttaque.Description_RU = Constantes.Physical_Description_RU;
            typeAttaque.Name_CO = Constantes.Physical_Name_CO;
            typeAttaque.Description_CO = Constantes.Physical_Description_CO;
            typeAttaque.Name_CN = Constantes.Physical_Name_CN;
            typeAttaque.Description_CN = Constantes.Physical_Description_CN;
            typeAttaque.Name_JP = Constantes.Physical_Name_JP;
            typeAttaque.Description_JP = Constantes.Physical_Description_JP;
            typeAttaque.UrlImg = Constantes.Physical_UrlImg;
            if (_repositoryTA.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Result.Count() == 0)
                typeAttaques.Add(typeAttaque);

            typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = Constantes.Special_Name_FR;
            typeAttaque.Description_FR = Constantes.Special_Description_FR;
            typeAttaque.Name_EN = Constantes.Special_Name_EN;
            typeAttaque.Description_EN = Constantes.Special_Description_EN;
            typeAttaque.Name_ES = Constantes.Special_Name_ES;
            typeAttaque.Description_ES = Constantes.Special_Description_ES;
            typeAttaque.Name_IT = Constantes.Special_Name_IT;
            typeAttaque.Description_IT = Constantes.Special_Description_IT;
            typeAttaque.Name_DE = Constantes.Special_Name_DE;
            typeAttaque.Description_DE = Constantes.Special_Description_DE;
            typeAttaque.Name_RU = Constantes.Special_Name_RU;
            typeAttaque.Description_RU = Constantes.Special_Description_RU;
            typeAttaque.Name_CO = Constantes.Special_Name_CO;
            typeAttaque.Description_CO = Constantes.Special_Description_CO;
            typeAttaque.Name_CN = Constantes.Special_Name_CN;
            typeAttaque.Description_CN = Constantes.Special_Description_CN;
            typeAttaque.Name_JP = Constantes.Special_Name_JP;
            typeAttaque.Description_JP = Constantes.Special_Description_JP;
            typeAttaque.UrlImg = Constantes.Special_UrlImg;
            if (_repositoryTA.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Result.Count() == 0)
                typeAttaques.Add(typeAttaque);

            typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = Constantes.Status_Name_FR;
            typeAttaque.Description_FR = Constantes.Status_Description_FR;
            typeAttaque.Name_EN = Constantes.Status_Name_EN;
            typeAttaque.Description_EN = Constantes.Status_Description_EN;
            typeAttaque.Name_ES = Constantes.Status_Name_ES;
            typeAttaque.Description_ES = Constantes.Status_Description_ES;
            typeAttaque.Name_IT = Constantes.Status_Name_IT;
            typeAttaque.Description_IT = Constantes.Status_Description_IT;
            typeAttaque.Name_DE = Constantes.Status_Name_DE;
            typeAttaque.Description_DE = Constantes.Status_Description_DE;
            typeAttaque.Name_RU = Constantes.Status_Name_RU;
            typeAttaque.Description_RU = Constantes.Status_Description_RU;
            typeAttaque.Name_CO = Constantes.Status_Name_CO;
            typeAttaque.Description_CO = Constantes.Status_Description_CO;
            typeAttaque.Name_CN = Constantes.Status_Name_CN;
            typeAttaque.Description_CN = Constantes.Status_Description_CN;
            typeAttaque.Name_JP = Constantes.Status_Name_JP;
            typeAttaque.Description_JP = Constantes.Status_Description_JP;
            typeAttaque.UrlImg = Constantes.Status_UrlImg;
            if (_repositoryTA.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Result.Count() == 0)
                typeAttaques.Add(typeAttaque);

            await _repositoryTA.AddRange(typeAttaques);
            _repositoryTA.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("DlUpdateTypeAttaquePathUrl")]
        public async Task DlUpdateTypeAttaquePathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<TypeAttaque> typeAttaques = await _repositoryTA.GetAll();
            foreach (TypeAttaque typeAttaque in typeAttaques)
            {
                StringBuilder nameBuilder = new StringBuilder();
                switch (typeAttaque.Name_FR)
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
                typeAttaque.PathImg = await HttpClientUtils.DownloadTypeAttackFileTaskAsync(httpClient, typeAttaque.UrlImg, nameBuilder.ToString());
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
            foreach (Pokemon pokemon in pokemons.Where(m => m.PathImg == null))
            {
                pokemon.PathImg = await HttpClientUtils.DownloadFileTaskAsync(httpClient, pokemon.UrlImg, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation);
                pokemon.PathSprite = await HttpClientUtils.DownloadFileTaskAsync(httpClient, pokemon.UrlSprite, pokemon.EN.Name.Replace(" ", "_"), pokemon.Generation, true);
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
        public void UpdateGlobale()
        {
            List<Talent> talents = this._repositoryTL.GetAll().Result.ToList();
            foreach (var item in talents)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryTL.UpdateRange(talents);
            _repositoryTL.UnitOfWork.SaveChanges();

            List<Attaque> attaques = this._repositoryAT.GetAll().Result.ToList();
            foreach (var item in attaques)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryAT.UpdateRange(attaques);
            _repositoryAT.UnitOfWork.SaveChanges();

            List<Pokemon_Attaque> pokemon_Attaques = this._repositoryPAT.GetAll().Result.ToList();
            foreach (var item in pokemon_Attaques)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryPAT.UpdateRange(pokemon_Attaques);
            _repositoryPAT.UnitOfWork.SaveChanges();

            List<Pokemon_Talent> pokemon_Talents = this._repositoryPTL.GetAll().Result.ToList();
            foreach (var item in pokemon_Talents)
            {
                item.UserCreation = "System";
                item.DateCreation = DateTime.Now;
            }
            this._repositoryPTL.UpdateRange(pokemon_Talents);
            _repositoryPTL.UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
