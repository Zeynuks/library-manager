using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class ReaderCategoryConfiguration : IEntityTypeConfiguration<ReaderCategory>
    {
        public void Configure( EntityTypeBuilder<ReaderCategory> builder )
        {
            builder.ToTable( nameof( ReaderCategory ) )
                .HasKey( rc => rc.Id );

            builder.Property( rc => rc.Name )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( rc => rc.DiscountRate )
                .HasColumnType( "decimal(18,2)" )
                .IsRequired();

            builder.HasIndex( rc => rc.Name )
                .IsUnique();
        }
    }
}