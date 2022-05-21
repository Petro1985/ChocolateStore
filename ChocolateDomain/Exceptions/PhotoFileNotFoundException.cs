namespace ChocolateDomain.Exceptions;

public class PhotoFileNotFoundException : Exception
{
    public PhotoFileNotFoundException(Photo photo) : base ($"Couldn't find photo file. Product Id is {photo.Product.Id}; photo ID is {photo.Id}.") {}
}