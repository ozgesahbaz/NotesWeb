using System.ComponentModel.DataAnnotations;

namespace WebApplication_Notes.ViewModels.UserModels
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(30, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(60, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [EmailAddress(ErrorMessage = "{0} geçersiz.")]
        [Display(Name = "E-Posta Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "{0} en fazla {1} ve en az {2} karakter olabilir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "{0} en fazla {1} ve en az {2} karakter olabilir.")]
        [Compare(nameof(Password), ErrorMessage = "{0} alanı ile {1} alanı uyuşmuyor.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        public string RePassword { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }

        [Display(Name = "Yönetici")]
        public bool IsAdmin { get; set; }
    }
}
