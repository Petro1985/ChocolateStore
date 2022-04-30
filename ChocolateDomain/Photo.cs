namespace ChocolateDomain;

public class Photo : IEntity
{
    public long Id { get; init; }
    public string PathToPhoto { get; set; }
    public Product Product {get; init; }

    private Photo(string pathToPhoto)
    {
        PathToPhoto = pathToPhoto;
        Product = null!;
    }
    
    public Photo(string pathToPhoto, Product product) : this (pathToPhoto)
    {
        Product = product;
    }
}