using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.DTOs.Me;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Mapper;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    /// <summary>
    /// Données personnelles du joueur connecté : favoris et profil.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v1.0/Me")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class MeController : ControllerBase
    {
        #region Constantes
        private const int MaxFavoritesImport = 2000;
        #endregion

        #region Fields
        private readonly ScrapingContext _context;
        private readonly PokemonMapper _pokemonMapper;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructor
        public MeController(ScrapingContext context, PokemonMapper pokemonMapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _pokemonMapper = pokemonMapper;
            _userManager = userManager;
        }
        #endregion

        #region Favorites
        /// <summary>Identifiants des Pokémon favoris (pour marquer les cartes du Pokédex).</summary>
        [HttpGet("favorites/ids")]
        public async Task<ActionResult<List<long>>> GetFavoriteIds()
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();

            return await _context.Favorites.AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.CreatedAt)
                .Select(f => f.PokemonId)
                .ToListAsync();
        }

        /// <summary>Pokémon favoris au format léger du Pokédex.</summary>
        [HttpGet("favorites")]
        public async Task<ActionResult<List<PokemonLightDto>>> GetFavorites([FromQuery] string? lang = "FR")
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();
            var language = LangHelper.Normalize(lang);

            var ids = _context.Favorites.Where(f => f.UserId == userId).Select(f => f.PokemonId);
            var pokemons = await _context.Pokemons.AsNoTracking()
                .Where(p => ids.Contains(p.Id))
                .Include(p => p.Pokemon_TypePoks).ThenInclude(t => t.TypePok)
                .IncludeLang(language)
                .AsSplitQuery()
                .ToListAsync();

            return pokemons
                .OrderBy(p => int.TryParse(p.Number, out var n) ? n : int.MaxValue)
                .ThenBy(p => p.Id)
                .Select(p => _pokemonMapper.MapLight(p, language))
                .ToList();
        }

        /// <summary>Ajoute un favori (idempotent).</summary>
        [HttpPut("favorites/{pokemonId:long}")]
        public async Task<IActionResult> AddFavorite(long pokemonId)
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();

            if (!await _context.Pokemons.AnyAsync(p => p.Id == pokemonId))
                return NotFound();

            if (!await _context.Favorites.AnyAsync(f => f.UserId == userId && f.PokemonId == pokemonId))
            {
                _context.Favorites.Add(new Favorite { UserId = userId, PokemonId = pokemonId });
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        /// <summary>Retire un favori (idempotent).</summary>
        [HttpDelete("favorites/{pokemonId:long}")]
        public async Task<IActionResult> RemoveFavorite(long pokemonId)
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();

            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.PokemonId == pokemonId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        /// <summary>
        /// Fusionne les favoris d'un invité (stockés dans le navigateur) avec ceux du compte après connexion.
        /// Renvoie la liste complète des identifiants favoris.
        /// </summary>
        [HttpPost("favorites/import")]
        public async Task<ActionResult<List<long>>> ImportFavorites([FromBody] FavoriteImportDto dto)
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();

            var requested = (dto?.PokemonIds ?? new List<long>()).Distinct().Take(MaxFavoritesImport).ToList();
            if (requested.Count > 0)
            {
                var existingPokemons = await _context.Pokemons.Where(p => requested.Contains(p.Id)).Select(p => p.Id).ToListAsync();
                var alreadyFavorite = await _context.Favorites.Where(f => f.UserId == userId).Select(f => f.PokemonId).ToListAsync();

                var toAdd = existingPokemons.Except(alreadyFavorite)
                    .Select(id => new Favorite { UserId = userId, PokemonId = id })
                    .ToList();

                if (toAdd.Count > 0)
                {
                    _context.Favorites.AddRange(toAdd);
                    await _context.SaveChangesAsync();
                }
            }

            return await GetFavoriteIds();
        }
        #endregion

        #region Profile
        [HttpGet("profile")]
        public async Task<ActionResult<ProfileDto>> GetProfile([FromQuery] string? lang = "FR")
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();
            var language = LangHelper.Normalize(lang);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var profile = await _context.UserProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == userId);

            PokemonLightDto? avatar = null;
            if (profile?.AvatarPokemonId != null)
            {
                var pokemon = await _context.Pokemons.AsNoTracking()
                    .Where(p => p.Id == profile.AvatarPokemonId)
                    .Include(p => p.Pokemon_TypePoks).ThenInclude(t => t.TypePok)
                    .IncludeLang(language)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync();
                if (pokemon != null)
                    avatar = _pokemonMapper.MapLight(pokemon, language);
            }

            var userQuizz = _context.Quizzs.Where(q => EF.Property<string>(q, "IdentityUserId") == userId);

            return new ProfileDto
            {
                UserName = user.UserName,
                Email = user.Email,
                AvatarPokemonId = profile?.AvatarPokemonId,
                Avatar = avatar,
                FavoritesCount = await _context.Favorites.CountAsync(f => f.UserId == userId),
                QuizzStarted = await userQuizz.CountAsync(),
                QuizzDone = await userQuizz.CountAsync(q => q.Done)
            };
        }

        [HttpPut("profile")]
        public async Task<ActionResult<ProfileDto>> UpdateProfile([FromBody] ProfileUpdateDto dto, [FromQuery] string? lang = "FR")
        {
            var userId = CurrentUserId();
            if (userId == null) return Unauthorized();

            if (dto.AvatarPokemonId != null && !await _context.Pokemons.AnyAsync(p => p.Id == dto.AvatarPokemonId))
                return BadRequest("Pokémon inconnu.");

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
            {
                profile = new UserProfile { UserId = userId };
                _context.UserProfiles.Add(profile);
            }

            profile.AvatarPokemonId = dto.AvatarPokemonId;
            profile.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return await GetProfile(lang);
        }
        #endregion

        #region Private Methods
        private string? CurrentUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Id");
        #endregion
    }
}
