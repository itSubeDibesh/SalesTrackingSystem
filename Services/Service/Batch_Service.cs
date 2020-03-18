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
    public class Batch_Service : Batch_Interface
    {
        public bool BatchExist(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long ProductId)
        {
            throw new NotImplementedException();
        }

        public Products_Model GetBatchById(long id)
        {
            throw new NotImplementedException();
        }

        public int GetNewBatchID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Batches.Max(Batch => Batch.BatchID);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<Batch_Model> ListAllData()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from Batch in _dbContext.Batches
                                join ProductCategory in _dbContext.ProductCategories on Batch.ProductCategoryId equals ProductCategory.ProductCategoryID
                                join Product in _dbContext.Products on Batch.ProductID equals Product.ProductID
                                select new Batch_Model()
                                {
                                    ProductID = Batch.ProductID,
                                    ProductName = Product.ProductName,
                                    ProductCategoryId = Batch.ProductCategoryId,
                                    ProductCategoryName = ProductCategory.ProductCategoryName,
                                    BatchID = Batch.BatchID,
                                    BatchName = Batch.BatchName,
                                    QunatityProduced = Batch.QunatityProduced,
                                    UnitPrice = Batch.UnitPrice,
                                    StockLeft = Batch.StockLeft,
                                    DateProduced = Batch.DateProduced,
                                    ExpiryDate = Batch.ExpiryDate
                                }).ToList().OrderBy(batch => batch.BatchName).ToList();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Save(Batch_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new Batch()
                    {
                        BatchID = GetNewBatchID(),
                        BatchName = model.BatchName,                        
                        ProductCategoryId = model.ProductCategoryId,
                        ProductID = model.ProductID,
                        QunatityProduced = model.QunatityProduced,
                        UnitPrice = model.UnitPrice,
                        StockLeft= model.StockLeft,
                        DateProduced= model.DateProduced,
                        ExpiryDate= model.ExpiryDate
                    };
                    _context.Batches.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Update(Batch_Model model)
        {
            throw new NotImplementedException();
        }
    }
}
