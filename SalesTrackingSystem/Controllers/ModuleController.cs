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
    public class ModuleController : Controller
    {
        // GET: Module
        Module_Interface Module_;
        ModuleAction_Interface ModuleAction_;
        public ModuleController()
        {
            Module_ = new Module_Service();
            ModuleAction_ = new ModuleAction_Service();
        }
        public ActionResult Module()
        {            
            return View("Module");
        }

        [HttpPost]
        public ActionResult ModuleAdd(Module_Model module_Model)
        {
            if (string.IsNullOrEmpty(module_Model.ModuleName)||string.IsNullOrEmpty(module_Model.ControllerName)|| module_Model.ModuleStatus==null)
            {
                ViewBag.AddError = "Error";
                return View("Module");
            }
            else
            {
                if (Module_.SaveModule(module_Model))
                {
                    Session["Success"] = module_Model.ModuleName + " added successfully!!";
                }
                else{
                    Session["Error"] = module_Model.ModuleName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Module");
            }          
        }

        [HttpGet]
        public ActionResult ModuleEdit(string action,Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {              
                Session["Error"] = " Module couldn't be found please retry!!";
                return View("Module");
            }
            else
            {
                if (Module_.ModuleExists(uaid))
                {
                    ViewBag.EditDropDown = "Drop";                   
                    return View("Module");
                }
                else
                {
                    Session["Error"] = " Module couldn't be found please retry!!";
                    return View("Module");
                }
            }
        }

        [HttpPost]
        public ActionResult ModuleUpdate(Module_Model module_Model)
        {
            if (string.IsNullOrWhiteSpace(module_Model.ModuleName) || string.IsNullOrWhiteSpace(module_Model.ControllerName) || module_Model.ModuleStatus == null)
            {
                ViewBag.UpdateError = "Error";
                ViewBag.UpdateData = module_Model.ModuleID;
                return View("Module");
            }
            else
            {
                if (Module_.UpdateModule(module_Model))
                {
                    Session["Success"] = module_Model.ModuleName + " updated successfully!!";
                }
                else
                {
                    Session["Error"] = module_Model.ModuleName + " couldn't be updated please retry!!";
                }
                return View("Module");
            }
        }

        [HttpPost]
        public ActionResult ModuleDelete(Module_Model module_Model)
        {
            var Module_Name = module_Model.ModuleName;
            try
            {
                if (Module_.DeleteModule(module_Model.ModuleID))
                {
                    return Json(Module_Name + " module has been deleted successfully");
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

        public ActionResult Action()
        {
            return View("Action");
        }

        [HttpPost]
        public ActionResult ActionAdd(ModuleAction_Model moduleAction_Model)
        {
            if (moduleAction_Model.ActionStatus == null || string.IsNullOrEmpty(moduleAction_Model.ActionName) || moduleAction_Model.ModuleID == null)
            {
                ViewBag.AddError = "Error";
                return View("Action");
            }
            else
            {
                if (ModuleAction_.SaveAction(moduleAction_Model))
                {
                    Session["Success"] = moduleAction_Model.ActionName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = moduleAction_Model.ActionName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Action");
            }
        }

        [HttpGet]
        public ActionResult ActionEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Action couldn't be found please retry!!";
                return View("Action");
            }
            else
            {
                if (Module_.ModuleExists(uaid))
                {
                    ViewBag.EditDropDown = "Drop";
                    return View("Action");
                }
                else
                {
                    Session["Error"] = " Action couldn't be found please retry!!";
                    return View("Action");
                }
            }
        }

        [HttpPost]
        public ActionResult ActionUpdate(ModuleAction_Model moduleAction_Model)
        {
            if (string.IsNullOrWhiteSpace(moduleAction_Model.ActionName) || moduleAction_Model.ModuleID==null || moduleAction_Model.ActionStatus == null)
            {
                ViewBag.UpdateError = "Error";
                ViewBag.UpdateData = moduleAction_Model.ModuleActionID;
                return View("Action");
            }
            else
            {
                if (ModuleAction_.UpdateAction(moduleAction_Model))
                {
                    Session["Success"] = moduleAction_Model.ActionName + " updated successfully!!";
                }
                else
                {
                    Session["Error"] = moduleAction_Model.ActionName + " couldn't be updated please retry!!";
                }
                return View("Action");
            }
        }

        [HttpPost]
        public ActionResult ActionDelete(ModuleAction_Model moduleAction_Model)
        {
            var Action_Name = moduleAction_Model.ActionName;
            try
            {
                if (ModuleAction_.DeleteAction(moduleAction_Model.ModuleActionID))
                {
                    return Json(Action_Name + " action has been deleted successfully");
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