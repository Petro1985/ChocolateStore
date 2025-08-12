namespace ApiContracts.Category;

public class AddMainPhotoRequest
{
    public string PhotoBase64 { get; set; }
    public Guid EntityId { get; set; }
}