using Models.Category;
using Models.Product;

namespace ChocolateUI.Services;

public interface IFetchService
{
    public string BaseUrl { get; }
    Task<ICollection<ProductDto>> GetProductByCategory(Guid categoryId);
    Task<ICollection<CategoryDto>> GetCategories();
    Task<ProductDto> GetProduct(Guid productId);
    Task<Guid> CreateNewCategory(CategoryDto categoryDto);
    Task<Guid> CreateNewProduct(ProductDto newProduct);
    Task<CategoryDto> GetCategory(Guid categoryId);
    Task AddPhoto(string photo, Guid productId);
    Task DeleteCategory(Guid categoryId);
    Task UpdateCategory(CategoryDto category);
    Task UpdateProduct(ProductDto product);
    Task DeleteProduct(Guid productId);
    Task<string> CropPhoto(Stream photo);
    Task DeletePhoto(Guid photoId);
    Task<Guid> AddCategoryPhoto(string imageData, Guid categoryId);
    Task<Guid> AddProductPhoto(string newImage, Guid productId);
    string MakeImageUrl(Guid imageId);
    string MakeThumbnailUrl(Guid imageId);
}