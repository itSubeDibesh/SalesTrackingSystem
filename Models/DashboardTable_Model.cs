using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DashboardTable_Model
    {
        public long DashboardTableId { get; set; }

        [Required(ErrorMessage = "Enter Table Name")]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
