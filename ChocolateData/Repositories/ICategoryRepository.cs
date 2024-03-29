using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;

namespace ChocolateData.Repositories;

public interface ICategoryRepository : IDbRepository<CategoryEntity> {
    public Task<IReadOnlyCollection<CategoryEntity>> GetPagedCategoriesSortedByName(int pageSize, int pageNumber);
}