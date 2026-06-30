namespace PasswordManager.Core.Interfaces;

public interface ISessionService
{
    byte[]? AesKey { get; set; }
    int UserId { get; set; }
}