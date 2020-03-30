using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TransactionDetail_Model
    {
        public long TransactionDetailsID { get; set; }
        public Nullable<long> TransactionID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public decimal Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string ProductName { get; set; }
    }
}
