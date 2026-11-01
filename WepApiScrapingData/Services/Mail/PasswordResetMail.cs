using System.Net;

namespace WepApiScrapingData.Services.Mail
{
    /// <summary>Contenu de l'e-mail de réinitialisation du mot de passe, dans la langue de l'interface.</summary>
    public static class PasswordResetMail
    {
        private record Texts(string Subject, string Hello, string Intro, string Button, string Expiry, string Ignore);

        private static readonly Dictionary<string, Texts> ByLang = new(StringComparer.OrdinalIgnoreCase)
        {
            ["FR"] = new("Réinitialisation de votre mot de passe", "Bonjour {0},",
                "Vous avez demandé à réinitialiser le mot de passe de votre compte PokeQuizz Dex.",
                "Choisir un nouveau mot de passe", "Ce lien est valable {0} minutes.",
                "Si vous n'êtes pas à l'origine de cette demande, ignorez cet e-mail : votre mot de passe ne change pas."),
            ["EN"] = new("Reset your password", "Hi {0},",
                "You asked to reset the password of your PokeQuizz Dex account.",
                "Choose a new password", "This link is valid for {0} minutes.",
                "If you did not make this request, ignore this e-mail: your password stays the same."),
            ["ES"] = new("Restablecer tu contraseña", "Hola {0}:",
                "Has pedido restablecer la contraseña de tu cuenta PokeQuizz Dex.",
                "Elegir una nueva contraseña", "Este enlace es válido durante {0} minutos.",
                "Si no has hecho esta solicitud, ignora este correo: tu contraseña no cambia."),
            ["IT"] = new("Reimposta la password", "Ciao {0},",
                "Hai chiesto di reimpostare la password del tuo account PokeQuizz Dex.",
                "Scegli una nuova password", "Questo link è valido per {0} minuti.",
                "Se non hai fatto tu questa richiesta, ignora questa e-mail: la password non cambia."),
            ["DE"] = new("Passwort zurücksetzen", "Hallo {0},",
                "Du hast angefordert, das Passwort deines PokeQuizz-Dex-Kontos zurückzusetzen.",
                "Neues Passwort wählen", "Dieser Link ist {0} Minuten gültig.",
                "Wenn du das nicht angefordert hast, ignoriere diese E-Mail: Dein Passwort bleibt unverändert.")
        };

        public static string NormalizeLang(string? lang) =>
            lang != null && ByLang.ContainsKey(lang) ? lang.ToUpperInvariant() : "FR";

        public static (string Subject, string Text, string Html) Build(string lang, string userName, string link, int validMinutes)
        {
            var t = ByLang[NormalizeLang(lang)];
            var hello = string.Format(t.Hello, userName);
            var expiry = string.Format(t.Expiry, validMinutes);

            var text = $"{hello}\n\n{t.Intro}\n\n{t.Button} :\n{link}\n\n{expiry}\n\n{t.Ignore}\n";

            var e = (string s) => WebUtility.HtmlEncode(s);
            var html = $@"<!doctype html>
<html><body style=""margin:0;padding:24px;background:#f4f6fb;font-family:Arial,Helvetica,sans-serif;color:#1f2937"">
  <div style=""max-width:520px;margin:0 auto;background:#ffffff;border-radius:16px;padding:28px"">
    <p style=""font-size:18px;font-weight:bold;color:#d42f17;margin:0 0 16px"">PokeQuizz Dex</p>
    <p>{e(hello)}</p>
    <p>{e(t.Intro)}</p>
    <p style=""text-align:center;margin:28px 0"">
      <a href=""{e(link)}"" style=""background:#f0432a;color:#ffffff;text-decoration:none;padding:12px 22px;border-radius:12px;font-weight:bold;display:inline-block"">{e(t.Button)}</a>
    </p>
    <p style=""font-size:13px;color:#6b7280"">{e(expiry)}</p>
    <p style=""font-size:13px;color:#6b7280"">{e(t.Ignore)}</p>
    <p style=""font-size:12px;color:#9ca3af;word-break:break-all"">{e(link)}</p>
  </div>
</body></html>";
            return (t.Subject, text, html);
        }
    }
}
