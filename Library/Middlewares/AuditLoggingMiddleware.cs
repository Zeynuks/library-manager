using System.Security.Claims;

namespace Library.Middlewares
{
    public class AuditLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditLoggingMiddleware> _logger;

        public AuditLoggingMiddleware( RequestDelegate next, ILogger<AuditLoggingMiddleware> logger )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke( HttpContext context )
        {
            string user = context.User.Identity?.IsAuthenticated == true
                ? context.User.FindFirstValue( ClaimTypes.Name ) ?? "Unknown"
                : "Anonymous";

            string method = context.Request.Method;
            PathString path = context.Request.Path;

            _logger.LogInformation( "User {User} made {Method} request to {Path}", user, method, path );

            await _next( context );
        }
    }
}