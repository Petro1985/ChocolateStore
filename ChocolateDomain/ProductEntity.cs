using ChocolateDomain.Interfaces;

namespace ChocolateDomain;

public class ProductEntity : IEntity
{
    public Guid Id { get; init; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public TimeSpan TimeToMake { get; set; }

    public IEnumerable<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();

    public ProductEntity()
    {
    }

    public ProductEntity(string description, decimal priceRub, TimeSpan timeToMake)
    {
        Description = description;
        PriceRub = priceRub;
        TimeToMake = timeToMake;
    }
}