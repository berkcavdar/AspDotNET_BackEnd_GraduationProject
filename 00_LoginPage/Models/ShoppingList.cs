﻿
namespace _00_LoginPage.Models;

public class ShoppingList : BaseEntity
{
    public string Name { get; set; } = null!;

    public bool IsEditable { get; set; } = true;

    public int UserId { get; set; }

    // Relations
    public virtual ICollection<ShoppingListProduct> ShoppingListProducts { get; set; }
    public virtual User User { get; set; }

    private ShoppingList()
    {
        
    }

    public ShoppingList(string name, int userId)
    {
        UserId = userId;
        Name = name;
    }
}
