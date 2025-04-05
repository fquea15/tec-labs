namespace Lab03_FreddyQuea.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<int> Add(T entity);
    Task<int> Update(T entity);
    Task<int> Delete(int id);
}