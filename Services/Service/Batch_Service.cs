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
        public bool BatchExist(long batchID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from batch in _dbContext.Batches.Where(batch => batch.BatchID == batchID)
                                select new Batch_Model()
                                {
                                    BatchID = batch.BatchID,
                                    BatchName = batch.BatchName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.BatchName) && batchID != data.BatchID)
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

        public bool Delete(long batchID)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Batches.Where(dist => dist.BatchID == batchID).FirstOrDefault();
                    _context.Batches.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Batch_Model GetBatchById(long batchID)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from batch in _context.Batches.Where(batch => batch.BatchID == batchID)
                                join product in _context.Products on batch.ProductID equals product.ProductID
                                join productCategory in _context.ProductCategories on batch.ProductCategoryId equals productCategory.ProductCategoryID
                                select new Batch_Model()
                                {
                                    BatchID = batch.BatchID,
                                    BatchName = batch.BatchName,
                                    ProductCategoryId = batch.ProductCategoryId,
                                    QunatityProduced = batch.QunatityProduced,
                                    UnitPrice = batch.UnitPrice,
                                    StockLeft = batch.StockLeft,
                                    ExpiryDate = batch.ExpiryDate,
                                    ProductName = product.ProductName,
                                    ProductID = batch.ProductID,
                                    ProductCategoryName = productCategory.ProductCategoryName,
                                    DateProduced = batch.DateProduced,
                                    DateCreated = batch.DateCreated,
                                    DateUpdated = batch.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
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
                    var data = (from batch in _dbContext.Batches
                                join product in _dbContext.Products on batch.ProductID equals product.ProductID
                                join productCategory in _dbContext.ProductCategories on batch.ProductCategoryId equals productCategory.ProductCategoryID
                                select new Batch_Model()
                                {
                                    BatchID = batch.BatchID,
                                    BatchName = batch.BatchName,
                                    ProductCategoryId = batch.ProductCategoryId,
                                    QunatityProduced = batch.QunatityProduced,
                                    UnitPrice = batch.UnitPrice,
                                    StockLeft = batch.StockLeft,
                                    ExpiryDate = batch.ExpiryDate,
                                    ProductName = product.ProductName,
                                    ProductID = batch.ProductID,
                                    ProductCategoryName = productCategory.ProductCategoryName,
                                    DateProduced = batch.DateProduced,
                                    DateCreated = batch.DateCreated,
                                    DateUpdated = batch.DateUpdated
                                }).ToList().OrderBy(batch => batch.BatchName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public decimal MaxPriceByProductID(long ProductID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    decimal MaxPrice = _dbContext.Batches.Where(bate => bate.ProductID == ProductID).Max(bat => bat.UnitPrice);
                    return MaxPrice;
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
                        StockLeft = Convert.ToInt64(model.QunatityProduced),
                        DateProduced = model.DateProduced,
                        ExpiryDate = model.ExpiryDate,
                        DateCreated = DateTime.Now
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

        public string StockLeftByProduct(long productID)
        {
            string StockLeft;
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    StockLeft = (_dbContext.Batches.Where(bate => bate.ProductID == productID).Sum(bat => bat.StockLeft)).ToString();
                    if (StockLeft == "")
                    {
                        StockLeft = "0";
                    }
                    return StockLeft;
                }
                catch (Exception)
                {
                    StockLeft = "0";
                    return StockLeft;
                }
            }
        }

        public bool SubtractStockLeft(decimal stockAmount, long productID)
        {         
            decimal stockLeft = Convert.ToDecimal(StockLeftByProduct(productID));
            if (stockLeft >= stockAmount)
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    decimal newFistbatchStockLeft;
                    var FirstBatchdata = (from batch in _context.Batches.Where(batch => batch.ProductID == productID && batch.StockLeft != 0)
                                            select new Batch_Model()
                                            {
                                                BatchID = batch.BatchID,
                                                StockLeft = batch.StockLeft,
                                            }).FirstOrDefault();
                    if (FirstBatchdata.StockLeft >= stockAmount)
                    {
                        newFistbatchStockLeft = Convert.ToDecimal(FirstBatchdata.StockLeft) - stockAmount;
                        var data = _context.Batches.Where(batch => batch.BatchID == FirstBatchdata.BatchID).FirstOrDefault();
                        data.StockLeft = newFistbatchStockLeft;
                        data.DateUpdated = DateTime.Now;
                        _context.SaveChanges();
                    }
                    else
                    {
                        newFistbatchStockLeft = Convert.ToDecimal(FirstBatchdata.StockLeft) - stockAmount;
                        var data = _context.Batches.Where(batch => batch.BatchID == FirstBatchdata.BatchID).FirstOrDefault();
                        data.StockLeft = newFistbatchStockLeft;
                        data.DateUpdated = DateTime.Now;
                        _context.SaveChanges();

                        var SecondtBatchdata = (from batch in _context.Batches.Where(batch => batch.ProductID == productID && batch.StockLeft != 0)
                                                select new Batch_Model()
                                                {
                                                    BatchID = batch.BatchID,
                                                    StockLeft = batch.StockLeft,
                                                }).FirstOrDefault();
                        var newSecondbatchStockLeft = Convert.ToDecimal(FirstBatchdata.StockLeft) - newFistbatchStockLeft;
                        var SecondData = _context.Batches.Where(batch => batch.BatchID == SecondtBatchdata.BatchID).FirstOrDefault();
                        data.StockLeft = newSecondbatchStockLeft;
                        data.DateUpdated = DateTime.Now;
                        _context.SaveChanges();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool Update(Batch_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Batches.Where(batch => batch.BatchID == model.BatchID).FirstOrDefault();
                    data.BatchName = model.BatchName;
                    data.ProductCategoryId = model.ProductCategoryId;
                    data.ProductID = model.ProductID;
                    data.QunatityProduced = model.QunatityProduced;
                    data.UnitPrice = model.UnitPrice;
                    data.StockLeft = model.StockLeft;
                    data.DateProduced = model.DateProduced;
                    data.ExpiryDate = model.ExpiryDate;
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
          
