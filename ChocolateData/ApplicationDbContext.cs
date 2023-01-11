using ChocolateDomain;
using ChocolateDomain.Entities;
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
            .WithOne(photo => photo.Product)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ProductEntity>().HasOne<PhotoEntity>(prod => prod.MainPhoto);
        
        base.OnModelCreating(modelBuilder);
    }
}