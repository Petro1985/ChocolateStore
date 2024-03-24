using ChocolateBackEnd.Auth.Fido2;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChocolateBackEnd.Auth;

public class OpenIddictDbContext : IdentityDbContext<ApplicationUser>
{
    public OpenIddictDbContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<FidoStoredCredential> FidoStoredCredential => Set<FidoStoredCredential>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FidoStoredCredential>().HasKey(m => m.Id);

        base.OnModelCreating(builder);
    }
}