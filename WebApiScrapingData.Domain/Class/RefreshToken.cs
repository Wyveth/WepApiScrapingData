using Microsoft.AspNetCore.Identity;

namespace WebApiScrapingData.Domain.Class
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string JwtId { get; set; } = string.Empty; // ← ajouté
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

        public bool Used { get; set; } = false;
        public bool Revoked { get; set; } = false;
        public DateTime? RevokedAt { get; set; }
    }
}
