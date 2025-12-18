namespace Library.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddCustomAuthentication( this IServiceCollection services )
        {
            services.AddAuthentication( "LibraryCookie" )
                .AddCookie( "LibraryCookie", options =>
                {
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                    options.AccessDeniedPath = "/account/access-denied";

                    options.Events.OnRedirectToLogin = context =>
                    {
                        if ( context.Request.Path.StartsWithSegments( "/api" ) )
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }

                        context.Response.Redirect( context.RedirectUri );
                        return Task.CompletedTask;
                    };

                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        if ( context.Request.Path.StartsWithSegments( "/api" ) )
                        {
                            context.Response.StatusCode = 403;
                            return Task.CompletedTask;
                        }

                        context.Response.Redirect( context.RedirectUri );
                        return Task.CompletedTask;
                    };

                    options.ExpireTimeSpan = TimeSpan.FromDays( 14 );
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                } );

            services.AddAuthorizationBuilder()
                .AddPolicy( "NotBlocked", policy =>
                    policy.RequireClaim( "IsBlocked", "False" ) );
        }
    }
}