using AutoMapper;
using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProducts();

    Task<Guid> AddNewProduct(ProductDTO product);

    Task UpdateProduct(ProductDTO product);

    Task<ProductDTO> Get(Guid productId);

    Task SetMainPhoto(Guid productId, Guid photoId);
}

public class ProductService : IProductService
{
    private readonly IDbRepository<ProductEntity> _productDb;
    private readonly IMapper _mapper;

    public ProductService(IDbRepository<ProductEntity> productDb, IMapper mapper)
    {
        _productDb = productDb;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProducts()
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(await _productDb.GetQuery().ToListAsync());
    }

    public async Task<Guid> AddNewProduct(ProductDTO product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _productDb.Add(productEntity);

        return productEntity.Id;
    }

    public async Task UpdateProduct(ProductDTO product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);

        await _productDb.Change(productEntity);
    }

    public async Task<ProductDTO> Get(Guid productId)
    {
        return _mapper.Map<ProductDTO>(await _productDb.Get(productId));
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
}