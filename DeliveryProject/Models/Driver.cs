using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Driver
    {
        public int DriverID { get; set; }
        public string LicenseNumber { get; set; }
        public string status { get; set; }

        public int UserID { get; set; }
        public User user { get; set; }

        public int VehicleID { get; set; }
        public Vehicle vehicle { get; set; }

        public int ZoneID { get; set; }
        public Zone zone { get; set; }

        public ICollection<Shipment> shipments { get; set; }
    }
}
