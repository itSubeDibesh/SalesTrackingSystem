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
        long TransactionId;
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
            throw new NotImplementedException();
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
                            batch_.SubtractStockLeft(item.Quantity, Convert.ToInt64(item.ProductID));                           
                        } 
                        
                        //sum quantity and find price from batch and multiply and store in balance

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
            throw new NotImplementedException();
        }

        public bool TransactionExists(long TransactionID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTransaction(Transaction_Model transaction)
        {
            throw new NotImplementedException();
        }
    }
}
