using System.ComponentModel.DataAnnotations.Schema;
using _00_LoginPage.Models;

namespace _00_LoginPage.ViewModeels
{
    public class ShoppingListViewModel : BaseEntity
    {
        public bool IsEditable { get; set; } = true;
        public virtual ICollection<ShoppingListProductViewModel> Products { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserViewModel User { get; set; }
    }
}
