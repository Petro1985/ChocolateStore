using Microsoft.AspNetCore.Http;

namespace Models.Photo;

public class AddPhotoRequest
{
    public Guid ProductId { get; set; }
    public string Photo { get; set; }
}

public class CropPhotoRequest
{
    public IFormFile Photo { get; set; }
}