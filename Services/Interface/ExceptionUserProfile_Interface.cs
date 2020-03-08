using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ExceptionUserProfile_Interface
    {
        bool BulkDeleteExeceptionByUserID(Int64 userId);
    }
}
