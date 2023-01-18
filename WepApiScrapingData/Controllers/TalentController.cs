using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TalentController : ControllerBase
    {
        #region Fields
        private readonly IRepository<Talent> _repository;
        private readonly IRepositoryExtendsPokemon<Pokemon> _repositoryPokemon;
        #endregion

        #region Constructors
        public TalentController(IRepository<Talent> repository, IRepositoryExtendsPokemon<Pokemon> repositoryPokemon)
        {
            _repository = repository;
            _repositoryPokemon = repositoryPokemon;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IEnumerable<Talent>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Talent> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("Find")]
        public IEnumerable<Talent> GetFind(Expression<Func<Talent, bool>> predicate)
        {
            return _repository.Find(predicate);
        }
        
        [HttpPost]
        [Route("UpdateTalent")]
        public async Task UpdateTalent()
        {
            IEnumerable<Talent> talentsDB = await _repository.GetAll();
            List<Talent> talents = talentsDB.ToList();
            List<Talent> newTalents = new List<Talent>();
            IEnumerable<Pokemon> pokemons = await _repositoryPokemon.GetAll();
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

            await _repository.AddRange(newTalents);
            _repository.UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
