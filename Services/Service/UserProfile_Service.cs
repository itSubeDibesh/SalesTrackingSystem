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
    public class UserProfile_Service : UserProfile_Interface
    {
        public List<UserAuth_Model> AuthDetailsByUserID(long userId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {           
                    var data = (from UPD in _dbContext.UserProfileDetails
                                join UP in _dbContext.UserProfiles on UPD.UserProfileID equals UP.UserProfileID
                                join U in _dbContext.Users.Where(u=>u.UserID==userId) on UP.UserProfileID equals U.UserProfileID
                                join M in _dbContext.Modules on UPD.ModuleID equals M.ModuleID
                                join MA in _dbContext.ModuleActions on UPD.ModuleActionID equals MA.ModuleActionID                               
                                select new UserAuth_Model()
                                {
                                   UserProfileDetailID=UPD.UserProfileDetailID,
                                   UserProfileID=UPD.UserProfileID.Value,
                                   ModuleID = UPD.ModuleID.Value,
                                   ModuleActionID=UPD.ModuleActionID.Value,
                                   ProfileDetailStatus=UPD.ProfileDetailStatus,
                                   CreatedBy=UPD.CreatedBy,
                                   ProfileName=UP.ProfileName,
                                   FullName=U.FullName,
                                   Email=U.Email,
                                   MobileNo=U.MobileNo,
                                   ModuleName=M.ModuleName,
                                   ControllerName=M.ControllerName,
                                   ActionName=MA.ActionName                                  
                                }).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool DeleteUserProfile(long userProfileId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.UserProfiles.Where(UserProfiles => UserProfiles.UserProfileID == userProfileId).FirstOrDefault();
                    _context.UserProfiles.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<UserProfile_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.UserProfiles.Select(userProfile => new UserProfile_Model()
                    {
                        UserProfileID = userProfile.UserProfileID,
                        ProfileName = userProfile.ProfileName,
                        UserProfileStatus = userProfile.UserProfileStatus,
                        CreatedBy = userProfile.CreatedBy,
                        Description = userProfile.Description
                    }).ToList().OrderBy(userProfile => userProfile.ProfileName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool SaveUserProfile(UserProfile_Model userProfile)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new UserProfile()
                    {
                        ProfileName = userProfile.ProfileName,
                        UserProfileStatus = userProfile.UserProfileStatus,
                        Description = userProfile.Description,
                        DateCreated = DateTime.Now
                       //CreatedBy = userProfile.CreatedBy
                    };
                    _dbContext.UserProfiles.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool UpdateUserProfile(UserProfile_Model userProfile)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.UserProfiles.Where(userProfiles => userProfiles.UserProfileID == userProfile.UserProfileID).FirstOrDefault();
                    data.ProfileName = userProfile.ProfileName;
                    data.UserProfileStatus = userProfile.UserProfileStatus;                  
                    data.Description = userProfile.Description;
                    //data.CreatedBy = userProfile.CreatedBy;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public UserProfile_Model UserProfilByID(long userProfileId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from userProfile in _dbContext.UserProfiles.Where(userProfiles => userProfiles.UserProfileID == userProfileId)
                                select new UserProfile_Model()
                                {
                                    UserProfileID = userProfile.UserProfileID,
                                    ProfileName = userProfile.ProfileName,
                                    UserProfileStatus = userProfile.UserProfileStatus,
                                    CreatedBy = userProfile.CreatedBy,
                                    Description = userProfile.Description
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UserProfileExists(long userProfileId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from userProfile in _dbContext.UserProfiles.Where(userProfile => userProfile.UserProfileID == userProfileId)
                                select new UserProfile_Model()
                                {
                                    UserProfileID= userProfile.UserProfileID,
                                    ProfileName= userProfile.ProfileName                                  

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.ProfileName) && userProfileId != data.UserProfileID)
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
