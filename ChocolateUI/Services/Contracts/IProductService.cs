using Models;

namespace ChocolateUI.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductByCategory(Guid categoryId);
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<ProductDTO> GetProduct(Guid productId);
}