using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SalesTrackingSystem.Helpers.AppAuthAttribute;

namespace SalesTrackingSystem.Controllers
{
    [Authorization]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult E404()
        {
            ViewBag.Title = "404";
            ViewBag.Message = "Page not found.";
            return View();
        }
        public ActionResult E401()
        {
            ViewBag.Title = "401";
            ViewBag.Message = "Un authorized Access.";
            return View();
        }
        public ActionResult E403()
        {
            ViewBag.Title = "403";
            ViewBag.Message = "Forbidden Access.";
            return View();
        }
        public ActionResult E500()
        {
            ViewBag.Title = "500";
            ViewBag.Message = "Under Mainteinance.";
            return View();
        }
        public ActionResult J404()
        {
            ViewBag.Title = "404";
            ViewBag.Message = "Javascript is dissabled.";
            return View();
        }
    }
}