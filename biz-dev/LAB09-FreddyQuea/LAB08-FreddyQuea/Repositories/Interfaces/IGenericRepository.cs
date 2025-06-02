namespace LAB08_FreddyQuea.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<int> AddAsync(TEntity entity);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
    
    IQueryable<TEntity> AsQueryable();
}