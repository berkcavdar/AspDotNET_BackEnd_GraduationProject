using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _00_LoginPage.Models
{
    public class ShoppingListProductViewModel
    {

        public new int Id { get; set; }

        public new string Name { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductViewModel Product { get; set; }

        public int Amount { get; set; }

        public int ShoppingListId { get; set; }
        [ForeignKey(nameof(ShoppingListId))]
        public virtual ShoppingListViewModel ShoppingList { get; set; }

        public bool IsAddedToCart { get; set; }
    }
}
