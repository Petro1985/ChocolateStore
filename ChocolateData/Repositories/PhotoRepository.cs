using ChocolateDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class PhotoRepository : BaseRepository<PhotoEntity>
{
    public PhotoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Guid>> GetPhotoIdsByProduct(Guid productId)
    {
        return await _dbContext
            .Photos
            .Where(photo => photo.Product.Id == productId)
            .Select(x => x.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<PhotoEntity>> GetPhotosByProduct(Guid productId)
    {
        return await _dbContext.Photos.Where(photo => photo.Product.Id == productId).ToListAsync();
    }
}