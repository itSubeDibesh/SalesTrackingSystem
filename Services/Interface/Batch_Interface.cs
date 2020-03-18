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
        bool Delete(long ProductId);
        Products_Model GetBatchById(long id);
        bool BatchExist(long id);
    }
}
