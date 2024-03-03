namespace ChocolateBackEnd.APIStruct;

public class ProductAddRequest 
{
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int TimeToMakeInHours { get; set; }
}