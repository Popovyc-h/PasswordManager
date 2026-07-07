namespace PasswordManager.Core.Interfaces;

public interface IDerivedKeyService
{
    public byte[] DeriveKey(string masterPassword, string aesKeySalt);
    string GenerateSalt();
}