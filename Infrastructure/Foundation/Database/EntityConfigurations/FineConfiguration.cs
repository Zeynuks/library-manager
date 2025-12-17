using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure( EntityTypeBuilder<Fine> builder )
        {
            builder.ToTable( nameof( Fine ) )
                .HasKey( f => f.Id );

            builder.Property( f => f.RentalId )
                .IsRequired();

            builder.Property( f => f.Description )
                .HasMaxLength( 500 )
                .IsRequired();

            builder.Property( f => f.Amount )
                .HasColumnType( "decimal(18,2)" )
                .IsRequired();

            builder.HasOne( f => f.Rental )
                .WithMany( r => r.Fines )
                .HasForeignKey( f => f.RentalId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}