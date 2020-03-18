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
    public class ProductsController : Controller
    {
        ProductCategory_Interface productCategoryService;
        Product_Interface productService;
        Batch_Interface batchService;
        // GET: Products
        public ProductsController()
        {
            productCategoryService = new ProductCategory_Service();
            productService = new Product_Service();
            batchService = new Batch_Service();
        }

        public ActionResult Unit()
        {
            return View();
        }
        public ActionResult Batch()
        {
            return View("Batch");
        }
        [HttpPost]
        public ActionResult AddBatch(Batch_Model batch_Model)
        {
            if (batch_Model.BatchName== null || batch_Model.QunatityProduced <= 0 || batch_Model.UnitPrice<=0 || batch_Model.StockLeft <= 0 || batch_Model.UnitPrice <= 0)
            {
                ViewBag.AddBatch = "Error";
                return View("Batch");
            }
            else
            {
                if (batchService.Save(batch_Model))
                {
                    Session["Success"] = "Batch inserted succcessfully";
                    return RedirectToAction("Batch");
                }
                else
                {
                    Session["Error"] = "Error occured!!";
                    return View("Batch");
                }
            }
        }
        public ActionResult Products()
        {            
            return View("Products");
        }
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
        public ActionResult EditProductCategory(string action,long pC)
        {
            if (string.IsNullOrEmpty(action) && pC == 0)
            {
                Session["Error"] = " ProductCategory couldn't be found please retry!!";
                return RedirectToAction("Products");
            }
            else
            {
                var productCategoryData = productCategoryService.GetProductCatgoryById(pC);
                if(productCategoryData!=null)
                {
                    ViewBag.EditDropDown = "Drop";
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
        public ActionResult EditProductCategory(ProductCategory_Model productCategory)
        {
            if (string.IsNullOrWhiteSpace(productCategory.ProductCategoryName) || productCategory.CategoryStatus== null)
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
        /*ProductCategory Ends*/
        
        /*Product*/
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
        public ActionResult EditProduct(string action, long p)
        {
            if (string.IsNullOrEmpty(action) && p == 0)
            {
                Session["Error"] = " ProductCategory couldn't be found please retry!!";
                return RedirectToAction("Products");
            }
            else
            {
                var productCategoryData = productService.GetProductById(p);
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
            if (string.IsNullOrWhiteSpace(model.ProductName) || model.ProductCategoryID== 0)
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
        /*Product Ends*/
    }
}