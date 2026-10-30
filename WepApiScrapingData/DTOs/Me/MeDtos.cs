using WepApiScrapingData.DTOs.Concrete;

namespace WepApiScrapingData.DTOs.Me
{
    public class FavoriteImportDto
    {
        public List<long> PokemonIds { get; set; } = new();
    }

    public class ProfileUpdateDto
    {
        /// <summary>Identifiant du Pokémon avatar, ou null pour le retirer.</summary>
        public long? AvatarPokemonId { get; set; }
    }

    public class ProfileDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public long? AvatarPokemonId { get; set; }
        public PokemonLightDto? Avatar { get; set; }
        public int FavoritesCount { get; set; }
        public int QuizzStarted { get; set; }
        public int QuizzDone { get; set; }
    }
}
