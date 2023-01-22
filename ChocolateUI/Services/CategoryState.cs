using Models.Category;

namespace ChocolateUI.Services;

public class CategoryState
{
    public Dictionary<Guid, CategoryDTO> Categories { get; set; } = new ();
}