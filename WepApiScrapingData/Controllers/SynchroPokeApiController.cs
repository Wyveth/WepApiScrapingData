using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
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
}
