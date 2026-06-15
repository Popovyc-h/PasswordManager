using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User> 
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(250);

        builder.HasIndex(u => u.Username)
               .IsUnique();

        builder.Property(u => u.MasterPasswordHash)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(u => u.MasterPasswordSalt)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(u => u.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(u => u.UserSettings)
               .WithOne(us => us.User)
               .HasForeignKey<UserSettings>(us => us.UserId);

        builder.HasMany(u => u.PasswordEntries)
               .WithOne(pe => pe.User)
               .HasForeignKey(pe => pe.UserId);
    }
}