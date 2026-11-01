using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiScrapingData.Domain.Class
{
    /// <summary>
    /// Informations de profil joueur rattachées à un compte Identity
    /// (équivalent du profil local de l'application Xamarin).
    /// </summary>
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        /// <summary>Pokémon choisi comme avatar.</summary>
        public long? AvatarPokemonId { get; set; }
        public Pokemon? AvatarPokemon { get; set; }

        /// <summary>Code ami permanent (8 caractères, sans tiret) pour les duels privés.</summary>
        [MaxLength(16)]
        public string? FriendCode { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
