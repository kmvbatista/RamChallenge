using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class GenericRepository<TEntity>
    : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly MainContext _dbContext;

    public GenericRepository(MainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<TEntity>> GetAll()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await _dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
