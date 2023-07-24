﻿using _00_LoginPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
            // 1. Validate User Input
            //if ( !(userModel.Password.Any(x => char.IsUpper(x) && char.IsLower(x) && char.IsDigit(x)) && userModel.Password.Length < 8))
            //{
            //    // Send an error message
            //    ModelState.AddModelError(key: "Password", errorMessage: "Password should contain an uppercase, lowercase and at least 8 letters.");
            //    return View(userModel);
            //}

            // 1. Validate User Input - Passoword should containm 1 uppercase, 1 lowercase, 1 digit and at least 8 letters
            
            if (!(userModel.Password.Any(x => char.IsUpper(x)) && userModel.Password.Any(x => char.IsLower(x)) && userModel.Password.Any(x => char.IsDigit(x)) && userModel.Password.Length >= 8))
            {
                // Send an error message
                ModelState.AddModelError(key: "Password", errorMessage: "Password should contain an uppercase, lowercase and at least 8 letters.");
                return View(userModel);
            }

            // 2. Create a new User entity

            User user = new User();

            user.UserId = userModel.Id;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.CreatedOn = DateTime.Now;

            // 3. Save this user to the database
            //userDbContext.Users.Add(user);  
            //userDbContext.SaveChanges();

            // 4. Redirect user to the "account/login" page

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = userDbContext.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

            if(user == null)
            {
                ModelState.AddModelError("Error", "Email veya paralonızı hatalı girdiniz.");
                return View();
            }

            var claims = new List<Claim>
            {
                // TODO: Add role to the user entity then add user role to the  claim 
                //new Claim(ClaimTypes.Role, user.),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Set the expiration time as needed
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                IsPersistent = true,
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return View();
        }
    }
}
