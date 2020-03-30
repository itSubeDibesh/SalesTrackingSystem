using DataAccessLayer;
using Models;
using Newtonsoft.Json.Linq;
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
    public class TransactionController : Controller
    {
        Transaction_Interface transaction_Interface;
        // GET: Transaction
       public  TransactionController()
        {
            transaction_Interface = new Transaction_Service();
        }
        public ActionResult Transaction()
        {
            return View("Transaction");
        }

        [HttpPost]
        public ActionResult TransactionAdd(Transaction_Model transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.InvoiceNo)|| transaction.ReceiverID==0)
            {
                ViewBag.AddError = "Error";
                return View("Transaction");
            }
            else
            {
                Int64 transactionID = transaction_Interface.SaveTransaction(transaction);
                if (transactionID!=0)
                {
                    return Json(transactionID);                   
                }
                else
                {
                    Session["Error"] = "Transaction couldn't be done please retry!!";
                }
                return RedirectToAction("Transaction");
            }
        }
        [HttpPost]
        public ActionResult TransactionDetailsAdd(List<TransactionDetail> transactionDetaila)
        {
            if (transactionDetaila == null)
            {
                ViewBag.AddError = "Error";
                return View("Transaction");
            }
            else
            {                     
                if (transaction_Interface.SaveTransactionDetails(transactionDetaila))
                {
                    Session["Success"] = "Transaction done successfully!!";
                }
                else
                {
                    Session["Error"] = "Transaction couldn't be done please retry!!";
                }
                return Json("Transaction done successfully");
            }
        }
    }
}