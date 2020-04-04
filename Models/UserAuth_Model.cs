using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserAuth_Model
    {
        public long UserProfileDetailID { get; set; }
        public long UserProfileID { get; set; }
        public long ModuleID { get; set; }
        public long ModuleActionID { get; set; }
        public Nullable<bool> ProfileDetailStatus { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public long UserID { get; set; }
        public string ProfileName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public long MobileNo { get; set; }
        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
