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
        public Nullable<long> UserProfileID { get; set; }
        public Nullable<long> DistrubitorID { get; set; }
        public Nullable<bool> ExeceptionProfile { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<bool> UserProfileStatus { get; set; }
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
        public long MobileNo { get; set; }
        public string ImageString { get; set; }
        public Nullable<byte> UsersStatus { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }


     
      
      
    }
}
