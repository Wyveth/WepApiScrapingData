using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Utils;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class PokemonRepository : Repository<Pokemon>, IRepositoryExtendsPokemon<Pokemon>
    {
        #region Fields
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
        public PokemonRepository(ScrapingContext context) : base(context)
        {
            _repositoryDI = new DataInfoRepository(context);
            _repositoryTP = new TypePokRepository(context);
            _repositoryTL = new TalentRepository(context);
            _repositoryAT = new AttaqueRepository(context);
            _repositoryTA = new TypeAttaqueRepository(context);
            _repositoryPTP = new Pokemon_TypePokRepository(context);
            _repositoryPW = new Pokemon_WeaknessRepository(context);
            _repositoryPAT = new Pokemon_AttaqueRepository(context);
            _repositoryPT = new Pokemon_TalentRepository(context);
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
                MapToInstance(pokemon, pokemonJson);
                await Add(pokemon);
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypeAttaque)
                .Include(m => m.Game)
                .Where(predicate ?? (s => true)).ToListAsync();
        }

        public override async Task<Pokemon?> Get(int id)
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypeAttaque)
                .Include(m => m.Game)
                .SingleAsync(x => x.Id.Equals(id));
        }

        public override async Task<Pokemon?> GetByGuid(Guid guid)
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypeAttaque)
                .Include(m => m.Game)
                .SingleAsync(x => x.Guid.Equals(guid));
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypeAttaque)
                .Include(m => m.Game)
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
                .Include(m => m.Pokemon_Talents).ThenInclude(u => u.Talent)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypeAttaque)
                .Include(m => m.Game)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pokemon>> GetAllLight()
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
                .ToListAsync();
        }
        
        public async Task<List<Pokemon>> GetFamilyWithoutVariantAsync(string family)
        {
            string[] vs = family.Split(',');
            List<Pokemon> result = new List<Pokemon>();
            
            foreach (var item in vs)
            {
                Pokemon pokemon = Find(m => m.FR.Name.Equals(item)).Result.FirstOrDefault();
                if (pokemon != null)
                    result.Add(pokemon);
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Pokemon>> GetAllVariantAsync(string number)
        {
            return await _context.Pokemons
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
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypePok)
                .Include(m => m.Pokemon_Attaques).ThenInclude(u => u.Attaque).ThenInclude(u => u.TypeAttaque)
                .Include(m => m.Game)
                .ToListAsync();
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
                    MapToInstanceImport(pokemon, pokemonJson);
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

        public Task SaveInfoPokemonAttackInDB(string json)
        {
            List<string> Erreurs = new();
            List<PokemonPokeBipJson> pokemonsPokeBipJson = JsonConvert.DeserializeObject<List<PokemonPokeBipJson>>(json);
            List<Attaque> attaques = new();
            List<Pokemon_Talent> pokemon_Talents = new();

            List<Attaque> attackAlreadyExist = _repositoryAT.GetAll().Result.ToList();
            List<Talent> talents = _repositoryTL.GetAll().Result.ToList();
            List<Pokemon_Talent> pokemonTalent_alreadyExist = _repositoryPT.GetAll().Result.ToList();

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
                                    typeAttaque = _repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités Physiques").Result;
                                    break;
                                case "Spéciale":
                                    typeAttaque = _repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités Spéciales").Result;
                                    break;
                                case "Statut":
                                    typeAttaque = _repositoryTA.SingleOrDefault(t => t.Name_FR == "Capacités de Statut").Result;
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
                                    TypeAttaque = typeAttaque,
                                    TypePok = _repositoryTP.Find(t => t.Name_FR == attackJson.Type).Result.FirstOrDefault()
                                };
                                attaques.Add(attaque);
                                attackAlreadyExist.Add(attaque);
                            }
                            else
                            {
                                attaque.TypeAttaque = typeAttaque;
                                attaque.TypePok = _repositoryTP.Find(t => t.Name_FR == attackJson.Type).Result.FirstOrDefault();
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

            _repositoryPT.AddRange(pokemon_Talents);
            _repositoryAT.AddRange(attaques);
            _context.SaveChanges();
            #endregion

            attackAlreadyExist = _repositoryAT.GetAll().Result.ToList();
            List<Pokemon_Attaque> pokemon_AttaquesAlreadyExist = _repositoryPAT.GetAll().Result.ToList();
            List<Pokemon_Attaque> pokemon_Attaques = new();

            foreach (PokemonPokeBipJson pokemonPokeBipJson in pokemonsPokeBipJson)
            {
                Pokemon pokemon = _context.Pokemons.FirstOrDefault(p => p.FR.Name == pokemonPokeBipJson.Name);

                pokemonPokeBipJson.AttackJsons.ForEach(attackJson =>
                {
                    Attaque attaque = attackAlreadyExist.FirstOrDefault(a => a.Name_FR == attackJson.Name);
                    Pokemon_Attaque pokemon_Attaque = pokemon_AttaquesAlreadyExist.FirstOrDefault(m => m.PokemonId == pokemon.Id && m.AttaqueId == attaque.Id);

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

            _repositoryPAT.AddRange(pokemon_Attaques);
            _context.SaveChanges();

            return Task.FromResult(true);
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
            pokemon.PathImg = pokemonJson.PathImg;
            pokemon.UrlSprite = pokemonJson.UrlSprite;
            pokemon.PathSprite = pokemonJson.PathSprite;
            pokemon.UrlSound = pokemonJson.UrlSound;
            pokemon.PathSound = pokemonJson.PathSound;
            pokemon.Game = _repositoryG.Find(m => m.Name_EN.Equals(pokemonJson.Game.Name_EN)).Result.FirstOrDefault();

            await Add(pokemon);

            foreach (TypesPokExportJson typePokJson in pokemonJson.Types)
            {
                TypePok typePok = _repositoryTP.Find(m => m.Name_EN.Equals(typePokJson.TypePok.Name_EN)).Result.FirstOrDefault();
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
                TypePok typePok = _repositoryTP.Find(m => m.Name_EN.Equals(weaknessJson.TypePok.Name_EN)).Result.FirstOrDefault();
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
                Talent talent = _repositoryTL.Find(m => m.Name_EN.Equals(talentJson.Talent.Name_EN)).Result.FirstOrDefault();
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
                Attaque attaque = _repositoryAT.Find(m => m.Name_EN.Equals(attaqueJson.Attaque.Name_EN)).Result.FirstOrDefault();
                if (attaque != null)
                {
                    Pokemon_Attaque pokemon_Attaque = new()
                    {
                        Pokemon = pokemon,
                        Attaque = attaque,
                        TypeLearn = attaqueJson.TypeLearn,
                        Level = attaqueJson.Level,
                        CTCS = attaqueJson.CTCS
                    };
                    await _repositoryPAT.Add(pokemon_Attaque);
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
        #endregion
    }
}
