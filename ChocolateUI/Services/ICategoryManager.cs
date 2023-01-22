using ChocolateUI.Pages.DisplayCategories;
using Models.Category;

namespace ChocolateUI.Services;

public interface ICategoryManager
{
    Task<IEnumerable<CategoryDTO>> GetAll();
    Task<CategoryDTO> Get();
    Task Add(CategoryDTO category);
    Task Delete(CategoryDTO category);
    Task Update(CategoryDTO category);
}