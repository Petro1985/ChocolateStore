using ChocolateData;
using ChocolateDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InitialFilling;

public class DbInitiator
{
    public async Task InitDB()
    {
        var optBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("DbConnectionString")
                               ?? "Server=db_choco;Port=5432;Database=ChocolateDB;User Id=postgres;password=123qwe!@#QWE;";
        optBuilder.UseNpgsql(connectionString);
        
        var context = new ApplicationDbContext(optBuilder.Options);
        await context.Database.MigrateAsync();

        // if (await context.Users.AnyAsync())
        //     return;
        //
        // var adminUser = new ApplicationUser
        // {
        //     PasswordHash = "AQAAAAIAAYagAAAAELkZJp+TRucpvVdt7xY1R8yKqkxLDIDbXQtrqTVKmMyXSjPohHhmbpL6C7l/EYCPFg==",
        //     UserName = "Admin",
        //     NormalizedUserName = "ADMIN",
        //     IsAdmin = true,
        // };
        //
        // context.Users.Add(adminUser);

        await context.SaveChangesAsync();
    }
    
}