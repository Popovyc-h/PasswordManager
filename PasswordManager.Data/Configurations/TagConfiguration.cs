using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
       .IsRequired()
       .HasMaxLength(50);

        builder.HasOne(t => t.User)
       .WithMany()
       .HasForeignKey(t => t.UserId);
    }
}