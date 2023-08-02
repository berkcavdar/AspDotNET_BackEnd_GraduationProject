using _00_LoginPage.Context;
using _00_LoginPage.Models;
using _00_LoginPage.Models.InsertModel.Models;
using _00_LoginPage.Models.UpdateModel.Models;
using _00_LoginPage.ViewModeels;
using _00_LoginPage.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace _00_LoginPage.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingDbContext _userDbContext;
        public ProductController(ShoppingDbContext dbContext)
        {
            _userDbContext = dbContext;
        }
        [HttpGet("Admin/Product")]
        [Authorize(Roles = "admin")]
        public IActionResult AdminProducts([FromQuery(Name = "categoryid")] int categoryId)
        {
            IReadOnlyList<ProductViewModel> ProductView = (from product in _userDbContext.Products
                                                           where ( (categoryId == 0 || product.CategoryId == categoryId))
                                                           select new ProductViewModel()
                                                           {
                                                             Id = product.Id,
                                                             Name = product.Name,
                                                             Color = product.Color,
                                                             Description = product.Description,
                                                             ImageUrl = product.ImageUrl,
                                                             Price = product.Price,
                                                             Category = new CategoryViewModel()
                                                             {
                                                                 Name = product.Category.Name,
                                                                 Id = product.Category.Id,
                                                             }
                                                           }).ToList();
            return View(ProductView);
        }

        [HttpGet("Product")]
        [Authorize(Roles = "user")]
        public IActionResult UserProducts([FromQuery(Name = "categoryid")] int categoryId, [FromQuery(Name ="shoppinglistid")] int shoppinglistId)
        {
            IReadOnlyList<ProductViewModel> ProductView = (from product in _userDbContext.Products
                                                           where ((categoryId == 0 || product.CategoryId == categoryId))
                                                           select new ProductViewModel()
                                                           {
                                                               Id = product.Id,
                                                               Name = product.Name,
                                                               Color = product.Color,
                                                               Description = product.Description,
                                                               ImageUrl = product.ImageUrl,
                                                               Price = product.Price,
                                                               ShoppingListProductId = shoppinglistId,
                                                               Category = new CategoryViewModel()
                                                               {
                                                                   Name = product.Category.Name,
                                                                   Id = product.Category.Id,
                                                               }
                                                           }).ToList();
            return View(ProductView);
        }

        //Admin Controller
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _userDbContext.Categories.Select( x => new CategoryViewModel() { 
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var model = new ProductInsertViewModel();
            model.Categories = categories;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductInsertViewModel input)
        {
            if (!_userDbContext.Categories.Any(c => c.Id == input.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Geçerli bir kategori seçmelisiniz.");
                return View(input);
            }

            var newProduct = new Product(input.Name, input.Description, input.Color, input.Price, input.ImageUrl, input.CategoryId);
            _userDbContext.Products.Add(newProduct);
            _userDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            //Get Product
            var product = _userDbContext.Products.SingleOrDefault(x => x.Id == id);

            // Null Check
            if (product == null)
            {
                throw new NullReferenceException();
            }
            
            ProductUpdateModel productUpdate = new ProductUpdateModel();

            productUpdate.Name = product.Name;
            productUpdate.Description = product.Description;
            productUpdate.Color = product.Color;
            productUpdate.Price = product.Price;
            productUpdate.ImageUrl = product.ImageUrl;

            return View(productUpdate);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateModel input, int id)
        {

            //Get Product
            var product = _userDbContext.Products.SingleOrDefault(x => x.Id == id);

            // Null Check
            if (product == null)
            {
                throw new NullReferenceException();
            }

            product.Name = input.Name;
            product.Price = input.Price;
            product.ImageUrl = input.ImageUrl;
            product.Description = input.Description;
            product.Color = input.Color;

            _userDbContext.Update(product);
            _userDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var productCheck = _userDbContext.Products.SingleOrDefault(x => x.Id == id); 
            if (productCheck != null)
            {
                _userDbContext.Products.Remove(productCheck);
                _userDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
