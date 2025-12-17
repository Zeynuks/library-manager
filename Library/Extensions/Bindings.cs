using Application.Services.AuthService;
using Application.Services.BookService;
using Application.Services.FineService;
using Application.Services.ReaderCategoryService;
using Application.Services.ReaderService;
using Application.Services.RentalService;
using Application.Services.TariffService;
using Application.Services.UserService;
using Domain.Foundation;
using Domain.Repositories;
using Infrastructure.Foundation;
using Infrastructure.Repositories;

namespace Library.Extensions
{
    public static class Bindings
    {
        public static void AddBindings( this IServiceCollection services )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Decorate<IUnitOfWork, UnitOfWorkExceptionDecorator>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IFineService, FineService>();
            services.AddScoped<IReaderService, ReaderService>();
            services.AddScoped<IReaderCategoryService, ReaderCategoryService>();
            services.AddScoped<ITariffService, TariffService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IFineRepository, FineRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IReaderCategoryRepository, ReaderCategoryRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();
        }
    }
}