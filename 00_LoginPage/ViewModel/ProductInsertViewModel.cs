using _00_LoginPage.ViewModeels;

namespace _00_LoginPage.ViewModel
{
    public class ProductInsertViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public IReadOnlyList<CategoryViewModel> Categories { get; set; }
    }
}
