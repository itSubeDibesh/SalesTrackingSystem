using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DistributorArea_Model
    {
        public long DistributorAreaID { get; set; }

        [Required(ErrorMessage = "Distrubitor Name required")]
        [Display(Name = "Distrubitor Name")]
        public Nullable<long> DistrubitorID { get; set; }

        [Required(ErrorMessage = "State required")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "District required")]
        [Display(Name = "District")]
        public string District { get; set; }

        [Required(ErrorMessage = "City required")]
        [Display(Name = "City")]
        public string City { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string DistrubitorName { get; set; }
    }
}
