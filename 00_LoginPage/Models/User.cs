namespace _00_LoginPage.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsAdmin { get; set; }

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
}

