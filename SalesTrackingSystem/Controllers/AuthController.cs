using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;
using Services.Interface;
using Services.Service;
using Models;
using System.Web.Helpers;


namespace SalesTrackingSystem.Controllers
{
    public class AuthController : Controller
    {
        #region //Interface Inatialization
        Users_Interface Users;
        Verification_Interface Verification;
        #endregion

        public AuthController()
        {
            Users = new Users_Service();
            Verification = new Verification_service();
        }

        [HttpGet]
        public ActionResult Login()
        {
            var LoginSession = (Users_Model)Session["auth"];
            string CheckLogin;
            if (LoginSession != null)
            {
                CheckLogin = Users.CheckLogin(LoginSession.Email, LoginSession.PasswordHash);
                if (CheckLogin == "ValidUserActiveStatus")
                {
                    Session["Success"] = "Let's do something greate " + LoginSession.FullName + ".";
                    /*Redirect to different assigned page*/
                    return RedirectToAction("Index", "Home");
                }
                else if (CheckLogin == "ValidUserInactiveStatus")
                {
                    Session["auth"] = Users.UpdateOnLogin(LoginSession.Email, LoginSession.PasswordHash, LoginSession.Token, 1);
                    LoginSession = (Users_Model)Session["auth"];
                    Session["Success"] = "Hello " + LoginSession.FullName + ", something great is going to happen.";
                    /*Redirect to different assigned page*/
                    return RedirectToAction("Index", "Home");
                }
                else if (CheckLogin == "ValidUserBlockedStatus")
                {
                    Session["Warning"] = "Your Account has been blocked please contact admin.";
                    return View("Login");
                }
                else if (CheckLogin == "InvalidUser")
                {
                    Session["Error"] = "Invalid user name or password.";
                    return View("Login");
                }
                else
                {
                    Session["Error"] = "Server error please retry!!";
                    return View("Login");
                }
            }
            else
            {
                /*Normal redirect*/
                return View("Login");
            }

        }

        [HttpPost]
        public ActionResult Login(string email = "", string password = "", string remember = "")
        {
            if (email != "" && password != "")
            {
                string Remembered;
                var LoginSalt = "SHA1" + email + "SalesTrackingSystem";
                var HashedValue = Crypto.SHA1(LoginSalt + password);                            
                if (Users.CheckNewAccount(email, HashedValue))
                {
                    /* Send to reset password page*/
                    var ChekingSession = Users.GetModelByEmail(email);
                    string generatedToken = Users.GenerateRandomString(20, 80);
                    if (Verification.updateResetAuthentication(ChekingSession.UserID, DateTime.Now, generatedToken))
                    {
                        Session["Warning"] = "Please reset you'r password on first login!!";
                        return RedirectToAction("Reset", "Auth", new { uac = email, uid = generatedToken });
                    }
                    else
                    {
                        Session["Error"] = "Problem creating reset environment. Please try again!!";
                        return View("Login");
                    }
                }
                else
                {
                    /* Normal login */
                    var LoginSession = (Users_Model)Session["auth"];
                    string CheckLogin = Users.CheckLogin(email, HashedValue);
                    if (CheckLogin == "ValidUserActiveStatus")
                    {
                        if (remember != "")
                        {
                            Remembered = Users.GenerateRandomString();
                        }
                        else
                        {
                            Remembered = null;
                        }

                        Session["auth"] = Users.UpdateOnLogin(email, HashedValue, Remembered);
                        LoginSession = (Users_Model)Session["auth"];
                        Session["Success"] = "Hello " + LoginSession.FullName + ", Hope you hade a wonderfull day.";
                        /*Redirect to different assigned page*/
                        return RedirectToAction("Index", "Home");
                    }
                    else if (CheckLogin == "ValidUserInactiveStatus")
                    {
                        if (remember != "")
                        {
                            Remembered = Users.GenerateRandomString();
                        }
                        else
                        {
                            Remembered = null;
                        }
                        Session["auth"] = Users.UpdateOnLogin(email, HashedValue, Remembered, 1);
                        LoginSession = (Users_Model)Session["auth"];
                        Session["Success"] = "Hello " + LoginSession.FullName + ", Nice to see you back!.";
                        /*Redirect to different assigned page*/
                        return RedirectToAction("Index", "Home");
                    }
                    else if (CheckLogin == "ValidUserBlockedStatus")
                    {
                        Session["Warning"] = "Your Account has been blocked please contact admin.";
                        return View("Login");
                    }
                    else if (CheckLogin == "InvalidUser")
                    {
                        Session["Error"] = "Invalid email or password.";
                        return View("Login");
                    }
                    else
                    {
                        Session["Error"] = "Email or password incorrect!!";
                        return View("Login");
                    }
                   
                }
               
            }
            else
            {
                Session["Error"] = "Email or password field are empty!!";
                return View("Login");
            }

        }

        [HttpGet]
        public ActionResult Logout()
        {
            var LoginSession = (Users_Model)Session["auth"];
            if (LoginSession != null)
            {
                Users.UpdateOnLogout(LoginSession.Email, LoginSession.PasswordHash);
                Session.Abandon();
                return View("Login");
            }
            else
            {
                Session.Abandon();
                return View("Login");
            }
        }

        [HttpGet]
        public ActionResult Forget()
        {
            return View("ForgetPassword");
        }

        [HttpPost]
        public ActionResult Forget(string email)
        {
            var LoginSession = new Users_Model();
            if (Users.checkEmail(email))
            {
                LoginSession = Users.GetModelByEmail(email);
                string generatedToken = Users.GenerateRandomString(20, 80);
                if (Verification.updateResetAuthentication(LoginSession.UserID,DateTime.Now,generatedToken))
                {
                    string subject = "Reset Password!";
                    string subjectTitle = "reset password";
                    string userName = LoginSession.FullName;
                    string message = "Your request to reset password has been accepted. Please confirm it's you by clicking the link below. This Link is valid for 10 minuts only.";
                    string redirectUrl = "https://" + Request.ServerVariables["HTTP_HOST"] + "/Auth/Reset?uac=" + email + "&uid=" + generatedToken;
                    string warningMessage = "If this wasn't you please ignore this email. Verifying the email will only allow to reset password.";
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
                        WebMail.Send(to: LoginSession.Email, subject: subject, body: EmailBody(subjectTitle, subject, userName, message, redirectUrl, warningMessage, appLink, copyrightDate), isBodyHtml: true);
                        Session["Success"] = "An email has been successfully to your account.";
                    }
                    catch (Exception)
                    {
                        Session["Error"] = "Problem while sending email.";

                    }
                    ViewBag.UserName = LoginSession.FullName;
                    return View("VerificationEmail");
                }
                else{
                    Session["Error"] = "Problem while sending email.";
                    return View("ForgetPassword");
                }
            }
            else
            {
                Session["Error"] = "The credentials you provide doesn't match our database.";
                return View("ForgetPassword");
            }           
        }

        [HttpGet]
        public ActionResult Reset(string uac,string uid)
        {           
            @ViewBag.Email = uac;
            @ViewBag.Token = uid;
            return View("ResetPassword");                          
        }

        [HttpPost]
        public ActionResult Reset(string email,string verificationToken,string newPassword)
        {
            if (email != null && verificationToken != null && newPassword != null)
            {
                if (Users.checkEmail(email))
                {
                    var LoginSession = Users.GetModelByEmail(email);
                    var RessetSession = Verification.checkReset(LoginSession.UserID, verificationToken);
                    DateTime verifiedDate = Convert.ToDateTime(RessetSession.ResetTriggered);
                    DateTime currentDate = DateTime.Now;

                    if ((verifiedDate - currentDate).Minutes <= 10)
                    {
                        if (Verification.CheckReset(LoginSession.UserID, verificationToken))
                        {
                            var LoginSalt = "SHA1" + LoginSession.Email + "SalesTrackingSystem";
                            var HashedValue = Crypto.SHA1(LoginSalt + newPassword);
                            if (Users.resetpassword(LoginSession.UserID, HashedValue))
                            {
                                Session["Success"] = "Password reset Successfully";
                                return RedirectToAction("Login");
                            }
                            else
                            {
                                return RedirectToAction("Reset", "Auth", new { uac = email, uid = verificationToken });
                            }                           
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                    }
                    else{
                        return View("ForgetPassword");
                    }
                }
                else
                {
                    return RedirectToAction("Reset", "Auth",new { uac = email ,uid=verificationToken});
                }
            }
            else
            {
                return RedirectToAction("Reset", "Auth", new { uac = email, uid = verificationToken });
            }
        }


        private string EmailBody(string SubjectTitle, string Subject, string UserName, string Message, string RedirectURL, string WarningMessage, string AppLink, string CopyrightDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/EmailTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{SubjectTitle}", SubjectTitle);
            body = body.Replace("{Subject}", Subject);
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{Message}", Message);
            body = body.Replace("{RedirectURL}", RedirectURL);
            body = body.Replace("{WarningMessage}", WarningMessage);
            body = body.Replace("{AppLink}", AppLink);
            body = body.Replace("{CopyrightDate}", CopyrightDate);
            return body;
        }

        [HttpGet]
        public ActionResult CheckVerification()
        {
            var LoginSession = (Users_Model)Session["auth"];
            if (Session["auth"] != null && LoginSession.IsVerified.Value == false)
            {
                string generatedToken = Users.GenerateRandomString(20, 50);
                if (Verification.updateVerificationAuthentacitation(LoginSession.UserID, 0, DateTime.Now, generatedToken))
                {
                    string subject = "Account confirmation!";
                    string subjectTitle = "Account confirmation";
                    string userName = LoginSession.FullName;
                    string message = "Your account has been registered to our server. Please confirm its you by clicking the link below. This Link is valid for 50 minuts only.";
                    string redirectUrl = "https://" + Request.ServerVariables["HTTP_HOST"] + "/Auth/RediecVerification?uat=" + LoginSession.UserID + "&uid=" + generatedToken;
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
                        WebMail.Send(to: LoginSession.Email, subject: subject, body: EmailBody(subjectTitle, subject, userName, message, redirectUrl, warningMessage, appLink, copyrightDate), isBodyHtml: true);
                        Session["Success"] = "An email has been successfully to your account.";
                    }
                    catch (Exception)
                    {
                        Session["Error"] = "Problem while sending email.";

                    }
                    return View("Verification");
                }
                else
                {
                    Session["Error"] = "Problem while sending email.";
                    return View("Verification");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult RediecVerification(Int64 uat, string uid)
        {
            if (uat != 0 && uid != null)
            {
                var verificationModel = Verification.checkVerification(uat, uid);
                if (verificationModel.IsVerified == false)
                {
                    DateTime verifiedDate = Convert.ToDateTime(verificationModel.DateVerified);
                    DateTime currentDate = DateTime.Now;
                    var LoginSession = (Users_Model)Session["auth"];
                    if ((verifiedDate - currentDate).Minutes <= 50)
                    {
                        if (Verification.updateCheckedVerification(uat, 1))
                        {
                            Session["auth"] = Users.GetModelById(uat);
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            Session["Error"] = "Problem while activating account.";
                            return View("LongTimeVerification");
                        }
                    }
                    else
                    {
                        return View("LongTimeVerification");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("CheckVerification");
            }
        }       
    }
}