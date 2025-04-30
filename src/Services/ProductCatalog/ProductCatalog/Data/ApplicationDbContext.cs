using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((context, _) =>
        {
            var electronics = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Electronics"
            };

            var sports = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Sports"
            };
            
            context.Set<ProductCategory>().Add(electronics);
            context.Set<ProductCategory>().Add(sports);
            
            context.Set<Product>().Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Basketball",
                Price = new decimal(100.00),
                ProductCategory = sports
            });
            
            context.Set<Product>().Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPad Pro",
                Price = new decimal(1000.00),
                ProductCategory = electronics
            });
        });
    }
}