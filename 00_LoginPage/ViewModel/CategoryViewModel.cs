using System.ComponentModel.DataAnnotations.Schema;
using _00_LoginPage.Models;

namespace _00_LoginPage.ViewModeels
{
    public class CategoryViewModel 
    {
        public int Id { get;  set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

    }
}
