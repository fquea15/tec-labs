﻿namespace Server.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();
}