using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Notes.Entities
{
    //CATEGORİES
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name="Kategori Adı")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Kategori Açıklama")]
        public string Desc { get; set; }

        [Display(Name = "Gizli")]
        public bool IsHidden { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreatedAt { get; set; } // 01.01.0001
    }
}
