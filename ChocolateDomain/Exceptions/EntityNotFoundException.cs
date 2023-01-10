namespace ChocolateDomain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Type type, Guid id) : base ($"Couldn't find entity name: {type.FullName} Id: {id}") {}
}