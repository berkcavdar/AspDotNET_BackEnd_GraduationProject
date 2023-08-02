using _00_LoginPage.ViewModeels;

namespace _00_LoginPage.Models.InsertModel.Models
{
    public class ShoppingListProductInsertModel
    {
        public int ProductId { get; set; }

        public int Amount { get; set; }

        public int ShoppingListId { get; set; }

        public string Description { get; set; }

    }
}
