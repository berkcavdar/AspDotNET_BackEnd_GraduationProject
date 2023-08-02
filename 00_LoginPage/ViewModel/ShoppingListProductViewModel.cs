namespace _00_LoginPage.ViewModeels
{
    public class ShoppingListProductViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public  ProductViewModel Product { get; set; }

        public int Amount { get; set; }

        public int ShoppingListId { get; set; }

        public bool IsAddedToCart { get; set; }

        public string ImageUrl { get; set; }
    }
}
