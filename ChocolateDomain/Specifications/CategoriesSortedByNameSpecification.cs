using ChocolateDomain.Entities;

namespace ChocolateDomain.Specifications;

public class CategoriesSortedByNameSpecification : Specification<CategoryEntity>
{
    public CategoriesSortedByNameSpecification() : base(null) {
        AddOrderBy(x => x.Name);
    }
}