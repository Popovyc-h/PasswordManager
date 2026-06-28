namespace PasswordManager.Core.Interfaces;

public interface IEncryptionService
{
    (string EncryptedText, string IV) Encrypt(string plainText, byte[] key);
    string Decrypt(string encryptedText, string iv, byte[] key);
}