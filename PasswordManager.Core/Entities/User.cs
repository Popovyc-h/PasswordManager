namespace PasswordManager.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string MasterPasswordHash { get; set; } = string.Empty;
    public string MasterPasswordSalt { get; set; } = string.Empty;
    public string AesKeySalt { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserSettings? UserSettings { get; set; }
    public ICollection<PasswordEntry> PasswordEntries { get; set; } = new List<PasswordEntry>();
}