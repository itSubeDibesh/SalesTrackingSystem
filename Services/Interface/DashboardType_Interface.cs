using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface DashboardType_Interface
    {
        List<DashboardType_Model> DisplayTable();
        bool SaveDashboardType(DashboardType_Model DashboardType);
        bool DashboardType_Exists(Int64 DashboardTypeId);
        DashboardType_Model DashboardTypeByID(Int64 DashboardTypeId);
        bool UpdateDashboardType(DashboardType_Model DashboardType);
        bool DeleteDashboardType(Int64 DashboardTypeId);
        Int64 GetNewDashboardID();
    }
}
