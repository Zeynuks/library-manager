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
            
            builder.HasMany(r => r.Fines)
                .WithOne(f => f.Rental)
                .HasForeignKey(f => f.RentalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}