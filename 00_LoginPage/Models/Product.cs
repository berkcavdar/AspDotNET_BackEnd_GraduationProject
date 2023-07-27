namespace _00_LoginPage.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
