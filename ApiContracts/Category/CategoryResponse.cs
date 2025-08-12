namespace ApiContracts.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MainPhotoId { get; set; }
}