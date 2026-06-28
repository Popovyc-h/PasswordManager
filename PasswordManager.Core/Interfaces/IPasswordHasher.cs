namespace PasswordManager.Core.Interfaces;

public interface IPasswordHasher
{
    (string Hash, string Salt) HashPassword(string password);
    bool VerifyPassword(string password, string masterPasswordHash, string masterPasswordSalt);
}