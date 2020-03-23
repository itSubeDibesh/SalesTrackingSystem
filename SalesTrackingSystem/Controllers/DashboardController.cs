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
    public class DashboardController : Controller
    {
        // GET: Dashboard
        // GET: User
        DashboardTable_Interface DashboardTable_Interface;
        DashboardType_Interface DashboardType_Interface;
        DashboardGivenColumn_Interface DashboardGivenColumn_Interface;
        public DashboardController()
        {
            DashboardTable_Interface = new DashboardTable_Service();
            DashboardType_Interface = new DashboardType_Service();
            DashboardGivenColumn_Interface = new DashboardGivenColumn_Service();
        }
        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TypeAdd(DashboardType_Model type_Model)
        {
            if (string.IsNullOrEmpty(type_Model.TypeName))
            {
                ViewBag.TypeAddError = "Error";
                return View("Settings");
            }
            else
            {
                if (DashboardType_Interface.SaveDashboardType(type_Model))
                {
                    Session["Success"] = type_Model.TypeName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = type_Model.TypeName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Settings");
            }
        }

        [HttpGet]
        public ActionResult TypeEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Type couldn't be found please retry!!";
                return View("Settings");
            }
            else
            {
                if (DashboardType_Interface.DashboardType_Exists(uaid))
                {
                    ViewBag.EditTypeDropDown = "Drop";
                    return View("Settings");
                }
                else
                {
                    Session["Error"] = " Type couldn't be found please retry!!";
                    return View("Settings");
                }
            }
        }

        [HttpPost]
        public ActionResult TypeUpdate(DashboardType_Model type_Model)
        {
            if (string.IsNullOrEmpty(type_Model.TypeName))
            {
                ViewBag.UpdateTypeError = "Error";
                ViewBag.UpdateTypeData = type_Model.DashboardTypeID;
                return View("Settings");
            }
            else
            {
                if (DashboardType_Interface.UpdateDashboardType(type_Model))
                {
                    Session["Success"] = type_Model.TypeName + " updated successfully!!";
                    return RedirectToAction("Settings");
                }
                else
                {
                    Session["Error"] = type_Model.TypeName + " couldn't be updated please retry!!";
                    return View("Settings");
                }

            }
        }

        [HttpPost]
        public ActionResult TypeDelete(DashboardType_Model type_Model)
        {
            var Type_Name = type_Model.TypeName;
            try
            {
                if (DashboardType_Interface.DeleteDashboardType(type_Model.DashboardTypeID))
                {
                    return Json(Type_Name + " type has been deleted successfully");
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

        [HttpPost]
        public ActionResult TableAdd(DashboardTable_Model table_Model)
        {
            if (string.IsNullOrEmpty(table_Model.TableName))
            {
                ViewBag.TableAddError = "Error";
                return View("Settings");
            }
            else
            {
                if (DashboardTable_Interface.SaveDashboardTable(table_Model))
                {
                    Session["Success"] = table_Model.TableName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = table_Model.TableName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Settings");
            }
        }

        [HttpGet]
        public ActionResult TableEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Table couldn't be found please retry!!";
                return View("Settings");
            }
            else
            {
                if (DashboardTable_Interface.DashboardTable_Exists(uaid))
                {
                    ViewBag.EditTableDropDown = "Drop";
                    return View("Settings");
                }
                else
                {
                    Session["Error"] = " Table couldn't be found please retry!!";
                    return View("Settings");
                }
            }
        }

        [HttpPost]
        public ActionResult TableUpdate(DashboardTable_Model table_Model)
        {
            if (string.IsNullOrEmpty(table_Model.TableName))
            {
                ViewBag.UpdateTableError = "Error";
                ViewBag.UpdateTableData = table_Model.DashboardTableId;
                return View("Settings");
            }
            else
            {
                if (DashboardTable_Interface.UpdateDashboardTable(table_Model))
                {
                    Session["Success"] = table_Model.TableName + " updated successfully!!";
                    return RedirectToAction("Settings");
                }
                else
                {
                    Session["Error"] = table_Model.TableName + " couldn't be updated please retry!!";
                    return View("Settings");
                }

            }
        }

        [HttpPost]
        public ActionResult TableDelete(DashboardTable_Model table_Model)
        {
            var Type_Name = table_Model.TableName;
            try
            {
                if (DashboardTable_Interface.DeleteDashboardTable(table_Model.DashboardTableId))
                {
                    return Json(Type_Name + " type has been deleted successfully");
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

        [HttpPost]
        public ActionResult ColumnAdd(DashboardGivenColumn_Model dashboardGivenColumn)
        {
            if (string.IsNullOrEmpty(dashboardGivenColumn.ColumnName)|| dashboardGivenColumn.DashboardTableId==0)
            {
                ViewBag.ColumnAddError = "Error";
                return View("Settings");
            }
            else
            {
                if (DashboardGivenColumn_Interface.SaveDashboardGivenColumn(dashboardGivenColumn))
                {
                    Session["Success"] = dashboardGivenColumn.ColumnName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = dashboardGivenColumn.ColumnName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Settings");
            }
        }

        [HttpGet]
        public ActionResult ColumnEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Column couldn't be found please retry!!";
                return View("Settings");
            }
            else
            {
                if (DashboardGivenColumn_Interface.DashboardGivenColumn_Exists(uaid))
                {
                    ViewBag.EditColumnDropDown = "Drop";
                    return View("Settings");
                }
                else
                {
                    Session["Error"] = " Column couldn't be found please retry!!";
                    return View("Settings");
                }
            }
        }

        [HttpPost]
        public ActionResult ColumnUpdate(DashboardGivenColumn_Model dashboardGivenColumn)
        {
            if (string.IsNullOrEmpty(dashboardGivenColumn.ColumnName) || dashboardGivenColumn.DashboardTableId == 0)
            {
                ViewBag.UpdateColumnError = "Error";
                ViewBag.UpdateColumnData = dashboardGivenColumn.DashboardGivenColumnId;
                return View("Settings");
            }
            else
            {
                if (DashboardGivenColumn_Interface.UpdateDashboardGivenColumnn(dashboardGivenColumn))
                {
                    Session["Success"] = dashboardGivenColumn.ColumnName + " updated successfully!!";
                    return RedirectToAction("Settings");
                }
                else
                {
                    Session["Error"] = dashboardGivenColumn.ColumnName + " couldn't be updated please retry!!";
                    return View("Settings");
                }

            }
        }

        [HttpPost]
        public ActionResult ColumnDelete(DashboardGivenColumn_Model dashboardGivenColumn)
        {
            var Column_Name = dashboardGivenColumn.ColumnName;
            try
            {
                if (DashboardGivenColumn_Interface.DeleteDashboardGivenColumn(dashboardGivenColumn.DashboardGivenColumnId))
                {
                    return Json(Column_Name + " type has been deleted successfully");
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