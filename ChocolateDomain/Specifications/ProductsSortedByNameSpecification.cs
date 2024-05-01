using ChocolateDomain.Entities;

namespace ChocolateDomain.Specifications;

public class ProductsSortedByNameSpecification : Specification<ProductEntity>
{
    public ProductsSortedByNameSpecification() : base(null) {
        AddOrderBy(x => x.Name);
        AddInclude(x => x.Category);
    }
}