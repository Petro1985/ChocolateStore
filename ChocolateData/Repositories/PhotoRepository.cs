using ChocolateDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class PhotoRepository : BaseRepository<PhotoEntity>, IPhotoRepository
{
    public PhotoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Guid>> GetPhotoIdsByProduct(Guid productId)
    {
        return await DbContext
            .Photos
            .Where(photo => photo.Product.Id == productId)
            .Select(x => x.Id)
            .ToListAsync();
    }

    public async Task<Dictionary<Guid, IEnumerable<Guid>>> GetPhotoIdsByProductIds(IEnumerable<Guid> productIds)
    {
        var photo = await DbContext.Photos
            .Where(x => productIds.Contains(x.ProductId!.Value))
            .GroupBy(x => x.ProductId)
            .Select(x => new { x.Key, Photos = x.Select(y => y.Id) })
            .ToDictionaryAsync(x => 
                x.Key!.Value, 
                x => x.Photos);

        return photo;
    }

    public async Task<IEnumerable<PhotoEntity>> GetPhotosByProduct(Guid productId)
    {
        return await DbContext.Photos.Where(photo => photo.Product.Id == productId).ToListAsync();
    }

    public async Task<byte[]> GetPhoto(Guid photoId)
    {
        return await DbContext.Photos.Where(x => x.Id == photoId)
            .Select(x => x.Image).FirstAsync();
    }

    public async Task<byte[]?> GetThumbnail(Guid photoId)
    {
        return await DbContext.Photos.Where(x => x.Id == photoId)
            .Select(x => x.Thumbnail).FirstAsync();
    }
}