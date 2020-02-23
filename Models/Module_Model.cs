using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Module_Model
    {
        public long ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public Nullable<bool> ModuleStatus { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
