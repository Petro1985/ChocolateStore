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
            .ToTable("Products")
            .HasKey("Id");
        modelBuilder.Entity<ProductEntity>()
            .HasMany<PhotoEntity>(product => product.Photos)
            .WithOne(photo => photo.Product)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ProductEntity>()
            .HasOne<PhotoEntity>(prod => prod.MainPhoto)
            .WithOne().HasForeignKey<ProductEntity>(x => x.MainPhotoId)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<ProductEntity>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products);
        
        
        modelBuilder.Entity<PhotoEntity>()
            .ToTable("Photos")
            .HasKey("Id");

        
        modelBuilder.Entity<CategoryEntity>()
            .ToTable("Categories")
            .HasKey("Id");
        modelBuilder.Entity<CategoryEntity>()
            .HasMany(x => x.Products)
            .WithOne(x => x.Category);
        
        
        base.OnModelCreating(modelBuilder);
    }
}