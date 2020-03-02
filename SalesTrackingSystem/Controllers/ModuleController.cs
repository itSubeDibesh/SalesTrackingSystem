using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Module()
        {
            return View("Module");
        }
        public ActionResult Action()
        {
            return View("Action");
        }
    }
}