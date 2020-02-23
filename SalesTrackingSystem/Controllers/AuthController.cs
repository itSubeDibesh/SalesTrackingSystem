using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interface;
using Services.Service;
using Models;
using System.Web.Helpers;
using SalesTrackingSystem.Helpers;

namespace SalesTrackingSystem.Controllers
{
    public class AuthController : Controller
    {
        #region //Interface Inatialization
        UserProfile_Interface UserProfile;
        UserProfileDetails_Interface UserProfileDetails;
        Users_Interface Users;
        ExceptionUserProfile_Interface ExceptionUserProfile;
        Module_Interface Module;
        ModuleAction_Interface ModuleAction;
        #endregion

        public AuthController()
        {
            UserProfile = new UserProfile_Service();
            UserProfileDetails = new UserProfileDetails_Service();
            Users = new Users_Service();
            ExceptionUserProfile = new ExceptionUserProfile_Service();
            Module = new Module_Service();
            ModuleAction = new ModuleAction_Service();
        }

        [HttpGet]
        public ActionResult Login()
        {
            var LoginSession = (Users_Model)Session["auth"];
            var Cookie = Request.Cookies["auth"];
            var DbContents = new Users_Model();
            string CheckLogin;
            if (Cookie != null)
            {
                DbContents = Users.GetModelByToken(Cookie.Value.ToString());
            }
            if (Request.Cookies["auth"] != null && DbContents != null && Cookie.Value.ToString() == DbContents.Token)
            {
                CheckLogin = Users.CheckLogin(DbContents.Email, DbContents.PasswordHash);
                if (CheckLogin == "ValidUserActiveStatus")
                {
                    Session["Success"] = "Let's do something greate " + DbContents.FullName + ".";
                    /*Redirect to different assigned page*/
                    return RedirectToAction("Index", "Home");
                }
                else if (CheckLogin == "ValidUserInactiveStatus")
                {
                    Session["auth"] = Users.UpdateOnLogin(DbContents.Email, DbContents.PasswordHash, DbContents.Token, 1);
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
                var DbContentsSession = new Users_Model();
                string CheckLoginSession = " ";
                if (LoginSession != null)
                {
                    DbContentsSession = Users.GetModelByToken(LoginSession.Token);
                    CheckLoginSession = Users.CheckLogin(DbContentsSession.Email, DbContentsSession.PasswordHash);
                }
                if (LoginSession != null && DbContentsSession != null && LoginSession.Token == DbContentsSession.Token)
                {
                    if (CheckLoginSession == "ValidUserActiveStatus")
                    {
                        Session["Success"] = "Going greate " + DbContentsSession.FullName + ".";
                        /*Redirect to different assigned page*/
                        return RedirectToAction("Index", "Home");
                    }
                    else if (CheckLoginSession == "ValidUserInactiveStatus")
                    {
                        Session["auth"] = Users.UpdateOnLogin(DbContentsSession.Email, DbContentsSession.PasswordHash, DbContentsSession.Token, 1);
                        LoginSession = (Users_Model)Session["auth"];
                        Session["Success"] = "Have a good day " + LoginSession.FullName + "!";
                        /*Redirect to different assigned page*/
                        return RedirectToAction("Index", "Home");
                    }
                    else if (CheckLoginSession == "ValidUserBlockedStatus")
                    {
                        Session["Warning"] = "Your Account has been blocked please contact admin.";
                        return View("Login");
                    }
                    else if (CheckLoginSession == "InvalidUser")
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
                else
                {
                    /*Normal redirect*/
                    return View("Login");
                }
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
                var LoginSession = (Users_Model)Session["auth"];
                HttpCookie Cookie;
                string CheckLogin = Users.CheckLogin(email, HashedValue);
                if (CheckLogin == "ValidUserActiveStatus")
                {
                    if (remember != "")
                    {
                        Remembered = Users.GenerateRandomString();
                        Cookie = new HttpCookie("auth", Remembered);
                        Cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(Cookie);
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
                        Cookie = new HttpCookie("auth", Remembered);
                        Cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(Cookie);
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
            else
            {
                Session["Error"] = "Email or password field are empty!!";
                return View("Login");
            }

        }

    }
}