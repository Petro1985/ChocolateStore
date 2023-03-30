using Models.Category;

namespace ChocolateAdminUI.Services;

public class CategoryState
{
    public Dictionary<Guid, CategoryDTO> Categories { get; set; } = new ();
}