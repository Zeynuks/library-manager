using Domain.Entities;
using Infrastructure.Foundation.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<ReaderCategory> ReaderCategories { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        public LibraryDbContext( DbContextOptions<LibraryDbContext> options )
            : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.ApplyConfiguration( new UserConfiguration() );
            modelBuilder.ApplyConfiguration( new BookConfiguration() );
            modelBuilder.ApplyConfiguration( new FineConfiguration() );
            modelBuilder.ApplyConfiguration( new ReaderConfiguration() );
            modelBuilder.ApplyConfiguration( new ReaderCategoryConfiguration() );
            modelBuilder.ApplyConfiguration( new RentalConfiguration() );
            modelBuilder.ApplyConfiguration( new TariffConfiguration() );
        }
    }
}