using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Domain;

namespace ProductCatalog.CQRS.Queries;

public record GetProductsQuery;

public class GetProductsQueryHandler(ApplicationDbContext context)
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await context.Products.Include(p => p.ProductCategory).ToListAsync();
    }
}