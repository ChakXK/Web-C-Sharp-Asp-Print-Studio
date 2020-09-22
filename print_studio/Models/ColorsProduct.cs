using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace print_studio.Models
{
    [Table("ColorsProduct")]
    public partial class ColorsProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ColorsProduct()
        {
            SavedProducts = new HashSet<SavedProduct>();
        }

        [Display(Name = "Код цвета")]
        public int id { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Цвет")]
        public string color { get; set; }

        public int? id_producttype { get; set; }

        public int? id_image { get; set; }

        public virtual Image Image { get; set; }

        public virtual ProductType ProductType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavedProduct> SavedProducts { get; set; }
    }
}