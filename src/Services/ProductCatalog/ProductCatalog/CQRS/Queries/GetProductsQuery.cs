using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.CQRS.Queries;

public record GetProductsQuery : IRequest<IEnumerable<Product>>;

public class GetProductsQueryHandler(ApplicationDbContext context) : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await context.Products.Include(p => p.ProductCategory).ToListAsync();
    }
}