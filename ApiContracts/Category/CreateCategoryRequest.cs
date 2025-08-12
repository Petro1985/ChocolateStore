namespace ApiContracts.Category;

public class CreateCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MainPhotoId { get; set; }
}