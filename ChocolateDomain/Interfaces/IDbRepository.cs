using ChocolateDomain.Specifications;

namespace ChocolateDomain.Interfaces;

public interface IDbRepository <TEntity> where TEntity : IEntity
{
    public Task<Guid> Add(TEntity entity);
    public Task Delete(TEntity entity);
    public Task Delete(Guid id);
    public Task Update(TEntity entity);
    public Task<TEntity> Get(Guid id);

    /// <summary>
    /// Метод расчета количества записей соответвующих спецификации
    /// </summary>
    /// <param name="specification"></param>
    /// <returns>Количество записей удовлетворяющих спецификации</returns>
    public Task<int> CountBySpecification(ISpecification<TEntity> specification);
    
    /// <summary>
    /// Получить записи удовлетворяющие спецификации 
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<TEntity>> GetBySpecification(ISpecification<TEntity> specification);
}