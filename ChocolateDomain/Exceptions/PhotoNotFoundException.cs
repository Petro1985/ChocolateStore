namespace ChocolateDomain.Exceptions;

public class PhotoNotFoundException : Exception
{
    public PhotoNotFoundException(long productId) : base ($"Couldn't find photo for product {productId}") {}
}