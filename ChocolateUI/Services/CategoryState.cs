using Models.Category;

namespace ChocolateUI.Services;

public class CategoryState
{
    public Dictionary<Guid, CategoryDto> Categories { get; set; } = new ();
}