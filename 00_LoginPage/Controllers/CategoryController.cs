using _00_LoginPage.Context;
using _00_LoginPage.Models;
using _00_LoginPage.Models.InsertModel.Models;
using _00_LoginPage.Models.UpdateModel.Models;
using _00_LoginPage.ViewModeels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace _00_LoginPage.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShoppingDbContext _userDbContext;
        public CategoryController(ShoppingDbContext dbContext)
        {
            _userDbContext = dbContext;
        }
        public IActionResult Index()
        {
            IReadOnlyList<CategoryViewModel> CategoryView = (from category in _userDbContext.Categories
                                                             join product in _userDbContext.Products
                                                                 on category.Id equals product.Id
                                                             into prd from product in prd.DefaultIfEmpty()
                                                             select new CategoryViewModel()
                                                             {
                                                                 Id = category.Id,
                                                                 Name = category.Name,
                                                                 ImageUrl = category.ImageUrl,
                                                             }).ToList();
                                                            
                                                            

            return View(CategoryView);
        }
        //Admin Controller
        [HttpGet]
        public IActionResult Create()
        {
            CategoryInsertModel category = new CategoryInsertModel();
            return View(category);
        }

        [HttpPost]
        public IActionResult Create(CategoryInsertModel input)
        {
            var newCategory = new Category(input.Name, input.ImageUrl);

            _userDbContext.Categories.Add(newCategory);
            _userDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var category = _userDbContext.Categories.SingleOrDefault(x => x.Id == id);

            // Null Check
            if (category == null)
            {
                throw new NullReferenceException();
            }

            CategoryUpdateModel categoryUpdate = new CategoryUpdateModel();
            categoryUpdate.Name = category.Name;
            categoryUpdate.ImageUrl = category.ImageUrl;
            return View(categoryUpdate);
        }

        [HttpPost]
        //[Route("/category/update/{id}")]
        public IActionResult Update(CategoryUpdateModel input, int id)
        {
            // Get Category
            var category = _userDbContext.Categories.SingleOrDefault(x => x.Id == id);

            // Null Check
            if(category == null)
            {
                throw new NullReferenceException();
            }

            // Update Category
            category.Name = input.Name;
            category.ImageUrl = input.ImageUrl;

            _userDbContext.Update(category);
            _userDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var categoryCheck = _userDbContext.Categories.FirstOrDefault(x => x.Id == id);
            if(categoryCheck != null)
            {
                _userDbContext.Categories.Remove(categoryCheck);
                _userDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
