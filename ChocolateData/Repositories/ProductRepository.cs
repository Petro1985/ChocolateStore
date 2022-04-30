using ChocolateDomain;
using ChocolateDomain.Exceptions;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateData.Repositories;

public class ProductRepository : BaseRepository<Product>
{
    public ProductRepository(DbContext dbContext) : base(dbContext)
    {
    }
}