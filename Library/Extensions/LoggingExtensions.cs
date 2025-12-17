namespace Library.Extensions
{
    public static class LoggingExtensions
    {
        public static void AddCustomLogging( this IServiceCollection services )
        {
            services.AddLogging( builder =>
            {
                builder.ClearProviders();

                builder.AddSimpleConsole( options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                } );

                builder.AddFilter( "Microsoft", LogLevel.Warning );
                builder.AddFilter( "Library", LogLevel.Information );
            } );
        }
    }
}