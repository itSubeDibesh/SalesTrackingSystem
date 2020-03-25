using DataAccessLayer;
using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class DashboardTable_Service : DashboardTable_Interface
    {
        public DashboardTable_Model DashboardTableByID(long DashboardTableId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from dashboardTable in _dbContext.DashboardTables.Where(dashboardTable => dashboardTable.DashboardTableId == DashboardTableId)                               
                                select new DashboardTable_Model()
                                {                                   
                                    DashboardTableId = dashboardTable.DashboardTableId,
                                    TableName = dashboardTable.TableName,                                  
                                    DateCreated = dashboardTable.DateCreated,
                                    DateUpdated = dashboardTable.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool DashboardTable_Exists(long DashboardTableId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from dashboardTable in _dbContext.DashboardTables.Where(dashboardTable => dashboardTable.DashboardTableId == DashboardTableId)
                                select new DashboardTable_Model()
                                {
                                    DashboardTableId = dashboardTable.DashboardTableId
                                }).FirstOrDefault();
                    if (data.DashboardTableId != DashboardTableId)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeleteDashboardTable(long DashboardTableId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DashboardTables.Where(dashboardTable => dashboardTable.DashboardTableId == DashboardTableId).FirstOrDefault();
                    _context.DashboardTables.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<DashboardTable_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from table in _dbContext.DashboardTables                              
                                select new DashboardTable_Model()
                                {
                                    DashboardTableId = table.DashboardTableId,
                                    TableName = table.TableName,
                                    DateCreated = table.DateCreated,
                                    DateUpdated = table.DateUpdated
                                }).ToList().OrderBy(table=> table.TableName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public long GetNewDashboardTableID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.DashboardTables.Max(u => u.DashboardTableId);
                    Int64 id = Convert.ToInt64(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool SaveDashboardTable(DashboardTable_Model DashboardTable)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new DashboardTable()
                    {                       
                        DashboardTableId = GetNewDashboardTableID(),
                        TableName = DashboardTable.TableName,
                        DateCreated = DateTime.Now
                    };
                    _dbContext.DashboardTables.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateDashboardTable(DashboardTable_Model DashboardTable)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DashboardTables.Where(dashboardTable => dashboardTable.DashboardTableId == DashboardTable.DashboardTableId).FirstOrDefault();
                    data.DashboardTableId = DashboardTable.DashboardTableId;
                    data.TableName = DashboardTable.TableName;
                    data.DateUpdated = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
