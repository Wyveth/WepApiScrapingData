using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;
namespace WebApiScrapingData.Infrastructure.Repository
{
    public class PokemonRepository : IRepository<Pokemon>
    {
        #region Fields
        private readonly ScrapingContext _context;
        private readonly DataInfoRepository _dataInfoRepository;
        private readonly TypePokRepository _typePokRepository;
        #endregion

        #region Constructor
        public PokemonRepository(ScrapingContext context)
        {
            this._context = context;
            this._dataInfoRepository = new DataInfoRepository(context);
            this._typePokRepository = new TypePokRepository(context);
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Pokemon entity)
        {
            UpdateInfo(entity);
            await this._context.Pokemons.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Pokemon> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Pokemons.AddRangeAsync(entities);
        }

        public async Task SaveJsonInDb(string json)
        {
            List<PokemonJson> pokemonsJson = JsonConvert.DeserializeObject<List<PokemonJson>>(json);
            foreach (PokemonJson pokemonJson in pokemonsJson)
            {
                Pokemon pokemon = new();
                this.MapToInstance(pokemon, pokemonJson);
                await this.Add(pokemon);
            }
        }
        #endregion

        #region Read
        public IEnumerable<Pokemon> Find(Expression<Func<Pokemon, bool>> predicate)
        {
            return this._context.Pokemons
                .Include("FR")
                .Include("EN")
                .Include("ES")
                .Include("IT")
                .Include("DE")
                .Include("RU")
                .Include("CO")
                .Include("CN")
                .Include("JP")
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Where(predicate)
                .AsQueryable();
        }

        public async Task<Pokemon> Get(int id)
        {
            return await this._context.Pokemons
                .Include(m => m.FR)
                .Include(m => m.EN)
                .Include(m => m.ES)
                .Include(m => m.IT)
                .Include(m => m.DE)
                .Include(m => m.RU)
                .Include(m => m.CO)
                .Include(m => m.CN)
                .Include(m => m.JP)
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Pokemon> Query()
        {
            return this._context.Pokemons
                .Include("FR")
                .Include("EN")
                .Include("ES")
                .Include("IT")
                .Include("DE")
                .Include("RU")
                .Include("CO")
                .Include("CN")
                .Include("JP")
                .AsQueryable();
        }

        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            return await this._context.Pokemons
                .Include(m => m.FR)
                .Include(m => m.EN)
                .Include(m => m.ES)
                .Include(m => m.IT)
                .Include(m => m.DE)
                .Include(m => m.RU)
                .Include(m => m.CO)
                .Include(m => m.CN)
                .Include(m => m.JP)
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Pokemon entity)
        {
            UpdateInfo(entity, true);
            this._context.Update(entity);
        }

        public void EditRange(IEnumerable<Pokemon> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Pokemons.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Pokemon entity)
        {
            this._context.Pokemons.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Pokemon> entities)
        {
            this._context.Pokemons.RemoveRange(entities);
        }

        public async Task<Pokemon?> SingleOrDefault(Expression<Func<Pokemon, bool>> predicate)
        {
            return await this._context.Pokemons.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        public async Task MapToInstance(Pokemon pokemon, PokemonJson pokemonJson)
        {
            pokemon.Number = pokemonJson.Number;
            pokemon.FR = await _dataInfoRepository.SaveJsonInDb(pokemonJson.FR);
            pokemon.EN = await _dataInfoRepository.SaveJsonInDb(pokemonJson.EN);
            pokemon.ES = await _dataInfoRepository.SaveJsonInDb(pokemonJson.ES);
            pokemon.IT = await _dataInfoRepository.SaveJsonInDb(pokemonJson.IT);
            pokemon.DE = await _dataInfoRepository.SaveJsonInDb(pokemonJson.DE);
            pokemon.RU = await _dataInfoRepository.SaveJsonInDb(pokemonJson.RU);
            pokemon.CO = await _dataInfoRepository.SaveJsonInDb(pokemonJson.CO);
            pokemon.CN = await _dataInfoRepository.SaveJsonInDb(pokemonJson.CN);
            pokemon.JP = await _dataInfoRepository.SaveJsonInDb(pokemonJson.JP);
            pokemon.TypeEvolution = pokemonJson.TypeEvolution;
            pokemon.StatPv = pokemonJson.StatPv;
            pokemon.StatAttaque = pokemonJson.StatAttaque;
            pokemon.StatDefense = pokemonJson.StatDefense;
            pokemon.StatAttaqueSpe = pokemonJson.StatAttaqueSpe;
            pokemon.StatDefenseSpe = pokemonJson.StatDefenseSpe;
            pokemon.StatVitesse = pokemonJson.StatVitesse;
            pokemon.StatTotal = pokemonJson.StatTotal;
            pokemon.Generation = pokemonJson.Generation;
            pokemon.UrlImg = pokemonJson.UrlImg;
            pokemon.UrlSprite = pokemonJson.UrlSprite;
        }

        private void UpdateInfo(Pokemon entity, bool edit = false)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;

            if (!edit)
            {
                entity.UserCreation = "System";
                entity.DateCreation = DateTime.Now;
                entity.versionModification = 1;
            }
            else
                entity.versionModification += 1;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => (IUnitOfWork)this._context;
        #endregion
    }
}
