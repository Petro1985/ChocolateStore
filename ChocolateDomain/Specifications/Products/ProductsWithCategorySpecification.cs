using ChocolateDomain.Entities;
using ChocolateDomain.Specifications.Common;

namespace ChocolateDomain.Specifications.Products;

public class ProductsWithCategorySpecification : Specification<ProductEntity>
{
    public ProductsWithCategorySpecification() : base(null)
    {
        AddInclude(x => x.Category);
    }
}