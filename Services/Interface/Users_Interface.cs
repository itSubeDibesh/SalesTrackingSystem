using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Users_Interface
    {
        string CheckLogin(string email, string password);
        string GenerateRandomString(int minLength=20,int maxLength=230);/*Set Max To 250 Limit from Database*/
        Users_Model UpdateOnLogin(string email, string password, string token=null, int status = 1);
        Users_Model GetModelByToken(string token);
        bool UpdateOnLogout(string email, string password, string token = null, int status = 2);
        Users_Model GetModelById(Int64 userID);
        Users_Model GetModelByEmail(string email);
        bool checkEmail(string email);
        bool resetpassword(Int64 userId, string password);
        List<Users_Model> DisplayTable();
        string GeneratePassword(int minLength = 8, int maxLength = 15);
        string GenerateRandomNumber(int minLength = 2, int maxLength = 8);
        bool SaveUserAccount(Users_Model users_Model);
        Int64 GetNewUserId();
        bool checkMobileNo(long number);
        bool UserExists(Int64 userId);
        bool UpdateUserAccount(Users_Model users_Model);
        bool MakeDistrubitorNull(Int64 userId);
        bool DeleteUser(Int64 userId);
        Users_Model GetModelOnlyById(Int64 userID);
        bool CheckNewAccount(string email, string password);
    }
}
