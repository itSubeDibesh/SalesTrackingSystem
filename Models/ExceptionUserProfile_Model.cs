using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ExceptionUserProfile_Model
    {
        public long ExceptionProfileID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<long> ModuleID { get; set; }
        public Nullable<long> ModuleActionID { get; set; }
        public Nullable<bool> ExceptionProfileStatus { get; set; }
        public string Description { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
