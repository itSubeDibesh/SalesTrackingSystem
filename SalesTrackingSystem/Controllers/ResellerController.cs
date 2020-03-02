using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
    public class ResellerController : Controller
    {
        // GET: Reseller
        public ActionResult Reseller()
        {
            return View("Reseller");
        }
    }
}