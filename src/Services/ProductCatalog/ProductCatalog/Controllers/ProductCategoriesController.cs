using Microsoft.AspNetCore.Mvc;
using ProductCatalog.CQRS.Commands;
using ProductCatalog.CQRS.Queries;
using ProductCatalog.Domain;
using Wolverine;

namespace ProductCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController(IMessageBus messageBus) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductCategories()
    {
        var productsCategories = await messageBus.InvokeAsync<IEnumerable<ProductCategory>>(new GetProductCategoriesQuery());
        return Ok(productsCategories);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductCategory(Guid id)
    {
        var productCategory = await messageBus.InvokeAsync<ProductCategory>(new GetProductCategoryByIdQuery(id));
        return Ok(productCategory);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryCommand command)
    {
        var productCategory = await messageBus.InvokeAsync<ProductCategory>(command);
        return CreatedAtAction(nameof(GetProductCategory), new { id = productCategory.Id }, productCategory);
    }
}