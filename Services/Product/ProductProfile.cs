using AutoMapper;
using ChocolateDomain;
using Models;

namespace Services.Product;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity, ProductDTO>().ReverseMap();
    }
}