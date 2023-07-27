namespace _00_LoginPage.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = null!;
}
