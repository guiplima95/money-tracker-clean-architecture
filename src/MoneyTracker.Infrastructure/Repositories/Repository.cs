﻿using Microsoft.EntityFrameworkCore;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Infrastructure.Repositories;

internal abstract class Repository<T>
    where T : Entity
{
    public readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        _dbContext.Add(entity);
    }
}
