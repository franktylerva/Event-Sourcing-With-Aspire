using ProductCatalog.Data;
using ProductCatalog.Domain;

namespace ProductCatalog.CQRS.Queries;

public record GetProductByIdQuery(Guid Id);

public class GetProductByIdQueryHandler(ApplicationDbContext context)
{
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Products.FindAsync(request.Id);
    }
}