using AutoMapper;
using ChocolateDomain.Entities;
using Models;
using Models.Category;
using Models.Product;

namespace Services.Profiler;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<ProductEntity, ProductDTO>();
        CreateMap<ProductDTO, ProductEntity>()
            .ForMember(x=>x.Category, op => op.Ignore());
        
        CreateMap<ProductCreateRequest, ProductEntity>();
        
        CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
    }
}