using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using Models.Category;
using Services.Models;

namespace Services.Category;

public interface ICategoryService
{
    public Task<PagedItems<CategoryEntity>> GetPagedCategoriesSortedByName(int pageSize, int pageNumber);

    public Task UpdateCategory(CategoryDto category);
}