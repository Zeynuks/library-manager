using Microsoft.OpenApi.Models;

namespace Library.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfiguration( this IServiceCollection services )
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen( c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc( "library", new OpenApiInfo
                {
                    Title = "Library API",
                    Version = "v1",
                    Description = "API для управления библиотекой"
                } );

                c.DocInclusionPredicate( ( doc, api ) =>
                    string.Equals( api.GroupName, doc, StringComparison.OrdinalIgnoreCase )
                );
            } );
        }

        public static void UseSwaggerConfiguration( this IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( !env.IsDevelopment() )
            {
                return;
            }

            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint( "/swagger/library/swagger.json", "Library API" );
            } );
        }
    }
}