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
    public class Unit_Service : Unit_Interface
    {
        public bool Delete(long unitID)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Units.Where(dist => dist.UnitId == unitID).FirstOrDefault();
                    _context.Units.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Unit_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from Unit in _dbContext.Units
                                select new Unit_Model()
                                {
                                    UnitId = Unit.UnitId,
                                    UnitAbb = Unit.UnitAbb,
                                    UnitName = Unit.UnitName,
                                    Description = Unit.Description,
                                    DateCreated = Unit.DateCreated,
                                    DateUpdated = Unit.DateUpdated,

                                }).ToList().OrderBy(Unit => Unit.UnitName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int GetNewUnitID()
        {
            try
            {
                using (var _context = new SalesTrackingSystemEntities())
                {
                    var data = _context.Units.Max(Unit => Unit.UnitId);
                    int id = Convert.ToInt32(data) + 1;
                    return id;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public Unit_Model GetUnitById(long unitID)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Units.Where(Unit => Unit.UnitId == unitID).Select(Unit => new Unit_Model()
                    {
                        UnitId = Unit.UnitId,
                        UnitAbb = Unit.UnitAbb,
                        UnitName = Unit.UnitName,
                        Description = Unit.Description,
                        DateCreated = Unit.DateCreated,
                        DateUpdated = Unit.DateUpdated
                    }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Save(Unit_Model unit_Model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new Unit()
                    {
                        UnitId = GetNewUnitID(),
                        UnitAbb = unit_Model.UnitAbb,
                        UnitName = unit_Model.UnitName,
                        Description = unit_Model.Description,
                        DateCreated = DateTime.Now
                    };
                    _context.Units.Add(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UnitExist(long unitID)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from unit in _dbContext.Units.Where(unit => unit.UnitId == unitID)
                                select new Unit_Model()
                                {
                                    UnitId = unit.UnitId,
                                    UnitName = unit.UnitName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.UnitName) && unitID != data.UnitId)
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

        public bool UpdateUnit(Unit_Model unit_Model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Units.Where(unit => unit.UnitId == unit_Model.UnitId).FirstOrDefault();
                    data.UnitId = unit_Model.UnitId;
                    data.UnitName = unit_Model.UnitName;
                    data.UnitAbb = unit_Model.UnitAbb;
                    data.Description = unit_Model.Description;
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

