namespace PasswordManager.Core.Entities;

public class UserSettings
{
    public int Id { get; set; }
    public string Theme { get; set; } = string.Empty;
    public required User User { get; set; }
    public int UserId { get; set; }
}