namespace PasswordManager.Core.Interfaces;

public interface IEncryptionService
{
    public (string EncryptedText, string IV) Encrypt(string plainText, byte[] key);
    public string Decrypt(string encryptedText, string iv, byte[] key);
}