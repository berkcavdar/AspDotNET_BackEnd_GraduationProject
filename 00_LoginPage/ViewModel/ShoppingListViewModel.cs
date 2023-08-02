using System.ComponentModel.DataAnnotations.Schema;
using _00_LoginPage.Models;

namespace _00_LoginPage.ViewModeels
{
    public class ShoppingListViewModel 
    {
        public int Id { get;  set; }
        public string Name { get; set; } = null!;
        public bool IsEditable { get; set; } = true;
        public  ICollection<ShoppingListProductViewModel> Products { get; set; }
    }
}
