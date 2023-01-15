namespace Models;

public class ProductDTO
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    
    public int TimeToMakeInHours { get; set; }
    public Guid? MainPhotoId { get; set; }
    
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public IEnumerable<Guid> Photos { get; set; }
}