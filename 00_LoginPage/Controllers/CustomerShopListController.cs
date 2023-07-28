using Microsoft.AspNetCore.Mvc;

namespace _00_LoginPage.Controllers
{
    public class CustomerShopListController : Controller
    {
        public IActionResult Index([FromQuery(Name = "shoopingListId")] int shoppingListId)
        {
            return View();
        }
    }
}
