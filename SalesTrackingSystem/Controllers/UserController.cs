using Models;
using Services.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static SalesTrackingSystem.Helpers.AppAuthAttribute;

namespace SalesTrackingSystem.Controllers
{
    [Authorization]
    public class UserController : Controller
    {
        // GET: User
        UserProfile_Interface UserProfile_Interface_;
        UserProfileDetails_Interface UserProfileDetails_;
        Users_Interface Users_Interface_;
        public UserController()
        {
            UserProfile_Interface_ = new UserProfile_Service();
            UserProfileDetails_ = new UserProfileDetails_Service();
            Users_Interface_ = new Users_Service();
        }

        public ActionResult UserProfile()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult UserProfileAdd(UserProfile_Model userProfile_)
        {
            if (string.IsNullOrEmpty(userProfile_.ProfileName) || string.IsNullOrEmpty(userProfile_.Description) || userProfile_.UserProfileStatus == null)
            {
                ViewBag.AddUserProfileError = "Error";
                return View("UserProfile");
            }
            else
            {
                if (UserProfile_Interface_.SaveUserProfile(userProfile_))
                {
                    Session["Success"] = userProfile_.ProfileName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = userProfile_.ProfileName + " couldn't be added please retry!!";
                }
                return RedirectToAction("UserProfile");
            }
        }

        [HttpGet]
        public ActionResult UserProfileEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Profile couldn't be found please retry!!";
                return View("UserProfile");
            }
            else
            {
                if (UserProfile_Interface_.UserProfileExists(uaid))
                {
                    ViewBag.EditUserProfileDropDown = "Drop";
                    return View("UserProfile");
                }
                else
                {
                    Session["Error"] = " Profile couldn't be found please retry!!";
                    return View("UserProfile");
                }
            }
        }

        [HttpPost]
        public ActionResult UserProfileUpdate(UserProfile_Model userProfile_)
        {
            if (string.IsNullOrEmpty(userProfile_.ProfileName) || string.IsNullOrEmpty(userProfile_.Description) || userProfile_.UserProfileStatus == null)
            {
                ViewBag.UpdateUserProfileError = "Error";
                ViewBag.UpdateUserProfileData = userProfile_.UserProfileID;
                return View("UserProfile");
            }
            else
            {
                if (UserProfile_Interface_.UpdateUserProfile(userProfile_))
                {
                    Session["Success"] = userProfile_.ProfileName + " updated successfully!!";
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    Session["Error"] = userProfile_.ProfileName + " couldn't be updated please retry!!";
                    return View("UserProfile");
                }

            }
        }

        [HttpPost]
        public ActionResult UserProfileDelete(UserProfile_Model userProfile_)
        {
            var Module_Name = userProfile_.ProfileName;
            try
            {
                if (UserProfile_Interface_.DeleteUserProfile(userProfile_.UserProfileID))
                {
                    return Json(Module_Name + " profile has been deleted successfully");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception e)
            {
                return Json("Error" + e.ToString());
            }

        }

        [HttpPost]
        public ActionResult UserProfileDetailsAdd(UserProfileDetails_Model userProfileDetails_)
        {
            if (userProfileDetails_.UserProfileID==null || userProfileDetails_.ModuleID==null || userProfileDetails_.ModuleActionID == null)
            {
                ViewBag.AddUserProfileDetailsError = "Error";
                return View("UserProfile");
            }
            else
            {
                if (UserProfileDetails_.SaveUserProfileDetails(userProfileDetails_))
                {
                    Session["Success"] = "Profile details added successfully!!";
                }
                else
                {
                    Session["Error"] = "Profile details couldn't be added please retry!!";
                }
                return RedirectToAction("UserProfile");
            }
        }

        [HttpGet]
        public ActionResult UserProfileDetailsEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = "Profile details couldn't be found please retry!!";
                return View("UserProfile");
            }
            else
            {
                if (UserProfileDetails_.UserProfileDetailsExists(uaid))
                {
                    ViewBag.EditUserProfileDetailsDropDown = "Drop";
                    return View("UserProfile");
                }
                else
                {
                    Session["Error"] = "Profile details couldn't be found please retry!!";
                    return View("UserProfile");
                }
            }
        }

        [HttpPost]
        public ActionResult UserProfileDetailsUpdate(UserProfileDetails_Model userProfileDetails_)
        {
            if ((userProfileDetails_.UserProfileID == null || userProfileDetails_.ModuleID == null || userProfileDetails_.ModuleActionID == null))
            {
                ViewBag.UpdateUserProfileDetailsError = "Error";
                ViewBag.UpdateUserProfileDetailsData = userProfileDetails_.UserProfileDetailID;
                return View("UserProfile");
            }
            else
            {
                if (UserProfileDetails_.UpdateUserProfileDetails(userProfileDetails_))
                {
                    Session["Success"] = "Profile updated successfully!!";
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    Session["Error"] = "Profile couldn't be updated please retry!!";
                    return View("UserProfile");
                }

            }
        }

        [HttpPost]
        public ActionResult UserProfileDetailsDelete(UserProfileDetails_Model userProfileDetails_)
        {          
            try
            {
                if (UserProfileDetails_.DeleteUserProfileDetails(userProfileDetails_.UserProfileDetailID))
                {
                    return Json("Profile details has been deleted successfully");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception e)
            {
                return Json("Error" + e.ToString());
            }

        }

        private string EmailBody(string SubjectTitle, string Subject, string UserName, string Message, string WarningMessage, string AppLink, string CopyrightDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/EmailTemplateNormal.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{SubjectTitle}", SubjectTitle);
            body = body.Replace("{Subject}", Subject);
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{Message}", Message);        
            body = body.Replace("{WarningMessage}", WarningMessage);
            body = body.Replace("{AppLink}", AppLink);
            body = body.Replace("{CopyrightDate}", CopyrightDate);
            return body;
        }
      
        public ActionResult Users()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult UserAdd(Users_Model users_, HttpPostedFileBase ImageString)
        {
            if (string.IsNullOrEmpty(users_.FullName) || string.IsNullOrEmpty(users_.Email) || users_.MobileNo <= 0 || users_.UserProfileID == null)
            {
                ViewBag.AddUserError = "Error";
                return View("Users");
            }
            else
            {
                if (Users_Interface_.checkEmail(users_.Email)==false)
                {
                    if (Users_Interface_.checkMobileNo(users_.MobileNo)==false)
                    {
                        var Datas = new Users_Model();
                        string GeneratedPassword = Users_Interface_.GeneratePassword();
                        string RandomNumber = Users_Interface_.GenerateRandomNumber();
                        var Salt = "SHA1" + users_.Email + "SalesTrackingSystem";
                        var UserPassword = Crypto.SHA1(Salt + GeneratedPassword);

                        string Root = "~/UserInformation";
                        string Email = users_.Email;
                        string FullName = users_.FullName;
                        string RootDir = Server.MapPath(Root);
                        string UserDirectory = Server.MapPath(Root + "/" + Email);
                        string ImageDirectory = Server.MapPath(Root + "/" + Email + "/" + "Images");
                        string FileDirectory = Server.MapPath(Root + "/" + Email + "/" + "Documents");
                        var ImageName = "";

                        if (users_.ImageString != null)
                        {
                            ImageName = RandomNumber + Path.GetExtension(ImageString.FileName).ToString();
                        }

                        Datas.DistrubitorID = users_.DistrubitorID;
                        Datas.UserProfileID = users_.UserProfileID;
                        Datas.FullName = users_.FullName;
                        Datas.PasswordHash = UserPassword;
                        Datas.Email = users_.Email;
                        Datas.MobileNo = users_.MobileNo;
                        Datas.UsersStatus = users_.UsersStatus;
                        Datas.ImageString = "/UserInformation/" + Email + "/" + "Images/" + ImageName;
                        if (Users_Interface_.SaveUserAccount(Datas))
                        {
                            if (!Directory.Exists(RootDir))
                            {
                                Directory.CreateDirectory(RootDir);
                            }

                            if (!Directory.Exists(UserDirectory))
                            {
                                Directory.CreateDirectory(UserDirectory);
                                if (Directory.Exists(UserDirectory))
                                {
                                    Directory.CreateDirectory(ImageDirectory);
                                    if (ImageString != null)
                                    {
                                        string imagePath = Path.Combine(Server.MapPath(Root + "/" + Email + "/" + "Images/" + ImageName));
                                        ImageString.SaveAs(imagePath);
                                    }
                                    Directory.CreateDirectory(FileDirectory);
                                }
                            }
                        }

                        string subject = "Account Setup!";
                        string subjectTitle = "Account Setup";
                        string userName = FullName;
                        string message = "Your account has been registered to our server. Please enter <b>" + GeneratedPassword + "</b> as your password on first Login.";
                        string warningMessage = "If this wasn't you please ignore this email. Verifying the email will only activate your account.";
                        string appLink = "https://" + Request.ServerVariables["HTTP_HOST"];
                        string copyrightDate = DateTime.Now.Year.ToString();
                        try
                        {
                            //Configuring webMail class to send emails  
                            //gmail smtp server  
                            WebMail.SmtpServer = "smtp.gmail.com";

                            //gmail port to send emails  
                            WebMail.SmtpPort = 587;
                            WebMail.SmtpUseDefaultCredentials = true;

                            //sending emails with secure protocol  
                            WebMail.EnableSsl = true;

                            //EmailId used to send emails from application  
                            WebMail.UserName = "jkclaws325@gmail.com";
                            WebMail.Password = "joker9813570528";

                            //Sender email address.  
                            WebMail.From = "jkclaws325@gmail.com";

                            //Send email  
                            WebMail.Send(to: Email, subject: subject, body: EmailBody(subjectTitle, subject, userName, message, warningMessage, appLink, copyrightDate), isBodyHtml: true);
                            Session["Success"] = "An account has been created and email has been sent to " + Email + ".";
                            return RedirectToAction("Users");
                        }
                        catch (Exception)
                        {
                            Session["Error"] = "Problem while sending email but account has been created.";
                            return View("Users");
                        }
                    }
                    else
                    {
                        ViewBag.AddUserError = "Error";
                        Session["Error"] =  users_.MobileNo + " exists please try different  mobile number!!";
                        return View("Users");
                    }
                 
                }
                else
                {
                    ViewBag.AddUserError = "Error";
                    Session["Error"] = users_.Email + "exists please try different email !!";
                    return View("Users");                   
                }
              
            }
        }

        [HttpGet]
        public ActionResult UserEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " User couldn't be found please retry!!";
                return View("Users");
            }
            else
            {
                if (Users_Interface_.UserExists(uaid))
                {
                    ViewBag.EditUserDropDown = "Drop";
                    return View("Users");
                }
                else
                {
                    Session["Error"] = " User couldn't be found please retry!!";
                    return View("Users");
                }
            }
        }

        [HttpPost]
        public ActionResult UserUpdate(Users_Model users_, HttpPostedFileBase ImageString)
        {
            if (string.IsNullOrEmpty(users_.FullName) || string.IsNullOrEmpty(users_.Email) || users_.MobileNo <= 0 || users_.UserProfileID == null)
            {
                ViewBag.UpdateUserError = "Error";
                ViewBag.UpdateUserData = users_.UserID;
                return View("Users");
            }
            else
            {

                if (!Users_Interface_.checkMobileNo(users_.MobileNo))
                {
                    var Datas = new Users_Model();
                  
                    string RandomNumber = Users_Interface_.GenerateRandomNumber();                                                    
                    string Root = "~/UserInformation";
                    string Email = users_.Email;
                    string FullName = users_.FullName;
                    string RootDir = Server.MapPath(Root);
                    string UserDirectory = Server.MapPath(Root + "/" + Email);
                    string ImageDirectory = Server.MapPath(Root + "/" + Email + "/" + "Images");
                    string FileDirectory = Server.MapPath(Root + "/" + Email + "/" + "Documents");
                    var ImageName = "";

                    if (users_.ImageString != null)
                    {
                        ImageName = RandomNumber + Path.GetExtension(ImageString.FileName).ToString();
                        Datas.ImageString = "/UserInformation/" + Email + "/" + "Images/" + ImageName;
                    }

                    Datas.UserID = users_.UserID;
                    Datas.DistrubitorID = users_.DistrubitorID;
                    Datas.UserProfileID = users_.UserProfileID;
                    Datas.FullName = users_.FullName;
                   
                    Datas.Email = users_.Email;
                    Datas.MobileNo = users_.MobileNo;
                    Datas.UsersStatus = users_.UsersStatus;
                                     
                    if (Users_Interface_.UpdateUserAccount(Datas))
                    {
                        if (!Directory.Exists(RootDir))
                        {
                            Directory.CreateDirectory(RootDir);
                        }
                        else{
                            if (!Directory.Exists(UserDirectory))
                            {
                                Directory.CreateDirectory(UserDirectory);
                            }
                            else
                            {
                                if (Directory.Exists(UserDirectory))
                                {
                                    Directory.CreateDirectory(ImageDirectory);
                                    if (ImageString != null)
                                    {
                                        string imagePath = Path.Combine(Server.MapPath(Root + "/" + Email + "/"  + "Images/" + ImageName));
                                        ImageString.SaveAs(imagePath);
                                    }
                                    Directory.CreateDirectory(FileDirectory);
                                }
                            }
                        }                      
                    }
                    Session["Success"] = "Account has been Updated successfully.";
                    return RedirectToAction("Users");
                }
                else
                {
                    ViewBag.AddUserError = "Error";
                    Session["Error"] = users_.MobileNo + " exists please try different mobile number!!";
                    return View("Users");
                }
            }
        }

        [HttpPost]
        public ActionResult UserDelete(Users_Model user_)
        {
            var User_Name = user_.FullName;
            var UserData = new Users_Model();
            Verification_Interface verification_ = new Verification_service();
            var Message = "";
            try
            {
                if (Users_Interface_.UserExists(user_.UserID))
                {
                    UserData = Users_Interface_.GetModelOnlyById(user_.UserID);
                    if (UserData.DistrubitorID != null)
                    {
                        if (Users_Interface_.MakeDistrubitorNull(user_.UserID))
                        {
                            Message = ", distributor account unlinked";
                        }
                        //make distributor null first
                    }                    
                    if (verification_.VerificationExists(user_.UserID))
                    {
                        if (verification_.DeleteVerification(user_.UserID))
                        {
                            Message += ", verification details removed";
                        }
                        //delete verification first
                    }
                    /*delede user folder*/
                    string Root = "~/UserInformation";
                    string Email = UserData.Email;
                    string RootDir = Server.MapPath(Root + "/" + Email);
                    if (Directory.Exists(RootDir))
                    {
                        Directory.Delete(RootDir, true);
                        Message += ", user directory deleted";
                    }

                    if (Users_Interface_.DeleteUser(user_.UserID))
                    {
                        /*delete user*/
                        Message += " and user deleted finally.";
                        return Json(User_Name + "'s " + Message);
                    }
                    else
                    {
                        return Json("Error");
                    }
                }
                else
                {
                    Session["Error"] = User_Name + " not found!!";
                    return View("Users");
                }
            }
            catch (Exception e)
            {
                return Json("Error" + e.ToString());
            }

        }
    }
}