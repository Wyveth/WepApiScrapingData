using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Utils;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class PokemonRepository : IRepositoryExtendsPokemon<Pokemon>
    {
        #region Fields
        private readonly ScrapingContext _context;
        private readonly DataInfoRepository _repositoryDI;
        private readonly TypePokRepository _repositoryTP;
        private readonly TalentRepository _repositoryTL;
        private readonly AttaqueRepository _repositoryAT;
        private readonly TypeAttaqueRepository _repositoryTA;
        private readonly GameRepository _repositoryG;
        private readonly Pokemon_TypePokRepository _repositoryPTP;
        private readonly Pokemon_WeaknessRepository _repositoryPW;
        private readonly Pokemon_AttaqueRepository _repositoryPAT;
        private readonly Pokemon_TalentRepository _repositoryPT;
        #endregion

        #region Constructor
        public PokemonRepository(ScrapingContext context)
        {
            this._context = context;
            this._repositoryDI = new DataInfoRepository(context);
            this._repositoryTP = new TypePokRepository(context);
            this._repositoryTL = new TalentRepository(context);
            this._repositoryAT = new AttaqueRepository(context);
            this._repositoryTA = new TypeAttaqueRepository(context);
            this._repositoryPTP = new Pokemon_TypePokRepository(context);
            this._repositoryPW = new Pokemon_WeaknessRepository(context);
            this._repositoryPAT = new Pokemon_AttaqueRepository(context);
            this._repositoryPT = new Pokemon_TalentRepository(context);
            this._repositoryG = new GameRepository(context);
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

        public Task SaveInfoPokemonAttackInDB(string json)
        {
            List<string> Erreurs = new();
            List<PokemonPokeBipJson> pokemonsPokeBipJson = JsonConvert.DeserializeObject<List<PokemonPokeBipJson>>(json);
            List<Attaque> attaques = new();
            List<Pokemon_Talent> pokemon_Talents = new();

            List<Attaque> attackAlreadyExist = this._repositoryAT.GetAll().Result.ToList();
            List<Talent> talents = this._repositoryTL.GetAll().Result.ToList();
            List<Pokemon_Talent> pokemonTalent_alreadyExist = this._repositoryPT.GetAll().Result.ToList();

            #region Update Info + Add/Update Attack
            try
            {
                foreach (PokemonPokeBipJson pokemonPokeBipJson in pokemonsPokeBipJson)
                {
                    Pokemon pokemon = this._context.Pokemons.FirstOrDefault(p => p.FR.Name == pokemonPokeBipJson.Name);

                    if (pokemon != null)
                    {
                        int pokemonId = Convert.ToInt32(pokemon.Id_FR);
                        DataInfo dataInfo = this._repositoryDI.Get(pokemonId).Result;

                        pokemon.BasicHappiness = pokemonPokeBipJson.BasicHappiness;
                        pokemon.CaptureRate = pokemonPokeBipJson.CaptureRate;
                        pokemon.EggMoves = pokemonPokeBipJson.EggMoves;

                        if (!string.IsNullOrEmpty(pokemonPokeBipJson.HiddenSkill))
                        {
                            Talent talent = talents.Find(m => m.Name_FR.Equals(pokemonPokeBipJson.HiddenSkill));
                            if (talent != null)
                            {
                                Pokemon_Talent pokemon_Talent = pokemonTalent_alreadyExist.Find(m => m.PokemonId.Equals(pokemon.Id) && m.TalentId.Equals(talent.Id));

                                if (talent != null && pokemon_Talent == null)
                                {
                                    Pokemon_Talent newPokemon_Talent = new()
                                    {
                                        PokemonId = pokemon.Id,
                                        TalentId = talent.Id,
                                        IsHidden = true
                                    };

                                    pokemon_Talents.Add(newPokemon_Talent);
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
                            Attaque attaque = attackAlreadyExist.FirstOrDefault(a => a.Name_FR == attackJson.Name);

                            TypeAttaque typeAttaque = new TypeAttaque();
                            switch (attackJson.Category)
                            {
                                case "Physique":
                                    typeAttaque = this._repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités Physiques").Result;
                                    break;
                                case "Spéciale":
                                    typeAttaque = this._repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités Spéciales").Result;
                                    break;
                                case "Statut":
                                    typeAttaque = this._repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités de Statut").Result;
                                    break;
                            }

                            if (attaque == null)
                            {
                                attaque = new Attaque
                                {
                                    Name_FR = attackJson.Name,
                                    Name_EN = attackJson.NameEN,
                                    Description_FR = attackJson.Description,
                                    Power = attackJson.Power,
                                    Precision = attackJson.Precision,
                                    PP = attackJson.PP,
                                    typeAttaque = typeAttaque,
                                    typePok = this._repositoryTP.Find(t => t.Name_FR == attackJson.Type).FirstOrDefault()
                                };
                                attaques.Add(attaque);
                                attackAlreadyExist.Add(attaque);
                            }
                            else
                            {
                                attaque.typeAttaque = typeAttaque;
                                attaque.typePok = this._repositoryTP.Find(t => t.Name_FR == attackJson.Type).FirstOrDefault();
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

            this._repositoryPT.AddRange(pokemon_Talents);
            this._repositoryAT.AddRange(attaques);
            this._context.SaveChanges();
            #endregion

            attackAlreadyExist = this._repositoryAT.GetAll().Result.ToList();
            List<Pokemon_Attaque> pokemon_AttaquesAlreadyExist = this._repositoryPAT.GetAll().Result.ToList();
            List<Pokemon_Attaque> pokemon_Attaques = new();

            foreach (PokemonPokeBipJson pokemonPokeBipJson in pokemonsPokeBipJson)
            {
                Pokemon pokemon = this._context.Pokemons.FirstOrDefault(p => p.FR.Name == pokemonPokeBipJson.Name);

                pokemonPokeBipJson.AttackJsons.ForEach(attackJson =>
                {
                    Attaque attaque = attackAlreadyExist.FirstOrDefault(a => a.Name_FR == attackJson.Name);
                    Pokemon_Attaque pokemon_Attaque = pokemon_AttaquesAlreadyExist.FirstOrDefault(m => m.PokemonId == pokemon.Id && m.AttaqueId == attaque.Id);

                    if (pokemon != null && pokemon.Game == null)
                    {
                        switch (attackJson.Game)
                        {
                            case Constantes.RedBlueUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.RedBlue_Name_FR).Result;
                                break;

                            case Constantes.YellowUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Yellow_Name_FR).Result;
                                break;

                            case Constantes.GoldSilverUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.GoldSilver_Name_FR).Result;
                                break;

                            case Constantes.CrystalUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Crystal_Name_FR).Result;
                                break;

                            case Constantes.RubySapphireUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.RubySapphire_Name_FR).Result;
                                break;

                            case Constantes.EmeraldUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Emerald_Name_FR).Result;
                                break;

                            case Constantes.FireRedLeafGreenUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.FireRedLeafGreen_Name_FR).Result;
                                break;

                            case Constantes.DiamondPearlUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.DiamondPearl_Name_FR).Result;
                                break;

                            case Constantes.PlatinumUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Platinum_Name_FR).Result;
                                break;

                            case Constantes.HeartGoldSoulSilverUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.HeartGoldSoulSilver_Name_FR).Result;
                                break;

                            case Constantes.BlackWhiteUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.BlackWhite_Name_FR).Result;
                                break;

                            case Constantes.Black2White2Url:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Black2White2_Name_FR).Result;
                                break;

                            case Constantes.X_YUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.X_Y_Name_FR).Result;
                                break;

                            case Constantes.SunMoonUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.SunMoon_Name_FR).Result;
                                break;

                            case Constantes.UltraSunUltraMoonUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.UltraSunUltraMoon_Name_FR).Result;
                                break;

                            case Constantes.SwordShieldUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.SwordShield_Name_FR).Result;
                                break;

                            case Constantes.ShiningDiamondShiningPearlUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.ShiningDiamondShiningPearl_Name_FR).Result;
                                break;

                            case Constantes.ArceusUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.Arceus_Name_FR).Result;
                                break;

                            case Constantes.ScarletVioletUrl:
                                pokemon.Game = this._repositoryG.SingleOrDefault(g => g.Name_FR == Constantes.ScarletViolet_Name_FR).Result;
                                break;

                        }
                    }

                    if (pokemon_Attaque == null)
                    {
                        pokemon_Attaque = new()
                        {
                            PokemonId = pokemon.Id,
                            Pokemon = null,
                            AttaqueId = attaque.Id,
                            Attaque = null,
                            Level = attackJson.Level,
                            CTCS = attackJson.CTCS,
                            TypeLearn = attackJson.TypeLearn
                        };

                        pokemon_Attaques.Add(pokemon_Attaque);
                        pokemon_AttaquesAlreadyExist.Add(pokemon_Attaque);
                    }
                });
            }

            this._repositoryPAT.AddRange(pokemon_Attaques);
            this._context.SaveChanges();

            return Task.FromResult(true);
        }

        public async Task<bool> ImportJsonToDb(string json)
        {
            List<PokemonExportJson> pokemonsJson = JsonConvert.DeserializeObject<List<PokemonExportJson>>(json);

            foreach (PokemonExportJson pokemonJson in pokemonsJson)
            {
                Pokemon pokemon = new();
                await MapToInstanceImport(pokemon, pokemonJson);
                await this.Add(pokemon);
                Console.WriteLine("Pokemon:" + pokemon.FR.Name);
            }
            
            this._context.SaveChanges();

            return await Task.FromResult(true);
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque)
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(m => m.typeAttaque)
                .Include(m => m.Game)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pokemon>> GetAllLight()
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
                .ToListAsync();
        }

        public async Task<List<Pokemon>> GetFamilyWithoutVariantAsync(string family)
        {
            string[] vs = family.Split(',');
            List<Pokemon> result = new List<Pokemon>();

            foreach (var item in vs)
            {
                Pokemon pokemon = Find(m => m.FR.Name.Equals(item)).FirstOrDefault();
                if (pokemon != null)
                    result.Add(pokemon);
            }

            return result;
        }

        public async Task<IEnumerable<Pokemon>> GetAllVariantAsync(string number)
        {
            return await this._context.Pokemons
                .Where(m => m.Number.Equals(number) && !m.TypeEvolution.Equals("Normal")).OrderBy(m => m.Number)
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
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
            pokemon.StatAttaque = Convert.ToInt32(pokemonJson.StatAttaque);
            pokemon.StatDefense = Convert.ToInt32(pokemonJson.StatDefense);
            pokemon.StatAttaqueSpe = Convert.ToInt32(pokemonJson.StatAttaqueSpe);
            pokemon.StatDefenseSpe = Convert.ToInt32(pokemonJson.StatDefenseSpe);
            pokemon.StatVitesse = Convert.ToInt32(pokemonJson.StatVitesse);
            pokemon.StatTotal = Convert.ToInt32(pokemonJson.StatTotal);
            pokemon.Generation = Convert.ToInt32(pokemonJson.Generation);
            pokemon.EggMoves = pokemonJson.EggMoves;
            pokemon.CaptureRate = pokemonJson.CaptureRate;
            pokemon.BasicHappiness = pokemonJson.BasicHappiness;
            pokemon.UrlImg = pokemonJson.UrlImg;
            pokemon.UrlSprite = pokemonJson.UrlSprite;
            pokemon.UrlSound = pokemonJson.UrlSound;
            pokemon.Game = _repositoryG.Find(m => m.Name_EN.Equals(pokemonJson.Game.Name_EN)).FirstOrDefault();

            foreach (TypesPokExportJson typePokJson in pokemonJson.Types)
            {
                TypePok typePok = _repositoryTP.Find(m => m.Name_EN.Equals(typePokJson.TypePok.Name_EN)).FirstOrDefault();
                if (typePok != null)
                {
                    Pokemon_TypePok pokemon_TypePok = new()
                    {
                        Pokemon = pokemon,
                        TypePok = typePok
                    };
                    await _repositoryPTP.Add(pokemon_TypePok);
                }
            }

            foreach (TypesPokExportJson weaknessJson in pokemonJson.Weaknesses)
            {
                TypePok typePok = _repositoryTP.Find(m => m.Name_EN.Equals(weaknessJson.TypePok.Name_EN)).FirstOrDefault();
                if (typePok != null)
                {
                    Pokemon_Weakness pokemon_Weakness = new()
                    {
                        Pokemon = pokemon,
                        TypePok = typePok
                    };
                    await _repositoryPW.Add(pokemon_Weakness);
                }
            }
            
            foreach (TalentsExportJson talentJson in pokemonJson.Talents)
            {
                Talent talent = _repositoryTL.Find(m => m.Name_EN.Equals(talentJson.Talent.Name_EN)).FirstOrDefault();
                if (talent != null)
                {
                    Pokemon_Talent pokemon_Talent = new()
                    {
                        Pokemon = pokemon,
                        Talent = talent,
                        IsHidden = talentJson.IsHidden
                    };
                    await _repositoryPT.Add(pokemon_Talent);
                }
            }

            foreach (AttaquesExportJson attaqueJson in pokemonJson.Attaques)
            {
                Attaque attaque = _repositoryAT.Find(m => m.Name_EN.Equals(attaqueJson.Attaque.Name_EN)).FirstOrDefault();
                if (attaque != null)
                {
                    Pokemon_Attaque pokemon_Attaque = new()
                    {
                        Pokemon = pokemon,
                        Attaque = attaque,
                        TypeLearn  = attaqueJson.TypeLearn,
                        Level = attaqueJson.Level,
                        CTCS = attaqueJson.CTCS
                    };
                    await _repositoryPAT.Add(pokemon_Attaque);
                }
            }
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
                Talent = dataInfoJson.Talent,
                DescriptionTalent = dataInfoJson.DescriptionTalent,
                Types = dataInfoJson.Types,
                Weakness = dataInfoJson.Weakness,
                Evolutions = dataInfoJson.Evolutions,
                WhenEvolution = dataInfoJson.WhenEvolution,
                NextUrl = dataInfoJson.NextUrl
            };

            await _repositoryDI.Add(dataInfo);
            return await Task.FromResult(dataInfo);
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => (IUnitOfWork)this._context;
        #endregion
    }
}
