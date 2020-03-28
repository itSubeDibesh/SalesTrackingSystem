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
    public class ResellerController : Controller
    {
        Reseller_Interface resellerService;
        public ResellerController()
        {
            resellerService = new Reseller_Service();
        }
        // GET: Reseller
        public ActionResult Reseller()
        {
            return View("Reseller");
        }
        [HttpPost]
        public ActionResult AddReseller(Reseller_Model reseller)
        {

            if (reseller.ResellerName == null || reseller.DistrubitorID <= 0)
            {
                ViewBag.AddReseller = "Error";
                return View("Reseller");
            }
            else
            {
                if (resellerService.Save(reseller))
                {
                    Session["Success"] = "Reseller added succcessfully";
                    return RedirectToAction("Reseller");
                }
                else
                {
                    Session["Error"] = "Error occured!!";
                    return View("Reseller");
                }
            }
        }

        [HttpGet]
        public ActionResult EditReseller(string action, long res)
        {
            if (string.IsNullOrEmpty(action) && res == 0)
            {
                Session["Error"] = " Reseller couldn't be found please retry!!";
                return RedirectToAction("Reseller");
            }
            else
            {
                var ResellerData = resellerService.GetResellerById(res);
                if (ResellerData != null)
                {
                    ViewBag.EditResellerDropDown = "Drop";
                    return View("Reseller");
                }
                else
                {
                    Session["Error"] = " Reseller couldn't be found please retry!!";
                    return View("Reseller");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateReseller(Reseller_Model model)
        {
            if (string.IsNullOrWhiteSpace(model.ResellerName) || model.DistrubitorID <= 0)
            {
                ViewBag.ResellerUpdateError = "Error";
                ViewBag.UpdateResellerData = model.ResellerID;
                return View("Reseller");
            }
            else
            {
                if (resellerService.Update(model))
                {
                    Session["Success"] = model.ResellerName + " updated successfully!!";
                    return RedirectToAction("Reseller");
                }
                else
                {
                    Session["Error"] = model.ResellerName + " couldn't be updated please retry!!";
                    return View("Reseller");
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteReseller(Reseller_Model model)
        {
            try
            {
                if (resellerService.Delete(model.ResellerID))
                {
                    return Json(model.ResellerName + " has been deleted successfully");
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

    }
}