using System.Data.Common;
using System.Linq;
using ChocolateData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChocolateBackEnd.Tests;

internal class MyWebSite : WebApplicationFactory<Program>
{
    private readonly DbConnection _connectionToDb;
    
    public MyWebSite()
    {
        _connectionToDb = new SqliteConnection("Data Source=:memory:");
        _connectionToDb.Open();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var result =  base.CreateHost(builder);
        using var scope = result.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreatedAsync();
        scope.Dispose();
        return result;
    }

    // Change service DbContextOptions<ApplicationDbContext> to use inMemory SQLite instead of usual one
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(collection =>
        {
            var serviceToDelete = collection.FirstOrDefault(q => q.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            collection.Remove(serviceToDelete);
            collection.AddScoped<DbContextOptions<ApplicationDbContext>>(provider =>
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                builder.UseApplicationServiceProvider(provider);
                builder.UseSqlite(_connectionToDb);

                return builder.Options;            
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        _connectionToDb.Close();
        base.Dispose(disposing);
    }
}