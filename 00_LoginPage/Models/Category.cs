namespace _00_LoginPage.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = null!;

    private Category()
    {
        
    }

    public Category(string name,string url)
    {
        this.Name = name;
        this.ImageUrl = url;
    }

}
