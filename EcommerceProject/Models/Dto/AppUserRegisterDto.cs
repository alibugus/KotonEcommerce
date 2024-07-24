using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Models.Dto
{
    public class AppUserRegisterDto
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçemezsiniz")]
        public string? UserName { get; set; }

        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Adınızı Boş Geçemezsiniz")]
        public string? FirstName { get; set; }

        [Display(Name = "Soy Adınız")]
        [Required(ErrorMessage = "Soy Adınızı Boş Geçemezsiniz")]
        public string? LastName { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Şehir Boş Geçemezsiniz")]
        public string? City { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Boş Geçemezsiniz")]
        [EmailAddress(ErrorMessage = "Geçersiz Email Adresi")]
        public string? Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Telefon Numarası Boş Geçemezsiniz")]
        [Phone(ErrorMessage = "Geçersiz Telefon Numarası")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Boş Geçemezsiniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre Tekrar Boş Geçemezsiniz")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
    }
}
