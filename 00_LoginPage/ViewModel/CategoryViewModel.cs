using System.ComponentModel.DataAnnotations.Schema;
using _00_LoginPage.Models;

namespace _00_LoginPage.ViewModeels
{
    public class CategoryViewModel : BaseEntity
    {
        [ForeignKey("CategoryId")]
        public virtual ProductViewModel Product { get; set; }
    }
}
