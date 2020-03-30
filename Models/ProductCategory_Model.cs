using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductCategory_Model
    {
        public long ProductCategoryID { get; set; }

        [Required(ErrorMessage = "Enter Product Category Name")]
        [Display(Name = "Product Category Name")]
        public string ProductCategoryName { get; set; }

        [Display(Name = "Is Sub-Category")]
        public Nullable<bool> IsSubCategory { get; set; }

        [Display(Name = "Sub-Category of")]
        public Nullable<long> SubCategoryOf { get; set; }

        [Required(ErrorMessage = "Please choose category status")]
        [Display(Name = "Category status")]
        public Nullable<bool> CategoryStatus { get; set; }

        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
