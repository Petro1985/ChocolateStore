using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using ChocolateDomain.Specifications;
using Microsoft.EntityFrameworkCore;
using Models.Category;
using Models.Product;

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

    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var specification = new CategoriesSortedByNameSpecification();
        var categories = await _categoryRepository.GetBySpecification(specification);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategory(Guid categoryId)
    {
        var category = await _categoryRepository.Get(categoryId);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<Guid> AddNewProduct(ProductCreateRequest product)
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
}