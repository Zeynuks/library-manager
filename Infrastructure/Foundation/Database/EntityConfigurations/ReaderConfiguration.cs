using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure( EntityTypeBuilder<Reader> builder )
        {
            builder.ToTable( nameof( Reader ) )
                .HasKey( r => r.Id );

            builder.Property( r => r.FirstName )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( r => r.MiddleName )
                .HasMaxLength( 100 );

            builder.Property( r => r.LastName )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( r => r.Address )
                .HasMaxLength( 300 )
                .IsRequired();

            builder.Property( r => r.PhoneNumber )
                .HasMaxLength( 20 )
                .IsRequired();

            builder.Property( r => r.CategoryId )
                .IsRequired();

            builder.HasIndex( r => r.PhoneNumber )
                .IsUnique();

            builder.HasOne( r => r.Category )
                .WithMany( c => c.Readers )
                .HasForeignKey( r => r.CategoryId )
                .OnDelete( DeleteBehavior.Restrict );
        }
    }
}