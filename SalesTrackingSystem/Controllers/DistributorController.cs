using Models;
using Services.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SalesTrackingSystem.Helpers.AppAuthAttribute;

namespace SalesTrackingSystem.Controllers
{
    [Authorization]
    public class DistributorController : Controller
    {
        Distributor_Interface distributor_Interface;
        DistributorArea_Interface distributorArea_Interface;
        public DistributorController()
        {
            distributor_Interface = new Distributor_Service();
            distributorArea_Interface = new DistributorArea_Service();
        }
        // GET: Distributor
        public ActionResult Distributor()
        {
            return View("Distributor");
        }
        [HttpPost]
        public ActionResult AddDistributor(Distributor_Model distributor_Model)
        {

            if (distributor_Model.DistrubitorName == null || distributor_Model.OwnerName == null|| distributor_Model.District==null|| distributor_Model.State == null || distributor_Model.Email==null)
            {
                ViewBag.AddDistributor = "Error";
                return View("Distributor");
            }
            else
            {
                if (distributor_Interface.Save(distributor_Model))
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
                var DistributorData = distributor_Interface.GetDistributorById(dist);
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
        public ActionResult UpdateDistributor(Distributor_Model distributor)
        {
            if (string.IsNullOrWhiteSpace(distributor.DistrubitorName) || distributor.OwnerName == null || distributor.District == null || distributor.State == null || distributor.Email == null)
            {
                ViewBag.DistributorUpdateError = "Error";
                ViewBag.UpdateDistributorData = distributor.DistrubitorID;
                return View("Distributor");
            }
            else
            {
                if (distributor_Interface.Update(distributor))
                {
                    Session["Success"] = distributor.DistrubitorName + " updated successfully!!";
                    return RedirectToAction("Distributor");
                }
                else
                {
                    Session["Error"] = distributor.DistrubitorName + " couldn't be updated please retry!!";
                    return View("Distributor");
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateDistributorSettings(Distributor_Model distributor)
        {
            if (string.IsNullOrWhiteSpace(distributor.DistrubitorName) || distributor.OwnerName == null || distributor.District == null || distributor.State == null || distributor.Email == null)
            {
                ViewBag.DistributorUpdateError = "Error";
                ViewBag.UpdateDistributorData = distributor.DistrubitorID;
                return RedirectToAction("Setting", "Home");
            }
            else
            {
                if (distributor_Interface.Update(distributor))
                {
                    Session["Success"] = distributor.DistrubitorName + " updated successfully!!";
                    return RedirectToAction("Setting", "Home");
                }
                else
                {
                    Session["Error"] = distributor.DistrubitorName + " couldn't be updated please retry!!";
                    return RedirectToAction("Setting", "Home");   
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteDistributor(Distributor_Model distributor)
        {
            try
            {
                if (distributor_Interface.Delete(distributor.DistrubitorID))
                {
                    return Json(distributor.DistrubitorName + " has been deleted successfully");
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

        public ActionResult DistributorArea()
        {
            return View("DistributorArea");
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
                if (distributorArea_Interface.Save(distArea_Model))
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
                var DistributorAreaData = distributorArea_Interface.GetDistributorAreaById(distAreaId);
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
                if (distributorArea_Interface.Update(model))
                {
                    Session["Success"] = " updated successfully!!";
                    return RedirectToAction("DistributorArea");
                }
                else
                {
                    Session["Error"] = " couldn't be updated please retry!!";
                    return View("DistributorArea");
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteDistributorArea(DistributorArea_Model model)
        {
            try
            {
                if (distributorArea_Interface.Delete(model.DistributorAreaID))
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