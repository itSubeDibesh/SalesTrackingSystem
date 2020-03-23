using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface DashboardGivenColumn_Interface
    {
        List<DashboardGivenColumn_Model> DisplayTable();
        bool SaveDashboardGivenColumn(DashboardGivenColumn_Model GivenColumn);
        bool DashboardGivenColumn_Exists(Int64 GivenColumnId);
        DashboardGivenColumn_Model DashboardGivenColumnByID(Int64 GivenColumnId);
        bool UpdateDashboardGivenColumnn(DashboardGivenColumn_Model GivenColumn);
        bool DeleteDashboardGivenColumn(Int64 GivenColumnId);
        Int64 GetNewDashboardGivenColumnID();
    }
}
