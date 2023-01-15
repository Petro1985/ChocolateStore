namespace Models;

public class CategoryDTO
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid MainPhotoId { get; set; }
}