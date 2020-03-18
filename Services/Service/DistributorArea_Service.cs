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
    public class DistributorArea_Service : DistributorArea_Interface
    {
        public bool Delete(long DistributorAreaId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DistributorAreas.Where(distArea => distArea.DistributonAreaID == DistributorAreaId).FirstOrDefault();
                    _context.DistributorAreas.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DistributorAreaExist(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from DistributorArea in _dbContext.DistributorAreas.Where(distArea => distArea.DistributonAreaID == id)
                                select new DistributorArea_Model()
                                {
                                    DistributonAreaID = DistributorArea.DistributonAreaID
                                }).FirstOrDefault();
                    if (id != data.DistributonAreaID)
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

        public DistributorArea_Model GetDistributorAreaById(long id)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DistributorAreas.Where(distArea => distArea.DistributonAreaID== id).Select(distArea => new DistributorArea_Model()
                    {
                        DistributonAreaID = distArea.DistributonAreaID,
                        DistrubitorID = distArea.DistrubitorID,
                        City = distArea.City,
                        State = distArea.State,
                        District = distArea.District,
                        Address = distArea.Address,
                        Latitude = distArea.Latitude,
                        Longitude = distArea.Longitude,
                        IsDeleted = distArea.IsDeleted
                    }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int GetNewDistributorAreaID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.DistributorAreas.Max(distArea => distArea.DistributonAreaID);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<DistributorArea_Model> ListAllData()
        {
            using(var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from DistributorArea in _context.DistributorAreas
                                join dist in _context.Distributors on DistributorArea.DistrubitorID equals dist.DistrubitorID
                                select new DistributorArea_Model()
                                {
                                    DistributonAreaID = DistributorArea.DistributonAreaID,
                                    DistrubitorID = DistributorArea.DistrubitorID,
                                    DistributorName = dist.DistrubitorName,
                                    State = DistributorArea.State,
                                    District = DistributorArea.District,
                                    City = DistributorArea.City,
                                    Address = DistributorArea.Address,
                                    Latitude = DistributorArea.Latitude,
                                    Longitude = DistributorArea.Longitude,
                                    IsDeleted = DistributorArea.IsDeleted,
                                }
                                ).ToList().OrderBy(DistributorArea => DistributorArea.State).ToList();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Save(DistributorArea_Model distArea)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new DistributorArea()
                    {
                        DistributonAreaID = GetNewDistributorAreaID(),
                        DistrubitorID = distArea.DistrubitorID,
                        City = distArea.City,
                        State = distArea.State,
                        District = distArea.District,
                        Address = distArea.Address,
                        Latitude = distArea.Latitude,
                        Longitude = distArea.Longitude
                    };
                    _context.DistributorAreas.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Update(DistributorArea_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.DistributorAreas.Where(distArea => distArea.DistributonAreaID == model.DistributonAreaID).FirstOrDefault();
                    data.DistributonAreaID = model.DistributonAreaID;
                    data.DistrubitorID = model.DistrubitorID;
                    data.State = model.State;
                    data.District = model.District;
                    data.City = model.City;
                    data.Address = model.Address;
                    data.Latitude = model.Latitude;
                    data.Longitude = model.Longitude;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    //throw;
                }
            }
        }
    }
}
