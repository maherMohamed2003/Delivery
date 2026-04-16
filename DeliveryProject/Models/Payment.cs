using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }

        public int ShipmentID { get; set; }
        public Shipment shipment { get; set; }

        public int ClientID { get; set; }
        public Client client { get; set; }
    }
}
