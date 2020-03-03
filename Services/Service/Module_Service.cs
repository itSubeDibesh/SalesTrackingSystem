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
    public class Module_Service : Module_Interface
    {
        public bool DeleteModule(long moduleId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Modules.Where(ModuleActions => ModuleActions.ModuleID == moduleId).FirstOrDefault();
                    _context.Modules.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Module_Model> DisplayTable()
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _dbContext.Modules.Select(ModuleAction => new Module_Model()
                    {
                       ModuleID= ModuleAction.ModuleID,
                       ModuleName= ModuleAction.ModuleName,
                       ControllerName= ModuleAction.ControllerName,
                       ModuleStatus=ModuleAction.ModuleStatus,
                       Description=ModuleAction.Description                     
                    }).ToList().OrderBy(ModuleAction=> ModuleAction.ModuleName).ToList();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Module_Model ModuleByID(long moduleId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from ModuleActions in _dbContext.Modules.Where(ModuleActions => ModuleActions.ModuleID == moduleId)
                                select new Module_Model()
                                {
                                    ModuleID = ModuleActions.ModuleID,
                                    ModuleName = ModuleActions.ModuleName,
                                    ModuleStatus=ModuleActions.ModuleStatus,
                                    ControllerName=ModuleActions.ControllerName,
                                    Description = ModuleActions.Description
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool ModuleExists(long moduleId)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = (from ModuleActions in _dbContext.Modules.Where(ModuleActions => ModuleActions.ModuleID == moduleId)
                                select new Module_Model()
                                {
                                    ModuleID = ModuleActions.ModuleID,
                                    ModuleName=ModuleActions.ModuleName
                                   
                                }).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(data.ModuleName) && moduleId != data.ModuleID)
                    {
                        return false;
                    }
                    else{
                        return true;
                    }                   
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool SaveModule(Module_Model model)
        {
            using (var _dbContext = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = new Module()
                    {
                        ModuleName = model.ModuleName,
                        ControllerName = model.ControllerName,
                        ModuleStatus=model.ModuleStatus,
                        Description = model.Description
                    };
                    _dbContext.Modules.Add(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateModule(Module_Model model)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    var data = _context.Modules.Where(ActionModules => ActionModules.ModuleID == model.ModuleID).FirstOrDefault();
                    data.ModuleName = model.ModuleName;
                    data.ControllerName = model.ControllerName;
                    data.ModuleStatus = model.ModuleStatus;
                    data.Description = model.Description;
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
