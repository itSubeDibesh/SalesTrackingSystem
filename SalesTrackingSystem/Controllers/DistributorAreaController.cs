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
    public class DistributorAreaController : Controller
    {
        DistributorArea_Interface distAreaService;
        // GET: DistributorArea
        public DistributorAreaController()
        {
            distAreaService = new DistributorArea_Service();
        }
        public ActionResult DistributorArea()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDistributorArea(DistributorArea_Model distArea_Model)
        {

            if (distArea_Model.DistrubitorID <= 0)
            {
                ViewBag.AddDistributorArea = "Error";
                return View("DistributorArea");
            }
            else
            {
                if (distAreaService.Save(distArea_Model))
                {
                    Session["Success"] = "Distributor Area added succcessfully";
                    return RedirectToAction("DistributorArea");
                }
                else
                {
                    Session["Error"] = "Error occured!!";
                    return View("DistributorArea");
                }
            }
        }

        [HttpGet]
        public ActionResult EditDistributorArea(string action, long distAreaId)
        {
            if (string.IsNullOrEmpty(action) && distAreaId == 0)
            {
                Session["Error"] = " Distributor Area couldn't be found please retry!!";
                return RedirectToAction("DistributorArea");
            }
            else
            {
                var DistributorAreaData = distAreaService.GetDistributorAreaById(distAreaId);
                if (DistributorAreaData != null)
                {
                    ViewBag.EditDistributorAreaDropDown = "Drop";
                    return View("DistributorArea");
                }
                else
                {
                    Session["Error"] = " Distributor Area couldn't be found please retry!!";
                    return View("DistributorArea");
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateDistributorArea(DistributorArea_Model model)
        {
            if (model.DistrubitorID <= 0)
            {
                ViewBag.DistributorUpdateAreaError = "Error";
                ViewBag.UpdateDistributorAreaData = model.DistributorAreaID;
                return View("DistributorArea");
            }
            else
            {
                if (distAreaService.Update(model))
                {
                    Session["Success"] =" updated successfully!!";
                    return RedirectToAction("DistributorArea");
                }
                else
                {
                    Session["Error"] =" couldn't be updated please retry!!";
                    return View("DistributorArea");
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteDistributorArea(DistributorArea_Model model)
        {
            try
            {
                if (distAreaService.Delete(model.DistributorAreaID))
                {
                    return Json("Deleted successfully");
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