namespace ChocolateDomain.Interfaces;

public interface IPhotoService
{
    public Task<IEnumerable<Photo>> GetPhotos(Product product);
    public Task AddPhoto(Product product, Byte[] photo);
}