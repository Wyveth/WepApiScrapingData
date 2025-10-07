using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using WebApiScrapingData.Infrastructure.Data;
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
        #endregion

        public SynchroPokeController(ILogger<SynchroPokeController> logger, ScrapingContext context, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _context = context;

            _pokeApiClient = httpClientFactory.CreateClient("pokeapi");
        }

        [HttpGet("SynchroAttacks")]
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
                    Descriptions = new Dictionary<string, string>()
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
    }

    record PokeList(List<PokeRef> Results);
    record PokeRef(string Name, string Url);

    public class MoveDto
    {
        public string Identifier { get; set; }
        public string Type { get; set; }
        public int? Power { get; set; }
        public int? Pp { get; set; }
        public int? Accuracy { get; set; }

        // Noms multilingues
        public Dictionary<string, string> Names { get; set; } = new();

        // Descriptions multilingues
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
