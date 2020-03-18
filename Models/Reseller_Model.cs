using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Reseller_Model
    {
        public long ResellerID { get; set; }
        public string ResellerName { get; set; }
        public string OwnerName { get; set; }
        public string RegestrationID { get; set; }
        public Nullable<long> DistrubitorID { get; set; }
        public string DistributorName { get; set; }
        public Nullable<long> Mobile { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
