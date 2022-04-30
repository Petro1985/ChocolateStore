namespace ChocolateBackEnd.APIStruct;

public class ProductAddRequest 
{
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public int TimeToMakeInHours { get; set; }
}