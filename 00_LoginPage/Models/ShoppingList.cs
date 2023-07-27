﻿
namespace _00_LoginPage.Models;

public class ShoppingList : BaseEntity
{
    public string Name { get; set; } = null!;

    public bool IsEditable { get; set; } = true;

    public int UserId { get; set; }

    // Relations
    public virtual ICollection<ShoppingListProduct> Products { get; set; }
    public virtual User User { get; set; }
}