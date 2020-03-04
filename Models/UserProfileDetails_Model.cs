using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfileDetails_Model
    {
        public long UserProfileDetailID { get; set; }

        [Required(ErrorMessage = "Select Profile Name")]
        [Display(Name = "Profile Name")]
        public Nullable<long> UserProfileID { get; set; }

        [Required(ErrorMessage = "Select Module Name")]
        [Display(Name = "Module Name")]
        public Nullable<long> ModuleID { get; set; }

        [Required(ErrorMessage = "Select Action Name")]
        [Display(Name = "Action Name")]
        public Nullable<long> ModuleActionID { get; set; }

        [Required(ErrorMessage = "Select Detail Status")]
        [Display(Name = "Detail Status")]
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
