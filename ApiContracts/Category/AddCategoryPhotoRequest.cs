namespace Models.Category;

public class AddCategoryPhotoRequest
{
    public string PhotoBase64 { get; set; }
    public Guid CategoryId { get; set; }
}