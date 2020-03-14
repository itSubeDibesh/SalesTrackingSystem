using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Verification_Model
    {
        public long VerificationID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<System.DateTime> DateVerified { get; set; }
        public string VerifiedToken { get; set; }
        public string ResetToken { get; set; }
        public Nullable<System.DateTime> ResetTriggered { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
