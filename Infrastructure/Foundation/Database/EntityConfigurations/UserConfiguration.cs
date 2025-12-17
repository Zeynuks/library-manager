using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( nameof( User ) )
                .HasKey( u => u.Id );

            builder.Property( u => u.Login )
                .HasMaxLength( 100 )
                .IsRequired();

            builder.Property( u => u.PasswordHash )
                .HasMaxLength( 300 )
                .IsRequired();

            builder.Property( u => u.Role )
                .HasConversion<string>()
                .IsRequired();

            builder.Property( u => u.IsBlocked )
                .IsRequired();

            builder.HasIndex( u => u.Login )
                .IsUnique();
        }
    }
}