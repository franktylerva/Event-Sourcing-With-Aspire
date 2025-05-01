using ProductCatalog.Data;

namespace ProductCatalog.CQRS.Commands;

public record UpdateProductCommand(Guid Id, string Name, decimal Price, Guid ProductCategoryId);

public class UpdateProductCommandHandler(ApplicationDbContext context)
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await context.ProductCategories.FindAsync(request.ProductCategoryId);
        var product = await context.Products.FindAsync(request.Id, cancellationToken);
        if (product == null) return false;

        product.Name = request.Name;
        product.Price = request.Price;
        product.ProductCategory = productCategory;
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}