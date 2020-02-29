using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public  interface Verification_Interface
    {
        bool updateVerificationAuthentacitation(Int64 userId,Byte isVerified,DateTime dateVerified,string verifiedtoken);
        Verification_Model checkVerification(Int64 userId, string verifiedtoken);
        bool updateCheckedVerification(Int64 userId, Byte isVerified);

    }
}
