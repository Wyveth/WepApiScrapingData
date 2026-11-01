namespace WepApiScrapingData.DTOs
{
    /// <summary>Demande de lien de réinitialisation.</summary>
    public class ForgotPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        /// <summary>Langue de l'e-mail et du lien : FR, EN, ES, IT ou DE.</summary>
        public string? Lang { get; set; } = "FR";
    }

    /// <summary>Nouveau mot de passe, avec le jeton reçu par e-mail.</summary>
    public class ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
