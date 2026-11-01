using System.Collections.Concurrent;

namespace WepApiScrapingData.Hubs
{
    /// <summary>
    /// Joueurs connectés : un compte est « en ligne » tant qu'il a au moins une connexion ouverte au hub des duels
    /// (le site ouvert et connecté, sur un ou plusieurs onglets ou appareils). Tenu en mémoire : une seule instance d'API.
    /// </summary>
    public class PresenceTracker
    {
        private readonly ConcurrentDictionary<string, int> _connections = new();

        public void Connected(string userId) =>
            _connections.AddOrUpdate(userId, 1, (_, count) => count + 1);

        public void Disconnected(string userId)
        {
            while (_connections.TryGetValue(userId, out var count))
            {
                if (count <= 1)
                {
                    if (_connections.TryRemove(new KeyValuePair<string, int>(userId, count)))
                        return;
                }
                else if (_connections.TryUpdate(userId, count - 1, count))
                    return;
            }
        }

        public bool IsOnline(string userId) => _connections.ContainsKey(userId);
    }
}
