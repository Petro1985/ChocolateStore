using Models;
using Models.Photo;
using Models.Product;

namespace Services.Photo;

public interface IPhotoService
{
    public Task<IEnumerable<PhotoDto>> GetPhotos(ProductDto product);
    public Task<Guid> AddPhoto(Guid? productId, byte[] photo);
    public Task Delete(PhotoDto photo);
    public Task Delete(Guid id);
    public Task TryDelete(Guid id);
    public Task<IEnumerable<PhotoDto>> GetPhotosByProductId(Guid productId);
    public Task<Stream> GetPhoto(Guid photoId);
    public Task<Stream> GetThumbnail(Guid photoId);
    public Task<byte[]> CropPhoto(Stream photo);
}