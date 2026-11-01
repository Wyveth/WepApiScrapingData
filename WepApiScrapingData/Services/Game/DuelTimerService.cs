namespace WepApiScrapingData.Services.Game
{
    /// <summary>
    /// Horloge des duels : chaque seconde, clôt les questions temps réel dont le chrono est écoulé
    /// et fait expirer les recherches et défis restés sans réponse.
    /// </summary>
    public class DuelTimerService : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(1);
        /// <summary>Après une erreur (base indisponible…), on espace les tentatives.</summary>
        private static readonly TimeSpan ErrorDelay = TimeSpan.FromSeconds(15);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DuelTimerService> _logger;

        public DuelTimerService(IServiceScopeFactory scopeFactory, ILogger<DuelTimerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = Interval;
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var duels = scope.ServiceProvider.GetRequiredService<DuelService>();
                    await duels.TickAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    // Une erreur ne doit jamais arrêter l'API
                    _logger.LogWarning(ex, "Horloge des duels : erreur, nouvelle tentative dans {Delay} s.", ErrorDelay.TotalSeconds);
                    delay = ErrorDelay;
                }

                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
