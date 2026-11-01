using Microsoft.AspNetCore.Identity;

namespace WebApiScrapingData.Domain.Class
{
    /// <summary>
    /// Pokémon mis en favori par un utilisateur connecté.
    /// Les favoris des invités restent dans le navigateur et peuvent être importés après connexion.
    /// </summary>
    public class Favorite
    {
        public long Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        public long PokemonId { get; set; }
        public Pokemon? Pokemon { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
