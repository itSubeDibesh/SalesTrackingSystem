using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Batch_Interface
    {
        List<Batch_Model> ListAllData();
        bool Save(Batch_Model model);
        bool Update(Batch_Model model);
        int GetNewBatchID();
        bool Delete(long batchID);
        Batch_Model GetBatchById(long batchID);
        bool BatchExist(long batchID);
        string StockLeftByProduct(long productID);
        bool SubtractStockLeft(decimal stockAmount, long productID);
        decimal MaxPriceByProductID(long ProductID);
    }
}
