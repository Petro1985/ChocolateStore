namespace ChocolateUI.Services;

public interface IFetchService
{
    public string BaseUrl { get; }
    Task<ICollection<ProductResponse>> GetProductByCategory(Guid categoryId);
    Task<ICollection<CategoryResponse>> GetCategories();
    Task<ProductResponse> GetProduct(Guid productId);
    Task<Guid> CreateNewCategory(CategoryResponse categoryResponse);
    Task<Guid> CreateNewProduct(ProductResponse newProduct);
    Task<CategoryResponse> GetCategory(Guid categoryId);
    Task AddPhoto(string photo, Guid productId);
    Task DeleteCategory(Guid categoryId);
    Task UpdateCategory(CategoryResponse category);
    Task UpdateProduct(ProductResponse product);
    Task DeleteProduct(Guid productId);
    Task<string> CropPhoto(Stream photo);
    Task DeletePhoto(Guid photoId);
    Task<Guid> AddCategoryPhoto(string imageData, Guid categoryId);
    Task<Guid> AddProductPhoto(string newImage, Guid productId);
    string MakeImageUrl(Guid imageId);
    string MakeThumbnailUrl(Guid imageId);
}