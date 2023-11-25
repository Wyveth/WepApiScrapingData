using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{

    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class GameController : ControllerBase
    {
        #region Fields
        private readonly Repository<Game> _repository;
        #endregion

        #region Constructors
        public GameController(Repository<Game> repository)
        {
            _repository = repository;
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Game> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public async Task<IEnumerable<Game>> GetFindByName(string name)
        {
            return await _repository.Find(m => m.Name_FR.Equals(name));
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
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
            if (_repository.Find(m => m.Name_FR.Equals(game.Name_FR)).Result.Count() == 0)
                games.Add(game);

            await _repository.AddRange(games);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("UpdateGameInDB")]
        public async Task UpdateGameInDB()
        {
            Game game = _repository.Find(m => m.Name_FR.Equals(Constantes.RedBlue_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.RedBlue_Name_FR;
            game.Name_EN = Constantes.RedBlue_Name_EN;
            game.Name_ES = Constantes.RedBlue_Name_ES;
            game.Name_IT = Constantes.RedBlue_Name_IT;
            game.Name_DE = Constantes.RedBlue_Name_DE;
            game.Name_RU = Constantes.RedBlue_Name_RU;
            game.Name_CO = Constantes.RedBlue_Name_CO;
            game.Name_CN = Constantes.RedBlue_Name_CN;
            game.Name_JP = Constantes.RedBlue_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.Yellow_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Yellow_Name_FR;
            game.Name_EN = Constantes.Yellow_Name_EN;
            game.Name_ES = Constantes.Yellow_Name_ES;
            game.Name_IT = Constantes.Yellow_Name_IT;
            game.Name_DE = Constantes.Yellow_Name_DE;
            game.Name_RU = Constantes.Yellow_Name_RU;
            game.Name_CO = Constantes.Yellow_Name_CO;
            game.Name_CN = Constantes.Yellow_Name_CN;
            game.Name_JP = Constantes.Yellow_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.GoldSilver_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.GoldSilver_Name_FR;
            game.Name_EN = Constantes.GoldSilver_Name_EN;
            game.Name_ES = Constantes.GoldSilver_Name_ES;
            game.Name_IT = Constantes.GoldSilver_Name_IT;
            game.Name_DE = Constantes.GoldSilver_Name_DE;
            game.Name_RU = Constantes.GoldSilver_Name_RU;
            game.Name_CO = Constantes.GoldSilver_Name_CO;
            game.Name_CN = Constantes.GoldSilver_Name_CN;
            game.Name_JP = Constantes.GoldSilver_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.Crystal_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Crystal_Name_FR;
            game.Name_EN = Constantes.Crystal_Name_EN;
            game.Name_ES = Constantes.Crystal_Name_ES;
            game.Name_IT = Constantes.Crystal_Name_IT;
            game.Name_DE = Constantes.Crystal_Name_DE;
            game.Name_RU = Constantes.Crystal_Name_RU;
            game.Name_CO = Constantes.Crystal_Name_CO;
            game.Name_CN = Constantes.Crystal_Name_CN;
            game.Name_JP = Constantes.Crystal_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.RubySapphire_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.RubySapphire_Name_FR;
            game.Name_EN = Constantes.RubySapphire_Name_EN;
            game.Name_ES = Constantes.RubySapphire_Name_ES;
            game.Name_IT = Constantes.RubySapphire_Name_IT;
            game.Name_DE = Constantes.RubySapphire_Name_DE;
            game.Name_RU = Constantes.RubySapphire_Name_RU;
            game.Name_CO = Constantes.RubySapphire_Name_CO;
            game.Name_CN = Constantes.RubySapphire_Name_CN;
            game.Name_JP = Constantes.RubySapphire_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.Emerald_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Emerald_Name_FR;
            game.Name_EN = Constantes.Emerald_Name_EN;
            game.Name_ES = Constantes.Emerald_Name_ES;
            game.Name_IT = Constantes.Emerald_Name_IT;
            game.Name_DE = Constantes.Emerald_Name_DE;
            game.Name_RU = Constantes.Emerald_Name_RU;
            game.Name_CO = Constantes.Emerald_Name_CO;
            game.Name_CN = Constantes.Emerald_Name_CN;
            game.Name_JP = Constantes.Emerald_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.FireRedLeafGreen_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.FireRedLeafGreen_Name_FR;
            game.Name_EN = Constantes.FireRedLeafGreen_Name_EN;
            game.Name_ES = Constantes.FireRedLeafGreen_Name_ES;
            game.Name_IT = Constantes.FireRedLeafGreen_Name_IT;
            game.Name_DE = Constantes.FireRedLeafGreen_Name_DE;
            game.Name_RU = Constantes.FireRedLeafGreen_Name_RU;
            game.Name_CO = Constantes.FireRedLeafGreen_Name_CO;
            game.Name_CN = Constantes.FireRedLeafGreen_Name_CN;
            game.Name_JP = Constantes.FireRedLeafGreen_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.DiamondPearl_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.DiamondPearl_Name_FR;
            game.Name_EN = Constantes.DiamondPearl_Name_EN;
            game.Name_ES = Constantes.DiamondPearl_Name_ES;
            game.Name_IT = Constantes.DiamondPearl_Name_IT;
            game.Name_DE = Constantes.DiamondPearl_Name_DE;
            game.Name_RU = Constantes.DiamondPearl_Name_RU;
            game.Name_CO = Constantes.DiamondPearl_Name_CO;
            game.Name_CN = Constantes.DiamondPearl_Name_CN;
            game.Name_JP = Constantes.DiamondPearl_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.Platinum_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Platinum_Name_FR;
            game.Name_EN = Constantes.Platinum_Name_EN;
            game.Name_ES = Constantes.Platinum_Name_ES;
            game.Name_IT = Constantes.Platinum_Name_IT;
            game.Name_DE = Constantes.Platinum_Name_DE;
            game.Name_RU = Constantes.Platinum_Name_RU;
            game.Name_CO = Constantes.Platinum_Name_CO;
            game.Name_CN = Constantes.Platinum_Name_CN;
            game.Name_JP = Constantes.Platinum_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.HeartGoldSoulSilver_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.HeartGoldSoulSilver_Name_FR;
            game.Name_EN = Constantes.HeartGoldSoulSilver_Name_EN;
            game.Name_ES = Constantes.HeartGoldSoulSilver_Name_ES;
            game.Name_IT = Constantes.HeartGoldSoulSilver_Name_IT;
            game.Name_DE = Constantes.HeartGoldSoulSilver_Name_DE;
            game.Name_RU = Constantes.HeartGoldSoulSilver_Name_RU;
            game.Name_CO = Constantes.HeartGoldSoulSilver_Name_CO;
            game.Name_CN = Constantes.HeartGoldSoulSilver_Name_CN;
            game.Name_JP = Constantes.HeartGoldSoulSilver_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.BlackWhite_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.BlackWhite_Name_FR;
            game.Name_EN = Constantes.BlackWhite_Name_EN;
            game.Name_ES = Constantes.BlackWhite_Name_ES;
            game.Name_IT = Constantes.BlackWhite_Name_IT;
            game.Name_DE = Constantes.BlackWhite_Name_DE;
            game.Name_RU = Constantes.BlackWhite_Name_RU;
            game.Name_CO = Constantes.BlackWhite_Name_CO;
            game.Name_CN = Constantes.BlackWhite_Name_CN;
            game.Name_JP = Constantes.BlackWhite_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.Black2White2_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Black2White2_Name_FR;
            game.Name_EN = Constantes.Black2White2_Name_EN;
            game.Name_ES = Constantes.Black2White2_Name_ES;
            game.Name_IT = Constantes.Black2White2_Name_IT;
            game.Name_DE = Constantes.Black2White2_Name_DE;
            game.Name_RU = Constantes.Black2White2_Name_RU;
            game.Name_CO = Constantes.Black2White2_Name_CO;
            game.Name_CN = Constantes.Black2White2_Name_CN;
            game.Name_JP = Constantes.Black2White2_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.X_Y_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.X_Y_Name_FR;
            game.Name_EN = Constantes.X_Y_Name_EN;
            game.Name_ES = Constantes.X_Y_Name_ES;
            game.Name_IT = Constantes.X_Y_Name_IT;
            game.Name_DE = Constantes.X_Y_Name_DE;
            game.Name_RU = Constantes.X_Y_Name_RU;
            game.Name_CO = Constantes.X_Y_Name_CO;
            game.Name_CN = Constantes.X_Y_Name_CN;
            game.Name_JP = Constantes.X_Y_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.SunMoon_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.SunMoon_Name_FR;
            game.Name_EN = Constantes.SunMoon_Name_EN;
            game.Name_ES = Constantes.SunMoon_Name_ES;
            game.Name_IT = Constantes.SunMoon_Name_IT;
            game.Name_DE = Constantes.SunMoon_Name_DE;
            game.Name_RU = Constantes.SunMoon_Name_RU;
            game.Name_CO = Constantes.SunMoon_Name_CO;
            game.Name_CN = Constantes.SunMoon_Name_CN;
            game.Name_JP = Constantes.SunMoon_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.UltraSunUltraMoon_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.UltraSunUltraMoon_Name_FR;
            game.Name_EN = Constantes.UltraSunUltraMoon_Name_EN;
            game.Name_ES = Constantes.UltraSunUltraMoon_Name_ES;
            game.Name_IT = Constantes.UltraSunUltraMoon_Name_IT;
            game.Name_DE = Constantes.UltraSunUltraMoon_Name_DE;
            game.Name_RU = Constantes.UltraSunUltraMoon_Name_RU;
            game.Name_CO = Constantes.UltraSunUltraMoon_Name_CO;
            game.Name_CN = Constantes.UltraSunUltraMoon_Name_CN;
            game.Name_JP = Constantes.UltraSunUltraMoon_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.LetsGoPikachuEvoli_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.LetsGoPikachuEvoli_Name_FR;
            game.Name_EN = Constantes.LetsGoPikachuEvoli_Name_EN;
            game.Name_ES = Constantes.LetsGoPikachuEvoli_Name_ES;
            game.Name_IT = Constantes.LetsGoPikachuEvoli_Name_IT;
            game.Name_DE = Constantes.LetsGoPikachuEvoli_Name_DE;
            game.Name_RU = Constantes.LetsGoPikachuEvoli_Name_RU;
            game.Name_CO = Constantes.LetsGoPikachuEvoli_Name_CO;
            game.Name_CN = Constantes.LetsGoPikachuEvoli_Name_CN;
            game.Name_JP = Constantes.LetsGoPikachuEvoli_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.SwordShield_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.SwordShield_Name_FR;
            game.Name_EN = Constantes.SwordShield_Name_EN;
            game.Name_ES = Constantes.SwordShield_Name_ES;
            game.Name_IT = Constantes.SwordShield_Name_IT;
            game.Name_DE = Constantes.SwordShield_Name_DE;
            game.Name_RU = Constantes.SwordShield_Name_RU;
            game.Name_CO = Constantes.SwordShield_Name_CO;
            game.Name_CN = Constantes.SwordShield_Name_CN;
            game.Name_JP = Constantes.SwordShield_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.ShiningDiamondShiningPearl_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.ShiningDiamondShiningPearl_Name_FR;
            game.Name_EN = Constantes.ShiningDiamondShiningPearl_Name_EN;
            game.Name_ES = Constantes.ShiningDiamondShiningPearl_Name_ES;
            game.Name_IT = Constantes.ShiningDiamondShiningPearl_Name_IT;
            game.Name_DE = Constantes.ShiningDiamondShiningPearl_Name_DE;
            game.Name_RU = Constantes.ShiningDiamondShiningPearl_Name_RU;
            game.Name_CO = Constantes.ShiningDiamondShiningPearl_Name_CO;
            game.Name_CN = Constantes.ShiningDiamondShiningPearl_Name_CN;
            game.Name_JP = Constantes.ShiningDiamondShiningPearl_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.Arceus_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.Arceus_Name_FR;
            game.Name_EN = Constantes.Arceus_Name_EN;
            game.Name_ES = Constantes.Arceus_Name_ES;
            game.Name_IT = Constantes.Arceus_Name_IT;
            game.Name_DE = Constantes.Arceus_Name_DE;
            game.Name_RU = Constantes.Arceus_Name_RU;
            game.Name_CO = Constantes.Arceus_Name_CO;
            game.Name_CN = Constantes.Arceus_Name_CN;
            game.Name_JP = Constantes.Arceus_Name_JP;
            _repository.Edit(game);

            game = _repository.Find(m => m.Name_FR.Equals(Constantes.ScarletViolet_Name_FR)).Result.FirstOrDefault();
            game.Name_FR = Constantes.ScarletViolet_Name_FR;
            game.Name_EN = Constantes.ScarletViolet_Name_EN;
            game.Name_ES = Constantes.ScarletViolet_Name_ES;
            game.Name_IT = Constantes.ScarletViolet_Name_IT;
            game.Name_DE = Constantes.ScarletViolet_Name_DE;
            game.Name_RU = Constantes.ScarletViolet_Name_RU;
            game.Name_CO = Constantes.ScarletViolet_Name_CO;
            game.Name_CN = Constantes.ScarletViolet_Name_CN;
            game.Name_JP = Constantes.ScarletViolet_Name_JP;
            _repository.Edit(game);

            _repository.UnitOfWork.SaveChanges();
        }
    }
}
