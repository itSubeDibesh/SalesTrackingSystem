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
    public class UserProfileDetails_Service : UserProfileDetails_Interface
    {
        public bool DeleteUserProfileDetails(long userProfileDetailsId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.UserProfileDetails.Where(userProfileDetails => userProfileDetails.UserProfileDetailID == userProfileDetailsId).FirstOrDefault();
                    _context.UserProfileDetails.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<UserProfileDetails_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from userProfileDetails in _dbContext.UserProfileDetails
                                join userProfile in _dbContext.UserProfiles on userProfileDetails.UserProfileID equals userProfile.UserProfileID
                                join moduleS in _dbContext.Modules on userProfileDetails.ModuleID equals moduleS.ModuleID
                                join moduleActionStatus in _dbContext.ModuleActions on userProfileDetails.ModuleActionID equals moduleActionStatus.ModuleActionID
                                select new UserProfileDetails_Model()
                                {
                                    UserProfileDetailID= userProfileDetails.UserProfileDetailID,
                                    UserProfileID= userProfileDetails.UserProfileID,
                                    ModuleID= userProfileDetails.ModuleID,
                                    ModuleActionID= userProfileDetails.ModuleActionID,
                                    ProfileDetailStatus= userProfileDetails.ProfileDetailStatus,
                                    Description= userProfileDetails.Description,
                                    CreatedBy= userProfileDetails.CreatedBy,
                                    ProfileName= userProfile.ProfileName,
                                    ModuleName = moduleS.ModuleName,
                                    ActionName = moduleActionStatus.ActionName
                                }).ToList().OrderBy(userProfile=> userProfile.ProfileName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool SaveUserProfileDetails(UserProfileDetails_Model userProfileDetails)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new UserProfileDetail()
                    {
                        UserProfileDetailID = userProfileDetails.UserProfileDetailID,
                        UserProfileID = userProfileDetails.UserProfileID,
                        ModuleID = userProfileDetails.ModuleID,
                        ModuleActionID = userProfileDetails.ModuleActionID,
                        ProfileDetailStatus = userProfileDetails.ProfileDetailStatus,
                        Description = userProfileDetails.Description,
                        CreatedBy = userProfileDetails.CreatedBy,
                        DateCreated = DateTime.Now
                      
                    };
                    _dbContext.UserProfileDetails.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool UpdateUserProfileDetails(UserProfileDetails_Model userProfileDetails)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.UserProfileDetails.Where(userProfileDetail => userProfileDetail.UserProfileDetailID == userProfileDetails.UserProfileDetailID).FirstOrDefault();
                    data.UserProfileDetailID = userProfileDetails.UserProfileDetailID;
                    data.UserProfileID = userProfileDetails.UserProfileID;
                    data.ModuleID = userProfileDetails.ModuleID;
                    data.ModuleActionID = userProfileDetails.ModuleActionID;
                    data.ProfileDetailStatus = userProfileDetails.ProfileDetailStatus;
                    data.Description = userProfileDetails.Description;
                    data.DateUpdated = DateTime.Now;
                    data.CreatedBy = userProfileDetails.CreatedBy;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public UserProfileDetails_Model UserProfilDetailsByID(long userProfileDetailsId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from userProfileDetails in _dbContext.UserProfileDetails.Where(userProfileDetails => userProfileDetails.UserProfileDetailID == userProfileDetailsId)
                                select new UserProfileDetails_Model()
                                {
                                    UserProfileDetailID = userProfileDetails.UserProfileDetailID,
                                    UserProfileID = userProfileDetails.UserProfileID,
                                    ModuleID = userProfileDetails.ModuleID,
                                    ModuleActionID = userProfileDetails.ModuleActionID,
                                    ProfileDetailStatus = userProfileDetails.ProfileDetailStatus,
                                    Description = userProfileDetails.Description,
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UserProfileDetailsExists(long userProfileDetailsId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from userProfileDetails in _dbContext.UserProfileDetails.Where(userProfileDetails => userProfileDetails.UserProfileDetailID == userProfileDetailsId)
                                select new UserProfileDetails_Model()
                                {
                                    UserProfileDetailID = userProfileDetails.UserProfileDetailID,
                                    UserProfileID = userProfileDetails.UserProfileID

                                }).FirstOrDefault();
                    if (userProfileDetailsId != data.UserProfileDetailID)
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
