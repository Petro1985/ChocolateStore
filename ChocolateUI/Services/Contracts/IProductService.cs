using Models;

namespace ChocolateUI.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>?> GetItems();
}