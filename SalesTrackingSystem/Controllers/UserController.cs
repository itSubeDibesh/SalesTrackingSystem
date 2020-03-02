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
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
    }
}