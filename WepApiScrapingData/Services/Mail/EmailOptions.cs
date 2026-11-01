namespace WepApiScrapingData.Services.Mail
{
    /// <summary>
    /// Serveur SMTP pour les e-mails de l'application (section "Email").
    /// Si Host est vide, aucun e-mail n'est envoyé : son contenu est écrit dans les journaux (pratique en développement).
    /// </summary>
    public class EmailOptions
    {
        public const string Section = "Email";

        public string? Host { get; set; }
        public int Port { get; set; } = 587;
        /// <summary>STARTTLS / SSL vers le serveur SMTP.</summary>
        public bool EnableSsl { get; set; } = true;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        /// <summary>Adresse d'expédition (ex. no-reply@mondomaine.fr).</summary>
        public string? From { get; set; }
        public string FromName { get; set; } = "PokeQuizz Dex";
    }

    /// <summary>Adresse publique du front, pour les liens envoyés par e-mail (section "App").</summary>
    public class AppOptions
    {
        public const string Section = "App";

        /// <summary>Ex. https://pokequizz.mondomaine.fr (sans / final).</summary>
        public string FrontUrl { get; set; } = "http://localhost:4200";
    }
}
