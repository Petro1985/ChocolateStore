using System.Linq.Expressions;
using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Specifications;

public class Specification<TEntity>(Expression<Func<TEntity, bool>>? criteria) : ISpecification<TEntity> where TEntity : IEntity
{
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

    public Expression<Func<TEntity, bool>>? Criteria { get; } = criteria;

    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    public PagingParameters? PagingParameters { get; set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) 
    {
        IncludeExpressions.Add(includeExpression);
    }

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;


    public Specification<TEntity> And(Specification<TEntity> andSpecification) {
        Specification<TEntity> newSpecification;
        if (Criteria is not null) {
            if (andSpecification.Criteria is not null) {
                var body = Expression.AndAlso(Criteria.Body, andSpecification.Criteria.Body);
                var lambda = Expression.Lambda<Func<TEntity,bool>>(body, Criteria.Parameters[0]);
                newSpecification = new Specification<TEntity>(lambda);
            }
            else {
                newSpecification = new Specification<TEntity>(Criteria);
            }
        }
        else {
            newSpecification = andSpecification.Criteria is not null ? 
                new Specification<TEntity>(andSpecification.Criteria) : 
                new Specification<TEntity>(null);
        }

        foreach (var expression in IncludeExpressions) {
            newSpecification.IncludeExpressions.Add(expression);
        }
        
        foreach (var expression in andSpecification.IncludeExpressions) {
            newSpecification.IncludeExpressions.Add(expression);
        }

        newSpecification.OrderByExpression = OrderByExpression ?? andSpecification.OrderByExpression;
        newSpecification.OrderByDescendingExpression = OrderByDescendingExpression ?? andSpecification.OrderByDescendingExpression;
        
        newSpecification.PagingParameters = PagingParameters ?? andSpecification.PagingParameters;

        return newSpecification;
    }
}

public struct PagingParameters(int pageSize, int pageNumber) {
    public readonly int PageSize = pageSize;
    public readonly int PageNumber = pageNumber;
}