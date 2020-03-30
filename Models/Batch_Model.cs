using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Batch_Model
    {
        public long BatchID { get; set; }

        [Required(ErrorMessage = "Enter Batch Name")]
        [Display(Name = "Batch Name")]
        public string BatchName { get; set; }

        [Required(ErrorMessage = "Choose Category Name")]
        [Display(Name = "Product Category Name")]
        public Nullable<long> ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Enter quantity produced")]
        [Display(Name = "Qunatity")]
        public decimal QunatityProduced { get; set; }

        [Required(ErrorMessage = "Enter unit price")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Enter stock")]
        [Display(Name = "Stock")]
        public Nullable<decimal> StockLeft { get; set; }

        [Required(ErrorMessage = "Enter date produced")]
        [Display(Name = "Date Produced")]
        public string DateProduced { get; set; }

        [Required(ErrorMessage = "Enter expiry date")]
        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }

        [Required(ErrorMessage = "Choose Product Name")]
        [Display(Name = "Product  Name")]
        public Nullable<long> ProductID { get; set; }

        public string ProductCategoryName { get; set; }
        public string ProductName { get; set; }

    }
}
