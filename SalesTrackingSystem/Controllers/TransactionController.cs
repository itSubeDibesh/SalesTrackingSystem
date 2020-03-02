using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Transaction()
        {
            return View("Transaction");
        }
    }
}