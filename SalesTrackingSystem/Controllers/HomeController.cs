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
    public class HomeController : Controller
    {
        Users_Interface Users;
        public HomeController(){
            Users = new Users_Service();
        }
        public ActionResult Index()
        {
            return View("Index");
        }
      
        public ActionResult Setting()
        {
            return View("Setting");
        }

        [HttpPost]
        public ActionResult Reset(string OldPassword, string NewPassword)
        {
           
            var LoginSession = (Users_Model)Session["auth"];
            if (LoginSession != null)
            {
                var LoginSalt = "SHA1" + LoginSession.Email + "SalesTrackingSystem";
                var oldPassword = Crypto.SHA1(LoginSalt + OldPassword);
                var newPassword = Crypto.SHA1(LoginSalt + NewPassword);
               
                if (Users.CheckReset(LoginSession.Email, oldPassword))
                {
                    /*Reset and logout*/
                    if (Users.resetpassword(LoginSession.UserID, newPassword))
                    {
                        Session.Abandon();
                        Session["Success"] = "Password reset successfully!!";
                        return RedirectToAction("Login", "Auth");
                    }
                    else{

                        Session["Error"] = "There Was problem while resetting password please retry!!";
                        return View("Setting");
                    }                   
                }
                else
                {
                    /*Redirect error*/
                    Session["Warning"] = "Old Password doesn't match!!";
                    return View("Setting");
                }               
            }
            else
            {
                Session["Warning"] = "Unauthorized access!!";
                return RedirectToAction("Login", "Auth");                             
            }          
        }

        [HttpPost]
        public ActionResult UpdateProfile(Users_Model users_, HttpPostedFileBase ImageString)
        {
            if (!string.IsNullOrWhiteSpace(users_.FullName) || users_.MobileNo >= 0)
            {
                /*Update*/
                var LoginSession = (Users_Model)Session["auth"];
                if (LoginSession!= null)
                {
                    var Datas = new Users_Model();

                    string RandomNumber = Users.GenerateRandomNumber();
                    string Root = "~/UserInformation";
                    string Email = LoginSession.Email;
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

                    Datas.UserID = LoginSession.UserID;   
                    Datas.FullName = users_.FullName; 
                    Datas.MobileNo = users_.MobileNo;

                    if (Users.UpdateUserProfile(Datas))
                    {
                        if (!Directory.Exists(RootDir))
                        {
                            Directory.CreateDirectory(RootDir);
                        }
                        else
                        {
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
                                        string imagePath = Path.Combine(Server.MapPath(Root + "/" + Email + "/" + "Images/" + ImageName));
                                        ImageString.SaveAs(imagePath);
                                    }
                                    Directory.CreateDirectory(FileDirectory);
                                }
                            }
                        }
                    }
                    Session["Success"] = "Profile has been Updated successfully.";
                    return View("Setting");
                }
                else
                {
                    Session["Warning"] = "Unauthorized access!!";
                    return RedirectToAction("Login", "Auth");
                }
            }
            else{
                /*Throw error*/
                Session["Error"] = "Full name or mobile number is not valid please try again!!";
                return View("Setting");
            }           
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
    }
}