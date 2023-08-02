using _00_LoginPage.Context;
using _00_LoginPage.Models;
using _00_LoginPage.Models.InsertModel.Models;
using _00_LoginPage.ViewModeels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _00_LoginPage.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly ILogger<ShoppingListController> _logger;
        private readonly ShoppingDbContext _userDbContext;
        private readonly string _cookieName;
        public ShoppingListController(ILogger<ShoppingListController> logger, ShoppingDbContext userDbContext)
        {
            _logger = logger;
            _userDbContext = userDbContext;
            _cookieName = ".AspNetCore." + CookieAuthenticationDefaults.AuthenticationScheme;
        }

        public IActionResult Index([FromQuery(Name = "shoppinglistid")] int shoppingListId)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException();
            }

            IReadOnlyList<ShoppingListViewModel> ShoppingList = (from shoppingList in _userDbContext.ShoppingLists
                                                                join shoppingListProduct in _userDbContext.ShoppingListProducts
                                                                    on shoppingList.Id equals shoppingListProduct.ShoppingListId
                                                                into slp from shoppingListProduct in slp.DefaultIfEmpty()
                                                                where shoppingList.UserId == int.Parse(userId.Value)
                                                                select new ShoppingListViewModel()
                                                                {
                                                                    Id = shoppingList.Id,
                                                                    Name = shoppingList.Name,
                                                                    IsEditable = shoppingList.IsEditable,
                                                                    Products = shoppingList.ShoppingListProducts.Select(x => new ShoppingListProductViewModel()
                                                                    {
                                                                        Id = x.Id,
                                                                        Name = x.Name,
                                                                        Amount = x.Amount,
                                                                        IsAddedToCart = x.IsAddedToCart,
                                                                        ShoppingListId = x.ShoppingListId,
                                                                        Product = new ProductViewModel()
                                                                        {
                                                                            Id = x.ProductId,
                                                                            Name = x.Product.Name,
                                                                            Color = x.Product.Color,
                                                                            Description = x.Product.Description,
                                                                            ImageUrl = x.Product.ImageUrl,
                                                                            Price = x.Product.Price,
                                                                            Category = new CategoryViewModel()
                                                                            {
                                                                                Id = x.Product.Category.Id,
                                                                                Name = x.Product.Category.Name,
                                                                            }
                                                                            
                                                                        }
                                                                    }).ToList(),
                                                                }).ToList();

            return View(ShoppingList);
        }

        [HttpPost]
        public IActionResult Index(ShoppingListViewModel shoppingListViewModel)
        {
            var isEditable = shoppingListViewModel.IsEditable;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ShoppingListInsertModel shoppingList = new ShoppingListInsertModel();
            return View(shoppingList);
        }

        [HttpPost]
        public ActionResult Create(ShoppingListInsertModel input)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            
            if ( userId == null )
            {
                throw new ArgumentException();
            }

            var shoppingList = new ShoppingList(input.Name,int.Parse(userId.Value));

            // 2. Save this shopping list to the database
            _userDbContext.ShoppingLists.Add(shoppingList);
            _userDbContext.SaveChanges();

            //Request.Cookies[_cookieName].

            //if (HttpContext.Request.Cookies.TryGetValue(".AspNetCore." + CookieAuthenticationDefaults.AuthenticationScheme, out string? value))
            //{
            //    return View();
            //}


            // 4. Redirect user to the "user/index" page in order to list shooping lists
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var shoppingListCheck = _userDbContext.ShoppingLists.SingleOrDefault(x => x.Id == id);
            if(shoppingListCheck != null)
            {
                _userDbContext.ShoppingLists.Remove(shoppingListCheck); 
                _userDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
