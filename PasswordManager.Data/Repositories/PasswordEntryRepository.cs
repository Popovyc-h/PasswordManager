using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using PasswordManager.Data.Data;

namespace PasswordManager.Data.Repositories;

public class PasswordEntryRepository : Repository<PasswordEntry>, IPasswordEntryRepository
{
    public PasswordEntryRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<PasswordEntry>> GetAllByUserIdAsync(int userId)
    {
        return await _context.PasswordEntries.Where(pe => pe.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<PasswordEntry>> SearchAsync(int userId, string searchTerm)
    {
        return await _context.PasswordEntries.Where(pe => pe.UserId == userId && (pe.Title.Contains(searchTerm) || pe.Login.Contains(searchTerm))).ToListAsync();
    }
}