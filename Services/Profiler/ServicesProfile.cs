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
        CreateMap<ProductEntity, ProductDTO>()
            .ForMember(x => x.TimeToMakeInHours, op => op.MapFrom(x => x.TimeToMake.Hours));
        CreateMap<ProductDTO, ProductEntity>()
            .ForMember(x => x.Category, op => op.Ignore())
            .ForMember(x => x.TimeToMake, op => op.MapFrom(x => TimeSpan.FromHours(x.TimeToMakeInHours)));

        CreateMap<ProductCreateRequest, ProductEntity>();

        CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
    }
}