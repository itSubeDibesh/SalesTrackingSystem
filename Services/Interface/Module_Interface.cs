using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Module_Interface
    {
        List<Module_Model> DisplayTable();
        bool SaveModule(Module_Model model);
        bool ModuleExists(Int64 moduleId);
        Module_Model ModuleByID(Int64 moduleId);
        bool UpdateModule(Module_Model model);
        bool DeleteModule(Int64 moduleId);
    }
}
