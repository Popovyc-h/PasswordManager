using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using PasswordManager.Data.Data;

namespace PasswordManager.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) {  }
    
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
}