using System.Net.Http.Json;
using Models;

namespace ChocolateUI.Services.Contracts;

class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDTO>?> GetItems()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>("Products");
            return products;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}