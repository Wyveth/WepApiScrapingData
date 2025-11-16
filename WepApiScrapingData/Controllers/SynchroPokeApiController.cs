using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class SynchroPokeController : ControllerBase
    {
        #region Private fields
        private readonly ILogger<SynchroPokeController> _logger;
        private readonly ScrapingContext _context;
        private readonly HttpClient _pokeApiClient;
        private readonly AttackRepository _repositoryAT;
        private readonly TypeAttackRepository _repositoryTA;
        private readonly TypePokRepository _repositoryTP;
        private readonly AbilityRepository _repositoryT;
        private readonly PokemonRepository _repositoryP;
        private readonly Pokemon_EvolvesToRepository _repositoryPET;
        private readonly EvolutionChainRepository _repositoryEC;
        #endregion

        public SynchroPokeController(ILogger<SynchroPokeController> logger,
            ScrapingContext context,
            IHttpClientFactory httpClientFactory,
            AttackRepository repositoryAT,
            TypeAttackRepository repositoryTA,
            TypePokRepository repositoryTP,
            AbilityRepository repositoryT,
            PokemonRepository repositoryP,
            EvolutionChainRepository repositoryEC,
            Pokemon_EvolvesToRepository repositoryPET)
        {
            _logger = logger;
            _context = context;

            _repositoryAT = repositoryAT;
            _repositoryTA = repositoryTA;
            _repositoryTP = repositoryTP;

            _pokeApiClient = httpClientFactory.CreateClient("pokeapi");
            _repositoryT = repositoryT;
            _repositoryP = repositoryP;
            _repositoryEC = repositoryEC;
            _repositoryPET = repositoryPET;
        }

        [HttpGet("UpdatePokemonEvolve")]
        public async Task<IActionResult> UpdatePokemonEvolve()
        {
            var pokemons = (await _repositoryP.GetAll()).ToList();
            var evolutionChains = (await _repositoryEC.GetAll()).ToList();

            foreach (var pokemon in pokemons)
            {
                if (string.IsNullOrEmpty(pokemon.EN?.Evolutions))
                    continue;

                // Liste ordonnée de la famille complète
                var familyNames = pokemon.EN.Evolutions
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (familyNames.Count == 0)
                    continue;

                // Clé unique pour la famille
                string key = string.Join("|", familyNames);

                // Vérifie si la chaîne existe déjà
                var chain = evolutionChains.FirstOrDefault(c =>
                    c.Evolutions != null &&
                    string.Equals(c.Evolutions, key, StringComparison.OrdinalIgnoreCase));

                if (chain == null)
                {
                    chain = new EvolutionChain { Evolutions = key };
                    await _repositoryEC.AddAsync(chain);
                    evolutionChains.Add(chain);
                }

                // Associe le Pokémon à cette chaîne
                pokemon.EvolutionChainId = chain.Id;
                await _repositoryP.UpdateAsync(pokemon);

                // Crée les relations d’évolution
                for (int i = 0; i < familyNames.Count - 1; i++)
                {
                    var baseName = familyNames[i];
                    var nextName = familyNames[i + 1];

                    var basePokemon = pokemons.FirstOrDefault(p => p.EN.DisplayName.Equals(baseName, StringComparison.OrdinalIgnoreCase));
                    var nextPokemon = pokemons.FirstOrDefault(p => p.EN.DisplayName.Equals(nextName, StringComparison.OrdinalIgnoreCase));

                    if (basePokemon == null || nextPokemon == null)
                        continue;

                    // Crée la relation Bulbasaur -> Ivysaur etc.
                    var exists = await _repositoryPET.ExistsAsync(basePokemon.Id, nextPokemon.Id);

                    if (!exists)
                    {
                        var relation = new Pokemon_EvolvesTo
                        {
                            PokemonId = basePokemon.Id,
                            EvolveToId = nextPokemon.Id,
                            WhenEvolutionFR = nextPokemon.FR.WhenEvolution,
                            WhenEvolutionEN = nextPokemon.EN.WhenEvolution,
                            WhenEvolutionES = nextPokemon.ES.WhenEvolution,
                            WhenEvolutionIT = nextPokemon.IT.WhenEvolution,
                            WhenEvolutionDE = nextPokemon.DE.WhenEvolution,
                            WhenEvolutionRU = nextPokemon.RU.WhenEvolution,
                            WhenEvolutionCO = nextPokemon.CO.WhenEvolution,
                            WhenEvolutionCN = nextPokemon.CN.WhenEvolution,
                            WhenEvolutionJP = nextPokemon.JP.WhenEvolution
                        };
                        await _repositoryPET.AddAsync(relation);
                    }
                    else
                    {
                        var pokEvol = await _repositoryPET.GetAsync(basePokemon.Id, nextPokemon.Id);
                        pokEvol.WhenEvolutionFR = nextPokemon.FR.WhenEvolution;
                        pokEvol.WhenEvolutionEN = nextPokemon.EN.WhenEvolution;
                        pokEvol.WhenEvolutionES = nextPokemon.ES.WhenEvolution;
                        pokEvol.WhenEvolutionIT = nextPokemon.IT.WhenEvolution;
                        pokEvol.WhenEvolutionDE = nextPokemon.DE.WhenEvolution;
                        pokEvol.WhenEvolutionRU = nextPokemon.RU.WhenEvolution;
                        pokEvol.WhenEvolutionCO = nextPokemon.CO.WhenEvolution;
                        pokEvol.WhenEvolutionCN = nextPokemon.CN.WhenEvolution;
                        pokEvol.WhenEvolutionJP = nextPokemon.JP.WhenEvolution;
                        await _repositoryPET.UpdateAsync(pokEvol);
                    }

                    // Tous les Pokémon de la famille partagent la même chaîne
                    nextPokemon.EvolutionChainId = chain.Id;
                    await _repositoryP.UpdateAsync(nextPokemon);
                }

                // 🧩 Gestion des formes spéciales (Méga / Gigamax)
                var baseNameNoForm = familyNames.Last();
                var variants = pokemons.Where(p =>
                    p.EN.DisplayName == baseNameNoForm)
                    .Skip(1);

                foreach (var variant in variants)
                {
                    // Venusaur -> Mega Venusaur, Gigamax Venusaur
                    var exists = await _repositoryPET.ExistsAsync(pokemons.First(x => x.EN.DisplayName == baseNameNoForm).Id, variant.Id);

                    if (!exists)
                    {
                        await _repositoryPET.AddAsync(new Pokemon_EvolvesTo
                        {
                            PokemonId = pokemons.First(x => x.EN.DisplayName == baseNameNoForm).Id,
                            EvolveToId = variant.Id,
                            WhenEvolutionFR = variant.FR.WhenEvolution,
                            WhenEvolutionEN = variant.EN.WhenEvolution,
                            WhenEvolutionES = variant.ES.WhenEvolution,
                            WhenEvolutionIT = variant.IT.WhenEvolution,
                            WhenEvolutionDE = variant.DE.WhenEvolution,
                            WhenEvolutionRU = variant.RU.WhenEvolution,
                            WhenEvolutionCO = variant.CO.WhenEvolution,
                            WhenEvolutionCN = variant.CN.WhenEvolution,
                            WhenEvolutionJP = variant.JP.WhenEvolution
                        });
                    }
                    else
                    {
                        var pokEvol = await _repositoryPET.GetAsync(pokemons.First(x => x.EN.DisplayName == baseNameNoForm).Id, variant.Id);
                        pokEvol.WhenEvolutionFR = variant.FR.WhenEvolution;
                        pokEvol.WhenEvolutionEN = variant.EN.WhenEvolution;
                        pokEvol.WhenEvolutionES = variant.ES.WhenEvolution;
                        pokEvol.WhenEvolutionIT = variant.IT.WhenEvolution;
                        pokEvol.WhenEvolutionDE = variant.DE.WhenEvolution;
                        pokEvol.WhenEvolutionRU = variant.RU.WhenEvolution;
                        pokEvol.WhenEvolutionCO = variant.CO.WhenEvolution;
                        pokEvol.WhenEvolutionCN = variant.CN.WhenEvolution;
                        pokEvol.WhenEvolutionJP = variant.JP.WhenEvolution;
                        await _repositoryPET.UpdateAsync(pokEvol);
                    }

                        variant.EvolutionChainId = chain.Id;
                    await _repositoryP.UpdateAsync(variant);
                }
            }

            return Ok("✅ Chaînes et relations d’évolution mises à jour !");
        }


        [HttpGet("SynchroAttacksFromPokeApi")]
        public async Task<IActionResult> SynchroAttacks()
        {
            var pokeList = await _pokeApiClient.GetFromJsonAsync<PokeList>("move?limit=10000");
            if (pokeList == null) return BadRequest("Impossible de récupérer les moves depuis PokéAPI");

            var pokeMoves = new List<MoveDto>();

            foreach (var item in pokeList.Results)
            {
                // item.Url est absolu → on le passe comme Uri
                var moveData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(item.Url));

                var dto = new MoveDto
                {
                    Identifier = moveData.GetProperty("name").GetString(),
                    Type = moveData.GetProperty("type").GetProperty("name").GetString(),
                    Power = moveData.TryGetProperty("power", out var p) && p.ValueKind != JsonValueKind.Null ? p.GetInt32() : null,
                    Pp = moveData.TryGetProperty("pp", out var pp) && pp.ValueKind != JsonValueKind.Null ? pp.GetInt32() : null,
                    Accuracy = moveData.TryGetProperty("accuracy", out var acc) && acc.ValueKind != JsonValueKind.Null ? acc.GetInt32() : null,
                    Names = new Dictionary<string, string>(),
                    Descriptions = new Dictionary<string, string>(),
                    DamageClass = moveData.TryGetProperty("damage_class", out var dmgClass) && dmgClass.ValueKind != JsonValueKind.Null
                        ? dmgClass.GetProperty("name").GetString()
                        : null,
                };

                // Récupération des noms multilingues
                if (moveData.TryGetProperty("names", out var names))
                {
                    foreach (var nameEntry in names.EnumerateArray())
                    {
                        var lang = nameEntry.GetProperty("language").GetProperty("name").GetString();
                        var value = nameEntry.GetProperty("name").GetString();
                        if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                            dto.Names[lang] = value;
                    }
                }

                // Récupération des descriptions multilingues
                if (moveData.TryGetProperty("flavor_text_entries", out var flavorTexts))
                {
                    foreach (var entry in flavorTexts.EnumerateArray())
                    {
                        var lang = entry.GetProperty("language").GetProperty("name").GetString();
                        var value = entry.GetProperty("flavor_text").GetString()?.Replace("\n", " ").Replace("\f", " ");

                        if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                        {
                            // garde la première description trouvée pour chaque langue
                            if (!dto.Descriptions.ContainsKey(lang))
                                dto.Descriptions[lang] = value;
                        }
                    }
                }

                pokeMoves.Add(dto);

                await Task.Delay(150); // respect du rate-limit de la PokéAPI
            }

            return Ok(pokeMoves);
        }

        [HttpGet("SynchroAttacksForDB")]
        public async Task<IActionResult> SynchroAttacksForDB()
        {
            string json;
            using (StreamReader r = new StreamReader(Constantes.pathExport + "PokeApi/Attacks.json"))
            {
                json = r.ReadToEnd();

                List<MoveDto> isExist = new();
                List<Attack> attaqueNotExist = new();

                if (!string.IsNullOrEmpty(json))
                {
                    List<MoveDto> moves = JsonConvert.DeserializeObject<List<MoveDto>>(json);

                    foreach (MoveDto move in moves)
                    {
                        Attack? attaque = await this._repositoryAT.GetByName(move.Names["en"]);

                        if (attaque != null)
                        {
                            attaque.Name_FR = move.Names.GetValueOrDefault("fr") ?? attaque.Name_FR;
                            attaque.Description_FR = move.Descriptions.GetValueOrDefault("fr") ?? attaque.Description_FR;
                            attaque.Name_EN = move.Names.GetValueOrDefault("en") ?? attaque.Name_EN;
                            attaque.Description_EN = move.Descriptions.GetValueOrDefault("en") ?? attaque.Description_EN;
                            attaque.Name_ES = move.Names.GetValueOrDefault("es") ?? attaque.Name_ES;
                            attaque.Description_ES = move.Descriptions.GetValueOrDefault("es") ?? attaque.Description_ES;
                            attaque.Name_IT = move.Names.GetValueOrDefault("it") ?? attaque.Name_IT;
                            attaque.Description_IT = move.Descriptions.GetValueOrDefault("it") ?? attaque.Description_IT;
                            attaque.Name_DE = move.Names.GetValueOrDefault("de") ?? attaque.Name_DE;
                            attaque.Description_DE = move.Descriptions.GetValueOrDefault("de") ?? attaque.Description_DE;
                            attaque.Name_CO = move.Names.GetValueOrDefault("ko") ?? attaque.Name_CO;
                            attaque.Description_CO = move.Descriptions.GetValueOrDefault("ko") ?? attaque.Description_CO;
                            attaque.Name_CN = move.Names.GetValueOrDefault("zh-Hans") ?? attaque.Name_CN;
                            attaque.Description_CN = move.Descriptions.GetValueOrDefault("zh-Hans") ?? attaque.Description_CN;
                            attaque.Name_JP = move.Names.GetValueOrDefault("ja") ?? attaque.Name_JP;
                            attaque.Description_JP = move.Descriptions.GetValueOrDefault("ja") ?? attaque.Description_JP;

                            await this._repositoryAT.UpdateAsync(attaque);
                        }
                        else
                        {
                            Attack newAttack = new()
                            {
                                Name_FR = move.Names.GetValueOrDefault("fr"),
                                Description_FR = move.Descriptions.GetValueOrDefault("fr"),
                                Name_EN = move.Names.GetValueOrDefault("en"),
                                Description_EN = move.Descriptions.GetValueOrDefault("en"),
                                Name_ES = move.Names.GetValueOrDefault("es"),
                                Description_ES = move.Descriptions.GetValueOrDefault("es"),
                                Name_IT = move.Names.GetValueOrDefault("it"),
                                Description_IT = move.Descriptions.GetValueOrDefault("it"),
                                Name_DE = move.Names.GetValueOrDefault("de"),
                                Description_DE = move.Descriptions.GetValueOrDefault("de"),
                                Name_CO = move.Names.GetValueOrDefault("ko"),
                                Description_CO = move.Descriptions.GetValueOrDefault("ko"),
                                Name_CN = move.Names.GetValueOrDefault("zh-Hans"),
                                Description_CN = move.Descriptions.GetValueOrDefault("zh-Hans"),
                                Name_JP = move.Names.GetValueOrDefault("ja"),
                                Description_JP = move.Descriptions.GetValueOrDefault("ja"),
                                TypeAttack = (await _repositoryTA.Find(m => m.Name_EN.Contains(move.DamageClass))).FirstOrDefault(),
                                TypePok = (await _repositoryTP.Find(m => m.Name_EN.Contains(move.Type))).FirstOrDefault(),
                                Power = move.Power.ToString(),
                                Precision = move.Accuracy.ToString(),
                                PP = move.Pp.ToString()
                            };

                            attaqueNotExist.Add(newAttack);
                        }
                    }

                    await this._repositoryAT.AddRangeAsync(attaqueNotExist);
                }

                return Ok();
            }
        }

        [HttpGet("SynchroAbilitiesFromPokeApi")]
        public async Task<IActionResult> SynchroAbilities()
        {
            // 1️⃣ Récupère la liste complète des abilities
            var abilityList = await _pokeApiClient.GetFromJsonAsync<PokeList>("ability?limit=10000");
            if (abilityList == null)
                return BadRequest("Impossible de récupérer les abilities depuis PokéAPI");

            var abilities = new List<AbilityDto>();

            // 2️⃣ Parcourt chaque ability et récupère ses infos détaillées
            foreach (var item in abilityList.Results)
            {
                var abilityData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(item.Url));

                var dto = new AbilityDto
                {
                    Identifier = abilityData.GetProperty("name").GetString(),
                    Names = new Dictionary<string, string>(),
                    Descriptions = new Dictionary<string, string>()
                };

                // 3️⃣ Récupération des noms multilingues
                if (abilityData.TryGetProperty("names", out var names))
                {
                    foreach (var nameEntry in names.EnumerateArray())
                    {
                        var lang = nameEntry.GetProperty("language").GetProperty("name").GetString();
                        var value = nameEntry.GetProperty("name").GetString();
                        if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                            dto.Names[lang] = value;
                    }
                }

                // 4️⃣ Récupération des descriptions multilingues
                if (abilityData.TryGetProperty("flavor_text_entries", out var flavorTexts))
                {
                    foreach (var entry in flavorTexts.EnumerateArray())
                    {
                        var lang = entry.GetProperty("language").GetProperty("name").GetString();
                        var value = entry.GetProperty("flavor_text").GetString()?.Replace("\n", " ").Replace("\f", " ");

                        if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                        {
                            // garde la première description trouvée pour chaque langue
                            if (!dto.Descriptions.ContainsKey(lang))
                                dto.Descriptions[lang] = value;
                        }
                    }
                }

                abilities.Add(dto);

                await Task.Delay(150);
            }

            return Ok(abilities);
        }

        [HttpGet("SynchroAbilitiesForDB")]
        public async Task<IActionResult> SynchroAbilitiesForDB()
        {
            string json;
            using (StreamReader r = new StreamReader(Constantes.pathExport + "PokeApi/Talents.json"))
            {
                json = r.ReadToEnd();

                List<AbilityDto> isExist = new();
                List<Ability> talentsNotExist = new();

                if (!string.IsNullOrEmpty(json))
                {
                    List<AbilityDto> abilities = JsonConvert.DeserializeObject<List<AbilityDto>>(json);

                    foreach (AbilityDto ability in abilities)
                    {
                        Ability? talent = await this._repositoryT.GetByName(ability.Names["en"]);

                        if (talent != null)
                        {
                            talent.Name_FR = ability.Names.GetValueOrDefault("fr") ?? talent.Name_FR;
                            talent.Description_FR = ability.Descriptions.GetValueOrDefault("fr") ?? talent.Description_FR;
                            talent.Name_EN = ability.Names.GetValueOrDefault("en") ?? talent.Name_EN;
                            talent.Description_EN = ability.Descriptions.GetValueOrDefault("en") ?? talent.Description_EN;
                            talent.Name_ES = ability.Names.GetValueOrDefault("es") ?? talent.Name_ES;
                            talent.Description_ES = ability.Descriptions.GetValueOrDefault("es") ?? talent.Description_ES;
                            talent.Name_IT = ability.Names.GetValueOrDefault("it") ?? talent.Name_IT;
                            talent.Description_IT = ability.Descriptions.GetValueOrDefault("it") ?? talent.Description_IT;
                            talent.Name_DE = ability.Names.GetValueOrDefault("de") ?? talent.Name_DE;
                            talent.Description_DE = ability.Descriptions.GetValueOrDefault("de") ?? talent.Description_DE;
                            talent.Name_CO = ability.Names.GetValueOrDefault("ko") ?? talent.Name_CO;
                            talent.Description_CO = ability.Descriptions.GetValueOrDefault("ko") ?? talent.Description_CO;
                            talent.Name_CN = ability.Names.GetValueOrDefault("zh-Hans") ?? talent.Name_CN;
                            talent.Description_CN = ability.Descriptions.GetValueOrDefault("zh-Hans") ?? talent.Description_CN;
                            talent.Name_JP = ability.Names.GetValueOrDefault("ja") ?? talent.Name_JP;
                            talent.Description_JP = ability.Descriptions.GetValueOrDefault("ja") ?? talent.Description_JP;

                            await this._repositoryT.UpdateAsync(talent);
                        }
                        else
                        {
                            Ability newTalent = new()
                            {
                                Name_FR = ability.Names.GetValueOrDefault("fr"),
                                Description_FR = ability.Descriptions.GetValueOrDefault("fr"),
                                Name_EN = ability.Names.GetValueOrDefault("en"),
                                Description_EN = ability.Descriptions.GetValueOrDefault("en"),
                                Name_ES = ability.Names.GetValueOrDefault("es"),
                                Description_ES = ability.Descriptions.GetValueOrDefault("es"),
                                Name_IT = ability.Names.GetValueOrDefault("it"),
                                Description_IT = ability.Descriptions.GetValueOrDefault("it"),
                                Name_DE = ability.Names.GetValueOrDefault("de"),
                                Description_DE = ability.Descriptions.GetValueOrDefault("de"),
                                Name_CO = ability.Names.GetValueOrDefault("ko"),
                                Description_CO = ability.Descriptions.GetValueOrDefault("ko"),
                                Name_CN = ability.Names.GetValueOrDefault("zh-Hans"),
                                Description_CN = ability.Descriptions.GetValueOrDefault("zh-Hans"),
                                Name_JP = ability.Names.GetValueOrDefault("ja"),
                                Description_JP = ability.Descriptions.GetValueOrDefault("ja")
                            };

                            talentsNotExist.Add(newTalent);
                        }
                    }

                    await this._repositoryT.AddRangeAsync(talentsNotExist);
                }

                return Ok();
            }
        }

        [HttpGet("SynchroPokemonsFromPokeApi")]
        public async Task<IActionResult> SynchroPokemons()
        {
            List<string> ErrorForm = new();
            List<string> Error = new();

            var pokeList = await _pokeApiClient.GetFromJsonAsync<PokeList>("pokemon?limit=100000&offset=0");
            if (pokeList?.Results == null || pokeList.Results.Count == 0)
                return BadRequest("Impossible de récupérer la liste des Pokémon depuis PokéAPI.");

            var pokemons = new ConcurrentBag<PokeDto>();

            // 🔹 Cache des types pour calcul des faiblesses
            var typeNames = new[]
            {
                "normal","fire","water","electric","grass","ice","fighting","poison",
                "ground","flying","psychic","bug","rock","ghost","dragon","dark","steel","fairy"
            };
            var typeCache = new ConcurrentDictionary<string, JsonElement>();
            await Parallel.ForEachAsync(typeNames, async (typeName, _) =>
            {
                try
                {
                    var typeData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(
                        new Uri($"https://pokeapi.co/api/v2/type/{typeName}")
                    );
                    typeCache[typeName] = typeData;
                }
                catch { }
            });

            // 🔹 Cache global des moves
            var moveNameCache = new ConcurrentDictionary<string, Task<string>>();
            var abilityNameCache = new ConcurrentDictionary<string, Task<string>>();
            int batchSize = 10;

            for (int i = 0; i < pokeList.Results.Count; i += batchSize)
            {
                var batch = pokeList.Results.Skip(i).Take(batchSize).ToList();
                var tasks = batch.Select(async item =>
                {
                    try
                    {
                        var pokemonData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(item.Url));
                        if (pokemonData.ValueKind == JsonValueKind.Undefined) return;


                        int id = pokemonData.GetProperty("id").GetInt32();
                        bool isSpecialForm = id >= 10000;
                        JsonElement? speciesData = null;
                        var dto = new PokeDto();

                        // --- 🔸 Si c’est une forme spéciale ---
                        if (isSpecialForm)
                        {
                            try
                            {
                                string urlForm = pokemonData.GetProperty("forms")[0].GetProperty("url").GetString();

                                var formData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(
                                    new Uri(urlForm)
                                );

                                dto.IsSpecialForm = true;
                                dto.FormIdentifier = formData.TryGetProperty("form_name", out var fn) ? fn.GetString() : null;
                                dto.FormNames = new Dictionary<string, string>();
                                dto.FormNameEn = null;

                                // Noms multilingues de la forme
                                if (formData.TryGetProperty("form_names", out var formNames) && formNames.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (var n in formNames.EnumerateArray())
                                    {
                                        var lang = n.GetProperty("language").GetProperty("name").GetString();
                                        var value = n.GetProperty("name").GetString();
                                        if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                                            dto.FormNames[lang] = value;
                                    }

                                    if (dto.FormNames.TryGetValue("en", out var en)) dto.FormNameEn = en;
                                }

                                // Récupération du lien vers la species
                                if (formData.TryGetProperty("pokemon", out var pokeRef) &&
                                    pokeRef.TryGetProperty("url", out var pokeUrlProp))
                                {
                                    var pokeUrl = pokeUrlProp.GetString();
                                    if (!string.IsNullOrEmpty(pokeUrl))
                                    {
                                        var pokeEntity = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(pokeUrl));
                                        var speciesUrl = pokeEntity.GetProperty("species").GetProperty("url").GetString();
                                        speciesData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(speciesUrl));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erreur récupération forme spéciale {item.Name}: {ex.Message}");
                                ErrorForm.Add($"Erreur récupération forme spéciale {item.Name}: {ex.Message}");
                            }
                        }

                        // --- 🔹 Si pas de forme spéciale ---
                        else
                        {
                            var speciesUrl = pokemonData.GetProperty("species").GetProperty("url").GetString();
                            speciesData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(speciesUrl));
                        }

                        if (pokemonData.TryGetProperty("id", out var idProp))
                        {
                            dto.Id = idProp.GetInt32();
                        }

                        // --- Données de base ---
                        dto.Identifier = pokemonData.GetProperty("name").GetString();
                        int nationalNumber = id;
                        // Recherche du vrai numéro Pokédex national depuis la species
                        if (speciesData.Value.TryGetProperty("pokedex_numbers", out var pokedexNumbers) &&
                        pokedexNumbers.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var p in pokedexNumbers.EnumerateArray())
                            {
                                var pokedex = p.GetProperty("pokedex").GetProperty("name").GetString();
                                if (pokedex == "national")
                                {
                                    nationalNumber = p.GetProperty("entry_number").GetInt32();
                                    break;
                                }
                            }
                        }
                        dto.NationalId = nationalNumber;
                        dto.NationalIdFormatted = nationalNumber.ToString("D4");
                        dto.Height = pokemonData.TryGetProperty("height", out var h) ? h.GetInt32() : 0;
                        dto.Weight = pokemonData.TryGetProperty("weight", out var w) ? w.GetInt32() : 0;

                        // --- Conversion métrique ---
                        dto.HeightMeters = Math.Round(dto.Height / 10.0, 2);  // dm → m
                        dto.WeightKilograms = Math.Round(dto.Weight / 10.0, 2); // hg → kg

                        // --- Conversion impériale ---
                        // 1 mètre = 3.28084 pieds
                        // 1 kg = 2.20462 livres
                        double totalFeet = dto.HeightMeters * 3.28084;
                        int feet = (int)Math.Floor(totalFeet);
                        int inches = (int)Math.Round((totalFeet - feet) * 12);
                        dto.HeightFeetInches = $"{feet}'{inches}\"";
                        dto.WeightPounds = Math.Round(dto.WeightKilograms * 2.20462, 2);

                        dto.BaseExperience = pokemonData.TryGetProperty("base_experience", out var exp) ? exp.GetInt32() : 0;

                        // Types
                        if (pokemonData.TryGetProperty("types", out var types) && types.ValueKind == JsonValueKind.Array)
                            foreach (var t in types.EnumerateArray())
                                dto.Types.Add(t.GetProperty("type").GetProperty("name").GetString());

                        // Abilities
                        if (pokemonData.TryGetProperty("abilities", out var abilities)
                             && abilities.ValueKind == JsonValueKind.Array)
                        {
                            using var client = new HttpClient();

                            foreach (var a in abilities.EnumerateArray())
                            {
                                var abilityUrl = a.GetProperty("ability").GetProperty("url").GetString();
                                var isHidden = a.GetProperty("is_hidden").GetBoolean();

                                // Récupère le nom anglais (avec cache)
                                var abilityNameTask = abilityNameCache.GetOrAdd(
                                    abilityUrl,
                                    url => GetAbilityNameEnAsync(url, client)
                                );

                                var abilityNameEn = await abilityNameTask;

                                dto.Abilities.Add(new AbilityLightDto
                                {
                                    Identifier = abilityUrl,
                                    NameEn = abilityNameEn,
                                    IsHidden = isHidden
                                });
                            }
                        }

                        // Stats
                        int statTotal = 0;
                        if (pokemonData.TryGetProperty("stats", out var stats) && stats.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var s in stats.EnumerateArray())
                            {
                                var statName = s.GetProperty("stat").GetProperty("name").GetString();
                                var baseStat = s.GetProperty("base_stat").GetInt32();
                                statTotal += baseStat;
                                dto.Stats[statName] = baseStat;
                            }
                            dto.StatsTotal = statTotal;
                        }

                        // Noms multilingues
                        if (speciesData.Value.TryGetProperty("names", out var names) && names.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var nameEntry in names.EnumerateArray())
                            {
                                var lang = nameEntry.GetProperty("language").GetProperty("name").GetString();
                                var value = nameEntry.GetProperty("name").GetString();
                                if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                                    dto.Names[lang] = value;
                            }
                        }

                        // Descriptions Pokédex par langue et par version
                        if (speciesData.Value.TryGetProperty("flavor_text_entries", out var flavorTexts)
                            && flavorTexts.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var entry in flavorTexts.EnumerateArray())
                            {
                                // Récupère la langue (ex: "fr", "en", "ja-Hrkt", etc.)
                                var lang = entry.GetProperty("language").GetProperty("name").GetString();

                                // Récupère la version du jeu (ex: "red", "gold", "shield", etc.)
                                var version = entry.GetProperty("version").GetProperty("name").GetString();

                                // Récupère le texte (et nettoie les retours à la ligne)
                                var value = entry.TryGetProperty("flavor_text", out var textProp)
                                    ? textProp.GetString()?.Replace("\n", " ").Replace("\f", " ").Trim()
                                    : null;

                                if (string.IsNullOrEmpty(lang) || string.IsNullOrEmpty(version) || string.IsNullOrEmpty(value))
                                    continue;

                                // Initialise le dictionnaire pour la langue si nécessaire
                                if (!dto.DescriptionsByGame.ContainsKey(lang))
                                    dto.DescriptionsByGame[lang] = new Dictionary<string, string>();

                                // Ajoute la description si elle n’existe pas déjà
                                if (!dto.DescriptionsByGame[lang].ContainsKey(version))
                                    dto.DescriptionsByGame[lang][version] = value;
                            }
                        }


                        if (speciesData.Value.TryGetProperty("genera", out var genera)
                            && genera.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var entry in genera.EnumerateArray())
                            {
                                var lang = entry.GetProperty("language").GetProperty("name").GetString();
                                var value = entry.TryGetProperty("genus", out var genusProp)
                                    ? genusProp.GetString()?.Replace("Pokémon ", "").Trim()
                                    : null;

                                if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value)
                                    && !dto.Categories.ContainsKey(lang))
                                {
                                    dto.Categories[lang] = value;
                                }
                            }
                        }

                        // Reproduction & capture
                        if (speciesData.Value.TryGetProperty("base_happiness", out var happiness))
                            dto.BaseHappiness = happiness.GetInt32();
                        if (speciesData.Value.TryGetProperty("capture_rate", out var capture))
                            dto.CaptureRate = capture.GetInt32();
                        if (speciesData.Value.TryGetProperty("hatch_counter", out var hatch) && hatch.ValueKind == JsonValueKind.Number)
                            dto.StepsToHatch = hatch.GetInt32() * 255;
                        if (speciesData.Value.TryGetProperty("color", out var color))
                            dto.Color = color.GetProperty("name").GetString();
                        if (speciesData.Value.TryGetProperty("has_gender_differences", out var has_gender_differences))
                            dto.HasGenderDifferences = has_gender_differences.GetBoolean();
                        if (speciesData.Value.TryGetProperty("is_baby", out var is_baby))
                            dto.IsBaby = is_baby.GetBoolean();
                        if (speciesData.Value.TryGetProperty("is_legendary", out var is_legendary))
                            dto.IsLegendary = is_legendary.GetBoolean();
                        if (speciesData.Value.TryGetProperty("is_mythical", out var is_mythical))
                            dto.IsMythical = is_mythical.GetBoolean();

                        // Moves (avec cache)
                        var moveSet = new HashSet<string>();
                        if (pokemonData.TryGetProperty("moves", out var moves) && moves.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var moveEntry in moves.EnumerateArray())
                            {
                                var moveUrl = moveEntry.GetProperty("move").GetProperty("url").GetString();
                                var moveNameTask = moveNameCache.GetOrAdd(moveUrl, url => GetMoveNameEnAsync(url, moveEntry));
                                var moveNameEn = await moveNameTask;

                                if (moveEntry.TryGetProperty("version_group_details", out var details) && details.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (var versionDetail in details.EnumerateArray())
                                    {
                                        var method = versionDetail.GetProperty("move_learn_method").GetProperty("name").GetString();
                                        var level = versionDetail.GetProperty("level_learned_at").GetInt32();
                                        var key = $"{moveNameEn}|{method}";
                                        if (moveSet.Add(key))
                                            dto.Moves.Add(new PokeMoveDto { NameEn = moveNameEn, LearnMethod = method, Level = level });
                                    }
                                }
                            }
                        }

                        // Faiblesses
                        var damageMultipliers = new Dictionary<string, double>();
                        foreach (var typeName in dto.Types)
                        {
                            if (!typeCache.TryGetValue(typeName, out var typeData)) continue;
                            if (!typeData.TryGetProperty("damage_relations", out var damageRelations)) continue;

                            void Apply(IEnumerable<JsonElement> list, double multiplier)
                            {
                                foreach (var t in list)
                                {
                                    var name = t.GetProperty("name").GetString();
                                    if (string.IsNullOrEmpty(name)) continue;
                                    if (!damageMultipliers.ContainsKey(name)) damageMultipliers[name] = 1.0;
                                    damageMultipliers[name] *= multiplier;
                                }
                            }

                            if (damageRelations.TryGetProperty("double_damage_from", out var doubleFrom))
                                Apply(doubleFrom.EnumerateArray(), 2.0);
                            if (damageRelations.TryGetProperty("half_damage_from", out var halfFrom))
                                Apply(halfFrom.EnumerateArray(), 0.5);
                            if (damageRelations.TryGetProperty("no_damage_from", out var noFrom))
                                Apply(noFrom.EnumerateArray(), 0.0);
                        }
                        dto.Weaknesses = damageMultipliers.Where(kv => kv.Value != 1.0)
                                                          .ToDictionary(kv => kv.Key, kv => kv.Value);

                        // Évolutions
                        if (speciesData.Value.TryGetProperty("evolution_chain", out var evoChainProp))
                        {
                            var evoChainUrl = evoChainProp.GetProperty("url").GetString();
                            if (!string.IsNullOrEmpty(evoChainUrl))
                            {
                                dto.Family = await GetEvolutionNamesEnAsync(evoChainUrl);
                                dto.Evolutions = await GetEvolutionDetailsWithItemsAsync(evoChainUrl);
                            }
                        }

                        // Formes spéciales liées
                        var speciesUrlForForms = pokemonData.GetProperty("species").GetProperty("url").GetString();
                        dto.Forms = await GetSpecialFormsAsync(speciesUrlForForms);

                        // Génération
                        if (speciesData.Value.TryGetProperty("generation", out var genProp))
                        {
                            var generationStr = genProp.GetProperty("name").GetString();
                            if(generationStr != null)
                                dto.Generation = RomanToInt(
                                Regex.Match(generationStr, @"generation-(\w+)", RegexOptions.IgnoreCase)
                                     .Groups[1].Value.ToUpper());
                             }

                        pokemons.Add(dto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur sur {item.Name}: {ex.Message}");
                        Error.Add($"Erreur sur {item.Name}: {ex.Message}");
                    }
                });

                await Task.WhenAll(tasks);

                await Task.Delay(500);
                Console.WriteLine($"Progression : {i + batch.Count}/{pokeList.Results.Count} Pokémon traités");
            }

            // 🔹 Export JSON téléchargeable
            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            var json = System.Text.Json.JsonSerializer.Serialize(pokemons, options);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var fileName = $"pokemons_{DateTime.Now:yyyyMMdd_HHmmss}.json";

            var x = Error;

            return File(bytes, "application/json", fileName);
        }

        private int RomanToInt(string roman)
        {
            var map = new Dictionary<char, int> {
                {'I', 1}, {'V', 5}, {'X', 10},
                {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
            };
                    int total = 0;
                    for (int i = 0; i < roman.Length; i++)
                    {
                        int value = map[roman[i]];
                        if (i + 1 < roman.Length && map[roman[i + 1]] > value)
                            total -= value;
                        else
                            total += value;
                    }
             return total;
        }

        [HttpGet("SynchroPokemonForDB")]
        public async Task<IActionResult> SynchroPokemonForDB()
        {
            string json;
            using (StreamReader r = new StreamReader(Constantes.pathExport + "PokeApi/Pokemon.json"))
            {
                json = r.ReadToEnd();

                #region Old code
                List<PokemonExistDto> isExist = new();
                List<string> pokemonsNotExist = new();

                List<PokemonExistDto> errorPoks = [
                    new() { NameDto ="terapagos-stellar", Name="/" },
                    new() { NameDto ="tauros-paldea-combat-breed", Name="Paldean Tauros (Combat Breed)" },
                    new() { NameDto ="braviary-hisui", Name="Hisuian Braviary" },
                    new() { NameDto ="flapple-gmax", Name="Gigantamax Flapple" },
                    new() { NameDto ="orbeetle-gmax", Name="Gigantamax Orbeetle" },
                    new() { NameDto ="toxtricity-low-key", Name="Toxtricity Low Key Form" },
                    new() { NameDto ="linoone-galar", Name="Galarian Linoone" },
                    new() { NameDto ="articuno-galar", Name="Galarian Articuno" },
                    new() { NameDto ="farfetchd-galar", Name="Galarian Farfetch’d" },
                    new() { NameDto ="necrozma-ultra", Name="Ultra Necrozma" },
                    new() { NameDto ="togedemaru-totem", Name="/" },
                    new() { NameDto ="minior-yellow", Name="/" },
                    new() { NameDto ="greninja-battle-bond", Name="/" },
                    new() { NameDto ="gumshoos-totem", Name="/" },
                    new() { NameDto ="raichu-alola", Name="Alolan Raichu" },
                    new() { NameDto ="rattata-alola", Name="Alolan Rattata" },
                    new() { NameDto ="glalie-mega", Name="Mega Glalie" },
                    new() { NameDto ="slowbro-mega", Name="Mega Slowbro" },
                    new() { NameDto ="floette-eternal", Name="/" },
                    new() { NameDto ="sceptile-mega", Name="Mega Sceptile" },
                    new() { NameDto ="mewtwo-mega-y", Name="Mega Mewtwo Y" },
                    new() { NameDto ="aerodactyl-mega", Name="Mega Aerodactyl" },
                    new() { NameDto ="pinsir-mega", Name="Mega Pinsir" },
                    new() { NameDto ="pumpkaboo-super", Name="/" },
                    new() { NameDto ="darmanitan-zen", Name="/" },
                    new() { NameDto ="basculin-blue-striped", Name="Basculin Blue-Striped Form" },
                    new() { NameDto ="dipplin", Name="/" },
                    new() { NameDto ="walking-wake", Name="/" },
                    new() { NameDto ="great-tusk", Name="Great Tusk" },
                    new() { NameDto ="meowstic-male", Name="Meowstic Male" },
                    new() { NameDto ="terapagos-terastal", Name="/" },
                    new() { NameDto ="ursaluna-bloodmoon", Name="/" },
                    new() { NameDto ="koraidon-sprinting-build", Name="/" },
                    new() { NameDto ="squawkabilly-blue-plumage", Name="Blue Plumage Squawkabilly" },
                    new() { NameDto ="squawkabilly-white-plumage", Name="White Plumage Squawkabilly" },
                    new() { NameDto ="squawkabilly-yellow-plumage", Name="Yellow Plumage Squawkabilly" },
                    new() { NameDto ="urshifu-single-strike-gmax", Name="Gigantamax Urshifu (Single Strike Style)" },
                    new() { NameDto ="zacian-crowned", Name="Zacian Crowned Sword" },
                    new() { NameDto ="zygarde-10", Name="Zygarde 10% Forme" },
                    new() { NameDto ="zapdos-galar", Name="Galarian Zapdos" },
                    new() { NameDto ="pikachu-partner-cap", Name="/" },
                    new() { NameDto ="rockruff-own-tempo", Name="/" },
                    new() { NameDto ="mimikyu-totem-disguised", Name="/" },
                    new() { NameDto ="banette-mega", Name="Mega Banette" },
                    new() { NameDto ="aegislash-blade", Name="Aegislash Blade Forme" },
                    new() { NameDto ="wormadam-plant", Name="Wormadam Plant Cloak" },
                    new() { NameDto ="koraidon-gliding-build", Name="Koraidon" },
                    new() { NameDto ="tatsugiri-stretchy", Name="Stretchy Form Tatsugiri" },
                    new() { NameDto ="wooper-paldea", Name="Paldean Wooper" },
                    new() { NameDto ="arcanine-hisui", Name="Hisuian Arcanine" },
                    new() { NameDto ="darmanitan-galar-standard", Name="Galarian Darmanitan" },
                    new() { NameDto ="darumaka-galar", Name="Galarian Darumaka" },
                    new() { NameDto ="exeggutor-alola", Name="Alolan Exeggutor" },
                    new() { NameDto ="pikachu-sinnoh-cap", Name="/" },
                    new() { NameDto ="hoopa-unbound", Name="Hoopa Unbound" },
                    new() { NameDto ="sharpedo-mega", Name="Mega Sharpedo" },
                    new() { NameDto ="lucario-mega", Name="Mega Lucario" },
                    new() { NameDto ="charizard-mega-x", Name="Mega Charizard X" },
                    new() { NameDto ="oinkologne-male", Name="Male Oinkologne" },
                    new() { NameDto ="thundurus-incarnate", Name="Thundurus Incarnate Forme" },
                    new() { NameDto ="miraidon-aquatic-mode", Name="/" },
                    new() { NameDto ="gimmighoul-roaming", Name="/" },
                    new() { NameDto ="oinkologne-female", Name="Female Oinkologne" },
                    new() { NameDto ="decidueye-hisui", Name="Hisuian Decidueye" },
                    new() { NameDto ="dialga-origin", Name="Dialga Origin Forme" },
                    new() { NameDto ="urshifu-rapid-strike-gmax", Name="Gigantamax Urshifu (Rapid Strike Style)" },
                    new() { NameDto ="grimmsnarl-gmax", Name="Gigantamax Grimmsnarl" },
                    new() { NameDto ="sandaconda-gmax", Name="Gigantamax Sandaconda" },
                    new() { NameDto ="corviknight-gmax", Name="Gigantamax Corviknight" },
                    new() { NameDto ="cinderace-gmax", Name="Gigantamax Cinderace" },
                    new() { NameDto ="urshifu-rapid-strike", Name="Urshifu Rapid Strike Style" },
                    new() { NameDto ="darmanitan-galar-zen", Name="/" },
                    new() { NameDto ="weezing-galar", Name="Galarian Weezing" },
                    new() { NameDto ="pikachu-world-cap", Name="/" },
                    new() { NameDto ="minior-green-meteor", Name="/" },
                    new() { NameDto ="salazzle-totem", Name="/" },
                    new() { NameDto ="zygarde-50-power-construct", Name="/" },
                    new() { NameDto ="zygarde-10-power-construct", Name="/" },
                    new() { NameDto ="graveler-alola", Name="Alolan Graveler" },
                    new() { NameDto ="camerupt-mega", Name="Mega Camerupt" },
                    new() { NameDto ="latios-mega", Name="Mega Latios" },
                    new() { NameDto ="aggron-mega", Name="Mega Aggron" },
                    new() { NameDto ="gardevoir-mega", Name="Mega Gardevoir" },
                    new() { NameDto ="tyranitar-mega", Name="Mega Tyranitar" },
                    new() { NameDto ="pumpkaboo-small", Name="/" },
                    new() { NameDto ="gourgeist-average", Name="Gourgeist" },
                    new() { NameDto ="landorus-incarnate", Name="Landorus Incarnate Forme" },
                    new() { NameDto ="giratina-altered", Name="Giratina Altered Forme" },
                    new() { NameDto ="miraidon-low-power-mode", Name="Miraidon" },
                    new() { NameDto ="tatsugiri-droopy", Name="Droopy Form Tatsugiri" },
                    new() { NameDto ="avalugg-hisui", Name="Hisuian Avalugg" },
                    new() { NameDto ="zorua-hisui", Name="Hisuian Zorua" },
                    new() { NameDto ="toxtricity-amped-gmax", Name="Gigantamax Toxtricity" },
                    new() { NameDto ="centiskorch-gmax", Name="Gigantamax Centiskorch" },
                    new() { NameDto ="zamazenta-crowned", Name="Zamazenta Crowned Shield" },
                    new() { NameDto ="eiscue-noice", Name="/" },
                    new() { NameDto ="zigzagoon-galar", Name="Galarian Zigzagoon" },
                    new() { NameDto ="mr-mime-galar", Name="Galarian Mr. Mime" },
                    new() { NameDto ="kommo-o-totem", Name="/" },
                    new() { NameDto ="minior-blue", Name="/" },
                    new() { NameDto ="minior-indigo", Name="/" },
                    new() { NameDto ="minior-violet-meteor", Name="/" },
                    new() { NameDto ="diglett-alola", Name="Alolan Diglett" },
                    new() { NameDto ="altaria-mega", Name="Mega Altaria" },
                    new() { NameDto ="miraidon-glide-mode", Name="/" },
                    new() { NameDto ="enamorus-therian", Name="Enamorus Therian Forme" },
                    new() { NameDto ="voltorb-hisui", Name="Hisuian Voltorb" },
                    new() { NameDto ="alcremie-gmax", Name="Gigantamax Alcremie" },
                    new() { NameDto ="garbodor-gmax", Name="Gigantamax Garbodor" },
                    new() { NameDto ="meowth-gmax", Name="Gigantamax Meowth" },
                    new() { NameDto ="blastoise-gmax", Name="Gigantamax Blastoise" },
                    new() { NameDto ="eevee-starter", Name="/" },
                    new() { NameDto ="pikachu-starter", Name="/" },
                    new() { NameDto ="necrozma-dusk", Name="Necrozma Dusk Mane" },
                    new() { NameDto ="minior-orange", Name="/" },
                    new() { NameDto ="minior-green", Name="/" },
                    new() { NameDto ="minior-indigo-meteor", Name="/" },
                    new() { NameDto ="minior-orange-meteor", Name="/" },
                    new() { NameDto ="golem-alola", Name="Alolan Golem" },
                    new() { NameDto ="vulpix-alola", Name="Alolan Vulpix" },
                    new() { NameDto ="sandshrew-alola", Name="Alolan Sandshrew" },
                    new() { NameDto ="pikachu-original-cap", Name="/" },
                    new() { NameDto ="beedrill-mega", Name="Mega Beedrill" },
                    new() { NameDto ="raticate-totem-alola", Name="/" },
                    new() { NameDto ="rayquaza-mega", Name="Mega Rayquaza" },
                    new() { NameDto ="manectric-mega", Name="Mega Manectric" },
                    new() { NameDto ="houndoom-mega", Name="Mega Houndoom" },
                    new() { NameDto ="mewtwo-mega-x", Name="Mega Mewtwo X" },
                    new() { NameDto ="kyurem-black", Name="Black Kyurem" },
                    new() { NameDto ="thundurus-therian", Name="Thundurus Therian Forme" },
                    new() { NameDto ="hydrapple", Name="/" },
                    new() { NameDto ="gouging-fire", Name="/" },
                    new() { NameDto ="fezandipiti", Name="/" },
                    new() { NameDto ="iron-jugulis", Name="Iron Jugulis" },
                    new() { NameDto ="palafin-zero", Name="Zero Form Palafin" },
                    new() { NameDto ="ogerpon-cornerstone-mask", Name="/" },
                    new() { NameDto ="miraidon-drive-mode", Name="/" },
                    new() { NameDto ="koraidon-limited-build", Name="/" },
                    new() { NameDto ="dudunsparce-three-segment", Name="Three-Segment Form Dudunsparce" },
                    new() { NameDto ="palkia-origin", Name="Palkia Origin Forme" },
                    new() { NameDto ="samurott-hisui", Name="Hisuian Samurott" },
                    new() { NameDto ="sliggoo-hisui", Name="Hisuian Sliggoo" },
                    new() { NameDto ="qwilfish-hisui", Name="Hisuian Qwilfish" },
                    new() { NameDto ="appletun-gmax", Name="Gigantamax Appletun" },
                    new() { NameDto ="duraludon-gmax", Name="Gigantamax Duraludon" },
                    new() { NameDto ="rillaboom-gmax", Name="Gigantamax Rillaboom" },
                    new() { NameDto ="venusaur-gmax", Name="Gigantamax Venusaur" },
                    new() { NameDto ="indeedee-female", Name="Indeedee Female" },
                    new() { NameDto ="cramorant-gulping", Name="Cramorant" },
                    new() { NameDto ="yamask-galar", Name="Galarian Yamask" },
                    new() { NameDto ="stunfisk-galar", Name="Galarian Stunfisk" },
                    new() { NameDto ="moltres-galar", Name="Galarian Moltres" },
                    new() { NameDto ="corsola-galar", Name="Galarian Corsola" },
                    new() { NameDto ="ribombee-totem", Name="/" },
                    new() { NameDto ="mimikyu-totem-busted", Name="/" },
                    new() { NameDto ="alakazam-mega", Name="Mega Alakazam" },
                    new() { NameDto ="rotom-frost", Name="Frost Rotom" },
                    new() { NameDto ="raging-bolt", Name="/" },
                    new() { NameDto ="iron-moth", Name="Iron Moth" },
                    new() { NameDto ="flutter-mane", Name="Flutter Mane" },
                    new() { NameDto ="morpeko-full-belly", Name="Morpeko Full Belly Mode" },
                    new() { NameDto ="mr-rime", Name="Mr. Rime" },
                    new() { NameDto ="aegislash-shield", Name="Aegislash Shield Forme" },
                    new() { NameDto ="deoxys-normal", Name="Deoxys Normal Forme" },
                    new() { NameDto ="nidoran-m", Name="Nidoran♂" },
                    new() { NameDto ="maushold-family-of-three", Name="Maushold (Family of Three)" },
                    new() { NameDto ="sneasel-hisui", Name="Hisuian Sneasel" },
                    new() { NameDto ="typhlosion-hisui", Name="Hisuian Typhlosion" },
                    new() { NameDto ="growlithe-hisui", Name="Hisuian Growlithe" },
                    new() { NameDto ="copperajah-gmax", Name="Gigantamax Copperajah" },
                    new() { NameDto ="lycanroc-dusk", Name="Lycanroc Dusk Form" },
                    new() { NameDto ="mimikyu-busted", Name="/" },
                    new() { NameDto ="geodude-alola", Name="Alolan Geodude" },
                    new() { NameDto ="salamence-mega", Name="Mega Salamence" },
                    new() { NameDto ="pikachu-phd", Name="/" },
                    new() { NameDto ="kyogre-primal", Name="Kyogre Primal Reversion" },
                    new() { NameDto ="metagross-mega", Name="Mega Metagross" },
                    new() { NameDto ="gourgeist-super", Name="/" },
                    new() { NameDto ="castform-rainy", Name="/" },
                    new() { NameDto ="wormadam-trash", Name="Wormadam Trash Cloak" },
                    new() { NameDto ="terapagos", Name="/" },
                    new() { NameDto ="urshifu-single-strike", Name="Urshifu Single Strike Style" },
                    new() { NameDto ="goodra-hisui", Name="Hisuian Goodra" },
                    new() { NameDto ="zoroark-hisui", Name="Hisuian Zoroark" },
                    new() { NameDto ="coalossal-gmax", Name="Gigantamax Coalossal" },
                    new() { NameDto ="inteleon-gmax", Name="Gigantamax Inteleon" },
                    new() { NameDto ="kingler-gmax", Name="Gigantamax Kingler" },
                    new() { NameDto ="ponyta-galar", Name="Galarian Ponyta" },
                    new() { NameDto ="magearna-original", Name="/" },
                    new() { NameDto ="minior-red", Name="Minior Meteor Form" },
                    new() { NameDto ="minior-blue-meteor", Name="/" },
                    new() { NameDto ="oricorio-pau", Name="Oricorio Pa’u Style" },
                    new() { NameDto ="zygarde-complete", Name="Zygarde Complete Forme" },
                    new() { NameDto ="ninetales-alola", Name="Alolan Ninetales" },
                    new() { NameDto ="rotom-heat", Name="Heat Rotom" },
                    new() { NameDto ="castform-snowy", Name="/" },
                    new() { NameDto ="rotom-wash", Name="Wash Rotom" },
                    new() { NameDto ="ogerpon", Name="/" },
                    new() { NameDto ="roaring-moon", Name="Roaring Moon" },
                    new() { NameDto ="iron-treads", Name="Iron Treads" },
                    new() { NameDto ="slither-wing", Name="Slither Wing" },
                    new() { NameDto ="brute-bonnet", Name="Brute Bonnet" },
                    new() { NameDto ="minior-red-meteor", Name="Minior Red Core" },
                    new() { NameDto ="oricorio-baile", Name="Oricorio Baile Style" },
                    new() { NameDto ="iron-boulder", Name="/" },
                    new() { NameDto ="munkidori", Name="/" },
                    new() { NameDto ="iron-bundle", Name="Iron Bundle" },
                    new() { NameDto ="scream-tail", Name="Scream Tail" },
                    new() { NameDto ="dudunsparce-two-segment", Name="Two-Segment Form Dudunsparce" },
                    new() { NameDto ="eiscue-ice", Name="Eiscue" },
                    new() { NameDto ="tapu-koko", Name="Tapu Koko" },
                    new() { NameDto ="wishiwashi-solo", Name="Wishiwashi Solo Form" },
                    new() { NameDto ="darmanitan-standard", Name="Darmanitan Standard Mode" },
                    new() { NameDto ="basculin-red-striped", Name="Basculin Red-Striped Form" },
                    new() { NameDto ="mr-mime", Name="Mr. Mime" },
                    new() { NameDto ="shaymin-sky", Name="Shaymin Sky Forme" },
                    new() { NameDto ="iron-leaves", Name="/" },
                    new() { NameDto ="sandy-shocks", Name="Sandy Shocks" },
                    new() { NameDto ="tatsugiri-curly", Name="Curly Form Tatsugiri" },
                    new() { NameDto ="enamorus-incarnate", Name="Enamorus Incarnate Forme" },
                    new() { NameDto ="tapu-lele", Name="Tapu Lele" },
                    new() { NameDto ="meloetta-aria", Name="Meloetta Aria Forme" },
                    new() { NameDto ="ogerpon-wellspring-mask", Name="/" },
                    new() { NameDto ="basculin-white-striped", Name="Basculin White-Striped Form" },
                    new() { NameDto ="drednaw-gmax", Name="Gigantamax Drednaw" },
                    new() { NameDto ="minior-yellow-meteor", Name="/" },
                    new() { NameDto ="oricorio-sensu", Name="Oricorio Sensu Style" },
                    new() { NameDto ="greninja-ash", Name="Ash-Greninja" },
                    new() { NameDto ="vikavolt-totem", Name="/" },
                    new() { NameDto ="marowak-alola", Name="Alolan Marowak" },
                    new() { NameDto ="meowth-alola", Name="Alolan Meowth" },
                    new() { NameDto ="dugtrio-alola", Name="Alolan Dugtrio" },
                    new() { NameDto ="raticate-alola", Name="Alolan Raticate" },
                    new() { NameDto ="gallade-mega", Name="Mega Gallade" },
                    new() { NameDto ="swampert-mega", Name="Mega Swampert" },
                    new() { NameDto ="garchomp-mega", Name="Mega Garchomp" },
                    new() { NameDto ="scizor-mega", Name="Mega Scizor" },
                    new() { NameDto ="blastoise-mega", Name="Mega Blastoise" },
                    new() { NameDto ="venusaur-mega", Name="Mega Venusaur" },
                    new() { NameDto ="rotom-mow", Name="Mow Rotom" },
                    new() { NameDto ="maushold-family-of-four", Name="Maushold (Family of Four)" },
                    new() { NameDto ="toxtricity-amped", Name="Toxtricity Amped Form" },
                    new() { NameDto ="tapu-bulu", Name="Tapu Bulu" },
                    new() { NameDto ="zygarde-50", Name="Zygarde 50% Forme" },
                    new() { NameDto ="keldeo-ordinary", Name="Keldeo Ordinary Form" },
                    new() { NameDto ="tornadus-incarnate", Name="Tornadus Incarnate Forme" },
                    new() { NameDto ="basculegion-female", Name="Basculegion Female" },
                    new() { NameDto ="toxtricity-low-key-gmax", Name="Gigantamax Toxtricity" },
                    new() { NameDto ="snorlax-gmax", Name="Gigantamax Snorlax" },
                    new() { NameDto ="gengar-gmax", Name="Gigantamax Gengar" },
                    new() { NameDto ="machamp-gmax", Name="Gigantamax Machamp" },
                    new() { NameDto ="calyrex-ice", Name="Ice Rider Calyrex" },
                    new() { NameDto ="eternatus-eternamax", Name="Eternatus" },
                    new() { NameDto ="muk-alola", Name="Alolan Muk" },
                    new() { NameDto ="grimer-alola", Name="Alolan Grimer" },
                    new() { NameDto ="sandslash-alola", Name="Alolan Sandslash" },
                    new() { NameDto ="nidoran-f", Name="Nidoran♀" },
                    new() { NameDto ="lopunny-mega", Name="Mega Lopunny" },
                    new() { NameDto ="steelix-mega", Name="Mega Steelix" },
                    new() { NameDto ="charizard-mega-y", Name="Mega Charizard Y" },
                    new() { NameDto ="gourgeist-small", Name="/" },
                    new() { NameDto ="pumpkaboo-large", Name="/" },
                    new() { NameDto ="mimikyu-disguised", Name="Mimikyu" },
                    new() { NameDto ="tauros-paldea-aqua-breed", Name="Paldean Tauros (Aqua Breed)" },
                    new() { NameDto ="tauros-paldea-blaze-breed", Name="Paldean Tauros (Blaze Breed)" },
                    new() { NameDto ="electrode-hisui", Name="Hisuian Electrode" },
                    new() { NameDto ="hatterene-gmax", Name="Gigantamax Hatterene" },
                    new() { NameDto ="butterfree-gmax", Name="Gigantamax Butterfree" },
                    new() { NameDto ="araquanid-totem", Name="/" },
                    new() { NameDto ="minior-violet", Name="/" },
                    new() { NameDto ="lurantis-totem", Name="/" },
                    new() { NameDto ="lycanroc-midnight", Name="Lycanroc Midnight Form" },
                    new() { NameDto ="oricorio-pom-pom", Name="Oricorio Pom-Pom Style" },
                    new() { NameDto ="pikachu-unova-cap", Name="/" },
                    new() { NameDto ="pikachu-kalos-cap", Name="/" },
                    new() { NameDto ="pikachu-alola-cap", Name="/" },
                    new() { NameDto ="pikachu-hoenn-cap", Name="/" },
                    new() { NameDto ="ampharos-mega", Name="Mega Ampharos" },
                    new() { NameDto ="keldeo-resolute", Name="Keldeo Resolute Form" },
                    new() { NameDto ="landorus-therian", Name="Landorus Therian Forme" },
                    new() { NameDto ="basculegion-male", Name="Basculegion Male" },
                    new() { NameDto ="type-null", Name="Type  Null" },
                    new() { NameDto ="pumpkaboo-average", Name="Pumpkaboo" },
                    new() { NameDto ="shaymin-land", Name="Shaymin Land Forme" },
                    new() { NameDto ="farfetchd", Name="Farfetch’d" },
                    new() { NameDto ="koraidon-swimming-build", Name="/" },
                    new() { NameDto ="lapras-gmax", Name="Gigantamax Lapras" },
                    new() { NameDto ="morpeko-hangry", Name="Morpeko Hangry Mode" },
                    new() { NameDto ="cramorant-gorging", Name="/" },
                    new() { NameDto ="slowking-galar", Name="Galarian Slowking" },
                    new() { NameDto ="marowak-totem", Name="/" },
                    new() { NameDto ="persian-alola", Name="Alolan Persian" },
                    new() { NameDto ="abomasnow-mega", Name="Mega Abomasnow" },
                    new() { NameDto ="latias-mega", Name="Mega Latias" },
                    new() { NameDto ="heracross-mega", Name="Mega Heracross" },
                    new() { NameDto ="meloetta-pirouette", Name="Meloetta Pirouette Forme" },
                    new() { NameDto ="ogerpon-hearthflame-mask", Name="/" },
                    new() { NameDto ="zarude-dada", Name="/" },
                    new() { NameDto ="slowbro-galar", Name="Galarian Slowbro" },
                    new() { NameDto ="meowth-galar", Name="Galarian Meowth" },
                    new() { NameDto ="rapidash-galar", Name="Galarian Rapidash" },
                    new() { NameDto ="wishiwashi-school", Name="Wishiwashi School Form" },
                    new() { NameDto ="pikachu-belle", Name="/" },
                    new() { NameDto ="groudon-primal", Name="Groudon Primal Reversion" },
                    new() { NameDto ="pidgeot-mega", Name="Mega Pidgeot" },
                    new() { NameDto ="kangaskhan-mega", Name="Mega Kangaskhan" },
                    new() { NameDto ="kyurem-white", Name="White Kyurem" },
                    new() { NameDto ="rotom-fan", Name="Fan Rotom" },
                    new() { NameDto ="giratina-origin", Name="Giratina Origin Forme" },
                    new() { NameDto ="pecharunt", Name="/" },
                    new() { NameDto ="archaludon", Name="/" },
                    new() { NameDto ="iron-thorns", Name="Iron Thorns" },
                    new() { NameDto ="indeedee-male", Name="Indeedee Male" },
                    new() { NameDto ="sirfetchd", Name="Sirfetch’d" },
                    new() { NameDto ="mime-jr", Name="Mime Jr." },
                    new() { NameDto ="palafin-hero", Name="Hero Form Palafin" },
                    new() { NameDto ="lilligant-hisui", Name="Hisuian Lilligant" },
                    new() { NameDto ="melmetal-gmax", Name="Gigantamax Melmetal" },
                    new() { NameDto ="pikachu-gmax", Name="Gigantamax Pikachu" },
                    new() { NameDto ="eevee-gmax", Name="Gigantamax Eevee" },
                    new() { NameDto ="charizard-gmax", Name="Gigantamax Charizard" },
                    new() { NameDto ="calyrex-shadow", Name="Shadow Rider Calyrex" },
                    new() { NameDto ="slowpoke-galar", Name="Galarian Slowpoke" },
                    new() { NameDto ="necrozma-dawn", Name="Necrozma Dawn Wings" },
                    new() { NameDto ="pikachu-cosplay", Name="/" },
                    new() { NameDto ="diancie-mega", Name="Mega Diancie" },
                    new() { NameDto ="sableye-mega", Name="Mega Sableye" },
                    new() { NameDto ="absol-mega", Name="Mega Absol" },
                    new() { NameDto ="medicham-mega", Name="Mega Medicham" },
                    new() { NameDto ="blaziken-mega", Name="Mega Blaziken" },
                    new() { NameDto ="mawile-mega", Name="Mega Mawile" },
                    new() { NameDto ="gengar-mega", Name="Mega Gengar" },
                    new() { NameDto ="gyarados-mega", Name="Mega Gyarados" },
                    new() { NameDto ="tornadus-therian", Name="Tornadus Therian Forme" },
                    new() { NameDto ="castform-sunny", Name="/" },
                    new() { NameDto ="deoxys-defense", Name="Deoxys Defense Forme" },
                    new() { NameDto ="deoxys-attack", Name="Deoxys Attack Forme" },
                    new() { NameDto ="iron-crown", Name="/" },
                    new() { NameDto ="okidogi", Name="/" },
                    new() { NameDto ="sinistcha", Name="/" },
                    new() { NameDto ="poltchageist", Name="/" },
                    new() { NameDto ="iron-valiant", Name="Iron Valiant" },
                    new() { NameDto ="iron-hands", Name="Iron Hands" },
                    new() { NameDto ="squawkabilly-green-plumage", Name="Green Plumage Squawkabilly" },
                    new() { NameDto ="tapu-fini", Name="Tapu Fini" },
                    new() { NameDto ="lycanroc-midday", Name="Lycanroc Midday Form" },
                    new() { NameDto ="pikachu-libre", Name="/" },
                    new() { NameDto ="pikachu-pop-star", Name="/" },
                    new() { NameDto ="pikachu-rock-star", Name="/" },
                    new() { NameDto ="audino-mega", Name="Mega Audino" },
                    new() { NameDto ="gourgeist-large", Name="/" },
                    new() { NameDto ="meowstic-female", Name="Meowstic Female" },
                    new() { NameDto ="deoxys-speed", Name="Deoxys Speed Forme" },
                    new() { NameDto ="wormadam-sandy", Name="Wormadam Sandy Cloak" }
                    ];

                if (!string.IsNullOrEmpty(json))
                {
                    List<PokeDto> pokemons = JsonConvert.DeserializeObject<List<PokeDto>>(json);

                    foreach (PokeDto pokeDto in pokemons)
                    {
                        Pokemon? pokemon = await this._repositoryP.FirstOrDefaultByName(pokeDto.Identifier, Constantes.EN);

                        if (pokemon != null)
                        {
                            isExist.Add(new()
                            {
                                Name = pokeDto.Names["en"],
                                NameDto = pokemon.EN.Name
                            });
                            //await this._repositoryT.UpdateAsync(talent);
                        }
                        else
                        {
                            var poke = errorPoks.FirstOrDefault(m => m.NameDto == pokeDto.Identifier);
                            if (poke.Name == "/")
                            {
                                pokemonsNotExist.Add(pokeDto.Id + ": " + pokeDto.Identifier);

                                Pokemon newPokemon = new()
                                {
                                    Guid = Guid.NewGuid(),
                                    Number = pokeDto.NationalId.ToString(),
                                    FR = new()
                                    {
                                        Guid = Guid.NewGuid(),
                                        Name = pokeDto.Names["fr"],
                                        DisplayName = pokeDto.Names["fr"],
                                        //DescriptionVx = pokeDto.Descriptions.ContainsKey("fr") ? pokeDto.Descriptions["fr"] : "",
                                        //DescriptionVy = pokeDto.Descriptions.ContainsKey("fr") ? pokeDto.Descriptions["fr"] : "",
                                        Size = pokeDto.HeightMeters + " m",
                                        Category = "",
                                        Weight = pokeDto.WeightKilograms + " kg",

                                    }
                                };
                            }
                            else
                            {
                                pokemon = await this._repositoryP.FirstOrDefaultByName(poke.Name, Constantes.EN);
                                isExist.Add(new()
                                {
                                    Name = pokeDto.Names["en"],
                                    NameDto = pokemon.EN.Name
                                });
                            }
                        }
                    }

                    //await this._repositoryP.AddRangeAsync(pokemonsNotExist);
                }

                return Ok(pokemonsNotExist);
                #endregion
            }
        }

        // Méthode helper async pour récupérer le nom anglais d’un move
        private async Task<string> GetMoveNameEnAsync(string moveUrl, JsonElement moveEntry)
        {
            try
            {
                var moveData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(moveUrl));
                if (moveData.TryGetProperty("names", out var names))
                {
                    foreach (var n in names.EnumerateArray())
                    {
                        if (n.GetProperty("language").GetProperty("name").GetString() == "en")
                            return n.GetProperty("name").GetString();
                    }
                }
            }
            catch { }

            // fallback si échec
            return moveEntry.GetProperty("move").GetProperty("name").GetString();
        }

        private static async Task<string> GetAbilityNameEnAsync(string url, HttpClient client)
        {
            try
            {
                using var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                using var jsonDoc = await JsonDocument.ParseAsync(stream);
                var root = jsonDoc.RootElement;

                if (root.TryGetProperty("names", out var names) && names.ValueKind == JsonValueKind.Array)
                {
                    foreach (var n in names.EnumerateArray())
                    {
                        var lang = n.GetProperty("language").GetProperty("name").GetString();
                        if (lang == "en")
                            return n.GetProperty("name").GetString() ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération de {url}: {ex.Message}");
            }

            return "";
        }

        private async Task<List<string>> GetEvolutionNamesEnAsync(string evolutionChainUrl)
        {
            var evolutionNames = new List<string>();

            var chainData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(evolutionChainUrl));
            if (chainData.ValueKind != JsonValueKind.Object) return evolutionNames;

            void TraverseChain(JsonElement node)
            {
                var speciesName = node.GetProperty("species").GetProperty("name").GetString();
                if (!string.IsNullOrEmpty(speciesName))
                    evolutionNames.Add(speciesName); // on met le nom interne pour l'instant

                if (node.TryGetProperty("evolves_to", out var evolves) && evolves.ValueKind == JsonValueKind.Array)
                {
                    foreach (var child in evolves.EnumerateArray())
                        TraverseChain(child);
                }
            }

            TraverseChain(chainData.GetProperty("chain"));

            // Récupérer le nom anglais pour chaque Pokémon
            var namesEn = new List<string>();
            foreach (var pokeName in evolutionNames)
            {
                try
                {
                    var speciesData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri($"https://pokeapi.co/api/v2/pokemon-species/{pokeName}"));
                    if (speciesData.TryGetProperty("names", out var names))
                    {
                        foreach (var n in names.EnumerateArray())
                        {
                            if (n.GetProperty("language").GetProperty("name").GetString() == "en")
                            {
                                namesEn.Add(n.GetProperty("name").GetString());
                                break;
                            }
                        }
                    }
                }
                catch { }
            }

            return namesEn.Distinct().ToList();
        }

        private async Task<List<PokeEvolutionDto>> GetEvolutionDetailsWithItemsAsync(string evoChainUrl)
        {
            var evolutions = new List<PokeEvolutionDto>();

            var evoChainData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(evoChainUrl));
            if (evoChainData.ValueKind == JsonValueKind.Undefined) return evolutions;

            // Méthode récursive asynchrone
            async Task ParseChainAsync(JsonElement chain, string fromSpecies = null)
            {
                if (!chain.TryGetProperty("species", out var species)) return;

                var currentSpeciesName = species.GetProperty("name").GetString();
                if (!string.IsNullOrEmpty(fromSpecies))
                {
                    if (chain.TryGetProperty("evolution_details", out var evoDetails) && evoDetails.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var detail in evoDetails.EnumerateArray())
                        {
                            var evoDto = new PokeEvolutionDto
                            {
                                FromSpecies = fromSpecies,
                                ToSpecies = currentSpeciesName,
                                Trigger = detail.TryGetProperty("trigger", out var triggerProp) ? triggerProp.GetProperty("name").GetString() : null,
                                MinLevel = detail.TryGetProperty("min_level", out var minLevelProp) && minLevelProp.ValueKind == JsonValueKind.Number ? minLevelProp.GetInt32() : (int?)null,
                                Happiness = detail.TryGetProperty("min_happiness", out var happyProp) && happyProp.ValueKind == JsonValueKind.Number ? happyProp.GetInt32() : (int?)null,
                                TimeOfDay = detail.TryGetProperty("time_of_day", out var timeProp) ? timeProp.GetString() : null,
                                Item = null
                            };

                            // Objet utilisé
                            if (detail.TryGetProperty("item", out var itemProp) && itemProp.ValueKind != JsonValueKind.Null)
                            {
                                var itemUrl = itemProp.GetProperty("url").GetString();
                                if (!string.IsNullOrEmpty(itemUrl))
                                {
                                    evoDto.Item = new PokeItemDto
                                    {
                                        Identifier = itemProp.GetProperty("name").GetString(),
                                        Names = new Dictionary<string, string>()
                                    };

                                    var itemData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(itemUrl));
                                    if (itemData.TryGetProperty("names", out var itemNames) && itemNames.ValueKind == JsonValueKind.Array)
                                    {
                                        foreach (var nameEntry in itemNames.EnumerateArray())
                                        {
                                            var lang = nameEntry.GetProperty("language").GetProperty("name").GetString();
                                            var value = nameEntry.GetProperty("name").GetString();
                                            if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                                                evoDto.Item.Names[lang] = value;
                                        }
                                    }
                                }
                            }

                            evolutions.Add(evoDto);
                        }
                    }
                }

                // Recurse
                if (chain.TryGetProperty("evolves_to", out var evolvesTo) && evolvesTo.ValueKind == JsonValueKind.Array)
                {
                    foreach (var next in evolvesTo.EnumerateArray())
                    {
                        await ParseChainAsync(next, currentSpeciesName);
                    }
                }
            }

            await ParseChainAsync(evoChainData.GetProperty("chain"));
            return evolutions;
        }

        /// <summary>
        /// Récupère les formes spéciales (méga, g-max, alola, etc.) avec leur nom anglais et conditions si possible
        /// </summary>
        private async Task<List<SpecialFormDto>> GetSpecialFormsAsync(string speciesUrl)
        {
            var forms = new List<SpecialFormDto>();

            try
            {
                var speciesData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(speciesUrl));

                if (speciesData.ValueKind != JsonValueKind.Undefined &&
                    speciesData.TryGetProperty("varieties", out var varieties))
                {
                    foreach (var varEntry in varieties.EnumerateArray())
                    {
                        var isDefault = varEntry.GetProperty("is_default").GetBoolean();
                        var pokeUrl = varEntry.GetProperty("pokemon").GetProperty("url").GetString();
                        var pokeName = varEntry.GetProperty("pokemon").GetProperty("name").GetString();

                        // On ignore la forme par défaut, on ne garde que les variantes
                        if (!isDefault)
                        {
                            string englishName = pokeName;

                            try
                            {
                                var pokeData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(pokeUrl));
                                if (pokeData.TryGetProperty("forms", out var formsArray))
                                {
                                    foreach (var f in formsArray.EnumerateArray())
                                    {
                                        var formName = f.GetProperty("name").GetString();
                                        if (!string.IsNullOrEmpty(formName) && formName != pokeName)
                                            englishName = formName; // on prend le nom anglais du form
                                    }
                                }
                            }
                            catch { }

                            forms.Add(new SpecialFormDto
                            {
                                Identifier = pokeName,
                                NameEn = englishName
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur récupération formes spéciales: {ex.Message}");
            }

            return forms;
        }
    }

    record PokeList(List<PokeRef> Results);
    record PokeRef(string Name, string Url);

    public class MoveDto
    {
        public string Identifier { get; set; }
        public string Type { get; set; }
        public string DamageClass { get; set; }
        public int? Power { get; set; }
        public int? Pp { get; set; }
        public int? Accuracy { get; set; }

        // Noms multilingues
        public Dictionary<string, string> Names { get; set; } = new();

        // Descriptions multilingues
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

    public class AbilityDto
    {
        public string Identifier { get; set; }
        public Dictionary<string, string> Names { get; set; }
        public Dictionary<string, string> Descriptions { get; set; }
    }

    public class AbilityLightDto
    {
        public string Identifier { get; set; }
        public string NameEn { get; set; }
        public bool IsHidden { get; set; }
    }

    public class PokeDto
    {
        public int Id { get; set; }
        // --- Informations de base ---
        public string Identifier { get; set; }              // nom interne (ex: "venusaur-mega")
        public int NationalId { get; set; }                 // ex: 1 ou 10033
        public string NationalIdFormatted { get; set; }     // ex: "0001"
        public int Height { get; set; }
        public int Weight { get; set; }
        public double HeightMeters { get; set; }
        public double WeightKilograms { get; set; }
        public string HeightFeetInches { get; set; }
        public double WeightPounds { get; set; }
        public int BaseExperience { get; set; }

        // --- Typage & compétences ---
        public List<string> Types { get; set; } = new();
        public List<AbilityLightDto> Abilities { get; set; } = new();

        // --- Multilingue ---
        public Dictionary<string, string> Names { get; set; } = new();         // noms de la species (FR, EN, JP...)
        public Dictionary<string, Dictionary<string, string>> DescriptionsByGame { get; set; } = new(); // Textes descriptifs
        public Dictionary<string, string> Categories { get; set; } = new();    // catégories (ex: "Seed Pokémon")

        // --- Statistiques ---
        public Dictionary<string, int> Stats { get; set; } = new();
        public int StatsTotal { get; set; }

        // --- Informations de reproduction et capture ---
        public int? BaseHappiness { get; set; }
        public int? CaptureRate { get; set; }
        public int? StepsToHatch { get; set; }
        public string? Color { get; set; }
        public bool HasGenderDifferences { get; set; }
        public bool IsBaby { get; set; }
        public bool IsLegendary { get; set; }
        public bool IsMythical { get; set; }

        // --- Capacités ---
        public List<PokeMoveDto> Moves { get; set; } = new();

        // --- Évolution ---
        public List<string> Family { get; set; } = new();                   // noms anglais de la lignée évolutive
        public List<PokeEvolutionDto> Evolutions { get; set; } = new();     // détails des conditions d'évolution

        // --- Formes spéciales ---
        public List<SpecialFormDto> Forms { get; set; } = new();            // liste des formes alternatives
        public bool IsSpecialForm { get; set; }                             // indique si ce Pokémon est une forme spéciale
        public string FormIdentifier { get; set; }                          // ex: "mega "gmax "alola"
        public string FormNameEn { get; set; }                              // nom anglais spécifique à la forme
        public Dictionary<string, string> FormNames { get; set; } = new();  // noms localisés de la forme                        // sprite propre à la forme (si différent)

        // --- Faiblesses et génération ---
        public Dictionary<string, double> Weaknesses { get; set; } = new();
        public int Generation { get; set; }
    }

    public class PokeMoveDto
    {
        public string NameEn { get; set; }          // Nom de l'attaque en anglais
        public string LearnMethod { get; set; }     // Méthode d'apprentissage (level-up, egg, tutor, machine)
        public int? Level { get; set; }
    }

    public class PokeEvolutionDto
    {
        public string FromSpecies { get; set; }
        public string ToSpecies { get; set; }
        public string Trigger { get; set; }
        public int? MinLevel { get; set; }
        public int? Happiness { get; set; }
        public string TimeOfDay { get; set; }
        public PokeItemDto Item { get; set; }
    }

    public class PokeItemDto
    {
        public string Identifier { get; set; }
        public Dictionary<string, string> Names { get; set; } // noms traduits
    }

    public class SpecialFormDto
    {
        public string Identifier { get; set; }
        public string NameEn { get; set; }
    }

    public class PokemonExistDto
    {
        public string Name { get; set; }
        public string NameDto { get; set; }
    }
}