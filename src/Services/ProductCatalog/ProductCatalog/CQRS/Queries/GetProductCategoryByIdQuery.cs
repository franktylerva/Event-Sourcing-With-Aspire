using MediatR;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.CQRS.Queries;

public record GetProductCategoryByIdQuery(Guid Id) : IRequest<ProductCategory>;

public class GetProductCategoryByIdQueryHandler(ApplicationDbContext context) : IRequestHandler<GetProductCategoryByIdQuery, ProductCategory>
{
    public async Task<ProductCategory> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.ProductCategories.FindAsync(request.Id);
    }
}