namespace PasswordManager.Core.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int UserId { get; set; }
    public required User User { get; set; }

    public ICollection<PasswordEntry> PasswordEntries { get; set; } = new List<PasswordEntry>();
}