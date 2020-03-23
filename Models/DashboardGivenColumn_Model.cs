using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DashboardGivenColumn_Model
    {
        public long DashboardGivenColumnId { get; set; }

        [Required(ErrorMessage = "Enter Table Name")]
        [Display(Name = "Table Name")]
        public Nullable<long> DashboardTableId { get; set; }
        public string TableName { get; set; }

        [Required(ErrorMessage = "Enter Column Name")]
        [Display(Name = "Column Name")]
        public string ColumnName { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
