using ChocolateDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChocolateData;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<PhotoEntity> Photos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>()
            .HasMany<PhotoEntity>(product => product.Photos)
            .WithOne(photo => photo.ProductEntity)
            .OnDelete(DeleteBehavior.Restrict);
        
        base.OnModelCreating(modelBuilder);
    }
}