using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Entities;

namespace PasswordManager.Data.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PasswordEntry> PasswordEntries => Set<PasswordEntry>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<PasswordHistory> PasswordHistories => Set<PasswordHistory>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=passwordmanager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}