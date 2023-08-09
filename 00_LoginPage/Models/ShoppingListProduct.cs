using System.ComponentModel.DataAnnotations.Schema;

namespace _00_LoginPage.Models;

public class ShoppingListProduct : BaseEntity
{
    public new int Id { get; set; }

    public int ProductId { get; set; }

    public int Amount { get; set; }

    public int ShoppingListId { get; set; }

    public bool IsAddedToCart { get; set; }

    public virtual ShoppingList ShoppingList { get; set; }

    public string Description { get; set; }

    public virtual Product Product { get; set; }

}
