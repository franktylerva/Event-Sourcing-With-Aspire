using MediatR;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.CQRS.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Product>;

public class GetProductByIdQueryHandler(ApplicationDbContext context) : IRequestHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Products.FindAsync(request.Id);
    }
}