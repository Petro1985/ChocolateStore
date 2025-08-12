using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using ChocolateDomain.Specifications;
using ChocolateDomain.Specifications.Categories;
using ChocolateDomain.Specifications.Common;
using ChocolateDomain.Specifications.Products;
using Microsoft.EntityFrameworkCore;
using Models.Product;
using Services.Models;

namespace Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IPhotoRepository photoRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _photoRepository = photoRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var specification = new ProductsWithCategorySpecification();
        var result = await _productRepository.GetBySpecification(specification);

        return result.Select(x =>
        {
            var prod = _mapper.Map<ProductDto>(x);
            prod.CategoryName = x.Category.Name;
            return prod;
        });
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategory(Guid categoryId)
    {
        var products = (await _productRepository.GetProductsByCategory(categoryId)).ToList();
        var productsPhotos = await _photoRepository.GetPhotoIdsByProductIds(products.Select(x => x.Id));

        var mappedPhotos = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        foreach (var productDto in mappedPhotos)
        {
            if (productsPhotos.TryGetValue(productDto.Id, out var photos))
            {
                productDto.Photos = photos;
            }
        }
        
        return mappedPhotos;
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var specification = new CategoriesSortedByNameSpecification();
        var categories = await _categoryRepository.GetBySpecification(specification);
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategory(Guid categoryId)
    {
        var category = await _categoryRepository.Get(categoryId);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<Guid> AddNewProduct(ProductDto product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _productRepository.Add(productEntity);
        
        return productEntity.Id;
    }

    public async Task UpdateCategory(CategoryDto category)
    {
        var categoryEntity = _mapper.Map<CategoryEntity>(category);

        await _categoryRepository.Update(categoryEntity);
    }

    public async Task UpdateProduct(ProductDto product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _productRepository.Update(productEntity);
    }

    public async Task<ProductDto> GetProduct(Guid productId)
    {
        var product = await _productRepository.Get(productId);
        var mappedProduct = _mapper.Map<ProductDto>(product);
        return mappedProduct;
    }

    public async Task<ProductDto> GetProductWithPhotoIds(Guid productId)
    {
        var product = await _productRepository.Get(productId);
        var mappedProduct = _mapper.Map<ProductDto>(product);
        mappedProduct.Photos = await _photoRepository.GetPhotoIdsByProduct(productId); 
        return mappedProduct;
    }

    public async Task SetProductPhoto(Guid productId, Guid photoId)
    {
        var product = await _productRepository.Get(productId);
        if (product.MainPhotoId.HasValue && product.MainPhotoId.Value != default)
        {
            await _photoRepository.Delete(product.MainPhotoId.Value);
        }

        product.MainPhotoId = photoId;
        await _productRepository.Update(product);
    }

    public async Task SetCategoryPhoto(Guid categoryId, Guid photoId)
    {
        var category = await _categoryRepository.Get(categoryId);
        if (category.MainPhotoId.HasValue && category.MainPhotoId.Value != default)
        {
            await _photoRepository.Delete(category.MainPhotoId.Value);
        }
        
        category.MainPhotoId = photoId;
        await _categoryRepository.Update(category);
    }

    public async Task<Guid> AddNewCategory(CategoryDto category)
    {
        var categoryEntity = new CategoryEntity
        {
            Name = category.Name
        };

        return await _categoryRepository.Add(categoryEntity);    
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        await _categoryRepository.Delete(categoryId);
    }

    public async Task DeleteProduct(Guid productId)
    {
        await _productRepository.Delete(productId);
    }

    public async Task<PagedItems<ProductEntity>> GetPagedCategoryProductsSortedByName(Guid? categoryId, int pageSize,
        int pageNumber)
    {
        if (pageSize <= 0) {
            throw new ArgumentException("Размер страницы должен быть больше 0", nameof(pageSize));
        }
        if (pageNumber <= 0) {
            throw new ArgumentException("Номер страницы должен быть больше 0 (нумерация с 1)", nameof(pageNumber));
        }

        Specification<ProductEntity> specification = new ProductsSortedByNameSpecification();
        if (categoryId is not null)
        {
            specification = specification.And(new Specification<ProductEntity>(x => x.CategoryId == categoryId));
        }
        var totalCount = await _productRepository.CountBySpecification(specification);

        specification = specification.And(new PagingSpecification<ProductEntity>(pageSize, pageNumber));
        var result = await _productRepository.GetBySpecification(specification);
        
        return new PagedItems<ProductEntity>
        {
            Items = result,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };    
    }
}