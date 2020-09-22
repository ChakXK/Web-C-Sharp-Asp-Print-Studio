using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace print_studio.Models
{
    [Table("ProductType")]
    public partial class ProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductType()
        {
            ColorsProducts = new HashSet<ColorsProduct>();
        }

        public int id { get; set; }

        [StringLength(40)]
        [Required]
        [Display(Name = "Название")]
        public string name { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Материал")]
        public string material { get; set; }

        [Required]
        [Display(Name = "м.кв. ткани")]
        public int? sq_m { get; set; }

        [StringLength(256)]
        [Display(Name = "Описание")]
        public string description { get; set; }

        [StringLength(20)]
        [Display(Name = "Количество дней для пошива")]
        public string days { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [Range(0, 100000)]
        public int? price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ColorsProduct> ColorsProducts { get; set; }
    }
}