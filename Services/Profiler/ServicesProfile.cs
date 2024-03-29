﻿using AutoMapper;
using ChocolateDomain.Entities;
using Models;
using Models.Category;
using Models.Product;

namespace Services.Profiler;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<ProductEntity, ProductDto>()
            .ForMember(x => x.TimeToMakeInHours, op => op.MapFrom(x => x.TimeToMake.Hours))
            .ForMember(x => x.Photos, op => op.Ignore());
        CreateMap<ProductDto, ProductEntity>()
            .ForMember(x => x.Category, op => op.Ignore())
            .ForMember(x => x.TimeToMake, op => op.MapFrom(x => TimeSpan.FromHours(x.TimeToMakeInHours)));

        CreateMap<ProductCreateRequest, ProductEntity>();

        CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
    }
}