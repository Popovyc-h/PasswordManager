namespace PasswordManager.Core.Entities;

public class PasswordEntry
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string EncryptedPassword { get; set; } = string.Empty;
    public string IV { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<PasswordHistory> PasswordHistories { get; set; } = new List<PasswordHistory>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}