using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;

namespace ChocolateData.Repositories;

public interface ICategoryRepository : IDbRepository<CategoryEntity>
{
}