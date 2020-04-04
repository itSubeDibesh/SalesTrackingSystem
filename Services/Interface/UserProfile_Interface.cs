using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface UserProfile_Interface
    {
        List<UserProfile_Model> DisplayTable();
        bool SaveUserProfile(UserProfile_Model userProfile);
        bool UserProfileExists(Int64 userProfileId);
        UserProfile_Model UserProfilByID(Int64 userProfileId);
        bool UpdateUserProfile(UserProfile_Model userProfile);
        bool DeleteUserProfile(Int64 userProfileId);
        List<UserAuth_Model> AuthDetailsByUserID(long userId);
    }
}
