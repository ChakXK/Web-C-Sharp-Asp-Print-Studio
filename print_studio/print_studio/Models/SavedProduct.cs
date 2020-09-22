using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace print_studio.Models
{
    [Table("SavedProduct")]
    public partial class SavedProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SavedProduct()
        {
            PrintOrders = new HashSet<PrintOrder>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? id_colorsproduct { get; set; }

        public int? id_image { get; set; }

        public virtual ColorsProduct ColorsProduct { get; set; }

        public virtual Image Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintOrder> PrintOrders { get; set; }
    }
}