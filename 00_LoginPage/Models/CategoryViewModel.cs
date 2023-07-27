using System.ComponentModel.DataAnnotations.Schema;

namespace _00_LoginPage.Models
{
    public class CategoryViewModel : BaseEntity
    {
        [ForeignKey("CategoryId")]
        public virtual ProductViewModel Product { get; set; }
    }
}
