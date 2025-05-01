using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Domain;

namespace ProductCatalog.CQRS.Queries;

public record GetProductCategoriesQuery;

public class GetProductCategoriesQueryHandler(ApplicationDbContext context)
{
    public async Task<IEnumerable<ProductCategory?>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await context.ProductCategories.ToListAsync(cancellationToken);
    }
}