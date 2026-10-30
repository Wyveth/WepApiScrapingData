using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WepApiScrapingData.DTOs.Play;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Services.Game;

namespace WepApiScrapingData.Controllers.Quizz
{
    /// <summary>
    /// Jeu de quiz pour le client web/mobile.
    /// Ouvert aux invités (le quiz est alors identifié uniquement par son Guid) et aux comptes connectés
    /// (le quiz est rattaché au compte et visible uniquement par lui).
    /// </summary>
    [ApiController]
    [Route("api/v1.0/Play")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PlayController : ControllerBase
    {
        #region Fields
        private readonly QuizzGameService _game;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Constructor
        public PlayController(QuizzGameService game, IWebHostEnvironment environment)
        {
            _game = game;
            _environment = environment;
        }
        #endregion

        #region Endpoints
        /// <summary>Crée un quiz (générations et difficultés choisies).</summary>
        [AllowAnonymous]
        [HttpPost]
        public Task<IActionResult> Create([FromBody] QuizzCreateDto options) =>
            Run(async () =>
            {
                var summary = await _game.CreateAsync(options, CurrentUserId());
                return CreatedAtAction(nameof(Get), new { id = summary.Id }, summary);
            });

        /// <summary>Quiz du compte connecté, du plus récent au plus ancien.</summary>
        [Authorize]
        [HttpGet("mine")]
        public Task<IActionResult> Mine() =>
            Run(async () => Ok(await _game.ListMineAsync(CurrentUserId()!)));

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public Task<IActionResult> Get(Guid id) =>
            Run(async () => Ok(await _game.GetSummaryAsync(id, CurrentUserId())));

        /// <summary>Question à afficher, sans la correction tant qu'elle n'a pas reçu de réponse.</summary>
        [AllowAnonymous]
        [HttpGet("{id:guid}/questions/{order:int}")]
        public Task<IActionResult> GetQuestion(Guid id, int order, [FromQuery] string? lang = "FR") =>
            Run(async () => Ok(await _game.GetQuestionAsync(id, order, lang, CurrentUserId())));

        /// <summary>Envoie la réponse et renvoie la correction.</summary>
        [AllowAnonymous]
        [HttpPost("{id:guid}/questions/{order:int}/answer")]
        public Task<IActionResult> Answer(Guid id, int order, [FromBody] AnswerSubmitDto submit) =>
            Run(async () => Ok(await _game.SubmitAsync(id, order, submit, CurrentUserId())));

        /// <summary>Image du sujet de la question (servie ici pour ne pas exposer le nom du fichier).</summary>
        [AllowAnonymous]
        [HttpGet("{id:guid}/questions/{order:int}/image")]
        public Task<IActionResult> Image(Guid id, int order) =>
            Run(async () => Media(await _game.GetSubjectMediaPathsAsync(id, order, sound: false, CurrentUserId())));

        /// <summary>Cri du Pokémon sujet de la question.</summary>
        [AllowAnonymous]
        [HttpGet("{id:guid}/questions/{order:int}/sound")]
        public Task<IActionResult> Sound(Guid id, int order) =>
            Run(async () => Media(await _game.GetSubjectMediaPathsAsync(id, order, sound: true, CurrentUserId())));

        [AllowAnonymous]
        [HttpGet("{id:guid}/result")]
        public Task<IActionResult> Result(Guid id, [FromQuery] string? lang = "FR") =>
            Run(async () => Ok(await _game.GetResultAsync(id, lang, CurrentUserId())));

        /// <summary>Rattache un quiz joué en invité au compte qui vient de se connecter.</summary>
        [Authorize]
        [HttpPost("{id:guid}/claim")]
        public Task<IActionResult> Claim(Guid id) =>
            Run(async () => Ok(await _game.ClaimAsync(id, CurrentUserId()!)));

        [AllowAnonymous]
        [HttpDelete("{id:guid}")]
        public Task<IActionResult> Delete(Guid id) =>
            Run(async () =>
            {
                await _game.DeleteAsync(id, CurrentUserId());
                return NoContent();
            });
        #endregion

        #region Private Methods
        private async Task<IActionResult> Run(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (QuizzGameException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        /// <summary>Sert le premier fichier existant parmi les chemins candidats (restreints au dossier Content).</summary>
        private IActionResult Media(IEnumerable<string> relativePaths)
        {
            var contentRoot = Path.GetFullPath(Path.Combine(_environment.ContentRootPath, "Content"));
            var fullPath = relativePaths
                .Select(p => Path.GetFullPath(Path.Combine(_environment.ContentRootPath, p)))
                .FirstOrDefault(p => p.StartsWith(contentRoot, StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(p));

            if (fullPath == null)
                return NotFound();

            var contentType = Path.GetExtension(fullPath).ToLowerInvariant() switch
            {
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".webp" => "image/webp",
                ".ogg" => "audio/ogg",
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                _ => "application/octet-stream"
            };
            return PhysicalFile(fullPath, contentType);
        }

        private string? CurrentUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Id");
        #endregion
    }
}
