using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction_Model
    {
        public long TransactionID { get; set; }
        public byte TransactionLevel { get; set; }
        public long SupplierID { get; set; }

        [Required(ErrorMessage = "Enter Receiver Name")]
        [Display(Name = "Receiver Name")]
        public long ReceiverID { get; set; }
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [Required(ErrorMessage = "Enter Invoice Date")]
        [Display(Name = "Invoice Date")]
        public System.DateTime InvoiceDate { get; set; }
        public Nullable<System.DateTime> InvoiceEntryDate { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }

        [Display(Name = "Discount %")]
        public Nullable<decimal> DiscountPercent { get; set; }

        [Display(Name = "Tax %")]
        public Nullable<decimal> TaxPercent { get; set; }
        public Nullable<decimal> Balance { get; set; }

        public string Years { get; set; }
        public string Months { get; set; }

    }
  
}
