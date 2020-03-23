using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DashboardType_Model
    {
        public long DashboardTypeID { get; set; }

        [Required(ErrorMessage = "Enter Type Name")]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
