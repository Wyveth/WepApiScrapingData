using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

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
        [Route("AddMissingTalent")]
        public async Task AddMissingTalent()
        {
            Talent talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Analytic_FR)).FirstOrDefault();
            if(talent == null)
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_PowerOfAlchemy_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Harvest_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Imposter_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Multiscale_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Moody_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_ToxicBoost_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Protean_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_FlareBoost_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_ZenMode_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_GaleWings_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Symbiosis_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_GrassPelt_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_LongReach_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_LiquidVoice_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Libero_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_MirrorArmor_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_PropellerTail_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_SteelySpirit_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_PerishBody_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_IceScales_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }
            
            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Stalwart_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Sharpness_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_RockyPayload_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            talent = _repository.Find(m => m.Name_FR.Equals(Constantes.Name_Costar_FR)).FirstOrDefault();
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
                await _repository.Add(talent);
            }

            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
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
