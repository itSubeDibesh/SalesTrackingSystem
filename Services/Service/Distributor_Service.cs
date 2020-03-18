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
    public class Distributor_Service : Distributor_Interface
    {
        public bool Delete(long DistributorId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Distributors.Where(dist => dist.DistrubitorID == DistributorId).FirstOrDefault();
                    _context.Distributors.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DistributorExist(long id)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from Distributor in _dbContext.Distributors.Where(dist => dist.DistrubitorID== id)
                                select new Distributor_Model()
                                {
                                    DistrubitorID = Distributor.DistrubitorID,
                                    DistrubitorName = Distributor.DistrubitorName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.DistrubitorName) && id != data.DistrubitorID)
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

        public Distributor_Model GetDistributorById(long id)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Distributors.Where(dist => dist.DistrubitorID== id).Select(dist => new Distributor_Model()
                    {
                        DistrubitorID = dist.DistrubitorID,
                        DistrubitorName = dist.DistrubitorName,
                        OwnerName = dist.OwnerName,
                        RegestrationID = dist.RegestrationID,
                        MobileNo = dist.MobileNo,
                        Phone = dist.Phone,
                        Fax = dist.Fax,
                        Email = dist.Email,
                        State = dist.State,
                        District = dist.District,
                        Address = dist.Address,
                        Latitude = dist.Latitude,
                        Longitude = dist.Longitude,
                        IsDeleted = dist.IsDeleted
                    }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int GetNewDistributorID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Distributors.Max(dist => dist.DistrubitorID);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<Distributor_Model> ListAllData()
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Distributors.Select(Distributor => new Distributor_Model()
                    {
                        DistrubitorID = Distributor.DistrubitorID,                        
                        DistrubitorName = Distributor.DistrubitorName,                        
                        OwnerName = Distributor.OwnerName,                        
                        RegestrationID = Distributor.RegestrationID,                        
                        MobileNo = Distributor.MobileNo,                        
                        Phone= Distributor.Phone,                        
                        Fax = Distributor.Fax,                        
                        Email= Distributor.Email,                        
                        State = Distributor.State,                        
                        District = Distributor.District,                        
                        Address = Distributor.Address,                        
                        Latitude = Distributor.Latitude,                        
                        Longitude = Distributor.Longitude,
                        IsDeleted = Distributor.IsDeleted
                    }).ToList().OrderBy(Dist => Dist.DistrubitorName).ToList();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Save(Distributor_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new Distributor()
                    {
                        DistrubitorID = GetNewDistributorID(),
                        DistrubitorName = model.DistrubitorName,
                        OwnerName = model.OwnerName,
                        RegestrationID = model.RegestrationID,
                        MobileNo = model.MobileNo,
                        Phone = model.Phone,
                        Fax = model.Fax,
                        Email = model.Email,
                        State = model.State,
                        District = model.District,
                        Address = model.Address,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                    };
                    _context.Distributors.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Update(Distributor_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Distributors.Where(dist => dist.DistrubitorID== model.DistrubitorID).FirstOrDefault();
                    data.DistrubitorID = model.DistrubitorID;
                    data.DistrubitorName = model.DistrubitorName;
                    data.OwnerName = model.OwnerName;
                    data.RegestrationID = model.RegestrationID;
                    data.MobileNo = model.MobileNo;
                    data.Phone = model.Phone;
                    data.Fax = model.Fax;
                    data.Email = model.Email;
                    data.State = model.State;
                    data.District = model.District;
                    data.Address = model.Address;
                    data.Latitude = model.Latitude;
                    data.Longitude = model.Longitude;
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
