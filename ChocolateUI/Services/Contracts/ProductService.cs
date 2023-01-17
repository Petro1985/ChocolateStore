using Models;
using Newtonsoft.Json;

namespace ChocolateUI.Services.Contracts;

class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductByCategory(Guid categoryId)
    {
        try
        {
            Console.WriteLine(_httpClient.BaseAddress);
            var response = await _httpClient.GetAsync($"Products/{categoryId}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(responseBody) ?? new List<ProductDTO>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Не удалось получить список товаров с сервера");
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        try
        {
            var response = await _httpClient.GetAsync("Categories");
    
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(responseBody) ?? new List<CategoryDTO>();
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
    
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<ProductDTO>(responseBody);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}