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
    /// Duels de quiz (compte obligatoire) : matchmaking par mode et difficulté, défis privés par code ami,
    /// jeu en temps réel ou asynchrone. Les changements d'état sont aussi poussés via SignalR (/hubs/duel).
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v1.0/Duel")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class DuelController : ControllerBase
    {
        #region Fields
        private readonly DuelService _duels;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Constructor
        public DuelController(DuelService duels, IWebHostEnvironment environment)
        {
            _duels = duels;
            _environment = environment;
        }
        #endregion

        #region Réglages
        /// <summary>Durées des questions par difficulté et délais (configurables côté serveur).</summary>
        [HttpGet("settings")]
        public IActionResult Settings() => Ok(_duels.GetSettings());
        #endregion

        #region Classement
        /// <summary>Classement public ; connecté, la ligne du joueur est signalée (et ajoutée s'il est hors du top).</summary>
        [AllowAnonymous]
        [HttpGet("leaderboard")]
        public async Task<IActionResult> Leaderboard([FromQuery] string? period = "all", [FromQuery] string? sort = "wins", [FromQuery] int top = 50)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Id");
            return Ok(await _duels.LeaderboardAsync(userId, period, sort, top));
        }
        #endregion

        #region Code ami
        [HttpGet("code")]
        public Task<IActionResult> GetCode() => Run(async user => Ok(await _duels.GetFriendCodeAsync(user)));

        [HttpPost("code/regenerate")]
        public Task<IActionResult> RegenerateCode() => Run(async user => Ok(await _duels.RegenerateFriendCodeAsync(user)));
        #endregion

        #region Duels
        /// <summary>Matchmaking : rejoint un adversaire en attente (même mode, même difficulté) ou se met en attente.</summary>
        [HttpPost("search")]
        public Task<IActionResult> Search([FromBody] DuelSearchDto dto) => Run(async user => Ok(await _duels.SearchAsync(user, dto)));

        /// <summary>Défi privé envoyé via le code ami de l'adversaire.</summary>
        [HttpPost("challenge")]
        public Task<IActionResult> Challenge([FromBody] DuelChallengeDto dto) => Run(async user => Ok(await _duels.ChallengeAsync(user, dto)));

        [HttpGet("mine")]
        public Task<IActionResult> Mine() => Run(async user => Ok(await _duels.ListMineAsync(user)));

        /// <summary>Duel en cours (200) ou aucun (204) : sert au front à bloquer le Pokédex pendant un duel.</summary>
        [HttpGet("active")]
        public Task<IActionResult> Active() => Run(async user =>
        {
            var duel = await _duels.ActiveAsync(user);
            return duel == null ? NoContent() : Ok(duel);
        });

        [HttpGet("invitations")]
        public Task<IActionResult> Invitations() => Run(async user => Ok(await _duels.InvitationsAsync(user)));

        [HttpGet("stats")]
        public Task<IActionResult> Stats() => Run(async user => Ok(await _duels.StatsAsync(user)));

        [HttpGet("{id:guid}")]
        public Task<IActionResult> Get(Guid id) => Run(async user => Ok(await _duels.GetAsync(user, id)));

        [HttpPost("{id:guid}/accept")]
        public Task<IActionResult> Accept(Guid id) => Run(async user => Ok(await _duels.AcceptAsync(user, id)));

        [HttpPost("{id:guid}/decline")]
        public Task<IActionResult> Decline(Guid id) => Run(async user => Ok(await _duels.DeclineAsync(user, id)));

        /// <summary>Annule une recherche ou un défi envoyé (avant que l'adversaire rejoigne).</summary>
        [HttpDelete("{id:guid}")]
        public Task<IActionResult> Cancel(Guid id) => Run(async user => Ok(await _duels.CancelAsync(user, id)));

        [HttpPost("{id:guid}/forfeit")]
        public Task<IActionResult> Forfeit(Guid id) => Run(async user => Ok(await _duels.ForfeitAsync(user, id)));
        #endregion

        #region Questions
        [HttpGet("{id:guid}/questions/{order:int}")]
        public Task<IActionResult> GetQuestion(Guid id, int order, [FromQuery] string? lang = "FR") =>
            Run(async user => Ok(await _duels.GetQuestionAsync(user, id, order, lang)));

        [HttpPost("{id:guid}/questions/{order:int}/answer")]
        public Task<IActionResult> Answer(Guid id, int order, [FromBody] AnswerSubmitDto submit) =>
            Run(async user => Ok(await _duels.AnswerAsync(user, id, order, submit)));

        [HttpGet("{id:guid}/questions/{order:int}/image")]
        public Task<IActionResult> Image(Guid id, int order) =>
            Run(async user => Media(await _duels.GetMediaPathsAsync(user, id, order, sound: false)));

        [HttpGet("{id:guid}/questions/{order:int}/sound")]
        public Task<IActionResult> Sound(Guid id, int order) =>
            Run(async user => Media(await _duels.GetMediaPathsAsync(user, id, order, sound: true)));
        #endregion

        #region Private Methods
        private async Task<IActionResult> Run(Func<string, Task<IActionResult>> action)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Id");
            if (userId == null)
                return Unauthorized();
            try
            {
                return await action(userId);
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
        #endregion
    }
}
