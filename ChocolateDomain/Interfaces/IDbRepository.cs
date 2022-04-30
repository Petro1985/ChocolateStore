namespace ChocolateDomain.Interfaces;

public interface IDbRepository <TEntity>
{
    public Task<long> Add(TEntity entity);
    public Task Delete(TEntity entity);
    public Task Delete(long id);
    public Task Change(TEntity entity);
    public Task<TEntity> Get(long id);
    public IQueryable<TEntity> GetQuery();
}