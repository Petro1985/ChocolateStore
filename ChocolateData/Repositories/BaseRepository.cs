using System.Linq.Expressions;
using ChocolateDomain;
using ChocolateDomain.Exceptions;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class BaseRepository<TEntity> : IDbRepository<TEntity> where TEntity : class, IEntity 
{
    protected readonly ApplicationDbContext DbContext;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<Guid> Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
        await DbContext.SaveChangesAsync();
        return entity.Id;
    }

    public virtual async Task Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = await Get(id);
        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<TEntity> Get(Guid id)
    {
        var entity = await DbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(prod => prod.Id == id);

        if (entity is null) throw new EntityNotFoundException(typeof(TEntity), id);
        return entity;
    }

    public virtual IQueryable<TEntity> GetQuery()
    {
        return DbContext.Set<TEntity>(); 
    }    
}