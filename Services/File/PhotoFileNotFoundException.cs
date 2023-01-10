namespace Services.File;

public class PhotoFileNotFoundException : Exception
{
    public PhotoFileNotFoundException(string fileName) 
        : base ($"Couldn't find photo file \"{fileName}\"") {}
}