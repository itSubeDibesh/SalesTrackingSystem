using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
    public class DistributorController : Controller
    {
        // GET: Distributor
        public ActionResult Distributor()
        {
            return View("Distributor");
        }
        public ActionResult DistributonArea()
        {
            return View("DistributonArea");
        }
        
    }
}