using System.Linq.Expressions;
using System.Runtime;
using ChocolateDomain;
using ChocolateDomain.Exceptions;
using ChocolateDomain.Interfaces;
using ChocolateDomain.Specifications;
using ChocolateDomain.Specifications.Common;
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

    public async Task<int> CountBySpecification(ISpecification<TEntity> specification)
    {
        var countQuery = ApplySpecification(specification);
        return await countQuery.CountAsync();
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        => ApplySpecification(specification, DbContext.Set<TEntity>());
    

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, IQueryable<TEntity> query)
    {
        if (specification.Criteria is not null) {
            query = query.Where(specification.Criteria);
        }

        query = specification.IncludeExpressions
            .Aggregate(query, (current, expression) => current.Include(expression));

        if (specification.OrderByExpression is not null) {
            query = query.OrderBy(specification.OrderByExpression);
        }

        if (specification.OrderByDescendingExpression is not null) {
            query = query.OrderByDescending(specification.OrderByDescendingExpression);
        }

        if (specification.PagingParameters is not null) {
            query = query
                .Skip((specification.PagingParameters.Value.PageNumber - 1) * specification.PagingParameters.Value.PageSize)
                .Take(specification.PagingParameters.Value.PageSize);
        }

        return query;
    }

    public async Task<IReadOnlyCollection<TEntity>> GetBySpecification(ISpecification<TEntity> specification)
    {
        var query = ApplySpecification(specification);
        return await query.ToListAsync();
    }
}