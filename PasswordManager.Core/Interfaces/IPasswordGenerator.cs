namespace PasswordManager.Core.Interfaces;

public interface IPasswordGenerator
{
    public string Generate(int length, bool useUppercase, bool useLowercase, bool useDigits, bool useSpecial);
}