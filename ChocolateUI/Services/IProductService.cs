using Models.Category;
using Models.Product;

namespace ChocolateUI.Services;

public interface IProductService
{
    Task<ICollection<ProductDTO>> GetProductByCategory(Guid categoryId);
    Task<ICollection<CategoryDTO>> GetCategories();
    Task<ProductDTO> GetProduct(Guid productId);
    Task<Guid> CreateNewCategory(CategoryDTO categoryDto);
    Task<Guid> CreateNewProduct(ProductDTO newProduct);
    Task<CategoryDTO> GetCategory(Guid categoryId);
    Task AddPhoto(string photo, Guid productId);
}