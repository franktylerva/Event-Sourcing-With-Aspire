using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
}