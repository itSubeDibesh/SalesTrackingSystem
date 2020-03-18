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
    public class DistributorController : Controller
    {
        Distributor_Interface distService;
        public DistributorController()
        {
            distService = new Distributor_Service();
        }
        // GET: Distributor
        //Add Update Distributor
        public ActionResult Distributor()
        {
            return View("Distributor");
        }
        [HttpPost]
        public ActionResult AddDistributor(Distributor_Model dist_Model)
        {

            if (dist_Model.DistrubitorName== null || dist_Model.OwnerName== null)
            {
                ViewBag.AddDistributor = "Error";
                return View("Distributor");
            }
            else
            {
                if (distService.Save(dist_Model))
                {
                    Session["Success"] = "Distributor added succcessfully";
                    return RedirectToAction("Distributor");
                }
                else
                {
                    Session["Error"] = "Error occured!!";
                    return View("Distributor");
                }
            }
        }

        [HttpGet]
        public ActionResult EditDistributor(string action, long dist)
        {
            if (string.IsNullOrEmpty(action) && dist == 0)
            {
                Session["Error"] = " Distributor couldn't be found please retry!!";
                return RedirectToAction("Distributor");
            }
            else
            {
                var DistributorData = distService.GetDistributorById(dist);
                if (DistributorData != null)
                {
                    ViewBag.EditDistributorDropDown = "Drop";
                    return View("Distributor");
                }
                else
                {
                    Session["Error"] = " Distributor couldn't be found please retry!!";
                    return View("Distributor");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateDistributor(Distributor_Model model)
        {
            if (string.IsNullOrWhiteSpace(model.DistrubitorName) || model.OwnerName==null)
            {
                ViewBag.DistributorUpdateError = "Error";
                ViewBag.UpdateDistributorData = model.DistrubitorID;
                return View("Distributor");
            }
            else
            {
                if (distService.Update(model))
                {
                    Session["Success"] = model.DistrubitorName+ " updated successfully!!";
                    return RedirectToAction("Distributor");
                }
                else
                {
                    Session["Error"] = model.DistrubitorName + " couldn't be updated please retry!!";
                    return View("Distributor");
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteDistributor(Distributor_Model model)
        {
            try
            {
                if (distService.Delete(model.DistrubitorID))
                {
                    return Json(model.DistrubitorName + " has been deleted successfully");
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
        //Add Update Distributor Ends

        public ActionResult DistributonArea()
        {
            return View("DistributonArea");
        }
        
    }
}