using System.ComponentModel.DataAnnotations;

namespace _00_LoginPage.ViewModeels
{
    public class UserViewModel
    {
        //Id
        public int Id { get; set; }

        //İsim
        [Required(AllowEmptyStrings = false, ErrorMessage = "Adınızı girmeniz gerekmektedir.")]
        public string FirstName { get; set; }

        //Soyad
        [Required(AllowEmptyStrings = false, ErrorMessage = "Soyadınızı girmeniz gerekmektedir.")]
        public string LastName { get; set; }

        //E-mail
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-Mail girmeniz gerekmektedir.")]
        public string Email { get; set; }

        //Şifre
        [Required(AllowEmptyStrings = false, ErrorMessage = "Parolanızı girmeniz gerekmektedir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //Şifre Onayı
        [Required(AllowEmptyStrings = false, ErrorMessage = "Parola Onayını tekrar girmeniz gerekmektedir.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifre Onayı, şifre ile uyumlu olmak zorundadır.")]
        public string ConfirmPassword { get; set; }

        //Oluşturulduğu Tarih
        //public DateTime CreatedOn { get; set; }

        //public string? SuccessMessage { get; set; }

    }
}
