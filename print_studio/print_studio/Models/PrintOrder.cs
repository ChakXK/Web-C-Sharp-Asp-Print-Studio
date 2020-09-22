using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace print_studio.Models
{
    [Table("PrintOrder")]
    public partial class PrintOrder
    {
        [Display(Name = "Номер заказа")]
        public int id { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime? date { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Фамилия")]
        public string surname { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Имя")]
        public string name { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Адресс")]
        public string adress { get; set; }

        [StringLength(20)]
        [Display(Name = "Телефон")]
        [Required]
        [RegularExpression(@"((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}", ErrorMessage = "Некорректный номер телефона")]
        public string phone { get; set; }
        [Display(Name = "Размер")]
        public int? id_size { get; set; }
        [Display(Name = "Статус")]
        public int? id_status { get; set; }

        public string id_employee { get; set; }
        [Display(Name = "Код изделия")]
        public int? id_savedproduct { get; set; }

        [Required]
        [Display(Name = "Количество")]
        [Range(0, 100)]
        public int? count { get; set; }
        [Display(Name = "Менеджер")]
        public virtual ApplicationUser Employee { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual Size Size { get; set; }

        public virtual SavedProduct SavedProduct { get; set; }
    }
}