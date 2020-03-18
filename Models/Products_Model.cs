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
        public string ProductCategoryName { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Pack Rate")]
        [Display(Name = "Pack Rate")]
        public decimal PackRate { get; set; }
        [Required(ErrorMessage = "Enter Pack Size")]
        [Display(Name = "Pack Size")]
        public decimal PackSize { get; set; }
        public Nullable<long> StockLeft { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }

    }
}
