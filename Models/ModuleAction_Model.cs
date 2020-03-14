using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModuleAction_Model
    {
        public long ModuleActionID { get; set; }

        [Required(ErrorMessage = "Select Module Name")]
        [Display(Name = "Module Name")]
        public Nullable<long> ModuleID { get; set; }

        [Required(ErrorMessage = "Enter Action Name")]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

        [Required(ErrorMessage = "Select Action Status")]
        [Display(Name = "Action Status")]
        public Nullable<bool> ActionStatus { get; set; }

        public string Description { get; set; }

        public string ModuleName { get; set; }
        public Nullable<bool> ModuleStatus { get; set; }
        public string ControllerName { get; set; }

        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
