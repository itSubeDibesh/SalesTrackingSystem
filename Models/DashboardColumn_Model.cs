using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DashboardColumn_Model
    {
        public long DashboardColumnId { get; set; }
        public Nullable<long> DashboardTableId { get; set; }
        public string Color { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<long> DashboardID { get; set; }
        public Nullable<long> DashboardGivenColumnId { get; set; }

    }
}
