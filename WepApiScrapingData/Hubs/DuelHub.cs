using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WepApiScrapingData.Hubs
{
    /// <summary>
    /// Canal temps réel des duels (/hubs/duel). Authentifié par le JWT passé en paramètre access_token.
    /// Le serveur envoie "duelUpdated" (Guid du duel) et "invitationsChanged" à l'utilisateur concerné
    /// (identifié par ClaimTypes.NameIdentifier) ; aucune méthode n'est appelée par le client.
    /// Les connexions servent aussi à savoir qui est en ligne (classement).
    /// </summary>
    [Authorize]
    public class DuelHub : Hub
    {
        private readonly PresenceTracker _presence;

        public DuelHub(PresenceTracker presence)
        {
            _presence = presence;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.UserIdentifier != null)
                _presence.Connected(Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.UserIdentifier != null)
                _presence.Disconnected(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
