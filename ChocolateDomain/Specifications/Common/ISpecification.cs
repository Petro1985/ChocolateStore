using System.Linq.Expressions;
using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Specifications.Common;

public interface ISpecification<TEntity> where TEntity : IEntity
{
    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    Expression<Func<TEntity, bool>>? Criteria { get; }
    Expression<Func<TEntity, object>>? OrderByExpression { get; }
    Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; }
    PagingParameters? PagingParameters { get; set; }
    Specification<TEntity> And(Specification<TEntity> andSpecification);
}