using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface DistributorArea_Interface
    {
        List<DistributorArea_Model> ListAllData();
        bool Save(DistributorArea_Model model);
        bool Update(DistributorArea_Model model);
        int GetNewDistributorAreaID();
        bool Delete(long DistributorId);
        DistributorArea_Model GetDistributorAreaById(long id);
        bool DistributorAreaExist(long id);
    }
}
