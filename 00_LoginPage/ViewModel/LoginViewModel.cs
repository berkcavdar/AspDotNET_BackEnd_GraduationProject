using _00_LoginPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _00_LoginPage.ViewModeels
{
    public class LoginViewModel 
    {
        [Required(ErrorMessage = "Email doldurulması gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola doldurulması gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
