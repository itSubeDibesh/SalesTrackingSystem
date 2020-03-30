using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Unit_Model
    {
        public long UnitId { get; set; }

        [Required(ErrorMessage = "Unit name required")]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Required(ErrorMessage = "Unit abbr required")]
        [Display(Name = "Unit (Abbr)")]
        public string UnitAbb { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
