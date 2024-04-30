using ChocolateData.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChocolateData;

public static class ServiceCollectionExt
{
    public static IServiceCollection AddDataBase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IPhotoRepository, PhotoRepository>();
        
        return serviceCollection;
    } 
}