namespace ChocolateDomain.Exceptions;

public class PhotoNotFoundException : Exception
{
    public PhotoNotFoundException(Guid productId) : base ($"Couldn't find photo for product {productId}") {}
}