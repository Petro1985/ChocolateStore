using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Category;
using Models.Product;

namespace Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productDb;
    private readonly ICategoryRepository _categoryDb;
    private readonly IPhotoRepository _photoDb;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productDb, ICategoryRepository categoryDb, IPhotoRepository photoDb, IMapper mapper)
    {
        _productDb = productDb;
        _mapper = mapper;
        _categoryDb = categoryDb;
        _photoDb = photoDb;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var product = await _productDb
            .GetQuery()
            .Include(x => x.Photos)
            .Include(x => x.Category).ToListAsync();

        return product.Select(x =>
        {
            var prod = _mapper.Map<ProductDto>(x);
            prod.CategoryName = x.Category.Name;
            return prod;
        });
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategory(Guid categoryId)
    {
        var products = (await _productDb.GetProductsByCategory(categoryId)).ToList();
        var productsPhotos = await _photoDb.GetPhotoIdsByProductIds(products.Select(x => x.Id));

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

    public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
    {
        var categories = await _categoryDb.GetQuery().ToListAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<CategoryDTO> GetCategory(Guid categoryId)
    {
        var category = await _categoryDb.Get(categoryId);
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task<Guid> AddNewProduct(ProductCreateRequest product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _productDb.Add(productEntity);
        
        return productEntity.Id;
    }

    public async Task UpdateCategory(CategoryDTO category)
    {
        var categoryEntity = _mapper.Map<CategoryEntity>(category);

        await _categoryDb.Update(categoryEntity);
    }

    public async Task UpdateProduct(ProductDto product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _productDb.Update(productEntity);
    }

    public async Task<ProductDto> GetProduct(Guid productId)
    {
        var product = await _productDb.Get(productId);
        var mappedProduct = _mapper.Map<ProductDto>(product);
        return mappedProduct;
    }

    public async Task<ProductDto> GetProductWithPhotoIds(Guid productId)
    {
        var product = await _productDb.Get(productId);
        var mappedProduct = _mapper.Map<ProductDto>(product);
        mappedProduct.Photos = await _photoDb.GetPhotoIdsByProduct(productId); 
        return mappedProduct;
    }

    public async Task SetProductPhoto(Guid productId, Guid photoId)
    {
        var product = await _productDb.Get(productId);
        if (product.MainPhotoId.HasValue && product.MainPhotoId.Value != default)
        {
            await _photoDb.Delete(product.MainPhotoId.Value);
        }

        product.MainPhotoId = photoId;
        await _productDb.Update(product);
    }

    public async Task SetCategoryPhoto(Guid categoryId, Guid photoId)
    {
        var category = await _categoryDb.Get(categoryId);
        if (category.MainPhotoId.HasValue && category.MainPhotoId.Value != default)
        {
            await _photoDb.Delete(category.MainPhotoId.Value);
        }
        
        category.MainPhotoId = photoId;
        await _categoryDb.Update(category);
    }

    public async Task<Guid> AddNewCategory(CategoryDTO category)
    {
        var categoryEntity = new CategoryEntity
        {
            Name = category.Name
        };

        return await _categoryDb.Add(categoryEntity);    
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        await _categoryDb.Delete(categoryId);
    }

    public async Task DeleteProduct(Guid productId)
    {
        await _productDb.Delete(productId);
    }
}