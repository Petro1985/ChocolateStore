using System.Linq.Expressions;
using ChocolateDomain.Entities;
using ChocolateDomain.Specifications.Common;

namespace ChocolateDomain.Specifications.Categories;

public class CategoriesSortedByNameSpecification : Specification<CategoryEntity>
{
    public CategoriesSortedByNameSpecification(Expression<Func<CategoryEntity, bool>>? criteria = null) : base(criteria) {
        AddOrderBy(x => x.Name);
    }
}