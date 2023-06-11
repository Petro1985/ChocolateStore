using ChocolateData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InitialFilling;

public class DbInitiator
{
    public async Task InitDB()
    {
        var optBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("DbConnectionString")
                               ?? @"Server=localhost;Port=5444;Database=ChocolateDB;User Id=postgres;password=123qwe!@#QWE";
        optBuilder.UseNpgsql(connectionString);
        
        var context = new ApplicationDbContext(optBuilder.Options);
        await context.Database.MigrateAsync();

        if (await context.Users.AnyAsync())
            return;

        var adminUser = new IdentityUser
        {
            PasswordHash = "AQAAAAIAAYagAAAAELkZJp+TRucpvVdt7xY1R8yKqkxLDIDbXQtrqTVKmMyXSjPohHhmbpL6C7l/EYCPFg==",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
        };
        
        context.Users.Add(adminUser);

        var usersClaim = new IdentityUserClaim<string>
        {
            Id = 1,
            ClaimType = "Admin",
            ClaimValue = "It's me'",
            UserId = adminUser.Id
        };

        context.UserClaims.Add(usersClaim);

        await context.SaveChangesAsync();
    }
    
}