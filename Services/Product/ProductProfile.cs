using AutoMapper;
using ChocolateDomain;
using ChocolateDomain.Entities;
using Models;

namespace Services.Product;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity, ProductDTO>().ReverseMap();
    }
}