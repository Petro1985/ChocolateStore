using ChocolateDomain.Entities;
using Models.Product;
using Services.Models;

namespace Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();
    
    Task<IEnumerable<ProductDto>> GetProductsByCategory(Guid categoryId);
    
    Task<List<CategoryDto>> GetAllCategories();
    
    Task<ProductDto> GetProduct(Guid productId);
    
    /// <summary>
    /// Получить продукт с заполненым массивом идентификаторов фотографий
    /// </summary>
    /// <param name="productId">Идентификатор товара</param>
    /// <returns></returns>
    Task<ProductDto> GetProductWithPhotoIds(Guid productId);
    
    Task<CategoryDto> GetCategory(Guid categoryId);
    
    /// <summary>
    /// Добавление нового товара
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Guid> AddNewProduct(ProductDto product);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task UpdateProduct(ProductDto product);
    Task UpdateCategory(CategoryDto category);
    Task SetProductPhoto(Guid productId, Guid photoId);
    Task SetCategoryPhoto(Guid categoryId, Guid photoId);
    Task<Guid> AddNewCategory(CategoryDto category);
    Task DeleteCategory(Guid categoryId);
    Task DeleteProduct(Guid productId);

    /// <summary>
    ///  Получение пагинированного списка продуктов отсортированного по имени
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    Task<PagedItems<ProductEntity>> GetPagedCategoryProductsSortedByName(Guid? categoryId, int pageSize, int pageNumber);
}