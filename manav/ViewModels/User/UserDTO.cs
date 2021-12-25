using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manav.ViewModels
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Giriniz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz.")]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz.")]
        [Display(Name = "Soyad")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "E-mail alanı boş bırakılamaz!")]
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "E-mail adresiniz doğru formatta değil.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefon Numarası boş bırakılamaz!")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz!")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
