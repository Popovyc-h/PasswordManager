namespace PasswordManager.Core.Entities;

public class PasswordHistory
{
    public int Id { get; set; }
    public int PasswordEntryId { get; set; }
    public PasswordEntry? PasswordEntry { get; set; }
    public string EncryptedPassword { get; set; } = string.Empty;
    public string IV { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
}