using ProductCatalog.Data;
using ProductCatalog.Domain;

namespace ProductCatalog.CQRS.Queries;

public record GetProductCategoryByIdQuery(Guid Id);

public class GetProductCategoryByIdQueryHandler(ApplicationDbContext context)
{
    public async Task<ProductCategory?> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.ProductCategories.FindAsync(request.Id);
    }
}