namespace PasswordManager.Core.Interfaces;

public interface IPasswordGenerator
{
    string Generate(int length, bool useUppercase, bool useLowercase, bool useDigits, bool useSpecial);
}