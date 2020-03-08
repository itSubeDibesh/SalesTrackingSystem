using DataAccessLayer;
using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class Users_Service : Users_Interface
    {
        public bool checkEmail(string email)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actions in _dbContext.Users.Where(actions => actions.Email == email)
                                select new Users_Model()
                                {
                                    FullName = actions.FullName,
                                    Email = actions.Email,
                                    PasswordHash = actions.PasswordHash,
                                    UsersStatus = actions.UsersStatus
                                }).FirstOrDefault();
                    if (data.Email == email)
                    {
                        return true;
                    }                   
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

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

        public bool checkMobileNo(long number)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actions in _dbContext.Users.Where(actions => actions.MobileNo == number)
                                select new Users_Model()
                                {
                                    FullName = actions.FullName,
                                    Email = actions.Email,
                                    PasswordHash = actions.PasswordHash,
                                    UsersStatus = actions.UsersStatus
                                }).FirstOrDefault();
                    if (data.MobileNo == number)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeleteUser(long userId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Users.Where(act => act.UserID == userId).FirstOrDefault();
                    _context.Users.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Users_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {           
                    var data = (from Users in _dbContext.Users
                                join actionUserProfile in _dbContext.UserProfiles on Users.UserProfileID equals actionUserProfile.UserProfileID                              
                                select new Users_Model()
                                {
                                    UserID = Users.UserID,
                                    UserProfileID = Users.UserProfileID,
                                    DistrubitorID = Users.DistrubitorID,
                                    ExeceptionProfile = Users.ExeceptionProfile,
                                    FullName = Users.FullName,
                                    PasswordHash = Users.PasswordHash,
                                    Email = Users.Email,
                                    Token = Users.Token,
                                    MobileNo = Users.MobileNo,
                                    ImageString = Users.ImageString,
                                    UsersStatus = Users.UsersStatus,
                                    DateCreated = Users.DateCreated,
                                    DateUpdated = Users.DateUpdated,
                                    ProfileName = actionUserProfile.ProfileName,
                                    Description = actionUserProfile.Description,
                                    CreatedBy = actionUserProfile.CreatedBy,
                                    UserProfileStatus = actionUserProfile.UserProfileStatus                                   
                                }).ToList().OrderBy(Users => Users.FullName).ToList();       
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string GeneratePassword(int minLength = 8, int maxLength = 15)
        {
            string GeneratedString;
            string RegxString = "abcdefghijklmnopqrstuvwxyz@ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            int stringLength = random.Next(minLength, maxLength + 1);
            while (stringLength-- > 0)
            {
                stringBuilder.Append(RegxString[random.Next(RegxString.Length)]);
            }
            GeneratedString = stringBuilder.ToString();
            return GeneratedString;
        }

        public string GenerateRandomNumber(int minLength = 2, int maxLength = 8)
        {
            string GeneratedString;
            string RegxString = "0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            int stringLength = random.Next(minLength, maxLength + 1);
            while (stringLength-- > 0)
            {
                stringBuilder.Append(RegxString[random.Next(RegxString.Length)]);
            }
            GeneratedString = stringBuilder.ToString();
            return GeneratedString;
        }

        public string GenerateRandomString(int minLength, int maxLength) 
        {
            string GeneratedString;
            string RegxString = "abcdefghijklmnopqrstuvwxyz@ABCDEFGHIJKLMNOPQRSTUVWXYZ$_123456789";
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

        public Users_Model GetModelByEmail(string email)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actionUser in _dbContext.Users.Where(actionUser => actionUser.Email == email)
                                join actionUserProfile in _dbContext.UserProfiles on actionUser.UserProfileID equals actionUserProfile.UserProfileID
                                join actionVericication in _dbContext.Verifications on actionUser.UserID equals actionVericication.UserID
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
                                    UserProfileStatus = actionUserProfile.UserProfileStatus,
                                    VerificationID = actionVericication.VerificationID,
                                    IsVerified = actionVericication.IsVerified,
                                    VerifiedToken = actionVericication.VerifiedToken,
                                    DateVerified = actionVericication.DateVerified,
                                    ResetToken = actionVericication.ResetToken,
                                    ResetTriggered = actionVericication.ResetTriggered,
                                    DateCreated = actionUser.DateCreated,
                                    DateUpdated = actionUser.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Users_Model GetModelById(long userID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actionUser in _dbContext.Users.Where(actionUser => actionUser.UserID == userID)
                                join actionUserProfile in _dbContext.UserProfiles on actionUser.UserProfileID equals actionUserProfile.UserProfileID
                                join actionVericication in _dbContext.Verifications on actionUser.UserID equals actionVericication.UserID
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
                                    UserProfileStatus = actionUserProfile.UserProfileStatus,
                                    VerificationID = actionVericication.VerificationID,
                                    IsVerified = actionVericication.IsVerified,
                                    VerifiedToken = actionVericication.VerifiedToken,
                                    DateVerified = actionVericication.DateVerified,
                                    ResetToken = actionVericication.ResetToken,
                                    ResetTriggered = actionVericication.ResetTriggered,
                                    DateCreated = actionUser.DateCreated,
                                    DateUpdated = actionUser.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Users_Model GetModelByToken(string token)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actionUser in _dbContext.Users.Where(actionUser => actionUser.Token == token)
                                      join actionUserProfile in _dbContext.UserProfiles on actionUser.UserProfileID equals actionUserProfile.UserProfileID
                                      join actionVericication in _dbContext.Verifications on actionUser.UserID equals actionVericication.UserID
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
                                          UserProfileStatus = actionUserProfile.UserProfileStatus,
                                          VerificationID = actionVericication.VerificationID,
                                          IsVerified = actionVericication.IsVerified,
                                          VerifiedToken = actionVericication.VerifiedToken,
                                          DateVerified = actionVericication.DateVerified,
                                          ResetToken = actionVericication.ResetToken,
                                          ResetTriggered = actionVericication.ResetTriggered,
                                          DateCreated=actionUser.DateCreated,
                                          DateUpdated=actionUser.DateUpdated
                                      }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Users_Model GetModelOnlyById(long userID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from actionUser in _dbContext.Users.Where(actionUser => actionUser.UserID == userID)
                                select new Users_Model()
                                {
                                    UserID = actionUser.UserID,
                                    DistrubitorID = actionUser.DistrubitorID,
                                    ExeceptionProfile = actionUser.ExeceptionProfile,
                                    FullName = actionUser.FullName,
                                    PasswordHash = actionUser.PasswordHash,
                                    Email = actionUser.Email,
                                    Token = actionUser.Token,
                                    MobileNo = actionUser.MobileNo,
                                    ImageString = actionUser.ImageString,
                                    UsersStatus = actionUser.UsersStatus,
                                    DateCreated = actionUser.DateCreated,
                                    DateUpdated = actionUser.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public long GetNewUserId()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Users.Max(u => u.UserID);
                    Int64 id = Convert.ToInt64(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool MakeDistrubitorNull(long userId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Users.Where(a => a.UserID == userId).FirstOrDefault();
                    data.DistrubitorID = null;
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
            }
        }

        public bool resetpassword(long userId, string password)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Users.Where(actionUser => actionUser.UserID == userId).FirstOrDefault();
                    data.PasswordHash = password;
                    data.UsersStatus = 2;
                    data.Token = null;
                    _dbContext.SaveChanges();                   
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool SaveUserAccount(Users_Model users_Model)
        {
           using (var db_Context= new SalesTrackingSystemEntities())
            {
                using (DbContextTransaction db = db_Context.Database.BeginTransaction())
                {
                    try
                    {
                        var UserData = new User()
                        {
                            UserID = GetNewUserId(),
                            UserProfileID = users_Model.UserProfileID,
                            ExeceptionProfile = users_Model.ExeceptionProfile,
                            FullName = users_Model.FullName,
                            PasswordHash = users_Model.PasswordHash,
                            Email = users_Model.Email,
                            MobileNo = users_Model.MobileNo,
                            ImageString = users_Model.ImageString,
                            UsersStatus = users_Model.UsersStatus
                        };
                        db_Context.Users.Add(UserData);
                        db_Context.SaveChanges();
                        var userId = UserData.UserID;
                        Verification_Interface verification_ = new Verification_service();
                        var DataVarification = new Verification()
                        {
                            VerificationID = verification_.GetNewVerificationId(),
                            UserID = userId,
                            IsVerified=false
                        };
                        db_Context.Verifications.Add(DataVarification);
                        db_Context.SaveChanges();
                        db.Commit();
                        return true;
                    }
                    catch (DbEntityValidationException)
                    {
                        db.Rollback();
                        return false;
                    }
                }
            }
        }

        public Users_Model UpdateOnLogin(string email, string password, string token = null, int status = 1)
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
                                      join actionVericication in _dbContext.Verifications on actionUser.UserID equals actionVericication.UserID
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
                                          UserProfileStatus = actionUserProfile.UserProfileStatus,
                                          VerificationID = actionVericication.VerificationID,
                                          IsVerified = actionVericication.IsVerified,
                                          VerifiedToken = actionVericication.VerifiedToken,
                                          DateVerified = actionVericication.DateVerified,
                                          ResetToken = actionVericication.ResetToken,
                                          ResetTriggered = actionVericication.ResetTriggered,
                                          DateCreated = actionUser.DateCreated,
                                          DateUpdated = actionUser.DateUpdated
                                      }).FirstOrDefault();
                    return returnData;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool UpdateOnLogout(string email, string password, string token = null, int status = 2)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Users.Where(a => a.Email == email && a.PasswordHash == password).FirstOrDefault();
                    data.Token = token;
                    data.UsersStatus = status;
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
            }
        }

        public bool UpdateUserAccount(Users_Model users_Model)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Users.Where(act=> act.UserID == users_Model.UserID).FirstOrDefault();
                    data.UserProfileID = users_Model.UserProfileID;
                    data.DistrubitorID = users_Model.DistrubitorID;
                    data.FullName = users_Model.FullName;
                    if (users_Model.UsersStatus==0)
                    {
                        data.PasswordHash = users_Model.PasswordHash;
                        data.Token = users_Model.Token;
                    }
                    data.MobileNo = users_Model.MobileNo;
                    if (users_Model.ImageString!=null)
                    {
                        data.ImageString = users_Model.ImageString;
                    }
                    data.UsersStatus = users_Model.UsersStatus;

                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
            }
        }

        public bool UserExists(long userId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from users in _dbContext.Users.Where(user => user.UserID == userId)
                                select new Users_Model()
                                {
                                    UserID = users.UserID                                  

                                }).FirstOrDefault();
                    if (userId != data.UserID)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
