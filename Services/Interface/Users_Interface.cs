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
        Users_Model UpdateOnLogin(string email, string password, string token=null, Byte status = 1);
        Users_Model GetModelByToken(string token);
        bool UpdateOnLogout(string email, string password, string token = null, byte status = 2);
        Users_Model GetModelById(Int64 userID);
        Users_Model GetModelByEmail(string email);
        bool checkEmail(string email);
        bool resetpassword(Int64 userId, string password);
    }
}
