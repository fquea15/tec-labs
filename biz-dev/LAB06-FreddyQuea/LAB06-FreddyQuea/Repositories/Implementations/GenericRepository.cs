using LAB06_FreddyQuea.Data;
using LAB06_FreddyQuea.Utilities;
using LAB06_FreddyQuea.Exceptions;
using LAB06_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LAB06_FreddyQuea.Repositories.Implementations;

public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    public async Task<ServiceResponse<IEnumerable<TEntity>>> GetAllAsync()
    {
        var entities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
        return new ServiceResponse<IEnumerable<TEntity>>(true, "Items fetched successfully", entities);
    }

    public async Task<ServiceResponse<TEntity>> GetByIdAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new ItemNotFoundException($"Item with ID {id} not found.");
        }

        return new ServiceResponse<TEntity>(true, "Item found successfully", entity);
    }

    public async Task<ServiceResponse<int>> AddAsync(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        var result = await context.SaveChangesAsync();
        return new ServiceResponse<int>(true, "Item added successfully", result);
    }

    public async Task<ServiceResponse<int>> UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        var result = await context.SaveChangesAsync();
        return new ServiceResponse<int>(true, "Item updated successfully", result);
    }

    public async Task<ServiceResponse<int>> DeleteAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new ItemNotFoundException($"Item with ID {id} not found.");
        }

        context.Set<TEntity>().Remove(entity);
        var result = await context.SaveChangesAsync();
        return new ServiceResponse<int>(true, "Item deleted successfully", result);
    }
}