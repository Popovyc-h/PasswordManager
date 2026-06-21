using PasswordManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string Hash, string Salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        int iterations = 210000;

        var hash = Rfc2898DeriveBytes.Pbkdf2
            (
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            32);

        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public bool VerifyPassword(string password, string masterPasswordHash, string masterPasswordSalt)
    {
        var salt = Convert.FromBase64String(masterPasswordSalt);
        int iterations = 210000;

        var computedHash = Rfc2898DeriveBytes.Pbkdf2
            (
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            32);

        var masterHashBytes = Convert.FromBase64String(masterPasswordHash);

        return CryptographicOperations.FixedTimeEquals(computedHash, masterHashBytes);
    }
}