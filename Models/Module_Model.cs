using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Module_Model
    {
        public long ModuleID { get; set; }

        [Required(ErrorMessage ="Enter Module Name")]
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Enter Controller Name")]
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Required(ErrorMessage = "Enter Module Status")]
        [Display(Name = "Module Status")]
        public Nullable<bool> ModuleStatus { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
