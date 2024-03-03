using Models;
using Models.Category;
using Models.Product;

namespace Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();
    Task<IEnumerable<ProductDto>> GetProductsByCategory(Guid categoryId);
    Task<IEnumerable<CategoryDTO>> GetAllCategories();
    Task<ProductDto> GetProduct(Guid productId);
    
    /// <summary>
    /// Получить продукт с заполненым массивом идентификаторов фотографий
    /// </summary>
    /// <param name="productId">Идентификатор товара</param>
    /// <returns></returns>
    Task<ProductDto> GetProductWithPhotoIds(Guid productId);
    
    Task<CategoryDTO> GetCategory(Guid categoryId);
    Task<Guid> AddNewProduct(ProductCreateRequest product);
    Task UpdateProduct(ProductDto product);
    Task UpdateCategory(CategoryDTO category);
    Task SetProductPhoto(Guid productId, Guid photoId);
    Task SetCategoryPhoto(Guid categoryId, Guid photoId);
    Task<Guid> AddNewCategory(CategoryDTO category);
    Task DeleteCategory(Guid categoryId);
    Task DeleteProduct(Guid productId);
}