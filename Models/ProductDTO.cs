namespace Models;

public class ProductDTO
{
    public Guid Id { get; init; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public int TimeToMakeInHours { get; set; }
    public Guid? MainPhotoId { get; set; }
}