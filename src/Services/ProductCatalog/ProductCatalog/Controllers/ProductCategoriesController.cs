using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.CQRS.Commands;
using ProductCatalog.CQRS.Queries;

namespace ProductCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductCategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductCategories()
    {
        var productsCategories = await mediator.Send(new GetProductCategoriesQuery());
        return Ok(productsCategories);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductCategory(Guid id)
    {
        var productCategory = await mediator.Send(new GetProductCategoryByIdQuery(id));
        return Ok(productCategory);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryCommand command)
    {
        var productCategory = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProductCategory), new { id = productCategory.Id }, productCategory);
    }
}