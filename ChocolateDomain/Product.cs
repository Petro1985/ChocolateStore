namespace ChocolateDomain;

public class Product : IEntity
{
    public long Id { get; init; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public TimeSpan TimeToMake { get; set; }

    public IEnumerable<Photo> Photos { get; set; } = new List<Photo>();

    public Product(string description, decimal priceRub, TimeSpan timeToMake)
    {
        Description = description;
        PriceRub = priceRub;
        TimeToMake = timeToMake;
    }
}