using AutoMapper;
using ChocolateDomain.Entities;
using Models;

namespace Services.Profiler;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<ProductEntity, ProductDTO>().ReverseMap();
        CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
    }
}