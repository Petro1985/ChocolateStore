using ChocolateDomain;
using ChocolateDomain.Exceptions;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class BaseRepository<TEntity> : IDbRepository<TEntity> where TEntity : class, IEntity 
{
    protected readonly DbContext _dbContext;

    protected BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<long> Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public virtual async Task Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task Delete(long id)
    {
        var entity = await Get(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task Change(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<TEntity> Get(long id)
    {
        var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(prod => prod.Id == id);
        if (entity is null) throw new EntityNotFoundException(typeof(TEntity), id);
        return entity;
    }

    public virtual IQueryable<TEntity> GetQuery()
    {
        return _dbContext.Set<TEntity>(); 
    }    
}