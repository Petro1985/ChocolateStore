namespace ChocolateDomain.Interfaces;

public interface IDbRepository <TEntity>
{
    public Task<Guid> Add(TEntity entity);
    public Task Delete(TEntity entity);
    public Task Delete(Guid id);
    public Task Update(TEntity entity);
    public Task<TEntity> Get(Guid id);
    public IQueryable<TEntity> GetQuery();
}