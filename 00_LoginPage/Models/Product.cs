using System.ComponentModel.DataAnnotations.Schema;

namespace _00_LoginPage.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }

    private Product()
    {
        
    }

    public Product(string name, string description,string color,decimal price,string url, int categoryId)
    {
        this.Name = name;
        this.Description = description;
        this.Color = color;
        this.Price = price;
        this.ImageUrl = url;
        this.CategoryId = categoryId;
    }
    public virtual Category Category { get; set; } = null!;
}
