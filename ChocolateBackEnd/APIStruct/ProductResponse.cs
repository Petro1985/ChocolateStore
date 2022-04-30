using ChocolateDomain;

namespace ChocolateBackEnd.APIStruct;

public class ProductResponse
{
    public long Id { get; init; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public int TimeToMakeInHours { get; set; }
//    public IEnumerable<Photo> Photos { get; set; } = new List<Photo>();
}