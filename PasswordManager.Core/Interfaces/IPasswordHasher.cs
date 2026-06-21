namespace PasswordManager.Core.Interfaces;

public interface IPasswordHasher
{
    public (string Hash, string Salt) HashPassword(string password);
    public bool VerifyPassword(string password, string masterPasswordHash, string masterPasswordSalt);
}