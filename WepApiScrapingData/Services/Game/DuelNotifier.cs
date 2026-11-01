using Microsoft.AspNetCore.SignalR;
using WebApiScrapingData.Domain.Class.Duels;
using WepApiScrapingData.Hubs;

namespace WepApiScrapingData.Services.Game
{
    /// <summary>
    /// Notifications temps réel des duels. Les messages ne portent que l'identifiant : le client relit l'état
    /// via l'API, ce qui garde une seule source de vérité (et évite d'envoyer des informations propres à chaque joueur).
    /// </summary>
    public class DuelNotifier
    {
        public const string DuelUpdatedEvent = "duelUpdated";
        public const string InvitationsChangedEvent = "invitationsChanged";
        public const string HangmanUpdatedEvent = "hangmanUpdated";

        private readonly IHubContext<DuelHub> _hub;
        private readonly ILogger<DuelNotifier> _logger;

        public DuelNotifier(IHubContext<DuelHub> hub, ILogger<DuelNotifier> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task DuelUpdatedAsync(Duel duel)
        {
            var users = new[] { duel.Player1Id, duel.Player2Id }.Where(u => !string.IsNullOrEmpty(u)).Select(u => u!).Distinct().ToList();
            await SafeSendAsync(() => _hub.Clients.Users(users).SendAsync(DuelUpdatedEvent, duel.Guid));
        }

        /// <summary>Partie de pendu en coop modifiée (lettre proposée, invitation acceptée…).</summary>
        public async Task HangmanUpdatedAsync(Guid gameId, params string?[] userIds)
        {
            var users = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => u!).Distinct().ToList();
            if (users.Count > 0)
                await SafeSendAsync(() => _hub.Clients.Users(users).SendAsync(HangmanUpdatedEvent, gameId));
        }

        public async Task InvitationsChangedAsync(string userId) =>
            await SafeSendAsync(() => _hub.Clients.User(userId).SendAsync(InvitationsChangedEvent));

        /// <summary>Une notification perdue ne doit pas faire échouer l'action : le client relit aussi l'état périodiquement.</summary>
        private async Task SafeSendAsync(Func<Task> send)
        {
            try
            {
                await send();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Notification de duel non envoyée.");
            }
        }
    }
}
