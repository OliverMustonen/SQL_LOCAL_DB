namespace ConsoleApp.Models.Entities;

internal class ProductEntity
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
