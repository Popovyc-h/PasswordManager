using PasswordManager.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Core.Services;

public class PasswordGenerator : IPasswordGenerator
{
    public string Generate(int length, bool useUppercase, bool useLowercase, bool useDigits, bool useSpecial)
    {
        var password = new StringBuilder();

        var charset = string.Empty;
        if (useUppercase) charset += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (useLowercase) charset += "abcdefghijklmnopqrstuvwxyz";
        if (useDigits) charset += "0123456789";
        if (useSpecial) charset += "!@#$%^&*";

        if (string.IsNullOrEmpty(charset))
            return "";

        for (int i = 0; i < length; i++)
        {
            var index = RandomNumberGenerator.GetInt32(charset.Length);
            password.Append(charset[index]);
        }

        return password.ToString();
    }
}