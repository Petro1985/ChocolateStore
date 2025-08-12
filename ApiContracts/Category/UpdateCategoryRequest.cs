namespace ApiContracts.Category;

public class UpdateCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MainPhotoId { get; set; }
}