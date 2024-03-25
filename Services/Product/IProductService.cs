using Models;
using Models.Category;
using Models.Product;

namespace Services.Product;

public interface IProductService
{
    /// <summary>
    /// Возвращает все продукты которые есть в БД
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProductDto>> GetAllProducts();
    
    /// <summary>
    /// Возвращает товары определенной категории
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<IEnumerable<ProductDto>> GetProductsByCategory(Guid categoryId);
    
    /// <summary>
    /// Возвращает все существующие категории
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CategoryDTO>> GetAllCategories();
    
    /// <summary>
    /// Возвращает продукт по идентификатору
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<ProductDto> GetProductById(Guid productId);
    
    /// <summary>
    /// Получить продукт с заполненым массивом идентификаторов фотографий
    /// </summary>
    /// <param name="productId">Идентификатор товара</param>
    /// <returns></returns>
    Task<ProductDto> GetProductWithPhotoById(Guid productId);
    
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