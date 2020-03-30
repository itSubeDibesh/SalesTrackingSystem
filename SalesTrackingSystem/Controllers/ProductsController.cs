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
    public class ProductsController : Controller
    {
        Unit_Interface unit_;
        ProductCategory_Interface productCategoryService;
        Product_Interface productService;
        Batch_Interface Batch_;             

        public ProductsController()
        {
            unit_ = new Unit_Service();
            productCategoryService = new ProductCategory_Service();
            productService = new Product_Service();
            Batch_ = new Batch_Service();                     
        }

        #region//Batch
        public ActionResult Batch()
        {
            return View("Batch");
        }
        [HttpPost]
        public ActionResult BatchAdd(Batch_Model batch_Model)
        {
            if (string.IsNullOrEmpty(batch_Model.BatchName))
            {
                ViewBag.AddBatchError = "Error";
                return View("Batch");
            }
            else
            {
                if (Batch_.Save(batch_Model))
                {
                    Session["Success"] = batch_Model.BatchName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = batch_Model.BatchName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Batch");
            }
        }

        [HttpGet]
        public ActionResult BatchEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Batch couldn't be found please retry!!";
                return View("Batch");
            }
            else
            {
                if (unit_.UnitExist(uaid))
                {
                    ViewBag.EditBatchDropDown = "Drop";
                    return View("Batch");
                }
                else
                {
                    Session["Error"] = " Batch couldn't be found please retry!!";
                    return View("Batch");
                }
            }
        }

        [HttpPost]
        public ActionResult BatchUpdate(Batch_Model batch_Model)
        {
            if (string.IsNullOrEmpty(batch_Model.BatchName))
            {
                ViewBag.UpdateBatchError = "Error";
                ViewBag.UpdateBatchData = batch_Model.BatchID;
                return View("Batch");
            }
            else
            {
                if (Batch_.Update(batch_Model))
                {
                    Session["Success"] = batch_Model.BatchName + " updated successfully!!";
                    return RedirectToAction("Batch");
                }
                else
                {
                    Session["Error"] = batch_Model.BatchName + " couldn't be updated please retry!!";
                    return View("Batch");
                }

            }
        }

        [HttpPost]
        public ActionResult BatchDelete(Batch_Model batch_Model)
        {
            var Batch_Name = batch_Model.BatchName;
            try
            {
                if (Batch_.Delete(batch_Model.BatchID))
                {
                    return Json(Batch_Name + " batch has been deleted successfully");
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
        #endregion

        #region//Products
        public ActionResult Products()
        {
            return View("Products");
        }
             
        [HttpPost]
        public ActionResult AddProduct(Products_Model product_Model)
        {

            if (product_Model.ProductName == null || product_Model.ProductCategoryID == null)
            {
                ViewBag.AddProduct = "Error";
                return View("Products");
            }
            else
            {
                if (productService.Save(product_Model))
                {
                    Session["Success"] = "Product Category inserted succcessfully";
                    return RedirectToAction("Products");
                }
                else
                {
                    Session["Error"] = "Error occured!!";
                    return View("Products");
                }
            }
        }
        [HttpGet]
        public ActionResult EditProduct(string action, long prod)
        {
            if (string.IsNullOrEmpty(action) || prod == 0)
            {
                Session["Error"] = " ProductCategory couldn't be found please retry!!";
                return RedirectToAction("Products");
            }
            else
            {
                var productCategoryData = productService.GetProductById(prod);
                if (productCategoryData != null)
                {
                    ViewBag.EditProductDropDown = "Drop";
                    return View("Products");
                }
                else
                {
                    Session["Error"] = " Module couldn't be found please retry!!";
                    return View("Products");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateProduct(Products_Model model)
        {
            if (string.IsNullOrWhiteSpace(model.ProductName) || model.ProductCategoryID == 0)
            {
                ViewBag.ProductUpdateError = "Error";
                ViewBag.UpdateProductData = model.ProductID;
                return View("Products");
            }
            else
            {
                if (productService.Update(model))
                {
                    Session["Success"] = model.ProductName + " updated successfully!!";
                    return RedirectToAction("Products");
                }
                else
                {
                    Session["Error"] = model.ProductName + " couldn't be updated please retry!!";
                    return View("Products");
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(Products_Model model)
        {
            try
            {
                if (productService.Delete(model.ProductID))
                {
                    return Json(model.ProductName + " has been deleted successfully");
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

        #endregion

        #region//Product Category

        [HttpPost]
        public ActionResult AddProductCategory(ProductCategory_Model productCategory_Model)
        {
            if (productCategory_Model.ProductCategoryName == null || productCategory_Model.CategoryStatus == null)
            {
                ViewBag.AddProductCategory = "Error";
                return View("Products");
            }
            else
            {
                if (productCategoryService.Save(productCategory_Model))
                {
                    Session["Success"] = "Product Category inserted succcessfully";
                    return RedirectToAction("Products");
                }
                else
                {
                    Session["Error"] = "Error occured!!";
                    return View("Products");
                }
            }
        }

        [HttpGet]
        public ActionResult EditProductCategory(string action, long pC)
        {
            if (string.IsNullOrEmpty(action) && pC == 0)
            {
                Session["Error"] = " Product Category couldn't be found please retry!!";
                return RedirectToAction("Products");
            }
            else
            {
                var productCategoryData = productCategoryService.GetProductCatgoryById(pC);
                if (productCategoryData != null)
                {
                    ViewBag.EditDropDown = "Drop";
                    return View("Products");
                }
                else
                {
                    Session["Error"] = " Product Category couldn't be found please retry!!";
                    return View("Products");
                }
            }
        }
        [HttpPost]
        public ActionResult EditProductCategory(ProductCategory_Model productCategory)
        {
            if (string.IsNullOrWhiteSpace(productCategory.ProductCategoryName) || productCategory.CategoryStatus == null)
            {
                ViewBag.UpdateError = "Error";
                ViewBag.UpdateProductCatData = productCategory.ProductCategoryID;
                return View("Products");
            }
            else
            {
                if (productCategoryService.Update(productCategory))
                {
                    Session["Success"] = productCategory.ProductCategoryName + " updated successfully!!";
                    return RedirectToAction("Products");
                }
                else
                {
                    Session["Error"] = productCategory.ProductCategoryName + " couldn't be updated please retry!!";
                    return View("Products");
                }
            }
        }
        [HttpPost]
        public ActionResult ProductCategoryDelete(ProductCategory_Model model)
        {
            try
            {
                if (productCategoryService.Delete(model.ProductCategoryID))
                {
                    return Json(model.ProductCategoryName + " has been deleted successfully");
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

        #endregion

        #region//Unit
        public ActionResult Unit()
        {
            return View("Unit");
        }

        [HttpPost]
        public ActionResult UnitAdd(Unit_Model unit_Model)
        {
            if (string.IsNullOrEmpty(unit_Model.UnitName) || string.IsNullOrEmpty(unit_Model.UnitAbb))
            {
                ViewBag.AddUnitError = "Error";
                return View("Unit");
            }
            else
            {
                if (unit_.Save(unit_Model))
                {
                    Session["Success"] = unit_Model.UnitName + " added successfully!!";
                }
                else
                {
                    Session["Error"] = unit_Model.UnitName + " couldn't be added please retry!!";
                }
                return RedirectToAction("Unit");
            }
        }

        [HttpGet]
        public ActionResult UnitEdit(string action, Int64 uaid)
        {
            if (string.IsNullOrEmpty(action) && uaid == 0)
            {
                Session["Error"] = " Unit couldn't be found please retry!!";
                return View("Unit");
            }
            else
            {
                if (unit_.UnitExist(uaid))
                {
                    ViewBag.EditUnitDropDown = "Drop";
                    return View("Unit");
                }
                else
                {
                    Session["Error"] = " Unit couldn't be found please retry!!";
                    return View("Unit");
                }
            }
        }

        [HttpPost]
        public ActionResult UnitUpdate(Unit_Model unit_Model)
        {
            if (string.IsNullOrEmpty(unit_Model.UnitName) || string.IsNullOrEmpty(unit_Model.UnitAbb))
            {
                ViewBag.UpdateUnitError = "Error";
                ViewBag.UpdateUnitData = unit_Model.UnitId;
                return View("Unit");
            }
            else
            {
                if (unit_.UpdateUnit(unit_Model))
                {
                    Session["Success"] = unit_Model.UnitName + " updated successfully!!";
                    return RedirectToAction("Unit");
                }
                else
                {
                    Session["Error"] = unit_Model.UnitName + " couldn't be updated please retry!!";
                    return View("Unit");
                }

            }
        }

        [HttpPost]
        public ActionResult UnitDelete(Unit_Model unit_Model)
        {
            var Unit_Name = unit_Model.UnitName;
            try
            {
                if (unit_.Delete(unit_Model.UnitId))
                {
                    return Json(Unit_Name + " unit has been deleted successfully");
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

        #endregion

    }
}