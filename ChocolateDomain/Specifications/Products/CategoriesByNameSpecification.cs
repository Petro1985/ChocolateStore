using ChocolateDomain.Entities;
using ChocolateDomain.Specifications.Common;

namespace ChocolateDomain.Specifications.Products;

public class ProductsByNameSpecification : Specification<ProductEntity>
{
    public ProductsByNameSpecification(string searchTerm) : base(x => 
        x.Name.ToLower().Contains(searchTerm.ToLower()))
    {
    }
}