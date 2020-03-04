using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfileDetails_Model
    {
        public long UserProfileDetailID { get; set; }
        public Nullable<long> UserProfileID { get; set; }
        public Nullable<long> ModuleID { get; set; }
        public Nullable<long> ModuleActionID { get; set; }
        public Nullable<bool> ProfileDetailStatus { get; set; }
        public string Description { get; set; }
        public Nullable<long> CreatedBy { get; set; }

        public string ProfileName { get; set; }
        public string ModuleName { get; set; }
        public string ActionName { get; set; }

        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
