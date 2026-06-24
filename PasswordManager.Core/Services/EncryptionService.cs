using PasswordManager.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Core.Services;

public class EncryptionService : IEncryptionService
{
    public (string EncryptedText, string IV) Encrypt(string plainText, byte[] key)
    {
        using var aes = Aes.Create();
        using var memoryStream = new MemoryStream();

        aes.Key = key;
        aes.GenerateIV();
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();
        var encryptedBytes = memoryStream.ToArray();

        return (Convert.ToBase64String(encryptedBytes), Convert.ToBase64String(aes.IV));
    }

    public string Decrypt(string encryptedText, string iv, byte[] key)
    {
        using var aes = Aes.Create();
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        using var memoryStream = new MemoryStream(encryptedBytes);

        aes.Key = key;
        aes.IV = Convert.FromBase64String(iv);
        var descryption = aes.CreateDecryptor(aes.Key, aes.IV);

        using var cryptoStream = new CryptoStream(memoryStream, descryption, CryptoStreamMode.Read);

        using var reader = new StreamReader(cryptoStream);
        return reader.ReadToEnd();
    }
}