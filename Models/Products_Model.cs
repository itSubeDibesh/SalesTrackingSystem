using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Products_Model
    {
        public long ProductID { get; set; }

        [Required(ErrorMessage = "Choose Category Name")]
        [Display(Name = "Product Category Name")]
        public Nullable<long> ProductCategoryID { get; set; }

        [Required(ErrorMessage = "Enter Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Pack Rate")]
        [Display(Name = "Pack Rate")]
        public decimal PackRate { get; set; }

        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }

        [Required(ErrorMessage = "Enter Unit")]
        [Display(Name = "Unit per quantity")]
        public Nullable<long> UnitId { get; set; }

        public string UnitAbb { get; set; }

        public string ProductCategoryName { get; set; }

    }
}
