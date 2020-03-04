using Models;
using Services.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserProfile_Interface UserProfile_Interface_;      
        public UserController()
        {
            UserProfile_Interface_ = new UserProfile_Service();

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

        public ActionResult Users()
        {
            return View();
        }
    }
}