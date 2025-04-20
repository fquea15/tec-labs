using System.Collections;
using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly Hashtable _repositories = new();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var typeName = typeof(TEntity).Name;

        if (_repositories.ContainsKey(typeName))
        {
            return (IGenericRepository<TEntity>)_repositories[typeName]!;
        }

        var repositoryInstance = new GenericRepository<TEntity>(context);
        _repositories[typeName] = repositoryInstance;

        return repositoryInstance;
    }

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}