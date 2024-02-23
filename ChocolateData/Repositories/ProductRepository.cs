using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Exceptions;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ChocolateData.Repositories;

public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsByCategory(Guid categoryId)
    {
        return await DbContext
            .Products
            .Where(x => x.CategoryId == categoryId).ToListAsync();
    }
}

public interface IProductRepository : IDbRepository<ProductEntity>
{
    Task<IEnumerable<ProductEntity>> GetProductsByCategory(Guid categoryId);
}




