using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Configurations;

public class PasswordEntryConfiguration : IEntityTypeConfiguration<PasswordEntry>
{
    public void Configure(EntityTypeBuilder<PasswordEntry> builder)
    {
        builder.ToTable("PasswordEntries");

        builder.HasKey(pe => pe.Id);

        builder.Property(pe => pe.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(pe => pe.Login)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(pe => pe.EncryptedPassword)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(pe => pe.IV)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(pe => pe.Url)
               .HasMaxLength(500);

        builder.HasOne(pe => pe.Category)
               .WithMany()
               .HasForeignKey(pe => pe.CategoryId);


        builder.Property(pe => pe.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(pe => pe.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(pe => pe.Tags)
               .WithMany(t => t.PasswordEntries)
               .UsingEntity(j => j.ToTable("EntryTags"));
    }
}