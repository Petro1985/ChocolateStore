using ChocolateDomain.Entities;

namespace ChocolateData.Repositories.Specifications;

public class CategoriesSortedByNameSpecification : Specification<CategoryEntity>
{
    public CategoriesSortedByNameSpecification() : base(null) {
        AddOrderBy(x => x.Name);
    }
}