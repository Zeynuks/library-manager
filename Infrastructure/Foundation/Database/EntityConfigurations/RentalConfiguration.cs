using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure( EntityTypeBuilder<Rental> builder )
        {
            builder.ToTable( nameof( Rental ) )
                .HasKey( r => r.Id );

            builder.Property( r => r.BookId )
                .IsRequired();

            builder.Property( r => r.ReaderId )
                .IsRequired();

            builder.Property( r => r.IssueDate )
                .IsRequired();

            builder.Property( t => t.RentalAmount )
                .HasColumnType( "decimal(18,2)" )
                .IsRequired();

            builder.Property( r => r.ExpectedReturnDate )
                .IsRequired();

            builder.Property( r => r.ActualReturnDate );

            builder.HasIndex( r => new
                {
                    r.BookId,
                    r.ReaderId,
                    r.IssueDate
                } )
                .IsUnique();

            builder.HasOne( r => r.Book )
                .WithMany( b => b.Rentals )
                .HasForeignKey( r => r.BookId )
                .OnDelete( DeleteBehavior.Restrict );

            builder.HasOne( r => r.Reader )
                .WithMany( rdr => rdr.Rentals )
                .HasForeignKey( r => r.ReaderId )
                .OnDelete( DeleteBehavior.Restrict );
        }
    }
}