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
        #endregion

        #region Constructor
        public PokemonRepository(ScrapingContext context)
        {
            this._context = context;
            this._dataInfoRepository = new DataInfoRepository(context);
        }
        #endregion

        #region Public Methods
        #region Create
        public void Add(Pokemon entity)
        {
            entity.UserCreation = "System";
            entity.DateCreation = DateTime.Now;
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification = 1;
            
            this._context.Pokemons.Add(entity);
        }

        public void AddRange(IEnumerable<Pokemon> entities)
        {
            this._context.Pokemons.AddRange(entities);
        }

        public void SaveJsonInDb(string json)
        {
            List<PokemonJson> pokemonsJson = JsonConvert.DeserializeObject<List<PokemonJson>>(json);
            foreach (PokemonJson pokemonJson in pokemonsJson)
            {
                Pokemon pokemon = new();
                this.MapToInstance(pokemon, pokemonJson);
                this.Add(pokemon);
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
                .Where(predicate)
                .AsQueryable();
        }

        public Pokemon Get(int id)
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
                .Single(x => x.Id.Equals(id));
        }
        
        public IEnumerable<Pokemon> GetAll()
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
        #endregion

        #region Update
        public void Edit(Pokemon entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.Update(entity);
        }

        public void EditRange(IEnumerable<Pokemon> entities)
        {
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

        public Pokemon? SingleOrDefault(Expression<Func<Pokemon, bool>> predicate)
        {
            return this._context.Pokemons.SingleOrDefault(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        public void MapToInstance(Pokemon pokemon, PokemonJson pokemonJson)
        {
            pokemon.Number = pokemonJson.Number;
            pokemon.FR = _dataInfoRepository.SaveJsonInDb(pokemonJson.FR);
            pokemon.EN = _dataInfoRepository.SaveJsonInDb(pokemonJson.EN);
            pokemon.ES = _dataInfoRepository.SaveJsonInDb(pokemonJson.ES);
            pokemon.IT = _dataInfoRepository.SaveJsonInDb(pokemonJson.IT);
            pokemon.DE = _dataInfoRepository.SaveJsonInDb(pokemonJson.DE);
            pokemon.RU = _dataInfoRepository.SaveJsonInDb(pokemonJson.RU);
            pokemon.CO = _dataInfoRepository.SaveJsonInDb(pokemonJson.CO);
            pokemon.CN = _dataInfoRepository.SaveJsonInDb(pokemonJson.CN);
            pokemon.JP = _dataInfoRepository.SaveJsonInDb(pokemonJson.JP);
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
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => (IUnitOfWork)this._context;
        #endregion
    }
}
