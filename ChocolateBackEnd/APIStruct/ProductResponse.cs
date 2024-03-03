using ChocolateDomain;

namespace ChocolateBackEnd.APIStruct;

public class ProductResponse
{
    public long Id { get; init; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int TimeToMakeInHours { get; set; }
}