using _00_LoginPage.Context;
using _00_LoginPage.Models;
using _00_LoginPage.Models.InsertModel.Models;
using _00_LoginPage.Models.UpdateModel.Models;
using _00_LoginPage.ViewModeels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _00_LoginPage.Controllers
{
    public class ShoppingListProductController : Controller
    {
        private readonly ShoppingDbContext _userDbContext;

        public ShoppingListProductController(ShoppingDbContext context)
        {
            _userDbContext = context;
        }

        public IActionResult Index()
        {

            IReadOnlyList<ShoppingListProductViewModel> shoppingListProducts = _userDbContext.ShoppingListProducts.Select(x => new ShoppingListProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Amount = x.Amount,
                ImageUrl = x.Product.ImageUrl,
                IsAddedToCart = x.IsAddedToCart,
                ShoppingListId = x.ShoppingListId,
                Product = new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.Product.ImageUrl,
                    Color = x.Product.Color,
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    Category = new CategoryViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }
                }
            }).ToList();

            return View(shoppingListProducts);

        }

        [HttpGet]
        public IActionResult Create()
        {
            ShoppingListProductInsertModel model = new ShoppingListProductInsertModel(); 
            return View(model);
        }


        [HttpPost]
        public IActionResult Create(ShoppingListProductInsertModel input)
        {

            var newShoppingListProducts = new ShoppingListProduct(input.ProductId, input.Amount, input.ShoppingListId, input.Description);

            _userDbContext.ShoppingListProducts.Add(newShoppingListProducts);
            _userDbContext.SaveChanges();

            return RedirectToAction("Index");
        }





        [HttpGet]
        public IActionResult Update(int id)
        {
            var shoppingListProducts = _userDbContext.ShoppingListProducts.SingleOrDefault(x => x.Id == id);  

           if(shoppingListProducts == null)
            {
                throw new NullReferenceException();
            }

            ShoppingListProductUpdateModel updateShoppingListProducts = new ShoppingListProductUpdateModel();

            shoppingListProducts.Amount = updateShoppingListProducts.Amount;
            return View(shoppingListProducts);
        }

        [HttpPost]
        public IActionResult Update(ShoppingListProductUpdateModel input, int id)
        {
            var shoppingListProducts = _userDbContext.ShoppingListProducts.SingleOrDefault(x => x.Id == id);    

            shoppingListProducts.Amount = input.Amount;
            shoppingListProducts.Description = input.Description;

            _userDbContext.Update(shoppingListProducts);
            _userDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var shoppingListProductsCheck = _userDbContext.ShoppingListProducts.SingleOrDefault(x => x.Id == id);

            if(shoppingListProductsCheck != null)
            {
                _userDbContext.Remove(shoppingListProductsCheck);
                _userDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
