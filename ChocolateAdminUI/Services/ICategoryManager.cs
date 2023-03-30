using Models.Category;

namespace ChocolateAdminUI.Services;

public interface ICategoryManager
{
    Task<IEnumerable<CategoryDTO>> GetAll();
    Task<CategoryDTO> Get();
    Task Add(CategoryDTO category);
    Task Delete(CategoryDTO category);
    Task Update(CategoryDTO category);
}