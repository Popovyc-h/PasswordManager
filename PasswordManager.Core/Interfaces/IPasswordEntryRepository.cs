using PasswordManager.Core.Entities;

namespace PasswordManager.Core.Interfaces;

public interface IPasswordEntryRepository : IRepository<PasswordEntry>
{
    Task<IEnumerable<PasswordEntry>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<PasswordEntry>> SearchAsync(int userId, string searchTerm);
}