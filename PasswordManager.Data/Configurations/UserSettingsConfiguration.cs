using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Configurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.ToTable("UserSettings");

        builder.HasKey(us => us.Id);

        builder.Property(us => us.Theme)
               .HasMaxLength(25)
               .IsRequired();

        builder.HasIndex(us => us.UserId)
               .IsUnique();
    }
}