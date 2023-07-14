using Models;
using Models.Photo;
using Models.Product;

namespace Services.Photo;

public interface IPhotoService
{
    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO product);
    public Task<Guid> AddPhoto(Guid? productId, byte[] photo);
    public Task Delete(PhotoDTO photo);
    public Task Delete(Guid id);
    public Task<IEnumerable<PhotoDTO>> GetPhotosByProductId(Guid productId);
    public Task<Stream> GetPhoto(Guid photoId);
    public Task<Stream> GetThumbnail(Guid photoId);
    public Task<byte[]> CropPhoto(Stream photo);
}