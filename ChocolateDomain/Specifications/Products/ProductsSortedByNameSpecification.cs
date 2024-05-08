using ChocolateDomain.Entities;
using ChocolateDomain.Specifications.Common;

namespace ChocolateDomain.Specifications.Products;

public class ProductsSortedByNameSpecification : Specification<ProductEntity>
{
    public ProductsSortedByNameSpecification() : base(null) {
        AddOrderBy(x => x.Name);
        AddInclude(x => x.Category);
    }
}