using ChocolateDomain;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class PhotoRepository : BaseRepository<Photo>
{
    public PhotoRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Photo>> GetPhotosByProduct(long productId)
    {
        return await _dbContext.Photos.Where(photo => photo.Product.Id == productId).ToListAsync();
    }
}