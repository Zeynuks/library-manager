using Library.Extensions;
using Library.Middlewares;

namespace Library
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

            builder.Services.AddSwaggerConfiguration();
            builder.Services.AddCustomLogging();
            builder.Services.AddCustomAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddBindings();
            builder.Services.AddLibraryDatabase( builder.Configuration );

            WebApplication app = builder.Build();

            app.UseMiddleware<AuditLoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwaggerConfiguration( app.Environment );
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.InitLibraryDatabase();

            app.Run();
        }
    }
}