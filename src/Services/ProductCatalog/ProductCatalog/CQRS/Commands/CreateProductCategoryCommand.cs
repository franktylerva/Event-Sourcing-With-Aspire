using ProductCatalog.Data;
using ProductCatalog.Domain;

namespace ProductCatalog.CQRS.Commands;

public record CreateProductCategoryCommand(string Name);

public class CreateProductCategoryCommandHandler(ApplicationDbContext context)
{
    public async Task<ProductCategory?> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = new ProductCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        
        context.ProductCategories.Add(productCategory);
        await context.SaveChangesAsync(cancellationToken);
        return productCategory;
    }
}