using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Batch()
        {
            return View("Batch");
        }

        public ActionResult Products()
        {
            return View("Products");
        }
    }
}