using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Unit_Interface
    {
        List<Unit_Model> DisplayTable();
        bool Save(Unit_Model unit_Model);
        bool UpdateUnit(Unit_Model unit_Model);
        int GetNewUnitID();
        bool Delete(long unitID);
        Unit_Model GetUnitById(long unitID);
        bool UnitExist(long unitID);
    }
}
