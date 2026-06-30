using PasswordManager.Core.Interfaces;

namespace PasswordManager.Core.Services;

public class SessionService : ISessionService
{
    public byte[]? AesKey { get; set; }
    public int UserId { get; set; }
}