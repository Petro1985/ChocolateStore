namespace Services.Photo;

public class PhotoServiceOptions
{
    public const string Path = "PhotoService";
    public int BigPhotoSize { get; set; }
    public int SmallPhotoSize { get; set; }
}