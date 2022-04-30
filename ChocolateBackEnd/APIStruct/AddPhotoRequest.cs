using Microsoft.AspNetCore.Mvc;

namespace ChocolateBackEnd.APIStruct;

public class AddPhotoRequest
{
    [FromRoute]
    public long ProductId { get; set; }

    [FromForm]
    public IFormFile Photo { get; set; }
}