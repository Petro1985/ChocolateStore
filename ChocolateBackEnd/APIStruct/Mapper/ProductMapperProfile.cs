using AutoMapper;
using ChocolateDomain;
using ChocolateDomain.Entities;

namespace ChocolateBackEnd.APIStruct.Mapper;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        base.CreateMap<ProductEntity, ProductResponse>()
            .ForMember(response => response.Id, option => option.MapFrom(taskToDo => taskToDo.Id))
            .ForMember(response => response.PriceRub, option => option.MapFrom(taskToDo => taskToDo.PriceRub))
            .ForMember(response => response.Description, option => option.MapFrom(taskToDo => taskToDo.Description))
            .ForMember(response => response.TimeToMakeInHours, option => option.MapFrom(product => (int)product.TimeToMake.TotalHours));
    }
}