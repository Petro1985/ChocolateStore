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
}