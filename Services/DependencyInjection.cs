using Microsoft.Extensions.DependencyInjection;
using Services.Category;
using Services.Photo;
using Services.Product;
using Services.Profiler;
using Services.Search;

namespace Services;

public static class DependencyInjection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddAutoMapper(opt =>
        {
            opt.AddProfile<ServicesProfile>();
        });
        
        return services;
    }
}