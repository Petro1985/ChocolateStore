using ChocolateUI.Pages.DisplayCategories;
using Models.Category;

namespace ChocolateUI.Services;

public interface ICategoryManager
{
    Task<IEnumerable<CategoryDto>> GetAll();
    Task<CategoryDto> Get();
    Task Add(CategoryDto category);
    Task Delete(CategoryDto category);
    Task Update(CategoryDto category);
}