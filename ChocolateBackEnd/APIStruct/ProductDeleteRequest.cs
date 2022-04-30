using Microsoft.AspNetCore.Mvc;

namespace ChocolateBackEnd.APIStruct;

public class ProductDeleteRequest
{
    public long Id { get; set; }
}

public class PhotoDeleteRequest
{
    public long ProductId { get; set; }
    public int PhotoNumber { get; set; }

}