using System.ComponentModel.DataAnnotations;

namespace WebApplication_Notes.ViewModels.UserModels
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(30, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "{0} en fazla {1} ve en az {2} karakter olabilir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
