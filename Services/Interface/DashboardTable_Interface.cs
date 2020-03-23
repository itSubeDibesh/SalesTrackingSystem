using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface DashboardTable_Interface
    {
        List<DashboardTable_Model> DisplayTable();
        bool SaveDashboardTable(DashboardTable_Model DashboardTable);
        bool DashboardTable_Exists(Int64 DashboardTableId);
        DashboardTable_Model DashboardTableByID(Int64 DashboardTableId);
        bool UpdateDashboardTable(DashboardTable_Model DashboardTable);
        bool DeleteDashboardTable(Int64 DashboardTableId);
        Int64 GetNewDashboardTableID();
    }
}
