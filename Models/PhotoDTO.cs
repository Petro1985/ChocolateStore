namespace Models;

public class PhotoDTO
{
    public Guid Id { get; init; }
    public string PathToPhoto { get; set; }
    public Guid ProductId { get; set; }
}