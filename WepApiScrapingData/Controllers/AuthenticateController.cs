using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Configurations;
using WebApiScrapingData.Infrastructure.Data;
using WepApiScrapingData.DTOs;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Services.Mail;

namespace WepApiScrapingData.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class AuthenticateController : ControllerBase
    {
        #region Fields
        private readonly SecurityOption _option;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly ScrapingContext _context;
        private readonly IAuthorizationService _authorization;
        private readonly EmailSender _emailSender;
        private readonly AppOptions _app;

        /// <summary>Durée de validité du lien de réinitialisation (voir DataProtectionTokenProviderOptions dans Program.cs).</summary>
        public const int ResetLinkMinutes = 60;
        /// <summary>Délai minimal entre deux demandes pour une même adresse (limite les envois abusifs).</summary>
        private static readonly TimeSpan ResetRequestCooldown = TimeSpan.FromMinutes(1);
        private static readonly ConcurrentDictionary<string, DateTime> LastResetRequests = new(StringComparer.OrdinalIgnoreCase);
        #endregion

        #region Constructor
        public AuthenticateController(
            ILogger<AuthenticateController> logger,
            UserManager<IdentityUser> userManager,
            IOptions<SecurityOption> options,
            ScrapingContext context,
            IAuthorizationService authorization,
            EmailSender emailSender,
            IOptions<AppOptions> app)
        {
            _emailSender = emailSender;
            _app = app.Value;
            _authorization = authorization;
            _option = options.Value;
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Public methods

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto dtoUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmail = await _userManager.FindByEmailAsync(dtoUser.Email);
            if (existingEmail != null)
                return BadRequest(new { error = "Cette adresse e-mail est déjà utilisée." });

            var existingUser = await _userManager.FindByNameAsync(dtoUser.UserName);
            if (existingUser != null)
                return BadRequest(new { error = "Ce nom d’utilisateur est déjà pris." });

            var user = new IdentityUser
            {
                UserName = dtoUser.UserName,
                Email = dtoUser.Email
            };

            var result = await _userManager.CreateAsync(user, dtoUser.Password);

            if (result.Succeeded)
            {
                var tokens = await GenerateTokensAsync(user);
                return Ok(tokens);
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto dtoUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dtoUser.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, dtoUser.Password))
                {
                    var tokens = await GenerateTokensAsync(user);
                    return Ok(tokens);
                }

                return BadRequest(new { error = "Email ou mot de passe incorrect." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du login");
                return Problem("Erreur interne lors de la connexion.");
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Id");
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { error = "Utilisateur introuvable" });

            // Administrateur : compte listé dans Jwt:AdminUserIds (le front affiche alors la page d'administration)
            var isAdmin = (await _authorization.AuthorizeAsync(User, SecurityMethods.AdministratorPolicy)).Succeeded;

            return Ok(new
            {
                user.UserName,
                user.Email,
                user.Id,
                IsAdmin = isAdmin
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequestDto request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_option.Key);

            try
            {
                var principal = jwtTokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = false // on veut pouvoir rafraîchir un token expiré
                }, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                {
                    return BadRequest(new { error = "Token invalide" });
                }

                var jti = jwtSecurityToken.Id;
                var userId = principal.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

                var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
                if (storedRefreshToken == null)
                    return BadRequest(new { error = "Refresh token invalide" });

                if (storedRefreshToken.ExpiresAt < DateTime.UtcNow)
                    return BadRequest(new { error = "Refresh token expiré" });

                if (storedRefreshToken.Used || storedRefreshToken.Revoked)
                    return BadRequest(new { error = "Refresh token déjà utilisé ou révoqué" });

                if (storedRefreshToken.JwtId != jti)
                    return BadRequest(new { error = "Le refresh token ne correspond pas au JWT" });

                storedRefreshToken.Used = true;
                _context.RefreshTokens.Update(storedRefreshToken);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(userId);
                var tokens = await GenerateTokensAsync(user);

                return Ok(tokens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du rafraîchissement du token");
                return BadRequest(new { error = "Token invalide" });
            }
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { error = "Utilisateur introuvable" });

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // 🔒 Révoque tous les refresh tokens du user
            var tokens = await _context.RefreshTokens
                .Where(t => t.UserId == user.Id && !t.Revoked && !t.Used)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.Revoked = true;
                token.Used = true;
                token.RevokedAt = DateTime.UtcNow;
            }

            _context.RefreshTokens.UpdateRange(tokens);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Tous les refresh tokens ont été révoqués pour l'utilisateur {UserId} après changement de mot de passe", user.Id);

            return Ok(new { message = "Mot de passe changé. Tous les tokens ont été révoqués." });
        }

        /// <summary>
        /// Mot de passe oublié : envoie un lien de réinitialisation par e-mail.
        /// Répond toujours 200, que l'adresse existe ou non, pour ne pas révéler quels comptes existent.
        /// </summary>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var email = dto?.Email?.Trim() ?? string.Empty;
            var accepted = Ok(new { message = "Si un compte existe pour cette adresse, un e-mail de réinitialisation vient d'être envoyé." });
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { error = "Adresse e-mail requise." });

            var now = DateTime.UtcNow;
            if (LastResetRequests.TryGetValue(email, out var last) && now - last < ResetRequestCooldown)
                return accepted;
            LastResetRequests[email] = now;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || string.IsNullOrEmpty(user.Email))
                return accepted;

            var lang = PasswordResetMail.NormalizeLang(dto!.Lang);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var link = $"{_app.FrontUrl.TrimEnd('/')}/{lang}/reset-password?email={Uri.EscapeDataString(user.Email)}&token={encodedToken}";

            var (subject, text, html) = PasswordResetMail.Build(lang, user.UserName ?? user.Email, link, ResetLinkMinutes);
            try
            {
                await _emailSender.SendAsync(user.Email, subject, text, html);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Échec de l'envoi de l'e-mail de réinitialisation pour l'utilisateur {UserId}", user.Id);
                return Problem("L'e-mail n'a pas pu être envoyé. Réessayez plus tard.");
            }

            return accepted;
        }

        /// <summary>Nouveau mot de passe à partir du lien reçu par e-mail. Toutes les sessions existantes sont révoquées.</summary>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var invalid = BadRequest(new { error = "Lien invalide ou expiré. Refaites une demande de réinitialisation." });
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Token))
                return invalid;

            var user = await _userManager.FindByEmailAsync(dto.Email.Trim());
            if (user == null)
                return invalid;

            string token;
            try
            {
                token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));
            }
            catch (FormatException)
            {
                return invalid;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword ?? string.Empty);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e.Code == "InvalidToken"))
                    return invalid;
                return BadRequest(new { errors = result.Errors.Select(e => e.Description).ToList() });
            }

            // Les anciennes sessions (refresh tokens) ne doivent plus fonctionner
            var tokens = await _context.RefreshTokens.Where(t => t.UserId == user.Id && !t.Revoked && !t.Used).ToListAsync();
            foreach (var refresh in tokens)
            {
                refresh.Revoked = true;
                refresh.Used = true;
                refresh.RevokedAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();

            _logger.LogInformation("Mot de passe réinitialisé pour l'utilisateur {UserId} ; {Count} session(s) révoquée(s)", user.Id, tokens.Count);
            return Ok(new { message = "Mot de passe modifié. Vous pouvez vous connecter." });
        }

        [HttpPost("logout")]
        [Authorize] // Nécessite que l’utilisateur soit connecté
        public async Task<IActionResult> Logout([FromBody] TokenRequestDto request)
        {
            try
            {
                // Recherche du refresh token
                var storedToken = await _context.RefreshTokens
                    .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

                if (storedToken == null)
                    return BadRequest(new { error = "Refresh token introuvable." });

                if (storedToken.Revoked)
                    return BadRequest(new { error = "Refresh token déjà révoqué." });

                // Révocation
                storedToken.Revoked = true;
                storedToken.Used = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Refresh token révoqué pour l'utilisateur {UserId}", storedToken.UserId);

                return Ok(new { message = "Déconnexion réussie. Le refresh token a été révoqué." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la déconnexion.");
                return Problem("Erreur interne lors de la déconnexion.");
            }
        }

        [HttpPost("logout-all")]
        [Authorize] // Nécessite que l’utilisateur soit connecté
        public async Task<IActionResult> LogoutAll([FromBody] TokenRequestDto request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { error = "Utilisateur non identifié." });

                // Récupère tous les refresh tokens actifs de l'utilisateur
                var tokens = await _context.RefreshTokens
                    .Where(t => t.UserId == userId && !t.Revoked && !t.Used)
                    .ToListAsync();

                if (!tokens.Any())
                    return Ok(new { message = "Aucun refresh token actif à révoquer." });

                // Révoque tous les refresh tokens
                foreach (var token in tokens)
                {
                    token.Revoked = true;
                    token.Used = true;
                    token.RevokedAt = DateTime.UtcNow;
                }

                _context.RefreshTokens.UpdateRange(tokens);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Tous les refresh tokens ont été révoqués pour l'utilisateur {UserId}", userId);

                return Ok(new { message = "Déconnexion réussie. Tous les refresh tokens ont été révoqués." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la déconnexion.");
                return Problem("Erreur interne lors de la déconnexion.");
            }
        }
        #endregion

        #region Internal methods

        private async Task<object> GenerateTokensAsync(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_option.Key);

            // 1️⃣ Crée les claims pour le JWT
            var claims = new[]
            {
        new Claim("Id", user.Id),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT ID unique
    };

            // 2️⃣ Crée le token JWT
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // JWT court
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            // 3️⃣ Supprime les anciens refresh tokens expirés ou utilisés
            var oldTokens = _context.RefreshTokens
                .Where(t => t.UserId == user.Id && (t.Used || (t.ExpiresAt.HasValue && t.ExpiresAt.Value < DateTime.UtcNow)));

            if (oldTokens.Any())
            {
                _context.RefreshTokens.RemoveRange(oldTokens);
                await _context.SaveChangesAsync();
            }

            // 4️⃣ Crée un nouveau refresh token
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),     // Token unique
                JwtId = token.Id,                      // Lie le refresh token au JWT
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7) // Refresh token long
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            // 5️⃣ Retourne JWT + refresh token + expiration
            return new
            {
                token = jwtToken,
                refreshToken = refreshToken.Token,
                tokenExpiresAt = tokenDescriptor.Expires,
                refreshTokenExpiresAt = refreshToken.ExpiresAt
            };
        }


        #endregion
    }
}
