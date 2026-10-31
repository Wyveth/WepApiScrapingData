using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Services.Game;

namespace WepApiScrapingData.Controllers
{
    /// <summary>
    /// Administration des données de jeu. Réservé aux comptes listés dans Jwt:AdminUserIds (politique Administrator).
    /// </summary>
    [ApiController]
    [Authorize(Policy = SecurityMethods.AdministratorPolicy)]
    [Route("api/v1.0/Admin")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AdminController : ControllerBase
    {
        /// <summary>Texte à renvoyer pour confirmer la purge (évite une suppression par erreur).</summary>
        public const string PurgeConfirmation = "PURGE";

        private readonly GameDataAdminService _gameData;

        public AdminController(GameDataAdminService gameData)
        {
            _gameData = gameData;
        }

        /// <summary>Ce que la purge supprimerait (aucune modification).</summary>
        [HttpGet("game-data")]
        public async Task<IActionResult> Preview() => Ok(await _gameData.PreviewAsync());

        /// <summary>Vide les quiz hors duels et toutes les parties de pendu. Corps : { "confirm": "PURGE" }.</summary>
        [HttpPost("game-data/purge")]
        public async Task<IActionResult> Purge([FromBody] PurgeRequestDto dto)
        {
            if (!string.Equals(dto?.Confirm, PurgeConfirmation, StringComparison.Ordinal))
                return BadRequest(new { message = $"Confirmation manquante : envoyer {{ \"confirm\": \"{PurgeConfirmation}\" }}." });

            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "?";
            return Ok(await _gameData.PurgeAsync(adminId));
        }
    }

    public class PurgeRequestDto
    {
        public string? Confirm { get; set; }
    }
}
