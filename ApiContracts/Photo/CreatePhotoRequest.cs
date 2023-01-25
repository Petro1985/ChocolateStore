using Microsoft.AspNetCore.Http;

namespace Models.Photo;

public class AddPhotoRequest
{
    public Guid? ProductId { get; set; }
    public string PhotoBase64 { get; set; }
}
