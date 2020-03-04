using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfile_Model
    {
        public long UserProfileID { get; set; }

        [Required(ErrorMessage = "Select Profile Name")]
        [Display(Name = "Profile Name")]
        public string ProfileName { get; set; }

        [Required(ErrorMessage = "Select Profile Status")]
        [Display(Name = "Profile Status")]
        public Nullable<bool> UserProfileStatus { get; set; }


        public string Description { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }

    }
}
