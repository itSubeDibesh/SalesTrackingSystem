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
                    var data = _context.Products.Where(Products => Products.ProductID== ProductId).FirstOrDefault();
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
                    var data = _context.Products.Max(pC => pC.ProductID);
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
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Products.Where(products => products.ProductID == id).Select(products => new Products_Model()
                    {
                        ProductID = products.ProductID,
                        ProductCategoryID = products.ProductCategoryID,
                        ProductName = products.ProductName,
                        Description = products.Description,
                        PackRate = products.PackRate,
                        PackSize = products.PackSize
                    }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
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
                                join ProductCategory in _dbContext.ProductCategories on product.ProductCategoryID equals ProductCategory.ProductCategoryID
                                join Batch in _dbContext.Batches on product.ProductID equals Batch.ProductID
                                select new Products_Model()
                                {
                                    ProductID = product.ProductID,
                                    ProductName = product.ProductName,
                                    ProductCategoryID = product.ProductCategoryID,
                                    ProductCategoryName = ProductCategory.ProductCategoryName,
                                    Description = product.Description,
                                    PackRate = product.PackRate,
                                    PackSize = product.PackSize,
                                    StockLeft = Batch.StockLeft

                                }).ToList().OrderBy(product => product.ProductCategoryName).ToList();
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
                    var data = (from Product in _dbContext.Products.Where(ProductAction => ProductAction.ProductID == id)
                                select new Products_Model()
                                {
                                    ProductID = Product.ProductID,
                                    ProductName = Product.ProductName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.ProductName) && id != data.ProductID)
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
                        ProductCategoryID = model.ProductCategoryID,
                        ProductName = model.ProductName,
                        Description = model.Description,
                        PackRate = model.PackRate,
                        PackSize = model.PackSize
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
                    var data = _context.Products.Where(Product => Product.ProductID == model.ProductID).FirstOrDefault();
                    data.ProductCategoryID = model.ProductCategoryID;
                    data.ProductName = model.ProductName;
                    data.Description = model.Description;
                    data.PackRate = model.PackRate;
                    data.PackSize = model.PackSize;
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
