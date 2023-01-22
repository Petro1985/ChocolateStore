namespace ChocolateUI.Services;

public class CategoryItem : IItem
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid MainPhotoId { get; set; }
}