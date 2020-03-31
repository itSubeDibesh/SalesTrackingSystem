using DataAccessLayer;
using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class Transaction_Service : Transaction_Interface
    {
        long TransactionId, productId;
        decimal QuantitySum;
        public bool DeleteTransaction(long TransactionID)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Transactions.Where(trans => trans.TransactionID == TransactionID).FirstOrDefault();
                    _context.Transactions.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Transaction_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from trans in _dbContext.Transactions
                                select new Transaction_Model()
                                {
                                    TransactionID = trans.TransactionID,
                                    SupplierID = trans.SupplierID,
                                    ReceiverID = trans.ReceiverID,
                                    InvoiceEntryDate = trans.InvoiceEntryDate,
                                    InvoiceNo = trans.InvoiceNo,
                                    Balance = trans.Balance,
                                    DateCreated = trans.DateCreated,
                                    DateUpdated = trans.DateUpdated,
                                    DiscountPercent = trans.DiscountPercent,
                                    TaxPercent = trans.TaxPercent,
                                    TransactionLevel = trans.TransactionLevel
                                }).ToList().OrderByDescending(trans => trans.InvoiceEntryDate).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //public long GetNewTransactionDetailsID()
        //{
        //    try
        //    {
        //        using (var _context = new SalesTrackingSystemEntities())
        //        {
        //            var data = _context.TransactionDetails.Max(t => t.TransactionDetailsID);
        //            Int64 id = Convert.ToInt64(data) + 1;
        //            return id;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return 1;
        //    }
        //}

        public long GetNewTransactionID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Transactions.Max(t => t.TransactionID);
                    Int64 id = Convert.ToInt64(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public Int64 SaveTransaction(Transaction_Model transaction)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {                            
                try
                {
                    var data = new Transaction()
                    {
                        TransactionID = GetNewTransactionID(),
                        TransactionLevel= transaction.TransactionLevel,
                        SupplierID= transaction.SupplierID,
                        ReceiverID= transaction.ReceiverID,
                        InvoiceNo= transaction.InvoiceNo,
                        InvoiceDate= transaction.InvoiceDate,
                        InvoiceEntryDate = DateTime.Now,
                        DiscountPercent = transaction.DiscountPercent,
                        TaxPercent=transaction.TaxPercent,
                        DateCreated = DateTime.Now
                    };
                    _dbContext.Transactions.Add(data);
                    _dbContext.SaveChanges();
                    var transactionID = data.TransactionID;
                    return transactionID;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool SaveTransactionDetails(List<TransactionDetail> transactionDetail)
        {
           
            using (SalesTrackingSystemEntities db_Context = new SalesTrackingSystemEntities())
            {
                using (DbContextTransaction db = db_Context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (TransactionDetail TDItem in transactionDetail)
                        {
                            db_Context.TransactionDetails.Add(TDItem);
                        }                                            
                        db_Context.SaveChanges();

                        Batch_Interface batch_ = new Batch_Service();

                        foreach (TransactionDetail item in transactionDetail)
                        {
                            TransactionId = Convert.ToInt64(item.TransactionID);
                            QuantitySum += item.Quantity;
                            productId = Convert.ToInt64(item.ProductID);
                            batch_.SubtractStockLeft(item.Quantity, Convert.ToInt64(item.ProductID));                           
                        }
                        decimal MamPrice = batch_.MaxPriceByProductID(productId);
                        decimal balance = QuantitySum * MamPrice;
                        UpateBalance(balance, TransactionId);
                        db.Commit();
                        return true;
                    }
                    catch (DbEntityValidationException)
                    {                       
                        db.Rollback();
                        DeleteTransaction(TransactionId);
                        return false;
                    }
                }
            }
        }

        public Transaction_Model TransactionByID(long TransactionID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from trans in _dbContext.Transactions.Where(trans => trans.TransactionID == TransactionID)
                                select new Transaction_Model()
                                {
                                    TransactionID = trans.TransactionID,
                                    SupplierID = trans.SupplierID,
                                    ReceiverID = trans.ReceiverID,
                                    InvoiceEntryDate = trans.InvoiceEntryDate,
                                    InvoiceNo = trans.InvoiceNo,
                                    Balance = trans.Balance,
                                    DateCreated = trans.DateCreated,
                                    DateUpdated = trans.DateUpdated,
                                    DiscountPercent = trans.DiscountPercent,
                                    TaxPercent = trans.TaxPercent,
                                    TransactionLevel = trans.TransactionLevel
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<TransactionDetail_Model> TransactionDetailsByID(long TransactionID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from trans in _dbContext.TransactionDetails.Where(trans => trans.TransactionID == TransactionID)
                                select new TransactionDetail_Model()
                                {
                                    TransactionDetailsID=trans.TransactionDetailsID,
                                    TransactionID = trans.TransactionID,
                                    ProductID = trans.ProductID,
                                    Quantity=trans.Quantity,
                                    DateCreated = trans.DateCreated,
                                    DateUpdated = trans.DateUpdated
                                }).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool TransactionExists(long TransactionID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from trans in _dbContext.Transactions.Where(trans => trans.TransactionID == TransactionID)
                                select new Transaction_Model()
                                {
                                    TransactionID = trans.TransactionID,

                                }).FirstOrDefault();
                    if (TransactionID != data.TransactionID)
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

        public bool UpateBalance(decimal balance, long transactionID)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Transactions.Where(trans => trans.TransactionID == transactionID).FirstOrDefault();
                    data.Balance = balance;
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
