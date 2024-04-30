using System.Linq.Expressions;
using ChocolateDomain.Entities;

namespace ChocolateDomain.Specifications;

public class ProductsWithCategorySpecification : Specification<ProductEntity>
{
    public ProductsWithCategorySpecification() : base(null)
    {
        AddInclude(x => x.Category);
    }
}