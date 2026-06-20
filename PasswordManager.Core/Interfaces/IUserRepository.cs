using PasswordManager.Core.Entities;

namespace PasswordManager.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByUsernameAsync(string username);
    public Task<bool> ExistsByUsernameAsync(string username);
}