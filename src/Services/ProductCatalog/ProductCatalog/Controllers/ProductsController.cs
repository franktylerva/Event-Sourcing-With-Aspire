using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.CQRS.Commands;
using ProductCatalog.CQRS.Queries;
using ProductCatalog.Domain;
using Wolverine;

namespace ProductCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMessageBus messageBus) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProducts()
    {
        var products = await messageBus.InvokeAsync<IEnumerable<Product>>(new GetProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await messageBus.InvokeAsync<Product>(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var product = await messageBus.InvokeAsync<Product>(command);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest();
        var success = await messageBus.InvokeAsync<bool>(command);
        return success ? NoContent() : NotFound();
    }
}