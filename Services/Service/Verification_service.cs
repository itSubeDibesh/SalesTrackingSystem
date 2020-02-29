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
    public class Verification_service : Verification_Interface
    {
        public Verification_Model checkVerification(long userId, string verifiedtoken)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {                          
                    var data = (from verificationAction in _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId && verificationAction.VerifiedToken == verifiedtoken)                                    
                                      select new Verification_Model()
                                      {
                                          VerificationID= verificationAction.VerificationID,
                                          UserID= verificationAction.UserID,
                                          IsVerified= verificationAction.IsVerified,
                                          DateVerified= verificationAction.DateVerified,
                                          VerifiedToken= verificationAction.VerifiedToken,                                        
                                          DateCreated = verificationAction.DateCreated,
                                          DateUpdated = verificationAction.DateUpdated
                                      }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool updateCheckedVerification(long userId, byte isVerified)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId).FirstOrDefault();                   
                    data.IsVerified = Convert.ToBoolean(isVerified);                  
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool updateVerificationAuthentacitation(long userId, byte isVerified, DateTime dateVerified, string verifiedtoken)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId).FirstOrDefault();
                    data.VerifiedToken = verifiedtoken;
                    data.IsVerified = Convert.ToBoolean(isVerified);
                    data.DateVerified = dateVerified;
                   _dbContext.SaveChanges();                 
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
