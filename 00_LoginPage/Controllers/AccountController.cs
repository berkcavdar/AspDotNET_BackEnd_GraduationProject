using _00_LoginPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _00_LoginPage.Controllers
{
    public class AccountController : Controller
    {
        UserDbContext userDbContext = new UserDbContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            UserModel userModel = new UserModel();
            return View(userModel);
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
                if (userModel.Password.Any(x => char.IsUpper(x) && char.IsLower(x) && char.IsDigit(x)))
                {
                    
                }
                User user = new User(); 
                user.UserId = userModel.Id;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.CreatedOn = DateTime.Now;
                userDbContext.Users.Add(user);  
                userDbContext.SaveChanges();
                userModel = new UserModel();
                userModel.SuccessMessage = "Başarılı bir şekilde kayıt oldunuz.";
                return View("Register");     
        }

        public IActionResult Login()
        {
            LoginModel login = new LoginModel();
            return View(login);
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if(userDbContext.Users.Where(user => user.Email == login.Email && user.Password == login.Password).FirstOrDefault() != null)
            {
                ModelState.AddModelError("Error", "Email veya paralonızı hatalı girdiniz.");
                return View();
            }
            else
            {
               /* Sess["Email"] = login.Email;*/
             }
            return View();
        }
    }
}
