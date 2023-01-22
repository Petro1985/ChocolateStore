using System.Net.Http.Json;
using System.Text.Json;
using Models.Category;
using Models.Photo;
using Models.Product;

namespace ChocolateUI.Services;

class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ICollection<ProductDTO>> GetProductByCategory(Guid categoryId)
    {
        try
        {
            Console.WriteLine(_httpClient.BaseAddress);
            var response = await _httpClient.GetAsync($"Products/{categoryId}");
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
            var response = await _httpClient.GetAsync($"Product/{productId}");
    
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
            var response = await _httpClient.PostAsJsonAsync("Category", categoryDto);
            
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
            var response = await _httpClient.GetAsync($"Category/{categoryId}");
    
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<CategoryDTO>();
            return responseBody ?? new CategoryDTO();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении [HTTPGet]/Category");
            throw;
        }
    }

    public async Task AddPhoto(string photo, Guid productId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"Products/{productId}/Photos", new AddPhotoRequest() {Photo = photo, ProductId = productId});
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении на [Post]Products/{productId}/Photos", productId);
            throw;
        }
    }
}