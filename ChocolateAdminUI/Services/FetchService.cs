using System.Net.Http.Headers;
using System.Net.Http.Json;
using ChocolateAdminUI.Pages.DisplayProducts;
using Models.Category;
using Models.Photo;
using Models.Product;

namespace ChocolateAdminUI.Services;

class FetchService : IFetchService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FetchService> _logger;
    private readonly string _serverUrl;
    
    public FetchService(IHttpClientFactory httpFactory, ILogger<FetchService> logger)
    {
        _httpClient = httpFactory.CreateClient("API");
        _logger = logger;
        _serverUrl = _httpClient.BaseAddress?.ToString() ?? "";
    }

    public async Task<ICollection<ProductDTO>> GetProductByCategory(Guid categoryId)
    {
        try
        {
            Console.WriteLine(_httpClient.BaseAddress);
            var response = await _httpClient.GetAsync($"Categories?categoryId={categoryId}");
            var responseBody = await response.Content.ReadFromJsonAsync<ICollection<ProductDTO>>();
            return responseBody ?? new List<ProductDTO>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Не удалось получить список товаров с сервера");
            throw;
        }
    }

    public async Task<ICollection<CategoryDTO>> GetCategories()
    {
        try
        {
            var response = await _httpClient.GetAsync("Categories");
    
            var responseBody = await response.Content.ReadFromJsonAsync<ICollection<CategoryDTO>>();
            return responseBody ?? new List<CategoryDTO>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ProductDTO> GetProduct(Guid productId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Products/{productId}");
    
            var responseBody = await response.Content.ReadFromJsonAsync<ProductDTO>();
            return responseBody ?? new ProductDTO();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Guid> CreateNewCategory(CategoryDTO categoryDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Categories", categoryDto);
            
            var responseBody = await response.Content.ReadFromJsonAsync<Guid>();
            return responseBody;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Guid> CreateNewProduct(ProductDTO newProduct)
    {
        try
        {
            var productRequest = new ProductCreateRequest
            {
                Description = newProduct.Description,
                Name = newProduct.Name,
                CategoryId = newProduct.CategoryId,
                PriceRub = newProduct.PriceRub,
                TimeToMakeInHours = newProduct.TimeToMakeInHours,
            };
            var response = await _httpClient.PostAsJsonAsync("Products", productRequest);

            var responseBody = await response.Content.ReadFromJsonAsync<Guid>();
            return responseBody;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении к [Post]Products");
            throw;
        }
    }

    public async Task<CategoryDTO> GetCategory(Guid categoryId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Categories/{categoryId}");
    
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<CategoryDTO>();
            return responseBody ?? new CategoryDTO();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении [HTTPGet]/Categories");
            throw;
        }
    }

    public async Task AddPhoto(string photo, Guid productId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"Products/{productId}/Photos", new AddPhotoRequest() {PhotoBase64 = photo, ProductId = productId});
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]Products/{productId}/Photos", productId);
            throw;
        }
    }

    public async Task<string> CropPhoto(Stream basePhoto)
    {
        try
        {
            using var fileStreamContent = new StreamContent(basePhoto);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            var response = await _httpClient.PostAsync("Photos/Crop", fileStreamContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]Photos/Crop");
            throw;
        }
    }

    public async Task DeletePhoto(Guid photoId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Photos/{photoId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Delete]Photos/{PhotoId}", photoId);
            throw;
        }
    }

    public async Task<Guid> AddCategoryPhoto(string imageData, Guid categoryId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"Categories/Photos", 
                new AddMainPhotoRequest {PhotoBase64 = imageData, EntityId = categoryId});
            return await response.Content.ReadFromJsonAsync<Guid>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]Categories/SetPhoto");
            throw;
        }
    }

    public async Task<Guid> AddProductPhoto(string imageData, Guid productId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Products/Photos", 
                new AddMainPhotoRequest {PhotoBase64 = imageData, EntityId = productId});
            return await response.Content.ReadFromJsonAsync<Guid>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]Products/SetPhoto");
            throw;
        }
    }

    public string GetImageUrl(Guid imageId)
    {
        return imageId == default 
            ? "/images/NoImage.png" 
            : $"{_serverUrl}image/{imageId}";
    }

    public async Task MakeBackUp()
    {
        try
        {
            var response = await _httpClient.PostAsync("DateBase/MakeBackup", null);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]DateBase/MakeBackup");
            throw;
        }
    }

    public async Task DownloadBackUp(Guid backupId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"DataBase/{backupId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Get]DateBase/{BackupId}", backupId);
            throw;
        }
    }

    public async Task RestoreBackup(byte[] backup)
    {
        try
        {
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(backup), "Backup");
            var response = await _httpClient.PostAsync("DateBase/RestoreBackup", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]DateBase/RestoreBackup");
            throw;
        }
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Categories/{categoryId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Delete]Categories/{CategoryId}", categoryId);
            throw;
        }
    }
    
    public async Task UpdateCategory(CategoryDTO category)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("Categories", category);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Put]Categories");
            throw;
        }
    }
    
    public async Task UpdateProduct(ProductDTO product)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("Product", product);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Put]Product");
            throw;
        }
    }

    public async Task DeleteProduct(Guid productId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Products/{productId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Delete]Products/{ProductId}", productId);
            throw;
        }
    }
}