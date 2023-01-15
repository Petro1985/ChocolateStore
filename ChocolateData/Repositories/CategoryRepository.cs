using ChocolateDomain.Entities;

namespace ChocolateData.Repositories;

public class CategoryRepository : BaseRepository<CategoryEntity>
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}