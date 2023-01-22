namespace Models.Photo;

public class AddPhotoRequest
{
    public Guid ProductId { get; set; }
    public string Photo { get; set; }
}