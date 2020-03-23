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
    public class DashboardType_Service : DashboardType_Interface
    {
        public DashboardType_Model DashboardTypeByID(long DashboardTypeId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from dashboardTypes in _dbContext.DashboardTypes.Where(dashboardTypes => dashboardTypes.DashboardTypeID == DashboardTypeId)
                                select new DashboardType_Model()
                                {
                                    DashboardTypeID = dashboardTypes.DashboardTypeID,
                                    TypeName = dashboardTypes.TypeName,
                                    DateCreated = dashboardTypes.DateCreated,
                                    DateUpdated = dashboardTypes.DateUpdated
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool DashboardType_Exists(long DashboardTypeId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from dashboardTypes in _dbContext.DashboardTypes.Where(dashboardTypes => dashboardTypes.DashboardTypeID == DashboardTypeId)
                                select new DashboardType_Model()
                                {
                                    DashboardTypeID = dashboardTypes.DashboardTypeID
                                }).FirstOrDefault();
                    if (data.DashboardTypeID != DashboardTypeId)
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

        public bool DeleteDashboardType(long DashboardTypeId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DashboardTypes.Where(dashboardTypes => dashboardTypes.DashboardTypeID == DashboardTypeId).FirstOrDefault();
                    _context.DashboardTypes.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<DashboardType_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from type in _dbContext.DashboardTypes
                                select new DashboardType_Model()
                                {
                                    DashboardTypeID = type.DashboardTypeID,
                                    TypeName = type.TypeName,
                                    DateCreated = type.DateCreated,
                                    DateUpdated = type.DateUpdated
                                }).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public long GetNewDashboardID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.DashboardTypes.Max(u => u.DashboardTypeID);
                    Int64 id = Convert.ToInt64(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public bool SaveDashboardType(DashboardType_Model DashboardType)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new DashboardType()
                    {
                        DashboardTypeID = GetNewDashboardID(),
                        TypeName = DashboardType.TypeName,
                        DateCreated = DateTime.Now
                    };
                    _dbContext.DashboardTypes.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateDashboardType(DashboardType_Model DashboardType)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DashboardTypes.Where(dashboardType => dashboardType.DashboardTypeID == DashboardType.DashboardTypeID).FirstOrDefault();
                    data.DashboardTypeID = DashboardType.DashboardTypeID;
                    data.TypeName = DashboardType.TypeName;
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
