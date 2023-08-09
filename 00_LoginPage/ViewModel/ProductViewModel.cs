using System.ComponentModel.DataAnnotations.Schema;
using _00_LoginPage.Models;

namespace _00_LoginPage.ViewModeels
{
    public class ProductViewModel 
    {
        public int Id { get;  set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public string Amount { get; set; }
        public  CategoryViewModel Category { get; set; }

        public int ProductId { get; set; }

        public int ShoppingListId { get; set; }
    }
}
