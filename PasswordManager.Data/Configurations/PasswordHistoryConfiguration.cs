using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Configurations;

public class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory>
{
    public void Configure(EntityTypeBuilder<PasswordHistory> builder)
    {
        builder.ToTable("PasswordHistories");

        builder.HasKey(ph => ph.Id);

        builder.Property(ph => ph.EncryptedPassword)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(ph => ph.IV)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(ph => ph.ChangedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(ph => ph.PasswordEntry)
               .WithMany(pe => pe.PasswordHistories)
               .HasForeignKey(ph => ph.PasswordEntryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}