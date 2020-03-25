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
    public class DashboardGivenColumn_Service : DashboardGivenColumn_Interface
    {
        public DashboardGivenColumn_Model DashboardGivenColumnByID(long GivenColumnId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from givenColumn in _dbContext.DashboardGivenColumns.Where(givenColumn => givenColumn.DashboardGivenColumnId == GivenColumnId)
                                join table in _dbContext.DashboardTables on givenColumn.DashboardTableId equals table.DashboardTableId
                                select new DashboardGivenColumn_Model()
                                {
                                    DashboardGivenColumnId = givenColumn.DashboardGivenColumnId,
                                    DashboardTableId = table.DashboardTableId,
                                    TableName = table.TableName,
                                    ColumnName = givenColumn.ColumnName,
                                    DateCreated = givenColumn.DateCreated,
                                    DateUpdated = givenColumn.DateUpdated
                                }).FirstOrDefault();                  
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool DashboardGivenColumn_Exists(long GivenColumnId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from givenColumn in _dbContext.DashboardGivenColumns.Where(givenColumn => givenColumn.DashboardGivenColumnId == GivenColumnId)
                                select new DashboardGivenColumn_Model()
                                {
                                    DashboardGivenColumnId=givenColumn.DashboardGivenColumnId                                   
                                }).FirstOrDefault();
                    if (data.DashboardGivenColumnId != GivenColumnId)
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

        public bool DeleteDashboardGivenColumn(long GivenColumnId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DashboardGivenColumns.Where(GivenColumns => GivenColumns.DashboardGivenColumnId == GivenColumnId).FirstOrDefault();
                    _context.DashboardGivenColumns.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<DashboardGivenColumn_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from givenColumn in _dbContext.DashboardGivenColumns
                                join table in _dbContext.DashboardTables on givenColumn.DashboardTableId equals table.DashboardTableId
                                select new DashboardGivenColumn_Model()
                                {
                                    DashboardGivenColumnId=givenColumn.DashboardGivenColumnId,
                                    DashboardTableId=table.DashboardTableId,
                                    TableName=table.TableName,
                                    ColumnName=givenColumn.ColumnName,
                                    DateCreated=givenColumn.DateCreated,
                                    DateUpdated=givenColumn.DateUpdated
                                }).ToList().OrderBy(givenColumn=> givenColumn.ColumnName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public long GetNewDashboardGivenColumnID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.DashboardGivenColumns.Max(u => u.DashboardGivenColumnId);
                    Int64 id = Convert.ToInt64(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool SaveDashboardGivenColumn(DashboardGivenColumn_Model GivenColumn)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new DashboardGivenColumn()
                    {
                        DashboardGivenColumnId = GetNewDashboardGivenColumnID(),
                        DashboardTableId = GivenColumn.DashboardTableId,                      
                        ColumnName = GivenColumn.ColumnName,
                        DateCreated =DateTime.Now                      
                    };
                    _dbContext.DashboardGivenColumns.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateDashboardGivenColumnn(DashboardGivenColumn_Model GivenColumn)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DashboardGivenColumns.Where(givenColumn => givenColumn.DashboardGivenColumnId == GivenColumn.DashboardGivenColumnId).FirstOrDefault();        
                    data.DashboardTableId = GivenColumn.DashboardTableId;
                    data.ColumnName = GivenColumn.ColumnName;
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
