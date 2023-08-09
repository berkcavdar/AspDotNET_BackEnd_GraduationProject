using _00_LoginPage.Context;
using _00_LoginPage.Models;
using _00_LoginPage.Models.InsertModel.Models;
using _00_LoginPage.Models.UpdateModel.Models;
using _00_LoginPage.ViewModeels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace _00_LoginPage.Controllers
{
    public class ShoppingListProductController : Controller
    {
        private readonly ShoppingDbContext _userDbContext;

        public ShoppingListProductController(ShoppingDbContext context)
        {
            _userDbContext = context;
        }

        public IActionResult Index([FromQuery(Name = "shoppinglistid")] int shoppingListId, [FromQuery(Name ="searching")] string searching, [FromQuery(Name ="category")] string category)
        {
            IReadOnlyList<ShoppingListProductViewModel> shoppingListProducts = _userDbContext.ShoppingListProducts.Where(x => x.ShoppingListId == shoppingListId)
                .Select(x => new ShoppingListProductViewModel()
            {
                Id = x.Id,
                Amount = x.Amount,
                Name = x.Product.Name,
                ImageUrl = x.Product.ImageUrl,
                IsAddedToCart = x.IsAddedToCart,
                ShoppingListId = x.ShoppingListId,
                Product = new ProductViewModel()
                {
                    Id = x.Id,
                    ImageUrl = x.Product.ImageUrl,
                    Color = x.Product.Color,
                    Description = x.Description,
                    Price = x.Product.Price,
                    Category = new CategoryViewModel()
                    {
                        Name = x.Product.Category.Name,
                        Id = x.Id,
                    }
                }

            }).ToList();


            if (!String.IsNullOrEmpty(searching))
            {
                shoppingListProducts = shoppingListProducts.Where(x => x.Name.ToLower().Contains(searching.ToLower())).ToList();
            }

            if(!String.IsNullOrEmpty(category))
            {
                shoppingListProducts = shoppingListProducts.Where(x => x.Product.Category.Name.ToLower().Contains(category.ToLower())).ToList();
            }

            return View(shoppingListProducts);

        }

        [HttpPost]
        public ActionResult IsAddedToTrue(int id)
        {
            var isAddedTocart = _userDbContext.ShoppingListProducts.SingleOrDefault(x => x.Id == id);
            isAddedTocart.IsAddedToCart = true;
            _userDbContext.ShoppingListProducts.Update(isAddedTocart);
            _userDbContext.SaveChanges();
            return RedirectToAction(nameof(Index),new {shoppinglistid = isAddedTocart.ShoppingListId});
        }

        [HttpGet]
        public IActionResult Create([FromQuery(Name ="productid")] int productId, [FromQuery(Name ="shoppinglistid")] int shoppingListId)
        {
            var model = new ShoppingListProductInsertModel() { ProductId = productId, ShoppingListId = shoppingListId};


            if (model == null)
            {
                throw new NullReferenceException();
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Create(ShoppingListProductInsertModel input, int id)
        {
            var newShoppingListProducts = new ShoppingListProduct();
            var isEditableCheck = _userDbContext.ShoppingLists.SingleOrDefault(x => x.Id == input.ShoppingListId);

            if (newShoppingListProducts == null)
            {
                throw new NullReferenceException();
            }

            if(isEditableCheck.IsEditable == true)
            {
                newShoppingListProducts.ShoppingListId = input.ShoppingListId;
                newShoppingListProducts.Amount = input.Amount;
                newShoppingListProducts.ProductId = input.ProductId;
                newShoppingListProducts.Description = input.Description;

                _userDbContext.ShoppingListProducts.Add(newShoppingListProducts);
                _userDbContext.SaveChanges();

                return this.RedirectToAction("Index", new { shoppinglistid = input.ShoppingListId });
            }
            else
            {
                throw new Exception("İsteğiniz Reddedildi");
            }

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

            return RedirectToAction("Index", new {shoppinglistid = shoppingListProductsCheck.ShoppingListId});
        }
    }
}
