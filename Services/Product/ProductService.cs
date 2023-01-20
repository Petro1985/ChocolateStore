using System.Linq.Expressions;
using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Category;
using Models.Product;

namespace Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productDb;
    private readonly IDbRepository<CategoryEntity> _categoryDb;
    private readonly PhotoRepository _photoDb;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productDb, IMapper mapper, IDbRepository<CategoryEntity> categoryDb, IDbRepository<PhotoEntity> photoDb)
    {
        _productDb = productDb;
        _mapper = mapper;
        _categoryDb = categoryDb;
        _photoDb = (PhotoRepository)photoDb;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProducts()
    {
        var product = await _productDb
            .GetQuery()
            .Include(x => x.Category).ToListAsync();

        return product.Select(x =>
        {
            var prod = _mapper.Map<ProductDTO>(x);
            prod.CategoryName = x.Category.Name;
            return prod;
        });
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(Guid categoryId)
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(await _productDb.GetProductsByCategory(categoryId));
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

        await _categoryDb.Change(categoryEntity);
    }

    public async Task UpdateProduct(ProductDTO product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);

        await _productDb.Change(productEntity);
    }

    public async Task<ProductDTO> GetProduct(Guid productId)
    {
        var product = await _productDb.Get(productId);
        var mappedProduct = _mapper.Map<ProductDTO>(product);
        return mappedProduct;
    }

    public async Task<ProductDTO> GetProductWithPhotoIds(Guid productId)
    {
        var product = await _productDb.Get(productId);
        var mappedProduct = _mapper.Map<ProductDTO>(product);
        mappedProduct.Photos = await _photoDb.GetPhotoIdsByProduct(productId); 
        return mappedProduct;
    }

    public async Task SetMainPhoto(Guid productId, Guid photoId)
    {
        var productEntity = new ProductEntity
        {
            Id = productId,
            MainPhotoId = photoId
        };
        
        await _productDb.Change(productEntity);
    }

    public async Task<Guid> AddNewCategory(CategoryDTO category)
    {
        var categoryEntity = new CategoryEntity
        {
            Name = category.Name
        };

        return await _categoryDb.Add(categoryEntity);    
    }
}