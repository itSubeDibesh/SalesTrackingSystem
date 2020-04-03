using DataAccessLayer;
using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class Product_Service : Product_Interface
    {
        public bool Delete(long ProductId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Products.Where(prod => prod.ProductID == ProductId).FirstOrDefault();
                    _context.Products.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public int GetNewProductID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Products.Max(prod => prod.ProductID);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public Products_Model GetProductById(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from product in _dbContext.Products.Where(prod =>prod.ProductID==id)
                                join unit in _dbContext.Units on product.UnitId equals unit.UnitId
                                join productCategory in _dbContext.ProductCategories on product.ProductCategoryID equals productCategory.ProductCategoryID
                                select new Products_Model()
                                {
                                    ProductID = product.ProductID,
                                    ProductCategoryID = product.ProductCategoryID,
                                    ProductCategoryName = productCategory.ProductCategoryName,
                                    ProductName = product.ProductName,
                                    Description = product.Description,
                                    PackRate = product.PackRate,
                                    UnitId = product.UnitId,
                                    UnitAbb = unit.UnitAbb,
                                    DateCreated = product.DateCreated,
                                    DateUpdated = product.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string GetProductNameById(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from product in _dbContext.Products.Where(prod => prod.ProductID == id)                               
                                select new Products_Model()
                                {                                                               
                                    ProductName = product.ProductName,                                   
                                }).FirstOrDefault();
                    if (data.ProductName!=null)
                    {
                        return data.ProductName;
                    }
                    else
                    {
                        return "Na";
                    }
                    
                }
                catch (Exception)
                {
                    return "Na";
                }
            }
        }

        public List<Products_Model> ListAllData()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from product in _dbContext.Products
                                join unit in _dbContext.Units on product.UnitId equals unit.UnitId
                                join productCategory in _dbContext.ProductCategories on product.ProductCategoryID equals productCategory.ProductCategoryID                         
                                select new Products_Model()
                                {
                                    ProductID = product.ProductID,
                                    ProductCategoryID = product.ProductCategoryID,
                                    ProductCategoryName= productCategory.ProductCategoryName,
                                    ProductName = product.ProductName,                                                                
                                    Description = product.Description,
                                    PackRate = product.PackRate,
                                    UnitId=product.UnitId,
                                    UnitAbb=unit.UnitAbb,
                                    DateCreated= product.DateCreated,
                                    DateUpdated= product.DateUpdated,                                 
                                }).ToList().OrderBy(product => product.ProductCategoryID).ToList();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool ProductExist(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from prod in _dbContext.Products.Where(prod => prod.ProductID == id)
                                select new Products_Model()
                                {
                                    ProductID = prod.ProductID,                               
                                }).FirstOrDefault();
                    if (id != data.ProductID)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Save(Products_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new Product()
                    {
                        ProductID = GetNewProductID(),  
                        ProductCategoryID= model.ProductCategoryID,
                        ProductName= model.ProductName,
                        PackRate= model.PackRate,
                        UnitId= model.UnitId,
                        Description = model.Description,
                        DateCreated = DateTime.Now
                    };
                    _context.Products.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Update(Products_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Products.Where(prod => prod.ProductID == model.ProductID).FirstOrDefault();
                    data.ProductID = model.ProductID;
                    data.ProductCategoryID = model.ProductCategoryID;
                    data.ProductName = model.ProductName;
                    data.PackRate = model.PackRate;
                    data.UnitId = model.UnitId;
                    data.Description = model.Description;
                    data.DateUpdated = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
