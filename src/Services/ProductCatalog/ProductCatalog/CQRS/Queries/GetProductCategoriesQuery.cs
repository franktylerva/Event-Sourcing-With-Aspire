using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.CQRS.Queries;

public record GetProductCategoriesQuery : IRequest<IEnumerable<ProductCategory>>;

public class GetProductCategoriesQueryHandler(ApplicationDbContext context) : IRequestHandler<GetProductCategoriesQuery, IEnumerable<ProductCategory>>
{
    public async Task<IEnumerable<ProductCategory>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await context.ProductCategories.ToListAsync(cancellationToken);
    }
}