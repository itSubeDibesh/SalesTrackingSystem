using DataAccessLayer;
using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class Users_Service : Users_Interface
    {
        public string CheckLogin(string email, string password)
        {
           using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actions in _dbContext.Users.Where(actions => actions.Email == email && actions.PasswordHash == password)
                                select new Users_Model()
                                {
                                    FullName=actions.FullName,
                                    Email=actions.Email,
                                    PasswordHash=actions.PasswordHash,
                                    UsersStatus=actions.UsersStatus
                                }).FirstOrDefault();
                    if (data.UsersStatus == 1 && data.Email==email && data.PasswordHash==password)
                    {
                        return "ValidUserActiveStatus";
                    }
                    else if (data.UsersStatus == 2 && data.Email == email && data.PasswordHash == password)
                    {
                        return "ValidUserInactiveStatus";
                    }
                    else if (data.UsersStatus == 3 && data.Email == email && data.PasswordHash == password)
                    {
                        return "ValidUserBlockedStatus";
                    }
                    else
                    {
                        return "InvalidUser";
                    }
                }
                catch (Exception)
                {
                    return "InvalidUserUnknownStatus";
                }
            }
        }
       
        public string GenerateRandomString(int minLength, int maxLength) 
        {
            string GeneratedString;
            string RegxString = "abcdefghijklmnopqrstuvwxyz@-+ABCDEFGHIJKLMNOPQRSTUVWXYZ$#";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            int stringLength = random.Next(minLength, maxLength + 1);
            while (stringLength-- > 0)
            {
                stringBuilder.Append(RegxString[random.Next(RegxString.Length)]);
            }
            GeneratedString = stringBuilder.ToString();
            return GeneratedString;
        }/*Set Max To 250 Limit from Database*/

        public Users_Model GetModelByToken(string token)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actionUser in _dbContext.Users.Where(actionUser => actionUser.Token == token)
                                      join actionUserProfile in _dbContext.UserProfiles on actionUser.UserProfileID equals actionUserProfile.UserProfileID
                                      select new Users_Model()
                                      {
                                          UserID = actionUser.UserID,
                                          UserProfileID = actionUserProfile.UserProfileID,
                                          DistrubitorID = actionUser.DistrubitorID,
                                          ExeceptionProfile = actionUser.ExeceptionProfile,
                                          FullName = actionUser.FullName,
                                          PasswordHash = actionUser.PasswordHash,
                                          Email = actionUser.Email,
                                          Token = actionUser.Token,
                                          MobileNo = actionUser.MobileNo,
                                          ImageString = actionUser.ImageString,
                                          UsersStatus = actionUser.UsersStatus,
                                          ProfileName = actionUserProfile.ProfileName,
                                          Description = actionUserProfile.Description,
                                          CreatedBy = actionUserProfile.CreatedBy,
                                          UserProfileStatus = actionUserProfile.UserProfileStatus
                                      }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Users_Model UpdateOnLogin(string email, string password, string token = null, byte status = 1)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Users.Where(actionUser => actionUser.Email == email && actionUser.PasswordHash == password).FirstOrDefault();
                    data.Token = token;
                    data.UsersStatus = status;
                    _dbContext.SaveChanges();
                    var returnData = (from actionUser in _dbContext.Users.Where(actionUser => actionUser.Email == email && actionUser.PasswordHash == password)
                                      join actionUserProfile in _dbContext.UserProfiles on actionUser.UserProfileID equals actionUserProfile.UserProfileID                                     
                                      select new Users_Model()
                                      {
                                          UserID=actionUser.UserID,
                                          UserProfileID=actionUserProfile.UserProfileID,
                                          DistrubitorID=actionUser.DistrubitorID,
                                          ExeceptionProfile=actionUser.ExeceptionProfile,
                                          FullName=actionUser.FullName,
                                          PasswordHash=actionUser.PasswordHash,
                                          Email=actionUser.Email,
                                          Token=actionUser.Token,
                                          MobileNo=actionUser.MobileNo,
                                          ImageString=actionUser.ImageString,
                                          UsersStatus=actionUser.UsersStatus,
                                          ProfileName = actionUserProfile.ProfileName,
                                          Description=actionUserProfile.Description,
                                          CreatedBy=actionUserProfile.CreatedBy,
                                          UserProfileStatus=actionUserProfile.UserProfileStatus
                                      }).FirstOrDefault();
                    return returnData;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
