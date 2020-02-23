using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModuleAction_Model
    {
        public long ModuleActionID { get; set; }
        public Nullable<long> ModuleID { get; set; }
        public string ActionName { get; set; }
        public Nullable<bool> ActionStatus { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
