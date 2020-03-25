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
    public class Reseller_Service : Reseller_Interface
    {
        public bool Delete(long ResellerId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Resellers.Where(res => res.ResellerID== ResellerId).FirstOrDefault();
                    _context.Resellers.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public int GetNewResellerId()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Resellers.Max(dist => dist.ResellerID);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public Reseller_Model GetResellerById(long id)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Resellers.Where(res => res.ResellerID== id).Select(res => new Reseller_Model()
                    {
                        ResellerID= res.ResellerID,
                        ResellerName = res.ResellerName,
                        OwnerName = res.OwnerName,
                        RegestrationID = res.RegestrationID,
                        DistrubitorID = res.DistrubitorID,
                        Mobile = res.Mobile,
                        Phone = res.Phone,                        
                        Email = res.Email,
                        State = res.State,
                        District = res.District,
                        Address = res.Address,
                        Latitude = res.Latitude,
                        Longitude = res.Longitude
                    }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Reseller_Model> ListAllData()
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from res in _context.Resellers
                                join dist in _context.Distributors on res.DistrubitorID equals dist.DistrubitorID
                                select new Reseller_Model()
                                {
                                    ResellerID = res.ResellerID,
                                    ResellerName = res.ResellerName,
                                    OwnerName = res.OwnerName,
                                    RegestrationID = res.RegestrationID,
                                    DistrubitorID = res.DistrubitorID,
                                    DistributorName = dist.DistrubitorName,
                                    Mobile = res.Mobile,
                                    Phone = res.Phone,
                                    Email = res.Email,
                                    State = res.State,
                                    District = res.District,
                                    Address = res.Address,
                                    Latitude = res.Latitude,
                                    Longitude = res.Longitude
                                }).ToList();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool ResellerExist(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from Reseller in _dbContext.Resellers.Where(res => res.ResellerID== id)
                                select new Reseller_Model()
                                {
                                    ResellerID = Reseller.ResellerID,
                                    ResellerName = Reseller.ResellerName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.ResellerName) && id != data.ResellerID)
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

        public bool Save(Reseller_Model res)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new Reseller()
                    {
                        ResellerID = GetNewResellerId(),
                        ResellerName = res.ResellerName,
                        OwnerName = res.OwnerName,
                        RegestrationID = res.RegestrationID,
                        DistrubitorID = res.DistrubitorID,
                        Mobile = res.Mobile,
                        Phone = res.Phone,
                        Email = res.Email,
                        State = res.State,
                        District = res.District,
                        Address = res.Address,
                        Latitude = res.Latitude,
                        Longitude = res.Longitude
                    };
                    _context.Resellers.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Update(Reseller_Model res)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Resellers.Where(reseller => reseller.ResellerID== res.ResellerID).FirstOrDefault();
                    data.ResellerID = res.ResellerID;
                    data.ResellerName = res.ResellerName;
                    data.OwnerName = res.OwnerName;
                    data.RegestrationID = res.RegestrationID;
                    data.DistrubitorID = res.DistrubitorID;
                    data.Mobile = res.Mobile;
                    data.Phone = res.Phone;
                    data.Email = res.Email;
                    data.State = res.State;
                    data.District = res.District;
                    data.Address = res.Address;
                    data.Latitude = res.Latitude;
                    data.Longitude = res.Longitude;
                    
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
