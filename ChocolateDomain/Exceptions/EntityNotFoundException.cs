namespace ChocolateDomain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Type type, long id) : base ($"Couldn't find entity name: {type.FullName} Id: {id}") {}
}
public class PhotoFileNotFoundException : Exception
{
    public PhotoFileNotFoundException(Photo photo) : base ($"Couldn't find photo file. Product Id is {photo.Product.Id}; photo ID is {photo.Id}.") {}
}

public class PhotoNotFoundException : Exception
{
    public PhotoNotFoundException(long productId) : base ($"Couldn't find photo for product {productId}") {}
}