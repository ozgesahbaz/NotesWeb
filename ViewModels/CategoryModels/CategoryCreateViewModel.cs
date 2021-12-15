using System.ComponentModel.DataAnnotations;

namespace WebApplication_Notes.ViewModels.CategoryModels
{
    public abstract class CategoryCreateEditViewModelBase
    {
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(40, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [Display(Name = "Kategori Açıklama")]
        public string Desc { get; set; }
    }

    public class CategoryCreateViewModel : CategoryCreateEditViewModelBase
    {
    }

    public class CategoryEditViewModel : CategoryCreateEditViewModelBase
    {
        [Display(Name = "Gizli")]
        public bool IsHidden { get; set; }
        public bool NotFound { get; set; }
    }
}
