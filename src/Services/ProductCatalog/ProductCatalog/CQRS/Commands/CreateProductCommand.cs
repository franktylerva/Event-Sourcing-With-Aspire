using MediatR;
using ProductCatalog.CQRS.Queries;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.CQRS.Commands;

public record CreateProductCommand(string Name, decimal Price, Guid ProductCategoryId) : IRequest<Product>;

public class CreateProductCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await context.ProductCategories.FindAsync(request.ProductCategoryId);
        
        var product = new Product { Id = Guid.NewGuid(), Name = request.Name, Price = request.Price, ProductCategory = productCategory };
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }
}