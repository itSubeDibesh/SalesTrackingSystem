using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface UserProfileDetails_Interface
    {
        List<UserProfileDetails_Model> DisplayTable();
        bool SaveUserProfileDetails(UserProfileDetails_Model userProfileDetails);
        bool UserProfileDetailsExists(Int64 userProfileDetailsId);
        UserProfileDetails_Model UserProfilDetailsByID(Int64 userProfileDetailsId);
        bool UpdateUserProfileDetails(UserProfileDetails_Model userProfileDetails);
        bool DeleteUserProfileDetails(Int64 userProfileDetailsId);
    }
}
