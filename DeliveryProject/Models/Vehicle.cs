using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string VehicleType { get; set; }
        public string PlateNumber { get; set; }
        public string Status { get; set; }

        public ICollection<Driver> drivers { get; set; }
    }
}
