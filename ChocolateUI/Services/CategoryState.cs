namespace ChocolateUI.Services;

public class CategoryState
{
    public Dictionary<Guid, CategoryResponse> Categories { get; set; } = new ();
}