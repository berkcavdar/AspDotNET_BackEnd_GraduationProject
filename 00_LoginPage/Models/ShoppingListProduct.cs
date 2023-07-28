using System.ComponentModel.DataAnnotations.Schema;

namespace _00_LoginPage.Models;

public class ShoppingListProduct : BaseEntity
{
    public new int Id { get; set; }

    public new string Name { get; set; } = null!;

    public int ProductId { get; set; }

    public int Amount { get; set; }

    public int ShoppingListId { get; set; }

    public bool IsAddedToCart { get; set; }

    // Relations
    [ForeignKey(nameof(ShoppingListId))]    
    public virtual ShoppingList ShoppingList { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
}
