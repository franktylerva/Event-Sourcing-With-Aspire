namespace Blazor.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}