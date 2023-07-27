using System.ComponentModel.DataAnnotations.Schema;

namespace _00_LoginPage.Models
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
