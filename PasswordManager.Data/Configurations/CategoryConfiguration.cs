using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasData(
            new Category { Id = 1, Name = "Соцмережі" },
            new Category { Id = 2, Name = "Пошта" },
            new Category { Id = 3, Name = "Банкінг" },
            new Category { Id = 4, Name = "Робота" },
            new Category { Id = 5, Name = "Розваги" },
            new Category { Id = 6, Name = "Інше" });
    }
}