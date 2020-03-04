using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ModuleAction_Interface
    {
        List<ModuleAction_Model> DisplayTable();
        bool SaveAction(ModuleAction_Model action);
        bool ActionExists(Int64 actionId);
        ModuleAction_Model ActionByID(Int64 actionId);
        bool UpdateAction(ModuleAction_Model action);
        bool DeleteAction(Int64 actionId);
    }
}
