namespace WepApiScrapingData.Middlewares
{
    public class LogRequestMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        private readonly ILogger<LogRequestMiddleware> _logger;

        #endregion

        #region Constructor
        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task Invoke(HttpContext context)
        {
            _logger.LogDebug(context.Request.Path.Value);
            await _next(context);
        }
        #endregion
    }
}
