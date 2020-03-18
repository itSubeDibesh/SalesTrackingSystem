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
        [Display(Name = "IsSubCategory")]
        public Nullable<bool> IsSubCategory { get; set; }
        public Nullable<long> SubCategoryOf { get; set; }
        [Required(ErrorMessage = "Please choose category status")]        
        public Nullable<bool> CategoryStatus { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }

        public bool Update(ProductCategory_Model productCategory)
        {
            throw new NotImplementedException();
        }
    }
}
