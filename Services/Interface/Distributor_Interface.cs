using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Distributor_Interface
    {
        List<Distributor_Model> ListAllData();
        bool Save(Distributor_Model model);
        bool Update(Distributor_Model model);
        int GetNewDistributorID();
        bool Delete(long DistributorId);
        Distributor_Model GetDistributorById(long id);
        bool DistributorExist(long id);
    }
}
