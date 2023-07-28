using Microsoft.AspNetCore.Mvc;
using _00_LoginPage.Context;
using _00_LoginPage.Models;
using _00_LoginPage.Models.InsertModel.Models;

namespace _00_LoginPage.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ShoppingDbContext _userDbContext;
        public UserController(ILogger<UserController> logger, ShoppingDbContext userDbContext)
        {
            _logger = logger;
            _userDbContext = userDbContext;

        }
        public IActionResult Index()
        {
            IReadOnlyList<ShoppingList> ShoppingList = (from shoplist in _userDbContext.ShoppingLists
                                                     select new ShoppingList()
                                                     {
                                                         Id = shoplist.Id,
                                                         Name = shoplist.Name,
                                                         IsEditable = shoplist.IsEditable,
                                                         UserId = shoplist.UserId,
                                                     }).ToList();

            return View(ShoppingList);
        }

        [HttpPost]
        public IActionResult Create(ShoppingListInsertModel shoppingListInsertModel)
        {
            try
            {
                _userDbContext.ShoppingLists.Add(new ShoppingList()
                {
                    Id = shoppingListInsertModel.UserId,
                    Name = shoppingListInsertModel.Name,
                });
                _userDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
