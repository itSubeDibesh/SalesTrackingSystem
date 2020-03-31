using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Transaction_Interface
    {
        List<Transaction_Model> DisplayTable();
        Int64 SaveTransaction(Transaction_Model transaction);
        bool SaveTransactionDetails(List<TransactionDetail> transactionDetail);
        bool TransactionExists(Int64 TransactionID);
        Transaction_Model TransactionByID(Int64 TransactionID);     
        bool DeleteTransaction(Int64 TransactionID);
        Int64 GetNewTransactionID();
        bool UpateBalance(decimal balance,long transactionID);
        List<TransactionDetail_Model> TransactionDetailsByID(Int64 TransactionID);
        //Int64 GetNewTransactionDetailsID();
    }
}
