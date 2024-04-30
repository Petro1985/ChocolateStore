using System.Security.AccessControl;
using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using ChocolateDomain.Specifications;
using Models.Category;
using Services.Models;

namespace Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<PagedItems<CategoryEntity>> GetPagedCategoriesSortedByName(int pageSize, int pageNumber) {

        if (pageSize <= 0) {
            throw new ArgumentException("Размер страницы должен быть больше 0", nameof(pageSize));
        }
        if (pageNumber <= 0) {
            throw new ArgumentException("Номер страницы должен быть больше 0 (нумерация с 1)", nameof(pageNumber));
        }

        var totalCount = await _categoryRepository.CountBySpecification(new CategoriesSortedByNameSpecification());

        Specification<CategoryEntity> specification = new CategoriesSortedByNameSpecification()
            .And(new PagingSpecification<CategoryEntity>(pageSize, pageNumber));

        var result = await _categoryRepository.GetBySpecification(specification);
        
        return new PagedItems<CategoryEntity>
        {
            Items = result,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
    }

    public async Task UpdateCategory(CategoryDto category)
    {
        var newCategory = _mapper.Map<CategoryEntity>(category);
        await _categoryRepository.Update(newCategory);
    }
}