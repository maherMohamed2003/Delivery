using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class ShipmentStatus
    {
        public int StatusID { get; set; }
        public string StatusValue { get; set; }
        public DateTime ChangeAt { get; set; }

        public int ShipmentID { get; set; }
        public Shipment shipment { get; set; }


    }
}
