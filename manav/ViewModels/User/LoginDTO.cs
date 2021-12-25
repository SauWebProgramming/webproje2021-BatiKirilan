using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manav.ViewModels
{
    public class LoginDTO
    {
        [Display(Name = "E-mail Adresiniz")]
        [Required(ErrorMessage = "E-mail alanı gereklidir.")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Şifreniz")]
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Şifreniz en az 4 karakter olmalıdır.")]
        public string Password { get; set; }
    }
}
