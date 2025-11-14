using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Utils;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class PokemonRepository : Repository<Pokemon>, IRepositoryExtendsPokemon<Pokemon>
    {
        #region Fields
        private readonly DataInfoRepository _repositoryDI;
        private readonly TypePokRepository _repositoryTP;
        private readonly AbilityRepository _repositoryTL;
        private readonly AttackRepository _repositoryAT;
        private readonly TypeAttackRepository _repositoryTA;
        private readonly GameRepository _repositoryG;
        private readonly Pokemon_TypePokRepository _repositoryPTP;
        private readonly Pokemon_WeaknessRepository _repositoryPW;
        private readonly Pokemon_AttackRepository _repositoryPAT;
        private readonly Pokemon_AbilityRepository _repositoryPT;
        #endregion

        #region Constructor
        public PokemonRepository(ScrapingContext context) : base(context)
        {
            _repositoryDI = new DataInfoRepository(context);
            _repositoryTP = new TypePokRepository(context);
            _repositoryTL = new AbilityRepository(context);
            _repositoryAT = new AttackRepository(context);
            _repositoryTA = new TypeAttackRepository(context);
            _repositoryPTP = new Pokemon_TypePokRepository(context);
            _repositoryPW = new Pokemon_WeaknessRepository(context);
            _repositoryPAT = new Pokemon_AttackRepository(context);
            _repositoryPT = new Pokemon_AbilityRepository(context);
            _repositoryG = new GameRepository(context);
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task SaveJsonInDb(string json)
        {
            List<PokemonJson> pokemonsJson = JsonConvert.DeserializeObject<List<PokemonJson>>(json);
            foreach (PokemonJson pokemonJson in pokemonsJson)
            {
                Pokemon pokemon = new();
                await MapToInstance(pokemon, pokemonJson);
                await AddAsync(pokemon);
            }
        }
        #endregion

        #region Read
        public override async Task<IEnumerable<Pokemon>> Find(Expression<Func<Pokemon, bool>> predicate)
        {
            return await _context.Pokemons
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
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .Where(predicate ?? (s => true))
                .OrderBy(m => Convert.ToInt32(m.Number))
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }

        public override async Task<Pokemon?> SingleOrDefault(Expression<Func<Pokemon, bool>> predicate)
        {
            return await _context.Pokemons
                .Include(p => p.FR)
                .Include(p => p.EN)
                .Include(p => p.ES)
                .Include(p => p.DE)
                .Include(p => p.IT)
                .Include(p => p.RU)
                .Include(p => p.CO)
                .Include(p => p.CN)
                .Include(p => p.JP)
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .Where(predicate ?? (s => true))
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Pokemon?> FirstOrDefaultByName(string name, string lang = Constantes.FR)
        {
            var query = _context.Pokemons
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .AsSplitQuery();

            query = lang switch
            {
                Constantes.FR => query.Include(p => p.FR).Where(p => EF.Functions.Collate(p.FR.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.ES => query.Include(p => p.ES).Where(p => EF.Functions.Collate(p.ES.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.DE => query.Include(p => p.DE).Where(p => EF.Functions.Collate(p.DE.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.IT => query.Include(p => p.IT).Where(p => EF.Functions.Collate(p.IT.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.RU => query.Include(p => p.RU).Where(p => EF.Functions.Collate(p.RU.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.CO => query.Include(p => p.CO).Where(p => EF.Functions.Collate(p.CO.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.CN => query.Include(p => p.CN).Where(p => EF.Functions.Collate(p.CN.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.JP => query.Include(p => p.JP).Where(p => EF.Functions.Collate(p.JP.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                _ => query.Include(p => p.EN).Where(p => EF.Functions.Collate(p.EN.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name))
            };

            return await query
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public override async Task<Pokemon?> Get(long id)
        {
            var pokemon = await _context.Pokemons
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
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            pokemon.EvolvesFrom = await _context.Pokemon_EvolveTo
                .Include(e => e.Pokemon)
                .Include(e => e.EvolveTo)
                .Where(e => e.EvolveToId == pokemon.Id)
                .FirstOrDefaultAsync();

            pokemon.Pokemons_EvolvesTo = await _context.Pokemon_EvolveTo
                .Include(e => e.Pokemon)
                .Include(e => e.EvolveTo)
                .Where(e => e.PokemonId == pokemon.Id)
                .ToListAsync();

            if (pokemon != null)
            {
                // Trier selon l'ordre d'insertion en base (Id de la table de jonction)
                pokemon.Pokemon_TypePoks = pokemon.Pokemon_TypePoks.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Weaknesses = pokemon.Pokemon_Weaknesses.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Abilities = pokemon.Pokemon_Abilities.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Attacks = pokemon.Pokemon_Attacks.OrderBy(t => t.Id).ToList();
            }

            return pokemon;
        }

        public async Task<List<Pokemon>?> GetFamilyAndVariant(long evolutionChainId, string displayName, string lang = Constantes.FR)
        {
            var query = _context.Pokemons
               .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
               .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
               .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
               .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
               .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
               .Include(m => m.Game)
               .AsSplitQuery();

            query = lang switch
            {
                Constantes.FR => query.Include(p => p.FR).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.FR.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.ES => query.Include(p => p.ES).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.ES.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.DE => query.Include(p => p.DE).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.DE.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.IT => query.Include(p => p.IT).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.IT.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.RU => query.Include(p => p.RU).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.RU.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.CO => query.Include(p => p.CO).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.CO.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.CN => query.Include(p => p.CN).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.CN.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                Constantes.JP => query.Include(p => p.JP).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.JP.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName))),
                _ => query.Include(p => p.EN).Where(p => p.TypeEvolution == Constantes.NormalEvolution || (EF.Functions.Collate(p.EN.DisplayName, "SQL_Latin1_General_CP1_CI_AI").Contains(displayName)))
            };

            var pokemons = await query
                .Where(p => p.EvolutionChainId == evolutionChainId)
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            if (pokemons != null)
            {
                foreach (var pokemon in pokemons)
                {
                    var queryEvolveFrom = _context.Pokemon_EvolveTo
                       .Include(e => e.Pokemon)
                       .Include(e => e.EvolveTo)
                       .AsSplitQuery();

                    queryEvolveFrom = lang switch
                    {
                        Constantes.FR => queryEvolveFrom.Include(p => p.EvolveTo.FR),
                        Constantes.ES => queryEvolveFrom.Include(p => p.EvolveTo.ES),
                        Constantes.DE => queryEvolveFrom.Include(p => p.EvolveTo.DE),
                        Constantes.IT => queryEvolveFrom.Include(p => p.EvolveTo.IT),
                        Constantes.RU => queryEvolveFrom.Include(p => p.EvolveTo.RU),
                        Constantes.CO => queryEvolveFrom.Include(p => p.EvolveTo.CO),
                        Constantes.CN => queryEvolveFrom.Include(p => p.EvolveTo.CN),
                        Constantes.JP => queryEvolveFrom.Include(p => p.EvolveTo.JP),
                        _ => queryEvolveFrom.Include(p => p.EvolveTo.EN)
                    };

                    pokemon.EvolvesFrom = await queryEvolveFrom
                        .Where(e => e.EvolveToId == pokemon.Id)
                        .FirstOrDefaultAsync();

                    var queryEvolvesTo = _context.Pokemon_EvolveTo
                       .Include(e => e.Pokemon)
                       .Include(e => e.EvolveTo)
                       .AsSplitQuery();

                    queryEvolvesTo = lang switch
                    {
                        Constantes.FR => queryEvolveFrom.Include(p => p.EvolveTo.FR),
                        Constantes.ES => queryEvolveFrom.Include(p => p.EvolveTo.ES),
                        Constantes.DE => queryEvolveFrom.Include(p => p.EvolveTo.DE),
                        Constantes.IT => queryEvolveFrom.Include(p => p.EvolveTo.IT),
                        Constantes.RU => queryEvolveFrom.Include(p => p.EvolveTo.RU),
                        Constantes.CO => queryEvolveFrom.Include(p => p.EvolveTo.CO),
                        Constantes.CN => queryEvolveFrom.Include(p => p.EvolveTo.CN),
                        Constantes.JP => queryEvolveFrom.Include(p => p.EvolveTo.JP),
                        _ => queryEvolveFrom.Include(p => p.EvolveTo.EN)
                    };

                    pokemon.Pokemons_EvolvesTo = await queryEvolvesTo
                        .Where(e => e.PokemonId == pokemon.Id)
                        .ToListAsync();

                    if (pokemon != null)
                    {
                        // Trier selon l'ordre d'insertion en base (Id de la table de jonction)
                        pokemon.Pokemon_TypePoks = pokemon.Pokemon_TypePoks.OrderBy(t => t.Id).ToList();
                        pokemon.Pokemon_Weaknesses = pokemon.Pokemon_Weaknesses.OrderBy(t => t.Id).ToList();
                        pokemon.Pokemon_Abilities = pokemon.Pokemon_Abilities.OrderBy(t => t.Id).ToList();
                        pokemon.Pokemon_Attacks = pokemon.Pokemon_Attacks.OrderBy(t => t.Id).ToList();
                    }
                }
            }

            return pokemons;
        }

        public override async Task<Pokemon?> GetByGuid(Guid guid)
        {
            var pokemon = await _context.Pokemons
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
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Guid.Equals(guid));

            if (pokemon != null)
            {
                // Trier selon l'ordre d'insertion en base (Id de la table de jonction)
                pokemon.Pokemon_TypePoks = pokemon.Pokemon_TypePoks.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Weaknesses = pokemon.Pokemon_Weaknesses.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Abilities = pokemon.Pokemon_Abilities.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Attacks = pokemon.Pokemon_Attacks.OrderBy(t => t.Id).ToList();
            }

            return pokemon;
        }

        public override IQueryable<Pokemon> Query()
        {
            return _context.Pokemons
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
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .AsNoTracking()
                .AsSplitQuery()
                .AsQueryable();
        }

        public override async Task<IEnumerable<Pokemon>> GetAll()
        {
            return await _context.Pokemons
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
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pokemon>> GetAllByLang(string lang = "FR")
        {
            var query = _context.Pokemons
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .AsSplitQuery();

            query = lang switch
            {
                Constantes.FR => query.Include(p => p.FR),
                Constantes.ES => query.Include(p => p.ES),
                Constantes.DE => query.Include(p => p.DE),
                Constantes.IT => query.Include(p => p.IT),
                Constantes.RU => query.Include(p => p.RU),
                Constantes.CO => query.Include(p => p.CO),
                Constantes.CN => query.Include(p => p.CN),
                Constantes.JP => query.Include(p => p.JP),
                _ => query.Include(p => p.EN)
            };

            return await query
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pokemon>> FindByNameAsync(string name, string lang = "FR")
        {
            lang = lang.ToUpper();

            IQueryable<Pokemon> query = _context.Pokemons
                .Include(p => p.Game)
                .Include(p => p.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(p => p.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .AsSplitQuery();

            query = lang switch
            {
                Constantes.FR => query.Include(p => p.FR).Where(p => EF.Functions.Collate(p.FR.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.ES => query.Include(p => p.ES).Where(p => EF.Functions.Collate(p.ES.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.DE => query.Include(p => p.DE).Where(p => EF.Functions.Collate(p.DE.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.IT => query.Include(p => p.IT).Where(p => EF.Functions.Collate(p.IT.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.RU => query.Include(p => p.RU).Where(p => EF.Functions.Collate(p.RU.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.CO => query.Include(p => p.CO).Where(p => EF.Functions.Collate(p.CO.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.CN => query.Include(p => p.CN).Where(p => EF.Functions.Collate(p.CN.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                Constantes.JP => query.Include(p => p.JP).Where(p => EF.Functions.Collate(p.JP.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name)),
                _ => query.Include(p => p.EN).Where(p => EF.Functions.Collate(p.EN.Name, "SQL_Latin1_General_CP1_CI_AI").Contains(name))
            };

            return await query
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Pokemon?> GetById(int id, string lang = "FR")
        {
            lang = lang.ToUpper();

            IQueryable<Pokemon> query = _context.Pokemons
                .Include(p => p.Game)
                .Include(p => p.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(p => p.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .AsSplitQuery();

            query = lang switch
            {
                Constantes.FR => query.Include(p => p.FR),
                Constantes.ES => query.Include(p => p.ES),
                Constantes.DE => query.Include(p => p.DE),
                Constantes.IT => query.Include(p => p.IT),
                Constantes.RU => query.Include(p => p.RU),
                Constantes.CO => query.Include(p => p.CO),
                Constantes.CN => query.Include(p => p.CN),
                Constantes.JP => query.Include(p => p.JP),
                _ => query.Include(p => p.EN)
            };

            var pokemon = await query
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsSplitQuery().FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon != null)
            {
                var queryEvolveFrom = _context.Pokemon_EvolveTo
                       .Include(e => e.Pokemon)
                       .Include(e => e.EvolveTo)
                       .AsSplitQuery();

                queryEvolveFrom = lang switch
                {
                    Constantes.FR => queryEvolveFrom.Include(p => p.Pokemon.FR),
                    Constantes.ES => queryEvolveFrom.Include(p => p.Pokemon.ES),
                    Constantes.DE => queryEvolveFrom.Include(p => p.Pokemon.DE),
                    Constantes.IT => queryEvolveFrom.Include(p => p.Pokemon.IT),
                    Constantes.RU => queryEvolveFrom.Include(p => p.Pokemon.RU),
                    Constantes.CO => queryEvolveFrom.Include(p => p.Pokemon.CO),
                    Constantes.CN => queryEvolveFrom.Include(p => p.Pokemon.CN),
                    Constantes.JP => queryEvolveFrom.Include(p => p.Pokemon.JP),
                    _ => queryEvolveFrom.Include(p => p.Pokemon.EN)
                };

                pokemon.EvolvesFrom = await queryEvolveFrom
                    .Where(e => e.EvolveToId == pokemon.Id)
                    .FirstOrDefaultAsync();

                var queryEvolvesTo = _context.Pokemon_EvolveTo
                   .Include(e => e.Pokemon)
                   .Include(e => e.EvolveTo)
                   .AsSplitQuery();

                queryEvolvesTo = lang switch
                {
                    Constantes.FR => queryEvolveFrom.Include(p => p.EvolveTo.FR),
                    Constantes.ES => queryEvolveFrom.Include(p => p.EvolveTo.ES),
                    Constantes.DE => queryEvolveFrom.Include(p => p.EvolveTo.DE),
                    Constantes.IT => queryEvolveFrom.Include(p => p.EvolveTo.IT),
                    Constantes.RU => queryEvolveFrom.Include(p => p.EvolveTo.RU),
                    Constantes.CO => queryEvolveFrom.Include(p => p.EvolveTo.CO),
                    Constantes.CN => queryEvolveFrom.Include(p => p.EvolveTo.CN),
                    Constantes.JP => queryEvolveFrom.Include(p => p.EvolveTo.JP),
                    _ => queryEvolveFrom.Include(p => p.EvolveTo.EN)
                };

                pokemon.Pokemons_EvolvesTo = await queryEvolvesTo
                    .Where(e => e.PokemonId == pokemon.Id)
                    .ToListAsync();


                // Trier selon l'ordre d'insertion en base (Id de la table de jonction)
                pokemon.Pokemon_TypePoks = pokemon.Pokemon_TypePoks.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Weaknesses = pokemon.Pokemon_Weaknesses.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Abilities = pokemon.Pokemon_Abilities.OrderBy(t => t.Id).ToList();
                pokemon.Pokemon_Attacks = pokemon.Pokemon_Attacks.OrderBy(t => t.Id).ToList();
            }

            return pokemon;
        }


        public async Task<IEnumerable<Pokemon>> GetAllLight(int? gen = null, bool desc = false, int max = 0, string lang = "FR")
        {
            var query = _context.Pokemons
                .Include(m => m.Pokemon_TypePoks)
                    .ThenInclude(u => u.TypePok)
                .AsSplitQuery();

            query = lang switch
            {
                "FR" => query.Include(p => p.FR),
                "ES" => query.Include(p => p.ES),
                "DE" => query.Include(p => p.DE),
                "IT" => query.Include(p => p.IT),
                "RU" => query.Include(p => p.RU),
                "CO" => query.Include(p => p.CO),
                "CN" => query.Include(p => p.CN),
                "JP" => query.Include(p => p.JP),
                _ => query.Include(p => p.EN)
            };

            // 🔹 Filtrer par génération si gen est spécifié
            if (gen.HasValue)
                query = query.Where(m => m.Generation == gen.Value);

            // 🔹 Appliquer le tri
            query = desc
                ? query.OrderByDescending(m => Convert.ToInt32(m.Number))
                : query.OrderBy(m => Convert.ToInt32(m.Number));

            // 🔹 Appliquer une limite côté SQL
            if (max > 0)
                query = query.Take(max);

            var pokemons = await query.ToListAsync();

            // Appliquer le même ordre que la base pour chaque Pokémon
            foreach (var p in pokemons)
            {
                p.Pokemon_TypePoks = p.Pokemon_TypePoks.OrderBy(t => t.Id).ToList();
            }

            return pokemons;
        }

        public async Task<IEnumerable<Pokemon>> GetFamilyWithoutVariantAsync(string family, string lang = "FR")
        {
            string[] vs = family.Split(',');
            List<Pokemon> result = new();

            foreach (var item in vs)
            {
                Pokemon pokemon = await FirstOrDefaultByName(item, lang.ToUpper());
                if (pokemon != null)
                    result.Add(pokemon);
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Pokemon>?> GetFamilyAsync(int evolutionChainId, string displayName, string lang = "FR")
        {
            return await Task.FromResult(await GetFamilyAndVariant(evolutionChainId, displayName, lang));
        }

        public async Task<IEnumerable<Pokemon>> GetAllVariantAsync(string number, string lang = "FR")
        {
            var query = _context.Pokemons
                .Include(m => m.Pokemon_TypePoks).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Weaknesses).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Abilities).ThenInclude(u => u.Ability)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attacks).ThenInclude(u => u.Attack).ThenInclude(u => u.TypeAttack)
                .Include(m => m.Game)
                .AsSplitQuery();

            query = lang switch
            {
                Constantes.FR => query.Include(p => p.FR),
                Constantes.ES => query.Include(p => p.ES),
                Constantes.DE => query.Include(p => p.DE),
                Constantes.IT => query.Include(p => p.IT),
                Constantes.RU => query.Include(p => p.RU),
                Constantes.CO => query.Include(p => p.CO),
                Constantes.CN => query.Include(p => p.CN),
                Constantes.JP => query.Include(p => p.JP),
                _ => query.Include(p => p.EN)
            };

            return await query
                .Where(m => m.Number.Equals(number) && !m.TypeEvolution.Equals("Normal")).OrderBy(m => m.Number)
                .OrderBy(p => Convert.ToInt32(p.Number))
                .AsSplitQuery()
                .ToListAsync();
        }
        #endregion

        #region Generate Quizz
        public async Task<Pokemon> GetPokemonRandom(bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus)
        {
            List<Pokemon> resultFilterGen = await GetPokemonsWithFilterGen(GetAllLight().Result.ToList(), gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8, gen9, genArceus);

            Random random = new Random();
            int numberRandom = random.Next(resultFilterGen.Count);

            return await Task.FromResult(resultFilterGen[numberRandom]);
        }

        public async Task<Pokemon> GetPokemonRandom(ClassQuizz.Quizz quizz)
        {
            List<Pokemon> resultFilterGen = await GetPokemonsWithFilterGen(GetAllLight().Result.ToList(), quizz.Gen1, quizz.Gen2, quizz.Gen3, quizz.Gen4, quizz.Gen5, quizz.Gen6, quizz.Gen7, quizz.Gen8, quizz.Gen9, quizz.GenArceus);

            Random random = new Random();
            int numberRandom = random.Next(resultFilterGen.Count);

            return await Task.FromResult(resultFilterGen[numberRandom]);
        }

        public async Task<Pokemon> GetPokemonRandom(ClassQuizz.Quizz quizz, List<Pokemon> alreadySelected)
        {
            List<Pokemon> resultFilterGen = await GetPokemonsWithFilterGen(GetAllLight().Result.ToList(), quizz.Gen1, quizz.Gen2, quizz.Gen3, quizz.Gen4, quizz.Gen5, quizz.Gen6, quizz.Gen7, quizz.Gen8, quizz.Gen9, quizz.GenArceus);

            Random random = new Random();
            int numberRandom = random.Next(resultFilterGen.Count);
            Pokemon pokemon = alreadySelected.Find(m => m.Id.Equals(resultFilterGen[numberRandom].Id));

            while (pokemon != null)
            {
                numberRandom = random.Next(resultFilterGen.Count);
                pokemon = alreadySelected.Find(m => m.Id.Equals(resultFilterGen[numberRandom].Id));
            }

            return await Task.FromResult(resultFilterGen[numberRandom]);
        }

        public async Task<Pokemon> GetPokemonRandom(ClassQuizz.Quizz quizz, TypePok typePok, List<Pokemon> alreadySelected)
        {
            List<Pokemon> resultFilterGen = await GetPokemonsWithFilterGen(GetAllLight().Result.ToList(), quizz.Gen1, quizz.Gen2, quizz.Gen3, quizz.Gen4, quizz.Gen5, quizz.Gen6, quizz.Gen7, quizz.Gen8, quizz.Gen9, quizz.GenArceus);
            resultFilterGen = await GetPokemonByFilterType(resultFilterGen, typePok.Name_EN);

            Random random = new Random();
            int numberRandom = random.Next(resultFilterGen.Count);
            Pokemon pokemon = alreadySelected.Find(m => m.Id.Equals(resultFilterGen[numberRandom].Id));

            while (pokemon != null)
            {
                numberRandom = random.Next(resultFilterGen.Count);
                pokemon = alreadySelected.Find(m => m.Id.Equals(resultFilterGen[numberRandom].Id));

                if (alreadySelected.Count.Equals(resultFilterGen.Count))
                    break;
            }

            return await Task.FromResult(resultFilterGen[numberRandom]);
        }
        #endregion

        public async Task<bool> ImportJsonToDb(string json)
        {
            try
            {
                List<PokemonExportJson> pokemonsJson = JsonConvert.DeserializeObject<List<PokemonExportJson>>(json);
                foreach (PokemonExportJson pokemonJson in pokemonsJson)
                {
                    Pokemon pokemon = new();
                    await MapToInstanceImport(pokemon, pokemonJson);
                    Console.WriteLine("Pokemon:" + pokemon.FR.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return await Task.FromResult(true);
        }

        public async Task SaveInfoPokemonAttackInDB(string json)
        {
            List<string> Erreurs = new();
            List<PokemonPokeBipJson> pokemonsPokeBipJson = JsonConvert.DeserializeObject<List<PokemonPokeBipJson>>(json);
            List<Attack> attacks = new();
            List<Pokemon_Ability> pokemon_Abilities = new();

            List<Attack> attackAlreadyExist = _repositoryAT.GetAll().Result.ToList();
            List<Ability> talents = _repositoryTL.GetAll().Result.ToList();
            List<Pokemon_Ability> pokemonTalent_alreadyExist = _repositoryPT.GetAll().Result.ToList();

            #region Update Info + Add/Update Attack
            try
            {
                foreach (PokemonPokeBipJson pokemonPokeBipJson in pokemonsPokeBipJson)
                {
                    Pokemon pokemon = _context.Pokemons.FirstOrDefault(p => p.FR.Name == pokemonPokeBipJson.Name);

                    if (pokemon != null)
                    {
                        int pokemonId = Convert.ToInt32(pokemon.Id_FR);
                        DataInfo dataInfo = _repositoryDI.Get(pokemonId).Result;

                        pokemon.BasicHappiness = pokemonPokeBipJson.BasicHappiness;
                        pokemon.CaptureRate = pokemonPokeBipJson.CaptureRate;
                        pokemon.EggMoves = pokemonPokeBipJson.EggMoves;

                        if (!string.IsNullOrEmpty(pokemonPokeBipJson.HiddenSkill))
                        {
                            Ability talent = talents.Find(m => m.Name_FR.Equals(pokemonPokeBipJson.HiddenSkill));
                            if (talent != null)
                            {
                                Pokemon_Ability pokemon_Ability = pokemonTalent_alreadyExist.Find(m => m.PokemonId.Equals(pokemon.Id) && m.AbilityId.Equals(talent.Id));

                                if (talent != null && pokemon_Ability == null)
                                {
                                    Pokemon_Ability newPokemon_Talent = new()
                                    {
                                        PokemonId = pokemon.Id,
                                        AbilityId = talent.Id,
                                        IsHidden = true
                                    };

                                    pokemon_Abilities.Add(newPokemon_Talent);
                                    pokemonTalent_alreadyExist.Add(newPokemon_Talent);
                                }
                            }
                            else
                            {
                                Erreurs.Add("Talent Manquant: " + pokemon.FR.Name + ": " + pokemonPokeBipJson.HiddenSkill);
                            }
                        }

                        pokemonPokeBipJson.AttackJsons.ForEach(attackJson =>
                        {
                            Attack attack = attackAlreadyExist.FirstOrDefault(a => a.Name_FR == attackJson.Name);

                            TypeAttack typeAttack = new TypeAttack();
                            switch (attackJson.Category)
                            {
                                case "Physique":
                                    typeAttack = _repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités Physiques").Result;
                                    break;
                                case "Spéciale":
                                    typeAttack = _repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités Spéciales").Result;
                                    break;
                                case "Statut":
                                    typeAttack = _repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités de Statut").Result;
                                    break;
                            }

                            if (attack == null)
                            {
                                attack = new Attack
                                {
                                    Name_FR = attackJson.Name,
                                    Name_EN = attackJson.NameEN,
                                    Description_FR = attackJson.Description,
                                    Power = attackJson.Power,
                                    Precision = attackJson.Precision,
                                    PP = attackJson.PP,
                                    TypeAttack = typeAttack,
                                    TypePok = _repositoryTP.Find(t => t.Name_FR == attackJson.Type).Result.FirstOrDefault()
                                };
                                attacks.Add(attack);
                                attackAlreadyExist.Add(attack);
                            }
                            else
                            {
                                attack.TypeAttack = typeAttack;
                                attack.TypePok = _repositoryTP.Find(t => t.Name_FR == attackJson.Type).Result.FirstOrDefault();
                            }
                        });
                    }
                }

                foreach (string erreur in Erreurs)
                {
                    Console.WriteLine(erreur);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await _repositoryPT.AddRangeAsync(pokemon_Abilities);
            await _repositoryAT.AddRangeAsync(attacks);
            _context.SaveChanges();
            #endregion

            attackAlreadyExist = _repositoryAT.GetAll().Result.ToList();
            List<Pokemon_Attack> pokemon_AttacksAlreadyExist = _repositoryPAT.GetAll().Result.ToList();
            List<Pokemon_Attack> pokemon_Attacks = new();

            foreach (PokemonPokeBipJson pokemonPokeBipJson in pokemonsPokeBipJson)
            {
                Pokemon pokemon = _context.Pokemons.FirstOrDefault(p => p.FR.Name == pokemonPokeBipJson.Name);

                pokemonPokeBipJson.AttackJsons.ForEach(attackJson =>
                {
                    Attack attack = attackAlreadyExist.FirstOrDefault(a => a.Name_FR == attackJson.Name);
                    Pokemon_Attack pokemon_Attack = pokemon_AttacksAlreadyExist.FirstOrDefault(m => m.PokemonId == pokemon.Id && m.AttackId == attack.Id);

                    if (pokemon != null && pokemon.Game == null)
                    {
                        switch (attackJson.Game)
                        {
                            case Constantes.RedBlueUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.RedBlue_Name_FR).Result;
                                break;

                            case Constantes.YellowUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Yellow_Name_FR).Result;
                                break;

                            case Constantes.GoldSilverUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.GoldSilver_Name_FR).Result;
                                break;

                            case Constantes.CrystalUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Crystal_Name_FR).Result;
                                break;

                            case Constantes.RubySapphireUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.RubySapphire_Name_FR).Result;
                                break;

                            case Constantes.EmeraldUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Emerald_Name_FR).Result;
                                break;

                            case Constantes.FireRedLeafGreenUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.FireRedLeafGreen_Name_FR).Result;
                                break;

                            case Constantes.DiamondPearlUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.DiamondPearl_Name_FR).Result;
                                break;

                            case Constantes.PlatinumUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Platinum_Name_FR).Result;
                                break;

                            case Constantes.HeartGoldSoulSilverUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.HeartGoldSoulSilver_Name_FR).Result;
                                break;

                            case Constantes.BlackWhiteUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.BlackWhite_Name_FR).Result;
                                break;

                            case Constantes.Black2White2Url:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Black2White2_Name_FR).Result;
                                break;

                            case Constantes.X_YUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.X_Y_Name_FR).Result;
                                break;

                            case Constantes.SunMoonUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.SunMoon_Name_FR).Result;
                                break;

                            case Constantes.UltraSunUltraMoonUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.UltraSunUltraMoon_Name_FR).Result;
                                break;

                            case Constantes.SwordShieldUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.SwordShield_Name_FR).Result;
                                break;

                            case Constantes.ShiningDiamondShiningPearlUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.ShiningDiamondShiningPearl_Name_FR).Result;
                                break;

                            case Constantes.ArceusUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Arceus_Name_FR).Result;
                                break;

                            case Constantes.ScarletVioletUrl:
                                pokemon.Game = _repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.ScarletViolet_Name_FR).Result;
                                break;

                        }
                    }

                    if (pokemon_Attack == null)
                    {
                        pokemon_Attack = new()
                        {
                            PokemonId = pokemon.Id,
                            Pokemon = null,
                            AttackId = attack.Id,
                            Attack = null,
                            Level = attackJson.Level,
                            CTCS = attackJson.CTCS,
                            TypeLearn = attackJson.TypeLearn
                        };

                        pokemon_Attacks.Add(pokemon_Attack);
                        pokemon_AttacksAlreadyExist.Add(pokemon_Attack);
                    }
                });
            }

            await _repositoryPAT.AddRangeAsync(pokemon_Attacks);
            _context.SaveChanges();
        }
        #endregion

        #region Private Methods
        private async Task MapToInstanceImport(Pokemon pokemon, PokemonExportJson pokemonJson)
        {
            pokemon.Number = pokemonJson.Number;
            pokemon.FR = await MapToInstanceImport(pokemonJson.FR);
            pokemon.EN = await MapToInstanceImport(pokemonJson.EN);
            pokemon.ES = await MapToInstanceImport(pokemonJson.ES);
            pokemon.IT = await MapToInstanceImport(pokemonJson.IT);
            pokemon.DE = await MapToInstanceImport(pokemonJson.DE);
            pokemon.RU = await MapToInstanceImport(pokemonJson.RU);
            pokemon.CO = await MapToInstanceImport(pokemonJson.CO);
            pokemon.CN = await MapToInstanceImport(pokemonJson.CN);
            pokemon.JP = await MapToInstanceImport(pokemonJson.JP);
            pokemon.TypeEvolution = pokemonJson.TypeEvolution;
            pokemon.StatPv = Convert.ToInt32(pokemonJson.StatPv);
            pokemon.StatAttack = Convert.ToInt32(pokemonJson.StatAttaque);
            pokemon.StatDefense = Convert.ToInt32(pokemonJson.StatDefense);
            pokemon.StatAttackSpe = Convert.ToInt32(pokemonJson.StatAttaqueSpe);
            pokemon.StatDefenseSpe = Convert.ToInt32(pokemonJson.StatDefenseSpe);
            pokemon.StatSpeed = Convert.ToInt32(pokemonJson.StatVitesse);
            pokemon.StatTotal = Convert.ToInt32(pokemonJson.StatTotal);
            pokemon.Generation = Convert.ToInt32(pokemonJson.Generation);
            pokemon.EggMoves = pokemonJson.EggMoves;
            pokemon.CaptureRate = pokemonJson.CaptureRate;
            pokemon.BasicHappiness = pokemonJson.BasicHappiness;
            pokemon.UrlImg = pokemonJson.UrlImg;
            pokemon.PathImgLegacy = pokemonJson.PathImgLegacy;
            pokemon.PathImgNormal = pokemonJson.PathImgNormal;
            pokemon.PathImgShiny = pokemonJson.PathImgShiny;
            pokemon.UrlSprite = pokemonJson.UrlSprite;
            pokemon.PathSpriteLegacy = pokemonJson.PathSpriteLegacy;
            pokemon.PathSpriteNormal = pokemonJson.PathSpriteNormal;
            pokemon.PathSpriteShiny = pokemonJson.PathSpriteShiny;
            pokemon.UrlSound = pokemonJson.UrlSound;
            pokemon.PathSound = pokemonJson.PathSound;
            pokemon.PathSoundLegacy = pokemonJson.PathSoundLegacy;
            pokemon.PathSoundCurrent = pokemonJson.PathSoundCurrent;
            pokemon.Game = _repositoryG.Find(m => m.Name_EN.Equals(pokemonJson.Game.Name_EN)).Result.FirstOrDefault();

            await AddAsync(pokemon);

            foreach (TypesPokExportJson typePokJson in pokemonJson.Types)
            {
                TypePok typePok = (await _repositoryTP.Find(m => m.Name_EN.Equals(typePokJson.TypePok.Name_EN))).FirstOrDefault();
                if (typePok != null)
                {
                    Pokemon_TypePok pokemon_TypePok = new()
                    {
                        Pokemon = pokemon,
                        TypePok = typePok
                    };
                    await _repositoryPTP.AddAsync(pokemon_TypePok);
                }
            }

            foreach (TypesPokExportJson weaknessJson in pokemonJson.Weaknesses)
            {
                TypePok typePok = (await _repositoryTP.Find(m => m.Name_EN.Equals(weaknessJson.TypePok.Name_EN))).FirstOrDefault();
                if (typePok != null)
                {
                    Pokemon_Weakness pokemon_Weakness = new()
                    {
                        Pokemon = pokemon,
                        TypePok = typePok
                    };
                    await _repositoryPW.AddAsync(pokemon_Weakness);
                }
            }

            foreach (TalentsExportJson talentJson in pokemonJson.Talents)
            {
                Ability talent = (await _repositoryTL.Find(m => m.Name_EN.Equals(talentJson.Talent.Name_EN))).FirstOrDefault();
                if (talent != null)
                {
                    Pokemon_Ability pokemon_Ability = new()
                    {
                        Pokemon = pokemon,
                        Ability = talent,
                        IsHidden = talentJson.IsHidden
                    };
                    await _repositoryPT.AddAsync(pokemon_Ability);
                }
            }

            foreach (AttaquesExportJson attackJson in pokemonJson.Attaques)
            {
                Attack attack = _repositoryAT.Find(m => m.Name_EN.Equals(attackJson.Attaque.Name_EN)).Result.FirstOrDefault();
                if (attack != null)
                {
                    Pokemon_Attack pokemon_Attack = new()
                    {
                        Pokemon = pokemon,
                        Attack = attack,
                        TypeLearn = attackJson.TypeLearn,
                        Level = attackJson.Level,
                        CTCS = attackJson.CTCS
                    };
                    await _repositoryPAT.AddAsync(pokemon_Attack);
                }
            }
        }

        public async Task<DataInfo> MapToInstanceImport(DataInfoExportJson dataInfoJson)
        {
            DataInfo dataInfo = new()
            {
                Name = dataInfoJson.Name,
                DisplayName = dataInfoJson.DisplayName,
                DescriptionVx = dataInfoJson.DescriptionVx,
                DescriptionVy = dataInfoJson.DescriptionVy,
                Size = dataInfoJson.Size,
                Category = dataInfoJson.Category,
                Weight = dataInfoJson.Weight,
                //Talent = dataInfoJson.Talent,
                //DescriptionTalent = dataInfoJson.DescriptionTalent,
                //Types = dataInfoJson.Types,
                //Weakness = dataInfoJson.Weakness,
                Evolutions = dataInfoJson.Evolutions,
                WhenEvolution = dataInfoJson.WhenEvolution,
                NextUrl = dataInfoJson.NextUrl
            };

            await _repositoryDI.AddAsync(dataInfo);
            return await Task.FromResult(dataInfo);
        }

        private async Task MapToInstance(Pokemon pokemon, PokemonJson pokemonJson)
        {
            pokemon.Number = pokemonJson.Number;
            pokemon.FR = await _repositoryDI.SaveJsonInDb(pokemonJson.FR);
            pokemon.EN = await _repositoryDI.SaveJsonInDb(pokemonJson.EN);
            pokemon.ES = await _repositoryDI.SaveJsonInDb(pokemonJson.ES);
            pokemon.IT = await _repositoryDI.SaveJsonInDb(pokemonJson.IT);
            pokemon.DE = await _repositoryDI.SaveJsonInDb(pokemonJson.DE);
            pokemon.RU = await _repositoryDI.SaveJsonInDb(pokemonJson.RU);
            pokemon.CO = await _repositoryDI.SaveJsonInDb(pokemonJson.CO);
            pokemon.CN = await _repositoryDI.SaveJsonInDb(pokemonJson.CN);
            pokemon.JP = await _repositoryDI.SaveJsonInDb(pokemonJson.JP);
            pokemon.TypeEvolution = pokemonJson.TypeEvolution;
            pokemon.StatPv = pokemonJson.StatPv;
            pokemon.StatAttack = pokemonJson.StatAttaque;
            pokemon.StatDefense = pokemonJson.StatDefense;
            pokemon.StatAttackSpe = pokemonJson.StatAttaqueSpe;
            pokemon.StatDefenseSpe = pokemonJson.StatDefenseSpe;
            pokemon.StatSpeed = pokemonJson.StatVitesse;
            pokemon.StatTotal = pokemonJson.StatTotal;
            pokemon.Generation = pokemonJson.Generation;
            pokemon.UrlImg = pokemonJson.UrlImg;
            pokemon.UrlSprite = pokemonJson.UrlSprite;
        }

        private async Task<List<Pokemon>> GetPokemonsWithFilterGen(List<Pokemon> result, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool gen9, bool genArceus)
        {
            List<Pokemon> resultFilterGen = new List<Pokemon>();

            if (gen1)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(1) && m.TypeEvolution.Equals(Constantes.NormalEvolution)));
            if (gen2)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(2) && m.TypeEvolution.Equals(Constantes.NormalEvolution)));
            if (gen3)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(3) && m.TypeEvolution.Equals(Constantes.NormalEvolution)));
            if (gen4)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(4) && m.TypeEvolution.Equals(Constantes.NormalEvolution)));
            if (gen5)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(5) && m.TypeEvolution.Equals(Constantes.NormalEvolution)));
            if (gen6)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(6) || m.TypeEvolution.Equals(Constantes.MegaEvolution)).Distinct());
            if (gen7)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(7) || m.TypeEvolution.Equals(Constantes.Alola)).Distinct());
            if (gen8)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(8) || m.TypeEvolution.Equals(Constantes.Galar) || m.TypeEvolution.Equals(Constantes.GigaEvolution)).Distinct());
            if (gen9)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(9) || m.TypeEvolution.Equals(Constantes.Paldea)));
            if (genArceus)
                resultFilterGen.AddRange(result.FindAll(m => m.Generation.Equals(0) || m.TypeEvolution.Equals(Constantes.Hisui)).Distinct());

            if (resultFilterGen.Count.Equals(0))
                resultFilterGen = result;

            return await Task.FromResult(resultFilterGen);
        }

        private async Task<List<Pokemon>> GetPokemonsWithFilterType(List<Pokemon> resultFilterGen, bool steel, bool fighting, bool dragon, bool water, bool electric, bool fairy, bool fire, bool ice, bool bug, bool normal, bool grass, bool poison, bool psychic, bool rock, bool ground, bool ghost, bool dark, bool flying)
        {
            List<Pokemon> resultFilterType = new List<Pokemon>();
            if (steel)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Steel));

            if (fighting)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Fighting));

            if (dragon)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Dragon));

            if (water)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Water));

            if (electric)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Electric));

            if (fairy)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Fairy));

            if (fire)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Fire));

            if (ice)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Ice));

            if (bug)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Bug));

            if (normal)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Normal));

            if (grass)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Grass));

            if (poison)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Poison));

            if (psychic)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Psychic));

            if (rock)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Rock));

            if (ground)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Ground));

            if (ghost)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Ghost));

            if (dark)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Dark));

            if (flying)
                resultFilterType.AddRange(await GetPokemonByFilterType(resultFilterGen, Constantes.Flying));

            if (resultFilterType.Count.Equals(0))
                resultFilterType = resultFilterGen;

            return await Task.FromResult(resultFilterType);
        }

        private async Task<List<Pokemon>> GetPokemonByFilterType(IEnumerable<Pokemon> resultFilterGen, string typeName)
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            TypePok typePok = await _repositoryTP.Single(m => m.Name_EN.Equals(typeName));
            List<Pokemon_TypePok> pokemonTypePoks = await _repositoryPTP.GetPokemonsByTypePok(typePok.Id);
            foreach (Pokemon_TypePok pokemonTypePok in pokemonTypePoks)
            {
                Pokemon pokemon = resultFilterGen.Single(m => m.Id.Equals(pokemonTypePok.PokemonId));
                if (pokemon != null)
                    pokemons.Add(pokemon);
            }
            return pokemons;
        }
        #endregion
    }
}
