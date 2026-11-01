using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace WepApiScrapingData.Services.Mail
{
    /// <summary>Envoi d'e-mails par SMTP (ou écriture dans les journaux si aucun serveur n'est configuré).</summary>
    public class EmailSender
    {
        private readonly EmailOptions _options;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailOptions> options, ILogger<EmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public bool IsConfigured => !string.IsNullOrWhiteSpace(_options.Host) && !string.IsNullOrWhiteSpace(_options.From);

        public async Task SendAsync(string to, string subject, string textBody, string htmlBody)
        {
            if (!IsConfigured)
            {
                _logger.LogWarning("Aucun serveur SMTP configuré (section Email) : e-mail non envoyé à {To}. Sujet : {Subject}{NewLine}{Body}",
                    to, subject, Environment.NewLine, textBody);
                return;
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_options.From!, _options.FromName),
                Subject = subject,
                Body = textBody,
                IsBodyHtml = false
            };
            message.To.Add(to);
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html"));

            using var client = new SmtpClient(_options.Host, _options.Port) { EnableSsl = _options.EnableSsl };
            if (!string.IsNullOrWhiteSpace(_options.UserName))
                client.Credentials = new NetworkCredential(_options.UserName, _options.Password);

            await client.SendMailAsync(message);
            _logger.LogInformation("E-mail « {Subject} » envoyé à {To}", subject, to);
        }
    }
}
