using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Reseller_Interface
    {
        List<Reseller_Model> ListAllData();
        bool Save(Reseller_Model model);
        bool Update(Reseller_Model model);
        int GetNewResellerId();
        bool Delete(long ResellerId);
        Reseller_Model GetResellerById(long id);
        bool ResellerExist(long id);
    }
}
