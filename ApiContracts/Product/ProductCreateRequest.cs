namespace Models.Product;

public class ProductCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public int TimeToMakeInHours { get; set; }
    public Guid CategoryId { get; set; }
}