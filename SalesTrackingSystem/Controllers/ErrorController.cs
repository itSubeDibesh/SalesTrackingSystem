using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Controllers
{
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