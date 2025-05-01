using ProductCatalog.Data;
using ProductCatalog.Domain;

namespace ProductCatalog.CQRS.Commands;

public record CreateProductCommand(string Name, decimal Price, Guid ProductCategoryId);

public class CreateProductCommandHandler(ApplicationDbContext context)
{
    public async Task<Product?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await context.ProductCategories.FindAsync(request.ProductCategoryId);
        
        var product = new Product { Id = Guid.NewGuid(), Name = request.Name, Price = request.Price, ProductCategory = productCategory };
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }
}