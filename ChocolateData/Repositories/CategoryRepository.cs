using ChocolateData.Repositories.Specifications;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<CategoryEntity>> GetPagedCategoriesSortedByName(int pageSize, int pageNumber) {

        if (pageSize <= 0) {
            throw new ArgumentException("Размер страницы должен быть больше 0", nameof(pageSize));
        }
        
        var specification = new CategoriesSortedByNameSpecification()
            .And(new PagingSpecification<CategoryEntity>(pageSize, pageNumber));
        var resultQuery = ApplySpecification(specification);
        resultQuery = ApplySpecification(resultQuery, specification);
        
        return await resultQuery.ToListAsync();
    }
}