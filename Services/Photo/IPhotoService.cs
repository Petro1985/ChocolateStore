using Models;

namespace Services.Photo;

public interface IPhotoService
{
    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO product);
    public Task<Guid> AddPhoto(Guid productId, Stream photo);
    public Task Delete(PhotoDTO photo);
    public Task Delete(Guid id);
    public Task<IEnumerable<PhotoDTO>> GetPhotosByProduct(Guid productId);
    public Task<Stream> GetPhotoFile(PhotoDTO photo);
}