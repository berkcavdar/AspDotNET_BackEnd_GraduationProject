﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _00_LoginPage.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email doldurulması gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola doldurulması gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
