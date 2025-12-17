using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure( EntityTypeBuilder<Book> builder )
        {
            builder.ToTable( nameof( Book ) )
                .HasKey( b => b.Id );

            builder.Property( b => b.Title )
                .HasMaxLength( 300 )
                .IsRequired();

            builder.Property( b => b.Author )
                .HasMaxLength( 200 )
                .IsRequired();

            builder.Property( b => b.Genre )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( b => b.Deposit )
                .HasColumnType( "decimal(18,2)" )
                .IsRequired();

            builder.Property( b => b.TariffId )
                .IsRequired();

            builder.HasIndex( b => new
                {
                    b.Title,
                    b.Author
                } )
                .IsUnique();

            builder.HasOne( b => b.Tariff )
                .WithMany()
                .HasForeignKey( b => b.TariffId )
                .OnDelete( DeleteBehavior.Restrict );
        }
    }
}