using ChocolateData.Repositories;
using ChocolateDomain.Specifications.Categories;
using ChocolateDomain.Specifications.Products;
using Services.Models;

namespace Services.Search;

public class SearchService : ISearchService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public SearchService(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<SearchResult> GlobalSearch(string searchString)
    {
        if (searchString.Length < 3)
        {
            return new SearchResult();
        }
        
        var categoriesSpecification = new CategoriesByNameSpecification(searchString);
        var categoriesResult = await _categoryRepository.GetBySpecification(categoriesSpecification);
        
        var productsSpecification = new ProductsByNameSpecification(searchString);
        var productsResult = await _productRepository.GetBySpecification(productsSpecification);

        var response = new SearchResult
        {
            Products = productsResult.Select(x => new ProductsSearchResult
            {
                Id = x.Id,
                Name = x.Name,
                MainPhotoId = x.MainPhotoId,
                CategoryId = x.CategoryId,
            }).ToList(),
            
            Categories = categoriesResult.Select(x => new CategoriesSearchResult
            {
                Id = x.Id,
                Name = x.Name,
                MainPhotoId = x.MainPhotoId,
            }).ToList(),
        };
        
        return response;
    }
}