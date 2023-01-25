using Models;
using Models.Photo;
using Models.Product;

namespace Services.Photo;

public interface IPhotoService
{
    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO product);
    public Task<Guid> AddPhoto(Guid productId, byte[] photo);
    public Task Delete(PhotoDTO photo);
    public Task Delete(Guid id);
    public Task<IEnumerable<PhotoDTO>> GetPhotosByProduct(Guid productId);
    public Task<Stream> GetImage(PhotoDTO photo);
    public Task<Stream> GetImage(Guid photoId);
    public Task<byte[]> CropPhoto(Stream photo);
}