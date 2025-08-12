using System.Linq.Expressions;
using ChocolateDomain.Entities;
using Services.Models;

namespace Services.Category;

public interface ICategoryService
{
    public Task<PagedItems<CategoryEntity>> GetPagedCategoriesSortedByName(int pageSize, int pageNumber,
        Expression<Func<CategoryEntity, bool>>? criteria = null);

    public Task UpdateCategory(CategoryDto category);

    public Task<IReadOnlyCollection<CategoryDto>> GetAllCategories();
}