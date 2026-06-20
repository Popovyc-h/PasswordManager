namespace PasswordManager.Core.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(int id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(int id);
}