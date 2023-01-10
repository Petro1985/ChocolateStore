namespace Models;

public class ProductDTO
{
    public long Id { get; init; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public int TimeToMakeInHours { get; set; }
}