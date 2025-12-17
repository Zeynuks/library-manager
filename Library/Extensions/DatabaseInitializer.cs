using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Extensions
{
    public static class DatabaseInitializer
    {
        public static void AddLibraryDatabase(
            this IServiceCollection services,
            IConfiguration configuration )
        {
            string? resolved = configuration.GetConnectionString( "LibraryDb" );

            if ( resolved is null )
            {
                throw new InvalidOperationException( "Connection string for LibraryDb is not configured." );
            }

            services.AddDbContext<LibraryDbContext>( options =>
            {
                options.UseLazyLoadingProxies().UseMySql(
                    resolved,
                    new MySqlServerVersion( new Version( 10, 11, 11 ) ),
                    mySqlOptions =>
                    {
                        mySqlOptions.MigrationsAssembly( "Infrastructure.Migrations" );
                        mySqlOptions.EnableRetryOnFailure( 5, TimeSpan.FromSeconds( 5 ), null );
                    } );

                options.EnableSensitiveDataLogging();
                options.LogTo( Console.WriteLine, LogLevel.Information );
            } );
        }

        public static void InitLibraryDatabase( this WebApplication app )
        {
            using IServiceScope scope = app.Services.CreateScope();
            LibraryDbContext db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

            try
            {
                db.Database.Migrate();
            }
            catch ( Exception ex )
            {
                throw new InvalidOperationException( $"Database migration failed: {ex.Message}", ex );
            }
        }
    }
}