using ChocolateDomain.Entities;

namespace ChocolateData.Repositories;

public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}