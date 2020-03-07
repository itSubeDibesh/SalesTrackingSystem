using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users_Model
    {
        public long UserID { get; set; }

        [Required(ErrorMessage = "Select Profile Name")]
        [Display(Name = "Profile Name")]
        public Nullable<long> UserProfileID { get; set; }
      
        [Display(Name = "Distributor Name")]
        public Nullable<long> DistrubitorID { get; set; }
        public Nullable<bool> ExeceptionProfile { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<bool> UserProfileStatus { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        [StringLength(20,ErrorMessage ="Password Must Be between 8 and 20 characters",MinimumLength =8)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage ="Email is required!")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        public long VerificationID { get; set; }   
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<System.DateTime> DateVerified { get; set; }
        public string VerifiedToken { get; set; }
        public string ResetToken { get; set; }
        public Nullable<System.DateTime> ResetTriggered { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [Display(Name = "Mobile Number")]
        public long MobileNo { get; set; }

        [Required(ErrorMessage = "User Image is necessary")]
        [Display(Name = "Image")]
        public string ImageString { get; set; }

        [Required(ErrorMessage = "User Status is required")]
        [Display(Name = "User Status")]
        public Nullable<byte> UsersStatus { get; set; }

        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }


     
      
      
    }
}
