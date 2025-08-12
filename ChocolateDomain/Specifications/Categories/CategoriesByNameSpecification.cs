using ChocolateDomain.Entities;
using ChocolateDomain.Specifications.Common;

namespace ChocolateDomain.Specifications.Categories;

public class CategoriesByNameSpecification : Specification<CategoryEntity>
{
    public CategoriesByNameSpecification(string searchTerm) : base(x => 
        x.Name.ToLower().Contains(searchTerm.ToLower()))
    {
    }
}