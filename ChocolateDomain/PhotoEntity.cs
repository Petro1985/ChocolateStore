using ChocolateDomain.Interfaces;

namespace ChocolateDomain;

public class PhotoEntity : IEntity
{
    public Guid Id { get; init; }
    public string PathToPhoto { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity ProductEntity {get; init; }

    private PhotoEntity(string pathToPhoto)
    {
        PathToPhoto = pathToPhoto;
        ProductEntity = null!;
    }
    
    public PhotoEntity(string pathToPhoto, Guid productId) : this (pathToPhoto)
    {
        ProductId = productId;
    }
}