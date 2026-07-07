using PasswordManager.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Core.Services;

public class DerivedKeyService : IDerivedKeyService
{
    public byte[] DeriveKey(string masterPassword, string aesKeySalt)
    {
        int iterations = 210000;

        var salt = Convert.FromBase64String(aesKeySalt);

        var hash = Rfc2898DeriveBytes.Pbkdf2
            (
            Encoding.UTF8.GetBytes(masterPassword),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            32);

        return hash;
    }

    public string GenerateSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
    }
}