using DataAccessLayer;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ExceptionUserProfile_Service : ExceptionUserProfile_Interface
    {
        public bool BulkDeleteExeceptionByUserID(long userId)
        {
            using (var _context = new SalesTrackingSystemEntities())
            {
                try
                {
                    _context.ExceptionUserProfiles.Where(act => act.UserID == userId).ToList().ForEach(varialbe => _context.ExceptionUserProfiles.Remove(varialbe));
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
