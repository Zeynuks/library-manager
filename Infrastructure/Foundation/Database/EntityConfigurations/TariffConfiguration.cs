using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure( EntityTypeBuilder<Tariff> builder )
        {
            builder.ToTable( nameof( Tariff ) )
                .HasKey( t => t.Id );

            builder.Property( t => t.Name )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( t => t.DailyRate )
                .HasColumnType( "decimal(18,2)" )
                .IsRequired();

            builder.HasIndex( t => t.Name )
                .IsUnique();
            
            builder.HasMany(t => t.Books)
                .WithOne(b => b.Tariff)
                .HasForeignKey(b => b.TariffId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}