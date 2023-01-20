using System.Net.Http.Json;
using Models;
using Models.Category;
using Models.Product;
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

    public async Task<ICollection<ProductDTO>> GetProductByCategory(Guid categoryId)
    {
        try
        {
            Console.WriteLine(_httpClient.BaseAddress);
            var response = await _httpClient.GetAsync($"Products/{categoryId}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(responseBody)?.ToList() ?? new List<ProductDTO>();
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

    public async Task<Guid> CreateNewCategory(CategoryDTO categoryDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Category", categoryDto);
            
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Guid>(responseBody);
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
            
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Guid>(responseBody);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CategoryDTO> GetCategory(Guid categoryId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Category/{categoryId}");
    
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<CategoryDTO>(responseBody) ?? new CategoryDTO();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при обращении [HTTPGet]/Category");
            throw;
        }
    }
}