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
        public List<Distributor_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from Distributor in _dbContext.Distributors                              
                                select new Distributor_Model()
                                {
                                    DistrubitorID = Distributor.DistrubitorID,
                                    DistrubitorName = Distributor.DistrubitorName,
                                    OwnerName = Distributor.OwnerName,
                                    RegestrationID = Distributor.RegestrationID,
                                    MobileNo = Distributor.MobileNo,
                                    Phone = Distributor.Phone,
                                    Email = Distributor.Email,
                                    Fax = Distributor.Fax,
                                    State = Distributor.State,
                                    District = Distributor.District,
                                    Address = Distributor.Address,
                                    Latitude = Distributor.Latitude,
                                    Longitude = Distributor.Longitude,
                                    DateCreated = Distributor.DateCreated,
                                    DateUpdated = Distributor.DateUpdated,
                                  
                                }).ToList().OrderBy(Distributor => Distributor.DistrubitorName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
