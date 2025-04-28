using LAB06_FreddyQuea.Utilities;

namespace LAB06_FreddyQuea.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<ServiceResponse<IEnumerable<TEntity>>> GetAllAsync();
        Task<ServiceResponse<TEntity>> GetByIdAsync(int id);
        Task<ServiceResponse<int>> AddAsync(TEntity entity);
        Task<ServiceResponse<int>> UpdateAsync(TEntity entity);
        Task<ServiceResponse<int>> DeleteAsync(int id);
    }
}
