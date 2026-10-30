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
    /// Pendu Pokémon : solo (compte facultatif, partie identifiée par son Guid) ou coop tour par tour (comptes, code ami).
    /// En coop, les changements sont poussés via SignalR (/hubs/duel, événement "hangmanUpdated").
    /// </summary>
    [ApiController]
    [Route("api/v1.0/Hangman")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HangmanController : ControllerBase
    {
        private readonly HangmanService _hangman;
        private readonly IWebHostEnvironment _environment;

        public HangmanController(HangmanService hangman, IWebHostEnvironment environment)
        {
            _hangman = hangman;
            _environment = environment;
        }

        /// <summary>Nouvelle partie solo.</summary>
        [AllowAnonymous]
        [HttpPost]
        public Task<IActionResult> CreateSolo([FromBody] HangmanCreateDto dto) => Run(async user => Ok(await _hangman.CreateSoloAsync(user, dto)));

        /// <summary>Invitation coop via le code ami du partenaire.</summary>
        [Authorize]
        [HttpPost("coop")]
        public Task<IActionResult> Invite([FromBody] HangmanInviteDto dto) => Run(async user => Ok(await _hangman.InviteAsync(user!, dto)));

        [Authorize]
        [HttpGet("invitations")]
        public Task<IActionResult> Invitations() => Run(async user => Ok(await _hangman.InvitationsAsync(user!)));

        [Authorize]
        [HttpGet("mine")]
        public Task<IActionResult> Mine() => Run(async user => Ok(await _hangman.MineAsync(user!)));

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public Task<IActionResult> Get(Guid id) => Run(async user => Ok(await _hangman.GetAsync(user, id)));

        [AllowAnonymous]
        [HttpPost("{id:guid}/guess")]
        public Task<IActionResult> Guess(Guid id, [FromBody] HangmanGuessDto dto) => Run(async user => Ok(await _hangman.GuessAsync(user, id, dto)));

        [Authorize]
        [HttpPost("{id:guid}/accept")]
        public Task<IActionResult> Accept(Guid id) => Run(async user => Ok(await _hangman.AcceptAsync(user!, id)));

        [Authorize]
        [HttpPost("{id:guid}/decline")]
        public Task<IActionResult> Decline(Guid id) => Run(async user => Ok(await _hangman.DeclineAsync(user!, id)));

        /// <summary>Annule l'invitation ou abandonne la partie (le score est conservé).</summary>
        [AllowAnonymous]
        [HttpDelete("{id:guid}")]
        public Task<IActionResult> Quit(Guid id) => Run(async user => Ok(await _hangman.QuitAsync(user, id)));

        /// <summary>Image du Pokémon à deviner, une fois l'indice « silhouette » débloqué (le client l'affiche en noir).</summary>
        [AllowAnonymous]
        [HttpGet("{id:guid}/silhouette")]
        public Task<IActionResult> Silhouette(Guid id) => Run(async user =>
        {
            var contentRoot = Path.GetFullPath(Path.Combine(_environment.ContentRootPath, "Content"));
            var fullPath = (await _hangman.SilhouettePathsAsync(user, id))
                .Select(p => Path.GetFullPath(Path.Combine(_environment.ContentRootPath, p)))
                .FirstOrDefault(p => p.StartsWith(contentRoot, StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(p));
            return fullPath == null ? NotFound() : PhysicalFile(fullPath, "image/png");
        });

        private async Task<IActionResult> Run(Func<string?, Task<IActionResult>> action)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Id");
            try
            {
                return await action(userId);
            }
            catch (QuizzGameException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }
    }
}
