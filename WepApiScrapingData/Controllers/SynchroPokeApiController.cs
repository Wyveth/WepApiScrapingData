using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.Json;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.DTOs.Concrete;
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
        private readonly AttaqueRepository _repositoryAT;
        private readonly TypeAttaqueRepository _repositoryTA;
        private readonly TypePokRepository _repositoryTP;
        private readonly TalentRepository _repositoryT;
        #endregion

        public SynchroPokeController(ILogger<SynchroPokeController> logger,
            ScrapingContext context,
            IHttpClientFactory httpClientFactory,
            AttaqueRepository repositoryAT,
            TypeAttaqueRepository repositoryTA,
            TypePokRepository repositoryTP,
            TalentRepository repositoryT)
        {
            _logger = logger;
            _context = context;

            _repositoryAT = repositoryAT;
            _repositoryTA = repositoryTA;
            _repositoryTP = repositoryTP;

            _pokeApiClient = httpClientFactory.CreateClient("pokeapi");
            _repositoryT = repositoryT;
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
                List<Attaque> attaqueNotExist = new();

                if (!string.IsNullOrEmpty(json))
                {
                    List<MoveDto> moves = JsonConvert.DeserializeObject<List<MoveDto>>(json);

                    foreach (MoveDto move in moves)
                    {
                        Attaque? attaque = await this._repositoryAT.GetByName(move.Names["en"]);

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
                            Attaque newAttack = new()
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
                                TypeAttaque = (await _repositoryTA.Find(m => m.Name_EN.Contains(move.DamageClass))).FirstOrDefault(),
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
                List<Talent> talentsNotExist = new();

                if (!string.IsNullOrEmpty(json))
                {
                    List<AbilityDto> abilities = JsonConvert.DeserializeObject<List<AbilityDto>>(json);

                    foreach (AbilityDto ability in abilities)
                    {
                        Talent? talent = await this._repositoryT.GetByName(ability.Names["en"]);

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
                            Talent newTalent = new()
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
            // Exemple : récupérer 150 Pokémon à partir de l'offset 0
            var pokeList = await _pokeApiClient.GetFromJsonAsync<PokeList>("pokemon?limit=150&offset=0");
            if (pokeList?.Results == null || pokeList.Results.Count == 0)
                return BadRequest("Impossible de récupérer la liste des Pokémon depuis PokéAPI.");

            var pokemons = new ConcurrentBag<PokeDto>();

            // Cache des types
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

            // 3️⃣ Cache global pour éviter les appels répétés de moves
            var moveNameCache = new ConcurrentDictionary<string, Task<string>>();

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

                        var speciesUrl = pokemonData.GetProperty("species").GetProperty("url").GetString();
                        var speciesData = await _pokeApiClient.GetFromJsonAsync<JsonElement>(new Uri(speciesUrl));

                        var dto = new PokeDto
                        {
                            Identifier = pokemonData.GetProperty("name").GetString(),
                            Height = pokemonData.TryGetProperty("height", out var h) ? h.GetInt32() : 0,
                            Weight = pokemonData.TryGetProperty("weight", out var w) ? w.GetInt32() : 0,
                            BaseExperience = pokemonData.TryGetProperty("base_experience", out var exp) ? exp.GetInt32() : 0,
                            Types = new List<string>(),
                            Abilities = new List<string>(),
                            Names = new Dictionary<string, string>(),
                            Descriptions = new Dictionary<string, string>(),
                            Weaknesses = new Dictionary<string, double>(),
                            Stats = new Dictionary<string, int>(),
                            Moves = new List<PokeMoveDto>()
                        };

                        // Types
                        if (pokemonData.TryGetProperty("types", out var types) && types.ValueKind == JsonValueKind.Array)
                            foreach (var t in types.EnumerateArray())
                                dto.Types.Add(t.GetProperty("type").GetProperty("name").GetString());

                        // Abilities
                        if (pokemonData.TryGetProperty("abilities", out var abilities) && abilities.ValueKind == JsonValueKind.Array)
                            foreach (var a in abilities.EnumerateArray())
                                dto.Abilities.Add(a.GetProperty("ability").GetProperty("name").GetString());

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
                        if (speciesData.TryGetProperty("names", out var names) && names.ValueKind == JsonValueKind.Array)
                            foreach (var nameEntry in names.EnumerateArray())
                            {
                                var lang = nameEntry.GetProperty("language").GetProperty("name").GetString();
                                var value = nameEntry.GetProperty("name").GetString();
                                if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value))
                                    dto.Names[lang] = value;
                            }

                        // Descriptions multilingues
                        if (speciesData.TryGetProperty("flavor_text_entries", out var flavorTexts) && flavorTexts.ValueKind == JsonValueKind.Array)
                            foreach (var entry in flavorTexts.EnumerateArray())
                            {
                                var lang = entry.GetProperty("language").GetProperty("name").GetString();
                                var value = entry.TryGetProperty("flavor_text", out var textProp)
                                    ? textProp.GetString()?.Replace("\n", " ").Replace("\f", " ")
                                    : null;
                                if (!string.IsNullOrEmpty(lang) && !string.IsNullOrEmpty(value) && !dto.Descriptions.ContainsKey(lang))
                                    dto.Descriptions[lang] = value;
                            }

                        // Base happiness, capture rate, steps to hatch
                        if (speciesData.TryGetProperty("base_happiness", out var happiness)) dto.BaseHappiness = happiness.GetInt32();
                        if (speciesData.TryGetProperty("capture_rate", out var capture)) dto.CaptureRate = capture.GetInt32();
                        if (speciesData.TryGetProperty("hatch_counter", out var hatch) && hatch.ValueKind == JsonValueKind.Number)
                            dto.StepsToHatch = hatch.GetInt32() * 255;

                        // 🔹 Moves avec cache global
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
                                        var key = $"{moveNameEn}|{method}";
                                        if (!moveSet.Contains(key))
                                        {
                                            moveSet.Add(key);
                                            dto.Moves.Add(new PokeMoveDto { NameEn = moveNameEn, LearnMethod = method });
                                        }
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
                        dto.Weaknesses = damageMultipliers.Where(kv => kv.Value != 1.0).ToDictionary(kv => kv.Key, kv => kv.Value);

                        // 1️⃣ Evolutions et famille
                        if (speciesData.TryGetProperty("evolution_chain", out var evoChainProp))
                        {
                            var evoChainUrl = evoChainProp.GetProperty("url").GetString();
                            if (!string.IsNullOrEmpty(evoChainUrl))
                            {
                                dto.Family = await GetEvolutionNamesEnAsync(evoChainUrl);
                                dto.Evolutions = await GetEvolutionDetailsWithItemsAsync(evoChainUrl);
                            }
                        }

                        // 2️⃣ Formes spéciales (Méga, G-Max, Alola...)
                        dto.Forms = await GetSpecialFormsAsync(speciesUrl);

                        // 3️⃣ Génération
                        if (speciesData.TryGetProperty("generation", out var genProp))
                            dto.Generation = genProp.GetProperty("name").GetString();

                        pokemons.Add(dto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur sur {item.Name}: {ex.Message}");
                    }
                });

                await Task.WhenAll(tasks);
                await Task.Delay(500);
                Console.WriteLine($"Progression : {i + batch.Count}/{pokeList.Results.Count} Pokémon traités");
            }

            return Ok(pokemons);
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

    public class PokeDto
    {
        public string Identifier { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseExperience { get; set; }
        public List<string> Types { get; set; }
        public List<string> Abilities { get; set; }
        public Dictionary<string, string> Names { get; set; }
        public Dictionary<string, string> Descriptions { get; set; }
        public Dictionary<string, double> Weaknesses { get; set; }
        public Dictionary<string, int> Stats { get; set; }
        public int StatsTotal { get; set; }
        public int? BaseHappiness { get; set; }
        public int? CaptureRate { get; set; }
        public int? StepsToHatch { get; set; }
        public List<PokeMoveDto> Moves { get; set; }
        public List<string> Family { get; set; }
        public List<PokeEvolutionDto> Evolutions { get; set; }
        public List<SpecialFormDto> Forms { get; set; }
        public string Generation { get; set; }

    }

    public class PokeMoveDto
    {
        public string NameEn { get; set; }          // Nom de l'attaque en anglais
        public string LearnMethod { get; set; }     // Méthode d'apprentissage (level-up, egg, tutor, machine)
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

}
