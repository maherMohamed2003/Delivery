using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        public int ClientID { get; set; }
        public Client client { get; set; }
        public int DriverID { get; set; }
        public Driver driver { get; set; }
        public ICollection<ShipmentStatus> shipmentStatuses { get; set; }
        public Payment payment { get; set; }
    }
}
