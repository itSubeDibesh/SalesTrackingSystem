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
        public Verification_Model checkReset(long userId, string resetToken)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from verificationAction in _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId && verificationAction.ResetToken == resetToken)
                                select new Verification_Model()
                                {
                                    VerificationID = verificationAction.VerificationID,
                                    UserID = verificationAction.UserID,
                                    ResetTriggered = verificationAction.ResetTriggered,
                                    ResetToken = verificationAction.ResetToken,
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

        public bool CheckReset(long userId, string resetToken)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from verificationAction in _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId && verificationAction.ResetToken == resetToken)
                                select new Verification_Model()
                                {
                                    VerificationID = verificationAction.VerificationID,
                                    UserID = verificationAction.UserID,
                                    ResetToken = verificationAction.ResetToken
                                }).FirstOrDefault();
                    if (data.UserID == userId && data.ResetToken == resetToken)
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

        public Verification_Model checkVerification(long userId, string verifiedtoken)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from verificationAction in _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId && verificationAction.VerifiedToken == verifiedtoken)
                                select new Verification_Model()
                                {
                                    VerificationID = verificationAction.VerificationID,
                                    UserID = verificationAction.UserID,
                                    IsVerified = verificationAction.IsVerified,
                                    DateVerified = verificationAction.DateVerified,
                                    VerifiedToken = verificationAction.VerifiedToken,
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

        public bool DeleteVerification(long userId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Verifications.Where(act => act.UserID == userId).FirstOrDefault();
                    _context.Verifications.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public long GetNewVerificationId()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Verifications.Max(u => u.VerificationID);
                    Int64 id = Convert.ToInt64(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
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

        public bool updateResetAuthentication(Int64 userId, DateTime dateTriggered, string resetToken)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId).FirstOrDefault();
                    data.ResetTriggered = dateTriggered;
                    data.ResetToken = resetToken;
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

        public bool VerificationExists(long userId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from verificationAction in _dbContext.Verifications.Where(verificationAction => verificationAction.UserID == userId)
                                select new Verification_Model()
                                {
                                    VerificationID = verificationAction.VerificationID,
                                    UserID = verificationAction.UserID,
                                    ResetToken = verificationAction.ResetToken
                                }).FirstOrDefault();
                    if (data.UserID == userId)
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
    }
}