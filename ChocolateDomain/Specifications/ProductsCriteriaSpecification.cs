
using System.Linq.Expressions;
using ChocolateDomain.Entities;

namespace ChocolateDomain.Specifications;

public class ProductsCriteriaSpecification : Specification<ProductEntity>
{
    public ProductsCriteriaSpecification(Expression<Func<ProductEntity, bool>> criteria) : base(criteria)
    {
    }
}