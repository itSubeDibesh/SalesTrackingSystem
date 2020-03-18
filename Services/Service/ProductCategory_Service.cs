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
    public class ProductCategory_Service : ProductCategory_Interface
    {
        public bool Delete(long ProductCategoryId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.ProductCategories.Where(ProductCategory => ProductCategory.ProductCategoryID== ProductCategoryId).FirstOrDefault();
                    _context.ProductCategories.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public int GetNewProductCategoryID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.ProductCategories.Max(pC => pC.ProductCategoryID);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public ProductCategory_Model GetProductCatgoryById(long id)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.ProductCategories.Where(productCategory => productCategory.ProductCategoryID == id).Select(productCategory => new ProductCategory_Model()
                    {
                        ProductCategoryID = productCategory.ProductCategoryID,
                        ProductCategoryName = productCategory.ProductCategoryName,
                        IsSubCategory = productCategory.IsSubCategory,
                        SubCategoryOf = productCategory.SubCategoryOf,
                        CategoryStatus = productCategory.CategoryStatus
                    }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<ProductCategory_Model> ListAllData()
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.ProductCategories.Select(ProductCategory => new ProductCategory_Model()
                    {
                        ProductCategoryID = ProductCategory.ProductCategoryID,
                        ProductCategoryName = ProductCategory.ProductCategoryName,
                        IsSubCategory = ProductCategory.IsSubCategory,
                        SubCategoryOf = ProductCategory.SubCategoryOf,
                        CategoryStatus = ProductCategory.CategoryStatus
                    }).ToList().OrderBy(ProductCategory=> ProductCategory.ProductCategoryName).ToList();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool ProductCategoryExist(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from ProductCategory in _dbContext.ProductCategories.Where(ProductAction => ProductAction.ProductCategoryID == id)
                                select new ProductCategory_Model()
                                {
                                    ProductCategoryID = ProductCategory.ProductCategoryID,
                                    ProductCategoryName = ProductCategory.ProductCategoryName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.ProductCategoryName) && id != data.ProductCategoryID)
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

        public bool Save(ProductCategory_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new ProductCategory()
                    {                        
                        ProductCategoryName = model.ProductCategoryName,
                        IsSubCategory = model.IsSubCategory,
                        SubCategoryOf = model.SubCategoryOf,
                        CategoryStatus = model.CategoryStatus
                    };
                    _context.ProductCategories.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Update(ProductCategory_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.ProductCategories.Where(ProductCategory => ProductCategory.ProductCategoryID== model.ProductCategoryID).FirstOrDefault();
                    data.ProductCategoryName = model.ProductCategoryName;
                    data.IsSubCategory = model.IsSubCategory;
                    data.SubCategoryOf = model.SubCategoryOf;
                    data.CategoryStatus = model.CategoryStatus;
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
