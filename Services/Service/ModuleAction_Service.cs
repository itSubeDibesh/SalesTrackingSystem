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
    public class ModuleAction_Service : ModuleAction_Interface
    {
        public bool DeleteAction(long actionId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.ModuleActions.Where(moduleAction => moduleAction.ModuleActionID == actionId).FirstOrDefault();
                    _context.ModuleActions.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
            public List<ModuleAction_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from moduleAction in _dbContext.ModuleActions
                                join module in _dbContext.Modules on moduleAction.ModuleID equals module.ModuleID
                                select new ModuleAction_Model()
                                {
                                  ModuleActionID=moduleAction.ModuleActionID,
                                  ModuleID=moduleAction.ModuleID,
                                  ActionName=moduleAction.ActionName,
                                  ActionStatus=moduleAction.ActionStatus,
                                  Description=moduleAction.Description,
                                  ModuleName = module.ModuleName,
                                  ControllerName = module.ControllerName,
                                  ModuleStatus=module.ModuleStatus
                                }).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool ActionExists(long actionId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from moduleActions in _dbContext.ModuleActions.Where(moduleActions => moduleActions.ModuleActionID == actionId)
                                select new ModuleAction_Model()
                                {
                                    ModuleActionID = moduleActions.ModuleActionID,
                                    ActionName = moduleActions.ActionName

                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.ActionName) && actionId != data.ModuleActionID)
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

        public ModuleAction_Model ActionByID(long actionId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from moduleActions in _dbContext.ModuleActions.Where(moduleActions => moduleActions.ModuleActionID == actionId)
                                select new ModuleAction_Model()
                                {
                                    ModuleActionID = moduleActions.ModuleActionID,
                                    ModuleID = moduleActions.ModuleID,
                                    ActionName = moduleActions.ActionName,
                                    ActionStatus = moduleActions.ActionStatus,
                                    Description = moduleActions.Description,                  
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool SaveAction(ModuleAction_Model action)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new ModuleAction()
                    {                       
                        ModuleID = action.ModuleID,
                        ActionName = action.ActionName,
                        ActionStatus = action.ActionStatus,                                          
                        Description = action.Description
                    };
                    _dbContext.ModuleActions.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateAction(ModuleAction_Model action)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.ModuleActions.Where(moduleActions => moduleActions.ModuleActionID == action.ModuleActionID).FirstOrDefault();
                    data.ModuleID = action.ModuleID;
                    data.ActionName = action.ActionName;
                    data.ActionStatus = action.ActionStatus;
                    data.Description = action.Description;
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
