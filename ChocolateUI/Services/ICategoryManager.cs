using ChocolateUI.Pages.DisplayCategories;

namespace ChocolateUI.Services;

public interface ICategoryManager
{
    Task<IEnumerable<CategoryResponse>> GetAll();
    Task<CategoryResponse> Get();
    Task Add(CategoryResponse category);
    Task Delete(CategoryResponse category);
    Task Update(CategoryResponse category);
}