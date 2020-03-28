using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Reseller_Model
    {
        public long ResellerID { get; set; }

        [Required(ErrorMessage = "Reseller Name required")]
        [Display(Name = "Reseller Name")]
        public string ResellerName { get; set; }

        [Required(ErrorMessage = "Owner Name required")]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Regestration No required")]
        [Display(Name = "Regestration No")]
        public string RegestrationID { get; set; }

        [Required(ErrorMessage = "Distrubitor Name required")]
        [Display(Name = "Distrubitor Name")]
        public Nullable<long> DistrubitorID { get; set; }

        [Required(ErrorMessage = "Mobile No required")]
        [Display(Name = "Mobile No")]
        public Nullable<long> Mobile { get; set; }
        public long Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "State required")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "District required")]
        [Display(Name = "District")]
        public string District { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string DistrubitorName { get; set; }
    }
}
