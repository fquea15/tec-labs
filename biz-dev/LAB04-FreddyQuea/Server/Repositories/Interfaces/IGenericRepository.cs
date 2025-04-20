namespace Server.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    void AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(int id);
}