using MediatR;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.CQRS.Commands;

public record CreateProductCategoryCommand(string Name) : IRequest<ProductCategory>;

public class CreateProductCategoryCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateProductCategoryCommand, ProductCategory>
{
    public async Task<ProductCategory> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
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